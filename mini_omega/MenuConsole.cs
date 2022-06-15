using System;
using System.Configuration;

namespace mini_omega
{
    class MenuConsole
    {
        private bool exit = false;
        ASCII ascii;

        public Menu MainMenu()
        {
            Menu menu = new Menu("Please select one option: ");
       
            menu.Add(new MenuItem("1.Begin", new Action(() =>
            {
                var m = MenuPath();
                var item = m.Execute();
                item.Execute();         
            })));
            menu.Add(new MenuItem("2.App Settings", new Action(() =>
            {
                var m = MenuSettings();
                var item = m.Execute();
                item.Execute();
                
            })));
              menu.Add(new MenuItem("3.Supported file types", new Action(() =>
            {
                Console.WriteLine("-------------------");
                Console.WriteLine("Image: jpg/jpeg,png");
                Console.WriteLine("Video: youtube url,youtube video id, gif, mp4");
                Console.WriteLine("TXT output: txt");
                Console.WriteLine("HTML output: html");
                Console.WriteLine("-------------------");
                
            })));  
            menu.Add(new MenuItem("4.Run test examples", new Action(() =>
            {
            Test.Run();   
            })));

            menu.Add(new MenuItem("5.Exit ", new Action(() =>
            {
                exit = true;
            })));
            while (!exit)
            {
                var item = menu.Execute();
                item.Execute();
            }
            return menu;
        }

        private Menu MenuSettings()
        {
            Menu m = new Menu("Settings: ");
            m.Add(new MenuItem("1. Choose Charset. Current charset: "+ConfigurationManager.AppSettings.Get("Characters"), new Action(() =>
                {
                var m = MenuCharset();
                var item = m.Execute();
                item.Execute();
                })
            ));
             m.Add(new MenuItem("2.Change .txt output destination. Current destination:"+ConfigurationManager.AppSettings.Get("TXToutputDestination"), new Action(() =>
                {
                    Console.WriteLine("Enter a directory you want the output.txt to end up!");
                    string input = Helper.PathInput();
                    ConfigurationManager.AppSettings.Set("TXToutputDestination",input+AppSetup.separator+"output.txt");
                })
            ));
            m.Add(new MenuItem("3.Change .html output destination. Current destination:"+ConfigurationManager.AppSettings.Get("HTMLoutputDestination"), new Action(() =>
                {
                    Console.WriteLine("Enter a directory you want the output.html to end up!");
                    string input = Helper.PathInput();
                    ConfigurationManager.AppSettings.Set("HTMLoutputDestination",input+AppSetup.separator+"output.html");
                })
            ));
            m.Add(new MenuItem("4.Change .mp4 output destination. Current destination:"+ConfigurationManager.AppSettings.Get("MP4outputDestination"), new Action(() =>
                {
                    Console.WriteLine("Enter a directory you want the output.mp4 to end up!");
                    string input = Helper.PathInput();
                    ConfigurationManager.AppSettings.Set("MP4outputDestination",input+AppSetup.separator+"output.mp4");
                })
            ));
             m.Add(new MenuItem("5.Change .mp4 output destination. Current destination:"+ConfigurationManager.AppSettings.Get("PNGoutputDestination"), new Action(() =>
                {
                    Console.WriteLine("Enter a directory you want the output.png to end up!");
                    string input = Helper.PathInput();
                    ConfigurationManager.AppSettings.Set("PNGoutputDestination",input+AppSetup.separator+"output.png");
                })
            ));
             m.Add(new MenuItem("6.Back ", new Action(() =>
                {
                
                })
            ));
            return m;
        }

   private Menu MenuCharset()
        {
            Menu m = new Menu("Charsets: ");
            m.Add(new MenuItem("1._.,-=+:;cba!?0123456789$W#@Ñ", new Action(() =>
                {
                ConfigurationManager.AppSettings.Set("Characters","_.,-=+:;cba!?0123456789$W#@Ñ");
                })));
            m.Add(new MenuItem("2.Ñ@#W$9876543210?!abc;:+=-,._", new Action(() =>
                {
                ConfigurationManager.AppSettings.Set("Characters","Ñ@#W$9876543210?!abc;:+=-,._");
                })));
            m.Add(new MenuItem("3.Set custom characters", new Action(() =>
                {
                Console.WriteLine("Write out your custom character string: ");
                string customChars = Console.ReadLine();
                ConfigurationManager.AppSettings.Set("Characters",customChars);
                })));
                m.Add(new MenuItem("4.Back ", new Action(() =>
                {
                
                })
            ));
            return m;
        }      
    public  Menu InstallMenu() {
        Console.WriteLine("Please insert the install directory path:");
        string path = Console.ReadLine();    
        AppSetup.Init(path);
        return MainMenu();
        }
    

    private Menu MenuImageOutput() {
        bool back = false;
        Menu m = new Menu("Select your output option: ");
        m.Add(new MenuItem("1.Console ", new Action(() =>
                {
                    ascii.setOutput(new ConsoleOutput());
                    ascii.Output();
                })
            ));
        m.Add(new MenuItem("2.Text/ HTML", new Action(() =>
                {
                var m = MenuImageTextOutput();
                var item = m.Execute();
                item.Execute();
                })
            ));
            m.Add(new MenuItem("3.PNG", new Action(() =>
                {
                ascii.setOutput(new ImageToPng("output.png"));
                ascii.Output();
                })
            ));
            m.Add(new MenuItem("3.Back ", new Action(() =>
                {
                ascii = null;                
                })
            ));
            return m;
           }
 
        private Menu MenuImageTextOutput() {
            
        Menu m = new Menu("Select your output option:");
        m.Add(new MenuItem("1.Plain text: ", new Action(() =>
                {
                    ascii.setOutput(new TextFileOutput());
                    ascii.Output();
                })
            ));
        m.Add(new MenuItem("2.HTML: ", new Action(() =>
                {
                    ascii.setOutput(new HTMLOutput());
                    ascii.Output();
                })
            ));
            m.Add(new MenuItem("3.Back ", new Action(() =>
                {
                
                })
            ));
          
            return m;
           }
        

            private Menu MenuVideoOutput() {
        bool back = false;
        Menu m = new Menu("Select your output option: ");
        m.Add(new MenuItem("1.Console ", new Action(() =>
                {
                    ascii.setOutput(new VideoToConsoleOutput());
                    ascii.Output();
                })
            ));
            m.Add(new MenuItem("2.MP4 ", new Action(() =>
                {
                    ascii.setOutput(new VideoToMP4());
                    ascii.Output();
                })
            ));
        
            m.Add(new MenuItem("3.Back ", new Action(() =>
                {
                ascii = null;
                
                })
            ));
            return m;
           }

    private Menu MenuPath() {
            bool isVideo = false;
            Console.WriteLine("Enter path to the image/video:");
            string path = Console.ReadLine();   

            while(!Helper.IsValidInput(path)){
                path = Console.ReadLine();
            }
            if(Helper.isVideo(path) || Helper.IsYoutubeVideo(path)) {
                isVideo = true;
            }
            Console.WriteLine("Enter desired output width (number of pixels per vertical line):");
            Console.WriteLine("Warning! Higher widths may look destructed in the console output. Highly recommend using widths up to 64");
            string input = Console.ReadLine();
            int width;
            while (!int.TryParse(input, out width))
            {
            Console.WriteLine("Your input is not an integer");
            input = Console.ReadLine();   
            }           
            if(!isVideo) {
            ascii = new ASCIIPicture(path,width);
            return MenuImageOutput();
            } else {
                ascii = new ASCIIVideo(path,width);
                return MenuVideoOutput();
            }
        }    

    }

}