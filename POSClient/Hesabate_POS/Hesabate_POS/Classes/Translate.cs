using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes
{
    public static  class Translate
    {
        public static string getResource(string key)
        {
            try
            {
                return GeneralInfoService.LanguageTerms.Where(x => x.id == key).FirstOrDefault().name;
            }
            catch { return ""; }
        }
    }
}
