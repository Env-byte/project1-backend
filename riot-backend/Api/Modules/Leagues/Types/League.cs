namespace riot_backend.Api.Modules.Leagues.Types;

using System;
using Newtonsoft.Json;

public class League
{
    [JsonProperty("leagueId", NullValueHandling = NullValueHandling.Ignore)]
    public Guid? LeagueId { get; set; }

    [JsonProperty("queueType")] public string QueueType { get; set; }

    [JsonProperty("tier", NullValueHandling = NullValueHandling.Ignore)]
    public string Tier { get; set; }

    [JsonProperty("rank", NullValueHandling = NullValueHandling.Ignore)]
    public string Rank { get; set; }

    [JsonProperty("summonerId")] public string SummonerId { get; set; }

    [JsonProperty("summonerName")] public string SummonerName { get; set; }

    [JsonProperty("leaguePoints", NullValueHandling = NullValueHandling.Ignore)]
    public long? LeaguePoints { get; set; }

    [JsonProperty("wins")] public long Wins { get; set; }

    [JsonProperty("losses")] public long Losses { get; set; }

    [JsonProperty("veteran", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Veteran { get; set; }

    [JsonProperty("inactive", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Inactive { get; set; }

    [JsonProperty("freshBlood", NullValueHandling = NullValueHandling.Ignore)]
    public bool? FreshBlood { get; set; }

    [JsonProperty("hotStreak", NullValueHandling = NullValueHandling.Ignore)]
    public bool? HotStreak { get; set; }

    [JsonProperty("ratedTier", NullValueHandling = NullValueHandling.Ignore)]
    public string RatedTier { get; set; }

    [JsonProperty("ratedRating", NullValueHandling = NullValueHandling.Ignore)]
    public long? RatedRating { get; set; }
}