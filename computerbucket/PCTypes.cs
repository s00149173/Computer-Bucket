//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace computerbucket
{
    using System;
    using System.Collections.Generic;
    
    public partial class PCTypes
    {
        public PCTypes()
        {
            this.PreBuildPCs = new HashSet<PreBuildPCs>();
        }
    
        public int PCTypeID { get; set; }
        public string TypeNae { get; set; }
    
        public virtual ICollection<PreBuildPCs> PreBuildPCs { get; set; }
    }
}
