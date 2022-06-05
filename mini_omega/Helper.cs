using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using FFMpegCore;
using FFMpegCore.Enums;
using System.Configuration;
using System.IO;

namespace mini_omega
{
    static class Helper
        {
            public static int Map(double measurement, double measurementMin, double measurementMax, double targetMin, double targetMax)
        {
            return (int)Math.Round((((measurement - measurementMin) / (measurementMax - measurementMin)) * (targetMax - targetMin)) + targetMin);

   

        }
        public static int[,] GrayScaleBitmapToArray(Bitmap grayscale)
        {
            int[,] pixelArray = new int[grayscale.Width, grayscale.Height];
            for (int y = 0; y < grayscale.Height; y++)
            {
                for (int i = 0; i < grayscale.Width; i++)
                {

                    int pixel = grayscale.GetPixel(i, y).R;
                    pixelArray[i, y] = pixel;

                }
            }

            return pixelArray;

        }
        
        public static Bitmap ConvertToGrayScale(Bitmap original)
        {
            Bitmap grayScale = new Bitmap(original.Width, original.Height);
            for (int y = 0; y < original.Height; y++)
            {
                for (int i = 0; i < original.Width; i++)
                {
                    Color originalColor = original.GetPixel(i, y);
                    int red = originalColor.R;
                    int green = originalColor.G;
                    int blue = originalColor.B;
                    int gray = (red + green + blue) / 3;
                    grayScale.SetPixel(i, y, Color.FromArgb(gray, gray, gray));
                }
            }
            return grayScale;
        }

        public static char MapPixelToChar(char[] chars, int pixel)
        {
            return chars[Map(pixel, 0, 255, 0, chars.Length - 1)];
        }

        public static char[,] PixelsArrayToChars(int[,] pixels, char[] charsToMap)
        {
            char[,] asciiImage = new char[pixels.GetLength(0), pixels.GetLength(1)];
             for(int i= 0; i<pixels.GetLength(0);i++) 
            {
                for (int y = 0; y < pixels.GetLength(1); y++)
                {
                    asciiImage[i, y] = MapPixelToChar(charsToMap, pixels[i, y]);
                }

            }
            return asciiImage;
        }
    
     public static int[,] Compress(int[,] pixels) {
        int[,] compressedPixels = new int[pixels.GetLength(0)/2,pixels.GetLength(1)/2];

           for(int i= 1; i<pixels.GetLength(0);i+=2)
            {
                for (int y = 1; y < pixels.GetLength(1); y+=2)
                {
                   int pixel1 = pixels[i-1,y-1];
                   int pixel2 = pixels[i-1,y];
                   int pixel3 = pixels[i,y-1];
                   int pixel4 = pixels[i,y];

                   int outputPixel = (int)Math.Floor(((double)pixel1 + (double)pixel2 + (double)pixel3 + (double)pixel4) / 4.0);
                   compressedPixels[(i-1)/2,(y-1)/2] = outputPixel; 
                }

            }
        return compressedPixels;
        }
    public static int[,] Upscale(int[,] pixels) {
        int[,] upscaledPixels = new int[pixels.GetLength(0)*2,pixels.GetLength(1)*2];

        for(int i= 0; i<pixels.GetLength(0);i+=1)
            {
                for (int y = 0; y < pixels.GetLength(1); y+=1)
                {
                    int indexI = i*2;
                    int indexY = y*2;
                    int pixel = pixels[i,y];
                    upscaledPixels[indexI,indexY] = pixel;
                    upscaledPixels[indexI+1,indexY] = pixel;
                    upscaledPixels[indexI,indexY+1] = pixel;
                    upscaledPixels[indexI+1,indexY+1] = pixel;
                }
            }
        return upscaledPixels;
    }
 
     public static void ConvertVideoToImages(string videoPath) {
        
   
    GlobalFFOptions.Configure(new FFOptions { BinaryFolder = "/usr/bin", TemporaryFilesFolder = "/tmp" }); //configuring ffmpeg location


    FFMpegArguments.FromFileInput(videoPath).OutputToFile(AppContext.BaseDirectory+"Video/Frame%d.png", true, 
    Options => { Options.WithVideoCodec(VideoCodec.Png); }).ProcessSynchronously();   

    }

    public static void ExtractControlFrame(string videoPath) {
      
    GlobalFFOptions.Configure(new FFOptions { BinaryFolder = "/usr/bin", TemporaryFilesFolder = "/tmp" }); //configuring ffmpeg location


    FFMpegArguments.FromFileInput(videoPath).OutputToFile(AppContext.BaseDirectory+"Video/Temp.png", true, 
    Options => { Options.WithVideoCodec(VideoCodec.Png); Options.WithFrameOutputCount(1); }).ProcessSynchronously();
    }

    
    public static char[,] asciiArray(string filePath) {

            char[] chars = ConfigurationManager.AppSettings.Get("Characters").ToCharArray();
            Bitmap bitmap = new Bitmap(filePath);
            int[,] array;
            
            
            array = Helper.GrayScaleBitmapToArray(Helper.ConvertToGrayScale(bitmap));
            array = Helper.Compress(array);
            array = Helper.Compress(array);
              array = Helper.Compress(array);
            char[,] asciiImage = Helper.PixelsArrayToChars(array, chars);
            return asciiImage;
         
    }
    public static void deleteFilesInDirectory(string dirPath) {
    DirectoryInfo di = new DirectoryInfo(dirPath);
    
    foreach (FileInfo file in di.GetFiles())
    {
    file.Delete(); 
    }
    }
    
    public static bool isDirectoryEmpty(string dirPath) {
        DirectoryInfo di = new DirectoryInfo(dirPath);
        return di.GetFiles().GetLength(0) == 0;
    }

    public static bool sameFiles(string filePath1, string filePath2)  {
            FileInfo file = new FileInfo(filePath1);
            FileInfo file2 = new FileInfo(filePath2);

            return file.Length == file2.Length;

    }
 
    public static int getDirectoryFileCount(string dirPath) {
         DirectoryInfo di = new DirectoryInfo(dirPath);
         return di.GetFiles().Length;
    }


    }
}