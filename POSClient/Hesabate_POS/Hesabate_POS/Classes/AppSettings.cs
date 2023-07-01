using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
//using System.Deployment.Application;
using System.Reflection;

namespace Hesabate_POS.Classes
{
    public class AppSettings
    {

        public static ResourceManager resourcemanager;
        public static ResourceManager resourcemanagerreport;
        public static ResourceManager resourcemanagerAr;
        public static ResourceManager resourcemanagerEn;

       
        public static string APIUri = "http://localhost:7473/api/";


        #region folders Paths
        public const string TMPFolder = "Thumb";
        public const string TMPSupFolder = "Thumb/SupDocuments";
        public const string TMPSettingFolder = "Thumb/setting"; // folder to save Logo photos locally 
        #endregion

        //general info
        internal static string accuracy = "3";
        internal static string currency = "د.ك";
        public static string dateFormat = "ShortDatePattern";

        #region company info
        //default system info
        internal static string companyName;
        internal static string companyNameAr;
        internal static string companyAddress;

        internal static string companyEmail;
        internal static string companyMobile;
        internal static string companyPhone;
        internal static string companyFax;
        internal static string companylogoImage;
        #endregion

    
        // app version
        //static public string CurrentVersion
        //{
        //    get
        //    {
        //        return ApplicationDeployment.IsNetworkDeployed
        //               ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
        //               : Assembly.GetExecutingAssembly().GetName().Version.ToString();
        //    }
        //}

        //public static string lang = "ar";
        public static string lang = "ar";


    }
}
