using Basboosify.ProductsService.BusinessLogicLayer;
using Basboosify.ProductsMicroService.API.Middleware;
using Basboosify.ProductsService.DataAccessLayer;
using FluentValidation.AspNetCore;
using Basboosify.ProductsMicroService.API.APIEndpoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Add DAL and BLL services
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();

builder.Services.AddControllers();

//fluent validations
builder.Services.AddFluentValidationAutoValidation();

//add model binder to read values from json to enum
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

//add swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Cors
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(builder => {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });    
});

var app = builder.Build();

app.UseMiddleware();
app.UseRouting();

//cors
app.UseCors();

//swagger
app.UseSwagger();
app.UseSwaggerUI();

//Auth
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapProductsAPIEndpoints();

app.Run();
