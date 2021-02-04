using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BB2020MVC.Models
{
    public class ModelRules : ModelBaseRepos, IRulesRepos
    {

        private int GetNewLevelTypeID()
        {
            int ID = 0;
            IList<Rules_LvlType> LvlTypeList = GetAllLevelTypes();
            foreach (var Skill in LvlTypeList)
            {
                if (ID == Skill.ID)
                {
                    ID++;
                }
                else { break; }
            }
            return ID;
        }
        private int GetNewSkillTypeID()
        {
            IList<Rules_Skills_Types> SkillTypeList = GetAllRulesSkillTypes();
            int ID = 0;
            foreach (var SkillType in SkillTypeList)
            {
                if (SkillType.STypeID == ID)
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
        private int GetNewSkillID()
        {
            IList<Rules_Skills_List> SkillList = GetAllSkills();
            int ID = 0;
            foreach (var Skills in SkillList)
            {
                if (Skills.SkillID == ID)
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
        private int GetNewSpecialRuleID()
        {

                IList<Rules_SpecialRules> SpecialRuleList = GetAllSpecialRules();
                int ID = 0;
                foreach (var SR in SpecialRuleList)
                {
                    if (SR.SRID == ID)
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
        private int GetNewInjuryTypeID()
        {
            int ID = 0;
            foreach (var Injury in GetAllInjuryTypes())
            {
                if(Injury.InjuryID != ID)
                {
                    break;
                }
                ID++;
            }
            return ID;
        }
        private int GetNewFSkillID()
        {

            int i = 1;
            foreach(var item in GetAllFSkills())
            {
                if(i != item.ForbiddenID)
                {
                    break;
                }
                i++;
            }
            return i;
        }

        public Rules_LvlType GetNewLevelTypeBase()
        {
            Rules_LvlType NewLevelType = new Rules_LvlType()
            {
                ID = GetNewLevelTypeID(),
                Description = "",
                Cost = 0
            };
            return NewLevelType;
        }
        public Rules_Skills_List GetNewSkillBase()
        {
            Rules_Skills_List NewSkillBase = new Rules_Skills_List()
            { 
                SkillID = GetNewSkillID(),
                Description = "",
                Name = "",
                NotOptional = false,
                SkillTypeID = -1
            };
            return NewSkillBase;
        }
        public Rules_Skills_Types GetNewSkillTypeBase()
        {
            Rules_Skills_Types NewSkillType = new Rules_Skills_Types()
            {
                STypeID = GetNewSkillTypeID(),
                Name = "",
                Description = ""
            };
            return NewSkillType;
        }
        public Rules_SpecialRules GetNewSpecialRuleBase()
        {
            Rules_SpecialRules NewSpecialRule = new Rules_SpecialRules()
            {
                SRID = GetNewSpecialRuleID(),
                Name = "",
                Description = ""
            };
            return NewSpecialRule;
        }
        public Rules_InjuryTypes GetNewInjuryTypesBase()
        {
            return new Rules_InjuryTypes
            {
                InjuryID = GetNewInjuryTypeID(),
                Description = "",
                Name = ""
            };
        }
        public Rules_Skills_FSkills GetNewFSkillBase(int SkillID)
        {
            return new Rules_Skills_FSkills
            {
                ForbiddenID = GetNewFSkillID(),
                SkillID = SkillID,
                FSkillID = 0
            };
        }


        public Rules_Skills_FSkills GetFSkillBase(int FSkillID)
        {
            var AllFSkills = GetAllFSkills();
            return (from FSQ in GetAllFSkills()
                    where FSQ.ForbiddenID == FSkillID
                    select FSQ).First();
        }





        public IList<SelectListItem> CreateSelectListRaces(int SelectValue = -1)
        {
            IList<SelectListItem> RaceSelect = new List<SelectListItem>();
            foreach(var Race in this.GetAllRaces())
            {
                RaceSelect.Add(
                    new SelectListItem()
                    {
                        Value = Race.RaceID.ToString(),
                        Text = Race.Name,
                        Selected = (SelectValue == Race.RaceID)
                    }
                );
            }
            RaceSelect.Add
                (
                    new SelectListItem()
                    {
                        Value = "-1",
                        Text = "None",
                        Selected = (SelectValue == -1)
                    }
                );
            return RaceSelect;
        }

        public void AddLevelType(Rules_LvlType LevelType)
        {
            _DataContext.Rules_LvlType.Add(LevelType);
            this.SaveChanges();
        }
        public void AddSkillType(Rules_Skills_Types Type)
        {
            _DataContext.Rules_Skills_Types.Add(Type);
            this.SaveChanges();
        }
        public void AddSkill(Rules_Skills_List Skill)
        {
            _DataContext.Rules_Skills_List.Add(Skill);
            this.SaveChanges();
        }
        public void AddSpecialRule(Rules_SpecialRules SpecialRule)
        {
            _DataContext.Rules_SpecialRules.Add(SpecialRule);
            this.SaveChanges();
        }
        public void AddInjuryType(Rules_InjuryTypes InjuryType)
        {
            _DataContext.Rules_InjuryTypes.Add(InjuryType);
            this.SaveChanges();
        }
        public void AddForbiddenSkill(Rules_Skills_FSkills FSkill)
        {
            _DataContext.Rules_Skills_FSkills.Add(FSkill);
            _DataContext.SaveChanges();
        }
        

        public void DeleteLevelType(int ID)
        {
            Rules_LvlType SelectedLevelType = GetLevelType(ID);
            IList<Rules_Skills_List> SkillsList = GetSkillsByType(SelectedLevelType.ID);
            foreach(var Skill in SkillsList)
            {
                Skill.SkillTypeID = -1;
            }
            
            _DataContext.Rules_LvlType.Remove(SelectedLevelType);
            this.SaveChanges();
        }
        public void DeleteSkill(int ID)
        {
            var SelectedSkill =
                (from Skill in _DataContext.Rules_Skills_List
                where Skill.SkillID == ID
                select Skill).First();


            var SelectedPlayerSkills =
                from Skill in _DataContext.Races_Players_Skills
                where Skill.SkillID == ID
                select Skill;
            _DataContext.Rules_Skills_List.Remove(SelectedSkill);
            _DataContext.Races_Players_Skills.RemoveRange(SelectedPlayerSkills);
            this.SaveChanges();
        }
        public void DeleteSpecialRule(int ID)
        {

                _DataContext.Races_SpecialRules.RemoveRange(GetRacesSpecialRulesBySRID(ID));
                _DataContext.Rules_SpecialRules.Remove(GetSpecialRule(ID));
                this.SaveChanges();

        }
        public void DeleteSkillType(int ID)
        {
            var SkillQuery = GetSkillType(ID);

            _DataContext.Rules_Skills_Types.Remove(SkillQuery);
            this.SaveChanges();
        }
        public void DeleteInjuryType(int ID)
        {
            _DataContext.Rules_InjuryTypes.Remove(GetInjuryType(ID));
            this.SaveChanges();
        }
        public void DeleteForbiddenSkill(int ID)
        {
            Rules_Skills_FSkills SkillSelect =
                (from FSkill in _DataContext.Rules_Skills_FSkills
                 where FSkill.ForbiddenID == ID
                 select FSkill).First();
            _DataContext.Rules_Skills_FSkills.Remove(SkillSelect);
            this.SaveChanges();
        }
        
        public void EditSkill(Rules_Skills_List Skill)
        {
            Rules_Skills_List StoredSkill =
                (from SkillData in _DataContext.Rules_Skills_List
                 where SkillData.SkillID == Skill.SkillID
                 select SkillData).First();

            StoredSkill.Name = Skill.Name;
            StoredSkill.SkillTypeID = Skill.SkillTypeID;
            StoredSkill.NotOptional = Skill.NotOptional;
            StoredSkill.Description = Skill.Description;
            this.SaveChanges();
        }
        public void EditLevelType(Rules_LvlType LevelType)
        {
            Rules_LvlType EditedLevelType = GetLevelType(LevelType.ID);
            EditedLevelType.Cost = LevelType.Cost;
            EditedLevelType.Description = LevelType.Description;
            this.SaveChanges();
        }
        public void EditSpecialRule(Rules_SpecialRules SpecialRule)
        {
            Rules_SpecialRules StoredSR =
                (from SRData in _DataContext.Rules_SpecialRules
                 where SRData.SRID == SpecialRule.SRID
                 select SRData).First();
            StoredSR.Name = SpecialRule.Name;
            StoredSR.Description = SpecialRule.Description;
            StoredSR.SRID = SpecialRule.SRID;
            this.SaveChanges();
        }
        public void EditSkillType(Rules_Skills_Types Type)
        {
            Rules_Skills_Types SelectedSkill = GetSkillType(Type.STypeID);
            SelectedSkill.Name = Type.Name;
            this.SaveChanges();
        }


        public Rules_SpecialRules GetSpecialRule(int id)
        {
            return (from SR in _DataContext.Rules_SpecialRules
                    where SR.SRID == id
                    select SR).First(); 
        }
        // FSkillID = Forbidden Skill definition
        // SkillID = SkillID Skill definition
        // ForbiddenID = primary key
        public IList<Rules_Skills_FSkills> GetAllFSkills()
        {
            return (from item in _DataContext.Rules_Skills_FSkills
                    select item).ToList();
        } 
        public IList<Rules_Skills_FSkills> GetFSkillListforSkill(int ID)
        {
            var FSkillList = GetAllFSkills();
            return (from item in FSkillList
                    where item.ForbiddenID == ID
                    select item).ToList();
        }
        public Rules_InjuryTypes GetInjuryType(int ID)
        {
            return (from InjuryType in _DataContext.Rules_InjuryTypes
                    where InjuryType.InjuryID == ID
                    select InjuryType).First();
        }

        public SelectList CreateFSkillList(int SkillID)
        {

            var SkillList =
                (from FSQ in GetFSkillListforSkill(SkillID)
                 from SQ in GetAllSkills()
                 where SQ.SkillID != FSQ.SkillID
                 where SQ.SkillID != SkillID
                 select SQ).ToList();

            IList<SelectListItem> List = new List<SelectListItem>();
            SelectListItem SLItem = new SelectListItem();
            foreach(var item in SkillList)
            {
                SLItem = new SelectListItem() { Text = item.Name, Value = item.SkillID.ToString() };
                List.Add(SLItem);
            }
            return new SelectList(List);

        }
    }
}