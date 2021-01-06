using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BB2020MVC.Models
{
    public class ModelRaces : ModelBaseRepos, IRacesRepos
    {

        public void RaceRepository()
        {
            _DataContext = new RaceSQLModelDataContext();
        }

        private int GetNewPlayerID()
        {
            int PlayerID = 0;
            IList<Races_Player> List = GetAllPlayers();
            foreach (var item in List)
            {
                if (PlayerID != item.ID)
                {
                    break;
                }
                PlayerID++;
            }
            return PlayerID;
        }
        private int GetNewSkillSelectID()
        {
            int ID = 0;
            IList<Races_Players_Skill> List =
                (from SkillSelect in _DataContext.Races_Players_Skills
                 select SkillSelect).ToList();

            foreach (var item in List)
            {
                if (ID != item.ID)
                {
                    break;
                }
                ID++;
            }
            return ID;
        }
        private int GetNewRaceID()
        {
            IList<RaceNames> List = GetRaceNames();
            int ID = 0;
            foreach (var item in List)
            {
                if (ID != item.ID)
                {
                    break;
                }
                ID++;
            }

            return ID;

        }
        private int GetNewSkillTypeID()
        {
            IList<Races_Players_SkillType> List = GetAllPlayerSkillTypes();
            int ID = 0;
            foreach (var item in List)
            {
                if (ID != item.ID)
                {
                    break;
                }
                ID++;
            }

            return ID;
        }

        public Race GetNewRaceBase()
        {
            Race NewRace = new Race()
            {
                ID = GetNewRaceID(),

                Name = "",
                Apothecary = false,
                RerollCost = 0,
                SRID1 = -1,
                SRID2 = -1,
                SRID3 = -1,

            };
            return NewRace;
        }
        public Races_Player GetNewPlayerBase(int RaceID)
        {
            Races_Player Player = new Races_Player()
            {
                ID = GetNewPlayerID(),
                Name = "",
                AG = 6,
                AV = 1,
                MA = 1,
                PA = 7,
                STRENGTH = 1,
                Cost = 0,
                RaceID = RaceID
            };
            return Player;
        }
        public Races_Players_Skill GetNewSkillBase(int PlayerID)
        {
            Races_Players_Skill PlayerSkill = new Races_Players_Skill()
            {
                ID = GetNewSkillSelectID(),
                PlayerID = PlayerID,

            };
            return PlayerSkill;
        }
        public Races_Players_SkillType GetNewPlayerSkillTypeBase(int PlayerID)
        {
            Races_Players_SkillType NewSkillType = new Races_Players_SkillType()
            {
                ID = GetNewSkillTypeID(),
                PlayerID = PlayerID,
                TypeID = -1

            };
            return NewSkillType;
        }

        public Race GetRaceBase(int ID)
        {
            Race SelectedRace =
                (from RaceData in _DataContext.Races
                 where RaceData.ID == ID
                 select RaceData).First();
            return SelectedRace;
        }
        public Races_Player GetPlayerBase(int ID)
        {
            Races_Player SelectedPlayer =
                (from PlayerData in _DataContext.Races_Players
                 where PlayerData.ID == ID
                 select PlayerData).First();
            return SelectedPlayer;
        }
        public Races_Players_Skill GetPlayerSkillBase(int ID)
        {
            Races_Players_Skill PlayerSkill =
                (from SkillData in _DataContext.Races_Players_Skills
                 where SkillData.ID == ID
                 select SkillData).First();
            return PlayerSkill;
        }
        public Races_Players_SkillType GetPlayerSkillTypeBase(int ID)
        {
            Races_Players_SkillType STResult =
                (from item in _DataContext.Races_Players_SkillTypes
                 where item.ID == ID
                 select item).First();
            return STResult;
        }
        
        public IList<Races_Player> GetAllPlayers()
        {
            IList<Races_Player> Result =
                (from PQ in _DataContext.Races_Players
                 select PQ).ToList();
            return Result;
        }
        public IList<Races_Players_Skill> GetAllPlayerSkills()
        {
            IList<Races_Players_Skill> PSQuery =
                (from PSQ in _DataContext.Races_Players_Skills
                 select PSQ).ToList();
            return PSQuery;
        }
        public IList<Races_Players_SkillType> GetAllPlayerSkillTypes()
        {
            var SkillTypesQuery =
                (from SkillTypes in _DataContext.Races_Players_SkillTypes
                 select SkillTypes).ToList();
            return SkillTypesQuery;
        }

        


        public void AddRace(Race NewRace)
        {
            _DataContext.Races.InsertOnSubmit(NewRace);
            _DataContext.SubmitChanges();

        }
        public void AddPlayer(Races_Player Player)
        {
            _DataContext.Races_Players.InsertOnSubmit(Player);
            _DataContext.SubmitChanges();
        }
        public void AddPlayerSkill(Races_Players_Skill Skill)
        {
            _DataContext.Races_Players_Skills.InsertOnSubmit(Skill);
            _DataContext.SubmitChanges();
        }
        public void AddPlayerSkillType(Races_Players_SkillType SkillType)
        {
            _DataContext.Races_Players_SkillTypes.InsertOnSubmit(SkillType);
            _DataContext.SubmitChanges();
        }

        public void EditRace(Race RaceEdit)
        {
            Race StoredRace =
                (from RaceData in _DataContext.Races
                 where RaceData.ID == RaceEdit.ID
                 select RaceData).First();

            StoredRace.Apothecary = RaceEdit.Apothecary;
            StoredRace.Name = RaceEdit.Name;
            StoredRace.RerollCost = RaceEdit.RerollCost;
            StoredRace.SRID1 = RaceEdit.SRID1;
            StoredRace.SRID2 = RaceEdit.SRID2;
            StoredRace.SRID3 = RaceEdit.SRID3;
            _DataContext.SubmitChanges();

        }
        public void EditPlayer(Races_Player Player)
        {
            Races_Player StoredPlayer =
                (from PlayerData in _DataContext.Races_Players
                 where PlayerData.ID == Player.ID
                 select PlayerData).First();

            StoredPlayer.Name = Player.Name;
            StoredPlayer.MA = Player.MA;
            StoredPlayer.PA = Player.PA;
            StoredPlayer.STRENGTH = Player.STRENGTH;
            StoredPlayer.Cost = Player.Cost;
            StoredPlayer.AG = Player.AG;
            StoredPlayer.AV = Player.AV;
            _DataContext.SubmitChanges();
        }
        public void EditPlayerSkillType(Races_Players_SkillType SkillType)
        {
            var SelectedPlayerSkillType = GetPlayerSkillTypeBase(SkillType.ID);
            SelectedPlayerSkillType.TypeID = SkillType.TypeID;
            SelectedPlayerSkillType.Single = SkillType.Single;
            _DataContext.SubmitChanges();
        }

        public void DelRace(int RaceID)
        {
            var RaceQuery =
                this.GetRaceBase(RaceID);

            var PlayerQuery = GetPlayersByRaceID(RaceID);

            foreach (var PQ in PlayerQuery)
            {
                this.DelPlayer(PQ.ID);
            };

            _DataContext.Races.DeleteOnSubmit(RaceQuery);
            _DataContext.SubmitChanges();
        }
        public void DeletePlayerSkill(int SkillID)
        {
            Races_Players_Skill SkillSelected = this.GetPlayerSkillBase(SkillID);

            _DataContext.Races_Players_Skills.DeleteOnSubmit(SkillSelected);
            _DataContext.SubmitChanges();
        }
        public void DelPlayer(int PlayerID)
        {
            var PlayerQuery = this.GetPlayerBase(PlayerID);

            var SkillQuery = GetPlayerSkillsbyPlayerID(PlayerID);


            foreach (var Skill in SkillQuery)
            {
                this.DeletePlayerSkill(Skill.ID);
            }

            _DataContext.Races_Players.DeleteOnSubmit(PlayerQuery);
            _DataContext.SubmitChanges();
        }
        public void DelPlayerSkillType(int SkillTypeID)
        {
            var SelectedSkillType = GetPlayerSkillTypeBase(SkillTypeID);
            _DataContext.Races_Players_SkillTypes.DeleteOnSubmit(SelectedSkillType);
        }

        public IList<SelectListItem> SelectListMA(int MA = -1)
        {

            return CreateStatSelectList(StatType.Other, 1, 9, MA);
        }
        public IList<SelectListItem> SelectListST(int ST = -1)
        {
            return CreateStatSelectList(StatType.Other, 1, 8, ST);
        }
        public IList<SelectListItem> SelectListAG(int AG = -1)
        {
            return CreateStatSelectList(StatType.Addition, 1, 6, AG);

        }
        public IList<SelectListItem> SelectListPA(int PA = -1)
        {
            return CreateStatSelectList(StatType.PA, 1, 7, PA);
        }
        public IList<SelectListItem> SelectListAV(int AV = -1)
        {
            return CreateStatSelectList(StatType.Addition, 3, 11, AV);
        }
        private IList<SelectListItem> CreateStatSelectList(StatType Type, int min, int max, int SelectedValue)
        {
            SelectListItem ThisSelectListItem = new SelectListItem();
            IList<SelectListItem> SelectListItems = new List<SelectListItem>();

            for (int Counter = min; Counter <= max; Counter++)
            {

                switch (Type)
                {
                    case StatType.PA:
                        ThisSelectListItem =
                            new SelectListItem()
                            {
                                Text = (Counter == 7 ? "-" : Counter.ToString() + "+"),
                                Value = ("-1"),
                                Selected = (SelectedValue == -1)
                            };
                        break;
                    case StatType.Addition:
                        ThisSelectListItem =
                            new SelectListItem()
                            {
                                Text = (Counter.ToString() + "+"),
                                Value = (Counter.ToString()),
                                Selected = (Counter == SelectedValue)
                            };
                        break;
                    case StatType.Other:
                        ThisSelectListItem =
                            new SelectListItem()
                            {
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
        public IList<SelectListItem> CreateSelectListPlayerSkills(int PlayerID)
        {
            IList<Races_Players_Skill> PlayerSkillsList = GetPlayerSkillsbyPlayerID(PlayerID);
            Rules_Skills_List SelectedSkill;
            IList<SelectListItem> ResultSelectList = new List<SelectListItem>();
            SelectListItem SelectItem;
            foreach(var item in PlayerSkillsList)
            {
                SelectedSkill = GetSkill((int)item.SkillID);
                SelectItem = new SelectListItem { Value = SelectedSkill.ID.ToString(), Text = SelectedSkill.Name };
                
            }
            SelectItem = new SelectListItem { Value = "-1", Text = "-" };
            ResultSelectList.Add(SelectItem);
            return ResultSelectList;

        }


        public int GetTopIDValue()
        {
            var Query =
                from RaceData in _DataContext.Races
                orderby RaceData.ID descending
                select RaceData.ID;

            return Query.First();


        }
        public int GetLowIDValue()
        {
            var Query =
                from RaceData in _DataContext.Races
                select RaceData.ID;
            return Query.First();
        }


        public IList<Races_Players_Skill> GetPlayerSkillsbyPlayerID(int PlayerID)
        {
            IList<Races_Players_Skill> AllSkills = GetAllPlayerSkills();
            IList<Races_Players_Skill> SkillList =
                (from Skills in AllSkills
                 where Skills.PlayerID == PlayerID
                 select Skills).ToList();
            return SkillList;
        }
        public IList<Races_Players_SkillType> GetPlayerSkillTypesbyPlayerID(int PlayerID)
        {
            IList<Races_Players_SkillType> AllSkillTypes = GetAllPlayerSkillTypes();
            IList<Races_Players_SkillType> STList =
                (from ST in AllSkillTypes
                 where ST.PlayerID == PlayerID
                 select ST).ToList();
            return STList;
        }
        public IList<Races_Player> GetPlayersByRaceID(int RaceID)
        {
            IList<Races_Player> AllPlayers = GetAllPlayers();
            IList<Races_Player> Players =
                (from PlayerDetail in AllPlayers
                 where PlayerDetail.RaceID == RaceID
                 select PlayerDetail).ToList();
            return Players;

        }
        public IList<Race> GetRacesbySpecialRule(int SRID)
        {
            var AllRaces = GetAllRaces();
            var RaceQuery =
                (from item in AllRaces
                 where item.SRID1 == SRID || item.SRID2 == SRID || item.SRID3 == SRID
                 select item).ToList();
            return RaceQuery;
        }
        public Race GetRacebyPlayerID(int PlayerID)
        {
            var SelectedPlayer = GetPlayerBase(PlayerID);
            var RaceResult = GetRaceBase(SelectedPlayer.RaceID);
            return RaceResult;
        }
        public IList<SelectListItem> GetValidSkills(int PlayerID)
        {
            var PlayerSkillList = GetPlayerSkillsbyPlayerID(PlayerID);
            var AllSkills = GetAllSkills();

            var SkillList =
                (from SQ in AllSkills
                 from PQ in PlayerSkillList
                 where SQ.ID != PQ.SkillID
                 select SQ).ToList();


            IList<SelectListItem> SkillSelect = new List<SelectListItem>();
            foreach(var SQ in SkillList)
            {
                SkillSelect.Add(
                        new SelectListItem() { Text = SQ.Name, Value = SQ.ID.ToString() }
                    );
            }

            
            //Finally Return the skill List
            return SkillSelect;
        }
        public BaseTeamStruct GetPlayer(int ID)
        {
            var PlayerQuery = GetPlayerBase(ID);
            var SkillsQuery = GetPlayerSkillsbyPlayerID(ID);
            var RaceQuery = GetRacebyPlayerID(ID);
            var SkillTypeQuery = GetPlayerSkillTypesbyPlayerID(ID);
            var AllRulesSkillTypes = GetAllRulesSkillTypes();

            var SingleSkillTypes =
                (
                    from item in SkillTypeQuery
                    from SkillType in AllRulesSkillTypes
                    where item.Single
                    where SkillType.ID == item.TypeID
                    select new CustomSkillType 
                    {
                        ID = item.ID,
                        SkillTypeName = SkillType.Name,
                        SkillTypeID = SkillType.ID
                        
                    }
                 ).ToList();
            var DoubleSkillTypes =
                (
                    from item in SkillTypeQuery
                    from SkillType in AllRulesSkillTypes
                    where !item.Single
                    where SkillType.ID == item.TypeID
                    select new CustomSkillType
                    {
                        ID = item.ID,
                        SkillTypeName = SkillType.Name,
                        SkillTypeID = SkillType.ID

                    }
                 ).ToList();

            
            BaseTeamStruct ResultPlayer = new BaseTeamStruct()
            {
                ID = PlayerQuery.ID,
                MA = PlayerQuery.MA,
                PA = PlayerQuery.PA,
                Name = PlayerQuery.Name,
                AG = PlayerQuery.AG,
                AV = PlayerQuery.AV,
                PlayerSkills = SkillsQuery.ToList(),
                ST = (int)PlayerQuery.STRENGTH,
                RaceID = RaceQuery.ID,
                RaceName = RaceQuery.Name,
                Cost = PlayerQuery.Cost,
                SingleSkillTypes = SingleSkillTypes,
                DoubleSkillTypes = DoubleSkillTypes
                
            };

            return ResultPlayer;
        }

 
    }

}