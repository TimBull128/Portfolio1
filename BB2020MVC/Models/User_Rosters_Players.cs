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
    
    public partial class User_Rosters_Players
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User_Rosters_Players()
        {
            this.User_Rosters_Injury = new HashSet<User_Rosters_Injury>();
            this.User_Rosters_LvlTypes = new HashSet<User_Rosters_LvlTypes>();
            this.User_Rosters_Skills = new HashSet<User_Rosters_Skills>();
        }
    
        public int RPlayerID { get; set; }
        public int PlayerID { get; set; }
        public int SPP { get; set; }
        public int RosterID { get; set; }
        public string Name { get; set; }
    
        public virtual Races_Players Races_Players { get; set; }
        public virtual User_Rosters User_Rosters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User_Rosters_Injury> User_Rosters_Injury { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User_Rosters_LvlTypes> User_Rosters_LvlTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User_Rosters_Skills> User_Rosters_Skills { get; set; }
    }
}
