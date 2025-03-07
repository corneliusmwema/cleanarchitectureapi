using API.Extensions;
using API.Filters;
using Application;
using HealthChecks.UI.Client;
using Infrastructure;
using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

//add logging
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

//add application services
builder.Services.AddApplication();

//add infrastructure services
builder.Services.AddInfrastructure(builder.Configuration);


//add controllers 
builder.Services.AddControllers(
    options =>
    {
        options.Filters.Add<ApiExceptionFilter>();
        options.Filters.Add<ApiResponseFilter>();
    }
);


//add swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    swagger =>
    {
        //this is to generate the default ui of swagger documentation
        swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoApp.API", Version = "v1" });

        //this is to add the jwt bearer authentication
        swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header
        });
        swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    }
);
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

app.MapDefaultEndpoints();


//use timezone here please,

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c => { c.RouteTemplate = "api/swagger/{documentname}/swagger.json"; });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = "api/swagger";
    });
}

app.MapHealthChecks("api/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.ApplyMigrations();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthentication(); //add this line for using authentication
app.UseMiddleware<UserContextMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();