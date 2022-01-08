using OdeToFood.Data.Models;
using OdeToFood.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Web.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IRestaurantData db;

        public RestaurantsController(IRestaurantData db)
        {
            this.db = db;
        }

        // GET: Restaurants
        [HttpGet]
        public ActionResult Index()
        {
            var model = db.GetAll();
            return View(model);
        }

        // /details
        [HttpGet]
        public ActionResult Details(int id)
        {
            // Get restaurant from DB, pass in ID from url
            var model = db.Get(id);

            // If invalid ID(restaurant doesn't exist), render 404 page
            if(model == null)
            {
                return View("NotFound");
            }

            // Otherwise render restaurant view, pass in data
            return View(model);
        }

        // Create form view - user goes to /restaurants/create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        // Create new restaurant once form submitted - takes in restaurant data from form - validates token first
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Restaurant restaurant)
        {
            
            // If validation specified in Restaurant model passes (name required)...
            if (ModelState.IsValid)
            {
                // Add new restaurant to DB
                db.Add(restaurant);
            
                // redirect to details page for new restaurant - goes to /restaurants/details/id
                return RedirectToAction("Details", new { id = restaurant.Id });
            }
            // Else just return the view (with validation error message)
            return View();
        }

        // Edit form view - user goes to /restaurants/edit/id
        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Get restaurant from DB
            var model = db.Get(id);
            if(model == null)
            {
                return View("NotFound");
            }
            
            // Render view, pass in restaurant details
            return View(model);
        }

        // Update restaurant in DB once form submitted
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Restaurant restaurant)
        {
            // If validation specified in Restaurant model passes (name required)...
            if (ModelState.IsValid)
            {
                // Update restaurant in DB
                db.Update(restaurant);
                TempData["Message"] = "Restaurant updated successfully";
                // redirect to details page for edited restaurant - goes to /restaurants/details/id
                return RedirectToAction("Details", new { id = restaurant.Id });
            }
            // Else just return the view (with validation error message), displaying edited data so far
            return View(restaurant);
        }


        // GET request to show delete confirmation form
        [HttpGet]
        public ActionResult Delete(int id)
        {
            // Get restaurant from DB
            var model = db.Get(id);
            if (model == null)
            {
                return View("NotFound");
            }

            // Render view, pass in restaurant details
            return View(model);
        }

        // Delete restaurant - POST not DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Form collection not used but need to add second parameter to differentiate from GET
        public ActionResult Delete(int id, FormCollection form)
        {
            db.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
