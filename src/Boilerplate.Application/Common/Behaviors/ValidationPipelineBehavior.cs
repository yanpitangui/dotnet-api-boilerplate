using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Common.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMediator _mediator;

    public ValidationPipelineBehavior(IServiceProvider serviceProvider, IMediator eventBus)
    {
        _serviceProvider = serviceProvider;
        _mediator = eventBus;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var errors = new List<ValidationError>();


        var validator = _serviceProvider.GetService<IValidator<TRequest>>();

        if (validator != null)
        {
        
            var result = await validator.ValidateAsync(request, cancellationToken);
            errors.AddRange(result.Errors.Select(e => new ValidationError(e.ErrorCode, e.ErrorMessage, e.PropertyName)));

            foreach (var failure in errors)
            {
                await _mediator.Publish(failure, cancellationToken);
            }

            if (errors.Count > 0)
                return default!;
        }
        
        return await next();
    }
}