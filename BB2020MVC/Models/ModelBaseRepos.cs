using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BB2020MVC.Models
{
    public class ModelBaseRepos : IBaseRepos
    {
        public BB2020Entities _DataContext = new BB2020Entities();
        public void BaseRepository()
        {
            _DataContext = new BB2020Entities();
        }
        public void SaveChanges()
        {
            _DataContext.SaveChanges();
        }

        public enum StatType
        {
            Other,
            AG,
            PA,
            AV
        }
        public Rules_Skills_Types GetSkillType(int ID)
        {
            return
                (from SkillType in _DataContext.Rules_Skills_Types
                 where SkillType.STypeID == ID
                 select SkillType).First();

        }
        public IList<Rules_LvlType> GetAllLevelTypes()
        {
            var AllLevelTypes =
                (from LevelTypes in _DataContext.Rules_LvlType
                 select LevelTypes).ToList();
            return AllLevelTypes;
        }
        public IList<RaceNames> GetRaceNames()
        {

            var AllRaces = GetAllRaces();
            var RaceQuery =
                from RaceData in AllRaces
                select new RaceNames
                {
                    ID = RaceData.RaceID,
                    Name = RaceData.Name
                };

            return RaceQuery.ToList();
        }
        public IList<Race> GetAllRaces()
        {
            var RaceQuery =
                (from RaceData in _DataContext.Races
                 select RaceData).ToList();
            return RaceQuery;
        }
        public IList<Rules_Skills_List> GetAllSkills()
        {
            return (from Skills in _DataContext.Rules_Skills_List
                    select Skills).ToList();
        }
        public Rules_Skills_List GetSkill(int ID)
        {
                
            return (from item in _DataContext.Rules_Skills_List
                    where item.SkillID == ID
                    select item).First();
        }
        public IList<Rules_Skills_Types> GetAllRulesSkillTypes()
        {                
            return (from SkillTypes in _DataContext.Rules_Skills_Types
                    select SkillTypes).ToList();
        }
        public IList<Rules_SpecialRules> GetAllSpecialRules()
        {
            return
                (from SpecialRule in _DataContext.Rules_SpecialRules
                 select SpecialRule).ToList();
        }
        public IList<Race> GetRacesbySpecialRule(int SRID)
        {
            return (from Races in _DataContext.Races
                    from RacesSQ in _DataContext.Races_SpecialRules
                    where RacesSQ.SRID == SRID
                    where Races.RaceID == RacesSQ.RaceID
                    select Races).ToList();

        }
        public IList<Races_SpecialRules> GetRacesSpecialRulesBySRID(int SRID)
        {
            return (from RSR in _DataContext.Races_SpecialRules
                    where RSR.SRID == SRID
                    select RSR).ToList();
        }
        public IList<Rules_InjuryTypes> GetAllInjuryTypes()
        {
            return (from Injury in _DataContext.Rules_InjuryTypes
                    select Injury).ToList();
        }
        public IList<Races_Players_SkillTypes> GetSingleSkillTypes(int PlayerID)
        {
            var SkillTypes = GetPlayerSkillTypes(PlayerID);
            var Result =
                (from ST in SkillTypes
                 where ST.Single
                 select ST).ToList();
            return Result;
        }
        public IList<Races_Players_SkillTypes> GetDoubleSkillTypes(int PlayerID)
        {
            var SkillTypes = GetPlayerSkillTypes(PlayerID);
            var Result =
                (from ST in SkillTypes
                 where ST.Single == false
                 select ST).ToList();
            return Result;
        }
        public IList<Races_Players_Skills> GetPlayerSkillsbyPlayerID(int PlayerID)
        {
            IList<Races_Players_Skills> AllSkills = GetAllPlayerSkills();
            IList<Races_Players_Skills> SkillList =
                (from Skills in AllSkills
                 where Skills.PlayerID == PlayerID
                 select Skills).ToList();
            return SkillList;
        }
        public IList<Races_Players_SkillTypes> GetAllPlayerSkillTypes()
        {
            var SkillTypesQuery =
                (from SkillTypes in _DataContext.Races_Players_SkillTypes
                 select SkillTypes).ToList();
            return SkillTypesQuery;
        }

        public IList<Races_Players_Skills> GetAllPlayerSkills()
        {
            IList<Races_Players_Skills> PSQuery =
                (from PSQ in _DataContext.Races_Players_Skills
                 select PSQ).ToList();
            return PSQuery;
        }
        public IList<Races_Players> GetAllRacePlayers()
        {
            IList<Races_Players> Result =
                (from PQ in _DataContext.Races_Players
                 select PQ).ToList();
            return Result;
        }
        public IList<Races_Players> GetPlayersByRaceID(int RaceID)
        {
            IList<Races_Players> AllPlayers = GetAllRacePlayers();
            IList<Races_Players> Players =
                (from PlayerDetail in AllPlayers
                 where PlayerDetail.RaceID == RaceID
                 select PlayerDetail).ToList();
            return Players;

        }
        public IList<Rules_Skills_List> GetSkillsByType(int TypeID)
        {
            return (from Skills in _DataContext.Rules_Skills_List
                    where Skills.SkillTypeID == TypeID
                    select Skills).ToList();
        }
        public Races_Players GetPlayerBase(int ID)
        {
            Races_Players SelectedPlayer =
                (from PlayerData in _DataContext.Races_Players
                 where PlayerData.PlayerID == ID
                 select PlayerData).First();
            return SelectedPlayer;
        }
        public Rules_LvlType GetLevelType(int ID)
        {
            var LevelTypeQuery =
                (from LevelType in _DataContext.Rules_LvlType
                 where LevelType.ID == ID
                 select LevelType).First();
            return LevelTypeQuery;
        }
        public IList<SelectListItem> CreateSelectListSkillTypes()
        {
            SelectListItem ThisSelectListItem;
            IList<SelectListItem> SelectListItems = new List<SelectListItem>();
            IList<Rules_Skills_Types> SkillTypes = GetAllRulesSkillTypes();
            foreach (var SkillType in SkillTypes)
            {
                ThisSelectListItem = new SelectListItem()
                {
                    Text = SkillType.Name,
                    Value = SkillType.STypeID.ToString()

                };
                SelectListItems.Add(ThisSelectListItem);
            }

            return SelectListItems;
        }
        public IList<SelectListItem> CreateSelectListSkills(int SelectedValue = -1)
        {
            SelectListItem NewSelectListItem;
            IList<SelectListItem> SelectListItems = new List<SelectListItem>();
            IList<Rules_Skills_List> Skills = GetAllSkills();
            foreach (var item in Skills)
            {
                NewSelectListItem = new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.SkillID.ToString(),
                    Selected = (item.SkillID == SelectedValue)
                };
                SelectListItems.Add(NewSelectListItem);
            }
            NewSelectListItem = new SelectListItem()
            {
                Text = "None",
                Value = "-1",
                Selected = (SelectedValue == -1)
            };
            SelectListItems.Add(NewSelectListItem);
            return SelectListItems;
        }
        public IList<SelectListItem> CreateSelectListLevelTypes(int SelectedValue = -1)
        {
            SelectListItem NewSelectListItem;
            IList<SelectListItem> SelectListItems = new List<SelectListItem>();
            IList<Rules_LvlType> Skills = GetAllLevelTypes();
            foreach (var item in Skills)
            {
                NewSelectListItem = new SelectListItem()
                {
                    Text = item.Description,
                    Value = item.ID.ToString(),
                    Selected = (item.ID == SelectedValue)
                };
                SelectListItems.Add(NewSelectListItem);
            }
            NewSelectListItem = new SelectListItem()
            {
                Text = "None",
                Value = "-1",
                Selected = (SelectedValue == -1)
            };
            SelectListItems.Add(NewSelectListItem);
            return SelectListItems;
        }

        public IList<Races_Players_SkillTypes> GetPlayerSkillTypes(int ID)
        {
            return (from item in _DataContext.Races_Players_SkillTypes
                    where item.PlayerID == ID
                    select item).ToList();
        }


        public Race GetRaceBase(int ID)
        {
            Race SelectedRace =
                (from RaceData in _DataContext.Races
                 where RaceData.RaceID == ID
                 select RaceData).First();
            return SelectedRace;
        }
        public IList<Races_SpecialRules> GetSpecialRulesByRaceID(int RaceID)
        {

            return (from SR in _DataContext.Races_SpecialRules
                    where SR.RaceID == RaceID
                    select SR).ToList();
        }
        public BaseRaceStruct GetRace(int ID)
        {
            var Race = GetRaceBase(ID);


            IList<BaseTeamStruct> Roster = new List<BaseTeamStruct>();
            foreach (var PL in GetPlayersByRaceID(ID))
            {
                Roster.Add(new BaseTeamStruct()
                {
                    PlayerSkills = GetPlayerSkillsbyPlayerID(PL.PlayerID),
                    SingleSkillTypes = (
                                        from STL in GetPlayerSkillTypes(PL.PlayerID)
                                        from ST in GetAllRulesSkillTypes()
                                        where STL.STypeID == ST.STypeID
                                        where STL.Single
                                        select new CustomSkillType
                                        {
                                            ID = STL.PSkillTypeID,
                                            SkillTypeID = ST.STypeID,
                                            SkillTypeName = ST.Name
                                        }).ToList(),

                    DoubleSkillTypes = (
                                        from STL in GetPlayerSkillTypes(PL.PlayerID)
                                        from ST in GetAllRulesSkillTypes()
                                        where STL.STypeID == ST.STypeID
                                        where !STL.Single
                                        select new CustomSkillType
                                        {
                                            ID = STL.PSkillTypeID,
                                            SkillTypeID = ST.STypeID,
                                            SkillTypeName = ST.Name
                                        }).ToList(),
                    MA = PL.MA,
                    ST = PL.ST,
                    AG = PL.AG,
                    AV = PL.AV,
                    PA = (int)PL.PA,
                    Cost = PL.Cost,
                    ID = PL.PlayerID,
                    Name = PL.Name,
                    RaceID = PL.RaceID,
                    MaxQTY = PL.MaxQTY

                }

                );
            };

            return new BaseRaceStruct()
            {
                ID = ID,
                Name = Race.Name,
                Apoth = Race.Apothecary,
                Team = Roster,
                Tier = Race.Tier,
                RerollCost = Race.RerollCost,
                SpecialRules = (from SR in GetAllSpecialRules()
                                from RSR in GetSpecialRulesByRaceID(ID)
                                where SR.SRID == RSR.SRID
                                select SR
                ).ToList(),

            };



        }
    }


}