using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Swedating.Controllers
{
    //Handler errors and display the error message on the screen.
    public class ErrorHandlerController : Controller
    {
        // GET: ErrorHandler
        public ActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}