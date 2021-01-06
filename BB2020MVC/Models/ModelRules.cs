using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BB2020MVC.Models
{
    public class ModelRules : ModelBaseRepos, IRulesRepos
    {

        public void RulesRepository()
        {
            _DataContext = new RaceSQLModelDataContext();
        }


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
            IList<Rules_Skills_Type> SkillTypeList = GetAllRulesSkillTypes();
            int ID = 0;
            foreach (var SkillType in SkillTypeList)
            {
                if (SkillType.ID == ID)
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
                if (Skills.ID == ID)
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

                IList<Rules_SpecialRule> SpecialRuleList = GetAllSpecialRules();
                int ID = 0;
                foreach (var SR in SpecialRuleList)
                {
                    if (SR.ID == ID)
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
                ID = GetNewSkillID(),
                Description = "",
                Name = "",
                NotOptional = false,
                SkillTypeID = -1
            };
            return NewSkillBase;
        }
        public Rules_Skills_Type GetNewSkillTypeBase()
        {
            Rules_Skills_Type NewSkillType = new Rules_Skills_Type()
            {
                ID = GetNewSkillTypeID(),
                Name = ""
            };
            return NewSkillType;
        }
        public Rules_SpecialRule GetNewSpecialRuleBase()
        {
            Rules_SpecialRule NewSpecialRule = new Rules_SpecialRule()
            {
                ID = GetNewSpecialRuleID(),
                Name = "",
                Description = ""
            };
            return NewSpecialRule;
        }




        public IList<Rules_Skills_List> GetSkillsByType(int TypeID)
        {
            var SkillQuery =
                (from Skill in _DataContext.Rules_Skills_Lists
                 where Skill.SkillTypeID == TypeID
                 select Skill).ToList();
            return SkillQuery;
        }

        public IList<SelectListItem> CreateSelectListSkillTypes(int SelectedValue = -1)
        {
            SelectListItem ThisSelectListItem;
            IList<SelectListItem> SelectListItems = new List<SelectListItem>();
            IList<Rules_Skills_Type> SkillTypes = GetAllRulesSkillTypes();
            foreach (var SkillType in SkillTypes)
            {
                ThisSelectListItem = new SelectListItem()
                {
                    Text = SkillType.Name,
                    Value = SkillType.ID.ToString(),
                    Selected = (SkillType.ID == SelectedValue)

                };
                SelectListItems.Add(ThisSelectListItem);
            }
            ThisSelectListItem = new SelectListItem()
            {
                Text = "None",
                Value = "-1",
                Selected = (SelectedValue == -1)
            };
            SelectListItems.Add(ThisSelectListItem);
            return SelectListItems;
        }
        public IList<SelectListItem> CreateSelectListSkills(int SelectedValue = -1)
        {
            SelectListItem NewSelectListItem;
            IList<SelectListItem> SelectListItems = new List<SelectListItem>();
            IList<Rules_Skills_List> Skills = GetAllSkills();
            foreach(var item in Skills)
            {
                NewSelectListItem = new SelectListItem()
                {
                    Text = item.Name,
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
        public IList<SelectListItem> CreateSelectListSpecialRules(int SelectedValue = -1)
        {
            SelectListItem NewSelectListItem;
            IList<SelectListItem> SelectListItems = new List<SelectListItem>();
            IList<Rules_SpecialRule> SpecialRules = GetAllSpecialRules();
            foreach (var item in SpecialRules)
            {
                NewSelectListItem = new SelectListItem()
                {
                    Text = item.Name,
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

        public void AddLevelType(Rules_LvlType LevelType)
        {
            _DataContext.Rules_LvlTypes.InsertOnSubmit(LevelType);
        }
        public void AddSkillType(Rules_Skills_Type Type)
        {
            _DataContext.Rules_Skills_Types.InsertOnSubmit(Type);
            _DataContext.SubmitChanges();
        }
        public void AddSkill(Rules_Skills_List Skill)
        {
            _DataContext.Rules_Skills_Lists.InsertOnSubmit(Skill);
            _DataContext.SubmitChanges();
        }
        public void AddSpecialRule(Rules_SpecialRule SpecialRule)
        {
            _DataContext.Rules_SpecialRules.InsertOnSubmit(SpecialRule);
            _DataContext.SubmitChanges();
        }

        public void DeleteLevelType(int ID)
        {
            Rules_LvlType SelectedLevelType = GetLevelType(ID);
            IList<Rules_Skills_List> SkillsList = GetSkillsByType(SelectedLevelType.ID);
            foreach(var Skill in SkillsList)
            {
                Skill.SkillTypeID = -1;
            }
            
            _DataContext.Rules_LvlTypes.DeleteOnSubmit(SelectedLevelType);
            _DataContext.SubmitChanges();
        }
        public void DeleteSkill(int ID)
        {
            var SelectedSkill =
                from Skill in _DataContext.Rules_Skills_Lists
                where Skill.ID == ID
                select Skill;


            var SelectedPlayerSkills =
                from Skill in _DataContext.Races_Players_Skills
                where Skill.SkillID == ID
                select Skill;
            _DataContext.Rules_Skills_Lists.DeleteAllOnSubmit(SelectedSkill);
            _DataContext.Races_Players_Skills.DeleteAllOnSubmit(SelectedPlayerSkills);
            _DataContext.SubmitChanges();
        }
        public void DeleteSpecialRule(int ID)
        {
            var SelectedSR =
                (from SpecialRule in _DataContext.Rules_SpecialRules
                 where SpecialRule.ID == ID
                 select SpecialRule).First();
            var Races =
                from RSR in _DataContext.Races
                where RSR.SRID1 == ID || RSR.SRID2 == ID || RSR.SRID3 == ID
                select RSR;
            foreach (var RQ in Races)
            {
                if (RQ.SRID1 == ID)
                {
                    RQ.SRID1 = null;
                }
                if (RQ.SRID2 == ID)
                {
                    RQ.SRID2 = null;
                }
                if (RQ.SRID3 == ID)
                {
                    RQ.SRID3 = null;
                }
            }
            _DataContext.Rules_SpecialRules.DeleteOnSubmit(SelectedSR);
            _DataContext.SubmitChanges();
        }
        public void DeleteSkillType(int ID)
        {
            var SkillQuery = GetSkillType(ID);

            _DataContext.Rules_Skills_Types.DeleteOnSubmit(SkillQuery);
        }

        public void EditSkill(Rules_Skills_List Skill)
        {
            Rules_Skills_List StoredSkill =
                (from SkillData in _DataContext.Rules_Skills_Lists
                 where SkillData.ID == Skill.ID
                 select SkillData).First();

            StoredSkill.Name = Skill.Name;
            StoredSkill.NotOptional = Skill.NotOptional;
            StoredSkill.Description = Skill.Description;
            _DataContext.SubmitChanges();
        }
        public void EditLevelType(Rules_LvlType LevelType)
        {
            Rules_LvlType EditedLevelType = GetLevelType(LevelType.ID);
            EditedLevelType.Cost = LevelType.Cost;
            EditedLevelType.Description = LevelType.Description;
            _DataContext.SubmitChanges();
        }
        public void EditSpecialRule(Rules_SpecialRule SpecialRule)
        {
            Rules_SpecialRule StoredSR =
                (from SRData in _DataContext.Rules_SpecialRules
                 where SRData.ID == SpecialRule.ID
                 select SRData).First();
            StoredSR.Name = SpecialRule.Name;
            StoredSR.Description = SpecialRule.Description;
            StoredSR.ID = SpecialRule.ID;
            _DataContext.SubmitChanges();
        }
        public void EditSkillType(Rules_Skills_Type Type)
        {
            Rules_Skills_Type SelectedSkill = GetSkillType(Type.ID);
            SelectedSkill.Name = Type.Name;
            _DataContext.SubmitChanges();
        }

        public Rules_LvlType GetLevelType(int ID)
        {
            var LevelTypeQuery =
                (from LevelType in _DataContext.Rules_LvlTypes
                 where LevelType.ID == ID
                 select LevelType).First();
            return LevelTypeQuery;
        }
        public Rules_SpecialRule GetSpecialRule(int id)
        {
            var SRQuery =
                (from SR in _DataContext.Rules_SpecialRules
                where SR.ID == id
                select SR).First();
            Rules_SpecialRule SpecialRule = new Rules_SpecialRule()
            {
                ID = SRQuery.ID,
                Name = SRQuery.Name,
                Description = SRQuery.Description
            };
            return SpecialRule;
        }
        public Rules_Skills_Type GetSkillType(int ID)
        {
            Rules_Skills_Type SelectedSkillType =
                (from SkillType in _DataContext.Rules_Skills_Types
                 where SkillType.ID == ID
                 select SkillType).First();
            return SelectedSkillType;
        }

        
    }
}