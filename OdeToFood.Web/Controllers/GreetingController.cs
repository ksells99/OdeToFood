using OdeToFood.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Web.Controllers
{
    public class GreetingController : Controller
    {
        // GET: Greeting - takes in name from query string e.g. /greeting?name=John
        public ActionResult Index(string name)
        {
            var model = new GreetingViewModel();

            // Sets model name to query string from HTTP request, or 'no name' if null
            model.Name = name ?? "no name";
            
            // Get message key from web.config
            model.Message = ConfigurationManager.AppSettings["message"];

            // Render greeting view and pass in model 
            return View(model);
        }
    }
}