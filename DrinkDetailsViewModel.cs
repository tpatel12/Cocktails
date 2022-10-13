using System;
using tushar.Models;

namespace tushar
{
    public static class DrinkDetailsViewModel
    {

        //public static DrinkList drinksListSimple;
        //public static Dictionary<string, bool> ingredients;
        public static Inventory inventory;
        private static APIClient apiclient;
        public static int? openedDrinkId = null;



        static DrinkDetailsViewModel()
        {
            URLRequest uRLRequest = new URLRequest();
            apiclient = new APIClient(uRLRequest);

            
        }

        public static Inventory LoadNewInventory()
        {
            
            Inventory inv = new Inventory(apiclient.GetDrinks());
            return inv;
        }

        public static void ReloadDrinks() {
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
    }
}

