using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FP.Manager
{
    public static class Enums
    {
        public enum eAlertType
        {
            error = 0,
            success = 1,
            info = 2,
            warning = 3
        }
        public enum eState
        {
            Jharkhand = 20
        }
        public enum eContraceptive
        {
            [Description("Temporary")]
            Temporary = 1,
            [Description("Permanent")]
            Permanent = 2,
            [Description("No Method")]
            NoMethod = 3,
            [Description("Other Method")]
            OtherMethod = 4,
        }
        public enum eAmount
        {
            CMMonthly = 300, //Monthly Incentive per month Amount(300)
            CNRMonthly = 160, //Monthly Incentive per day fixed -10 days Amount(160 * 10)
            CMMobilization = 20, // Mobilization Incentives per beneficiary Service Adopted amount CM - Amount 20 distribution 20
            CNPMobilization = 80 // Mobilization Incentives per beneficiary Service Adopted amount CM - Amount 80 distribution 80
        }

        public enum eEnumExtension
        {
            [Description("image/*,application/pdf")]
            FileType = 1
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString(); //ToDo:Sunil, change to Enum.GetName() if if doesn't return the name of enum.
        }

        public enum eReturnReg
        {
            [Description("Record has been submitted successfully.")]
            Insert = 1,
            [Description("Record has been updated successfully.")]
            Update = 2,
            [Description("This record is already exists.")]
            Already = 3,
            [Description("Error")]
            Error = 4,
            [Description("Record Not Found.")]
            RecordNotFound = 5,

            [Description("Something went wrong while processing your request! Please try again later.")]
            ExceptionError = 6,
            [Description("All fields are mandatory !!.")]
            AllFieldsRequired = 7,
            [Description("Not Submit Data !!.")]
            NotSubmitData = 9,
            [Description("Please Select Vaild Month !!.")]
            VaildMonth = 10,
        }

        public enum eIsStatus
        {
            True = 1,
            False = 0
        }
        public enum eTypeLayer
        {
            [Description("MRP-CLF")]
            MRP = 1,
            [Description("AC")]
            CC = 2,
            [Description("BPIU")]//BPMU
            BPIU = 3,
        }
        public enum eTypeApprove
        {

            [Description("Approved")]
            Approve = 1,
            [Description("Rejected")]
            Reject = 2,
            [Description("Default")]
            Default = 3,
        }
        public enum eTypeOfPayment
        {
            [Description("MonthlyCNRP")]
            MonthlyCNRP = 1,
            [Description("MonthlyCM")]
            MonthlyCM = 2,
            [Description("MobilizationCMCNRP")]
            MobilizationCMCNRP = 3,
        }
        public enum ParticipantTypeValue
        {

            [Description("All")]
            All = 0,
            [Description("Male")]
            Male = 1,
            [Description("Female")]
            Female = 2,
            [Description("Male and Female")]
            MaleFemale = 3,
            [Description("Total")]
            Total = 4,
        }

        public enum QuarterTargetType
        {

            [Description("Numeric")]
            Numeric = 1,
            [Description("Boolean")]
            Boolean = 2,
        }
        public enum OptionYesNo
        {
            [Description("Yes")]
            Yes,
            [Description("No")]
            No,
        }
        public enum OptionRevision
        {
            [Description("Revised Request")]
            ReviseRequest=1,
            [Description("Revised")]
            Revised=2,
        }
        public enum OptionMailSubject
        {
            [Description("Member activity updation")]
            MemberUpdation = 1,
            [Description("SPMU Lead requested revision for ")]
            SPMULeadRevisionRequest = 2,
            [Description("Member activity updation revised")]
            MemberUpdationRevised = 3,
            [Description("SPMU Lead Review")]
            SPMULeadReview = 4,
            [Description("Finalize Weekly Report")]
            SPMULeadFinalize = 5,
            [Description("Requested For Compiled Lead Revision")]
            CompiledLeadRevisionRequest = 6,
        }
        public enum OptionMailBody
        {
            [Description("Updated the activity")]
            MemberUpdation = 1,
            [Description("SPMU Lead requested resivion for ")]
            SPMULeadRevisionRequest = 2,
            [Description("Ativity has been revised by SPMU Lead")]
            MemberUpdationRevised = 3,
            [Description("SMPU Lead Review Activity")]
            SPMULeadReview = 4,
            [Description("Finalize Weekly Report")]
            SPMULeadFinalize = 5,
            [Description("Requested For Compiled Lead Revision")]
            CompiledLeadRevisionRequest = 6,
        }
    }
}
