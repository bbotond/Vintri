using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BeerApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }




        //public class ValidateModelAttribute : System.Web.Mvc.ActionFilterAttribute
        //{
        //    public override void OnActionExecuting(HttpActionContext actionContext)
        //    {
        //        if (!actionContext.ModelState.IsValid)
        //        {
        //            List<Errors> errors = new List<Errors>();

        //            // Set error message and errorCode
        //            foreach (var key in keys)
        //            {
        //                if (!actionContext.ModelState.IsValidField(key))
        //                {
        //                    error.Add(new HttpResponseError
        //                    {
        //                        Code = ???????????,
        //                        Message = actionContext.ModelState[key].Errors.FirstOrDefault().ErrorMessage
        //                    });
        //                }
        //            }

        //            // Return to client
        //            actionContext.Response = actionContext.Request.CreateResponse(
        //                System.Net.HttpStatusCode.BadRequest, errors);
        //        }
        //    }
        //}




        public class AccessActionFilter : System.Web.Http.Filters.ActionFilterAttribute, System.Web.Http.Filters.IActionFilter
        {
//            private readonly string[] _permissions;
            public AccessActionFilter()
            {
        //        _permissions = permissions;
            }



//            public override void OnActionExecuting(HttpActionContext actionContext)
//            {
//                base.OnActionExecuting(actionContext);
//                if (!actionContext.ModelState.IsValid)
//                    throw new ModelStateException(actionContext.ModelState);
//            }

            public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
            {
                //... custom logic here
                //                var hasPermissons = UserService.HasPermission(string[] permissions);

                if (!actionContext.ModelState.IsValid)
                {

                }

                base.OnActionExecuting(actionContext);
            }

            public override bool AllowMultiple
            {
                get { return false; }
            }
        }


    }
}
