using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UseCases.Articles.Service;

public static class ADependencyInjection
{
    public static void AddUseCases(this IServiceCollection Services)
    {
        Services.AddLogging();

        //builder.Services.AddFluentValidation();
        Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        Services.AddFluentValidationAutoValidation();
        Services.AddFluentValidationClientsideAdapters();

        Services.AddScoped<IArticleService, ArticleService>();





    }

}

