using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Newtonsoft.Json;
using NflBot.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NflBot.Models
{
    [LuisModel("b53ff76d-462e-4f91-a6a3-14ff79f1bd07", "65e28aead39846b88f4df18b5380b01f")]
    [Serializable]
    public class NflBotLuisDialog : LuisDialog<object>
    {
        private readonly IMatchupRepository _matchupRepository;

        public NflBotLuisDialog(IMatchupRepository matchupRepository)
        {
            _matchupRepository = matchupRepository;
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry I did not understand: " + string.Join(", ", result.Intents.Select(i => i.Intent));
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Matchup")]
        public async Task Matchup(IDialogContext context, LuisResult result)
        {
            List<String> teams = new List<String>();

            foreach(EntityRecommendation entityRecommendation in result.Entities)
            {
                switch (entityRecommendation.Type)
                {
                    case "Team":
                        teams.Add(entityRecommendation.Entity);
                        break;
                }
            }

            if(teams.Count == 2)
            {
                Matchup matchup = await Scraper.ScrapeSchedule(teams[0], teams[1], this._matchupRepository);
                
                await context.PostAsync(JsonConvert.SerializeObject(matchup));
            }
            else
            {
                string message = $"Sorry I did not understand: " + string.Join(", ", result.Intents.Select(i => i.Intent));
                await context.PostAsync(message);
            }

            context.Wait(MessageReceived);
        }
    }
}