using BB2020MVC.Controllers;

using BB2020MVC.Models.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BB2020MVC.Models.BusinessLayer
{
    public interface IBusinessRosters
    {

    }
    public class BusinessRosters : IBusinessRosters
    {
        internal ISRosters _Rosters_repo;
        public BusinessRosters() : this(new ServiceLayer.Rosters) { }
        public BusinessRosters(ISRosters RosterRepos)
        {
            _Rosters_repo = RosterRepos;
        }
        TODO: Review and update
        public void AddRoster(User_Rosters Roster)
        {
            Roster = _Rosters_repo.Create(Roster);

            for (int i = 1; i <= 16; i++)
            {
                _DataContext.User_Rosters_Players.Add(this.NewRosterPlayer(Roster.RosterID, Roster.RaceID));
            }

        }
    }
}
