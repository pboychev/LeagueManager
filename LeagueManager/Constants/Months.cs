using System.Collections.Generic;
using LeagueManager.Models;

namespace LeagueManager.Constants
{
    public static class Months
    {
        public static List<Month> MonthList = new List<Month>()
        {
            new Month() { Value = 0, Start = 1, End = 38 },
            new Month() { Value = 1, Start = 1, End = 3 }, // August      Gameweek 1 Gameweek 3
            new Month() { Value = 2, Start = 4, End = 6 }, // September   Start=4,End=6},
            new Month() { Value = 3, Start = 7, End = 10 }, // October    Start=7,End=10},
            new Month() { Value = 4, Start = 11, End = 14 }, // November  Start=11,End=14}
            new Month() { Value = 5, Start = 15, End = 20 }, // December  Start=15,End=20}
            new Month() { Value = 6, Start = 21, End = 23 }, // January   Start=21,End=23}
            new Month() { Value = 7, Start = 24, End = 27 }, // February  Start=24,End=27}
            new Month() { Value = 8, Start = 28, End = 30 }, // March     Start=28,End=30}
            new Month() { Value = 9, Start = 31, End = 35 }, // April     Start=31,End=35}
            new Month() { Value = 10, Start = 36, End = 38 }, // May       Start=36,End=38}
        };
    }
}
