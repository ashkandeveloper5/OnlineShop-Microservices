using Ocelot.DependencyInjection;
using Ocelot.Middleware;



var builder = WebApplication.CreateBuilder(args);




builder.Services.AddOcelot();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json",true,true);

//builder.Services.AddOcelot
//builder.Logging.((hostingContext, loggingBuilder) =>
//{
//    loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
//    loggingBuilder.AddConsole();
//    loggingBuilder.AddDebug();
//});



var app = builder.Build();


app.UseOcelot();

app.MapGet("/", () => "Hello World!");





app.Run();
