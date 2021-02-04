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
    
    public partial class Rules_Skills_List
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rules_Skills_List()
        {
            this.Races_Players_Skills = new HashSet<Races_Players_Skills>();
            this.Rules_Skills_FSkills = new HashSet<Rules_Skills_FSkills>();
            this.Rules_Skills_FSkills1 = new HashSet<Rules_Skills_FSkills>();
            this.User_Rosters_Skills = new HashSet<User_Rosters_Skills>();
        }
    
        public int SkillID { get; set; }
        public int SkillTypeID { get; set; }
        public string Name { get; set; }
        public bool NotOptional { get; set; }
        public string Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Races_Players_Skills> Races_Players_Skills { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rules_Skills_FSkills> Rules_Skills_FSkills { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rules_Skills_FSkills> Rules_Skills_FSkills1 { get; set; }
        public virtual Rules_Skills_Types Rules_Skills_Types { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User_Rosters_Skills> User_Rosters_Skills { get; set; }
    }
}
