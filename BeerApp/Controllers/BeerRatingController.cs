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
      

        [WebApiConfig.AccessActionFilter]
        // GET api/<controller>/5
        public async System.Threading.Tasks.Task<string> GetAsync(int id)
        {
            string path = "https://api.punkapi.com/v2/beers/" + id;
            HttpResponseMessage response = await client.GetAsync(path);
            //   Beerd BeerRes = null;
            if (response.IsSuccessStatusCode)
            {
                var Samochod = response.Content.ReadAsStringAsync();
                List<BeerRatingViewModel> json = JsonConvert.DeserializeObject<List<BeerRatingViewModel>>(Samochod.Result);
                try
                {
                    List<BeerRatingViewModel> punkBeers = new List<BeerRatingViewModel>();

                    if (File.Exists(Models.Db_Json_Path.database_json_Path))
                    {
                        using (FileStream fs = File.OpenRead(Models.Db_Json_Path.database_json_Path))
                        {
                            punkBeers = await System.Text.Json.JsonSerializer.DeserializeAsync<List<BeerRatingViewModel>>(fs);
                        }
                    }
                    punkBeers.Add(json.First());


                    using (FileStream fs = File.OpenWrite(Models.Db_Json_Path.database_json_Path))
                    {
                        await System.Text.Json.JsonSerializer.SerializeAsync(fs, punkBeers);
                    }
                   
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            else
            {
                return response.StatusCode.ToString();
            }

            return "value";
        }

        // POST api/<controller>
        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> PostAsync([FromUri]int id, [FromBody]BeerRatingViewModel rating)
        {
            if (ModelState.IsValidField("Rating"))
            {
                string path = "https://api.punkapi.com/v2/beers/" + id;
                HttpResponseMessage response = await client.GetAsync(path);
                //   Beerd BeerRes = null;
                if (response.IsSuccessStatusCode)
                {
                    var Samochod = response.Content.ReadAsStringAsync();
                    List<BeerRatingViewModel> json = JsonConvert.DeserializeObject<List<BeerRatingViewModel>>(Samochod.Result);

                    var val = json.First();
                    val.BeerId = id;
                    val.Comments = rating.Comments;
                    val.Rating = rating.Rating;
                    val.Username = rating.Username;


                    try
                    {
                        List<BeerRatingViewModel> punkBeers = new List<BeerRatingViewModel>();
                        if (File.Exists(Models.Db_Json_Path.database_json_Path))
                        {
                            using (FileStream fs = File.OpenRead(Models.Db_Json_Path.database_json_Path))
                            {
                                punkBeers = await System.Text.Json.JsonSerializer.DeserializeAsync<List<BeerRatingViewModel>>(fs);
                            }
                        }
                        punkBeers.Add(val);
                        using (FileStream fs = File.OpenWrite(Models.Db_Json_Path.database_json_Path))
                        {
                            await System.Text.Json.JsonSerializer.SerializeAsync(fs, punkBeers);
                        }
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Could not locate a Beer with that Id");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }


    public class BeersController : ApiController
    {
        static HttpClient client = new HttpClient();

        // GET api/<controller>/5
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


    }
}