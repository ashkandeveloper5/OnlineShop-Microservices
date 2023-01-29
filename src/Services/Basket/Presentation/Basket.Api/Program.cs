using Basket.Api.EventBusConsumer;
using Basket.Application.Common.Mapper;
using Basket.Application.Extensions;
using Basket.Application.UOW;
using Basket.DataAccess.Persistence.Context;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

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


builder.Services.AddHttpClient();

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<BasketCheckoutConsumer>();
    config.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        config.ReceiveEndpoint(EventBus.Message.Common.EventBusConstants.BasketCheckoutQueue, c =>
        {
            c.ConfigureConsumer<BasketCheckoutConsumer>(context);
        });
    });
});

builder.Services.AddMassTransitHostedService();
#region DI
RegisterServices(builder.Services);
#endregion

#region JwtAuthentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(optioin =>
{
    optioin.RequireHttpsMetadata = true;
    optioin.SaveToken = true;
    optioin.TokenValidationParameters = new TokenValidationParameters()
    {
        ClockSkew = TimeSpan.Zero,
        RequireExpirationTime = true,
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidIssuer = "AccountServer",
        ValidateAudience = true,
        ValidAudience = "Account",
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AccountApiAccountApiAccountApiAccountApi")),
        TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567891011121")),
    };
    //optioin.Events = new JwtBearerEvents
    //{
    //    OnTokenValidated = async (context) =>
    //    {
    //        var claims = (context.Principal.Identity as ClaimsIdentity).Claims.ToList();
    //        var userUid = claims.Where(u => u.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
    //    },
    //};
});
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

#region Seed Data
app.MigrateDatabase<BasketContext>((context, services) =>
{
    var logger = services.GetService<ILogger<BasketContextSeed>>();
    BasketContextSeed.SeedAsync(context, logger).Wait();
});
#endregion

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();

#region Register Services Method
static void RegisterServices(IServiceCollection services)
{
    //DependencyContainer.RegisterServices(services);
    services.AddTransient<IUnitOfWork, UnitOfWork>();
    services.AddAutoMapper(typeof(MappingProfile));
    services.AddScoped<BasketCheckoutConsumer>();
}
#endregion