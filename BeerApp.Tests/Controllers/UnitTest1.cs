using System;
using BeerApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeerApp.Tests.Controllers
{
    [TestClass]
    public class BeerReviewTest
    {


        [TestMethod]
        public async System.Threading.Tasks.Task PostAsync()
        {
            // Arrange
            BeerRatingController controller = new BeerRatingController();
//            await controller.PostAsync(1, new Models.BeerRatingViewModel() { Comments = "123", Rating = 4, Username = "bren@hotmail.com" });

            //            BeerRatingController controller = new BeerRatingController(new Models.HttpClientHandler());

            // Act
            //          controller.PostAsync(1, new Models.BeerRatingViewModel() { Comments = "example comment", Rating = 6, Username = "brendan"} );

            // Assert



        }
    }
}
