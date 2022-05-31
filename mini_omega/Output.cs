using System;
using System.Drawing;

namespace mini_omega
{
    public class Output
    {
        public static void ConsoleOut() {
          
            string characters = "_.,-=+:;cba!?0123456789$W#@Ñ";
     
         
            char[] chars = characters.ToCharArray();
            Bitmap bitmap = new Bitmap("/home/krystof/Desktop/mini_omega/mini_omega/bin/Debug/netcoreapp3.1/man_black.jpg");
            int[,] array;
            
            
            array = Helper.GrayScaleBitmapToArray(Helper.ConvertToGrayScale(bitmap));
            array = Helper.Compress(array);

            char[,] asciiImage = Helper.PixelsArrayToChars(array, chars);
            for (int y = 0; y < asciiImage.GetLength(0); y++)
            {
                for (int i = 0; i < asciiImage.GetLength(1); i++)
                {
                    Console.Write(asciiImage[i, y]);
                    Console.Write(" ");
            

                }
                Console.WriteLine();
            }

        }
        public void TxtOut() {
            //TODO
        }
 
    }
}