using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Boilerplate.Api.Configurations;

public static class ValidationSetup
{
    public static void AddValidationSetup(this IMvcBuilder builder)
    {
        builder.AddFluentValidation(x =>
        {
            x.AutomaticValidationEnabled = false;
            x.RegisterValidatorsFromAssemblyContaining<Application.IAssemblyMarker>();
        });
    }
}
