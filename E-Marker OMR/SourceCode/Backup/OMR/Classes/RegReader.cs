using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace OMR
{
    public class RegistryEditor
    {
        public static void WriteRegStr(string AppName, string ValueName, object value)
        {
            RegistryKey MyKey = Registry.CurrentUser.CreateSubKey("Software\\" + AppName, RegistryKeyPermissionCheck.ReadWriteSubTree);
            try
            {
                MyKey.SetValue(ValueName, Convert.ToInt32(value), RegistryValueKind.DWord);
            }
            catch
            {
                MyKey.SetValue(ValueName, value.ToString(), RegistryValueKind.String);
            }
        }
        public static void DeleteReg(string AppName, string ValueName)
        {
            RegistryKey MyKey = Registry.CurrentUser.CreateSubKey("Software\\" + AppName, RegistryKeyPermissionCheck.ReadWriteSubTree);
            try
            {
                MyKey.DeleteValue(ValueName);
            }
            catch
            {
            }
        }
        public static object ReadReg(string AppName, string ValueName)
        {
            RegistryKey MyKey;
            try
            {
                MyKey = Registry.CurrentUser.OpenSubKey("Software\\" + AppName, RegistryKeyPermissionCheck.ReadWriteSubTree);
                if (MyKey == null)
                    throw new NullReferenceException();
            }
            catch
            {
                MyKey = Registry.CurrentUser.CreateSubKey("Software\\" + AppName, RegistryKeyPermissionCheck.ReadWriteSubTree);
            }
            try
            {
                object obj = MyKey.GetValue(ValueName) ;
                if (obj == null)
                    throw new Exception();
                return obj;
            }
            catch
            {
                WriteRegStr(AppName, ValueName, false);
                object obj = MyKey.GetValue(ValueName);
                return obj;
            }
        }
    }
}
