using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NflBot.Models
{
    public interface IMatchupRepository
    {
        Matchup GetByTeams(Teams firstTeam, Teams secondTeam);
        IEnumerable<Matchup> List();
        void AddOrUpdate(Matchup matchup);
    }
}
