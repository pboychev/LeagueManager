using System.Linq;
using System.Threading.Tasks;
using LeagueManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeagueManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILeagueService _leagueService;

        public HomeController(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chips()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var playerDetails = await _leagueService.GetPlayerDetails(id);

            return PartialView("_PlayersTable", playerDetails.OrderByDescending(x => x.Score).ToList());
        }
    }
}