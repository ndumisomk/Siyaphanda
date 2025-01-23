using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapperApp.Models;

namespace WebScrapperApp
{
    public class SBDataScrapper
    {
        public List<Game> GetGames()
        {
            var games = new List<Game>();

            //HtmlWeb web = new HtmlWeb();
            //var htmlDoc = web.LoadFromWebAsync("https://sports.sportingbet.co.za/en/sports/football-4/after-2-days").Result;

            var filename = "C:\\Users\\bbdnet0443\\source\\repos\\Siyaphanda\\src\\Applications\\WebScrapperApp\\Data\\Sportingbet_20250125.html";

            if (File.Exists(filename))
            {
                var doc = new HtmlDocument();

                doc.Load(filename);

                var eventGroups = doc.QuerySelectorAll("ms-event-group.event-group");

                foreach (var eventGroup in eventGroups) {

                    var isPairGame = eventGroup.QuerySelectorAll("div.participants-pair-game");

                    if (isPairGame?.Count > 0)
                    {
                        var tournament = eventGroup.QuerySelector("div.title")?.InnerText?.Trim();

                        var events = eventGroup.QuerySelectorAll("div.calendar-grid-info");

                        foreach (var item in events)
                        {
                            var odds = item.QuerySelectorAll("ms-font-resizer");

                            var goals = odds.Count > 3 ? odds[3]?.InnerText?.Trim() : "-";
                            var total = new Totals
                            {
                                Name = $"{goals} Goals",
                                Over = odds.Count > 4 ? odds[4]?.InnerText?.Trim() : "-",
                                Under = odds.Count > 5 ? odds[5]?.InnerText?.Trim() : "-",
                            };

                            var gameOdds = new Odds
                            {
                                HomeWin = odds[0]?.InnerText?.Trim(),
                                Draw = odds[1]?.InnerText?.Trim(),
                                AwayWin = odds[2]?.InnerText?.Trim(),
                                BTS = new BothTeamsScore
                                {
                                    Yes = odds.Count > 6 ? odds[6]?.InnerText?.Trim() : "-",
                                    No = odds.Count > 7 ? odds[7]?.InnerText?.Trim() : "-"
                                },
                                Totals = [ total ]
                            };

                            var participants = item.QuerySelectorAll("div.participant");

                            var game = new Game
                            {
                                Tournament = tournament,
                                StartTime = item.QuerySelector("ms-prematch-timer.starting-time")?.InnerText?.Trim(),
                                HomeTeam = participants?.FirstOrDefault()?.InnerText?.Trim(),
                                AwayTeam = participants?.LastOrDefault()?.InnerText?.Trim(),
                                GameOdds = gameOdds
                            };

                            games.Add(game);
                        }
                    }

                }

            }

            return games;
        }
    }
}
