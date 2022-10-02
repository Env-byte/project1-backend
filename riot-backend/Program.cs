using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;
using riot_backend.Api;
using riot_backend.Api.Modules.GoogleAuth;
using riot_backend.Api.Modules.Leagues;
using riot_backend.Api.Modules.Matches;
using riot_backend.Api.Modules.Summoner;
using riot_backend.Api.Modules.TeamComps;
using riot_backend.Api.Modules.Users;
using riot_backend.Middleware;
using riot_backend.ScopedTypes;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

configuration.AddJsonFile($"appsettings.{env}.json", true, true);
//allow dapper to match database columns
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IHttpClientWrapper, HttpClientWrapper>();
builder.Services.AddSingleton<DatabaseFactory>();

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();
builder.Services.AddScoped<Header>();
builder.Services.AddScoped<GoogleAuthService>();

//add repository's
builder.Services.AddScoped<SummonerRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<MatchRepository>();
builder.Services.AddScoped<TeamCompRepository>();
builder.Services.AddScoped<LeagueRepository>();

//add providers
builder.Services.AddScoped<SummonerProvider>();
builder.Services.AddScoped<MatchProvider>();
builder.Services.AddScoped<LeagueProvider>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (env == "Development")
{
    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = ctx =>
        {
            ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
            ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers",
                "Origin, X-Requested-With, Content-Type, Accept");
        }
    });
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
//app.UseHttpsRedirection();
app.UseAuthorization();
app.UseHeaderHandler();
app.MapControllers();
app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().WithMethods());
Console.WriteLine("database: " + configuration.GetConnectionString("database"));
app.Run();