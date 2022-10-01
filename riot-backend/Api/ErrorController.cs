using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace riot_backend.Api;

[AllowAnonymous]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    [Route("error")]
    public ErrorResponse Error()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context.Error; // exception
        var code = 500; // Internal Server Error by default
        if (exception is NotFoundException) code = 404; // Not Found
        else if (exception is AccessDeniedException) code = 401; // Unauthorized
        else if (exception is BadRequestException) code = 400; // Bad Request
        Response.StatusCode = code;
        return new ErrorResponse(exception); // Your error model
    }
}
