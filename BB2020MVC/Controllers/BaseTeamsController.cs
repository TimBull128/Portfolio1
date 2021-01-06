using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BB2020MVC.Models;

namespace BB2020MVC.Controllers
{
    public class BaseTeamsController : Controller
    {
        private readonly IRacesRepos _RaceRepository;

        public BaseTeamsController() : this(new ModelRaces()) { }


        public BaseTeamsController(IRacesRepos _Repository)
        {
            _RaceRepository = _Repository;
        }

        [HttpGet]
        public ActionResult Index()
        {

            IList<RaceNames> Result = _RaceRepository.GetRaceNames();
            return View(Result);
        }
 
        [HttpGet]
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
                return RedirectToAction("ViewRace", new { ID = PostedRace.ID });
            }
            else
            {
                ViewBag.SpecialRules = _RaceRepository.GetAllSpecialRules();
                return View(PostedRace);
            }
        }

        public ActionResult AddPlayer(int IRaceID)
        {
            Races_Player NewPlayer = new Races_Player()
            {
                ID = _RaceRepository.GetNewPlayerID(),
                Name = "",
                AG = 6,
                AV = 1,
                MA = 1,
                PA = 7,
                STRENGTH = 1,
                Cost = 0,
                RaceID = IRaceID,
                
            };


            ViewBag.MASelect = _RaceRepository.SelectListMA(NewPlayer.MA);
            ViewBag.STSelect = _RaceRepository.SelectListST((int)NewPlayer.STRENGTH);
            ViewBag.AGSelect = _RaceRepository.SelectListAG(NewPlayer.AG);
            ViewBag.PASelect = _RaceRepository.SelectListPA((int)NewPlayer.PA);
            ViewBag.AVSelect = _RaceRepository.SelectListAV(NewPlayer.AV);
            ViewBag.RaceName = _RaceRepository.GetRace(IRaceID).Name;
            return View(NewPlayer);
            
        }
        [HttpPost]
        public ActionResult AddPlayer(Races_Player GatheredPlayer)
        {

            _RaceRepository.AddPlayer(GatheredPlayer);
            return RedirectToAction("ViewRace", new { ID = GatheredPlayer.RaceID });

        }
        public ActionResult DeletePlayer(int ID)
        {
            var SelectedPlayer = _RaceRepository.GetPlayer(ID);
            return View(SelectedPlayer);
        }
        [HttpPost]
        public ActionResult DeletePlayer(FormCollection FC)
        {
            BaseTeamStruct Player = new BaseTeamStruct();
            if (int.TryParse(FC["id"], out int PlayerID))
            {
                Player = _RaceRepository.GetPlayer(PlayerID);

                _RaceRepository.DelPlayer(Player.ID);
                _RaceRepository.SaveChanges();
            }
            return RedirectToAction("ViewRace", new { Id = Player.RaceID });
        }
        public ActionResult DeleteRace(int ID)
        {
            var SelectedRace = _RaceRepository.GetRace(ID);
            return View(SelectedRace);
        }
        [HttpPost]
        public ActionResult DeleteRace(FormCollection FC)
        {

            if (int.TryParse(FC["id"], out int RaceID))
            {
                _RaceRepository.DelRace(RaceID);
                _RaceRepository.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult AddRace()
        {


            Race NewRace =  new Race()
                {
                    ID = _RaceRepository.GetNewRaceID(),

                    Name = "",
                    Apothecary = false,
                    RerollCost = 0,
                    SRID1 = 19,
                    SRID2 = 19,
                    SRID3 = 19,

                };
            
            ViewBag.SpecialRules = _RaceRepository.GetAllSpecialRules();
            return View(NewRace);
        }
        [HttpPost]
        public ActionResult AddRace(Race PostedRace)
        {
            //Check the modelstate - SRID is nullable
            if (ModelState.IsValid)
            {
                //if SRID is null, set to 19
                if(PostedRace.SRID1 == null)
                {
                    PostedRace.SRID1 = 19;
                }
                if(PostedRace.SRID2 == null)
                {
                    PostedRace.SRID2 = 19;
                }
                if(PostedRace.SRID3 == null)
                {
                    PostedRace.SRID3 = 19;
                }
                
                _RaceRepository.AddRace(PostedRace);
                _RaceRepository.SaveChanges();
                return RedirectToAction("ViewRace", new { ID = PostedRace.ID });
            }
            else
            {
                ViewBag.SpecialRules = _RaceRepository.GetAllSpecialRules();
                return View(PostedRace);
            }
            

        }




        public ActionResult ViewRace(int ID)
        {
            BaseRaceStruct Result = _RaceRepository.GetRace(ID);
            IList <Rules_Skills_List> SkillList= _RaceRepository.GetAllSkills();
            int MaxResult = _RaceRepository.GetTopIDValue();
            int MinResult = _RaceRepository.GetLowIDValue();
            ViewBag.MaxLimit = false;
            ViewBag.MinLimit = false;
            ViewBag.SkillsList = SkillList;

            if (ID >= MaxResult)
            {
                ViewBag.MaxLimit = true;
            }
            if (ID <= MinResult)
            {
                ViewBag.MinLimit = true;
            }

            return View(Result);
        }

        // TODO: Test
        public ActionResult NextRace(int ID)
        {
            int RaceID = ID;
            bool Selected = false;
            IList<RaceNames> RaceList = _RaceRepository.GetRaceNames();
            do
            {
                RaceID++;
                foreach(var Race in RaceList)
                {
                    if(Race.ID == RaceID)
                    {
                        Selected = true;
                        break;
                    }
                }

            } while (!Selected);
            return RedirectToAction("ViewRace", new { ID = RaceID });
        }
        //TODO: Test
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

        //TODO: Test after creating Rosters
        public ActionResult GetRostersByRace(int ID)
        {
            return RedirectToAction(actionName: "GetRostersByRace", controllerName: "GetRostersByRace", new { ID = ID });
        }

        //TODO: Test after adding skills to Rules DB
        //Done: initial Test - forgot to add skills 
        public ActionResult AddSkill(int ID)
        {
            Races_Players_Skill NewPlayerSkill = _RaceRepository.GetNewSkillBase(ID);

            ViewBag.Skills = _RaceRepository.GetValidSkills(ID);
            ViewBag.SkillNames = _RaceRepository.GetAllSkills();
            ViewBag.Player = _RaceRepository.GetPlayer(ID);
            return View(NewPlayerSkill);
        }
        
        [HttpPost]
        public ActionResult AddSkill(Races_Players_Skill PlayerSkill)
        {
            BaseTeamStruct Player = _RaceRepository.GetPlayer((int)PlayerSkill.PlayerID);
            if (ModelState.IsValid)
            {
                _RaceRepository.AddPlayerSkill(PlayerSkill);
                return RedirectToAction("ViewRace", new { ID = Player.RaceID });
            }
            else
            {
                 
                ViewBag.Skills = _RaceRepository.GetValidSkills((int)PlayerSkill.PlayerID);
                ViewBag.Player = Player;


                return View(PlayerSkill);
            }

        }

        public ActionResult SelectDeleteSkill(int ID)
        {
            IList<Races_Players_Skill> SkillList = _RaceRepository.GetPlayerSkillsbyPlayerID(ID);
            IList<Rules_Skills_List> SkillRules = new List<Rules_Skills_List>();
            
            foreach(var Skill in SkillList)
            {
                SkillRules.Add(_RaceRepository.GetSkill((int)Skill.SkillID));
            }
            ViewBag.SkillRules = SkillRules;
            return View(SkillList);
        }
        //TODO: Test after adding skills to player
        public ActionResult DeleteSkill(int ID)
        {
            Races_Players_Skill Skill = _RaceRepository.GetPlayerSkillBase(ID);
            Rules_Skills_List SkillName = _RaceRepository.GetSkill((int)Skill.SkillID);
            ViewBag.Player = _RaceRepository.GetPlayer((int)Skill.PlayerID);
            ViewBag.SkillName = SkillName.Name;
            return View(Skill);
        }
        [HttpPost]
        public ActionResult DeleteSkill(Races_Players_Skill GatheredSkill)
        {
            Races_Player Player = _RaceRepository.GetPlayerBase((int)GatheredSkill.PlayerID);
            _RaceRepository.DeletePlayerSkill(GatheredSkill.ID);
            return RedirectToAction("ViewRace", new { ID = Player.RaceID });
        }


        public ActionResult EditPlayer(int ID)
        {

            Races_Player SelectedPlayer = _RaceRepository.GetPlayerBase(ID);
            ViewBag.MASelect = _RaceRepository.SelectListMA(SelectedPlayer.MA);
            ViewBag.STSelect = _RaceRepository.SelectListST((int)SelectedPlayer.STRENGTH);
            ViewBag.AGSelect = _RaceRepository.SelectListAG(SelectedPlayer.AG);
            ViewBag.PASelect = _RaceRepository.SelectListPA((int)SelectedPlayer.PA);
            ViewBag.AVSelect = _RaceRepository.SelectListAV(SelectedPlayer.AV);
            ViewBag.Race = _RaceRepository.GetRaceBase(SelectedPlayer.RaceID);
            return View(SelectedPlayer);
        }
        [HttpPost]
        public ActionResult EditPlayer(Races_Player Player)
        {
            if (ModelState.IsValid)
            {
                _RaceRepository.EditPlayer(Player);
                return RedirectToAction("ViewRace", new { ID = Player.RaceID });
            }
            else
            {
                ViewBag.MASelect = _RaceRepository.SelectListMA(Player.MA);
                ViewBag.STSelect = _RaceRepository.SelectListST((int)Player.STRENGTH);
                ViewBag.AGSelect = _RaceRepository.SelectListAG(Player.AG);
                ViewBag.PASelect = _RaceRepository.SelectListPA((int)Player.PA);
                ViewBag.AVSelect = _RaceRepository.SelectListAV(Player.AV);
                ViewBag.RaceName = _RaceRepository.GetRaceBase(Player.RaceID).Name;
                ViewBag.Race = _RaceRepository.GetRaceBase(Player.RaceID);
                return View(Player);
            }


        }
    }
}




