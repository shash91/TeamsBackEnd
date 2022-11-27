using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace TeamsBackEnd.Filters
{
    public class CustomAuthorizeActionFilter: Attribute, IAuthorizationFilter
    {        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Session.TryGetValue("accessToken", out byte[] acc))
            {
                context.Result = new UnauthorizedResult();
            }
                          //Write you code here to authorize or unauthorize the user
        }
    }
}
