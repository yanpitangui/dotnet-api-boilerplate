using Ardalis.Result;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Api.Common;

public sealed class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        Exception ex = exception.Demystify();
        logger.LogError(ex, "An error ocurred: {Message}", ex.Message);
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        Result? result = Result.Error(exception.ToStringDemystified());
        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);
        return true;
    }
}