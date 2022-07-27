using Boilerplate.Application.Common;
using Boilerplate.Application.Common.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Threading.Tasks;

namespace Boilerplate.Api.Common;

public class ValidationErrorResultFilter : IAsyncResultFilter
{
    private readonly ValidationErrorHandler _errorHandler;

    public ValidationErrorResultFilter(INotificationHandler<ValidationError> errorHandler)
    {
        _errorHandler = (ValidationErrorHandler)errorHandler;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (_errorHandler.HasErrors)
        {
            var errors = _errorHandler.GetErrors();

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            
            await context.HttpContext.Response.WriteAsJsonAsync(errors)
                .ConfigureAwait(false);

            // short circuit .NET request/response pipeline
            return;
        }

        await next().ConfigureAwait(false);
    }
}