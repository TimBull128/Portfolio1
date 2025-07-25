using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using static BB2020MVC.Models.ServiceLayer.PlayerSkillTypes;
using BB2020MVC.Models.DataLayer;
using System.Web.UI.WebControls;

namespace BB2020MVC.Models.ServiceLayer
{
    //Interfaces
    //TODO: update all to take IDs, add "Single" function
    public interface ISRaces : IDataLayer<Race>
    {
        IList<Race> All();
        Race Single(int RaceID);
    }
    public interface ISPlayers : IDataLayer<Races_Players>
    {
        IList<Races_Players> All(int RaceID = 0);
        Races_Players Single(int PlayerID);
    }
    public interface ISPlayerSkills : IDataLayer<Races_Players_Skills>
    {
        IList<Races_Players_Skills> All(int PlayerID = 0);
        Races_Players_Skills Single(int PSkillID);
    }
    public interface ISRaceSpecialRules : IDataLayer<Races_SpecialRules> 
    {
        IList<Races_SpecialRules> All(int SRID = 0, int RaceID = 0);
        Races_SpecialRules Single(int RSRID);

    }
    public interface ISPlayerSkillTypes : IDataLayer<Races_Players_SkillTypes> 
    {
        IList<Races_Players_SkillTypes> All(int PlayerID = 0, CheckSkillTypes CheckType = CheckSkillTypes.NoCheck);
    }

    //Classes
    public class Races : DataLayer<Race>, ISRaces
    {
        public Races() => SetTable(DataContext().Races);
        public IList<Race> All()
        {
            return Read();
        }
        public Race Single(int RaceID)
        {
            return (from items in Read() where items.RaceID.Equals(RaceID) select items).First();
        }
    }
    public class Players : DataLayer<Races_Players>, ISPlayers
    {
        public Players() => SetTable(DataContext().Races_Players);
        public IList<Races_Players> All(int RaceID = 0)
        {
            return (from items in Read()
                    where (RaceID == 0 || items.RaceID.Equals(RaceID))
                    select items).ToList();
        }
        public Races_Players Single(int PlayerID)
        {
            return (from items in Read() where items.PlayerID.Equals(PlayerID) select items).First();
        }
    }
    public class PlayerSkills : DataLayer<Races_Players_Skills>, ISPlayerSkills
    {
        public PlayerSkills() => SetTable(DataContext().Races_Players_Skills);

        public IList<Races_Players_Skills> All(int PlayerID = 0)
        {
            return (from item in Read()
                    where (PlayerID == 0 || item.PlayerID.Equals(PlayerID))
                    select item).ToList();
        }
        public Races_Players_Skills Single(int PSkillID)
        {
            return (from item in Read() where item.PSkillID.Equals(PSkillID) select item).First();
        }
    }
    public class RaceSpecialRules : DataLayer<Races_SpecialRules>, ISRaceSpecialRules
    {
        public RaceSpecialRules() => SetTable(DataContext().Races_SpecialRules);
        public IList<Races_SpecialRules> All(int SRID = 0, int RaceID = 0)
        {
            return (from items in Read()
                    where ((SRID == 0 || items.SRID.Equals(SRID)) && (RaceID == 0 || items.RaceID.Equals(RaceID)))
                    select items).ToList();
        }
        public Races_SpecialRules Single (int RSRID)
        {
            return (from item in Read() where item.RSRID.Equals(RSRID) select item).First();
        }
    }
    public class PlayerSkillTypes : DataLayer<Races_Players_SkillTypes>, ISPlayerSkillTypes
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

        public Races_Players_SkillTypes Single(int PSkillTypeID)
        {
            return (from item in Read() where item.PSkillTypeID.Equals(PSkillTypeID) select item).Single();
        }
    }
    public enum CheckSkillTypes
    {
        NoCheck = 0,
        Single = 1,
        Double = 2
    }
}
