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
        public AccountManageController(IBooksManage manage,
            IQueue queue,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _manage = manage;
            _queue = queue;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> LoanHistory()
        {
            ViewData["Title"] = "LoanHistory";
            ViewData["Selected"] = AccountMenuOptions.Options;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return View(new LoanHistoryViewModel {
                LoanHistories = _loanHistory.GetAllUserLoanHistories(user)
            });
        }

        public async Task<IActionResult> InQueue()
        {
            ViewData["Title"] = "InQueue";
            ViewData["Selected"] = AccountMenuOptions.Options;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return View(new InQueueViewModel {
                Books = _queue.GetAllUserBooksInQueue(user)
            });
        }

        public async Task<IActionResult> Loaned()
        {
            ViewData["Title"] = "Loaned";
            ViewData["Selected"] = AccountMenuOptions.Options;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return View(new LoanHistoryViewModel {
                LoanHistories = _loanHistory.GetAllUserLoanHistories(user)
            });
        }
    }
}