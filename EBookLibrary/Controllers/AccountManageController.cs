using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EBookLibraryData.Models;
using EBookLibraryData.Models.ViewModels.AccountManage;
using EBookLibraryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EBookLibrary.Controllers
{
    [Authorize]
    public class AccountManageController : Controller
    {
        private readonly IBooksManage _manage;
        private readonly IQueue _queue;
        private readonly ILoanHistory _loanHistory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserManage _user;
        private readonly List<string[]> menuOptions = new List<string[]>() {
            new string[]{
                "LoanHistory",
                "Historia Wyporzyczeń",
            },
            new string[]{
                "InQueue",
                "Oczekujące"
            },
            new string[]{
                "Loaned",
                "Wyporzyczone"
            },
            new string[]{
                "AccountManage",
                "Zmień dane"
            },
            new string[]{
                "ChangePassword",
                "Zmień hasło"
            }
        };
        enum Option
        {
            Name,
            CommonName,
            LoanHistory = 0,
            InQueue,
            Loaned,
            AccountManage,
            ChangePassword
        };
        public AccountManageController(IBooksManage manage,
            IQueue queue,
            ILoanHistory loanHistory,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserManage user)
        {
            _manage = manage;
            _queue = queue;
            _userManager = userManager;
            _signInManager = signInManager;
            _loanHistory = loanHistory;
            _user = user;
        }
        public async Task<IActionResult> LoanHistory()
        {
            ViewData["Title"] = "Historia wyporzyczeń";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.LoanHistory][(int)Option.Name];
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return View(new LoanHistoryViewModel {
                LoanHistories = _loanHistory.GetAllUserLoanHistories(user)
            });
        }

        public async Task<IActionResult> InQueue()
        {
            ViewData["Title"] = "Oczekujące";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.InQueue][(int)Option.Name];
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return View(new InQueueViewModel {
                Books = _queue.GetAllUserBooksInQueue(user)
            });
        }

        public async Task<IActionResult> Loaned()
        {
            ViewData["Title"] = "Wyporzyczone";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.Loaned][(int)Option.Name];
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return View(new LoanedViewModel {
                Books = _manage.GetAllUserLoanedBooks(user)
            });
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            ViewData["Title"] = "Zmień hasło";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.ChangePassword][(int)Option.Name];
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return View(new ChangePasswordViewModel {
                User = user
            });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            ViewData["Title"] = "Zmień hasło";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.ChangePassword][(int)Option.Name];
            var user = await _userManager.GetUserAsync(HttpContext.User);
            model.User = user;
            model.ChangedSuccessfully = false;
            model.ChangedError = false;
            if (_user.SetPassword(model.User, model.OldPassword, model.NewPassword))
            {
                model.ChangedSuccessfully = true;
            }
            else model.ChangedError = true;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AccountManage()
        {
            ViewData["Title"] = "Zmień dane";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.AccountManage][(int)Option.Name];
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return View(new ManageAccountViewModel {
                User = user
            });
        }
        
        [HttpPost]
        public async Task<IActionResult> AccountManage(ManageAccountViewModel model)
        {
            ViewData["Title"] = "Zmień dane";
            ViewData["Options"] = menuOptions;
            ViewData["Selected"] = menuOptions[(int)Option.AccountManage][(int)Option.Name];
            var user = await _userManager.GetUserAsync(HttpContext.User);
            model.User = user;
            model.ChangedSuccessfully = false;
            model.ChangedError = false;
            if (_user.SetLoginAndEmail(model.User, model.Email, model.Username))
            {
                model.ChangedSuccessfully = true;
            }
            else model.ChangedError = true;
            return View(model);
        }
    }
}