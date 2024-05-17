using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapMe
{
    public static class SettingsData
    {

        // set the default directory to desktop
        public static string FolderDirectory { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

    }
}
