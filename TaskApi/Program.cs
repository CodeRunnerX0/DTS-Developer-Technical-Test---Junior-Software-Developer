using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskApi.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Swagger configuration 
builder.Services.AddSwaggerGen(options =>
{

//Include XML Documentation

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Configure DbContext with SQLite
builder.Services.AddDbContext<TaskContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


//Add CORS to allow React to access API 
builder.Services.AddCors(options => {
    options.AddPolicy("AllowReactApp", policy => 
        policy.WithOrigins("http://localhost:3000") // React’s URL
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build(); // Build the application



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}


app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers(); 

app.Run();

