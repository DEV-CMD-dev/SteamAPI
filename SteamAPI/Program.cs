using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SteamDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RemoteDb")));


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(cfg => { }, typeof(MapperProfile));
builder.Services.AddScoped<Shared.Interfaces.IGamesService, Shared.Services.GamesService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Configure swagger ui
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SteamAPI");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
