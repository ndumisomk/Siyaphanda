using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapperApp.Models
{
    public class Game
    {
        public string? StartTime { get; set; }
        public string? Tournament { get; set; }
        public string? HomeTeam { get; set; }
        public string? AwayTeam { get; set; }
        public Odds? GameOdds { get; set; }
    }
}
