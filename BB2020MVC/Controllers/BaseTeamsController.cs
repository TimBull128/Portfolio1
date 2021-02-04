using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using BB2020MVC.Models;

namespace BB2020MVC.Controllers
{
    public class BaseTeamsController : Controller
    {



        /*
        -----------------------------------------------------------
        ------------------Base Functions, do not change!------------
        -----------------------------------------------------------
        */
        private readonly IRacesRepos _RaceRepository;
        //Determines the start of the program, any item requiring start of code is put into here
        //Also determines each of the Repositories
        public BaseTeamsController(IRacesRepos _Repository)
        {
            _RaceRepository = _Repository;


        }
        //sets the Model to the repository
        public BaseTeamsController() : this(new ModelRaces()) { }
        /*
        -----------------------------------------------------------
        -----------------------------Races-------------------------
        -----------------------------------------------------------
         */

        //Index - Views all Races
        public ActionResult Index()
        {

            IList<RaceNames> Result = _RaceRepository.GetRaceNames();
            return View(Result);
        }

        // Edit Race base stats
        public ActionResult EditRace(int ID)
        {
            Race Result = _RaceRepository.GetRaceBase(ID);
            ViewBag.SpecialRules = _RaceRepository.GetAllSpecialRules();
            return View(Result);
        }
        [HttpPost]
        public ActionResult EditRace(Race PostedRace)
        {
            if (ModelState.IsValid)
            {
                _RaceRepository.EditRace(PostedRace);
                return RedirectToAction("ViewRace", new { ID = PostedRace.RaceID });
            }
            else
            {
                ViewBag.SpecialRules = _RaceRepository.GetAllSpecialRules();
                return View(PostedRace);
            }
        }
        // Delete Race
        public ActionResult DeleteRace(int ID)
        {
            var SelectedRace = _RaceRepository.GetRaceBase(ID);
            return View(SelectedRace);
        }
        [HttpPost]
        public ActionResult DeleteRace(Race Race)
        {

            _RaceRepository.DeleteRace(Race.RaceID);
            return RedirectToAction("Index");
        }
        // Add new race
        public ActionResult AddRace()
        {
            Race NewRace = _RaceRepository.GetNewRaceBase();
            return View(NewRace);
        }
        [HttpPost]
        public ActionResult AddRace(Race PostedRace)
        {
            if (ModelState.IsValid)
            {

                _RaceRepository.AddRace(PostedRace);
                return RedirectToAction("ViewRace", new { ID = PostedRace.RaceID });
            }
            else
            {
                ViewBag.SpecialRules = _RaceRepository.GetAllSpecialRules();
                return View(PostedRace);
            }


        }
        //View Races
        public ActionResult ViewRace(int ID)
        {

            var Result = _RaceRepository.GetRace(ID);
            ViewBag.AllSkills = _RaceRepository.GetAllSkills();

            ViewBag.MaxLimit = false;
            ViewBag.MinLimit = false;

            if (ID >= _RaceRepository.GetTopIDValue())
            {
                ViewBag.MaxLimit = true;
            }
            if (ID <= _RaceRepository.GetLowIDValue())
            {
                ViewBag.MinLimit = true;
            }

            return View(Result);
        }

        public ActionResult NextRace(int ID)
        {
            int RaceID = ID;
            bool Selected = false;
            IList<RaceNames> RaceList = _RaceRepository.GetRaceNames();
            do
            {
                RaceID++;
                foreach (var Race in RaceList)
                {
                    if (Race.ID == RaceID)
                    {
                        Selected = true;
                        break;
                    }
                }

            } while (!Selected);
            return RedirectToAction("ViewRace", new { ID = RaceID });
        }

        public ActionResult PrevRace(int ID)
        {
            int RaceID = ID;
            bool Selected = false;
            IList<RaceNames> RaceList = _RaceRepository.GetRaceNames();
            do
            {
                RaceID--;

                foreach (var Race in RaceList)
                {
                    if (Race.ID == RaceID)
                    {
                        Selected = true;
                        break;
                    }
                }

            } while (!Selected);
            return RedirectToAction("ViewRace", new { ID = RaceID });
        }

        /*
        -------------------------------------------------
        ------------------Players------------------------
        -------------------------------------------------
        */
        //Add Players
        public ActionResult BAddPlayer(int ID)
        {
            Races_Players NewPlayer = _RaceRepository.GetNewPlayerBase(ID);


            ViewBag.MA = _RaceRepository.SelectListMA();
            ViewBag.ST = _RaceRepository.SelectListST();
            ViewBag.AG = _RaceRepository.SelectListAG();
            ViewBag.PA = _RaceRepository.SelectListPA();
            ViewBag.AV = _RaceRepository.SelectListAV();
            ViewBag.RaceName = _RaceRepository.GetRaceBase(ID).Name;
            return View(NewPlayer);

        }
        [HttpPost]
        public ActionResult BAddPlayer(Races_Players GatheredPlayer)
        {

            _RaceRepository.AddPlayer(GatheredPlayer);
            return RedirectToAction("ViewRace", new { ID = GatheredPlayer.RaceID });

        }
        //Delete Players
        public ActionResult BDeletePlayer(int ID)
        {

            var SelectedPlayer = _RaceRepository.GetPlayer(ID);
            ViewBag.Race = _RaceRepository.GetRaceBase(SelectedPlayer.RaceID);
            return View(SelectedPlayer);
        }
        [HttpPost]
        public ActionResult BDeletePlayer(Races_Players Player)
        {
            _RaceRepository.DelPlayer(Player.PlayerID);
            return RedirectToAction("ViewRace", new { Id = Player.RaceID });
        }

        //Edit Player stats
        public ActionResult BEditPlayer(int ID)
        {
            Races_Players SelectedPlayer = _RaceRepository.GetPlayerBase(ID);
            //Note: At runtime the view works properly (with preselect) with the these with MASelect ect - don't know why, it is not listed anywhere in any of the controllers
            //Also MASelect ect was a previous version - maybe storing a previous version? If View stops working try renaming these to MASelect ect
            ViewBag.MA = _RaceRepository.SelectListMA();
            ViewBag.ST = _RaceRepository.SelectListST();
            ViewBag.AG = _RaceRepository.SelectListAG();
            ViewBag.PA = _RaceRepository.SelectListPA();
            ViewBag.AV = _RaceRepository.SelectListAV();
            ViewBag.RaceName = _RaceRepository.GetRaceBase(SelectedPlayer.RaceID);

            return View(SelectedPlayer);
        }
        [HttpPost]
        public ActionResult BEditPlayer(Races_Players Player)
        {
            if (ModelState.IsValid)
            {
                _RaceRepository.EditPlayer(Player);
                return RedirectToAction("ViewRace", new { ID = Player.RaceID });
            }
            else
            {
                ViewBag.MA = _RaceRepository.SelectListMA();
                ViewBag.ST = _RaceRepository.SelectListST();
                ViewBag.AG = _RaceRepository.SelectListAG();
                ViewBag.PA = _RaceRepository.SelectListPA();
                ViewBag.AV = _RaceRepository.SelectListAV();
                ViewBag.RaceName = _RaceRepository.GetRaceBase(Player.RaceID).Name;
                ViewBag.Race = _RaceRepository.GetRaceBase(Player.RaceID);
                return View(Player);
            }


        }

        /*
        --------------------------------------------------
        ------------------Player Skills-------------------
        --------------------------------------------------
        */
        //PlayerSkills Splash
        public ActionResult SkillIndex(int ID)
        {
            var PlayerSkills = _RaceRepository.GetPlayerSkillsbyPlayerID(ID);
            var PlayerBase = _RaceRepository.GetPlayerBase(ID);
            var RulesSkills =
                (from SQ in _RaceRepository.GetAllSkills()
                 from PSQ in PlayerSkills
                 where SQ.SkillID == PSQ.SkillID
                 select SQ).ToList();

            SkillIndexVM Model = new SkillIndexVM
            {
                Player = PlayerBase,
                PSkillList = PlayerSkills,
                SkillList = RulesSkills,
                SkillTypes = _RaceRepository.GetAllRulesSkillTypes(),
                Race = _RaceRepository.GetRaceBase(PlayerBase.RaceID)

            };
            return View(Model);
        }
        //Add Skill
        public ActionResult BAddSkill(int ID)
        {
            Races_Players_Skills NewPlayerSkill = _RaceRepository.GetNewSkillBase(ID);

            ViewBag.Skills = _RaceRepository.SLSkills();
            ViewBag.Player = _RaceRepository.GetPlayer(ID);
            return View(NewPlayerSkill);
        }

        [HttpPost]
        public ActionResult BAddSkill(Races_Players_Skills PlayerSkill)
        {
            Races_Players Player = _RaceRepository.GetPlayerBase(PlayerSkill.PlayerID);

            if (ModelState.IsValid)
            {
                _RaceRepository.AddPlayerSkill(PlayerSkill);
                return RedirectToAction("SkillIndex", new { ID = Player.PlayerID });
            }
            else
            {

                ViewBag.Skills = _RaceRepository.SLSkills();
                ViewBag.Player = Player;


                return View(PlayerSkill);
            }

        }

        //Delete Skill
        public ActionResult BDeleteSkill(int ID)
        {
            Races_Players_Skills Skill = _RaceRepository.GetPlayerSkillBase(ID);
            Races_Players Player = _RaceRepository.GetPlayerBase(Skill.PlayerID);
            SkillAlterVM Model = new SkillAlterVM()
            {
                PlayerSkill = Skill,
                RuleSkill = _RaceRepository.GetSkill(Skill.SkillID),
                Player = Player,
                Race = _RaceRepository.GetRaceBase(Player.RaceID)
            };
            return View(Model);

        }
        [HttpPost]
        public ActionResult BDeleteSkill(SkillAlterVM Model)
        {
            _RaceRepository.DeletePlayerSkill(Model.PlayerSkill.PSkillID);
            return RedirectToAction("SkillIndex", new { ID = Model.Player.PlayerID });
        }

        /*
        ----------------------------------------------------
        -------------Special Rules--------------------------
        ----------------------------------------------------
        */



        public ActionResult BEditSpecialRules(int ID)
        {
            var SelectedSR = _RaceRepository.GetSpecialRulesByRaceID(ID);
            EditRaceSRVM Model = new EditRaceSRVM()
            {

                RSR1 = SelectedSR[0],
                RSR2 = SelectedSR[1],
                RSR3 = SelectedSR[2],
                Select = _RaceRepository.CreateSelectListSpecialRules(),
                Race = _RaceRepository.GetRaceBase(ID)

            };


            return View(Model);
        }
        //TODO: test
        [HttpPost]
        public ActionResult BEditSpecialRules(EditRaceSRVM SR)
        {
            _RaceRepository.EditRaceSpecialRule(SR.RSR1);
            _RaceRepository.EditRaceSpecialRule(SR.RSR2);
            _RaceRepository.EditRaceSpecialRule(SR.RSR3);

            return RedirectToAction("ViewRace", new { ID = SR.RSR1.RaceID });
        }

        /*
        ----------------------------------------------------
        -------------------SkillTypes-----------------------
        ----------------------------------------------------
        */
        //Initial splash page
        public ActionResult SkillTypesIndex(int ID)
        {
            ViewBag.Player = _RaceRepository.GetPlayer(ID);
            IList<Races_Players_SkillTypes> SkillTypes = _RaceRepository.GetPlayerSkillTypes(ID);
            ViewBag.SkillTypes = _RaceRepository.GetAllRulesSkillTypes();
            return View(SkillTypes);
        }

        //Delete Skill Type
        public ActionResult BDeleteSkillType(int ID)
        {
            var SkillType = _RaceRepository.GetPlayerSkillTypeBase(ID);
            ViewBag.Player = _RaceRepository.GetPlayerBase(SkillType.PlayerID);
            ViewBag.STName = _RaceRepository.GetSkillType(SkillType.STypeID);
            return View(SkillType);
        }
        [HttpPost]
        public ActionResult BDeleteSkillType(Races_Players_SkillTypes SkillType)
        {
            var PlayerID = SkillType.PlayerID;
            _RaceRepository.DelPlayerSkillType(SkillType.PSkillTypeID);
            return RedirectToAction("SkillTypesIndex", new { ID = PlayerID });
        }

        //Add Skill Type
        public ActionResult BAddSkillType(int ID)
        {
            Races_Players_SkillTypes NewSkillType = _RaceRepository.GetNewPlayerSkillTypeBase(ID);
            ViewBag.Player = _RaceRepository.GetPlayerBase(NewSkillType.PlayerID);
            ViewBag.SkillTypes = _RaceRepository.CreateSelectListSkillTypes();
            return View(NewSkillType);
        }
        [HttpPost]
        public ActionResult BAddSkillType(Races_Players_SkillTypes SkillType)
        {
            if (ModelState.IsValid)
            {
                _RaceRepository.AddPlayerSkillType(SkillType);
                return RedirectToAction("SkillTypesIndex", new { ID = SkillType.PlayerID });
            }
            else
            {
                return View(SkillType);
            }
        }

        /*
        -----------------------------------------------------
        ------------------Additional-------------------------
        -----------------------------------------------------
        */

        public ActionResult GetRostersByRace(int ID)
        {
            return RedirectToAction(actionName: "GetRostersByRace", controllerName: "Rosters", new { ID });
        }
    }
}




