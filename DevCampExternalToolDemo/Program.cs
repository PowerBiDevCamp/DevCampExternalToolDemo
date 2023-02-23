namespace DevCampExternalToolDemo {

  internal static class Program {

    [STAThread]
    static void Main(string[] args) {

      // call to AppSettings to process start up parameters
      AppSettings.processStartupParameters(args);
      
      // continue with Windows Forms app initalization
      ApplicationConfiguration.Initialize();
      Application.Run(new FormMain());
    
    }
  }
}
