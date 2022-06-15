using System;
using System.Configuration;

namespace mini_omega
{
    public class Test
    {
        public static void Run() {
            int numberOfFails = 0;
            ASCIIPicture picture = new ASCIIPicture("man_black.jpg",64);
            try {
           
            picture.setOutput(new ConsoleOutput());
            picture.Output();
            Console.WriteLine("Console image test done!");
            } catch(Exception e) {
                numberOfFails++;
                Console.WriteLine(e.Data);
            }
            try {
            picture.setOutput(new TextFileOutput());
            picture.Output();
            Console.WriteLine("Text file output done! Written to:" + ConfigurationManager.AppSettings.Get("TXToutputDestination"));
            } catch(Exception e) {
                numberOfFails++;
                Console.WriteLine(e.Data);
            }
            try {
            picture.setOutput(new HTMLOutput());
            picture.Output();
            Console.WriteLine("HTML file output done! Written to:" + ConfigurationManager.AppSettings.Get("HTMLoutputDestination"));
            } catch(Exception e) {
                numberOfFails++;
                Console.WriteLine(e.Data);
            }

            ASCIIVideo video = new ASCIIVideo("https://www.youtube.com/watch?v=IX8kWeZNW-A",64);
            
            try {
            video.setOutput(new VideoToConsoleOutput());
            video.Output();
            Console.WriteLine("Console video test done!");
            } catch(Exception e) {
                numberOfFails++;
                Console.WriteLine(e.Data);
            }
            try {
            video.setOutput(new VideoToMP4());
            video.Output();
            Console.WriteLine("MP4 file output done! Written to:" +ConfigurationManager.AppSettings.Get("MP4outputDestination"));
            } catch(Exception e) {
                numberOfFails++;
                Console.WriteLine(e.Data);
            }
            

                Console.WriteLine("Failed " +numberOfFails+"/5 tests!");
           }
    }
}