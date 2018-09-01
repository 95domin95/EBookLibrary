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
    public enum OperationType : int
    {
        add,
        modify,
        remove,
        select
    }

    [Authorize(Roles = "Admin")]
    public class BooksManageController : Controller
    {
        private readonly IBooksManage _manage;
        public BooksManageController(IBooksManage manage)
        {
            _manage = manage;
        }

        [HttpGet]
        public IActionResult ManagePanel()
        {
            return View(new ManagePanelViewModel {
                Categories = _manage.GetAllCategories(),
                Operations = new List<string>()
                {
                    "Dodaj nową książkę",
                    "Modyfikuj dane książki",
                    "Usuń książkę",
                    "Wyszukaj książki"
                }
            });
        }

        [HttpPost]
        public IActionResult ManagePanel(ManagePanelViewModel model)
        {
            switch(model.OperationType)
            {
                case (int)OperationType.add:
                    _manage.Add(model.Title, model.Path, model.ISBN, model.Pages,
                        model.Author, model.Publisher, model.Category);
                    break;
                case (int)OperationType.modify:
                    _manage.UpdateById((int)model.Id, model.Title, model.ISBN, 
                        model.Author, model.Pages, model.Publisher, model.Category);
                    break;
                case (int)OperationType.remove:
                    _manage.DeleteById((int)model.Id);
                    break;
                case (int)OperationType.select:
                    if (model.Id == null)
                    {
                        _manage.GetBooks(model.Title, model.ISBN, model.Author,
                            model.PagesMin, model.PagesMax, model.Publisher, model.Category);
                    }
                    else _manage.GetById((int)model.Id);
                    break;
                default:
                    break;
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult AddBook(AddBookViewModel model)
        {
            return View(model);
        }
        [HttpGet]
        public IActionResult AddBook()
        {
            return View(new AddBookViewModel { Categories = _manage.GetAllCategories() });
        }
        [HttpPost]
        public IActionResult SelectBook(SelectBookViewModel model)
        {
            if (model.Id == null)
            {
                _manage.GetBooks(model.Title, model.ISBN, model.Author,
                    model.PagesMin, model.PagesMax, model.Publisher, model.Category);
            }
            else _manage.GetById((int)model.Id);
            return View(model);
        }
        [HttpGet]
        public IActionResult SelectBook()
        {
            return View(new SelectBookViewModel { Categories = _manage.GetAllCategories() });
        }
        [HttpPost]
        public IActionResult UpdateBook(UpdateBookViewModelcs model)
        {
            _manage.UpdateById((int)model.Id, model.Title, model.ISBN,
            model.Author, model.Pages, model.Publisher, model.Category);
            return View(model);
        }
        [HttpGet]
        public IActionResult UpdateBook()
        {
            return View(new UpdateBookViewModelcs { Categories = _manage.GetAllCategories() });
        }
        [HttpPost]
        public IActionResult DeleteBook(DeleteBookViewModel model)
        {
            _manage.DeleteById(model.Id);
            return View(model);
        }
        [HttpGet]
        public IActionResult DeleteBook()
        {
            return View();
        }
    }
}