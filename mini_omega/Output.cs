using System;
using System.Drawing;
using System.IO;
using System.Configuration;
using System.Threading;


namespace mini_omega
{
//youtube api    
    public class Output
    {
        public static void ConsoleOut() {
          string imgPath = ConfigurationManager.AppSettings.Get("IMGpath");
          
          char[,] asciiImage = Helper.asciiArray(imgPath);
         
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
        public static void TxtOut() {
        
        string imgPath = ConfigurationManager.AppSettings.Get("IMGpath");
        string filePath = ConfigurationManager.AppSettings.Get("TXToutputDestination");
        if(Path.GetExtension(filePath) == ".txt") {
   
        char[,] asciiImage = Helper.asciiArray(imgPath);     
        using (StreamWriter writer = new StreamWriter(filePath))  
        {  
                for (int i = 0; i < asciiImage.GetLength(1); i++)
                {
            for (int y = 0; y < asciiImage.GetLength(0); y++)
            {   
                    writer.Write(asciiImage[y, i]);
                    writer.Write(" ");
            

                }
                writer.WriteLine(); 
        }
        }              
        }
        else {
            Console.WriteLine("Destination file is not .txt");
        }

        }

    public static void VideoOut() {
        string videoDir = AppContext.BaseDirectory +"/Video";

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
       
               for(int file=1;file<=Helper.getDirectoryFileCount(videoDir);file++) {
                string fileName = "/Frame"+file+".png";        
                string filePath = videoDir + fileName;
                char[,] asciiImage = Helper.asciiArray(filePath);  
              
                    for (int i = 0; i < asciiImage.GetLength(1); i++)
                        {
                            for (int y = 0; y < asciiImage.GetLength(0); y++)
                                            {   
                                Console.Write(asciiImage[y, i]);
                                Console.Write(" ");
            

                                            }
                Console.WriteLine(); 

        }
        Thread.Sleep(1000);
        Console.Clear();
    }
    }
    }

}