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
    
    public partial class Races_Players_SkillTypes
    {
        public int PSkillTypeID { get; set; }
        public int STypeID { get; set; }
        public int PlayerID { get; set; }
        public bool Single { get; set; }
    
        public virtual Races_Players Races_Players { get; set; }
        public virtual Rules_Skills_Types Rules_Skills_Types { get; set; }
    }
}
