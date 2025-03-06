using API.Extensions;
using API.Filters;
using Application;
using HealthChecks.UI.Client;
using Infrastructure;
using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

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
        swagger.SwaggerDoc("v1", new() { Title = "TodoApp.API", Version = "v1" });

        //this is to add the jwt bearer authentication
        swagger.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        });
        swagger.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
               Array.Empty<string>()
            }
        });

    }
);

var app = builder.Build();


//use timezone here please,

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
 {
     c.RouteTemplate = "api/swagger/{documentname}/swagger.json";
 });
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
app.UseAuthentication();//add this line for using authentication
app.UseMiddleware<UserContextMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();
