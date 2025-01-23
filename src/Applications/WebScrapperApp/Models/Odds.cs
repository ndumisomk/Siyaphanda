using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapperApp.Models
{
    public record Odds {
        public string? HomeWin { get; set; }
        public string? Draw { get; set; }
        public string? AwayWin { get; set; }
        public List<Totals>? Totals { get; set; }
        public BothTeamsScore? BTS { get; set; }
    }

    public record Totals {
        public string? Name { get; set; }
        public string? Over { get; set; }
        public string? Under { get; set; }
    }

    public record BothTeamsScore {
        public string? Yes {  get; set; }
        public string? No { get; set; }
    }
}
