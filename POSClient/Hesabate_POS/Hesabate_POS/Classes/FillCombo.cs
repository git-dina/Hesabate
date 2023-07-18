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

    }
}
