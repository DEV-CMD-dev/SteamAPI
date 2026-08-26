using DataAccess;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using SteamAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApiServices(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<SteamDbContext>();
	await context.Database.MigrateAsync();
	await context.SeedAchievementsAsync();
	await context.SeedRecentlyPlayedGamesAsync();
}

app.UseWebApiPipeline();

app.Run();
