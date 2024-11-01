using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace CasusExotischNederland.Model
{
    public class LocationService
    {
        private readonly string _apiKey = "7788b50fbe931c";

        public async Task<LocationInfo> GetLocationInfoAsync()
        {
            string url = $"https://ipinfo.io/json?token={_apiKey}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(responseBody);


                return new LocationInfo
                {

                    City = json["city"].ToString(),
                    Region = json["region"].ToString(),
                    Country = json["country"].ToString(),
                    CoordinateY = float.Parse(json["loc"].ToString().Split(',')[0]),
                    CoordinateX = float.Parse(json["loc"].ToString().Split(',')[1])
                };
            }
        }
    }
}
