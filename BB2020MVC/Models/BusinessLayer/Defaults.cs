using BB2020MVC.Models.DataLayer.Races;
using BB2020MVC.Models.ServiceLayer;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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
    }
}