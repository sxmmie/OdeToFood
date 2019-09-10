using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Models;
using OdeToFood.Services;

namespace OdeToFood.Pages.Restuarants
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IRestaurantData _restaurantData;

        [BindProperty]
        public Restaurant Restuarant { get; set; }

        public EditModel(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }
        // A Get request to display form where we present info about restaurant
        // and allow user to edit the restaurant
        public IActionResult OnGet(int id)
        {
            Restuarant = _restaurantData.Get(id);
            if (Restuarant == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _restaurantData.Update(Restuarant);
                return RedirectToAction("Details", "Home", new { id = Restuarant.Id });
            }

            return Page();
        }
    }
}