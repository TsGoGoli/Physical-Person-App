using BAL.Services.Person;
using Microsoft.Data.SqlClient;
using System.Globalization;
using PhyisicalPersonsApp.Filters;
using PhyisicalPersonsApp.Middlewares;
using PhyisicalPersonsApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Use the extension method to add custom services
builder.Services.AddCustomServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<LoggerMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
