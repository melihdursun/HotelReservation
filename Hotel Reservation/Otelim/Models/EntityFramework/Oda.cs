//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Otelim.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Oda
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Oda()
        {
            this.Rezervasyonlar = new HashSet<Rezervasyonlar>();
        }
    
        public int id { get; set; }
        public int tipi { get; set; }
        public string manzara { get; set; }
        public Nullable<bool> sigaraicilme { get; set; }
        public int fiyat { get; set; }
        public int odano { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rezervasyonlar> Rezervasyonlar { get; set; }
    }
}
