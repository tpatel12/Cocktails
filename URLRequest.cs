using System;

namespace tushar
{
    public class URLRequest
    {
        public static HttpClient client = new HttpClient();
        const string BASE_ADDRESS = "http://www.thecocktaildb.com/api/json/v1/";
        const string API_KEY = "1";

        public URLRequest()
        {
        }

        //Completes a URL using endpoint string s
        public static string completeString(string s)
        {
            return BASE_ADDRESS + API_KEY + "/" + s;
        }

        //Sends Http request using endpoint and reads JSON into model of type T
        public T getFromEndpoint<T>(string endpoint)
        {
         
            HttpResponseMessage response = client.GetAsync(completeString(endpoint)).Result;

            if (response.IsSuccessStatusCode)
            {
                HttpContent content = response.Content;
                T? item = content.ReadFromJsonAsync<T>().Result;

                return item;
            }
            else
            {
                throw new Exception("Unsuccessful API execution");
            }

        }
    }
}

