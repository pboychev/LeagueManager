using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LeagueManager.Models
{
    public class PlayerHistory
    {
        public List<Current> Current { get; set; }
        public List<object> Past { get; set; }
        public List<Chip> Chips { get; set; }
    }

    public class Current
    {
        public int Event { get; set; }
        public int Points { get; set; }
        public int Rank { get; set; }
        public int Bank { get; set; }
        public int Value { get; set; }

        [JsonProperty("total_points")]
        public int TotalPoints { get; set; }
        [JsonProperty("overall_rank")]
        public int OverallRank { get; set; }
        [JsonProperty("rank_sort")]
        public int RankSort { get; set; }
        [JsonProperty("event_transfers")]
        public int EventTransfers { get; set; }
        [JsonProperty("event_transfers_cost")]
        public int EventTransfersCost { get; set; }
        [JsonProperty("points_on_bench")]
        public int PointsOnBench { get; set; }
    }

    public class Chip
    {
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public int Event { get; set; }
    }
}