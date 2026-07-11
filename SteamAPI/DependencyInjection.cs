using BusinessLogic.Configurations;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using SteamApi.Middlewares;
using System.Text;


namespace SteamAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connStr = configuration.GetConnectionString("RemoteDb")
                ?? throw new Exception("Connection string not found");

            services.AddDbContext<SteamDbContext>(options =>
                options.UseSqlServer(connStr));

            services.AddControllers();
            services.AddOpenApi();
            services.AddAutoMapper(cfg => { }, typeof(MapperProfile));

            // BLL services
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPasswordService, PasswordService>();

            // Configurations
            services.AddOptions<ScalarOptions>().BindConfiguration("Scalar");
            services.AddOptions<JwtOptions>()
                .Bind(configuration.GetSection(nameof(JwtOptions)))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            services.AddOptions<EmailOptions>()
                .Bind(configuration.GetSection(nameof(EmailOptions)))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            // Identity
            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<SteamDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                int tokenLifetimeMinutes = configuration.GetValue<int>("ResetPasswordTokenLifetimeInMinutes");
                options.TokenLifespan = TimeSpan.FromMinutes(tokenLifetimeMinutes);
            });

            // JWT auth
            var jwtOpts = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>()
                ?? throw new Exception("Jwt options not found");

            services.AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOpts.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpts.Key)),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSteamApp", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            return services;
        }

        public static WebApplication UseWebApiPipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference("", options => options.WithTitle("Steam API"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowSteamApp");
            app.UseErrorHandler();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
    }

}
