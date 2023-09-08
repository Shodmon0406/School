using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using WebApi.ExtensionMethods.AuthConfiguration;
using WebApi.ExtensionMethods.RegisterService;
using WebApi.ExtensionMethods.SwaggerConfiguration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRegisterService(builder.Configuration);
builder.Services.AddCors();

builder.Services.SwaggerService();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAuthConfigureService(builder.Configuration);


var app = builder.Build();

try
{
    var serviceProvider = app.Services.CreateScope().ServiceProvider;
    var dataContext = serviceProvider.GetRequiredService<DataContext>();
    await dataContext.Database.MigrateAsync();
}
catch (Exception)
{

	throw;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
