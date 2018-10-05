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

        public enum OperationError
        {
            add = 0,
            remove = 1,
            modify = 2
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
            //int convertedId;
            //if(Int32.TryParse(id, out convertedId))
            //_manage.DeleteById(convertedId);

            return RedirectToAction("ManagePanel", model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ManagePanel(ManagePanelViewModel model)
        {
            model.BookAdded = false;
            model.BookModified = false;
            model.BookRemoved = false;
            model.BookSearched = false;
            model.OperationErrorName = string.Empty;
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
                        model.Book, model.BookCovering))
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
                            model.PagesMin, model.PagesMax, model.Publisher, model.Category);

                        var elementsCount = 0;

                        if (!(model.Books == null))
                        {
                            elementsCount = model.Books.Count();
                        }

                        var allPagesCount = elementsCount / model.ElementsOnPage;

                        var elementsToTake = model.ElementsOnPage;

                        if (elementsCount % model.ElementsOnPage != 0) allPagesCount++;
                        if (model.Page < 1) model.Page = 1;
                        if (model.Page > allPagesCount) model.Page = model.AllPagesCount;
                        model.AllPagesCount = allPagesCount;
                        if (model.Page.Equals(model.AllPagesCount))
                        {
                            elementsToTake = elementsCount - ((model.AllPagesCount - 1) * model.ElementsOnPage);
                        }
                        if (elementsCount > 0)
                        {
                            model.Books = model.Books.Skip((model.Page - 1) * model.ElementsOnPage).Take(elementsToTake);
                            model.AnyElements = true;
                        }
                        else model.AnyElements = false;
                        if (elementsCount <= model.ElementsOnPage)
                        {
                            model.MoreThanOnePage = false;
                        }
                    }
                    else
                    {
                        model.BookSearched = true;
                        var book = _manage.GetById((int)model.Id);
                        if(book != null)
                        {
                            model.Books = new List<Book>
                            {
                                book
                            };
                            model.AnyElements = true;
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