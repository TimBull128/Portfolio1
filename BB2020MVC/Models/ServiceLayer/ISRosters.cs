using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB2020MVC.Models.ServiceLayer
{
    public interface ISRosters : IServiceLayer_Base<User_Rosters>
    {
    }
    public interface ISRostersInjury : IServiceLayer_Base<User_Rosters_Injury>
    { 
    }
    public interface ISRostersLevelTypes : IServiceLayer_Base<User_Rosters_LvlTypes>
    {
    }
    public interface ISRostersPlayers : IServiceLayer_Base<User_Rosters_Players> 
    { 
    }
    public interface ISRostersSkills : IServiceLayer_Base<User_Rosters_Skills>
    {
    }
    public class Rosters : ServiceLayer_Base<User_Rosters>, ISRosters
    {
        public Rosters() => SetTable(DataContext().User_Rosters);
    }
    public class RostersInjury : ServiceLayer_Base<User_Rosters_Injury>, ISRostersInjury
    {
        public RostersInjury() => SetTable(DataContext().User_Rosters_Injury);
    }
    public class RostersLevelTypes : ServiceLayer_Base<User_Rosters_LvlTypes>, ISRostersLevelTypes
    {
        public RostersLevelTypes() => SetTable(DataContext().User_Rosters_LvlTypes);
    }
    public class RostersPlayers : ServiceLayer_Base<User_Rosters_Players>, ISRostersPlayers
    {
        public RostersPlayers() => SetTable(DataContext().User_Rosters_Players);
    }
    public class RostersSkills : ServiceLayer_Base<User_Rosters_Skills>, ISRostersSkills
    {
        public RostersSkills() => SetTable(DataContext().User_Rosters_Skills);
    }
}
