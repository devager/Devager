namespace Devager
{
    using System;
    using Microsoft.Win32;
    using System.Windows.Forms;
    public class Reg
    {
        private bool _showError = false;

        public Reg(string productName)
        {
            this._subKey = "SOFTWARE\\" + productName;
        }

        public bool ShowError
        {
            get { return _showError; }
            set { _showError = value; }
        }

        private string _subKey { get; set; }

        public string SubKey
        {
            get { return _subKey; }
            set { _subKey = value; }
        }

        private RegistryKey _baseRegistryKey = Registry.LocalMachine;
        public RegistryKey BaseRegistryKey
        {
            get { return _baseRegistryKey; }
            set { _baseRegistryKey = value; }
        }


        public string Read(string keyName)
        {
            // Opening the registry key
            var rk = _baseRegistryKey;
            // Open a subKey as read-only
            var sk1 = rk.OpenSubKey(_subKey);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return null;
            }
            try
            {
                // If the RegistryKey exists I get its value
                // or null is returned.
                return (string)sk1.GetValue(keyName);
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Reading registry " + keyName);
                return null;
            }
        }

        public bool Write(string keyName, object value)
        {
            try
            {
                // Setting
                var rk = _baseRegistryKey;
                // I have to use CreateSubKey 
                // (create or open it if already exits), 
                // 'cause OpenSubKey open a subKey as read-only
                var sk1 = rk.CreateSubKey(_subKey);
                // Save the value
                sk1.SetValue(keyName, value);

                return true;
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Writing registry " + keyName);
                return false;
            }
        }
        public bool DeleteKey(string keyName)
        {
            try
            {
                // Setting
                var rk = _baseRegistryKey;
                var sk1 = rk.CreateSubKey(_subKey);
                // If the RegistrySubKey doesn't exists -> (true)
                if (sk1 == null)
                    return true;
                sk1.DeleteValue(keyName);

                return true;
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Deleting SubKey " + _subKey);
                return false;
            }
        }

        public bool DeleteSubKeyTree()
        {
            try
            {
                // Setting
                var rk = _baseRegistryKey;
                var sk1 = rk.OpenSubKey(_subKey);
                // If the RegistryKey exists, I delete it
                if (sk1 != null)
                    rk.DeleteSubKeyTree(_subKey);

                return true;
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Deleting SubKey " + _subKey);
                return false;
            }
        }

        public int SubKeyCount()
        {
            try
            {
                // Setting
                var rk = _baseRegistryKey;
                var sk1 = rk.OpenSubKey(_subKey);
                // If the RegistryKey exists...
                return sk1 != null ? sk1.SubKeyCount : 0;
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Retriving subkeys of " + _subKey);
                return 0;
            }
        }

        public int ValueCount()
        {
            try
            {
                // Setting
                var rk = _baseRegistryKey;
                var sk1 = rk.OpenSubKey(_subKey);
                // If the RegistryKey exists...
                return sk1 != null ? sk1.ValueCount : 0;
            }
            catch (Exception e)
            {
                ShowErrorMessage(e, "Retriving keys of " + _subKey);
                return 0;
            }
        }

        private void ShowErrorMessage(Exception e, string title)
        {
            if (_showError)
                MessageBox.Show(e.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
