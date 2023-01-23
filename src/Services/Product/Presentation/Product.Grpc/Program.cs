using Microsoft.EntityFrameworkCore;
using Product.Application.UOW;
using Product.IoC.DependencyContainer;
using Product.Persistence.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
#region DI
RegisterServices(builder.Services);
#endregion
#region SqlServerConnection
builder.Services.AddDbContext<ProductContext>(options => { options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectionString"]); }, ServiceLifetime.Transient);
//builder.Services.AddDbContext<AccountContext>(options => options.UseSqlServer("Data Source=.; Initial Catalog = AccountApiDB; Integrated Security=True"));
#endregion

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

#region Register Services Method
static void RegisterServices(IServiceCollection services)
{
    DependencyContainer.RegisterServices(services);
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddAutoMapper(Assembly.GetExecutingAssembly());
}
#endregion