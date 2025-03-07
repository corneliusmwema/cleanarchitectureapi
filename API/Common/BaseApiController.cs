using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Common;

[ApiController]
public abstract class BaseApiController : ControllerBase
{
    //property to access the currentuser from httpContext.Items
    protected CurrentUser CurrentUser
    {
        get
        {
            if (HttpContext.Items["CurrentUser"] is CurrentUser currentUser) return currentUser;

            // Return null if the user is not authenticated
            return null;
        }
    }
}