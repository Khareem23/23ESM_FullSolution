using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaylaaShop.Helpers
{
    public class KaylaCustomAuth : AuthorizeAttribute, IAuthorizationFilter
    {
        readonly string role;

        public KaylaCustomAuth(string _role)
        {
            role = _role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            if (!isAuthenticated)
            {
                context.Result = new RedirectResult("/index");
            }
            else
            {
                if (!string.IsNullOrEmpty(role))
                {
                    if (!context.HttpContext.User.IsInRole(role))
                    {
                        context.Result = new RedirectResult("/AccessDenied");

                    }
                }
            }
        }
    }
}
