using System;
using tushar.Models;

namespace tushar
{
    public class Inventory
    {
        public SortedDictionary<string, bool> ingredients;
        

        public List<FullDrink> drinks { get; set;}

        public Inventory(List<FullDrink> d)
        {
            drinks = d;

            loadIngredientsFromDrinks();
            SortDrinksList();
        }

        //Sets all ingredients' value to "val"
        public void setAllIngredients(bool val)
        {
            foreach(string key in ingredients.Keys.ToList())
            {
                ingredients[key] = val;
            }
        }

        //Pop
        public void loadIngredientsFromDrinks() {

            ingredients = new SortedDictionary<string, bool>();
            foreach(FullDrink drink in drinks)
            {
                foreach(string ingredient in drink.ingredients)
                {
                    this.ingredients.TryAdd(ingredient, false);
                }
            }
        }


        public List<string> getMissingIngredients(FullDrink d)
        {
            List<string> list = new List<string>();

            foreach (string ingredient in d.ingredients)
            {
                if (!this.ingredients.Keys.Contains(ingredient) || !this.ingredients[ingredient])
                {
                    list.Add(ingredient);
                }
            }
            return list;
        }

        public string getMissingIngredientsString(FullDrink d) {
            return string.Join(", ", getMissingIngredients(d));
        }


        public void SortDrinksList() {
            drinks.Sort(drinkComparer);
        }

        //Comparer method for sotring drinks by availability
        private int drinkComparer(FullDrink x, FullDrink y)
        {
            return countMissingIngredients(x) - countMissingIngredients(y);
        }

        //Possible TODO: replace this simple heuristic with a more advanced one,
        // that weights missing ingredients based on importance (list index)
        private int countMissingIngredients(FullDrink d) {
            int count = 0;
            foreach(string ingredient in d.ingredients){
                if (!this.ingredients.Keys.Contains(ingredient) || !this.ingredients[ingredient])
                {
                    count++;
                }
            }
            return count;

        }
       
    }
}

