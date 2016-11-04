using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using NflBot.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NflBot.Framework
{
    public static class Scraper
    {
        public async static Task<Matchup> ScrapeSchedule(String firstTeamString, String secondTeamString, IMatchupRepository matchupRepository)
        {
            Matchup matchup = null;
            var parser = new Chronic.Parser();
            String currentDate = String.Empty;
            String startTime = String.Empty;
            DateTime when = DateTime.MinValue;
            List<HtmlNode> statusNodes = null;

            Teams firstTeam = Teams.None;
            Teams secondTeam = Teams.None;

            if (!firstTeamString.FindTeamByNameVariation(out firstTeam) || !secondTeamString.FindTeamByNameVariation(out secondTeam))
            {
                return matchup;
            }

            Matchup existing = matchupRepository.GetByTeams(firstTeam, secondTeam);
            if (existing != null)
            {
                matchup = existing;
                return matchup;
            }

            Teams awayTeam = Teams.None;
            Teams homeTeam = Teams.None;
            Networks network = Networks.None;

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(await Methods.DownloadPageStringAsync("http://www.foxsports.com/nfl/schedule"));

            HtmlNode scheduleTable = doc.DocumentNode.QuerySelector("table.wisbb_scheduleTable");

            foreach(HtmlNode el in scheduleTable.ChildNodes)
            {
                if(el.Name == "thead")
                {
                    currentDate = el.QuerySelector("th").InnerText;
                }
                else if(el.Name == "tbody")
                {
                    foreach(HtmlNode row in el.ChildNodes.Where(x => x.Name == "tr"))
                    {
                        try
                        {
                            Matchup temp = new Matchup();

                            statusNodes = row.QuerySelectorAll("td.wisbb_gameInfo .wisbb_status").ToList();
                            if (statusNodes.Count == 0) continue;

                            if (row.QuerySelector(".wisbb_firstTeam .wisfb_logoImage").Attributes["alt"].Value.FindTeamByNameVariation(out awayTeam)
                                && row.QuerySelector(".wisbb_secondTeam .wisfb_logoImage").Attributes["alt"].Value.FindTeamByNameVariation(out homeTeam))
                            {
                                temp.AwayTeamEnum = awayTeam;
                                temp.HomeTeamEnum = homeTeam;

                                startTime = statusNodes.LastOrDefault(x => !String.IsNullOrEmpty(x.InnerText)).InnerText;
                                startTime = startTime.Replace("a", " AM").Replace("p", " PM").Replace(" ET", String.Empty);

                                temp.WhenDate = DateTime.ParseExact($"{currentDate} {startTime}", "dddd, MMMM d h:mm tt", CultureInfo.InvariantCulture);


                                temp.Where = row.QuerySelector(".wisbb_location").InnerText.Replace("&amp;", "&").Trim();

                                row.QuerySelector(".wisbb_network").InnerText.Trim().FindNetworkByNameVariation(out network);
                                temp.NetworkEnum = network;

                                matchupRepository.AddOrUpdate(temp);

                                if ((firstTeam == awayTeam && secondTeam == homeTeam) || (secondTeam == awayTeam && firstTeam == homeTeam))
                                {
                                    matchup = temp;
                                    return matchup;
                                }
                            }
                        }
                        catch (Exception)
                        {

                        }           
                    }
                }
            }

            return matchup;
        }
    }
}