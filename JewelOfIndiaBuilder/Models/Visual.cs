//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JewelOfIndiaBuilder.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Visual
    {
        public Visual()
        {
            this.Apartments = new HashSet<Apartment>();
            this.Properties = new HashSet<Property>();
            this.Towers = new HashSet<Tower>();
        }
    
        public long Id { get; set; }
        public string ImageName { get; set; }
    
        public virtual ICollection<Apartment> Apartments { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<Tower> Towers { get; set; }
    }
}
