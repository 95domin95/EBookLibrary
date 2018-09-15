using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EBookLibraryData;
using EBookLibraryData.Models;
using EBookLibraryData.Models.ViewModels.BooksManage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBookLibrary.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BooksManageController : Controller
    {
        private readonly IBooksManage _manage;
        public BooksManageController(IBooksManage manage)
        {
            _manage = manage;
        }

        private readonly List<string[]> operations = new List<string[]>()
        {
            new string[]{ "Dodaj nową książkę", "add" },
            new string[]{ "Modyfikuj dane książki", "update" },
            new string[]{ "Usuń książkę", "delete" },
            new string[]{ "Wyszukaj książki", "select" }
        };

        [HttpGet]
        public IActionResult ManagePanel()
        {
            return View(new ManagePanelViewModel {
                Categories = _manage.GetAllCategories(),
                Operations = operations
            });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ManagePanel(ManagePanelViewModel model)
        {
            ViewData["message"] = String.Empty;

            switch(model.OperationType)
            {
                case "add":
                    if (!await _manage.Add(model.Title, model.ISBN, model.Pages,
                        model.Author, model.Publisher, model.Category,
                        model.Book, model.BookCovering))
                    {
                        ViewData["message"] = "Nie udało się dodać książki.";
                    }
                    break;
                case "update":
                    _manage.UpdateById(model.Id, model.Title, model.ISBN, 
                        model.Author, model.Pages, model.Publisher, model.Category);
                    break;
                case "delete":
                    if(!(model.Id == null||model.Id.Equals(String.Empty)))
                    {
                        _manage.DeleteById((int)model.Id);
                    }
                    break;
                case "select":
                    if (model.Id == null||model.Id.Equals(String.Empty))
                    {
                        model.Books = _manage.GetBooks(model.Title, model.ISBN, model.Author,
                            model.PagesMin, model.PagesMax, model.Publisher, model.Category);
                    }
                    else
                    {
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
    }
}