using System;
using System.Configuration;
using System.IO;
namespace mini_omega
{
    class AppSetup
        {
    private delegate Menu startMenu();       
    public static char separator = Path.DirectorySeparatorChar;
    public static string videoDir = ConfigurationManager.AppSettings.Get("InstallationPath") +AppSetup.separator+"Video";
    public static string asciiImagesDir = ConfigurationManager.AppSettings.Get("InstallationPath") +AppSetup.separator+"ASCIIImages";

    public static void Start() {
        startMenu start;
        MenuConsole console = new MenuConsole();
        if(ConfigurationManager.AppSettings.Get("InstallationPath") == "" || !Directory.Exists(ConfigurationManager.AppSettings.Get("InstallationPath")))
            {
           start = console.InstallMenu;
            }
        else {
            if(!Directory.Exists(ConfigurationManager.AppSettings.Get("InstallationPath")+separator+"Video")) {
                Directory.CreateDirectory(ConfigurationManager.AppSettings.Get("InstallationPath")+separator+"Video");
            }
              if(!Directory.Exists(ConfigurationManager.AppSettings.Get("InstallationPath")+separator+"ASCIIImages")) {
                Directory.CreateDirectory(ConfigurationManager.AppSettings.Get("InstallationPath")+separator+"ASCIIImages");
            }
             start = console.MainMenu;
        }
        start();
    }
   
    public static void Init(string path) {
      Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
      var settings = config.AppSettings.Settings;
      settings["InstallationPath"].Value = path;
      settings["TXToutputDestination"].Value = path+separator+"output.txt"; 
      config.Save(ConfigurationSaveMode.Modified);
      ConfigurationManager.RefreshSection("appSettings");
      try{
      Directory.CreateDirectory(path);
      File.Create(path+separator+"output.txt");
      Directory.CreateDirectory(path+separator+"Video");
      Directory.CreateDirectory(path+separator+"ASCIIImages");
        } catch(UnauthorizedAccessException) {
        Console.WriteLine("You dont have permissions to write here, select another directory!");
        Start();
     }
     }
   }
}