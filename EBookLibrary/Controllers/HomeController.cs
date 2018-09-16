using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EBookLibraryData.Models.ViewModels.Home;
using EBookLibraryServices;
using EBookLibraryData.Models;

namespace EBookLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBooksManage _manage;
        public HomeController(IBooksManage manage)
        {
            _manage = manage;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BrowseBooks()
        {
            return View(new BrowseBooksViewModel
            {
                Categories = _manage.GetAllCategories()
            });
        }

        [HttpGet]
        public IActionResult BrowseBooks(BrowseBooksViewModel model)
        {
            model.Books = _manage.GetBooks(model.Title, model.ISBN, model.Author, model.PagesMin,
                model.PagesMax, model.Publisher, model.Category);

            model.Categories = _manage.GetAllCategories();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
