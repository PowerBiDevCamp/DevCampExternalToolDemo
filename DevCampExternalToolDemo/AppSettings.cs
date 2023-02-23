  using DevCampExternalToolDemo.Properties;
using DevCampExternalToolDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCampExternalToolDemo {

  class AppSettings {

    private static Settings settings = new Settings();

    public static string Server {
      get { return settings.Server; }
      set {
        settings.Server = value;
        settings.Save();
      }
    }

    public static string Database {
      get { return settings.Database; }
      set {
        settings.Database = value;
        settings.Save();
      }
    }

    public static bool ExpandTableMembers {
      get { return settings.ExpandTableMembers; }
      set {
        settings.ExpandTableMembers = value;
        settings.Save();
      }
    }

    public static bool ShowHiddenDatasetObjects {
      get { return settings.ShowHiddenDatasetObjects; }
      set {
        settings.ShowHiddenDatasetObjects = value;
        settings.Save();
      }
    }

    public static void processStartupParameters(string[] args) {

      if (args.Length > 0) {
        if (args[0].ToLower().Contains("powerbi:")) {
          UserInteraction.PromptUserWithWarning("PBIX projects with Live Mode Datasets Not Supported.");
        }
        else {
          Server = args[0];
        }
      }
    
      if (args.Length > 1) {
        Database = args[1];
      }
    
    }

  }

}
