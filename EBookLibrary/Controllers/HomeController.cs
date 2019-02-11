using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EBookLibraryData.Models.ViewModels.Home;
using EBookLibraryServices;
using EBookLibraryData.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using static EBookLibraryServices.BooksManageService;

namespace EBookLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBooksManage _manage;
        private readonly IQueue _queue;
        private readonly ILoanHistory _loanHistory;
        private readonly ILoans _loans;
        public HomeController(IBooksManage manage,
            UserManager<ApplicationUser> userManager,
            IQueue queue,
            ILoanHistory loanHistory,
            ILoans loans)
        {
            _manage = manage;
            _userManager = userManager;
            _queue = queue;
            _loanHistory = loanHistory;
            _loans = loans;
        }

        public IActionResult Index()
        {
            return View(new IndexViewModel {
                MostRecent = _manage.GetMostRecentBooks(),
                MostPopular = _manage.GetMostPopularBooks()
            });
        }
        [HttpGet]
        public IActionResult Index(IndexViewModel model)
        {
            model.MostRecent = _manage.GetMostRecentBooks();
            model.MostPopular = _manage.GetMostPopularBooks();
            return View(model);
        }

        public IActionResult BrowseBooks()
        {
            return View(new BrowseBooksViewModel
            {
                Categories = _manage.GetAllCategories()
            });
        }

        [Authorize]
        [HttpGet]
        [Route("Home/GetBook/{file}")]
        public async Task<IActionResult> GetBook(string file)
        {
            if(file != null)
            {
                var book = _manage.GetBookByPath(file);
                if (book != null)
                {
                    var loans = _loans.GetLoansByBook(book);
                    if (loans != null)
                    {
                        var user = await _userManager.GetUserAsync(HttpContext.User);
                        foreach (var loan in loans)
                        {
                            if (loan.UserId.Equals(user.Id))
                            {
                                var path = Path.Combine(
                                               Directory.GetCurrentDirectory(),
                                               "library_assets//Book", file);

                                var memory = new MemoryStream();
                                using (var stream = new FileStream(path, FileMode.Open))
                                {
                                    await stream.CopyToAsync(memory);
                                }
                                memory.Position = 0;
                                return File(memory, "application/pdf", Path.GetFileName(path));
                            }
                        }
                    }
                }
            }
            return new NotFoundResult();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LeaveQueue(BookPreviewViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var bookPreviewModel = new BookPreviewViewModel
            {
                BookId = model.BookId,
                BookRented = false,
                BookRent = false,
                JoinQueueError = false
            };
            if (_queue.CheckIfAlreadyInQueue(user, _manage.GetById((int)model.BookId)))
            {
                if (_queue.RemoveQueue(_queue.GetQueue(user, _manage.GetById((int)model.BookId))))
                {
                    bookPreviewModel.InQueue = false;
                }
            }

            var referer = new string(Request.Headers["Referer"].ToString().Reverse().ToArray());
            string actionName = string.Empty;
            foreach(var i in referer)
            {
                if (i.Equals('/'))
                {
                    break;
                }
                actionName += i;
            }
            if (new string(actionName.Reverse().ToArray()).Equals("InQueue"))
            {
                return RedirectToAction("InQueue", "AccountManage");
            }
            return RedirectToAction("BookPreview", bookPreviewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> JoinQueue(BookPreviewViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            model.JoinQueueError = false;
            var bookPreviewModel = new BookPreviewViewModel
            {
                BookId = model.BookId,
                InQueue = false,
                BookRented = false,
                BookRent = false
            };
            if (!_queue.CheckIfAlreadyInQueue(user, _manage.GetById((int)model.BookId)))
            {
                if (_queue.AddQueue(user, _manage.GetById((int)model.BookId)))
                {
                    bookPreviewModel.InQueue = true;
                }
                else model.JoinQueueError = true;
            }
            else model.JoinQueueError = true;
            return RedirectToAction("BookPreview", bookPreviewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ReturnBook(LoanViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            model.BookRented = false;
            if(_manage.RemoveUserLoan(user, _manage.GetById((int)model.BookId)))
            {
                _manage.IncrementBookCopy(_manage.GetById((int)model.BookId));
                _manage.ChangeBookLoansNumberByOne((int)model.BookId, Operation.Increment);
            }
            var bookPreviewModel = new BookPreviewViewModel
            {
                BookId = model.BookId,
                BookRented = false,
                BookRent = false,
                JoinQueueError = false
            };
            var loanHistory = _loanHistory.GetLoanHistory(user, _manage.GetById((int)model.BookId));
            if (loanHistory != null) _loanHistory.ModifyReturnDate(DateTime.Now, loanHistory);

            var referer = new string(Request.Headers["Referer"].ToString().Reverse().ToArray());
            string actionName = string.Empty;
            foreach (var i in referer)
            {
                if (i.Equals('/'))
                {
                    break;
                }
                actionName += i;
            }
            if (new string(actionName.Reverse().ToArray()).Equals("Loaned"))
            {
                return RedirectToAction("Loaned", "AccountManage");
            }
            return RedirectToAction("BookPreview", bookPreviewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LoanBook(LoanViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var book = _manage.GetById((int)model.BookId);
            model.LoanError = false;
            model.BookRented = false;
            model.BookNotAvailable = false;
            if (book != null)
            {
                Copy copy = null;
                try
                {
                    copy = _manage.GetAvailableBookCopies(book).FirstOrDefault();
                }
                catch(ArgumentNullException)
                {

                }

                if (copy != null)
                {
                    var loan = new Loan
                    {
                        User = user,
                        UserId = user.Id,
                        Copy = copy,
                        CopyId = copy.CopyId,
                        EndDate = model.EndDate,
                        StartDate = DateTime.Now
                    };
                    if (_manage.AddLoan(loan))
                    {
                        if(_manage.DecrementBookCopy(book))
                        {
                            _manage.ChangeCopyRentedStatus(copy);
                            _loanHistory.AddLoanHistory(loan, user, _manage.GetById((int)model.BookId));
                            _manage.ChangeBookLoansNumberByOne(book.BookId, Operation.Decrement);
                            return RedirectToAction("BookPreview", new BookPreviewViewModel
                            {
                                BookId = model.BookId,
                                BookRented = true,
                                LoanError = true,
                                JoinQueueError = false
                            });
                        }
                    }
                }
                return RedirectToAction("BookPreview", new BookPreviewViewModel
                {
                    BookId = model.BookId,
                    BookRented = false,
                    BookNotAvailable = true,
                    JoinQueueError = false
                });
            }
            return new NoContentResult();
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> BookPreview(BookPreviewViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (model.BookId != null)
            {
                model.BookRent = false;
                model.InQueue = false;
                model.Book = _manage.GetById((int)model.BookId);
                model.JoinQueueError = false;
                if (model.Book != default(Book))
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        var userCopies = _manage.GetUserLoanCopies(user);
                        if (userCopies != null && userCopies.Any())
                        {
                            foreach (var userCopy in userCopies)
                            {
                                if (userCopy.BookId.Equals(model.BookId))
                                {
                                    model.BookRent = true;
                                    var loan = _manage.GetLoanByCopy(userCopy);
                                    if(loan != null)
                                    {
                                        if(loan.EndDate <= DateTime.Today)
                                        {
                                            _manage.RemoveUserLoan(user, model.Book);
                                            model.BookRent = false;
                                            model.BookRented = false;
                                        }
                                    }
                                }
                            }
                        }
                        #region book loan if in queue
                        var userQueue = _queue.GetQueue(user, model.Book);
                        if (userQueue != null)
                        {
                            model.InQueue = true;
                            var queues = _queue.GetQueuesForBook(model.Book);
                            if (queues.ElementAt(0).JoinDate < userQueue.JoinDate) model.InQueue = true;
                            else if(queues.ElementAt(0).JoinDate >= userQueue.JoinDate)
                            {
                                var copies = _manage.GetAvailableBookCopies(model.Book);
                                if(copies != null)
                                {
                                    if (_manage.AddLoan(new Loan
                                    {
                                        Copy = copies.FirstOrDefault(),
                                        User = user,
                                        StartDate = DateTime.Now,
                                        EndDate = model.EndDate
                                    }))
                                    {
                                        if (_queue.RemoveQueue(userQueue))
                                        {
                                            model.InQueue = true;
                                        }
                                        else model.InQueue = false;
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    return View(model);
                }
                else return new NotFoundResult();
            }
            else return new NotFoundResult();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Reader(BookPreviewViewModel model)
        {
            return View(model);
        }

        [HttpGet]
        public IActionResult BrowseBooks(BrowseBooksViewModel model)
        {
            if(!model.PagesRangeSelected)
            {
                model.PagesMin = null;
                model.PagesMax = null;
            }
            if (model.Category == null)
            {
                var firstCategory = _manage.GetAllCategories().FirstOrDefault();
                if (firstCategory != default(Category))
                {
                    model.Category = firstCategory.Name;
                }
            }
            model.Categories = _manage.GetAllCategories();
            model.Authors = _manage.GetAllAuthors();
            model.Books = _manage.GetBooks(model.Title, model.ISBN, model.Author, model.PagesMin,
                model.PagesMax, model.Publisher, model.Category);

            #region pagination

            var elementsCount = 0;
            if (!(model.Books is null))
            {
                elementsCount = model.Books.Count();
            }
            var allPagesCount = elementsCount / model.ElementsOnPage;
            var elementsToTake = model.ElementsOnPage;
            if (elementsCount % model.ElementsOnPage !=0) allPagesCount++;
            if (model.Page < 1) model.Page = 1;
            if (model.Page > allPagesCount) model.Page = model.AllPagesCount;
            model.AllPagesCount = allPagesCount;
            if (model.Page.Equals(model.AllPagesCount))
            {
                elementsToTake = elementsCount - ((model.AllPagesCount - 1) * model.ElementsOnPage);
            }
            if(elementsCount > 0)
            {
                model.Books = model.Books.Skip((model.Page - 1) * model.ElementsOnPage).Take(elementsToTake);
                model.AnyElements = true;
            }
            else model.AnyElements = false;
            if(elementsCount <= model.ElementsOnPage)
            {
                model.MoreThanOnePage = false;
            }

            #endregion

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
