using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BB2020MVC.Models;

namespace BB2020MVC.Controllers
{
    public class RostersController : Controller
    {
        private readonly IRosterRepos _RosterRepository;
        public RostersController() : this(new ModelRosters()) { }
        public RostersController(IRosterRepos _Repository)
        {
            _RosterRepository = _Repository;

        }

        public ActionResult RostersIndex()
        {
            return View();
        }
        public ActionResult NewRoster()
        {
            NewRosterVM Model = new NewRosterVM()
            {
                RaceID = 0,
                SLRace = _RosterRepository.SLRace()
            };
            return View(Model);

        }
        [HttpPost]
        public ActionResult NewRoster(NewRosterVM Model)
        {
            var NewRoster = _RosterRepository.NewRoster(Model.RaceID);
            _RosterRepository.AddRoster(NewRoster);
            return RedirectToAction("ViewRoster", new { NewRoster.RosterID });

        }

        public ActionResult ViewRoster(int RosterID)
        {
            var Roster = _RosterRepository.GetRoster(RosterID);
            ViewRosterVM Model = new ViewRosterVM()
            {
                Roster = Roster,
                RosteredPlayers = 
            }
        }

    }
}