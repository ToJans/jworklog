using System;
using System.IO;
using System.Linq;
using System.Text;
using J = Jira.SDK;
using JSON = Newtonsoft.Json.JsonConvert;
using JSONFormat = Newtonsoft.Json.Formatting;

namespace jworklog
{

    public class ProgramSettings
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ServerUrl { get; set; }
        public string BaseJQL { get; set; }

        static readonly string ConfigFileName;
        public static readonly string CheckConfigMessage;

        static ProgramSettings()
        {
            var appdatapath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            ProgramSettings.ConfigFileName = Path.Combine(appdatapath, "jirasettings.json");
            ProgramSettings.CheckConfigMessage = "Please check your config file at :\n\t" + ProgramSettings.ConfigFileName;
        }

        public ProgramSettings()
        {
            this.Username = "jira username";
            this.Password = "jira password";
            this.ServerUrl = "http://somejiraurl.com";
            this.BaseJQL = "updated > -2w";
        }

        public static bool TryGetProgramSettings(out ProgramSettings settings)
        {
            if (File.Exists(ProgramSettings.ConfigFileName))
            {
                var json = File.ReadAllText(ProgramSettings.ConfigFileName, Encoding.UTF8);
                settings = JSON.DeserializeObject<ProgramSettings>(json);
                return true;
            }
            else
            {
                settings = new ProgramSettings();
                var json = JSON.SerializeObject(settings, JSONFormat.Indented);
                File.WriteAllText(ProgramSettings.ConfigFileName, json, Encoding.UTF8);
                return false;
            }
        }
    }

    class Program
    {
        static int Main(string[] args)
        {
            ProgramSettings settings = null;
            J.JiraClient jc = null;

            if (!ProgramSettings.TryGetProgramSettings(out settings))
            {
                Console.WriteLine(
                    "No config found; created one.\n" +
                    ProgramSettings.CheckConfigMessage);
                return -1;
            }
            else
            {
                Console.WriteLine("Querying Jira based on settings from config file.\n" +
                    ProgramSettings.CheckConfigMessage);
            }

            Console.WriteLine();

            try
            {
                jc = new J.JiraClient(settings.ServerUrl, settings.Username, settings.Password);

                var result = jc.SearchIssues(settings.BaseJQL)
                    .SelectMany(x => x.GetWorklogs().Select(y => new { Issue = x, Log = y }))
                    .GroupBy(x => new { 
                        Issue = x.Issue.Key, 
                        Author = x.Log.Author.Name,
                        Date = x.Log.Started.Date
                    })
                    .OrderBy(x => x.Key.Author)
                    .ThenBy(x=>x.Key.Date)
                    .ThenBy(x=>x.Key.Issue)
                    .Select(x => new {
                        x.Key.Author,
                        x.Key.Date,
                        x.Key.Issue,
                        HoursSpent = x.Sum(l => l.Log.TimeSpentSeconds) / 3600.0
                    });

                var output1 = result
                    .Select(x => string.Join("\t",
                        x.Author,
                        x.Date.ToShortDateString(),
                        x.Issue,
                        x.HoursSpent));
                    
                Console.WriteLine(String.Join("\n",output1));

                Console.WriteLine();
                Console.WriteLine();

                var result2 = result
                    .GroupBy(x=>new {x.Author,x.Date})
                    .Select(x => new { 
                        x.Key.Author,
                        x.Key.Date,
                        HoursSpent = x.Sum(y=>y.HoursSpent)
                    });

                var output2= result2
                    .Select(x => string.Join("\t",
                        x.Author,
                        x.Date.ToShortDateString(),
                        x.HoursSpent));
                    
                Console.WriteLine(String.Join("\n",output2));

                return 0;
            }
            catch (System.Net.WebException e)
            {
                Console.WriteLine(
                    "Unable to connect to your jira server\n" +
                    ProgramSettings.CheckConfigMessage);

                Console.WriteLine("More info:\n" + e.Message);

                return -2;
            }
        }
    }
}
