using System.Collections.Generic;
using LeagueManager.Models;

namespace LeagueManager.Constants
{
    public static class Months
    {
        public static readonly List<Month> LeagueMonths = new ()
        {
            new () { Value = 0, Start = 1, End = 38 },
            new () { Value = 1, Start = 1, End = 3 },
            new () { Value = 2, Start = 4, End = 6 },
            new () { Value = 3, Start = 7, End = 10 },
            new () { Value = 4, Start = 11, End = 14 },
            new () { Value = 5, Start = 15, End = 20 },
            new () { Value = 6, Start = 21, End = 23 },
            new () { Value = 7, Start = 24, End = 27 },
            new () { Value = 8, Start = 28, End = 30 },
            new () { Value = 9, Start = 31, End = 35 },
            new () { Value = 10, Start = 36, End = 38 }
        };
    }
}