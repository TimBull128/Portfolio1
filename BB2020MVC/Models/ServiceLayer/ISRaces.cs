using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using static BB2020MVC.Models.ServiceLayer.PlayerSkillTypes;

namespace BB2020MVC.Models.ServiceLayer
{
    //Interfaces
    public interface ISRaces : IServiceLayer_Base<Race>
    {
    }
    public interface ISPlayers : IServiceLayer_Base<Races_Players>
    {
        IList<Races_Players> All(int RaceID = 0);
    }
    public interface ISPlayerSkills : IServiceLayer_Base<Races_Players_Skills>
    {
        IList<Races_Players_Skills> All(int PlayerID = 0);
    }
    public interface ISRaceSpecialRules : IServiceLayer_Base<Races_SpecialRules> 
    {
        IList<Races_SpecialRules> All(int SRID = 0, int RaceID = 0);
    }
    public interface ISPlayerSkillTypes : IServiceLayer_Base<Races_Players_SkillTypes> 
    {
        IList<Races_Players_SkillTypes> All(int PlayerID = 0, CheckSkillTypes CheckType = CheckSkillTypes.NoCheck);
    }

    //Classes
    public class Races : ServiceLayer_Base<Race>, ISRaces
    {
        public Races() => SetTable(DataContext().Races);
    }
    public class Players : ServiceLayer_Base<Races_Players>, ISPlayers
    {
        public Players() => SetTable(DataContext().Races_Players);
        public IList<Races_Players> All(int RaceID = 0)
        {
            return (from items in Read()
                    where (RaceID == 0 || items.RaceID.Equals(RaceID))
                    select items).ToList();
        }
    }
    public class PlayerSkills : ServiceLayer_Base<Races_Players_Skills>, ISPlayerSkills
    {
        public PlayerSkills() => SetTable(DataContext().Races_Players_Skills);

        public IList<Races_Players_Skills> All(int PlayerID = 0)
        {
            return (from item in Read()
                    where (PlayerID == 0 || item.PlayerID.Equals(PlayerID))
                    select item).ToList();
        }
    }
    public class RaceSpecialRules : ServiceLayer_Base<Races_SpecialRules>, ISRaceSpecialRules
    {
        public RaceSpecialRules() => SetTable(DataContext().Races_SpecialRules);
        public IList<Races_SpecialRules> All(int SRID = 0, int RaceID = 0)
        {
            return (from items in Read()
                    where ((SRID == 0 || items.SRID.Equals(SRID)) && (RaceID == 0 || items.RaceID.Equals(RaceID)))
                    select items).ToList();
        }
    }
    public class PlayerSkillTypes : ServiceLayer_Base<Races_Players_SkillTypes>, ISPlayerSkillTypes
    {
        public PlayerSkillTypes() => SetTable(DataContext().Races_Players_SkillTypes);
        public IList<Races_Players_SkillTypes> All(int PlayerID = 0, CheckSkillTypes CheckType = CheckSkillTypes.NoCheck)
        {
            return (from item in Read()
                    where (
                    (CheckType == CheckSkillTypes.Single && item.Single)
                    || (CheckType == CheckSkillTypes.Double && !item.Single)
                    || (CheckType == CheckSkillTypes.NoCheck && 
                            (PlayerID == 0 || item.PlayerID.Equals(PlayerID))
                        )
                    )
                    select item).ToList();
        }
        public enum CheckSkillTypes
        {
            NoCheck = 0,
            Single = 1,
            Double = 2
        }
    }
}
