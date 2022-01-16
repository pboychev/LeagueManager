using System.Collections.Generic;
using System.Threading.Tasks;
using LeagueManager.Models;

namespace LeagueManager.Services
{
    public interface ILeagueService
    {
        Task<IEnumerable<PlayerModel>> GetPlayerDetails(int id);
    }
}