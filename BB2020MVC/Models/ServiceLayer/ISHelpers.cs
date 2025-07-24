using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB2020MVC.Models.ServiceLayer
{
    internal interface ISHelpers
    {
    }

    public class Helpers : ISHelpers
    {
        private readonly ISRaces _RacesRepos;
        private readonly ISRaceSpecialRules _RaceSpecRepos;
        public Helpers() : this(new Races(), new RaceSpecialRules()) { }
        public Helpers(ISRaces raceRepos, ISRaceSpecialRules sRaceSpecRepos)
        {
            _RacesRepos = raceRepos;
            _RaceSpecRepos = sRaceSpecRepos;
        }


    }
}
