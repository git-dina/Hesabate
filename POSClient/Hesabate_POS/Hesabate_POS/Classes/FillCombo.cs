using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System;
using Hesabate_POS.Classes.ApiClasses;

namespace Hesabate_POS.Classes
{
    public class FillCombo
    {

        //static public string deliveryPermission = "setUserSetting_delivery";
        //static public string administrativeMessagesPermission = "setUserSetting_administrativeMessages";
        //static public string administrativePosTransfersPermission = "setUserSetting_administrativePosTransfers";

        #region language

        static public async Task fillLanguages(ComboBox combo)
        {
            if(GeneralInfoService.Languages == null)
                 await GeneralInfoService.GetLanguages();

            combo.ItemsSource = GeneralInfoService.Languages;
            combo.SelectedValuePath = "id";
            combo.DisplayMemberPath = "name";
            combo.SelectedIndex =0;

        }

        #endregion

        #region cashBox
        static public  void fillCashBoxes(ComboBox combo)
        {
            combo.ItemsSource = GeneralInfoService.cashBoxes;
            combo.SelectedValuePath = "BoxId";
            combo.DisplayMemberPath = "Name";
            combo.SelectedIndex = 0;

        }
        #endregion

        #region unit
        //static public  void fillUnits(ComboBox combo)
        //{
        //    combo.ItemsSource = GeneralInfoService.GeneralInfo.units;
        //    combo.SelectedValuePath = "Key";
        //    combo.DisplayMemberPath = "Value";
        //    combo.SelectedIndex = 0;

        //}
        #endregion

        #region externalType
        
        static public List<keyValueString> externalTypeList;
        static public List<keyValueString> RefreshExternalTypeList()
        {
            externalTypeList = new List<keyValueString> {
                new keyValueString { key = "0" ,  value = "-" },
                new keyValueString { key = "1" ,  value ="حفظ كفاتورة"},
                new keyValueString { key = "2" ,  value ="حفظ كفاتورة معلقة"},
                 };
            return externalTypeList;
        }
        static public void FillExternalType(ComboBox cmb)
        {
            #region fill ExternalType
            if (externalTypeList is null)
                RefreshExternalTypeList();
            cmb.SelectedValuePath = "key";
            cmb.DisplayMemberPath = "value";
            cmb.ItemsSource = externalTypeList;
            //cmb.SelectedIndex = 0;
            #endregion
        }
        #endregion

    }
}
