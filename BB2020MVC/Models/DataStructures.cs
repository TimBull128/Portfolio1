using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BB2020MVC.Models
{
    //Data setups - all are different from Data model
    
    /// <summary>
    /// Structure for Races
    /// ID = Primary key
    /// Name = Name of the Race
    /// Apoth = is Apothacary available
    /// ReRollCost = Cost of Rerolls
    /// Special Rules = What special rules apply - max = 3 - if null - the special rules is for multiple "Favoured of" 
    
    /// </summary>
    public class BaseRaceStruct
    {
        public int ID, RerollCost, Tier;
        public string Name;
        public bool Apoth;
        public IList<Rules_SpecialRules> SpecialRules;
        public IList<BaseTeamStruct> Team;


    }
    public class CustomSkillType
    {
        public int ID, SkillTypeID;
        public string SkillTypeName;
    }


    public class RaceNames
    {
        public int ID;
        public string Name;
    }
    public class BaseTeamStruct
    {
        public int ID, MA, ST, AG, AV, Cost, MaxQTY, RaceID;
        public string Name;
        public int PA;
        public IList<Races_Players_Skills> PlayerSkills;
        public IList<CustomSkillType> SingleSkillTypes;
        public IList<CustomSkillType> DoubleSkillTypes;
    }



    //------------------------------------------------------------------------------------

    //Rosters Custom Structs

    //------------------------------------------------------------------------------------

    public class RosterNames
    {
        public int ID, RaceID;
        public string Name, RaceName;

    }
    public class SkillTypeSelects
    { 
        public string Type;
        public IList<SelectListItem> SelectList;
    }
    public class PlayerMissing
    {
        public int PlayerID;
        public bool MNG;
    }
    //--------------------------------------------------------------------------
    //-------------------------ViewModels --------------------------------------
    //--------------------------------------------------------------------------
    //Naming convention - [NameOfScreen]VM
    public class EditRaceSRVM
    {
        public Races_SpecialRules RSR1 { get; set; }
        public Races_SpecialRules RSR2 { get; set; }
        public Races_SpecialRules RSR3 { get; set; }
        public Race Race { get; set; }
        public SelectList Select { get; set; }
    }
    public class SkillIndexVM
    {
        public IList<Rules_Skills_Types> SkillTypes;
        public IList<Rules_Skills_List> SkillList;
        public IList<Races_Players_Skills> PSkillList;
        public Race Race;
        public Races_Players Player;
        
    }
    public class SkillAlterVM
    {
        public Race Race { get; set; }
        public Races_Players Player { get; set; } 
        public Races_Players_Skills PlayerSkill { get; set; }
        public Rules_Skills_List RuleSkill { get; set; }
    }

    public class FSkillsVM
    {
        public IList<Rules_Skills_FSkills> FSkillList;
        public IList<Rules_Skills_List> SkillList;
        public Rules_Skills_List Skill;
    }
    public class AddFSkillVM
    {
        public Rules_Skills_List Skill { get; set; }
        public Rules_Skills_FSkills FSkill { get; set; }
        public SelectList List;
    }
    public class DeleteFSkillVM
    {
        public Rules_Skills_List Skill { get; set; }
        public Rules_Skills_List FSkill;
        public Rules_Skills_FSkills FSkillDetail { get; set; }
    }
}