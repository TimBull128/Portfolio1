using BB2020MVC.Models.ServiceLayer;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;

namespace BB2020MVC.Models.BusinessLayer
{
    public interface IDefaults
    {
        Race Race();
        Races_Players Races_Players(int RaceID);
        Races_Players_Skills Races_Players_Skills(int PlayerID);
        Races_SpecialRules Races_Special_Rules(int IRaceID);
        Races_Players_SkillTypes Races_Players_SkillTypes(int PlayerID, int STypeID);

    }
    public class Default : IDefaults
    {
        public Race Race()
        {
            return new Race()
            {
                RaceID = 0,
                Name = "",
                Apothecary = false,
                RerollCost = 0,
            };
        }
        public Races_Players Races_Players(int RaceID) 
        {
            return new Races_Players()
            {
                PlayerID = 0,
                Name = "",
                AG = 6,
                AV = 1,
                MA = 1,
                PA = 7,
                ST = 1,
                Cost = 0,
                RaceID = RaceID
            };
        }
        public Races_Players_Skills Races_Players_Skills(int PlayerID)
        {
            return new Races_Players_Skills()
            {
                PlayerID = PlayerID,
                PSkillID = 0,
            };
        }
        public Races_SpecialRules Races_Special_Rules(int IRaceID)
        {
            return new Races_SpecialRules()
            {
                RSRID = 0,
                RaceID = IRaceID,
                SRID = 0
            };
        }
        public Races_Players_SkillTypes Races_Players_SkillTypes(int PlayerID, int STypeID)
        {
            return new Races_Players_SkillTypes()
            {
                PSkillTypeID = 0,
                PlayerID = PlayerID,
                Single = false,
                STypeID = STypeID,

            };
        }
        public Rules_LvlType Rules_LvlType()
        {
            return new Rules_LvlType()
            {
                ID = 0,
                Description = "",
                Cost = 0
            };
        }
        public Rules_Skills_List Rules_Skills_List()
        {
            return new Rules_Skills_List()
            {
                SkillID = 0,
                Description = "",
                Name = "",
                NotOptional = false,
                SkillTypeID = -1
            };
            return NewSkillBase;
        }
        public Rules_Skills_Types GetNewSkillTypeBase()
        {
            Rules_Skills_Types NewSkillType = new Rules_Skills_Types()
            {
                STypeID = GetNewSkillTypeID(),
                Name = "",
                Description = ""
            };
            return NewSkillType;
        }
        public Rules_SpecialRules GetNewSpecialRuleBase()
        {
            Rules_SpecialRules NewSpecialRule = new Rules_SpecialRules()
            {
                SRID = GetNewSpecialRuleID(),
                Name = "",
                Description = ""
            };
            return NewSpecialRule;
        }
        public Rules_InjuryTypes GetNewInjuryTypesBase()
        {
            return new Rules_InjuryTypes
            {
                InjuryID = GetNewInjuryTypeID(),
                Description = "",
                Name = ""
            };
        }
        public Rules_Skills_FSkills GetNewFSkillBase(int SkillID) { }
        public User_Rosters NewRoster(int RaceID)
        {
            return new User_Rosters
            {
                RosterID = GetNewRosterID(),
                Apoths = false,
                CheerLeadersQTY = 0,
                RaceID = RaceID,
                RerollsQTY = 0,
                CoachesQTY = 0,
                InTourney = false,
                Name = "",
                Treasury = 0,
                TV = 0

            };
        }
        public User_Rosters_Players NewRosterPlayer(int RosterID, int RaceID)
        {
            var Player =
                (from item in GetPlayersByRaceID(RaceID)
                 where item.Name == "NA"
                 select item).First();

            return new User_Rosters_Players
            {
                RPlayerID = GetNewRosterPlayerID(),
                PlayerID = Player.PlayerID,
                Name = "",
                RosterID = RosterID,
                SPP = 0
            };
        }
    }
}