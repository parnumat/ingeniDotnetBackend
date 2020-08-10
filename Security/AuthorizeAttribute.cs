using System;
using ingeniProjectFDotnetBackend.Models.Profiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage (AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter {
    public void OnAuthorization (AuthorizationFilterContext context) {
        var user = (UserProfile) context.HttpContext.Items["UserProfile"];
        if (user == null) {
            Console.WriteLine(user);
            // not logged in
            context.Result = new JsonResult (new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}