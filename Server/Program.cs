using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"Domain.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"UseCases.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"ApiContracts.xml"));
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
