//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppTilausDBJaanaPusa.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TilauksetPaivittain
    {
        public long RiviID { get; set; }
        public Nullable<System.DateTime> Tilauspaiva { get; set; }
        public string Viikonpaiva { get; set; }
        public Nullable<int> Tilausmaara { get; set; }
    }
}