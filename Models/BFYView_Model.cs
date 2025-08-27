using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FP.Models
{
    public static class BFYView_Model
    {
        public static int HindiEng = 1;
        public static string DDis
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
        public static string DBlck
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
        public static string DCLF
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
                    CN = "समूह";
                }
                return CN;
            }
        }
        public static string DPanyt
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
        public static string DVO
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
        public static string HealthCD
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
        public static string DBFYVillage
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
        public static string ReportingDtD
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
        public static string Q1D
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
        public static string Q2D
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
        public static string Q3D
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
        public static string Q4D
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
        public static string Q4ABFYAgeD
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Age of Beneficiary";
                }
                else if (HindiEng == 2)
                {
                    CN = "लाभार्थी का उम्र";
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
        public static string Q5D
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
        public static string Q6_YearD
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
        public static string Q6D
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

        public static string Q7D
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
        public static string Q8D
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
        public static string Q9D
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
        public static string Q10D
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
        public static string Q11D
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
        public static string Q12D
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
        public static string Q12_1D
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
        public static string Q13D
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
        public static string Q14D
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
        public static string Q15D
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
        public static string Q16D
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
                    CN = "अस्थायी साधन";
                }
                return CN;
            }
        }
        public static string Q17D
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
                    CN = "स्थायी साधन";
                }
                return CN;
            }
        }
        public static string Q18D
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
                    CN = "अन्य साधन";
                }
                return CN;
            }
        }
        public static string Q19D
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
        public static string Q20D
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
        public static string Q21D
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
        public static string Reportedby
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Reported By";
                }
                else if (HindiEng == 2)
                {
                    CN = "";
                }
                return CN;
            }
        }
        public static string ReportedDt
        {
            get
            {
                string CN = string.Empty;
                if (HindiEng == 1)
                {
                    CN = "Reported Date";
                }
                else if (HindiEng == 2)
                {
                    CN = "";
                }
                return CN;
            }
        }

    }
}