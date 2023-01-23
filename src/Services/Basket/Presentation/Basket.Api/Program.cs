using Basket.Application.UOW;
using Basket.DataAccess.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region JwtBearer
builder.Services.AddSwaggerGen(option =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Enter Token",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    option.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});
#endregion

#region DI
RegisterServices(builder.Services);
#endregion

#region Base Settings
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region SqlServerConnection
builder.Services.AddDbContext<BasketContext>(options => { options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectionString"]); }, ServiceLifetime.Transient);
//builder.Services.AddDbContext<AccountContext>(options => options.UseSqlServer("Data Source=.; Initial Catalog = AccountApiDB; Integrated Security=True"));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

#region Register Services Method
static void RegisterServices(IServiceCollection services)
{
    //DependencyContainer.RegisterServices(services);
    services.AddTransient<IUnitOfWork, UnitOfWork>();
    services.AddAutoMapper(Assembly.GetExecutingAssembly());
}
#endregion