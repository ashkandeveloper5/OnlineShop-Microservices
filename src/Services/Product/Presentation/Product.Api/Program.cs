using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Product.Application.Extensions;
using Product.Application.UOW;
using Product.Common.Utilities.Mapping;
using Product.Domain.Interfaces.BaseInterfaces;
using Product.IoC.DependencyContainer;
using Product.Persistence.Context;
using Product.Persistence.Repository;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Base Settings
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
//"ConnectionString": "Data Source=.; Initial Catalog = ProductApiDB; Integrated Security=True;TrustServerCertificate=True"
#region DI
RegisterServices(builder.Services);
#endregion

#region SqlServerConnection
builder.Services.AddDbContext<ProductContext>(options => { options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectionStringDocker"]); }, ServiceLifetime.Transient);
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
app.MigrateDatabase<ProductContext>((context, services) =>
{
    var logger = services.GetService<ILogger<ProductContextSeed>>();
    ProductContextSeed.SeedAsync(context, logger).Wait();
});
#endregion

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#region Register Services Method
static void RegisterServices(IServiceCollection services)
{
    DependencyContainer.RegisterServices(services);
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddAutoMapper(typeof(MappingProfile));
}
#endregion