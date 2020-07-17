using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeerApp.Models
{

    /// <summary>
    /// path for where the Database.json file is located
    /// </summary>
    public static class Db_Json_Path
    {
        public static string database_json_Path = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Database.json");
    }


    public class BeerRatingViewModel
    {
        /// <summary>
        /// id of beer from punk api used later to match comments to the beer
        /// </summary>
        public int BeerId { get; set; }

        /// <summary>
        /// username needs to be an email address
        /// </summary>
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessage = "Please provide a valid email address")]
        public string Username { get; set; }

        /// <summary>
        /// the rating of the beer value must be between 1 and 5
        /// </summary>
        [Range(1,5, ErrorMessage ="Please provide a value between 1 and 5")]
        public int Rating { get; set; }

        /// <summary>
        /// comment left by users about the beer
        /// </summary>
        public string Comments { get; set; }


    }      

}