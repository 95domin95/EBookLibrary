using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EBookLibraryData.Models;
using EBookLibraryData.Models.ViewModels.AccountManage;
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
            }
        };
        enum Option
        {
            Name,
            CommonName,
            LoanHistory = 0,
            InQueue,
            Loaned
        };
        public AccountManageController(IBooksManage manage,
            IQueue queue,
            ILoanHistory loanHistory,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _manage = manage;
            _queue = queue;
            _userManager = userManager;
            _signInManager = signInManager;
            _loanHistory = loanHistory;
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
    }
}