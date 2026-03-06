using GameNLog.Data;
using GameNLog.Services;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GameNLogContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddHttpClient<IGDBService>(client =>
{
    client.BaseAddress = new Uri("https://api.igdb.com/v4/");
    client.DefaultRequestHeaders.Add("Client-ID", builder.Configuration["IGDB:ClientId"]);
    client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", builder.Configuration["IGDB:AccessToken"]);
});

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();
