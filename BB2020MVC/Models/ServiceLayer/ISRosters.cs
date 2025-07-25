using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BB2020MVC.Models.DataLayer;

namespace BB2020MVC.Models.ServiceLayer
{
    public interface ISRosters : IDataLayer<User_Rosters>
    {
        IList<User_Rosters> All();
        User_Rosters Single(int RosterID);

    }
    public interface ISRostersInjury : IDataLayer<User_Rosters_Injury>
    {
        IList<User_Rosters_Injury> All();
        User_Rosters_Injury Single(int RInjuryID);
    }
    public interface ISRostersLevelTypes : IDataLayer<User_Rosters_LvlTypes>
    {
        IList<User_Rosters_LvlTypes> All();
        User_Rosters_LvlTypes Single(int RLevelTypeID);
    }
    public interface ISRostersPlayers : IDataLayer<User_Rosters_Players> 
    {
        IList<User_Rosters_Players> All();
        User_Rosters_Players Single(int RPlayerID);
    }
    public interface ISRostersSkills : IDataLayer<User_Rosters_Skills>
    {
        IList<User_Rosters_Skills> All();
        User_Rosters_Skills Single(int RPlayerID);
    }
    public class Rosters : DataLayer<User_Rosters>, ISRosters
    {
        public Rosters() => SetTable(DataContext().User_Rosters);
        public IList<User_Rosters> All()
        {
            return Read();
        }
        public User_Rosters Single(int RosterID)
        {
            return (from item in Read() where item.RosterID.Equals(RosterID) select item).First();
        }
    }
    public class RostersInjury : DataLayer<User_Rosters_Injury>, ISRostersInjury
    {
        public RostersInjury() => SetTable(DataContext().User_Rosters_Injury);
        public IList<User_Rosters_Injury> All()
        {
            return Read();
        }
        public User_Rosters_Injury Single(int RInjuryID)
        {
            return (from item in Read() where item.RInjuryID.Equals(RInjuryID) select item).First();
        }
    }
    public class RostersLevelTypes : DataLayer<User_Rosters_LvlTypes>, ISRostersLevelTypes
    {
        public RostersLevelTypes() => SetTable(DataContext().User_Rosters_LvlTypes);
        public IList<User_Rosters_LvlTypes> All()
        {
            return Read();
        }
        public User_Rosters_LvlTypes Single(int RLevelTypeID)
        {
            return (from item in Read() where item.RLevelTypeID.Equals(RLevelTypeID) select item).First();
        }
    }
    public class RostersPlayers : DataLayer<User_Rosters_Players>, ISRostersPlayers
    {
        public RostersPlayers() => SetTable(DataContext().User_Rosters_Players);
        public IList<User_Rosters_Players> All()
        {
            return Read();
        }
        public User_Rosters_Players Single(int RPlayerID)
        {
            return (from item in Read() where item.RPlayerID.Equals(RPlayerID) select item).First();
        }
    }
    public class RostersSkills : DataLayer<User_Rosters_Skills>, ISRostersSkills
    {
        public RostersSkills() => SetTable(DataContext().User_Rosters_Skills);
        public IList<User_Rosters_Skills> All()
        {
            return Read();
        }
        public User_Rosters_Skills Single(int RPlayerID)
        {
            return (from item in Read() where item.RSkillID.Equals(RPlayerID) select item).First();
        }
    }
}
