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
    /// <summary>
    /// action filter to validate model attributes without having to perform the validation at the controller level
    /// </summary>
    public class VintriFilter : ActionFilterAttribute
    {
        /// <summary>
        /// executes before controller action to ensure that model state is valid
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }

}