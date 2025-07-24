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
    public interface ISInjuryTypes : IServiceLayer_Base<Rules_InjuryTypes>
    { 
    }
    public interface ISLevelTypes : IServiceLayer_Base<Rules_LvlType> 
    {
    }
    public interface ISForbiddenSkills : IServiceLayer_Base<Rules_Skills_FSkills> 
    { 
    }
    public interface ISSkills : IServiceLayer_Base<Rules_Skills_List> {
        IList<Rules_Skills_List> All(int SkillTypeID = 0);
    }
    public interface ISSkillsTypes : IServiceLayer_Base<Rules_Skills_Types> {
    }
    public interface ISSpecialRules : IServiceLayer_Base<Rules_SpecialRules> {
    }

    //Classes
    public class InjuryTypes : ServiceLayer_Base<Rules_InjuryTypes>, ISInjuryTypes
    {
        public InjuryTypes() => SetTable(DataContext().Rules_InjuryTypes);
    }
    public class LevelTypes : ServiceLayer_Base<Rules_LvlType>, ISLevelTypes
    {
        public LevelTypes() => SetTable(DataContext().Rules_LvlType);
    }
    public class ForbiddenSkills : ServiceLayer_Base<Rules_Skills_FSkills>, ISForbiddenSkills
    {
        public ForbiddenSkills() => SetTable(DataContext().Rules_Skills_FSkills);
    }
    public class Skills : ServiceLayer_Base<Rules_Skills_List>, ISSkills
    {
        public Skills() => SetTable(DataContext().Rules_Skills_List);
        
        public IList<Rules_Skills_List> All(int SkillTypeID = 0)
        {
            return (from item in Read()
                    where(SkillTypeID == 0 || item.SkillTypeID == SkillTypeID )
                    select item).ToList();
        }
    }
    public class SkillTypes : ServiceLayer_Base<Rules_Skills_Types>, ISSkillsTypes
    {
        public SkillTypes() => SetTable(DataContext().Rules_Skills_Types);
    }
    public class SpecialRules : ServiceLayer_Base<Rules_SpecialRules>, ISSpecialRules
    {
        public SpecialRules() => SetTable(DataContext().Rules_SpecialRules);
    }
}
