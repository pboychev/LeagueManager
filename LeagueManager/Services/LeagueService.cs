using LeagueManager.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LeagueManager.Constants;

namespace LeagueManager.Services
{
    public class LeagueService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HttpClient httpClient;

        public LeagueService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        //private async Task GetPlayer(IEnumerable<PlayerModel> players)
        //{
        //    foreach (var player in players)
        //    {
        //        var url = $"https://fantasy.premierleague.com/api/entry/{player.Entry}/history/";
        //        var playerHistory = await httpClient.GetStringAsync(url);

        //        var poco = JsonConvert.DeserializeObject<PlayerPoco>(playerHistory);
        //        var monthEntry = Months.MonthList.FirstOrDefault(mL => mL.Value == id);
        //        var score = poco.Current.Where(x => x["event"] >= monthEntry?.Start && x["event"] <= monthEntry?.End).Sum(x => x["points"]);
        //        var benchScore = poco.Current.Where(x => x["event"] >= monthEntry?.Start && x["event"] <= monthEntry?.End).Sum(x => x["points_on_bench"]);
        //        var transfersMade = poco.Current.Where(x => x["event"] >= monthEntry?.Start && x["event"] <= monthEntry?.End).Sum(x => x["event_transfers"]);
        //        var playerTransfersCost = poco.Current.Where(x => x["event"] >= monthEntry?.Start && x["event"] <= monthEntry?.End).Sum(x => x["event_transfers_cost"]);
        //        var test = poco.Chips.Where(x => int.Parse(x["event"]) >= monthEntry?.Start && int.Parse(x["event"]) <= monthEntry?.End);
        //        var resultFinal = score - playerTransfersCost;

        //        var counter = 0;
        //        foreach (var testItem in test)
        //        {
        //            player.ChipsUsed += testItem["name"] + " - GW: " + testItem["event"];
        //            if (counter != test.Count() - 1)
        //            {
        //                player.ChipsUsed += " | ";
        //            }
        //            counter++;
        //        }

        //        player.Score = resultFinal;
        //        player.BenchScore = benchScore;
        //        player.Transfers = transfersMade;
        //        player.PlayerTransfersCost = playerTransfersCost;
        //    }

        //    return player;
        //}

        private IEnumerable<PlayerModel> GetPlayers(PrivateLeague league)
        {
            return league
            .Standings
            .Results
            .Select(p => new PlayerModel
            {
                Entry = p.Entry,
                Team = p.EntryName,
                PlayerName = p.PlayerName
            });
        }

        private async Task<PrivateLeague> GetLeagueResultsAsync()
        {
            var client = new HttpClient();
            var leagueCode = GetCookie();
            var result = await client.GetStringAsync($"https://fantasy.premierleague.com/api/leagues-classic/{leagueCode}/standings/");
            PrivateLeague league;

            try
            {
                league = JsonConvert.DeserializeObject<PrivateLeague>(result);
            }
            catch (Exception)
            {
                throw new JsonSerializationException();
            }

            return league;
        }

        private string GetCookie()
        {
            var cookie = httpContextAccessor.HttpContext.Request.Cookies["leagueCode"];

            return cookie;
        }
    }
}
