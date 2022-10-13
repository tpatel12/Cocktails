using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tushar.Models;

namespace tushar.Pages
{
    public class CocktailModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public CocktailModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
            
        }

        //public List<FullDrink> drinks;
        //public Dictionary<string, bool> ingredients;
        public Inventory inventory;
        public int? openedDrinkId = null;

        public void OnGet()
        {

            ViewData["output"] = "abc";

            ViewModel.UpdateDrinks();
            inventory = ViewModel.inventory;
            openedDrinkId = ViewModel.openedDrinkId;
        }

        public IActionResult OnPostUpdate()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ViewModel.UpdateInventory(Request.Form);

            return RedirectToAction("Get");
        }

        public IActionResult OnPostSelectAll()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ViewModel.inventory.setAllIngredients(true);

            return RedirectToAction("Get");
        }

        public IActionResult OnPostUnselectAll()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ViewModel.inventory.setAllIngredients(false);

            return RedirectToAction("Get");
        }


        public IActionResult OnPostDetails(int id)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            ViewModel.setOpenedDrinkId(id);

            return RedirectToAction("Get");
        }

    }
}
