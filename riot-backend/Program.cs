using System.Text.Json.Serialization;
using riot_backend.Api;
using riot_backend.Api.Modules.Champions;
using riot_backend.Api.Modules.GoogleAuth;
using riot_backend.Api.Modules.Items;
using riot_backend.Api.Modules.Leagues;
using riot_backend.Api.Modules.Matches;
using riot_backend.Api.Modules.Summoner;
using riot_backend.Api.Modules.TeamComps;
using riot_backend.Api.Modules.Traits;
using riot_backend.Api.Modules.Users;
using riot_backend.Middleware;
using riot_backend.Services;

var builder = WebApplication.CreateBuilder(args);

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

//add services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<SummonerService>();
builder.Services.AddScoped<MatchService>();
builder.Services.AddScoped<TraitService>();
builder.Services.AddScoped<TeamCompService>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<GoogleAuthService>();
builder.Services.AddScoped<ChampionService>();
builder.Services.AddScoped<LeagueService>();
builder.Services.AddScoped<RegionService>();

//add repository's
builder.Services.AddScoped<SummonerRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<MatchRepository>();
builder.Services.AddScoped<TeamCompRepository>();
builder.Services.AddScoped<LeagueRepository>();

//add providers
builder.Services.AddScoped<SummonerProvider>();
builder.Services.AddScoped<TraitProvider>();
builder.Services.AddScoped<MatchProvider>();
builder.Services.AddScoped<ItemProvider>();
builder.Services.AddScoped<ChampionProvider>();
builder.Services.AddScoped<LeagueProvider>();

var app = builder.Build();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
        ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers",
            "Origin, X-Requested-With, Content-Type, Accept");
    }
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRegionHandler();
app.MapControllers();
app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().WithMethods());

app.Run();