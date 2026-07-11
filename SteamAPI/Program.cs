using SteamAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApiServices(builder.Configuration);

var app = builder.Build();

app.UseWebApiPipeline();

app.Run();
