using System;
using System.Drawing;
using System.IO;
namespace mini_omega
{
    public class Output
    {
         private static string characters = "_.,-=+:;cba!?0123456789$W#@Ñ";

        public static string Characters { get => characters; set => characters = value; }

        public static void ConsoleOut() {
          
         
            char[] chars = Characters.ToCharArray();
            Bitmap bitmap = new Bitmap("/home/krystof/Desktop/mini_omega/mini_omega/bin/Debug/netcoreapp3.1/man_black.jpg");
            int[,] array;
            
            
            array = Helper.GrayScaleBitmapToArray(Helper.ConvertToGrayScale(bitmap));

            char[,] asciiImage = Helper.PixelsArrayToChars(array, chars);
            
         
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
        public static void TxtOut(string filePath) {
        
        if(Path.GetExtension(filePath) == ".txt") {
   
      char[] chars = Characters.ToCharArray();
            Bitmap bitmap = new Bitmap("/home/krystof/Desktop/mini_omega/mini_omega/bin/Debug/netcoreapp3.1/man_black.jpg");
            int[,] array;
            array = Helper.GrayScaleBitmapToArray(Helper.ConvertToGrayScale(bitmap));
            char[,] asciiImage = Helper.PixelsArrayToChars(array, chars);
            
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