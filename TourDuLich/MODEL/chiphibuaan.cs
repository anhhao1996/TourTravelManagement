//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MODEL
{
    using System;
    using System.Collections.Generic;
    
    public partial class chiphibuaan
    {
        public chiphibuaan()
        {
            this.doanbuaans = new HashSet<doanbuaan>();
        }
    
        public int id { get; set; }
        public string buaan { get; set; }
    
        public virtual ICollection<doanbuaan> doanbuaans { get; set; }
    }
}
