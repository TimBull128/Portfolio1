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
        // GET: Rosters
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AllRosters()
        {
            IList<RosterStruct> RosterList = _RosterRepository.GetAllRosters();
            return View(RosterList);
        }
        public ActionResult ViewRoster(int ID)
        {
            RosterStruct Roster = _RosterRepository.GetRoster(ID);
            return View(Roster);
        }
        public ActionResult RostersBySelectRace()
        {
            IList<RaceNames> RaceNames = _RosterRepository.GetRaceNames();

            return View(RaceNames);
        }
        [HttpPost]
        public ActionResult RostersBySelectRace(FormCollection FC)
        {
            int.TryParse(FC["RaceID"], out int RaceID);
            return RedirectToAction("ViewRostersByRace", new { RaceID = RaceID });
        }
        public ActionResult ViewRostersByRace(int RaceID)
        {
            IList<RosterStruct> RosterList = _RosterRepository.GetRostersByRace(RaceID);
            return View(RosterList);
        }
        public ActionResult ViewRosterBySR(int SRID)
        {
            IList<RosterStruct> RosterList = _RosterRepository.GetRostersBySpecialRule(SRID);
            return View(RosterList);
        }
        public ActionResult ViewRostersBySelectSR()
        {
            IList<Rules_SpecialRule> SRList = _RosterRepository.GetAllSpecialRules();
            return View(SRList);
        }
        [HttpPost]
        public ActionResult ViewRostersBySelectSR(FormCollection FC)
        {
            int.TryParse(FC["ID"], out int SRID);
            IList<RosterStruct> RosterList = _RosterRepository.GetRostersBySpecialRule(SRID);
            return View(RosterList);
        }
        public ActionResult AddRoster()
        {
            ViewBag.Races = _RosterRepository.GetAllRaces();
            return View();
        }
        [HttpPost]
        public ActionResult AddRoster(FormCollection FC)
        {
            User_Roster Roster = new User_Roster()
            {
                Name = FC["Name"],
                Apoths = false,
                InTourney = false,
            };

            
            if(int.TryParse(FC["RaceID"], out int RaceID))
            {
                Roster.RaceID = RaceID;
            }
            if(int.TryParse(FC["TV"], out int TV))
            {
                Roster.TV = TV;
            }
            if(int.TryParse(FC["Treasury"], out int Treasury))
            {
                Roster.Treasury = Treasury;
            }
            else
            {
                Roster.Treasury = 0;
            }
            if(int.TryParse(FC["RerollsQTY"], out int RerollsQTY))
            {
                Roster.RerollsQTY = RerollsQTY;
            }
            if(FC["Apoths"] != "false")
            {
                Roster.Apoths = true;
            }
            if(FC["InTourney"] != "false")
            {
                Roster.InTourney = true;
            }
            if(int.TryParse(FC["CheerLeadersQTY"], out int CheerLeaders))
            {
                Roster.CheerLeadersQTY = CheerLeaders;
            }
            else
            {
                Roster.CheerLeadersQTY = 0;
            }
            if(int.TryParse(FC["CoachesQTY"], out int Coaches))
            {
                Roster.CoachesQTY = Coaches;
            }
            else
            {
                Roster.CoachesQTY = 0;
            }
            _RosterRepository.AddRoster(Roster);
            return RedirectToAction("AllRosters");

        }
        public ActionResult DeleteRoster()
        {
            return View();

        }
        [HttpPost]
        public ActionResult DeleteRoster(FormCollection FC)
        {
            int.TryParse(FC["ID"], out int ID);
            _RosterRepository.DeleteRoster(ID);
            return RedirectToAction("AllRosters");
        }
        public ActionResult EditRoster(int ID)
        {
            User_Roster Roster = _RosterRepository.GetRosterBase(ID);
            ViewBag.Race = _RosterRepository.GetRace(Roster.RaceID);
           
            return View(Roster);
        }
        [HttpPost]
        public ActionResult EditRoster(FormCollection FC)
        {

            User_Roster Roster = new User_Roster()
            {
                Name = FC["Name"],
                Apoths = false,
                InTourney = false,
            };


            if (int.TryParse(FC["RaceID"], out int RaceID))
            {
                Roster.RaceID = RaceID;
            }
            if (int.TryParse(FC["TV"], out int TV))
            {
                Roster.TV = TV;
            }
            if (int.TryParse(FC["Treasury"], out int Treasury))
            {
                Roster.Treasury = Treasury;
            }
            else
            {
                Roster.Treasury = 0;
            }
            if (int.TryParse(FC["RerollsQTY"], out int RerollsQTY))
            {
                Roster.RerollsQTY = RerollsQTY;
            }
            if (FC["Apoths"] != "false")
            {
                Roster.Apoths = true;
            }
            if (FC["InTourney"] != "false")
            {
                Roster.InTourney = true;
            }
            if (int.TryParse(FC["CheerLeadersQTY"], out int CheerLeaders))
            {
                Roster.CheerLeadersQTY = CheerLeaders;
            }
            else
            {
                Roster.CheerLeadersQTY = 0;
            }
            if (int.TryParse(FC["CoachesQTY"], out int Coaches))
            {
                Roster.CoachesQTY = Coaches;
            }
            else
            {
                Roster.CoachesQTY = 0;
            }
            _RosterRepository.EditRoster(Roster);
            return RedirectToAction("AllRosters");

            
        }

    }
}