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

namespace EBookLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBooksManage _manage;
        public HomeController(IBooksManage manage,
            UserManager<ApplicationUser> userManager)
        {
            _manage = manage;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(new IndexViewModel());
        }
        [HttpGet]
        public IActionResult Index(IndexViewModel model)
        {
            return View(model);
        }

        public IActionResult BrowseBooks()
        {
            return View(new BrowseBooksViewModel
            {
                Categories = _manage.GetAllCategories()
            });
        }

        [HttpPost]
        public async Task<IActionResult> ReturnBook(LoanViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            model.BookRented = false;
            _manage.RemoveUserLoan(user, _manage.GetById((int)model.BookId));
            return RedirectToAction("BookPreview", new BookPreviewViewModel {
                BookId = model.BookId,
                BookRented = false,
                BookRent = false
            });
        }

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
                var copy = _manage.GetAvailableBookCopies(book).FirstOrDefault();
                if (copy != default(Copy))
                {
                    var loan = new Loan
                    {
                        User = user,
                        UserId = user.Id,
                        Copy = copy,
                        CopyId = copy.CopyId,
                        LoanDurationDays = 7,//tutaj to zmienić
                        StartDate = DateTime.Now//Dodać jeszcze sprawdzanie czy wyporzyczenie nie ubiegło końca przy wyświetlaniu preview na przykład najprościej
                        //Dodać filtrowanie po dostępności w book preview i może w managerze dodawania oraz dodać w managerze pole ilość kopii
                    };
                    if (_manage.AddLoan(loan))
                    {
                        if(_manage.DecrementBookCopy(book))
                        {
                            return RedirectToAction("BookPreview", new BookPreviewViewModel
                            {
                                BookId = model.BookId,
                                BookRented = true,
                                LoanError = true
                            });
                        }
                    }
                }
                return RedirectToAction("BookPreview", new BookPreviewViewModel
                {
                    BookId = model.BookId,
                    BookRented = false,
                    BookNotAvailable = true,
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
                model.Book = _manage.GetById((int)model.BookId);
                if (model.Book != default(Book))
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        var userCopies = _manage.GetUserLoanCopies(user);
                        if (userCopies != null && userCopies.Any())
                        {
                            foreach (var i in userCopies)
                            {
                                if (i.BookId.Equals(model.BookId))//tu może się coś jebać przez to że BookId mogło nie wyciągnąć
                                {
                                    model.BookRent = true;
                                }
                            }
                        }
                    }
                    return View(model);
                }
                else return new NotFoundResult();
            }
            else return new NotFoundResult();
        }

        //[HttpPost]
        //public async Task<IActionResult> BookPreview(int BookId)
        //{
        //    var user = await _userManager.GetUserAsync(HttpContext.User);
        //    var book = _manage.GetById(BookId);
        //    var model = new BookPreviewViewModel
        //    {
        //        Book = book
        //    };
        //    var bookCopies = _manage.GetBookCopies(book);
        //    model.BookRent = false;
        //    if (bookCopies.Any())
        //    {
        //        foreach (var i in bookCopies)
        //        {
        //            if (i.UserId.Equals(user.Id))
        //            {
        //                model.BookRent = true;
        //            }
        //        }
        //    }
        //    if (book != default(Book))
        //    {
        //        return View(model);
        //    }
        //    else return new NotFoundResult();
        //}
        //[HttpPost]
        //public async Task<IActionResult> LoanBook(BookPreviewViewModel model)
        //{
        //    if (model.Book != default(Book))
        //    {
        //        model.LoanError = false;
        //        model.BookNotAvailable = false;
        //        var availableCopies = _manage.GetAvailableBookCopies(model.Book);
        //        if (availableCopies.Any())
        //        {
        //            var user = await _userManager.GetUserAsync(HttpContext.User);
        //            var copyToRent = availableCopies.First();
        //            var loan = new Loan
        //            {
        //                Copy = copyToRent,
        //                CopyId = copyToRent.CopyId,
        //                User = user,
        //                UserId = user.Id
        //            };
        //            if (!_manage.AddLoan(loan))
        //            {
        //                model.LoanError = true;
        //            }
        //        }
        //        else model.BookNotAvailable = true;
        //    }
        //    return RedirectToAction("BookPreview", model);
        //}

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
            model.Books = _manage.GetBooks(model.Title, model.ISBN, model.Author, model.PagesMin,
                model.PagesMax, model.Publisher, model.Category);

            model.Categories = _manage.GetAllCategories();

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
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
