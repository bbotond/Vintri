using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeerApp.Models
{

    public static class Db_Json_Path
    {
        public static string database_json_Path = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Database.json");
    }
    public class BeerRatingViewModel
    {
        public int BeerId { get; set; }

        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Please provide a valid email address")]
        public string Username { get; set; }

        [Range(1,5, ErrorMessage ="Please provide a value between 1 and 5")]
        public int Rating { get; set; }

        public string Comments { get; set; }


    }
       


    public class Task2
    {
        public string Name { get; set; }

        public string Description { get; set; }


        public List<BeerRatingViewModel> Ratings { get; set; }
             

        public Task2()
        {
            Ratings = new List<BeerRatingViewModel>();
        }
    }

}