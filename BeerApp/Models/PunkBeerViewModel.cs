using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeerApp.Models
{
    /// <summary>
    /// viewmodel representing the data to capture from punk api response
    /// </summary>
    public class PunkBeerViewModel
    {
        /// <summary>
        /// id returned from the punk deer json result
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// the name of the beer
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// a description about the beer
        /// </summary>
        public string description { get; set; }


    }
}