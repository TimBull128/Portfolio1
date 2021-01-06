using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public int ID, RerollCost;
        public string Name;
        public bool Apoth;
        public IList<Rules_SpecialRule> SpecialRules;
        public IList<BaseTeamStruct> Team;


    }
    public class CustomSkillType
    {
        public int ID, SkillTypeID;
        public string SkillTypeName;
    }
    public class NewRaceStruct
    {
        public int ID, RerollCost;
        public string Name;
        public bool Apoth;
        public int? SRID1, SRID2, SRID3;
    }
    public class NewPlayerStruct
    {
        public int ID, RaceID, MA, AG, AV, Cost;
        public int? PA, STR;
    }
    public class NewPlayerSkillStruct
    {
        public int ID;
        public int? PlayerID, SkillID;
    }

    public class RaceNames
    {
        public int ID;
        public string Name;
    }
    public class BaseTeamStruct
    {
        public int ID, MA, ST, AG, AV, Cost, RaceID;
        public string Name, RaceName;
        public int? PA;
        public IList<Races_Players_Skill> PlayerSkills;
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
}