using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskApi.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Registers controllers for API end points
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// Configure Swagger for API documentation 
builder.Services.AddSwaggerGen(options =>
{
    // Include XML comments generated from code comments

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Registers db context to use SQL Lite for EF to be able to interact with the database
// Uses the connection string to connect to sqlite database
builder.Services.AddDbContext<TaskContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))); 
    


//Add CORS to allow React to communicate to the API 
builder.Services.AddCors(options => {
    options.AddPolicy("AllowReactApp", policy => 
        policy.WithOrigins("http://localhost:3000") // React’s URL
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build(); // Finalizes the services and builds the application



// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}


app.UseHttpsRedirection(); // Enforces the use of HTTPS (secure connection)
app.UseCors("AllowReactApp"); // Applies the CORS policy defined earlier (allows React access)
app.UseAuthorization(); 
app.MapControllers(); // Maps the incoming HTTP requests to the correct Controller methods

app.Run(); // Starts the web host making the API available to receive requests

