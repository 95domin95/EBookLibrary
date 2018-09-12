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

        [HttpGet]
        public IActionResult ManagePanel()
        {
            return View(new ManagePanelViewModel {
                Categories = _manage.GetAllCategories(),
                Operations = new List<string[]>()
                {
                    new string[]{ "Dodaj nową książkę", "add" },
                    new string[]{ "Modyfikuj dane książki", "update" },
                    new string[]{ "Usuń książkę", "delete" },
                    new string[]{ "Wyszukaj książki", "select" }
                }
            });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult ManagePanel(ManagePanelViewModel model)
        {
            switch(model.OperationType)
            {
                case "add":
                    _manage.Add(model.Title, model.ISBN, model.Pages,
                        model.Author, model.Publisher, model.Category);
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
                    if (model.Id == null|| model.Id.Equals(String.Empty))
                    {
                        _manage.GetBooks(model.Title, model.ISBN, model.Author,
                            model.PagesMin, model.PagesMax, model.Publisher, model.Category);
                    }
                    else _manage.GetById((int)model.Id);
                    break;
                default:
                    break;
            }
            return RedirectToAction("ManagePanel");
        }
    }
}