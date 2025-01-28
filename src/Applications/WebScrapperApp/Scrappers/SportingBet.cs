using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using WebScrapperApp.HttpClients;

namespace WebScrapperApp.Scrappers
{
    /// <summary>
    /// Get the football odds from sportingbet website 
    /// Url -> https://sports.sportingbet.co.za/en/sports/football-4/after-2-days
    /// </summary>
    public class SportingBet
    {
        public static int GetGames()
        {

            var apiClient = new GamesApiClient();

            var total = 0;

            //HtmlWeb web = new HtmlWeb();
            //var htmlDoc = web.LoadFromWebAsync("https://sports.sportingbet.co.za/en/sports/football-4/after-2-days").Result;

            //var filename = @"C:\Users\bbdnet0443\source\repos\Siyaphanda\src\Applications\WebScrapperApp\Data\Sportingbet_20250125.html";
            //var filename = @"src\Applications\WebScrapperApp\Data\Sportingbet_20250125.html";
            var filename = @"\Data\Sportingbet_20250125.html";

            if (File.Exists(filename))
            {
                var doc = new HtmlDocument();

                doc.Load(filename);

                var eventGroups = doc.QuerySelectorAll("ms-event-group.event-group");

                foreach (var eventGroup in eventGroups)
                {

                    var isPairGame = eventGroup.QuerySelectorAll("div.participants-pair-game");

                    if (isPairGame?.Count > 0)
                    {
                        var tournament = eventGroup.QuerySelector("div.title")?.InnerText?.Trim();

                        var events = eventGroup.QuerySelectorAll("div.calendar-grid-info");

                        foreach (var item in events)
                        {
                            var participants = item.QuerySelectorAll("div.participant");

                            var game = new
                            {
                                startTime = item.QuerySelector("ms-prematch-timer.starting-time")?.InnerText?.Trim(),
                                tournament,
                                source = 1,
                                timeStamp = DateTime.Now,
                                homeTeam = GetTeam(participants?.FirstOrDefault()?.InnerText?.Trim() ?? ""),
                                awayTeam = GetTeam(participants?.LastOrDefault()?.InnerText?.Trim() ?? ""),
                                gameOdds = GetGameOdds(item)
                            };

                            _ = GamesApiClient.CreateGameAsync(game);
                            total++;
                        }
                    }
                }
            }

            return total;
        }


        private static List<Object> GetGameOdds(HtmlNode? item)
        {
            var odds = item.QuerySelectorAll("ms-font-resizer");

            var h2h = new 
            {
                oddsType = 0,
                homeWin = odds[0]?.InnerText?.Trim(),
                draw = odds[1]?.InnerText?.Trim(),
                awayWin = odds[2]?.InnerText?.Trim(),

            };

            var goals = odds.Count > 3 ? odds[3]?.InnerText?.Trim() : "-";

            var noOfGoals = new
            {
                oddsType = 2,
                total = $"{goals} Goals",
                over = odds.Count > 4 ? odds[4]?.InnerText?.Trim() : "-",
                under = odds.Count > 5 ? odds[5]?.InnerText?.Trim() : "-",
            };

            var bts = new
            {
                oddsType = 1,
                yes = odds.Count > 6 ? odds[6]?.InnerText?.Trim() : "-",
                no = odds.Count > 7 ? odds[7]?.InnerText?.Trim() : "-"
            };

            return [h2h, bts, noOfGoals];
        }


        private static Object GetTeam(string name)
        {
            // DOTO : Add code to get team from DB using the given name

            return new 
            {
                Name = name,
                LeaguePosition = Int32.MaxValue,
                OtherNames = new List<string> { "name1", "name2" }
            };
        }
    }
}
