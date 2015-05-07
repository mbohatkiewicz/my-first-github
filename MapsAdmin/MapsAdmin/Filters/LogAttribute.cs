using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapsAdmin.Filters
{
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //TODO implement logging
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //TODO implement logging
        }
    }
}