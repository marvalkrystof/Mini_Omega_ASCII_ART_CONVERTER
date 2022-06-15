using System;
using System.Drawing;
using FFMpegCore;
using FFMpegCore.Enums;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using System.Text.RegularExpressions;

namespace mini_omega
{
    static class Helper
        {
             
            public static int Map(double measurement, double measurementMin, double measurementMax, double targetMin, double targetMax)
        {
            return (int)Math.Round((((measurement - measurementMin) / (measurementMax - measurementMin)) * (targetMax - targetMin)) + targetMin);

   

        }
        public static char MapPixelToChar(char[] chars, int pixel)
        {
            return chars[Map(pixel, 0, 255, 0, chars.Length - 1)];
        }


    public static Bitmap Scale(Bitmap original, int newWidth) {
        int height = original.Height;
        int width = original.Width;
        float aspectRatio = height / (float)width;
        int newHeight = (int)Math.Floor(newWidth*aspectRatio);
        if(newWidth % 2 != 0) {
            newWidth += 1;
        }

        if(newHeight % 2 != 0) {
            newHeight += 1;
        }

        Bitmap resized = new Bitmap(original,new Size(newWidth,newHeight));
        return resized;
    }
 
     public static void ConvertVideoToImages(string videoPath) {
        
   
    GlobalFFOptions.Configure(new FFOptions { BinaryFolder = "/usr/bin", TemporaryFilesFolder = "/tmp" }); //configuring ffmpeg location


    FFMpegArguments.FromFileInput(videoPath).OutputToFile(ConfigurationManager.AppSettings.Get("InstallationPath")+"/Video/Frame%d.png", true, 
    Options => { Options.WithVideoCodec(VideoCodec.Png); }).ProcessSynchronously();   

    }

    public static void ExtractControlFrame(string videoPath) {
      
    GlobalFFOptions.Configure(new FFOptions { BinaryFolder = "/usr/bin", TemporaryFilesFolder = "/tmp" }); //configuring ffmpeg location


    FFMpegArguments.FromFileInput(videoPath).OutputToFile(ConfigurationManager.AppSettings.Get("InstallationPath")+"/Video/Temp.png", true, 
    Options => { Options.WithVideoCodec(VideoCodec.Png); Options.WithFrameOutputCount(1); }).ProcessSynchronously();
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
   public static bool IsValidInput(string path) {
    string pattern = @"^https:\/\/www\.youtube\.com\/watch\?v=[a-zA-Z0-9-_]{11}$";
    string pattern2 = @"[a-zA-Z0-9-_]{11}";
    RegexOptions options = RegexOptions.Multiline;

           if(File.Exists(path)) {
            if(Path.GetExtension(path) == ".png" || 
            Path.GetExtension(path) == ".jpeg"
            || 
            Path.GetExtension(path) == ".jpg"
            || 
            Path.GetExtension(path) == ".mp4"
            || 
            Path.GetExtension(path) == ".gif"
            ) {
            return true;
            }
           
            else {
                Console.WriteLine("Not supported file type!");
            }
           }
           else if(Regex.Match(path,pattern,options).Success) {
                
                return true;
            }
           else if(Regex.Match(path,pattern2,options).Success) {
                
                return true;
           }
        else{
            Console.WriteLine("File doesnt exist or not a valid youtube link or video id! Check your path");
        }
        return false;
    }
      public static bool IsYoutubeVideo(string path) {
     string pattern = @"^https:\/\/www\.youtube\.com\/watch\?v=[a-zA-Z0-9-_]{11}$";
     string pattern2 = @"[a-zA-Z0-9-_]{11}";
     RegexOptions options = RegexOptions.Multiline;
     
     if(Regex.Match(path,pattern,options).Success) {
         return true;
     }
        else if(Regex.Match(path,pattern2,options).Success) {
            return true;
        }
      else {
         return false;
     }

   }
    public static bool isPicture(string path) {
            if(Path.GetExtension(path) == ".png" || 
            Path.GetExtension(path) == ".jpeg"
            || 
            Path.GetExtension(path) == ".jpg"
            ) {
                return true;
            } else {
                return false;
            }
    }
    public static bool isVideo(string path) {
         if(Path.GetExtension(path) == ".mp4" 
            || Path.GetExtension(path) == ".gif"
            ) {
                return true;
            } else {
                return false;
            }
    }

public static async Task DownloadYoutubeVideo(string url)
{   
  
      var youtube = new YoutubeClient();
    var video = await youtube.Videos.GetAsync(url);
        var streamManifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);

if(ConfigurationManager.AppSettings.Get("LastDownloadedYoutubeVideo") == video.Id.Value && File.Exists(ConfigurationManager.AppSettings.Get("InstallationPath") +AppSetup.separator+"ytvid.mp4")) {
    Console.WriteLine("Skipping download, already have this video in storage from last time!");
    return;
}
else if(File.Exists(ConfigurationManager.AppSettings.Get("InstallationPath") +AppSetup.separator+"ytvid.mp4")) {
      File.Delete(ConfigurationManager.AppSettings.Get("InstallationPath") +AppSetup.separator+"ytvid.mp4");
    }
  var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
    await youtube.Videos.Streams.DownloadAsync(streamInfo, ConfigurationManager.AppSettings.Get("InstallationPath")+AppSetup.separator+"ytvid.mp4");
    ConfigurationManager.AppSettings.Set("LastDownloadedYoutubeVideo",video.Id.Value);
    }

public static string PathInput() {
    string input = Console.ReadLine();
                 while(!Directory.Exists(input)) {
                    Console.WriteLine("The input path is invalid, enter another path");
                    input = Console.ReadLine();
                 }  
        if(input.EndsWith(AppSetup.separator)) {
            input = input.Remove(input.Length-1);
        }
        return input;
}

    }
 }


