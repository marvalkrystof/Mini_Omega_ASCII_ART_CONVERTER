using System;
using System.Collections.Generic;

using System.Text;

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

            menu.Add(new MenuItem("2.Settings", new Action(() =>
            {
                var m = MenuSettings();
                var item = m.Execute();
                item.Execute();
                
            })));
            menu.Add(new MenuItem("3.Run test examples", new Action(() =>
            {
                //todo
                
            })));

            menu.Add(new MenuItem("3.Exit ", new Action(() =>
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
            m.Add(new MenuItem("1. Choose Charset ", new Action(() =>
                {
                var m = MenuCharset();
                var item = m.Execute();
                item.Execute();
                })
            ));
             m.Add(new MenuItem("2.Change .txt output destination", new Action(() =>
                {
                //todo
                })
            ));
            m.Add(new MenuItem("3.Configure tcp server", new Action(() =>
                {
                //todo if going to implement
                })
            ));
            m.Add(new MenuItem("3.Save settings", new Action(() =>
                {
                //todo
                })
            ));
             m.Add(new MenuItem("2.Back ", new Action(() =>
                {
                
                })
            ));
            return m;
        }

   private Menu MenuCharset()
        {
            Menu m = new Menu("Charsets: ");
            m.Add(new MenuItem("1. Dark -> Light ", new Action(() =>
                {
                Output.Characters = "_.,-=+:;cba!?0123456789$W#@Ñ";
                })));
            m.Add(new MenuItem("2. Light -> Dark ", new Action(() =>
                {
                Output.Characters = "Ñ@#W$9876543210?!abc;:+=-,._";
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
        m.Add(new MenuItem("2.Text ", new Action(() =>
                {
                Output.TxtOut("TODO");
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