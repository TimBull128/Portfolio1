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
    
    public partial class Rules_LvlType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rules_LvlType()
        {
            this.User_Rosters_LvlTypes = new HashSet<User_Rosters_LvlTypes>();
        }
    
        public int ID { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User_Rosters_LvlTypes> User_Rosters_LvlTypes { get; set; }
    }
}
