using Projects;
using Serilog;

try
{
    var builder = DistributedApplication.CreateBuilder(args);

    builder.Services.AddSerilog(new LoggerConfiguration()
        .WriteTo.Console()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger());

    builder.AddProject<API>("video-api");

    builder.Build().Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}

finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}