using Serilog;
using Server.AppFilters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());

builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"Domain.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"UseCases.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"ApiContracts.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"Results.xml"));
});
{
    builder.Services.AddUseCases();
    //builder.Services.AddInfrastructureUseSqlServer
    //("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=RefitSample;Integrated Security=True;Multiple Active Result Sets=True");

    builder.Services.AddInfrastructureUseSqlite();

}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
