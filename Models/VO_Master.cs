//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VO_Master
    {
        public int Void_pk { get; set; }
        public Nullable<int> DistrictId_fk { get; set; }
        public Nullable<int> BlockId_fk { get; set; }
        public Nullable<int> CLF_Id_fk { get; set; }
        public Nullable<int> Panchayatid_fk { get; set; }
        public string Panchayat { get; set; }
        public string Village_Organization { get; set; }
        public string F5 { get; set; }
        public string CRUD { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
