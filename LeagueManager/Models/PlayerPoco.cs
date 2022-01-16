using Newtonsoft.Json;
using System.Collections.Generic;

namespace LeagueManager.Models
{
    public partial class PlayerPoco
    {
        [JsonProperty("current")]
        public Dictionary<string, long>[] Current { get; set; }
        public Dictionary<string, string>[] Chips { get; set; }
    }
}