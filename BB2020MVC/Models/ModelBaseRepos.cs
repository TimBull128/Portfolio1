using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BB2020MVC.Models
{
    public class ModelBaseRepos : IBaseRepos
    {
        public RaceSQLModelDataContext _DataContext = new RaceSQLModelDataContext();
        public void BaseRepository()
        {
            _DataContext = new RaceSQLModelDataContext();
        }
        public void SaveChanges()
        {
            _DataContext.SubmitChanges();
        }
        public enum StatType
        {
            PA,
            Addition,
            Other
        }

        public IList<Rules_LvlType> GetAllLevelTypes()
        {
            var AllLevelTypes =
                (from LevelTypes in _DataContext.Rules_LvlTypes
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
                    ID = RaceData.ID,
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
            IList<Rules_Skills_List> SkillsQuery =
                (from Skills in _DataContext.Rules_Skills_Lists
                 select Skills).ToList();

            return SkillsQuery;
        }
        public Rules_Skills_List GetSkill(int ID)
        {
            var SkillQuery =
                (from item in _DataContext.Rules_Skills_Lists
                 where item.ID == ID
                 select item).First();
            return SkillQuery;
        }
        public IList<Rules_Skills_Type> GetAllRulesSkillTypes()
        {
            var SkillTypeQuery =
                (from SkillTypes in _DataContext.Rules_Skills_Types
                 select SkillTypes).ToList();
            return SkillTypeQuery;
        }
        public IList<Rules_SpecialRule> GetAllSpecialRules()
        {
            var SpecialRulesQuery =
                (from SpecialRule in _DataContext.Rules_SpecialRules
                 select SpecialRule).ToList();
            return SpecialRulesQuery;


        }
    }


}