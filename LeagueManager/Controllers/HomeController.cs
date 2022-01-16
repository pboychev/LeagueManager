using LeagueManager.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LeagueManager.Controllers
{
    public class HomeController : Controller
    {
        public List<Month> monthList = new List<Month>()
        {
            new Month(){Value=0,Start=1,End=38},
            new Month(){Value=1,Start=1,End=3}, //August      Gameweek 1 Gameweek 3
            new Month(){Value=2,Start=4,End=6}, //September   Start=4,End=6}, 
            new Month(){Value=3,Start=7,End=10}, //October    Start=7,End=10},
            new Month(){Value=4,Start=11,End=14}, //November  Start=11,End=14}
            new Month(){Value=5,Start=15,End=20}, //December  Start=15,End=20}
            new Month(){Value=6,Start=21,End=23}, //January   Start=21,End=23}
            new Month(){Value=7,Start=24,End=27}, //February  Start=24,End=27}
            new Month(){Value=8,Start=28,End=30}, //March     Start=28,End=30}
            new Month(){Value=9,Start=31,End=35}, //April     Start=31,End=35}
            new Month(){Value=10,Start=36,End=38}, //May       Start=36,End=38}
        };

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Rules()
        {
            return View();
        }

        public ActionResult Chips()
        {
            return View();
        }

        public async Task<ActionResult> Details(int id)
        {
            HttpClient client = new HttpClient();

            var leagueCode = GetCookie();



            var result = await client.GetStringAsync($"https://fantasy.premierleague.com/api/leagues-classic/{leagueCode}/standings/");
            PrivateLeague viewModel = JsonConvert.DeserializeObject<PrivateLeague>(result);
            List<PlayerModel> viewModels = new List<PlayerModel>();

            foreach (var item in viewModel.Standings.Results)
            {
                viewModels.Add(new PlayerModel()
                {
                    Entry = item.Entry,
                    Team = item.EntryName,
                    PlayerName = item.PlayerName
                });

            }

            foreach (var model in viewModels)
            {
                var url = $"https://fantasy.premierleague.com/api/entry/{model.Entry}/history/";
                var temp = client.GetStringAsync(url).Result;

                var poco = JsonConvert.DeserializeObject<PlayerPoco>(temp);
                var monthEntry = monthList.FirstOrDefault(mL => mL.Value == id);
                var score = poco.Current.Where(x => x["event"] >= monthEntry?.Start && x["event"] <= monthEntry?.End).Sum(x => x["points"]);
                var benchScore = poco.Current.Where(x => x["event"] >= monthEntry?.Start && x["event"] <= monthEntry?.End).Sum(x => x["points_on_bench"]);
                var transfersMade = poco.Current.Where(x => x["event"] >= monthEntry?.Start && x["event"] <= monthEntry?.End).Sum(x => x["event_transfers"]);
                var playerTransfersCost = poco.Current.Where(x => x["event"] >= monthEntry?.Start && x["event"] <= monthEntry?.End).Sum(x => x["event_transfers_cost"]);
                var test = poco.Chips.Where(x => int.Parse(x["event"]) >= monthEntry?.Start && int.Parse(x["event"]) <= monthEntry?.End);
                var resultFinal = score - playerTransfersCost;

                var counter = 0;
                foreach (var testItem in test)
                {
                    model.ChipsUsed += testItem["name"] + " - GW: " + testItem["event"];
                    if (counter != test.Count() - 1)
                    {
                        model.ChipsUsed += " | ";
                    }
                    counter++;
                }

                model.Score = resultFinal;
                model.BenchScore = benchScore;
                model.Transfers = transfersMade;
                model.PlayerTransfersCost = playerTransfersCost;
            }

            return PartialView("_PlayersTable", viewModels.OrderByDescending(x => x.Score).ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public string GetCookie()
        {
            var cookie = Request.Cookies["leagueCode"];

            return cookie;
        }
    }
}