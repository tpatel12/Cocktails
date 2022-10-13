using System;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Timers;

namespace tushar.Models
{
    //Simple drink
    public class Drink
    {
        [JsonPropertyName("strDrink")]
        public string name { get; set; }
        public string strDrinkThumb { get; set; }
        public int idDrink { get; set; }

        public override string ToString()
        {
            return name;
        }
    }

    //Detailed drink object, with ingredients, measures, instructions, image, etc.
    public class FullDrink {
        [JsonPropertyName("idDrink")]
        public int id { get; set; }

        [JsonPropertyName("strDrink")]
        public string name { get; set; }

        [JsonPropertyName("strDrinkAlternate")]
        public string nameAlternate { get; set; }

        [JsonPropertyName("strDrinkThumb")]
        public string imageSource { get; set; }

        [JsonIgnore]
        public string imageSourcePreview { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement> extensionData { get; set; }

        [JsonIgnore]
        public string instructions { get; set; }

        [JsonIgnore]
        public List<string> ingredients = new List<string>();

        [JsonIgnore]
        public Dictionary<string, string> ingredientMeasures = new Dictionary<string, string>();


        public void run()
        {
            imageSourcePreview = imageSource + "/preview";

            //CultureInfo.CurrentCulture.TwoLetterISOLanguageName
            //TODO Make the instruction language based on culture info
            //Or buttons to change langauge

            for (int i = 1; i <= 15; i++)
            {

                JsonElement ingredient = extensionData["strIngredient" + i];

                if (ingredient.ValueKind == JsonValueKind.Null)
                {
                    break;
                }

                string capitalIngredient = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ingredient.GetString().ToLower());

                if (!ingredients.Contains(capitalIngredient))
                {
                    ingredients.Add(capitalIngredient);
                    ingredientMeasures.Add(capitalIngredient, extensionData["strMeasure" + i].GetString());
                }

            }

        }





    }

    //Raw Model
    public class FullDrinkList
    {
        [JsonPropertyName("drinks")]
        public List<FullDrink> drinks { get; set; }

    }

    //Raw Model
    public class DrinkList {
        public List<Drink> drinks { get; set; }
        public override string ToString()
        {
            return string.Join(",", drinks);
        }
    }

    //Raw Model
    public class IngredientsList{
        [JsonPropertyName("drinks")]
        public List<Dictionary<string, string>> drinks { get; set; }
    }


    public class APIClient
    {
        
        public URLRequest urlrequest;

        public APIClient(URLRequest url)
        {
            this.urlrequest = url;
        }

        
        //Gets the details of a specific drink by ID
        public FullDrink getDrinkDetails(int id) {

            FullDrink d = urlrequest.getFromEndpoint<FullDrinkList>("lookup.php?i=" + id).drinks[0];

            d.run();

            return d;
        }

        //Gets a list of all ingredients
        //sorted dictionary mapping names to booleans -> default value false
        public SortedDictionary<string,bool> getAllIngredients()
        {
            List<Dictionary<string, string>> ingredients = urlrequest.getFromEndpoint<IngredientsList>("list.php?i=list").drinks;
            SortedDictionary<string, bool> output = new SortedDictionary<string, bool>();

            foreach(Dictionary<string, string> dict in ingredients) {

                output.Add(dict["strIngredient1"], false);
            }



            return output;
        }

        //Returns a list of all drinks, only wtih names, ID's, and thumbnails.
        public DrinkList getDrinksSimple()
        {
            return urlrequest.getFromEndpoint<DrinkList>("filter.php?c=Cocktail");

        }


        public const string ALL_STARTING_CHARS = "abcdefghijklmnopqrstuvwxyz1234567890";


        //Produces a full list of all drinks
        public List<FullDrink> GetDrinks()
        {
            //List<Drink> list = getDrinksSimple().drinks;
            //List<FullDrink> output = new List<FullDrink>();

            //foreach(Drink drink in list) {

            //    int id = drink.idDrink;
            //    output.Add(getDrinkDetails(id));


            //}
            //return output;


            List<FullDrink> output = new List<FullDrink>();

            foreach(char c in ALL_STARTING_CHARS)
            {
                output.AddRange(getDrinksFirstLetter(c));
            }
            return output;
        }


        //Gets a list of all drinks that begin with given character (letter or number)
        private List<FullDrink> getDrinksFirstLetter(char letter)
        {

            List<FullDrink> list = urlrequest.getFromEndpoint<FullDrinkList>("search.php?f=" + letter).drinks;

            if(list == null) {
                 return new List<FullDrink>();
            }

            foreach(FullDrink d in list) {
                d.run();
            }

            return list;
        }

    }
}

