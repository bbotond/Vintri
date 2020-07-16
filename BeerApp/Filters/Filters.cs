using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BeerApp.Filters
{
    public class VintriFilter
    {



    }


    public class ValidationActionFilter : ActionFilterAttribute
    {
        //public override void OnActionExecuting(HttpActionContext actionContext)
        //{
        //    var modelState = actionContext.ModelState;


        //    var property = filterContext.Controller.GetType().GetProperty("YourProperty");

        //    if (property == null)
        //    {
        //        throw new InvalidOperationException("There is no YourProperty !!!");
        //    }




        //    //            modelState.TryGetValue("Name", )
        //    if (!modelState.IsValid)
        //        actionContext.Response = actionContext.Request
        //             .CreateErrorResponse(HttpStatusCode.BadRequest, modelState);
        //}




        //         return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        //                actionContext.Response = actionContext.Request
        //                     .CreateErrorResponse(HttpStatusCode.BadRequest, modelState);



    }




}