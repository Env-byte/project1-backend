using System.Text.Json.Serialization;
using riot_backend.Api;
using riot_backend.Api.Modules.GoogleAuth;
using riot_backend.Api.Modules.Items;
using riot_backend.Api.Modules.Matches;
using riot_backend.Api.Modules.Summoner;
using riot_backend.Api.Modules.TeamComps;
using riot_backend.Api.Modules.Traits;
using riot_backend.Api.Modules.Users;

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

//add repository's
builder.Services.AddScoped<SummonerRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<MatchRepository>();
builder.Services.AddScoped<TeamCompRepository>();

//add providers
builder.Services.AddScoped<SummonerProvider>();
builder.Services.AddScoped<TraitProvider>();
builder.Services.AddScoped<MatchProvider>();
builder.Services.AddScoped<ItemProvider>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().WithMethods());

app.Run();