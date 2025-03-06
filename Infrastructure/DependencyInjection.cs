
using Application.Interfaces.RepositoryInterfaces;
using Infrastructure.Configurations;
using Infrastructure.ExternalServices;
using Infrastructure.Persistence.Data;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            

            // Configure DbContext for MySQL
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new ArgumentNullException(nameof(configuration), "Connection string 'DefaultConnection' not found.");

            services.AddDbContext<VideoDBContext>((sp, optionsBuilder) =>
            {
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            });


            // Add JWT authentication
            var jwtSettings = configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettings);


            var secretKey = jwtSettings["SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException(nameof(secretKey), "JWT SecretKey is not configured.");
            }
            var key = Encoding.UTF8.GetBytes(secretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    //ClockSkew = TimeSpan.Zero // Immediate expiration
                };
            });


            // Register repositories in the Infrastructure layer
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            // services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IVideoRepository, VideoRepository>();

            // Add more repositories here
            // services.AddTransient<IOtherRepository, OtherRepository>();

            // Add health checks
            services.AddHealthChecks()
                .AddDbContextCheck<VideoDBContext>();

            // Build service provider to get logger
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<VideoDBContext>>();

            try
            {
                // Validate the database connection
                using (var scope = serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<VideoDBContext>();
                    dbContext.Database.OpenConnection();
                    dbContext.Database.CloseConnection();
                }

                if (logger != null)
                {
                    logger.LogInformation("Database connection successfully configured.");
                }
            }
            catch (Exception ex)
            {
                if (logger != null)
                {
                    logger.LogError(ex, "An error occurred while configuring the database connection.");
                }
                throw new Exception("Database connection failed");
            }

            return services;
        }
    }
}