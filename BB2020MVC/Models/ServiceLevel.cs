using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BB2020MVC.Models
{
    public interface IBaseRepos
    {

        IList<SelectListItem> SelectListMA( int MA);
        IList<SelectListItem> SelectListST( int ST);
        IList<SelectListItem> SelectListAG( int AG);
        IList<SelectListItem> SelectListPA( int PA);
        IList<SelectListItem> SelectListAV( int AV);
        void SaveChanges();
        IList<Rules_SpecialRule> GetAllSpecialRules();
        IList<Rules_Skills_List> GetAllSkills();
        Rules_SpecialRule GetSpecialRule(int ID);
        Rules_Skills_List GetSkill(int ID);
        IList<RaceNames> GetRaceNames();
        BaseRaceStruct GetRace(int ID);
        int GetNewPlayerID();
        int GetNewRaceID();
        
    }
    public interface IRacesRepos : IBaseRepos
    {

        Race GetNewRaceBase();
        Races_Player GetNewPlayerBase(int ID);
        Races_Players_Skill GetNewSkillBase(int ID);
        IList<Races_Players_Skill> RaceGetPlayerSkillsByPlayerID(int PlayerID);

        IList<SelectListItem> GetValidSkills(int PlayerID);


        Races_Players_Skill GetSkillBase(int ID); 
        int GetTopIDValue();
        int GetLowIDValue();
        Race GetRaceBase(int ID);
        void AddRace(Race NewRace);
        void EditRace(Race EditRace);
        void DelRace(int RaceID);
        BaseTeamStruct GetPlayer(int ID);
        Races_Players_Skill GetPlayerSkillBase(int ID);
        Races_Player GetPlayerBase(int ID);
        void AddPlayer(Races_Player Player);
        void DelPlayer(int ID);
        void AddPlayerSkill(Races_Players_Skill Skill);
        void DeletePlayerSkill(int Skill);
        void EditPlayer(Races_Player Player);

    }
    public interface IRulesRepos : IBaseRepos
    {
        int GetNewRuleID();
        void AddSkill(Rules_Skills_List Skill);
        void DeleteSkill(int ID);
        void EditSkill(Rules_Skills_List Skill);
        Rules_Skills_List GetSkillBase(int ID);
        Rules_SpecialRule GetSpecialRuleBase(int ID);

        
        void AddSpecialRule(Rules_SpecialRule SpecialRule);
        void DeleteSpecialRule(int ID);
        void EditSpecialRule(Rules_SpecialRule SpecialRule);
        IList<Rules_Skills_Type> GetAllSkillTypes();
        IList<SelectListItem> CreateSelectListType(int TypeID);
        int GetNewSkillTypeID();
        int GetNewSkillID();
        void AddSkillType(Rules_Skills_Type Type);
        void EditSkillType(Rules_Skills_Type Type);
        void DeleteSkillType(int ID);
        Rules_Skills_Type GetSkillTypeByID(int ID);
    }
    public interface IRosterRepos : IBaseRepos
    {
        IList<RosterStruct> GetAllRosters();
        RosterStruct GetRoster(int ID);
        void DeleteRoster(int ID);
        void AddRoster(User_Roster Roster);
        void EditRoster(User_Roster Roster);

        RosterTeamStruct GetPlayer(int ID);
        void DeletePlayer(int ID);
        void AddPlayer(int PositonalID, string Name, int RosterID);
        void EditPlayer(User_Rosters_Positional Player);
        IList<RosterStruct> GetRostersBySpecialRule(int SPID);
        IList<RosterStruct> GetRostersByRace(int RaceID);
        IList<BaseRaceStruct> GetAllRaces();
        User_Roster GetRosterBase(int ID);

    }

}
