using System;
using System.Diagnostics;
using static System.Environment;

namespace tp_unikit.Helpers.Device
{
    public static class SystemHelper
    {
        /* :: Get an environment folder path for Windows environment folders. :: */
        public static string GetPath(SpecialFolder folder) {
            return Environment.GetFolderPath(folder);
        }

        public static void OpenPath(SpecialFolder folder) {
            Process.Start(GetPath(folder));
        }
    }
}
