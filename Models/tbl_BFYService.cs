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
    
    public partial class tbl_BFYService
    {
        public System.Guid ServiceBFYId_pk { get; set; }
        public Nullable<System.Guid> FollowId_fk { get; set; }
        public Nullable<System.Guid> BFYId_fk { get; set; }
        public Nullable<int> ServiceYearId { get; set; }
        public Nullable<int> ServiceMonthId { get; set; }
        public Nullable<bool> IsPeerPresent { get; set; }
        public Nullable<bool> IsFollowUpHV { get; set; }
        public Nullable<bool> IsContraception { get; set; }
        public Nullable<int> ContraceptionId_fk { get; set; }
        public string ContraceptionOther { get; set; }
        public Nullable<int> UseMethodId_fk { get; set; }
        public Nullable<bool> Isservice { get; set; }
        public Nullable<System.DateTime> ServiceRevcDt { get; set; }
        public Nullable<int> ServiceProvider { get; set; }
        public string Location { get; set; }
        public Nullable<decimal> CMEligible { get; set; }
        public Nullable<decimal> CNRPEligible { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> DistrictId_fk { get; set; }
        public Nullable<int> BlockId_fk { get; set; }
        public Nullable<int> ClusterId_fk { get; set; }
        public Nullable<int> PanchayatId_fk { get; set; }
        public Nullable<int> VoId_fk { get; set; }
        public Nullable<bool> Approved1Status { get; set; }
        public Nullable<System.DateTime> Approved1Date { get; set; }
        public string Approved1Remarks { get; set; }
        public string Approved1By { get; set; }
        public Nullable<bool> Approved2Status { get; set; }
        public Nullable<System.DateTime> Approved2Date { get; set; }
        public string Approved2Remarks { get; set; }
        public string Approved2By { get; set; }
        public Nullable<bool> Approved3Status { get; set; }
        public Nullable<System.DateTime> Approved3Date { get; set; }
        public string Approved3Remarks { get; set; }
        public string Approved3By { get; set; }
    }
}
