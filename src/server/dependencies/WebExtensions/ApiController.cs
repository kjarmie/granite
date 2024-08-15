using System.Net;
using System.Runtime.InteropServices.JavaScript;
using ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Mvc;

public class ApiController : ControllerBase
{
    protected readonly ILogger<ApiController> _logger;

    public ApiController(ILogger<ApiController> logger)
    {
        _logger = logger;
    }

    protected ActionResult<ApiResponse<T>> API<T>(Result<T> result)
    {
        var res = ApiResponse.FromResult(result);

        var actionResult = result.Match<ActionResult>(
            ok => Ok(res),
            err => err.Code switch
            {
                HttpStatusCode.OK => Ok(res), // Should not be 200, but just in case
                HttpStatusCode.BadRequest => BadRequest(res),
                HttpStatusCode.NotFound => NotFound(res),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.Unauthorized => Unauthorized(res),
                _ => throw new NotImplementedException($"The status code '{err.Code}' has not been handled in the 'API' controller method.")
            }
        );

        return actionResult;
    }

    protected ActionResult<ApiResponse<T>> API<T>(T data)
    {
        return Ok(ApiResponse.FromData(data));
    }

    protected ActionResult<ApiResponse<object>> API()
    {
        return API((object)new {});
    }

    protected ContentResult Json(object data)
    {
        return Content(JsonConvert.SerializeObject(data), "text/json");
    }
}