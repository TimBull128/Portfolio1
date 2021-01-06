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
            IList<RosterNames> RosterNames = GetAllRosterNames();
            int ID = 0;
            foreach(var Roster in RosterNames)
            {
                if(Roster.ID != ID)
                {
                    break;
                }
                ID++;
            }
            return ID;
        }
        private int GetNewRosterPlayerID()
        {
            IList<User_Rosters_Positional> AllPlayers = GetAllRosterPlayers();
            int ID = 0;
            foreach (var item in AllPlayers)
            {
                if (item.ID != ID)
                {
                    break;
                }
                ID++;
            }
            return ID;
        }
        private int GetNewRosterSkillID()
        {
            IList<User_Rosters_Skill> AllSkills = GetAllRosterSkill();
            int ID = 0;
            foreach (var item in AllSkills)
            {
                if (item.ID != ID)
                {
                    break;
                }
                ID++;
            }
            return ID;
        }
        private int GetNewRosterLVlTypeID()
        {
            IList<User_Rosters_LvlType> AllLvlTypes = GetAllLvlTypes();
            int ID = 0;
            foreach (var item in AllLvlTypes)
            {
                if (item.ID != ID)
                {
                    break;
                }
                ID++;
            }
            return ID;
        }

        public User_Roster GetNewRosterBase()
        {
            User_Roster NewRoster = new User_Roster()
            {
                ID = GetNewRosterID(),
                Apoths = true,
                RaceID = 0,
                RerollsQTY = 0,
                CheerLeadersQTY = 0,
                CoachesQTY = 0,
                InTourney = false,
                Name = "",
                Treasury = 0,
                TV = 0
            };
            return NewRoster;
        }
        public User_Rosters_Skill GetNewRosterSkillBase(int PlayerID)
        {
            User_Rosters_Skill NewSkill = new User_Rosters_Skill()
            {
                ID = GetNewRosterSkillID(),
                PositionalID = PlayerID,
                SkillID = 99
            };
            return NewSkill;
        }
        public User_Rosters_Positional GetNewRosterPlayerBase(int RosterID)
        {
            User_Rosters_Positional NewPlayer = new User_Rosters_Positional()
            {
                ID = GetNewRosterPlayerID(),
                Name = "",
                PositionID = -1,
                RosterID = RosterID,
                SPP = 0
            };
            return NewPlayer; 
        }
        public User_Rosters_LvlType GetNewRosterLvlTypeBase(int PlayerID)
        {
            User_Rosters_LvlType NewLvlType = new User_Rosters_LvlType()
            {
                ID = GetNewRosterLVlTypeID(),
                PositionID = PlayerID,
                LvlTypeID = -1
            };
            return NewLvlType;
        }
        
        public User_Rosters_Skill GetRosterSkillBase(int ID)
        {
            var SkillQuery =
                (from Skill in _DataContext.User_Rosters_Skills
                 where Skill.ID == ID
                 select Skill).First();
            return SkillQuery;
        }
        public User_Roster GetRosterBase (int ID)
        {
            var RosterQuery =
                (from Roster in _DataContext.User_Rosters
                 where Roster.ID == ID
                 select Roster).First();
            return RosterQuery;
        }
        public User_Rosters_Positional GetPlayerBase(int PlayerID)
        {
            var PlayerQuery = (
                from Player in _DataContext.User_Rosters_Positionals
                where Player.PositionID == PlayerID
                select Player).First();
            return PlayerQuery;
        }
        public User_Rosters_LvlType GetLvlTypeBase(int ID)
        {

            if(ID == -1) {
                
            }
            var LvlType =
                (from Type in _DataContext.User_Rosters_LvlTypes
                 where Type.ID == ID
                 select Type).First();
            return LvlType;
        }

        public IList<User_Rosters_LvlType> GetLvlTypesByPlayerID(int PlayerID)
        {
            var Types =
                (from LVLType in _DataContext.User_Rosters_LvlTypes
                 where LVLType.PositionID == PlayerID
                 select LVLType).ToList();
            return Types;
        }
        public IList<User_Rosters_Positional> GetPlayersByRosterID(int RosterID)
        {
            var PlayerQuery =
                (from Player in _DataContext.User_Rosters_Positionals
                 where Player.RosterID == RosterID
                 select Player).ToList();
            return PlayerQuery;
        }
        public IList<User_Rosters_Skill> GetSkillsByPlayerID(int PlayerID)
        {
            var SkillQuery =
                (from Skill in _DataContext.User_Rosters_Skills
                 where Skill.PositionalID == PlayerID
                 select Skill).ToList();
            return SkillQuery;
        }
        public IList<User_Roster> GetRostersByRace(int RaceID)
        {
            var RaceQuery =
                (from Races in _DataContext.User_Rosters
                 where Races.RaceID == RaceID
                 select Races).ToList();
            return RaceQuery;
        }
        public IList<User_Roster> GetRostersBySpecialRule(int SRID)
        {
            var AllRaces = GetAllRaces();

            var RaceID =
                (from Race in AllRaces
                 where Race.SRID1 == SRID || Race.SRID2 == SRID || Race.SRID3 == SRID
                 select Race.ID
                ).ToList();
            IList<User_Roster> SelectedRosters = new List<User_Roster>();

            foreach (int ID in RaceID)
            {
                foreach(var item in GetRostersByRace(ID))
                {
                    SelectedRosters.Add(item);
                }
            }
            
            return SelectedRosters;
        }

        public IList<User_Roster> GetAllRosters()
        {
            IList<User_Roster> Rosters =
                (from Roster in _DataContext.User_Rosters
                 select Roster).ToList();
            return Rosters;
        }
        public IList<RosterNames> GetAllRosterNames()
        {
            IList<User_Roster> AllRosters = GetAllRosters();
            IList<RaceNames> AllRaceNames = GetRaceNames();
            IList<RosterNames> RosterNames = new List<RosterNames>();
            RosterNames RosterName = new RosterNames();
            foreach (var Rosters in AllRosters)
            {
                foreach (var RaceNames in AllRaceNames)
                {
                    RosterName.ID = Rosters.ID;
                    RosterName.Name = Rosters.Name;
                    RosterName.RaceID = Rosters.RaceID;
                    if (Rosters.RaceID == RaceNames.ID)
                    {
                        RosterName.RaceName = RaceNames.Name;
                    }

                    RosterNames.Add(RosterName);
                }
            }
            return RosterNames;
        }
        public IList<User_Rosters_Positional> GetAllRosterPlayers()
        {
            var PlayerList =
                (from Player in _DataContext.User_Rosters_Positionals
                 select Player).ToList();
            return PlayerList;
        }
        public IList<User_Rosters_LvlType> GetAllLvlTypes()
        {
            var LvlTypeList =
                (from LvlType in _DataContext.User_Rosters_LvlTypes
                select LvlType).ToList();
            return LvlTypeList;
        }
        public IList<User_Rosters_Skill> GetAllRosterSkill()
        {
            var SkillTypeList =
                (from Skill in _DataContext.User_Rosters_Skills
                 select Skill).ToList();
            return SkillTypeList;
        }


        public void AddRoster(User_Roster Roster)
        {
            _DataContext.User_Rosters.InsertOnSubmit(Roster);
            _DataContext.SubmitChanges();
        }
        public void EditRoster(User_Roster Roster)
        {
            User_Roster SelectedRoster = GetRosterBase(Roster.ID);
            SelectedRoster.Name = Roster.Name;
            SelectedRoster.InTourney = Roster.InTourney;
            SelectedRoster.RaceID = Roster.RaceID;
            SelectedRoster.RerollsQTY = Roster.RerollsQTY;
            SelectedRoster.Treasury = Roster.Treasury;
            SelectedRoster.TV = Roster.TV;
            SelectedRoster.Apoths = Roster.Apoths;
            SelectedRoster.CoachesQTY = Roster.CoachesQTY;
            SelectedRoster.CheerLeadersQTY = Roster.CheerLeadersQTY;
            _DataContext.SubmitChanges();
        }
        public void DeleteRoster(int ID)
        {
            User_Roster SelectedRoster = GetRosterBase(ID);
            IList<User_Rosters_Positional> PlayersList = GetPlayersByRosterID(SelectedRoster.ID);
            foreach(var Player in PlayersList)
            {
                DeleteRosterPlayer(Player.ID);
            }
            _DataContext.User_Rosters.DeleteOnSubmit(SelectedRoster);
            _DataContext.SubmitChanges();
        }

        public void AddRosterPlayer(User_Rosters_Positional Player)
        {
            _DataContext.User_Rosters_Positionals.InsertOnSubmit(Player);
            _DataContext.SubmitChanges();
        }
        public void EditRosterPlayer(User_Rosters_Positional Player)
        {
            User_Rosters_Positional PlayerBase = GetPlayerBase(Player.ID);
            PlayerBase.Name = Player.Name;
            PlayerBase.PositionID = Player.PositionID;
            PlayerBase.SPP = Player.SPP;
            _DataContext.SubmitChanges();
        }
        public void DeleteRosterPlayer(int ID)
        {
            var SelectedPlayer = GetPlayerBase(ID);
            var LvlTypes = GetLvlTypesByPlayerID(ID);
            var SkillList = GetSkillsByPlayerID(ID);
            foreach(var LVlTypes in LvlTypes)
            {
                DeleteLvlType(LVlTypes.ID);
            }
            foreach(var Skills in SkillList)
            {
                DeletePlayerSkill(Skills.ID);
            }
            _DataContext.User_Rosters_Positionals.DeleteOnSubmit(SelectedPlayer);
            _DataContext.SubmitChanges();
        }

        public void AddPlayerSkill(User_Rosters_Skill Skill)
        {
            _DataContext.User_Rosters_Skills.InsertOnSubmit(Skill);
            _DataContext.SubmitChanges();
        }
        public void DeletePlayerSkill(int ID)
        {
            User_Rosters_Skill SelectedSkill = GetRosterSkillBase(ID);
            _DataContext.User_Rosters_Skills.DeleteOnSubmit(SelectedSkill);
            _DataContext.SubmitChanges();
        }
        
        public void AddLvlType(User_Rosters_LvlType LvlType)
        {
            _DataContext.User_Rosters_LvlTypes.InsertOnSubmit(LvlType);
            _DataContext.SubmitChanges();
        }
        public void DeleteLvlType(int ID)
        {
            var SelectedLvlType = GetLvlTypeBase(ID);
            _DataContext.User_Rosters_LvlTypes.DeleteOnSubmit(SelectedLvlType);
            _DataContext.SubmitChanges();
        }

        public IList<SelectListItem> CreateValidSkillSelect(int PlayerID)
        {
            var PlayerSkillList =
                (from SQ in _DataContext.User_Rosters_Skills
                 where SQ.PositionalID == PlayerID
                 select SQ).ToList();

            var SkillList =
                (from SQ in _DataContext.Rules_Skills_Lists
                 from PQ in _DataContext.User_Rosters_Skills
                 where PQ.ID == PlayerID
                 where SQ.ID != PQ.SkillID
                 select SQ).ToList();


            IList<SelectListItem> SkillSelect = new List<SelectListItem>();
            foreach (var SQ in SkillList)
            {
                SkillSelect.Add(
                        new SelectListItem() { Text = SQ.Name, Value = SQ.ID.ToString() }
                    );
            }


            //Finally Return the skill List
            return SkillSelect;
        }
        public IList<SelectListItem> CreateLevelTypeSelect(int LevelSelectID = -1)
        {
            User_Rosters_LvlType LevelType;
            if (LevelSelectID != -1)
            {
                LevelType = GetLvlTypeBase(LevelSelectID);
            }
            else
            {
                LevelType = new User_Rosters_LvlType() { LvlTypeID = -1 };
            }
           
            IList<Rules_LvlType> AllLevelTypes = GetAllLevelTypes();
            IList<SelectListItem> LevelTypeSelect = new List<SelectListItem>();
            SelectListItem SelectItem;
            foreach(var LevelTypeQuery in AllLevelTypes)
            {
                SelectItem = new SelectListItem() { Selected = (LevelType.LvlTypeID == LevelTypeQuery.ID), Text = LevelTypeQuery.Description, Value = LevelTypeQuery.ID.ToString() };
                LevelTypeSelect.Add(SelectItem);
            }
            SelectItem = new SelectListItem() { Selected = (LevelType.LvlTypeID == -1), Text = "-", Value = "-1" };
            LevelTypeSelect.Add(SelectItem);
            return LevelTypeSelect;
            
        }
        public IList<SelectListItem> CreateSelectListRace(int SelectValue = -1)
        {
            IList<Race> AllRaces = GetAllRaces();
            SelectListItem NewSelectListItem;
            IList<SelectListItem> SelectListItems = new List<SelectListItem>();
            foreach (var RaceData in AllRaces)
            {
                NewSelectListItem = new SelectListItem()
                {
                    Value = RaceData.ID.ToString(),
                    Text = RaceData.Name,
                    Selected = (RaceData.ID == SelectValue)
                };
                SelectListItems.Add(NewSelectListItem);
            }
            NewSelectListItem = new SelectListItem()
            {
                Value = "-1",
                Text = "-",
                Selected = (SelectValue == -1)
            };
            SelectListItems.Add(NewSelectListItem);
            return SelectListItems;
        }


    }
}