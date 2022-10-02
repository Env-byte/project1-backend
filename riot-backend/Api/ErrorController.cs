using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using riot_backend.Api.Responses;

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
        var code = exception switch
        {
            NotFoundException => 404,
            AccessDeniedException => 401,
            BadRequestException => 400,
            _ => 500
        };
        Response.StatusCode = code;
        return new ErrorResponse(exception); // Your error model
    }
}