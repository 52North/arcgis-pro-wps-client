using AgpWps.Model.Services;
using System;
using System.IO;

namespace AgpWps.Client
{
    public class AppData : IAppData
    {
        public string DataDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "ArcGisProWpsAddIn");

        public AppData()
        {
            if(!Directory.Exists(DataDirectory))
            {
                Directory.CreateDirectory(DataDirectory);
            }
        }

    }
}
