using MySqlConnector;
using System.Data;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IDbConnection, MySqlConnection>(x =>
  new MySqlConnection(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_MyCors", builder =>
    {
        //builder.WithOrigins("http://localhost");
        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
        .AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("_MyCors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
