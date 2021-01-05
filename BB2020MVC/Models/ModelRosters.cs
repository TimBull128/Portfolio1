using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BB2020MVC.Models
{
    public class ModelRosters : ModelBaseRepos, IRosterRepos
    {
        private RaceSQLModelDataContext _DataContext = new RaceSQLModelDataContext();

        public void AddPlayer(int PositonalID, string Name, int RosterID)
        {
            throw new NotImplementedException();
        }

        public void AddRoster(User_Roster Roster)
        {
            throw new NotImplementedException();
        }

        public void DeletePlayer(int ID)
        {
            throw new NotImplementedException();
        }

        public void DeleteRoster(int ID)
        {
            throw new NotImplementedException();
        }

        public void EditPlayer(User_Rosters_Positional Player)
        {
            throw new NotImplementedException();
        }

        public void EditRoster(User_Roster Roster)
        {
            throw new NotImplementedException();
        }

        public IList<BaseRaceStruct> GetAllRaces()
        {
            throw new NotImplementedException();
        }

        public IList<RosterStruct> GetAllRosters()
        {
            throw new NotImplementedException();
        }

        public RosterTeamStruct GetPlayer(int ID)
        {
            throw new NotImplementedException();
        }

        public RosterStruct GetRoster(int ID)
        {
            throw new NotImplementedException();
        }

        public User_Roster GetRosterBase(int ID)
        {
            throw new NotImplementedException();
        }

        public void RostersRepository()
        {
            _DataContext = new RaceSQLModelDataContext();
        }

        IList<RosterStruct> IRosterRepos.GetRostersByRace(int RaceID)
        {
            throw new NotImplementedException();
        }

        IList<RosterStruct> IRosterRepos.GetRostersBySpecialRule(int SPID)
        {
            throw new NotImplementedException();
        }
    }
}