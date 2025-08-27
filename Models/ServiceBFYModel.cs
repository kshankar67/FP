using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
//using ExpressiveAnnotations.Attributes;

namespace FP.Models
{
    public class ServiceBFYMainModel
    {
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> BlockId { get; set; }
        public Nullable<int> ClusterId { get; set; }
        public string ApprovedRemarks { get; set; }
        public int TypeLayer { get; set; }
       
    }
    public class ServiceBFYModel
    {
        public ServiceBFYModel()
        {
            ServiceBFYId_pk = Guid.Empty;
        }
        [Key]
        public System.Guid ServiceBFYId_pk { get; set; }
        public Nullable<int> DistrictId_fk { get; set; }
        public Nullable<int> BlockId_fk { get; set; }
        public Nullable<int> ClusterId_fk { get; set; }
        public Nullable<int> PanchayatId_fk { get; set; }
        public Nullable<int> VoId_fk { get; set; }
        [Required]
        [Display(Name = DisplayAchBFY.ServiceYearId)]
        public Nullable<int> ServiceYearId { get; set; }
        [Required]
        [Display(Name = DisplayAchBFY.ServiceMonthId)]
        public Nullable<int> ServiceMonthId { get; set; }
        //[Required]
        public Nullable<System.Guid> FollowId_fk { get; set; }
        // public Nullable<System.Guid> PlanId_fk { get; set; }
        [Required]
        public Nullable<System.Guid> BFYId_fk { get; set; }

        [Required]
        [Display(Name = DisplayAchBFY.IsPPresent)]
        public Nullable<bool> IsPeerPresent { get; set; }

        [Required]
        [Display(Name = DisplayAchBFY.IsFUpHV)]
        public Nullable<bool> IsFollowUpHV { get; set; }
        //[Required]
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsPeerPresent==true || IsFollowUpHV == true)")]
        [Display(Name = DisplayAchBFY.IsPPrIsCt)]
        public Nullable<bool> IsContraception { get; set; } = true;
        [RequiredIf("IsContraception", true)]
        [Display(Name = DisplayAchBFY.Ct)]
        public Nullable<int> ContraceptionId_fk { get; set; }
        [Display(Name = DisplayAchBFY.CtusemethodOther)]
        //[RequiredIf("ContraceptionId_fk", 4)]
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsContraception==true && ContraceptionId_fk == 4)")]
        public string ContraceptionOther { get; set; }
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsContraception==true && (ContraceptionId_fk == 1 || ContraceptionId_fk == 2))")]
        [Display(Name = DisplayAchBFY.Ctusemethod)]
        public Nullable<int> UseMethodId_fk { get; set; }
        [Display(Name = DisplayAchBFY.Isservice)]
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsContraception==true && (ContraceptionId_fk == 1 || ContraceptionId_fk == 2))")]
        public Nullable<bool> Isservice { get; set; }
        [Display(Name = DisplayAchBFY.ServiceRevcDt)]
        //[RequiredIf("Isservice", true)]
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsContraception==true && Isservice==true && (ContraceptionId_fk == 1 || ContraceptionId_fk == 2))")]
        public Nullable<System.DateTime> ServiceRevcDt { get; set; }
        [Display(Name = DisplayAchBFY.ServiceProvider)]
        //[RequiredIf("Isservice", true)]
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsContraception==true && Isservice==true && (ContraceptionId_fk == 1 || ContraceptionId_fk == 2))")]
        public Nullable<int> ServiceProvider { get; set; }

        //[RequiredIf("Isservice", true)]
        [Display(Name = DisplayAchBFY.Location)]
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsContraception==true && Isservice==true && (ContraceptionId_fk == 1 || ContraceptionId_fk == 2))")]
        public string Location { get; set; }

        //[RequiredIf("Isservice", true)]
        [Display(Name = DisplayAchBFY.AashaName)]
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsContraception==true && Isservice==true && (ContraceptionId_fk == 1 || ContraceptionId_fk == 2))")]
        public string AashaName { get; set; }

        //[Display(Name = DisplayAchBFY.CMEligible)]
        public Nullable<decimal> CMEligible { get; set; }
        public Nullable<decimal> CNRPEligible { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
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
        public int PlanApprove { get; set; }
        public Guid ReportedByUserId { get; set; }
    }
    public static class DisplayAchBFY
    {
        public const string ServiceYearId = "Year (वर्ष)";
        public const string ServiceMonthId = "Month (महीना)";
        public const string IsPPresent = "Availed contraception after meeting/HV (मीटिंग/एचवी के बाद गर्भनिरोधक का लाभ उठाया)";
        public const string IsFUpHV = "FollowUp/HV in Current Month (वर्तमान माह में फॉलोअप/एचवी)";
        public const string IsPPrIsCt = "Want to use contraception after meeting/HV (मीटिंग/एचवी के बाद गर्भनिरोधक का उपयोग करना चाहते हैं)";
        public const string Ct = "Method of contraception (गर्भनिरोधन की विधि)";
        public const string Ctusemethod = "Use method (साधन का चयन करें)";
        public const string CtusemethodOther = "Other use method (अन्य साधन का चयन करें)";
        public const string Isservice = "Linked to ASHA for service (सेवा के लिए आशा से जुड़े)";
        public const string ServiceRevcDt = "Service Received Date (सेवा प्राप्त होने की तिथि)";
        public const string ServiceProvider = "Service Provider (सेवा प्रदाता)";
        public const string Location = "Facility Name (Location) (सुविधा का नाम(स्थान))";
        public const string AashaName = "Aasha Name (आशा नाम)";
        public const string CMEligible = "CM Eligible for Incentive";//If Copper T/Antara inj/Permanent 20
        public const string CNRPEligible = "CNRP Eligible for Incentive";// 80
    }
}