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
            _DataContext = new BB2020Entities();
        }

        private int GetNewPlayerID()
        {
            int PlayerID = 1;
            IList<Races_Players> List = GetAllRacePlayers();
            foreach (var item in List)
            {
                if (PlayerID != item.PlayerID)
                {
                    break;
                }
                PlayerID++;
            }
            return PlayerID;
        }
        private int GetNewSkillSelectID()
        {
            int ID = 1;
            IList<Races_Players_Skills> List =
                (from SkillSelect in _DataContext.Races_Players_Skills
                 select SkillSelect).ToList();

            foreach (var item in List)
            {
                if (ID != item.PSkillID)
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
            int ID = 1;
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
 
            int ID = 1;
            foreach (var item in GetAllPlayerSkillTypes())
            {
                if (ID != item.PSkillTypeID)
                {
                    break;
                }
                ID++;
            }

            return ID;
        }
        private int GetNewSRID()
        {
            var List = GetAllRacesSpecialRules();
            int ID = 1;
            foreach(var item in List)
            {
                if (ID != item.RSRID)
                {
                    break;
                }
                ID++;
            }
            return ID;
        }


        public Race GetNewRaceBase()
        {
            return new Race()
            {
                RaceID = GetNewRaceID(),

                Name = "",
                Apothecary = false,
                RerollCost = 0,

            }; ;
        }
        public Races_Players GetNewPlayerBase(int RaceID)
        {
            Races_Players Player = new Races_Players()
            {
                PlayerID = GetNewPlayerID(),
                Name = "",
                AG = 6,
                AV = 1,
                MA = 1,
                PA = 7,
                ST= 1,
                Cost = 0,
                RaceID = RaceID
            };
            return Player;
        }
        public Races_Players_Skills GetNewSkillBase(int PlayerID)
        {
            Races_Players_Skills PlayerSkill = new Races_Players_Skills()
            {
                PSkillID = GetNewSkillSelectID(),
                PlayerID = PlayerID,

            };
            return PlayerSkill;
        }
        public Races_Players_SkillTypes GetNewPlayerSkillTypeBase(int PlayerID)
        {
            return new Races_Players_SkillTypes
            {
                PSkillTypeID = GetNewSkillTypeID(),
                PlayerID = PlayerID,
                STypeID = 0,
                Single = false
            };
        }
        public Races_SpecialRules GetNewRaceSpecialRuleBase(int IRaceID)
        {
            return new Races_SpecialRules (){ 
                RSRID = GetNewSRID(), 
                RaceID = IRaceID, 
                SRID = 0 };
        }
        


        
        public Races_Players_Skills GetPlayerSkillBase(int ID)
        {
            Races_Players_Skills PlayerSkill =
                (from SkillData in GetAllPlayerSkills()
                 where SkillData.PSkillID == ID
                 select SkillData).First();
            return PlayerSkill;
        }
        public Races_Players_SkillTypes GetPlayerSkillTypeBase(int ID)
        {
            Races_Players_SkillTypes STResult =
                (from item in _DataContext.Races_Players_SkillTypes
                 where item.PSkillTypeID == ID
                 select item).First();
            return STResult;
        }
        public Races_SpecialRules GetSpecialRulesBase(int ID)
        {
            return (from SR in _DataContext.Races_SpecialRules
                    where SR.RSRID == ID
                    select SR).First();
        }



        private IList<Races_SpecialRules> GetAllRacesSpecialRules()
        {
            return (from SpecialRule in _DataContext.Races_SpecialRules
                    select SpecialRule).ToList();
        }
        public IList<Races_Players_Skills> GetPlayerSkillsByPlayerID(int PlayerID)
        {
            return (from item in _DataContext.Races_Players_Skills
                    where item.PlayerID == PlayerID
                    select item).ToList() ;
        }





        public void AddRace(Race NewRace)
        {
            //Add the Race to the Database
            _DataContext.Races.Add(NewRace);
            this.SaveChanges();
            //Add 3 Special Rules of Rule "None"
            for (int i = 1; i <= 3; i++)
            {
                _DataContext.Races_SpecialRules.Add(
                    new Races_SpecialRules() { RaceID = NewRace.RaceID, SRID = 0, RSRID = GetNewSRID() }
                    );
                this.SaveChanges();
            }
            //Save all changes
            this.SaveChanges();
        }
        public void AddPlayer(Races_Players Player)
        {
            _DataContext.Races_Players.Add(Player);
            this.SaveChanges();
        }
        public void AddPlayerSkill(Races_Players_Skills Skill)
        {
            _DataContext.Races_Players_Skills.Add(Skill);
            this.SaveChanges();
        }
        public void AddPlayerSkillType(Races_Players_SkillTypes SkillType)
        {
            _DataContext.Races_Players_SkillTypes.Add(SkillType);
            this.SaveChanges();
        }



        public void EditRace(Race RaceEdit)
        {
            Race StoredRace =
                (from RaceData in _DataContext.Races
                 where RaceData.RaceID == RaceEdit.RaceID
                 select RaceData).First();

            bool Changed = false;
            if(StoredRace.Apothecary != RaceEdit.Apothecary)
            {
                StoredRace.Apothecary = RaceEdit.Apothecary;
                Changed = true;
            }
            if(StoredRace.Name != RaceEdit.Name)
            {
                StoredRace.Name = RaceEdit.Name;
                Changed = true;
            }
            if(StoredRace.RerollCost != RaceEdit.RerollCost)
            {
                StoredRace.RerollCost = RaceEdit.RerollCost;
                Changed = true;
            }
            if (Changed) { this.SaveChanges(); }


        }
        public void EditPlayer(Races_Players Player)
        {
            bool Changed = false;
            Races_Players StoredPlayer =
                (from PlayerData in _DataContext.Races_Players
                 where PlayerData.PlayerID == Player.PlayerID
                 select PlayerData).First();
            if(StoredPlayer.Name != Player.Name)
            {
                StoredPlayer.Name = Player.Name;
                Changed = true;
            }
            if(StoredPlayer.MA != Player.MA)
            {
                StoredPlayer.MA = Player.MA;
                Changed = true;
            }
            if(StoredPlayer.PA != Player.PA)
            {
                StoredPlayer.PA = Player.PA;
                Changed = true;
            }
            if (StoredPlayer.ST != Player.ST)
            {
                StoredPlayer.ST = Player.ST;
                Changed = true;
            }
            if (StoredPlayer.Cost != Player.Cost)
            {
                StoredPlayer.Cost = Player.Cost;
                Changed = true;
            }
            if (StoredPlayer.AG != Player.AG)
            {
                StoredPlayer.AG = Player.AG;
                Changed = true;
            }

            if (StoredPlayer.AV != Player.AV)
            {
                StoredPlayer.AV = Player.AV;
                Changed = true;
            }
            if (Changed)
            {
                this.SaveChanges();
            }

        }
        public void EditPlayerSkillType(Races_Players_SkillTypes SkillType)
        {
            bool Changed = false;
            var SelectedPlayerSkillType = GetPlayerSkillTypeBase(SkillType.STypeID);
            if(SelectedPlayerSkillType.STypeID == SkillType.STypeID)
            {
                SelectedPlayerSkillType.STypeID = SkillType.STypeID;
                Changed = true;
            }
            
            if(SelectedPlayerSkillType.Single = SkillType.Single)
            {
                SelectedPlayerSkillType.Single = SkillType.Single;
                Changed = true;
            }
            if (Changed)
            {
                this.SaveChanges();
            }    

            return;
        }
        public void EditRaceSpecialRule(Races_SpecialRules SR)
        {
            int RSRID = SR.RSRID;
            Races_SpecialRules SRQ = (from SQ in _DataContext.Races_SpecialRules
                       where SQ.RSRID == RSRID
                       select SQ).First();
            if(SRQ.SRID != SR.SRID)
            {
                SRQ.SRID = SR.SRID;
                this.SaveChanges();
            };


        }


        public void DeleteRace(int RaceID)
        {

            

            foreach (var PQ in GetPlayersByRaceID(RaceID))
            {
                this.DelPlayer(PQ.PlayerID);
            };
            foreach(var SQ in GetSpecialRulesByRaceID(RaceID))
            {
                DelRaceSpecialRule(SQ.RSRID);
            }
            var RaceQuery = this.GetRaceBase(RaceID);
            _DataContext.Races.Remove(RaceQuery);
            this.SaveChanges();
        }
        public void DeletePlayerSkill(int PSkillID)
        {
            var SkillSelected = GetPlayerSkillBase(PSkillID);
            _DataContext.Races_Players_Skills.Remove(SkillSelected);
            this.SaveChanges();
        }
        public void DelPlayer(int PlayerID)
        {
            var Player = GetPlayerBase(PlayerID);
            var PlayerSkills = GetPlayerSkillsbyPlayerID(PlayerID);
            foreach(var item in PlayerSkills)
            {
                DeletePlayerSkill(item.PSkillID);
            }
            var PlayerSkillTypes = GetPlayerSkillTypes(PlayerID);
            foreach(var item in PlayerSkillTypes)
            {
                DelPlayerSkillType(item.PSkillTypeID);
            }
            _DataContext.Races_Players.Remove(Player);

        }
        public void DelPlayerSkillType(int SkillTypeID)
        {
            var SelectedSkillType = GetPlayerSkillTypeBase(SkillTypeID);
            _DataContext.Races_Players_SkillTypes.Remove(SelectedSkillType);
            this.SaveChanges();
        }
        public void DelRaceSpecialRule(int RSRID)
        {
            _DataContext.Races_SpecialRules.Remove(GetSpecialRulesBase(RSRID));
            this.SaveChanges();
                
        }
        public SelectList SelectListMA()
        {
            IList<SelectListItem> List = new List<SelectListItem>();
            SelectListItem Item;
            for(int i = 1; i <=9; i++)
            {
                Item = new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                };
                List.Add(Item);
            }
            
            return new SelectList(List, "Value", "Text");
        }


        public SelectList SelectListST()
        {
            IList<SelectListItem> List = new List<SelectListItem>();
            SelectListItem Item;
            for ( int i = 1; i<=8; i++)
            {
                Item = new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString(),

                };
                List.Add(Item);
            }
            return new SelectList(List, "Value", "Text");
        }
        public SelectList SelectListAG()
        {
            IList<SelectListItem> List = new List<SelectListItem>();
            SelectListItem Item;
            for (int i = 6; i>=1; i--)
            {
                Item = new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString() + "+",

                };
                List.Add(Item);
            }
            return new SelectList( List, "Value", "Text");

        }
        
        public SelectList SelectListPA()
        {
            IList<SelectListItem> List = new List<SelectListItem>();
            SelectListItem Item;
            for(int i = 7; i >= 1; i--)
            {
                Item = new SelectListItem
                {
                    Value = i.ToString(),
                    Text = (i==7)? "-":i.ToString() + "+"

                };
                List.Add(Item);
            }
            return new SelectList(List, "Value","Text");

        }
        public SelectList SelectListAV()
        {
            IList<SelectListItem> List = new List<SelectListItem>();
            SelectListItem Item;
            for(int i = 3; i<=11; i++)
            {
                Item = new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString() + "+"
                };
                List.Add(Item);
            }
            return new SelectList(List, "Value","Text");
        }
 
        public SelectList CreateSelectListPlayerSkills(int PlayerID)
        {
            IList<Races_Players_Skills> PlayerSkillsList = GetPlayerSkillsbyPlayerID(PlayerID);
            IList<SelectListItem> ResultSelectList = new List<SelectListItem>();

            foreach(var item in PlayerSkillsList)
            {
                Rules_Skills_List SelectedSkill = GetSkill((int)item.SkillID);
                ResultSelectList.Add(new SelectListItem { Value = SelectedSkill.SkillID.ToString(), Text = SelectedSkill.Name });
                
            }
            ResultSelectList.Add( new SelectListItem { Value = "-1", Text = "-" });

            return new SelectList(ResultSelectList, "value", "Text");

        }
        public SelectList CreateSelectListSpecialRules()
        {
            IList<SelectListItem> SelectList = new List<SelectListItem>();

            foreach(var item in GetAllSpecialRules())
            {
                SelectList.Add(new SelectListItem { Value = item.SRID.ToString(), Text = item.Name });
            }

            return new SelectList(SelectList, "Value","Text");
        }
        public SelectList SLSkills()
        {
            IList<SelectListItem> SelectList = new List<SelectListItem>();
            foreach(var Item in GetAllSkills())
            {
                SelectList.Add(new SelectListItem { Value = Item.SkillID.ToString(), Text = Item.Name.ToString() });
            }
            return new SelectList(SelectList, "Value", "Text");
        }

        public int GetTopIDValue()
        {
            var Query =
                from RaceData in _DataContext.Races
                orderby RaceData.RaceID descending
                select RaceData.RaceID;

            return Query.First();


        }
        public int GetLowIDValue()
        {
            var Query =
                from RaceData in _DataContext.Races
                select RaceData.RaceID;
            return Query.First();
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
                 where SQ.SkillID != PQ.SkillID
                 select SQ).ToList();


            IList<SelectListItem> SkillSelect = new List<SelectListItem>();
            foreach(var SQ in SkillList)
            {
                SkillSelect.Add(
                        new SelectListItem() { Text = SQ.Name, Value = SQ.SkillID.ToString() }
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
            var SkillTypeQuery = GetPlayerSkillTypes(ID);
            var AllRulesSkillTypes = GetAllRulesSkillTypes();

            var SingleSkillTypes =
                (
                    from item in SkillTypeQuery
                    from SkillType in AllRulesSkillTypes
                    where item.Single
                    where SkillType.STypeID == item.STypeID
                    select new CustomSkillType 
                    {
                        ID = item.STypeID,
                        SkillTypeName = SkillType.Name,
                        SkillTypeID = SkillType.STypeID
                        
                    }
                 ).ToList();
            var DoubleSkillTypes =
                (
                    from item in SkillTypeQuery
                    from SkillType in AllRulesSkillTypes
                    where !item.Single
                    where SkillType.STypeID == item.STypeID
                    select new CustomSkillType
                    {
                        ID = item.STypeID,
                        SkillTypeName = SkillType.Name,
                        SkillTypeID = SkillType.STypeID

                    }
                 ).ToList();

            
            BaseTeamStruct ResultPlayer = new BaseTeamStruct()
            {
                ID = PlayerQuery.PlayerID,
                MA = PlayerQuery.MA,
                PA = (int)PlayerQuery.PA,
                Name = PlayerQuery.Name,
                AG = PlayerQuery.AG,
                AV = PlayerQuery.AV,
                PlayerSkills = SkillsQuery.ToList(),
                ST = (int)PlayerQuery.ST,
                RaceID = RaceQuery.RaceID,
                Cost = PlayerQuery.Cost,
                SingleSkillTypes = SingleSkillTypes,
                DoubleSkillTypes = DoubleSkillTypes
                
            };

            return ResultPlayer;
        }



     }

}