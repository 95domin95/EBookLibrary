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
            return View();
        }

        [HttpPost]
        public IActionResult ManagePanel(ManagePanelViewModel model)
        {
            switch(model.OperationType)
            {
                case (int)OperationType.add:
                    _manage.Add(new Book {
                        ISBN = model.ISBN,
                        Pages = model.Pages,
                        Title = model.Title,
                        Path = model.Path,
                        Publisher = _manage.GetPublisher(model.Publisher)
                    });
                    break;
                case (int)OperationType.modify:
                    _manage.UpdateById((int)model.Id, model.Title, model.ISBN, 
                        model.Author, model.Pages, model.Publisher, model.Category);
                    break;
                case (int)OperationType.remove:
                    _manage.DeleteById((int)model.Id);
                    break;
                case (int)OperationType.select:
                    if (model.Id != null)
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
    }
}