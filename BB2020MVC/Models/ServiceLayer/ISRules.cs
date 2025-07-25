using BB2020MVC.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB2020MVC.Models.ServiceLayer
{
    //Interfaces
    public interface ISInjuryTypes : IDataLayer<Rules_InjuryTypes>
    { 
    }
    public interface ISLevelTypes : IDataLayer<Rules_LvlType> 
    {
    }
    public interface ISForbiddenSkills : IDataLayer<Rules_Skills_FSkills> 
    { 
    }
    public interface ISSkills : IDataLayer<Rules_Skills_List> {
        IList<Rules_Skills_List> All(int SkillTypeID = 0);
    }
    public interface ISSkillsTypes : IDataLayer<Rules_Skills_Types> {
    }
    public interface ISSpecialRules : IDataLayer<Rules_SpecialRules> {
    }

    //Classes
    public class InjuryTypes : DataLayer<Rules_InjuryTypes>, ISInjuryTypes
    {
        public InjuryTypes() => SetTable(DataContext().Rules_InjuryTypes);
    }
    public class LevelTypes : DataLayer<Rules_LvlType>, ISLevelTypes
    {
        public LevelTypes() => SetTable(DataContext().Rules_LvlType);
    }
    public class ForbiddenSkills : DataLayer<Rules_Skills_FSkills>, ISForbiddenSkills
    {
        public ForbiddenSkills() => SetTable(DataContext().Rules_Skills_FSkills);
    }
    public class Skills : DataLayer<Rules_Skills_List>, ISSkills
    {
        public Skills() => SetTable(DataContext().Rules_Skills_List);
        
        public IList<Rules_Skills_List> All(int SkillTypeID = 0)
        {
            return (from item in Read()
                    where(SkillTypeID == 0 || item.SkillTypeID == SkillTypeID )
                    select item).ToList();
        }
    }
    public class SkillTypes : DataLayer<Rules_Skills_Types>, ISSkillsTypes
    {
        public SkillTypes() => SetTable(DataContext().Rules_Skills_Types);
    }
    public class SpecialRules : DataLayer<Rules_SpecialRules>, ISSpecialRules
    {
        public SpecialRules() => SetTable(DataContext().Rules_SpecialRules);
    }
}
