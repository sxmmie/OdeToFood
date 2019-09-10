using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IRestaurantData _restaurant;
        private readonly IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        {
            _restaurant = restaurantData;
            _greeter = greeter;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = new HomeIndexViewModel();
            model.Restaurants = _restaurant.GetAll();
            model.CurrenetMessage = _greeter.GetMessageOfDay();

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _restaurant.Get(id);
            if (model == null)
                return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newRestuarant = new Restaurant();
                /// map the Restaurant model properties to the ViewModel properties 
                newRestuarant.Name = model.Name;
                newRestuarant.Cuisine = model.Cuisine;

                _restaurant.Add(newRestuarant);

                return RedirectToAction(nameof(Details), new { id = newRestuarant.Id });
            }
            else
            {
                return View();
            }                        
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}
