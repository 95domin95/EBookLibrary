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
        private readonly IUserManage _users;

        public LibraryManageController(IBooksManage manage,
            IQueue queue,
            ILoanHistory loanHistory,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAuthors author,
            IPublishers publisher,
            ICategory category,
            ILoans loans,
            IUserManage users)
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
            _users = users;
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
            LoanHistories
        };

        enum OperationError
        {
            add = 0,
            remove = 1,
            modify = 2
        };

        [HttpGet]
        public IActionResult Books()
        {
            ViewData["Title"] = "Książki";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Books][(int)Option.Name];
            return View(new BooksManageViewModel {
                Categories = _manage.GetAllCategories(),
                Authors = _manage.GetAllAuthors(),
                Publishers = _publisher.GetMany(1000),
                Books = _manage.GetMany(1000),
                
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BooksManageViewModel model)
        {
            ViewData["Title"] = "Autorzy";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Books][(int)Option.Name];
            model.RemovedSuccessfully = false;
            model.AddedSuccessfully = false;
            model.AddError = false;
            model.RemoveError = false;
            model.ModifiedSuccessfully = false;
            model.ModifiedError = false;

            if(await _manage.Add(model.Title, model.ISBN, model.Pages, model.Author, model.Publisher,
                model.Category, model.Book, model.BookCovering, (int)model.CopiesCount))
            {
                model.AddedSuccessfully = true;
            }

            model.Take = 1000;
            model.Authors = _author.GetMany(model.Take);
            model.Categories = _manage.GetAllCategories();
            model.Publishers = _publisher.GetMany(1000);
            model.Books = _manage.GetMany(1000);
            return RedirectToAction("Books", model);
        }

        [HttpPost]
        public IActionResult RemoveBook(BooksManageViewModel model)
        {
            ViewData["Title"] = "Autorzy";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Books][(int)Option.Name];
            model.RemovedSuccessfully = false;
            model.AddedSuccessfully = false;
            model.AddError = false;
            model.RemoveError = false;
            model.ModifiedSuccessfully = false;
            model.ModifiedError = false;

            if (_manage.DeleteById((int)model.Id))
            {
                model.RemovedSuccessfully = true;
            }

            model.Take = 1000;
            model.Authors = _author.GetMany(model.Take);
            model.Categories = _manage.GetAllCategories();
            model.Publishers = _publisher.GetMany(1000);
            model.Books = _manage.GetMany(1000);
            return RedirectToAction("Books", model);
        }

        [HttpPost]
        public IActionResult ModifyBook(BooksManageViewModel model)
        {
            ViewData["Title"] = "Autorzy";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Books][(int)Option.Name];
            model.RemovedSuccessfully = false;
            model.AddedSuccessfully = false;
            model.AddError = false;
            model.RemoveError = false;
            model.ModifiedSuccessfully = false;
            model.ModifiedError = false;

            if(_manage.Modify(model))
            {
                model.ModifiedSuccessfully = true;
                model.Title = null;
                model.CopiesCount = null;
                model.BookCovering = null;
                model.Pages = null;
                model.Author = null;
                model.Publisher = null;
                model.ISBN = null;
            }

            model.Take = 1000;
            model.Authors = _author.GetMany(model.Take);
            model.Categories = _manage.GetAllCategories();
            model.Publishers = _publisher.GetMany(1000);
            model.Books = _manage.GetMany(1000);
            return RedirectToAction("Books", model);
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
                if (_publisher.Add(model.Name))
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
                if (_queue.RemoveById((int)model.QueueId))
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
                if (_loanHistory.Remove((int)model.LoanHistoryId))
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
                if (_loans.Remove((int)model.LoanId))
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

        [HttpGet]
        public IActionResult Users()
        {
            ViewData["Title"] = "Użytkownicy";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Users][(int)Option.Name];
            return View(new UsersManageViewModel
            {
                Roles = _users.GetAllRoles(),
                Users = _users.GetMany(1000),
                Take = 1000
            });
        }

        [HttpPost]
        public IActionResult Users(UsersManageViewModel model)
        {
            ViewData["Title"] = "Użytkownicy";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Users][(int)Option.Name];
            model.RemovedSuccessfully = false;
            model.RemoveError = false;

            if (model.UserId != null)
            {
                if (_users.Remove(model.UserId))
                {
                    model.RemovedSuccessfully = true;
                }
                else model.RemoveError = true;
            }
            model.Take = 1000;
            model.Users = _users.GetMany(model.Take);
            model.UserId = null;
            model.RoleChoosed = null;
            return View(model);
        }

        [HttpPost]
        public IActionResult ChangeLockStatus(UsersManageViewModel model)
        {
            var user = _users.GetById(model.UserId);
            if(user != null)
            {
                if(user.LockoutEnabled)
                {
                    _users.Unlock(model.UserId);
                }
                else _users.Lock(model.UserId);
            }
            return RedirectToAction("Users");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(UsersManageViewModel model)
        {
            var user = _users.GetById(model.UserId);
            if (user != null && model.RoleChoosed != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRoleAsync(user, roles.FirstOrDefault());
                await _userManager.AddToRoleAsync(user, model.RoleChoosed);
            }
            return RedirectToAction("Users");
        }
    }
}