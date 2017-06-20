using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace DeskNotes
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        string filePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\settings.json";

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    string json = r.ReadToEnd();
                    Settings.Pages = JsonConvert.DeserializeObject<List<Page>>(json);
                }
            }

            if(Settings.PageCount == 0)
            {
                Page p = new Page();
                p.backColor = "";
                p.titleColor = "";
                p.title = "Notes - 1";
                p.pageNumber = 1;
                p.content = "";
                p.location = Settings.DefaultLocation;
                p.size = new System.Drawing.Size(300, 400);
                Settings.Pages = new List<Page>();
                Settings.Pages.Add(p);
                string json = JsonConvert.SerializeObject(Settings.Pages);
                File.WriteAllText(filePath, json);
            }

            Main m = new DeskNotes.Main();
            m.Show();

            foreach(var page in Settings.Pages)
            {
                Note note = new Note(page.pageNumber);
                note.Show();
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (File.Exists(filePath)) File.Delete(filePath);

            string json = JsonConvert.SerializeObject(Settings.Pages);
            File.WriteAllText(filePath, json);
        }
    }
}
