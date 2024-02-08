using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
//using System.Deployment.Application;
using System.Reflection;
using System.Net.Http;

namespace Hesabate_POS.Classes
{
    public class AppSettings
    {
        public static readonly HttpClient httpClient = new HttpClient()
        {
            Timeout = TimeSpan.FromMinutes(3),
            
    };

        public static ResourceManager resourcemanager;
        public static ResourceManager resourcemanagerreport;
        public static ResourceManager resourcemanagerAr;
        public static ResourceManager resourcemanagerEn;

       
        public static string APIUri ;
        public static bool menuState = false;

        #region folders Paths
        public const string TMPFolder = "Thumb";
        public const string ItemsImgPath = "Thumb/Items"; // folder to save items photos locally 
        public const string TMPSettingFolder = "Thumb/setting"; // folder to save Logo photos locally 
        #endregion

        //general info
        internal static string accuracy = "3";
        internal static string currency = "TL";
        internal static int MainCurrency ;
        public static string dateFormat = "ShortDatePattern";



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
        #region language
        public static string lang = "ar";
        public static int langId ;
        public static string dir = "rtl";
        #endregion
        // small, big
        public static string invoiceDetailsType = "small";

        #region User Info
        public static string loginName; // login Name
        public static string userId;//رقم المستخدم على النظام
        public static string userName;//اسم المستخدم على النظام
        public static string database_id;//رقم قاعدة البيانات التي يعمل عليها النظام في برنامج المحاسبة
        public static string cashBoxId;//رقم الصندوق و في حال كان الرقم 0 يجب ان يختار واحد من الصناديق الملحقة و اذا كان الرقم اكبر من صفر يتم الاختيار بشكل آلي ولا يسمح للمستخدم بالاختيار
        public static string token;//يتم استخدامها في كل الاوامر اللاحقة بحيث تكون مفتاح لقبول العمل على النظام
        #endregion

        #region Operation Permissions
        public static string handhold_out;//مسموح استخدام سند دفع
        public static string handhold_in;////مسموح استخدام سند القبض
        public static string handhold;//مسموح استخدام سند الصرف
        public static string items_page;//مسموح استخدام بطاقة الاصناف --حاليا غير مستخدمه
        public static string reservation;//مسموح فتح الحجوزات
        public static string customer_report;//مسموح استخدام كشف حساب العميل
        public static string convert;//مسموح استخدام كبسة التحويل : وهي كبسة تقوم بترحيل العمليات الى السيرفر أو استقبال بيانات من السيرفر             
        public static string showPx;//يجب اظهار شاشة  تسليم العهده للمستخدم قبل العمل على البرنامج      
        #endregion

        #region options

        #endregion

        #region appSettings

        #endregion


    }
}
