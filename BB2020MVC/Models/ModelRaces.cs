using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BB2020MVC.Models
{
    public class ModelRaces : ModelBaseRepos, IRacesRepos
    {

        private RaceSQLModelDataContext _DataContext = new RaceSQLModelDataContext();
        public void RaceRepository()
        {
            _DataContext = new RaceSQLModelDataContext();
        }


        public Race GetNewRaceBase()
        {
            Race NewRace = new Race()
            {
                ID = GetNewRaceID(),
                
                Name = "",
                Apothecary = false,
                RerollCost = 0,
                SRID1 = 19,
                SRID2 = 19,
                SRID3 = 19,

            };
            return NewRace;
        }
        public Races_Player GetNewPlayerBase(int IRaceID)
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
                RaceID = IRaceID
            };
            return Player;
        }
        public Races_Players_Skill GetNewSkillBase(int ID)
        {
            Races_Players_Skill PlayerSkill = new Races_Players_Skill()
            {
                ID = GetNewSkillSelectID(),
                PlayerID = ID,
 
            };
            return PlayerSkill;
        }



        public IList<SelectListItem> GetValidSkills(int PlayerID)
        {
            var PlayerSkillList =
                (from SQ in _DataContext.Races_Players_Skills
                 where SQ.PlayerID == PlayerID
                 select SQ).ToList();

            var SkillList =
                (from SQ in _DataContext.Rules_Skills_Lists
                 from PQ in _DataContext.Races_Players_Skills
                 where PQ.ID == PlayerID
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

        public Race GetRaceBase(int ID)
        {
            Race SelectedRace =
                (from RaceData in _DataContext.Races
                 where RaceData.ID == ID
                 select RaceData).First();
            return SelectedRace;
        }
        public Races_Player GetPlayerBase(int PlayerID)
        {
            Races_Player SelectedPlayer =
                (from PlayerData in _DataContext.Races_Players
                 where PlayerData.ID == PlayerID
                 select PlayerData).First();
            return SelectedPlayer;
        }
        public Races_Players_Skill GetPlayerSkillBase(int SkillID)
        {
            var Skill = 
            (from SkillDetail in _DataContext.Races_Players_Skills
             where SkillDetail.ID == SkillID
             select SkillDetail).First();
            return Skill;
        }
        public BaseTeamStruct GetPlayer(int ID)
        {
            var SelectedPlayer =
                (from Player in _DataContext.Races_Players
                 where Player.ID == ID
                 select Player).First();
            var SkillsQuery =
                from SkillSelect in _DataContext.Races_Players_Skills
                from SkillItem in _DataContext.Rules_Skills_Lists
                
                where SkillSelect.PlayerID == SelectedPlayer.ID
                where SkillSelect.SkillID == SkillItem.ID
                select SkillSelect;

            var RaceQuery =
                (from RaceSelect in _DataContext.Races
                
                where RaceSelect.ID == SelectedPlayer.RaceID
                select RaceSelect).First();

            BaseTeamStruct ResultPlayer = new BaseTeamStruct()
            {
                ID = SelectedPlayer.ID,
                MA = SelectedPlayer.MA,
                PA = SelectedPlayer.PA,
                Name = SelectedPlayer.Name,
                AG = SelectedPlayer.AG,
                AV = SelectedPlayer.AV,
                PlayerSkills = SkillsQuery.ToList(),
                ST = (int)SelectedPlayer.STRENGTH,
                RaceID = RaceQuery.ID,
                RaceName = RaceQuery.Name
                
                
            };

            return ResultPlayer;
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

            var SkillQuery = GetPlayerSkillsByPlayerID(PlayerID);

            
            foreach(var Skill in SkillQuery)
            {
                this.DeletePlayerSkill(Skill.ID);
            }

            _DataContext.Races_Players.DeleteOnSubmit(PlayerQuery);
            _DataContext.SubmitChanges();
        }

        public Races_Players_Skill GetSkillBase(int ID)
        {
            var SkillSelect =
                (from Skill in _DataContext.Races_Players_Skills
                 where Skill.ID == ID
                 select Skill).First();
            return SkillSelect;
        }

        public IList<Races_Players_Skill> RaceGetPlayerSkillsByPlayerID(int PlayerID)
        {
            IList<Races_Players_Skill> PlayerSkills =
                (from PQ in _DataContext.Races_Players_Skills
                 where PQ.PlayerID == PlayerID
                 select PQ).ToList();
            return PlayerSkills;
        }
    }

}