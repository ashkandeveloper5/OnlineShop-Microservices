using Microsoft.EntityFrameworkCore;
using Product.Application.Extensions;
using Product.Application.UOW;
using Product.Grpc.Mapping;
using Product.Grpc.Services;
using Product.IoC.DependencyContainer;
using Product.Persistence.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
builder.Services.AddGrpc();
#region DI
RegisterServices(builder.Services);
#endregion
#region SqlServerConnection
builder.Services.AddDbContext<ProductContext>(options => { options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectionStringDocker"]); }, ServiceLifetime.Transient);
//builder.Services.AddDbContext<AccountContext>(options => options.UseSqlServer("Data Source=.; Initial Catalog = AccountApiDB; Integrated Security=True"));
#endregion

// Add services to the container.

var app = builder.Build();


// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
//app.UseEndpoints(endpoints =>
//{

//    endpoints.MapGet("/", async context =>
//    {
//        await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
//    });
//});
#region Seed Data
app.MigrateDatabase<ProductContext>((context, services) =>
{
    var logger = services.GetService<ILogger<ProductContextSeed>>();
    ProductContextSeed.SeedAsync(context, logger).Wait();
});
#endregion
app.UseRouting();
app.MapGrpcService<ProductService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

#region Register Services Method
static void RegisterServices(IServiceCollection services)
{
    DependencyContainer.RegisterServices(services);
    services.AddGrpc();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddAutoMapper(typeof(MappingProfile));
}
#endregion