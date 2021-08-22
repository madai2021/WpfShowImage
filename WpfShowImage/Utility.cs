using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfShowImage
{
    class Utility
    {
        public static string OpenFile(string title, string filter)
        {
            string ret = string.Empty;
            var dialog = new OpenFileDialog()
            {
                Title = title,
                Filter = filter
            };

            if (dialog.ShowDialog() == true)
            {
                ret = dialog.FileName;
            }

            return ret;
        }
    }
}