//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BB2020MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_Rosters
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User_Rosters()
        {
            this.User_Rosters_Players = new HashSet<User_Rosters_Players>();
        }
    
        public int RosterID { get; set; }
        public int RaceID { get; set; }
        public string Name { get; set; }
        public int RerollsQTY { get; set; }
        public bool Apoths { get; set; }
        public int CheerLeadersQTY { get; set; }
        public int CoachesQTY { get; set; }
        public int TV { get; set; }
        public int Treasury { get; set; }
        public bool InTourney { get; set; }
    
        public virtual Race Race { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User_Rosters_Players> User_Rosters_Players { get; set; }
    }
}
