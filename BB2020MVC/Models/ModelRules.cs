using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BB2020MVC.Models
{
    public class ModelRules : ModelBaseRepos, IRulesRepos
    {
        private RaceSQLModelDataContext _DataContext = new RaceSQLModelDataContext();


        public int GetNewRuleID()
        {
            int ID = 0;
            IList<Rules_Skills_List> SkillsList = GetAllSkills();
            foreach(var Skill in SkillsList)
            {
                if(ID == Skill.ID)
                {
                    ID++;
                }
                else { break; }
            }
            return ID;
        }
        public void RulesRepository()
        {
            _DataContext = new RaceSQLModelDataContext();
        }
        public void AddSkill(Rules_Skills_List Skill)
        {
            _DataContext.Rules_Skills_Lists.InsertOnSubmit(Skill);
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
        public void EditSkill (Rules_Skills_List Skill)
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
        public IList<SelectListItem> CreateSelectListType(int SelectedValue)
        {
            SelectListItem ThisSelectListItem;
            IList<SelectListItem> SelectListItems = new List<SelectListItem>();
            IList<Rules_Skills_Type> SkillTypes = GetAllSkillTypes();
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
                Value = "99",
                Selected = (SelectedValue > SkillTypes.Count - 1)
            };
            SelectListItems.Add(ThisSelectListItem);
            return SelectListItems;
        }
        
        public IList<Rules_Skills_Type> GetAllSkillTypes()
        {
            IList<Rules_Skills_Type> SkillsTypeList = new List<Rules_Skills_Type>();
            var SkillTypeQuery =
                from ST in _DataContext.Rules_Skills_Types
                select ST;
            foreach(var STQ in SkillTypeQuery)
            {
                SkillsTypeList.Add(STQ);
            }
            return SkillsTypeList;
        }
        public Rules_Skills_List GetSkillBase(int id)
        {
            var SkillQuery =
                (from Skills in _DataContext.Rules_Skills_Lists
                 where Skills.ID == id
                 select Skills).First();
            Rules_Skills_List Skill = new Rules_Skills_List()
            {
                ID = SkillQuery.ID,
                Name = SkillQuery.Name,
                SkillTypeID = SkillQuery.SkillTypeID
            };
            return Skill;
        }
       
        public void AddSpecialRule(Rules_SpecialRule SpecialRule)
        {
            _DataContext.Rules_SpecialRules.InsertOnSubmit(SpecialRule);
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
                if(RQ.SRID1 == ID)
                {
                    RQ.SRID1 = null;
                }
                if(RQ.SRID2 == ID)
                {
                    RQ.SRID2 = null;
                }
                if(RQ.SRID3 == ID)
                {
                    RQ.SRID3 = null;
                }
            }
            _DataContext.Rules_SpecialRules.DeleteOnSubmit(SelectedSR);
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
        public Rules_SpecialRule GetSpecialRuleBase(int id)
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

  
        public int GetNewSkillTypeID()
        {
            IList<Rules_Skills_Type> SkillTypeList = GetAllSkillTypes();
            int ID = 0;
            foreach(var SkillType in SkillTypeList)
            {
                if(SkillType.ID == ID)
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

        public void AddSkillType(Rules_Skills_Type Type)
        {
            _DataContext.Rules_Skills_Types.InsertOnSubmit(Type);
            _DataContext.SubmitChanges();
        }

        public void EditSkillType(Rules_Skills_Type Type)
        {
            Rules_Skills_Type SelectedSkill = GetSkillTypeByID(Type.ID);
            SelectedSkill.Name = Type.Name;
            _DataContext.SubmitChanges();
        }

        public void DeleteSkillType(int ID)
        {
            var SkillQuery =
                from Skill in _DataContext.Rules_Skills_Lists
                where Skill.SkillTypeID == ID
                select Skill;

            foreach(var Skill in SkillQuery)
            {
                Skill.SkillTypeID = 99;
            }

            Rules_Skills_Type SelectedSkillType = GetSkillTypeByID(ID);
            _DataContext.Rules_Skills_Types.DeleteOnSubmit(SelectedSkillType);
            _DataContext.SubmitChanges();
        }

        public Rules_Skills_Type GetSkillTypeByID(int ID)
        {
            Rules_Skills_Type SelectedSkillType =
                (from SkillType in _DataContext.Rules_Skills_Types
                 where SkillType.ID == ID
                 select SkillType).First();
            return SelectedSkillType;
        }

        public int GetNewSkillID()
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
    }
}