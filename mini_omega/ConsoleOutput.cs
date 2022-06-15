using System;
using System.Configuration;
using System.IO;

namespace mini_omega
{
    public class ConsoleOutput : IOutputStrategy

    {


        public void Output(char[,] asciiImage)
        {
         
                for (int i = 0; i < asciiImage.GetLength(1); i++)
                {
            for (int y = 0; y < asciiImage.GetLength(0); y++)
            {   
                    Console.Write(asciiImage[y, i]);
                    Console.Write(" ");
                }
                Console.WriteLine(); 
               }
        }


        public static void VideoOutputInit() {
        string videoDir = ConfigurationManager.AppSettings.Get("InstallationPath") +AppSetup.separator+"Video";
        string videoPath = ConfigurationManager.AppSettings.Get("VIDpath");
       
        if(Helper.isDirectoryEmpty(videoDir)) {
            Helper.ConvertVideoToImages(videoPath);
        }
        else {         
         if(File.Exists(videoDir+"/Frame1.png")) {
            Helper.ExtractControlFrame(videoPath);
            if(!Helper.sameFiles(videoDir+"/Frame1.png",videoDir+"/Temp.png")) {
                Helper.ConvertVideoToImages(videoPath);
        }
            File.Delete(videoDir+"/Temp.png");
        }

        }
}

}

}