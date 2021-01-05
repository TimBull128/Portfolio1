using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BB2020MVC.Models
{
    public class ModelBaseRepos : IBaseRepos
    {
        public enum StatType
        {
            PA,
            Addition,
            Other
        }
        private RaceSQLModelDataContext _DataContext = new RaceSQLModelDataContext();

        public void BaseRepository()
        {
            _DataContext = new RaceSQLModelDataContext();
        }
        public IList<RaceNames> GetRaceNames()
        {
            var RaceQuery =
                from RaceData in _DataContext.Races
                select new RaceNames
                {
                    ID = RaceData.ID,
                    Name = RaceData.Name
                };

            return RaceQuery.ToList();
        }
        public Rules_Skills_List GetSkill(int SkillID)
        {
            var SkillResult =
                (from Skills in _DataContext.Rules_Skills_Lists
                 where Skills.ID == SkillID
                select Skills).First();
            return SkillResult;
        }

        public IList<Rules_Skills_List> GetAllSkills()
        {
            IList<Rules_Skills_List> SkillsQuery =
                (from Skills in _DataContext.Rules_Skills_Lists
                select Skills).ToList();

            return SkillsQuery;
        }

        public IList<Rules_SpecialRule> GetAllSpecialRules()
        {
            var SpecialRulesQuery =
                from SpecialRule in _DataContext.Rules_SpecialRules
                select SpecialRule;
            return SpecialRulesQuery.ToList();
            

        }

        public IList<RosterTeamStruct> GetRostersByRace(int RaceID)
        {
            throw new NotImplementedException();
        }

        public IList<RosterTeamStruct> GetRostersBySpecialRule(int SPID)
        {
            throw new NotImplementedException();
        }

        public Rules_SpecialRule GetSpecialRule(int SRID)
        {
            var SelectedSR =
                (from SpecialRule in _DataContext.Rules_SpecialRules
                where SpecialRule.ID == SRID
                select SpecialRule).First();
            Rules_SpecialRule ResultRule = new Rules_SpecialRule
            {
                ID = SelectedSR.ID,
                Name = SelectedSR.Name,
                Description = SelectedSR.Description
            };
            return ResultRule;
        }

        public void SaveChanges()
        {

            _DataContext.SubmitChanges();

        }

        public BaseRaceStruct GetRace(int ID)
        {
            var SelectedRace =
                (from Races in _DataContext.Races
                 where Races.ID == ID
                 select Races).First();
            
           
            
            var PlayerRoster =
                from Player in _DataContext.Races_Players
                where Player.RaceID == ID
                select Player;

            IList<Rules_SpecialRule> SpecialRulesQuery =
               (from SpecialRules in _DataContext.Rules_SpecialRules
                where SpecialRules.ID == SelectedRace.SRID1 || SpecialRules.ID == SelectedRace.SRID2 || SpecialRules.ID == SelectedRace.SRID3
                select SpecialRules).ToList();

            IList<BaseTeamStruct> PlayersList = new List<BaseTeamStruct>();
            BaseTeamStruct ThisPlayer = new BaseTeamStruct();
            foreach(var PQ in PlayerRoster)
            {
                ThisPlayer = new BaseTeamStruct
                {
                    ID = PQ.ID,
                    RaceID = PQ.RaceID,
                    Name = PQ.Name,
                    RaceName = PQ.Name,
                    MA = PQ.MA,
                    ST = (int)PQ.STRENGTH,
                    AG = PQ.AG,
                    AV = PQ.AV,
                    PA = PQ.PA,
                    Cost = PQ.Cost,
                    PlayerSkills =
                        (from PlayerSkillsQuery in _DataContext.Races_Players_Skills
                         where PlayerSkillsQuery.ID == PQ.ID
                         select PlayerSkillsQuery).ToList()
                };
                PlayersList.Add(ThisPlayer);
            }
                

            BaseRaceStruct ThisRace = new BaseRaceStruct()
            {
                ID = SelectedRace.ID,
                Name = SelectedRace.Name,
                Apoth = SelectedRace.Apothecary,
                SpecialRules = SpecialRulesQuery,
                RerollCost = SelectedRace.RerollCost,
                Team = PlayersList
            };
            return ThisRace;
        }

        public IList<SelectListItem> SelectListMA(int MA)
        {

            return CreateStatSelectList(StatType.Other,1,9,MA);
        }

        public IList<SelectListItem> SelectListST(int ST)
        {
            return CreateStatSelectList(StatType.Other, 1, 8, ST);
        }

        public IList<SelectListItem> SelectListAG(int AG)
        {
            return CreateStatSelectList(StatType.Addition, 1, 6, AG);
            
        }

        public IList<SelectListItem> SelectListPA(int PA)
        {
            return CreateStatSelectList( StatType.PA, 1,7,PA);
        }

        public IList<SelectListItem> SelectListAV(int AV)
        {
            return CreateStatSelectList(StatType.Addition, 3,11, AV);
        }

        public int GetNewPlayerID()
        {
            int PlayerID = 0;
            IList<Races_Player> AllPlayers = GetAllPlayers();
            foreach (var Players in AllPlayers)
            {
                if (PlayerID == Players.ID)
                {
                    PlayerID++;
                }
                else
                {
                    break;
                }
            }
            return PlayerID;
        }
        public int GetNewSkillSelectID()
        {
            int ID = 0;
            IList<Races_Players_Skill> SkillSelectList =
                (from SkillSelect in _DataContext.Races_Players_Skills
                 select SkillSelect).ToList();

            foreach (var Skill in SkillSelectList)
            {
                if (Skill.ID == ID)
                {
                    ID++;
                }
                else
                {
                    break;
                }
            }
            return ID;
        }
        public int GetNewRaceID()
        {
            IList<RaceNames> RaceNames = GetRaceNames();
            int RaceID = 0;
            foreach(var Race in RaceNames)
            {
                if(Race.ID == RaceID)
                {
                    RaceID++;
                }
                else
                {
                    break;
                }
            }
            
            return RaceID;
            
        }
        public IList<Races_Player> GetAllPlayers()
        {
            IList<Races_Player> Result =
                (from PQ in _DataContext.Races_Players
                 select PQ).ToList();
            return Result;
        }
        public IList<Races_Player> GetPlayersByRaceID(int RaceID)
        {
            IList<Races_Player> Players =
                (from PlayerDetail in _DataContext.Races_Players
                 where PlayerDetail.RaceID == RaceID
                 select PlayerDetail).ToList();
            return Players;

        }
        public IList<Races_Players_Skill> GetPlayerSkillsByPlayerID(int PlayerID)
        {
            IList<Races_Players_Skill> SkillList =
                (from Skills in _DataContext.Races_Players_Skills
                 where Skills.PlayerID == PlayerID
                 select Skills).ToList();
            return SkillList;
        }

        public IList<SelectListItem> CreateStatSelectList(StatType Type, int min, int max, int SelectedValue)
        {
            SelectListItem ThisSelectListItem = new SelectListItem();
            IList<SelectListItem> SelectListItems = new List<SelectListItem>();

            for (int Counter = min; Counter <= max; Counter++)
            {

                switch (Type)
                {
                    case StatType.PA:
                        ThisSelectListItem = 
                            new SelectListItem() { 
                                Text = (Counter == 7 ? "-" : Counter.ToString()+ "+"), 
                                Value = (Counter.ToString()), 
                                Selected = (Counter == SelectedValue) 
                            };
                        break;
                    case StatType.Addition:
                        ThisSelectListItem = 
                            new SelectListItem() { 
                                Text = (Counter.ToString() + "+"), 
                                Value = (Counter.ToString()), 
                                Selected = (Counter == SelectedValue) 
                            };
                        break;
                    case StatType.Other:
                        ThisSelectListItem = 
                            new SelectListItem() { 
                                Text = (Counter.ToString()), 
                                Value = (Counter.ToString()), 
                                Selected = (Counter == SelectedValue) 
                            };
                        break;     
                }
                SelectListItems.Add(ThisSelectListItem);
            }
            if (Type == StatType.Other)
            {
                return SelectListItems;
            }
            else
            {
                SelectListItems.Reverse();
                return SelectListItems;
            }
        }
    }


}