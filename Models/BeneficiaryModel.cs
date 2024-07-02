using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FP.Models
{
    public class BeneficiaryModel
    {
        public BeneficiaryModel()
        {
            Beneficiary_Id_pk = Guid.Empty;
        }
        public System.Guid Beneficiary_Id_pk { get; set; }

        [Display(Name = "Language")]
        public Nullable<int> HindiEng { get; set; }

        [Required]
        [Display(Name = "District")]
        public Nullable<int> DistrictId_fk { get; set; }

        [Required]
        [Display(Name = "Block")]
        public Nullable<int> BlockId_fk { get; set; }
        [Required]
        [Display(Name = "CLF")]
        public Nullable<int> CLFId_fk { get; set; }

        [Required]
        [Display(Name = "PanchayatId")]
        public Nullable<int> PanchayatId_fk { get; set; }

        [Required]
        [Display(Name = "Village Organization")]
        public Nullable<int> VillageOId_fk { get; set; }

        [Required]
        [Display(Name = "Reporting Month")]
        public Nullable<int> ReportingMonth { get; set; }

        [Required]
        [Display(Name = "Reporting Year")]
        public Nullable<int> ReportingYear { get; set; }

        [Required]
        [Display(Name = "Health Center")]
        public string HealthCenter { get; set; }

        [Required]
        [Display(Name = "Village Name")]
        public string BFYVillageName { get; set; }

        [Display(Name = "Beneficiary ID")]
        public string Q1 { get; set; }

        [Required]
        [Display(Name = "Name of SHG")]
        public string Q2 { get; set; }

        [Required]
        [Display(Name = "Name of Beneficiary")]
        public string Q3 { get; set; }

        [Required]
        [Display(Name = " ")]
        public int IsDOB { get; set; }

        [RequiredIf("IsDOB", 1)]
        [Display(Name = "Date Of Birth")]
        public Nullable<System.DateTime> BFYDOB { get; set; }

        [RequiredIf("IsDOB", 2)]
        [Display(Name = "Year of Birth")]
        public Nullable<int> BFYDOBYear { get; set; }

        [Display(Name = "Age of Beneficiary (In year/Month)")]
        public Nullable<double> Q4 { get; set; }

        [Required]
        [Display(Name = "Husband's name of Beneficiary")]
        public string Q5 { get; set; }

        [Required]
        [Display(Name = "Date of marriage / Year")]
        public Nullable<int> Q6DOMYear { get; set; }

        [RequiredIf("Q6DOMYear", 1)]
        [Display(Name = "Date of marriage")]
        public Nullable<System.DateTime> Q6 { get; set; }

        [RequiredIf("Q6DOMYear", 2)]
        public Nullable<int> Q6_Year { get; set; }

        [Required]
        [Display(Name = "Mobile No")]
        public string Q7 { get; set; }

        [Required]
        [Display(Name = "SHG Affiliation")]
        public Nullable<int> Q8 { get; set; }

        [RequiredIf("Q8", 2)]
        [Display(Name = "Beneficiary's family member name who is associated with the SHG group")]
        public string Q9 { get; set; }

        [Required]
        [Display(Name = "No of male child at present")]
        public Nullable<int> Q10 { get; set; }

        [Required]
        [Display(Name = "No of female child at present")]
        public Nullable<int> Q11 { get; set; }

        [Display(Name = "Age of youngest child (Year/months)")]
        public Nullable<double> Q12 { get; set; }

        [ExpressiveAnnotations.Attributes.RequiredIf("(Q12_1=='Boy' || Q12_1 == 'Girl')")]
        [Display(Name = "Date Of Birth (Youngest Child)")]
        //[GreaterThanOrEqualTo("Q12")]
        public Nullable<System.DateTime> YoungestDOB { get; set; }

        [Required]
        [Display(Name = "Youngest child gender (Boy/Girl)")]
        public string Q12_1 { get; set; }

        [Required]
        [Display(Name = "Code no of related AWC")]
        public string Q13 { get; set; }

        [Required]
        [Display(Name = "Name of related ASHA")]
        public string Q14 { get; set; }

        [Required]
        [Display(Name = "Which contraceptive method are you currently using")]
        public Nullable<int> Q15 { get; set; }

        [RequiredIf("Q15", 1)]
        [Display(Name = "Temporary Method")]
        public Nullable<int> Q16 { get; set; }

        [RequiredIf("Q15", 2)]
        [Display(Name = "Permanent Method")]
        public Nullable<int> Q17 { get; set; }

        [RequiredIf("Q15", 4)]
        [Display(Name = "Other Method")]
        public string Q18 { get; set; }

        public Nullable<int> Q19 { get; set; }

        [Required]
        [Display(Name = "Number of SHGs where module was rolled out")]
        public Nullable<int> Q20 { get; set; }

        [Required]
        [Display(Name = "Medium of module rollout")]
        public Nullable<int> Q21 { get; set; }

        public Nullable<bool> IsActive { get; set; }

        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }

        [Required]
        public bool IsPregnant { get; set; }

        public string DPregnant
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Is Beneficiary pregnant at present";
                }
                else if (HindiEng == 2)
                {
                    CN = "क्या लाभार्थी वर्तमान में गर्भवती है?";
                }
                return CN;
            }
        }
        public string DDis
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "District";
                }
                else if (HindiEng == 2)
                {
                    CN = "ज़िला";
                }
                return CN;
            }
        }
        public string DBlck
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Block";
                }
                else if (HindiEng == 2)
                {
                    CN = "प्रखंड";
                }
                return CN;
            }
        }
        public string DCLF
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Cluster";
                }
                else if (HindiEng == 2)
                {
                    CN = "क्लस्टर";
                }
                return CN;
            }
        }
        public string DPanyt
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Panchayat";
                }
                else if (HindiEng == 2)
                {
                    CN = "पंचायत";
                }
                return CN;
            }
        }
        public string DVO
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Name of village organization";
                }
                else if (HindiEng == 2)
                {
                    CN = "ग्राम संगठन का नाम";
                }
                return CN;
            }
        }
        public string HealthCD
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Health Center";
                }
                else if (HindiEng == 2)
                {
                    CN = "स्वास्थ्य केंद्र";
                }
                return CN;
            }
        }
        public string DBFYVillage
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Village Name";
                }
                else if (HindiEng == 2)
                {
                    CN = "गाँव";
                }
                return CN;
            }
        }
        public string ReportingDtD
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Reporting Month";
                }
                else if (HindiEng == 2)
                {
                    CN = "रिपोर्टिंग की माह";
                }
                return CN;
            }
        }
        public string Q1D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Beneficiary_Id";
                }
                else if (HindiEng == 2)
                {
                    CN = "लाभार्थी_आईडी";
                }
                return CN;
            }
        }
        public string Q2D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Name of SHG";
                }
                else if (HindiEng == 2)
                {
                    CN = "समूह का नाम";
                }
                return CN;
            }
        }
        public string Q3D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Name of Beneficiary";
                }
                else if (HindiEng == 2)
                {
                    CN = "लाभार्थी का नाम";
                }
                return CN;
            }
        }
        public string Q4D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Date of birth beneficiary";
                }
                else if (HindiEng == 2)
                {
                    CN = "लाभार्थी की जन्मतिथि";
                }
                //if (HindiEng == 1)
                //{
                //    CN = "Age of Beneficiary (In year/Month)";
                //}
                //else if (HindiEng == 2)
                //{
                //    CN = "लाभार्थी का उम्र (वर्ष में/माह)";
                //}
                return CN;
            }
        }
        public string Q5D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Husband's name of Beneficiary";
                }
                else if (HindiEng == 2)
                {
                    CN = "लाभार्थी के पति का नाम";
                }
                return CN;
            }
        }
        public string Q6_YearD
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Year";
                }
                else if (HindiEng == 2)
                {
                    CN = "लाभार्थी के शादी की वर्ष";
                }
                return CN;
            }
        }
        public string Q6D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Date of marriage";
                }
                else if (HindiEng == 2)
                {
                    CN = "लाभार्थी के शादी की तिथि";
                }
                return CN;
            }
        }

        public string Q7D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Mobile No";
                }
                else if (HindiEng == 2)
                {
                    CN = "लाभार्थी का मोबाईल नंबर";
                }
                return CN;
            }
        }
        public string Q8D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "SHG Affiliation";
                }
                else if (HindiEng == 2)
                {
                    CN = "समूह से जुड़ाव";
                }
                return CN;
            }
        }
        public string Q9D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Beneficiary's family member name who is associated with the SHG group";
                }
                else if (HindiEng == 2)
                {
                    CN = "लाभार्थी के परिवार का जो सदस्य समूह से जुड़ी हैं उस महिला का नाम लिखें";
                }
                return CN;
            }
        }
        public string Q10D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "No of male child at present";
                }
                else if (HindiEng == 2)
                {
                    CN = "वर्तमान में लड़कों की संख्या";
                }
                return CN;
            }
        }
        public string Q11D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "No of female child at present";
                }
                else if (HindiEng == 2)
                {
                    CN = "वर्तमान में लड़कियों की संख्या";
                }
                return CN;
            }
        }
        public string Q12D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Date of Birth (Youngest Child)";
                }
                else if (HindiEng == 2)
                {
                    CN = "सबसे छोटे बच्चे की जन्मतिथि";
                }
                //if (HindiEng == 1)
                //{
                //    CN = "Age of youngest child (Year/months)";
                //}
                //else if (HindiEng == 2)
                //{
                //    CN = "सबसे छोटे बच्चे की उम्र";
                //}
                return CN;
            }
        }
        //public string QDOBYearD
        //{
        //    get
        //    {
        //        string CN = string.Empty;
        //        if (HindiEng == 1)
        //        {
        //            CN = "Date of Birth (Youngest Child)";
        //        }
        //        else if (HindiEng == 2)
        //        {
        //            CN = "सबसे छोटे बच्चे की जन्मतिथि";
        //        }
        //        return CN;
        //    }
        //}
        public string Q12_1D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Gender of Youngest child (Boy/Girl)";//Youngest child gender
                }
                else if (HindiEng == 2)
                {
                    CN = "सबसे छोटे बच्चे का लिंग लड़का/लड़की";
                }
                return CN;
            }
        }
        public string Q13D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Code no of related AWC";
                }
                else if (HindiEng == 2)
                {
                    CN = "कोड संख्या। संबंधित आंगनवाड़ी केंद्र की";
                }
                return CN;
            }
        }
        public string Q14D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Name of related ASHA";
                }
                else if (HindiEng == 2)
                {
                    CN = "संबंधित आशा का नाम";
                }
                return CN;
            }
        }
        public string Q15D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Which contraceptive method are you currently using";
                }
                else if (HindiEng == 2)
                {
                    CN = "वर्त्तमान में कौन से गर्भनिरोधक साधन का उपयोग कर रहे हैं";
                }
                return CN;
            }
        }
        public string Q16D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Temporary Method";
                }
                else if (HindiEng == 2)
                {
                    CN = "अस्थायी विधि";
                }
                return CN;
            }
        }
        public string Q17D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Permanent Method";
                }
                else if (HindiEng == 2)
                {
                    CN = "अस्थायी विधि";
                }
                return CN;
            }
        }
        public string Q18D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Other Method";
                }
                else if (HindiEng == 2)
                {
                    CN = "अस्थायी विधि";
                }
                return CN;
            }
        }
        public string Q19D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Beneficiary followed-up in the reporting month";
                }
                else if (HindiEng == 2)
                {
                    CN = "रिपोर्टिंग माह लाभार्थी के यहाँ फॉलो अप किया गया";
                }
                return CN;
            }
        }
        public string Q20D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Number of SHGs where module was rolled out";
                }
                else if (HindiEng == 2)
                {
                    CN = "एसएचजी जहां मॉड्यूल शुरू किया गया था";
                }
                return CN;
            }
        }
        public string Q21D
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Medium of module rollout";
                }
                else if (HindiEng == 2)
                {
                    CN = "मॉड्यूल रोलआउट का माध्यम";
                }
                return CN;
            }
        }


    }
}