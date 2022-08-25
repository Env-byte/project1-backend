using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using riot_backend.Api;

namespace riot_backend_tests.Helper;

public static class TestHelper
{
    public static T ToSuccess<T>(this IActionResult response) where T : BaseSuccessResponse

    {
        Assert.That(response, Is.TypeOf<OkObjectResult>(), "Response was not OK or contained no object.");
        var castObj = (ObjectResult) response;
        var result = (T)castObj.Value;
        Assert.That(result, Is.Not.Null, $"Response content is not of type {typeof(T)}");
        return result;
    }
}