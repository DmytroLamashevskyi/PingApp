using IniParser.Model;
using IniParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PingApp.Model;
using System.IO;

namespace PingApp.Controllers
{
    public class SettingsController
    {
        private static readonly Lazy<SettingsController> lazy  = new Lazy<SettingsController>(() => new SettingsController()); 
        public static SettingsController Instance  => lazy.Value;


        public static string SettingsPath = ".\\Settings\\Settings.ini";

        public void LoadSettings()
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(SettingsPath);
            var listdata = data.Global.ToList<KeyData>();
            foreach (var d in listdata)
            { 
                PingManager.Instance.Add(new PingController(new PingData(d.KeyName, d.Value))); 
            }

            PingManager.Instance.InitCollection();
        }

        public void SaveSettings()
        {
            using (StreamWriter writer = new StreamWriter(SettingsPath))
            {
                foreach(var data in PingManager.Instance)
                {
                    writer.WriteLine($"{data.pingData.Name}={data.pingData.Ip}");
                }

            }
        }

        public void OpenLocation()
        {
            var process = System.Diagnostics.Process.Start("explorer.exe", SettingsPath);
            process.Exited += LoadProcessSettings;
        }

        private void LoadProcessSettings(object? sender, EventArgs e)
        {
            LoadSettings();
        }
    }
}
