using Microsoft.AspNetCore.Mvc.Filters;

namespace WebLibrary.Data;

public class WarehouseAuthorization : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userType = context.HttpContext.Session.GetString("Type");
        if (string.Equals(userType, "Warehouse") || string.Equals(userType, "Admin"))
        {
            return;
        }
        context.Result = new BadRequestResult();
    }
}