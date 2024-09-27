using Microsoft.AspNetCore.Mvc;
using Services;
using System.Net;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomBaseController : ControllerBase
{
    [NonAction]
    public IActionResult CreateActionResult<T>(ServiceResult<T> result)
    {

        return result.HttpStatusCode switch
        {
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.Created => Created(result.UrlAsCreated, result),
            _ => new ObjectResult(result) { StatusCode = (int)result.HttpStatusCode }
        };

    }

    [NonAction]
    public IActionResult CreateActionResult(ServiceResult result)
    {
        return result.HttpStatusCode switch
        {
            HttpStatusCode.NoContent => new ObjectResult(null) { StatusCode = (int)result.HttpStatusCode },
            _ => new ObjectResult(result) { StatusCode = (int)result.HttpStatusCode }
        };
    }
}
