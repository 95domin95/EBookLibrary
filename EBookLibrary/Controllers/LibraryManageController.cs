using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EBookLibraryData;
using EBookLibraryData.Models;
using EBookLibraryData.Models.JsonBinding;
using EBookLibraryData.Models.ViewModels.LibraryManage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EBookLibrary.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LibraryManageController : Controller
    {
        private readonly IBooksManage _manage;
        private readonly IQueue _queue;
        private readonly ILoanHistory _loanHistory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthors _author;
        private readonly IPublishers _publisher;
        private readonly ICategory _category;
        private readonly ILoans _loans;

        public LibraryManageController(IBooksManage manage,
            IQueue queue,
            ILoanHistory loanHistory,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAuthors author,
            IPublishers publisher,
            ICategory category,
            ILoans loans)
        {
            _manage = manage;
            _queue = queue;
            _userManager = userManager;
            _signInManager = signInManager;
            _loanHistory = loanHistory;
            _author = author;
            _publisher = publisher;
            _category = category;
            _loans = loans;
        }

        private readonly List<string[]> operations = new List<string[]>()
        {
            new string[]{ "Dodaj nową książkę", "add" },
            new string[]{ "Modyfikuj dane książki", "update" },
            new string[]{ "Usuń książkę", "delete" },
            new string[]{ "Wyszukaj książki", "select" }
        };

        private readonly List<string[]> menuOptions = new List<string[]>() {
            new string[]{
                "Books",
                "Książki",
            },
            new string[]{
                "Authors",
                "Autorzy"
            },
            new string[]{
                "Publishers",
                "Wydawcy"
            },
            new string[]{
                "Users",
                "Użytkownicy"
            },
            new string[]{
                "Loans",
                "Wyporzyczenia",
            },
            new string[]{
                "Queues",
                "Kolejki"
            },
            new string[]{
                "Categories",
                "Kategorie"
            },
            new string[]{
                "LoanHistories",
                "Historie wyporzyczeń"
            },
            new string[]{
                "Stats",
                "Statystyki",
            }
        };
        enum Option
        {
            Name,
            CommonName,
            Books = 0,
            Authors,
            Publishers,
            Users,
            Loans,
            Queues,
            Categories,
            LoanHistories,
            Stats
        };

        enum OperationError
        {
            add = 0,
            remove = 1,
            modify = 2
        };

        [HttpGet]
        public IActionResult ManagePanel()
        {
            ViewData["Title"] = "Książki";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Books][(int)Option.Name];
            return View(new ManagePanelViewModel {
                Categories = _manage.GetAllCategories(),
                Operations = operations,
                Authors = _manage.GetAllAuthors()
        });
        }

        [HttpPost]
        public IActionResult AddNewAuthor([FromBody]NewAuthor newAuthor)
        {
            if (newAuthor != null)
            {
                if (_manage.AddAuthor(newAuthor.Name)) return new JsonResult("Success");
            }
            return new JsonResult("Failed");
        }

        [HttpPost]
        public IActionResult DeleteBook(string id, ManagePanelViewModel model)
        {
            if (!(model.Id == null || model.Id.Equals(string.Empty)))
            {
                if (!_manage.DeleteById((int)model.Id))
                {
                    model.OperationErrorName = model.Errors.ElementAt((int)OperationError.remove);
                }
            }
            else model.OperationErrorName = model.Errors.ElementAt((int)OperationError.remove);
            model.BookRemoved = true;

            return RedirectToAction("ManagePanel", model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ManagePanel(ManagePanelViewModel model)
        {
            ViewData["Title"] = "Książki";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Books][(int)Option.Name];
            model.BookAdded = false;
            model.BookModified = false;
            model.BookRemoved = false;
            model.BookSearched = false;
            model.OperationErrorName = string.Empty;
            model.Authors = _manage.GetAllAuthors();
            if (!model.PagesRangeSelected)
            {
                model.PagesMin = null;
                model.PagesMax = null;
            }

            switch (model.OperationType)
            {
                case "add":
                    if (!await _manage.Add(model.Title, model.ISBN, model.Pages,
                        model.Author, model.Publisher, model.Category,
                        model.Book, model.BookCovering, model.CopiesCount))
                    {
                        model.OperationErrorName = model.Errors.ElementAt((int)OperationError.add);
                    }
                    model.BookAdded = true;
                    break;
                case "update":
                    if(!_manage.UpdateById(model.Id, model.Title, model.ISBN, 
                        model.Author, model.Pages, model.Publisher, model.Category))
                    {
                        model.OperationErrorName = model.Errors.ElementAt((int)OperationError.modify);
                    }
                    model.BookModified = true;
                    break;
                case "delete":
                    if(!(model.Id == null||model.Id.Equals(string.Empty)))
                    {
                        if(!_manage.DeleteById((int)model.Id))
                        {
                            model.OperationErrorName = model.Errors.ElementAt((int)OperationError.remove);
                        }
                    }
                    else model.OperationErrorName = model.Errors.ElementAt((int)OperationError.remove);
                    model.BookRemoved = true;
                    break;
                case "select":
                    if (model.Id == null||model.Id.Equals(string.Empty))
                    {
                        //Wyszukiwanie po właściwościach
                        model.BookSearched = true;

                        if (model.Category == null)
                        {
                            var firstCategory = _manage.GetAllCategories().FirstOrDefault();
                            if (firstCategory != default(Category))
                            {
                                model.Category = firstCategory.Name;
                            }
                        }

                        model.Books = _manage.GetBooks(model.Title, model.ISBN, model.Author,
                            model.PagesMin, model.PagesMax, model.Publisher, model.Category, model.ElementsToshow);

                        if(model.Availability)
                        {
                            model.Books = model.Books.Where(b => b.CopiesCount > 0);
                        }
                    }
                    else
                    {
                        //Wyszukiwanie konkretnej książki po Id
                        model.BookSearched = true;
                        var book = _manage.GetById((int)model.Id);
                        if(book != null)
                        {
                            model.Books = new List<Book>
                            {
                                book
                            };
                        }
                    }
                    break;
                default:
                    break;
            }
            model.Operations = operations;
            model.Categories = _manage.GetAllCategories();
            return View(model);
        }

        [HttpGet]
        public IActionResult Authors()
        {
            ViewData["Title"] = "Autorzy";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Authors][(int)Option.Name];
            return View(new AuthorsManageViewModel
            {
                Authors = _author.GetMany(1000),
                Take = 1000
            });
        }

        [HttpPost]
        public IActionResult Authors(AuthorsManageViewModel model)
        {
            ViewData["Title"] = "Autorzy";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Authors][(int)Option.Name];
            model.RemovedSuccessfully = false;
            model.AddedSuccessfully = false;
            model.AddError = false;
            model.RemoveError = false;
            if (model.Name != null)
            {
                if(_author.Add(model.Name))
                {
                    model.AddedSuccessfully = true;
                }
                else model.AddError = true;
            }
            else if(model.AuthorId != null)
            {
                if (_author.Remove((int)model.AuthorId))
                {
                    model.RemovedSuccessfully = true;
                }
                else model.RemoveError = true;
            }
            model.Take = 1000;
            model.Authors = _author.GetMany(model.Take);
            model.AuthorId = null;
            model.Name = null;
            return View(model);
        }

        [HttpGet]
        public IActionResult Publishers()
        {
            ViewData["Title"] = "Wydawcy";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Publishers][(int)Option.Name];
            return View(new PublisherManageViewModel
            {
                Publishers = _publisher.GetMany(),
                Take = 1000
            });
        }

        [HttpPost]
        public IActionResult Publishers(PublisherManageViewModel model)
        {
            ViewData["Title"] = "Wydawcy";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Publishers][(int)Option.Name];
            model.RemovedSuccessfully = false;
            model.AddedSuccessfully = false;
            model.AddError = false;
            model.RemoveError = false;
            if (model.Name != null)
            {
                if (_publisher.Add(model.Name, model.City))
                {
                    model.AddedSuccessfully = true;
                }
                else model.AddError = true;
            }
            else if (model.PublisherId != null)
            {
                if (_publisher.Remove((int)model.PublisherId))
                {
                    model.RemovedSuccessfully = true;
                }
                else model.RemoveError = true;
            }
            model.Take = 1000;
            model.Publishers = _publisher.GetMany(model.Take);
            model.PublisherId = null;
            model.Name = null;
            return View(model);
        }

        [HttpGet]
        public IActionResult Categories()
        {
            ViewData["Title"] = "Kategorie";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Categories][(int)Option.Name];
            return View(new CategoriesManageViewModel
            {
                Categories = _category.GetMany(1000),
                Take = 1000
            });
        }

        [HttpPost]
        public IActionResult Categories(CategoriesManageViewModel model)
        {
            ViewData["Title"] = "Kategorie";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Categories][(int)Option.Name];
            model.RemovedSuccessfully = false;
            model.AddedSuccessfully = false;
            model.AddError = false;
            model.RemoveError = false;

            if (model.Name != null)
            {
                if (_category.Add(model.Name))
                {
                    model.AddedSuccessfully = true;
                }
                else model.AddError = true;
            }
            else if (model.CategoryId != null)
            {
                if (_category.Remove((int)model.CategoryId))
                {
                    model.RemovedSuccessfully = true;
                }
                else model.RemoveError = true;
            }
            model.Take = 1000;
            model.Categories = _category.GetMany(model.Take);
            model.CategoryId = null;
            model.Name = null;
            return View(model);
        }

        [HttpGet]
        public IActionResult Queues()
        {
            ViewData["Title"] = "Kolejki";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Queues][(int)Option.Name];
            return View(new QueuesManageViewModel
            {
                Queues = _queue.GetMany(1000),
                Take = 1000
            });
        }

        [HttpPost]
        public IActionResult Queues(QueuesManageViewModel model)
        {
            ViewData["Title"] = "Kolejki";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Queues][(int)Option.Name];
            model.RemovedSuccessfully = false;
            model.RemoveError = false;

            if (model.QueueId != null)
            {
                if (_category.Remove((int)model.QueueId))
                {
                    model.RemovedSuccessfully = true;
                }
                else model.RemoveError = true;
            }
            model.Take = 1000;
            model.Queues = _queue.GetMany(model.Take);
            model.QueueId = null;
            return View(model);
        }

        [HttpGet]
        public IActionResult LoanHistories()
        {
            ViewData["Title"] = "Historie wyporzyczeń";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.LoanHistories][(int)Option.Name];
            return View(new LoanHistoriesManageViewModel
            {
                LoanHistories = _loanHistory.GetMany(1000),
                Take = 1000
            });
        }

        [HttpPost]
        public IActionResult LoanHistories(LoanHistoriesManageViewModel model)
        {
            ViewData["Title"] = "Historie wyporzyczeń";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.LoanHistories][(int)Option.Name];
            model.RemovedSuccessfully = false;
            model.RemoveError = false;

            if (model.LoanHistoryId != null)
            {
                if (_category.Remove((int)model.LoanHistoryId))
                {
                    model.RemovedSuccessfully = true;
                }
                else model.RemoveError = true;
            }
            model.Take = 1000;
            model.LoanHistories = _loanHistory.GetMany(model.Take);
            model.LoanHistoryId = null;
            return View(model);
        }

        [HttpGet]
        public IActionResult Loans()
        {
            ViewData["Title"] = "Wyporzyczenia";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Loans][(int)Option.Name];
            return View(new LoansManageViewModel
            {
                Loans = _loans.GetMany(1000),
                Take = 1000
            });
        }

        [HttpPost]
        public IActionResult Loans(LoansManageViewModel model)
        {
            ViewData["Title"] = "Wyporzyczenia";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Loans][(int)Option.Name];
            model.RemovedSuccessfully = false;
            model.RemoveError = false;

            if (model.LoanId != null)
            {
                if (_category.Remove((int)model.LoanId))
                {
                    model.RemovedSuccessfully = true;
                }
                else model.RemoveError = true;
            }
            model.Take = 1000;
            model.Loans = _loans.GetMany(model.Take);
            model.LoanId = null;
            return View(model);
        }
    }
}