// Program.cs (kiterjesztve a Swagger dokumentációval)
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Vizvezetek.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// DB Context konfiguráció
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = ServerVersion.AutoDetect(connectionString);
builder.Services.AddDbContext<vizvezetekContext>(
    options => options.UseMySql(connectionString, serverVersion));

// Swagger/OpenAPI konfiguráció
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Tiszta Viz Kft. API",
        Version = "v1",
        Description = "Web API a vízvezeték-szerelõ munkalapok kezeléséhez.",
        Contact = new OpenApiContact
        {
            Name = "Tiszta Viz Kft.",
            Email = "info@tisztaviz.hu"
        }
    });

    // XML dokumentáció hozzáadása
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tiszta Viz Kft. API v1");

        // Swagger UI az alkalmazás gyökerén
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();