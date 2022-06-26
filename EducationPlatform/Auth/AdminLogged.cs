using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationPlatform.Auth
{
    public class AdminLogged: AuthorizeAttribute, IAuthorizationFilter
    {
        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["Id"] != null)
            {
                return true;
            }
             return false;
           // filterContext.Result = new RedirectResult("~/Default/Index");

        }
    }
}