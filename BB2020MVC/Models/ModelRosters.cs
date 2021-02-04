using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BB2020MVC.Models
{
    public class ModelRosters : ModelBaseRepos, IRosterRepos
    {
        private int GetNewRosterID()
        {
            int i = 0;
            foreach(var item in GetAllRosters())
            {
                if(item.RosterID != i)
                {
                    break;
                    
                }
                i++;
            }
            return i;
        }
        private int GetNewRosterPlayerID()
        {
            var Players = GetAllRosteredPlayers();
            int i = 0;
            foreach(var item in Players)
            {
                if (item.RPlayerID != i)
                {
                    break;

                }
                i++;
            }
            return i;
        } 


        public User_Rosters NewRoster(int RaceID)
        {
            return new User_Rosters
            {
                RosterID = GetNewRosterID(),
                Apoths = false,
                CheerLeadersQTY = 0,
                RaceID = RaceID,
                RerollsQTY = 0,
                CoachesQTY = 0,
                InTourney = false,
                Name = "",
                Treasury = 0,
                TV = 0

            };
        }
        public User_Rosters_Players NewRosterPlayer(int RosterID, int RaceID)
        {
            var Player =
                (from item in GetPlayersByRaceID(RaceID)
                 where item.Name == "NA"
                 select item).First();

            return new User_Rosters_Players
            {
                RPlayerID = GetNewRosterPlayerID(),
                PlayerID = Player.PlayerID,
                Name = "",
                RosterID = RosterID,
                SPP = 0
            };
        }


        public IList<User_Rosters> GetAllRosters()
        {
            return (from Rosters in _DataContext.User_Rosters
                    select Rosters).ToList();
        }
        public IList<User_Rosters_Players> GetAllRosteredPlayers()
        {
            return (from item in _DataContext.User_Rosters_Players
                    select item).ToList();
        }


        public SelectList SLRace()
        {
            IList < SelectListItem > List = new List<SelectListItem>();
            SelectListItem Item;
            foreach(var item in GetAllRaces())
            {
                Item = new SelectListItem() { Text = item.Name, Value = item.RaceID.ToString() };
                List.Add(Item);
            }
            return new SelectList(List, "Value", "Text");
        }

        public void AddRoster(User_Rosters Roster)
        {
            _DataContext.User_Rosters.Add(Roster);
            this.SaveChanges();

            for (int i = 1; i <= 16; i++)
            {
                _DataContext.User_Rosters_Players.Add(this.NewRosterPlayer(Roster.RosterID, Roster.RaceID));
            }

        }

        public User_Rosters GetRoster(int RosterID)
        {
            throw new NotImplementedException();
        }
    }
}