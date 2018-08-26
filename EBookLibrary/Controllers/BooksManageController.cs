using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EBookLibrary.Controllers
{
    public class BooksManageController : Controller
    {
        public IActionResult ManagePanel()
        {
            return View();
        }
    }
}