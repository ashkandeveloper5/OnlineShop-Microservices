using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddOcelot().AddCacheManager(x =>
{
    x.WithDictionaryHandle();
});
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Configuration.AddJsonFile($"appsettings.json",true,true);
builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json",true,true);
builder.Configuration.AddEnvironmentVariables();



#region JwtAuthentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("AccountApiAccountApiAccountApiAccountApi", optioin =>
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


var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();


app.MapGet("/", () => "Hello World!");

app.UseOcelot().Wait();




app.Run();
