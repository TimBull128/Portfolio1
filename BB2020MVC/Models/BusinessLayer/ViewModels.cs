using BB2020MVC.Models.DataLayer.Races;
using BB2020MVC.Models.DataLayer.Rules;
using BB2020MVC.Models.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB2020MVC.Models.BusinessLayer
{
    public class PlayerVM
    {
        public PlayerVM(Races_Players player, ISPlayerSkills Skills_repo, ISSkillsTypes SkillTypes_repo, ISPlayerSkillTypes PlayerSkillTypes_repo)
        {
            ID = player.PlayerID;
            MA = player.MA;
            ST = player.ST;
            AG = player.AG;
            AV = player.AV;
            Cost = player.Cost;
            MaxQTY = player.MaxQTY;
            RaceID = player.RaceID;
            Name = player.Name;
            PA = player.PA;
            PlayerSkills = Skills_repo.All(player.PlayerID);
            SingleSkillTypes = (from PlayerSkillTypes in PlayerSkillTypes_repo.All(player.PlayerID, PlayerSkillTypes.CheckSkillTypes.Single)
                                from SkillType in SkillTypes_repo.All()
                                where PlayerSkillTypes.STypeID == SkillType.SkillTypeID
                                select new PlayerSkillVM()
                                {
                                    ID = PlayerSkillTypes.STypeID,
                                    SkillTypeID = SkillType.SkillTypeID,
                                    SkillTypeName = SkillType.Name,
                                    Description = SkillType.Description,
                                }).ToList();
            DoubleSkillTypes = (from PlayerSkillTypes in PlayerSkillTypes_repo.All(player.PlayerID, PlayerSkillTypes.CheckSkillTypes.Double)
                                from SkillType in SkillTypes_repo.All()
                                where PlayerSkillTypes.STypeID == SkillType.SkillTypeID
                                select new PlayerSkillVM()
                                {
                                    ID = PlayerSkillTypes.STypeID,
                                    SkillTypeID = SkillType.SkillTypeID,
                                    SkillTypeName = SkillType.Name,
                                    Description = SkillType.Description,
                                }).ToList();

        }

        public int ID, MA, ST, AG, AV, Cost, MaxQTY, RaceID;
        public string Name;
        public int? PA;
        public IList<Races_Players_Skills> PlayerSkills;
        public IList<PlayerSkillVM> SingleSkillTypes;
        public IList<PlayerSkillVM> DoubleSkillTypes;
    }
    public class RaceVM
    {
        public RaceVM(Race Race, IList<PlayerVM> Players, ISSpecialRules SpecialRules_repo, ISRaceSpecialRules RaceSpecialRules_repo) {
            ID = Race.RaceID;
            RerollCost = Race.RerollCost;
            Tier = Race.Tier;
            Name = Race.Name;
            Apothecary = Race.Apothecary;
            SpecialRules = (from SpecialRules in SpecialRules_repo.All()
                            from RaceSpecialRules in RaceSpecialRules_repo.All(Race.RaceID)
                            where SpecialRules.SRID == RaceSpecialRules.SRID
                            select SpecialRules).ToList();
            this.Players = Players;
        }

        public int ID, RerollCost, Tier;
        public string Name;
        public bool Apothecary;
        public IList<Rules_SpecialRules> SpecialRules;
        public IList<PlayerVM> Players;
    }
    public class PlayerSkillVM
    {
        public int ID;
        public int SkillTypeID;
        public string SkillTypeName;
        public string Description;
    }
}