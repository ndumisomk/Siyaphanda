namespace Games.API.Models
{
    public record Game(
        string StartTime = "",
        string Tournament = "",
        DataSource? Source = null,
        DateTime? TimeStamp = null,
        Team? HomeTeam = null,
        Team? AwayTeam = null,
        List<Object>? GameOdds = null
    )
    { 
        public Guid Id { get; set; }
    };

    public enum OddsTypes
    {
        Head2Head = 0,
        BothTeamsScore = 1,
        NumberOfGoals = 2
    }

    public enum DataSource
    {
        SportingBet_za = 1,
        WorldSportBetting_za = 2,
        Superbet_za = 3,
        Flashscore = 4
    }

    public record class Team(string Name, int LeaguePosition)
    {
        public required string[] OtherNames { get; set; }
    };

    public interface IOdds
    {
        OddsTypes OddsType { get; }
    }

    public class Head2Head
    {
        public OddsTypes OddsType => OddsTypes.Head2Head;
        public string? HomeWin { get; set; }
        public string? Draw { get; set; }
        public string? AwayWin { get; set; }
    }

    public class BothTeamsScore
    {
        public OddsTypes OddsType => OddsTypes.BothTeamsScore;
        public string? Yes { get; set; }
        public string? No { get; set; }
    }

    public class NumberOfGoals
    {
        public OddsTypes OddsType => OddsTypes.NumberOfGoals;
        public string? Total { get; set; }
        public string? Over { get; set; }
        public string? Under { get; set; }
    }
}
