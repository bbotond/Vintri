using BeerApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text.Json;


namespace BeerApp.Controllers
{
    public class BeerRatingController : ApiController
    {
        static HttpClient client = new HttpClient();
      
        // POST api/<controller>
        /// <summary>
        /// action is using filter to provide validation
        /// </summary>
        /// <param name="id">The beer Id in Punk to search for</param>
        /// <param name="rating">object representing user rating, email and comment about the beer</param>
        /// <returns>status message showing if the request was successful or failed</returns>
        /// 
        [Filters.VintriFilter]
        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> PostAsync([FromUri]int id, [FromBody]BeerRatingViewModel rating)
        {
            string path = "https://api.punkapi.com/v2/beers/" + id;
            HttpResponseMessage response = await client.GetAsync(path);
            //   Beerd BeerRes = null;
            if (response.IsSuccessStatusCode)
            {
                //read the result and convert it to a .net object
                var jsonResult = response.Content.ReadAsStringAsync();
                List<BeerRatingViewModel> json = JsonConvert.DeserializeObject<List<BeerRatingViewModel>>(jsonResult.Result);

                //make sure their is a result in our collection
                if (json.First() != null)
                {
                    var val = json.First();
                    //pass values frombody into values collected from punk api 
                    val.BeerId = id; val.Comments = rating.Comments; val.Rating = rating.Rating; val.Username = rating.Username;
                    try
                    {
                        List<BeerRatingViewModel> punkBeers = new List<BeerRatingViewModel>();
                        //check and load any entrys in database.json
                        if (File.Exists(Models.Db_Json_Path.database_json_Path))
                        {
                            using (FileStream fs = File.OpenRead(Models.Db_Json_Path.database_json_Path))
                            {
                                punkBeers = await System.Text.Json.JsonSerializer.DeserializeAsync<List<BeerRatingViewModel>>(fs);
                            }
                        }
                        punkBeers.Add(val);
                        //write out the new database.json file
                        using (FileStream fs = File.OpenWrite(Models.Db_Json_Path.database_json_Path))
                        {
                            await System.Text.Json.JsonSerializer.SerializeAsync(fs, punkBeers);
                        }
                    }
                    catch (Exception ex)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Could not locate a Beer with that Id");
                    }
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not locate a Beer with that Id");
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }


    public class BeersController : ApiController
    {
        static HttpClient client = new HttpClient();

        // GET api/<controller>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<IEnumerable<dynamic>> GetAsync(string id)
        {
            string path = "https://api.punkapi.com/v2/beers/?beer_name=" + id;
            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                var Samochod = response.Content.ReadAsStringAsync();
                List<PunkBeerViewModel> json = JsonConvert.DeserializeObject<List<PunkBeerViewModel>>(Samochod.Result);

                //load the data from database.json
                List<BeerRatingViewModel> punkBeers = new List<BeerRatingViewModel>();
                if (File.Exists(Models.Db_Json_Path.database_json_Path))
                {
                    using (FileStream fs = File.OpenRead(Models.Db_Json_Path.database_json_Path))
                    {
                        punkBeers = await System.Text.Json.JsonSerializer.DeserializeAsync<List<BeerRatingViewModel>>(fs);
                    }
                }

                return from c in json
                           join p in punkBeers on c.id equals p.BeerId into ps
                           select new {c.id, c.name, c.description, userRatings = ps.Select(ds => new { ds.Rating, ds.Username, ds.Comments }) };
            }
            Request.CreateErrorResponse(HttpStatusCode.BadRequest, response.RequestMessage.ToString());
            return null;
        }



        // GET api/<controller>/5
        /// <summary>
        /// REST API endpoint to retrieve a list of beers.
        /// </summary>
        /// <param name="id">the name of the beer to collect from the punk api</param>
        /// <returns></returns>
        public IEnumerable<dynamic> Get(string id)
        {
            string path = "https://api.punkapi.com/v2/beers/?beer_name=" + id;
            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                var Samochod = response.Content.ReadAsStringAsync();
                List<PunkBeerViewModel> json = JsonConvert.DeserializeObject<List<PunkBeerViewModel>>(Samochod.Result);

                //check that database.json exists and load the data from database.json
                List<BeerRatingViewModel> punkBeers = new List<BeerRatingViewModel>();
                if (File.Exists(Models.Db_Json_Path.database_json_Path))
                {
                    using (FileStream fs = File.OpenRead(Models.Db_Json_Path.database_json_Path))
                    {
                        punkBeers = System.Text.Json.JsonSerializer.Deserialize<List<BeerRatingViewModel>>(fs.ToString());
                    }
                }
                //call to service failed return no data
                return from c in json
                       join p in punkBeers on c.id equals p.BeerId into ps
                       select new { c.id, c.name, c.description, userRatings = ps.Select(ds => new { ds.Rating, ds.Username, ds.Comments }) };
            }
            Request.CreateErrorResponse(HttpStatusCode.BadRequest, response.RequestMessage.ToString());
            return null;
        }





    }
}