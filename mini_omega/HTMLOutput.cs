using System;
using System.Configuration;
using System.IO;

namespace mini_omega
{
    //add styles for better presentation
    class HTMLOutput : IOutputStrategy
    {
        public void Output(char[,] asciiImage)
        {
        string filePath = ConfigurationManager.AppSettings.Get("HTMLoutputDestination");
        if(Path.GetExtension(filePath) == ".html") {
        
        using (StreamWriter writer = new StreamWriter(filePath))  
        {  
                for (int i = 0; i < asciiImage.GetLength(1); i++)
                {
                writer.Write("<p style=\"font-family:Courier New\"> ");
            for (int y = 0; y < asciiImage.GetLength(0); y++)
            {   

                if(asciiImage[y,i] == ' ') {
                 writer.Write("&nbsp");
                    writer.Write("&nbsp");
                } else { 
                    writer.Write(asciiImage[y, i]);
                    writer.Write("&nbsp");
                }
                }
                writer.Write(" </p>");
                writer.WriteLine(); 
        }
        }              
        }
        else {
            Console.WriteLine("Destination file is not .html");
        }
 
        }
    }
}