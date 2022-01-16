using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using LeagueManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static LeagueManager.Constants.Months;

namespace LeagueManager.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public LeagueService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IEnumerable<PlayerModel>> GetPlayerDetails(int id)
        {
            var league = await GetLeagueAsync();
            var players = GetPlayers(league).ToList();
            await SetPlayersDataAsync(players, id);

            return players;
        }

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

        private async Task<PrivateLeague> GetLeagueAsync()
        {
            PrivateLeague league;

            var leagueCode = GetCookie();
            var leagueUrl = string.Format(_configuration["Fpl:LeagueUrl"], leagueCode);
            var httpClient = _httpClientFactory.CreateClient();
            var result = await httpClient.GetStringAsync(leagueUrl);

            try
            {
                league = JsonConvert.DeserializeObject<PrivateLeague>(result);
            }
            catch (Exception e)
            {
                throw new JsonSerializationException(e.Message);
            }

            return league;
        }

        private string GetCookie()
        {
            var cookie = _httpContextAccessor.HttpContext?.Request.Cookies["leagueCode"];

            return cookie;
        }

        private async Task SetPlayersDataAsync(List<PlayerModel> players, int month)
        {
            foreach (var player in players)
            {
                var history = await GetPlayerHistoryAsync(player);

                if (history == null) continue;

                SetPlayerDetails(month, history, player);
            }
        }

        private static void SetPlayerDetails(int month, PlayerHistory history, PlayerModel player)
        {
            var monthEntry = LeagueMonths.FirstOrDefault(mL => mL.Value == month);
            var score = history
                .Current
                .Where(h => h.Event >= monthEntry?.Start && h.Event <= monthEntry?.End)
                .Sum(h => h.Points);

            var benchScore = history
                .Current
                .Where(h => h.Event >= monthEntry?.Start && h.Event <= monthEntry?.End)
                .Sum(h => h.PointsOnBench);

            var transfersMade = history
                .Current
                .Where(h => h.Event >= monthEntry?.Start && h.Event <= monthEntry?.End)
                .Sum(h => h.EventTransfers);

            var playerTransfersCost = history
                .Current
                .Where(h => h.Event >= monthEntry?.Start && h.Event <= monthEntry?.End)
                .Sum(h => h.EventTransfersCost);

            var chips = history
                .Chips
                .Where(h => h.Event >= monthEntry?.Start && h.Event <= monthEntry?.End)
                .ToList();

            var resultFinal = score - playerTransfersCost;

            SetPlayerChips(player, chips);

            player.Score = resultFinal;
            player.BenchScore = benchScore;
            player.Transfers = transfersMade;
            player.PlayerTransfersCost = playerTransfersCost;
        }

        private static void SetPlayerChips(PlayerModel player, List<Chip> chips)
        {
            var counter = 0;

            foreach (var chip in chips)
            {
                player.ChipsUsed += chip.Name + " - GW: " + chip.Event;
                if (counter != chips.Count - 1)
                {
                    player.ChipsUsed += " | ";
                }

                counter++;
            }
        }

        private async Task<PlayerHistory> GetPlayerHistoryAsync(PlayerModel player)
        {
            PlayerHistory history;

            var historyUrl = string.Format(_configuration["Fpl:HistoryUrl"], player.Entry);
            var httpClient = _httpClientFactory.CreateClient();
            var playerHistory = await httpClient.GetStringAsync(historyUrl);

            try
            {
                history = JsonConvert.DeserializeObject<PlayerHistory>(playerHistory);
            }
            catch (Exception e)
            {
                throw new SerializationException(e.Message);
            }

            return history;
        }
    }
}