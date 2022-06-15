using System;
using System.Configuration;
using System.IO;

namespace mini_omega
{
    public class TextFileOutput : IOutputStrategy
    {
        public void Output(char[,] asciiImage)
        {
        string filePath = ConfigurationManager.AppSettings.Get("TXToutputDestination");
        if(Path.GetExtension(filePath) == ".txt") {
        
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
    }
}