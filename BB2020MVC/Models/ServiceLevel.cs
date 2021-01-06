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

        IList<Rules_LvlType> GetAllLevelTypes();
        IList<RaceNames> GetRaceNames();
        IList<Race> GetAllRaces();
        IList<Rules_Skills_List> GetAllSkills();
        Rules_Skills_List GetSkill(int ID);
        IList<Rules_Skills_Type> GetAllRulesSkillTypes();
        IList<Rules_SpecialRule> GetAllSpecialRules();


    }
    public interface IRacesRepos : IBaseRepos
    {
        Race GetNewRaceBase();
        Races_Player GetNewPlayerBase(int RaceID);
        Races_Players_Skill GetNewSkillBase(int PlayerID);
        Races_Players_SkillType GetNewPlayerSkillTypeBase(int PlayerID);

        Race GetRaceBase(int ID);
        Races_Player GetPlayerBase(int ID);
        Races_Players_Skill GetPlayerSkillBase(int ID);
        Races_Players_SkillType GetPlayerSkillTypeBase(int ID);

        IList<Races_Player> GetAllPlayers();
        IList<Races_Players_Skill> GetAllPlayerSkills();
        IList<Races_Players_SkillType> GetAllPlayerSkillTypes();

        void AddRace(Race NewRace);
        void AddPlayer(Races_Player Player);
        void AddPlayerSkill(Races_Players_Skill Skill);
        void AddPlayerSkillType(Races_Players_SkillType SkillType);

        void EditRace(Race RaceEdit);
        void EditPlayer(Races_Player Player);
        void EditPlayerSkillType(Races_Players_SkillType SkillType);

        void DeletePlayerSkill(int SkillID);
        void DelPlayer(int PlayerID);
        void DelPlayerSkillType(int SkillTypeID);

        IList<SelectListItem> SelectListMA(int MA = -1);
        IList<SelectListItem> SelectListST(int ST = -1);
        IList<SelectListItem> SelectListAG(int AG = -1);
        IList<SelectListItem> SelectListPA(int PA = -1);
        IList<SelectListItem> SelectListAV(int AV = -1);
        IList<SelectListItem> CreateSelectListPlayerSkills(int PlayerID);

        int GetTopIDValue();
        int GetLowIDValue();

        IList<Races_Players_Skill> GetPlayerSkillsbyPlayerID(int PlayerID);
        IList<Races_Players_SkillType> GetPlayerSkillTypesbyPlayerID(int PlayerID);
        IList<Race> GetRacesbySpecialRule(int SRID);
        Race GetRacebyPlayerID(int PlayerID);
        IList<SelectListItem> GetValidSkills(int PlayerID);
        BaseTeamStruct GetPlayer(int ID);
    }
    public interface IRulesRepos : IBaseRepos
    {
        Rules_LvlType GetNewLevelTypeBase();
        Rules_Skills_List GetNewSkillBase();
        Rules_Skills_Type GetNewSkillTypeBase();
        Rules_SpecialRule GetNewSpecialRuleBase();
        

        IList<Rules_Skills_List> GetSkillsByType(int TypeID);

        IList<SelectListItem> CreateSelectListSkillTypes(int SelectedValue = -1);
        IList<SelectListItem> CreateSelectListSkills(int SelectedValue = -1);
        IList<SelectListItem> CreateSelectListLevelTypes(int SelectedValue = -1);
        IList<SelectListItem> CreateSelectListSpecialRules(int SelectedValue = -1);

        void AddLevelType(Rules_LvlType LevelType);
        void AddSkillType(Rules_Skills_Type Type);
        void AddSkill(Rules_Skills_List Skill);
        void AddSpecialRule(Rules_SpecialRule SpecialRule);

        void EditSkill(Rules_Skills_List Skill);
        void EditLevelType(Rules_LvlType LevelType);
        void EditSpecialRule(Rules_SpecialRule SpecialRule);
        void EditSkillType(Rules_Skills_Type Type);

        void DeleteLevelType(int ID);
        void DeleteSkill(int ID);
        void DeleteSpecialRule(int ID);
        void DeleteSkillType(int ID);

        Rules_LvlType GetLevelType(int ID);
        Rules_SpecialRule GetSpecialRule(int ID);
        Rules_Skills_Type GetSkillType(int ID);

    }
    public interface IRosterRepos : IBaseRepos
    {
        User_Roster GetNewRosterBase();
        User_Rosters_Skill GetNewRosterSkillBase(int PlayerID);
        User_Rosters_Positional GetNewRosterPlayerBase(int RosterID);
        User_Rosters_LvlType GetNewRosterLvlTypeBase(int PlayerID);

        User_Rosters_Skill GetRosterSkillBase(int ID);
        User_Roster GetRosterBase(int ID);
        User_Rosters_Positional GetPlayerBase(int PlayerID);
        User_Rosters_LvlType GetLvlTypeBase(int ID);

        IList<User_Rosters_LvlType> GetLvlTypesByPlayerID(int PlayerID);
        IList<User_Rosters_Positional> GetPlayersByRosterID(int RosterID);
        IList<User_Rosters_Skill> GetSkillsByPlayerID(int PlayerID);
        IList<User_Roster> GetRostersByRace(int RaceID);
        IList<User_Roster> GetRostersBySpecialRule(int SRID);

        IList<User_Roster> GetAllRosters();
        IList<RosterNames> GetAllRosterNames();
        IList<User_Rosters_Positional> GetAllRosterPlayers();
        IList<User_Rosters_LvlType> GetAllLvlTypes();
        IList<User_Rosters_Skill> GetAllRosterSkill();

        void AddRoster(User_Roster Roster);
        void EditRoster(User_Roster Roster);
        void DeleteRoster(int ID);

        void AddRosterPlayer(User_Rosters_Positional Player);
        void EditRosterPlayer(User_Rosters_Positional Player);
        void DeleteRosterPlayer(int ID);

        void AddPlayerSkill(User_Rosters_Skill Skill);
        void DeletePlayerSkill(int ID);

        void AddLvlType(User_Rosters_LvlType LvlType);
        void DeleteLvlType(int ID);

        IList<SelectListItem> CreateValidSkillSelect(int PlayerID);
        IList<SelectListItem> CreateLevelTypeSelect(int LevelSelectID = -1);
        IList<SelectListItem> CreateSelectListRace(int SelectValue = -1);
    }
    

}
