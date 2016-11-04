using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NflBot.Models
{
    [Serializable]
    public class InMemoryMatchupRepository : IMatchupRepository
    {
        private static Dictionary<String, Matchup> _matchups = new Dictionary<String, Matchup>();

        public Matchup GetByTeams(Teams firstTeam, Teams secondTeam)
        {
            String key1 = $"{firstTeam.GetAttribute<DisplayAttribute>().Display}.{secondTeam.GetAttribute<DisplayAttribute>().Display}";
            String key2 = $"{secondTeam.GetAttribute<DisplayAttribute>().Display}.{firstTeam.GetAttribute<DisplayAttribute>().Display}";

            if (_matchups.ContainsKey(key1))
            {
                return _matchups[key1];
            }
            else if (_matchups.ContainsKey(key2))
            {
                return _matchups[key2];
            }
            return null;
        }

        public IEnumerable<Matchup> List()
        {
            return _matchups.Values;
        }

        public void AddOrUpdate(Matchup matchup)
        {
            if (_matchups.ContainsKey(matchup.Key))
            {
                _matchups[matchup.Key] = matchup;
            }
            else
            {
                _matchups.Add(matchup.Key, matchup);
            }
        }
    }
}