using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BB2020MVC.Models
{
    public interface IBaseRepos
    {
        IList<RaceNames> GetRaceNames();
        IList<SelectListItem> CreateSelectListSkillTypes();
        IList<SelectListItem> CreateSelectListSkills(int SelectedValue = -1);
        IList<SelectListItem> CreateSelectListLevelTypes(int SelectedValue = -1);
        IList<Races_Players_SkillTypes> GetPlayerSkillTypes(int ID);
        IList<Races_SpecialRules> GetSpecialRulesByRaceID(int RaceID);
        BaseRaceStruct GetRace(int ID);

    }
    public interface IRacesRepos : IBaseRepos
    {
        Race GetNewRaceBase();
        Races_Players GetNewPlayerBase(int RaceID);
        Races_Players_Skills GetNewSkillBase(int PlayerID);
        Races_Players_SkillTypes GetNewPlayerSkillTypeBase(int PlayerID);


        Races_Players_Skills GetPlayerSkillBase(int ID);
        Races_Players_SkillTypes GetPlayerSkillTypeBase(int ID);
        Races_SpecialRules GetSpecialRulesBase(int ID);

        SelectList SelectListMA();
        SelectList SelectListST();
        SelectList SelectListAG();
        SelectList SelectListPA();
        SelectList SelectListAV();
        SelectList CreateSelectListPlayerSkills(int PlayerID);
        SelectList CreateSelectListSpecialRules();
        SelectList SLSkills();

        void EditRaceSpecialRule(Races_SpecialRules SR);

        int GetTopIDValue();
        int GetLowIDValue();

        IList<Races_Players_Skills> GetPlayerSkillsbyPlayerID(int PlayerID);

        IList<Race> GetRacesbySpecialRule(int SRID);
        Race GetRacebyPlayerID(int PlayerID);
        IList<SelectListItem> GetValidSkills(int PlayerID);
        BaseTeamStruct GetPlayer(int ID);
        


    }
    public interface IRulesRepos : IBaseRepos
    {
        Rules_LvlType GetNewLevelTypeBase();
        Rules_Skills_List GetNewSkillBase();
        Rules_Skills_Types GetNewSkillTypeBase();
        Rules_SpecialRules GetNewSpecialRuleBase();
        Rules_InjuryTypes GetNewInjuryTypesBase();
        Rules_Skills_FSkills GetNewFSkillBase(int SkillID);



        IList<Rules_Skills_List> GetSkillsByType(int TypeID);



        void AddLevelType(Rules_LvlType LevelType);
        void AddSkillType(Rules_Skills_Types Type);
        void AddSkill(Rules_Skills_List Skill);
        void AddSpecialRule(Rules_SpecialRules SpecialRule);
        void AddInjuryType(Rules_InjuryTypes InjuryType);
        void AddForbiddenSkill(Rules_Skills_FSkills FSkill);

        void EditSkill(Rules_Skills_List Skill);
        void EditLevelType(Rules_LvlType LevelType);
        void EditSpecialRule(Rules_SpecialRules SpecialRule);
        void EditSkillType(Rules_Skills_Types Type);

        void DeleteLevelType(int ID);
        void DeleteSkill(int ID);
        void DeleteSpecialRule(int ID);
        void DeleteSkillType(int ID);
        void DeleteInjuryType(int ID);
        void DeleteForbiddenSkill(int ID);


        Rules_SpecialRules GetSpecialRule(int ID);
        Rules_Skills_FSkills GetFSkillBase(int FSkillID);
        Rules_InjuryTypes GetInjuryType(int ID);

        IList<Rules_Skills_FSkills> GetAllFSkills();
        IList<Rules_InjuryTypes> GetAllInjuryTypes();
        IList<Rules_Skills_FSkills> GetFSkillListforSkill(int ID);
        SelectList CreateFSkillList(int SkillID);

    }
    public interface IRosterRepos : IBaseRepos
    {
        User_Rosters NewRoster(int RaceID);


        IList<User_Rosters> GetAllRosters();


        SelectList SLRace();

        void AddRoster(User_Rosters Roster);

        User_Rosters GetRoster(int RosterID);
    }
    

}
