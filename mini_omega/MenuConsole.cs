using System;
using System.Collections.Generic;
using System.Configuration;

namespace mini_omega
{
    class MenuConsole
    {
        private bool exit = false;

        public void MainMenu()
        {
            Menu menu = new Menu("Please select one option: ");
       
           
            menu.Add(new MenuItem("1.Begin", new Action(() =>
            {
                var m = MenuApp();
                var item = m.Execute();
                item.Execute();
            })));
            menu.Add(new MenuItem("2.Change image path. Current image path: "+ConfigurationManager.AppSettings.Get("IMGpath"), new Action(() =>
            {
            //todo check if file is png or jpg
            Console.WriteLine("Enter path:");
            string path = Console.ReadLine();                
            ConfigurationManager.AppSettings.Set("IMGpath",path);
            })));
            
            menu.Add(new MenuItem("3.App Settings", new Action(() =>
            {
                var m = MenuSettings();
                var item = m.Execute();
                item.Execute();
                
            })));
              menu.Add(new MenuItem("4.Supported file types", new Action(() =>
            {
                Console.WriteLine("-------------------");
                Console.WriteLine("Images: jpg/jpeg,png");
                Console.WriteLine("TXT output: txt");
                Console.WriteLine("-------------------");
                
            })));  
            menu.Add(new MenuItem("5.Run test examples", new Action(() =>
            {
                //todo
                
            })));

            menu.Add(new MenuItem("6.Exit ", new Action(() =>
            {
                exit = true;
            })));


            while (!exit)
            {
                var item = menu.Execute();
                item.Execute();
            }

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
                //todo
                })
            ));
             m.Add(new MenuItem("3.Back ", new Action(() =>
                {
                
                })
            ));
            return m;
        }

   private Menu MenuCharset()
        {
            Menu m = new Menu("Charsets: ");
            m.Add(new MenuItem("_.,-=+:;cba!?0123456789$W#@Ñ", new Action(() =>
                {
                ConfigurationManager.AppSettings.Set("Characters","_.,-=+:;cba!?0123456789$W#@Ñ");
                })));
            m.Add(new MenuItem("Ñ@#W$9876543210?!abc;:+=-,._", new Action(() =>
                {
                ConfigurationManager.AppSettings.Set("Characters","Ñ@#W$9876543210?!abc;:+=-,._");
                })));
                m.Add(new MenuItem("3.Back ", new Action(() =>
                {
                
                })
            ));
            return m;
        }      


    private Menu MenuApp() {
        Menu m = new Menu("Select your output option: ");
        m.Add(new MenuItem("1.Console ", new Action(() =>
                {
                Output.ConsoleOut();
                })
            ));
        m.Add(new MenuItem("2.Text. Destination file: "+ConfigurationManager.AppSettings.Get("TXToutputDestination"), new Action(() =>
                {
                Output.TxtOut();
                })
            ));
            m.Add(new MenuItem("3.Back ", new Action(() =>
                {
                
                })
            ));
 
        return m;
 
    }    
    }

}