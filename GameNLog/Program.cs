using GameNLog.Data;
using GameNLog.Services;
using Microsoft.EntityFrameworkCore;
using PhenX.EntityFrameworkCore.BulkInsert.PostgreSql;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GameNLogContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseBulkInsertPostgreSql()
);
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<SyncService>();
builder.Services.AddHttpClient<IgdbService>(client =>
{
    var clientId = builder.Configuration["IGDB:ClientId"];
    var accessToken = builder.Configuration["IGDB:AccessToken"];
    client.BaseAddress = new Uri("https://api.igdb.com/v4/");
    client.DefaultRequestHeaders.Add("Client-ID", clientId);
    client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", accessToken);
    client.Timeout = TimeSpan.FromMinutes(10);
});
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();


app.MapGet("/", () => "Hello World!");

app.Run();
