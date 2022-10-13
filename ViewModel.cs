using System;
using tushar.Models;

namespace tushar
{
    public static class ViewModel
    {

        public static Inventory inventory;
        private static APIClient apiclient;
        public static int? openedDrinkId = null;

        static ViewModel()
        {
            URLRequest uRLRequest = new URLRequest();
            apiclient = new APIClient(uRLRequest);

            inventory = LoadNewInventory();
        }

        public static Inventory LoadNewInventory()
        {

            Inventory inv = new Inventory(apiclient.GetDrinks());
            return inv;
        }

        public static void UpdateDrinks() {
            inventory.SortDrinksList();
        }

        internal static void setOpenedDrinkId(int id)
        {

            if (openedDrinkId.HasValue && openedDrinkId == id)
            {
                openedDrinkId = null;
            }
            else
            {
                openedDrinkId = id;
            }
        }

        internal static void UpdateInventory(IFormCollection form)
        {
            foreach(string ingredient in inventory.ingredients.Keys.ToList())
            {
                inventory.ingredients[ingredient] = form["item-" + ingredient].Contains("on");
            }
        }
    }
}

