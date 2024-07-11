using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FP.Models
{
    public class CMFollowupModel
    {
        public CMFollowupModel()
        {
            FollowupID_pk = Guid.Empty;
        }
        [Display(Name = "District")]
        public string DistrictId { get; set; }
        [Display(Name = "Block")]
        public string BlockId { get; set; }
        [Display(Name = "Cluster")]
        public string CLFId { get; set; }
        [Display(Name = "Panchayat")]
        public string PanchayatId { get; set; }
        [Display(Name = "Village Organization")]
        public string VOId { get; set; }
        public string CMID { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        [Key]
        public System.Guid FollowupID_pk { get; set; }
        [Required]
        public Nullable<System.Guid> BFYID_fk { get; set; }
        [Display(Name = "Beneficiary ID")]
        public string BFYRegID { get; set; }
        [Required]
        [Display(Name = "Month")]
        public Nullable<int> FMonth { get; set; }
        [Required]
        [Display(Name = "Year")]
        public Nullable<int> FYear { get; set; }

        //[Required]
        [Display(Name = DisplayFollowBFY.IsFUpHV)]
        public Nullable<bool> IsFollowUp { get; set; }
        //[Required]
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsFollowUp == true)")]
        [Display(Name = DisplayFollowBFY.IsPPrIsCt)]
        public Nullable<bool> IsContraception { get; set; }
        [RequiredIf("IsContraception", true)]
        [Display(Name = DisplayFollowBFY.Ct)]
        public Nullable<int> ContraceptionId_fk { get; set; }
        [Display(Name = DisplayFollowBFY.CtusemethodOther)]
        //[RequiredIf("ContraceptionId_fk", 4)]
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsContraception==true && ContraceptionId_fk == 4)")]
        public string ContraceptionOther { get; set; }
        [ExpressiveAnnotations.Attributes.RequiredIf("(IsContraception==true && (ContraceptionId_fk == 1 || ContraceptionId_fk == 2))")]
        [Display(Name = DisplayFollowBFY.Ctusemethod)]
        public Nullable<int> UseMethodId_fk { get; set; }
        //[Required]
        [Display(Name = DisplayFollowBFY.ModuleRollout)]
        public Nullable<int> ModuleRollout { get; set; }
        //[Required]
        [Display(Name = DisplayFollowBFY.ModuleRolloutId_fk)]
        public Nullable<int> ModuleRolloutId_fk { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }

        [Display(Name = DisplayFollowBFY.Totalnoof_malechild)]
        [Required]
        public Nullable<int> Totalnoof_malechild { get; set; }
        [Required]
        [Display(Name = DisplayFollowBFY.Totalnoof_Femalechild)]
        public Nullable<int> Totalnoof_Femalechild { get; set; }
        public Nullable<int> Type { get; set; }
    }
    public  static class DisplayFollowBFY
    {
        public const string IsPPresent = "Present in Peer Group Meeting";
        public const string IsFUpHV = "Followup/HV in Current Month";
        public const string IsPPrIsCt = "Want to use contraception after meeting/HV";
        public const string Ct = "Method of contraception";
        public const string Ctusemethod = "Use method";
        public const string CtusemethodOther = "Other use method";
        public const string ModuleRollout = "Number of SHGs where module was rolled out";
        public const string ModuleRolloutId_fk = "Medium of module rollout";
        public const string Totalnoof_malechild = "No of male child at present";
        public const string Totalnoof_Femalechild = "No of female child at present";
        public const string BeneficiaryRegID = "Beneficiary ID";
    }
}