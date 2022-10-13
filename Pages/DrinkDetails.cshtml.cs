using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using tushar.Models;

namespace tushar.Pages
{
    public class DrinkDetailsModel : PageModel
    {

        public FullDrink drink;
   
        public void OnGet()
        {
            int id = int.Parse(Request.Query["drinkid"][0]);

            URLRequest uRLRequest = new URLRequest();
            APIClient apiclient = new APIClient(uRLRequest);


            drink = apiclient.getDrinkDetails(id);
        }
    }
}
