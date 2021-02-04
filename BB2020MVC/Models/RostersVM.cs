using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BB2020MVC.Models
{

    public class NewRosterVM
    {

        public int RaceID { get; set; } = 0;
        public SelectList SLRace;


    }

    public class ViewRosterVM
    {
        public User_Rosters Roster;
        public IList<User_Rosters_Players> RosteredPlayers;
        public BaseRaceStruct BaseRace;
    }

}