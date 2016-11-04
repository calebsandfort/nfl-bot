using HtmlAgilityPack;
using NflBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Fizzler.Systems.HtmlAgilityPack;
using NflBot.Framework;

namespace NflBot.Controllers
{
    public class HelperController : ApiController
    { 
        private readonly IMatchupRepository _matchupRepository;

        public HelperController(IMatchupRepository matchupRepository)
        {
            _matchupRepository = matchupRepository;
        }

        // GET: api/Helper
        [Route("helper/teamnamevariations")]
        public String GetTeamNameVariations()
        {
            List<String> teamNameVariations = new List<string>();

            foreach (Teams team in Enum.GetValues(typeof(Teams)))
            {
                teamNameVariations.AddRange(team.GetAttribute<DisplayVariationsAttribute>().DisplayVariations);
            }

            return String.Join(", ", teamNameVariations);
        }


        [Route("helper/scrapefoxschedule/{firstTeam}/{secondTeam}")]
        public async Task<Matchup> GetScrapeFoxSchedule(String firstTeam, String secondTeam)
        {
            Matchup matchup = await Scraper.ScrapeSchedule(firstTeam, secondTeam, this._matchupRepository);

            return matchup;
        }
    }
}
