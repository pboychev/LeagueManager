using System;
using Newtonsoft.Json;

namespace LeagueManager.Models
{
    public partial class PrivateLeague
    {
        [JsonProperty("new_entries")]
        public NewEntries NewEntries { get; set; }

        [JsonProperty("last_updated_data")]
        public object LastUpdatedData { get; set; }

        [JsonProperty("league")]
        public League League { get; set; }

        [JsonProperty("standings")]
        public NewEntries Standings { get; set; }
    }

    public partial class League
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("closed")]
        public bool Closed { get; set; }

        [JsonProperty("max_entries")]
        public object MaxEntries { get; set; }

        [JsonProperty("league_type")]
        public string LeagueType { get; set; }

        [JsonProperty("scoring")]
        public string Scoring { get; set; }

        [JsonProperty("admin_entry")]
        public long AdminEntry { get; set; }

        [JsonProperty("start_event")]
        public long StartEvent { get; set; }

        [JsonProperty("code_privacy")]
        public string CodePrivacy { get; set; }

        [JsonProperty("has_cup")]
        public bool HasCup { get; set; }

        [JsonProperty("cup_league")]
        public object CupLeague { get; set; }

        [JsonProperty("rank")]
        public object Rank { get; set; }
    }

    public partial class NewEntries
    {
        [JsonProperty("has_next")]
        public bool HasNext { get; set; }

        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("entry")]
        public long Entry { get; set; }

        [JsonProperty("entry_name")]
        public string EntryName { get; set; }

        [JsonProperty("joined_time")]
        public DateTimeOffset JoinedTime { get; set; }

        [JsonProperty("player_name")]
        public string PlayerName { get; set; }
    }
}
