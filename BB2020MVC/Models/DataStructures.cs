using BB2020MVC.Models.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BB2020MVC.Models
{
    //Data setups - all are different from Data model

    public class RaceNames
    {
        public int ID;
        public string Name;
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