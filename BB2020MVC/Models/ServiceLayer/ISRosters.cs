using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BB2020MVC.Models.DataLayer;

namespace BB2020MVC.Models.ServiceLayer
{
    public interface ISRosters : IDataLayer<User_Rosters>
    {
        IList<User_Rosters> All(int RosterID = 0);
        User_Rosters Single(int RosterID);

    }
    public interface ISRostersInjury : IDataLayer<User_Rosters_Injury>
    {
        IList<User_Rosters_Injury> All();
    }
    public interface ISRostersLevelTypes : IDataLayer<User_Rosters_LvlTypes>
    {
        IList<User_Rosters_LvlTypes> All();
    }
    public interface ISRostersPlayers : IDataLayer<User_Rosters_Players> 
    {
        IList<User_Rosters_Players> All();
    }
    public interface ISRostersSkills : IDataLayer<User_Rosters_Skills>
    {
        IList<User_Rosters_Skills> All();
    }
    public class Rosters : DataLayer<User_Rosters>, ISRosters
    {
        public Rosters() => SetTable(DataContext().User_Rosters);
        public IList<User_Rosters> All(int RosterID = 0)
        {
            return (from item in Read() 
                    where (RosterID == 0 || item.RosterID.Equals(RosterID) )
                    select item).ToList();
        }
        public User_Rosters Single(int RosterID)
        {
            return All(RosterID).First();
        }
    }
    public class RostersInjury : DataLayer<User_Rosters_Injury>, ISRostersInjury
    {
        public RostersInjury() => SetTable(DataContext().User_Rosters_Injury);
        public IList<User_Rosters_Injury> All()
        {
            return Read();
        }
    }
    public class RostersLevelTypes : DataLayer<User_Rosters_LvlTypes>, ISRostersLevelTypes
    {
        public RostersLevelTypes() => SetTable(DataContext().User_Rosters_LvlTypes);
        public IList<User_Rosters_LvlTypes> All()
        {
            return Read();
        }
    }
    public class RostersPlayers : DataLayer<User_Rosters_Players>, ISRostersPlayers
    {
        public RostersPlayers() => SetTable(DataContext().User_Rosters_Players);
        public IList<User_Rosters_Players> All()
        {
            return Read();
        }
    }
    public class RostersSkills : DataLayer<User_Rosters_Skills>, ISRostersSkills
    {
        public RostersSkills() => SetTable(DataContext().User_Rosters_Skills);
        public IList<User_Rosters_Skills> All()
        {
            return Read();
        }
    }
}
