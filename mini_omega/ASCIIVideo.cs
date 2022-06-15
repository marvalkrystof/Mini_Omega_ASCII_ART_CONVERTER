using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using FFMpegCore;
namespace mini_omega
{
    public class ASCIIVideo : ASCII
    {
        string path;
        IVideoOutputStrategy output;
        int width;
        double framerate;

        public string Path { get => path; set 
         {
        
         if(Helper.isVideo(value)) {
             path = value;
         }
        else if (Helper.IsYoutubeVideo(value)) {
       try {
        Console.WriteLine("Downloading the youtube video, please wait");
        Task task = Task.Run(async() => {await Helper.DownloadYoutubeVideo(value); });
        task.Wait();
        path = ConfigurationManager.AppSettings.Get("InstallationPath") +AppSetup.separator+"ytvid.mp4";
       } 
       catch(YoutubeExplode.Exceptions.VideoUnavailableException) {
           Console.WriteLine("The video is unavailable, check the url/videoId");
           AppSetup.Start();
       }
       catch(YoutubeExplode.Exceptions.VideoRequiresPurchaseException) {
           Console.WriteLine("Video requires purchase");
            AppSetup.Start();
       }
       catch(YoutubeExplode.Exceptions.RequestLimitExceededException) {
           Console.WriteLine("Exceeded requests to youtube, try again later");
            AppSetup.Start();   
    }
        }
         else {
            throw new WrongInputFileFormatException("The input video isnt in a supported format, check supported formats in main menu: ", value);
         }
         } 
         }
     
        public ASCIIVideo(string path, int width) {
            Path = path;
            init();
            this.width = width;
            var mediaInfo = FFProbe.Analyse(Path);
            this.framerate = Helper.getDirectoryFileCount(AppSetup.videoDir)/mediaInfo.Duration.TotalSeconds;

        }
           public override void Output() {
            output.Output(width,framerate);
        }
        public override void setOutput(IVideoOutputStrategy output) {
                this.output = output;
            }

        private void init() {
        Console.WriteLine("Frame extraction, please wait!");
        if(Helper.isDirectoryEmpty(AppSetup.videoDir)) {
            Helper.ConvertVideoToImages(Path);
            Console.WriteLine("Extraction complete!");
        }
        else {         
         if(File.Exists(AppSetup.videoDir+AppSetup.separator+"Frame1.png")) {
            Helper.ExtractControlFrame(Path);
            if(!Helper.sameFiles(AppSetup.videoDir+AppSetup.separator+"Frame1.png",AppSetup.videoDir+AppSetup.separator+"Temp.png")) {
                Helper.deleteFilesInDirectory(AppSetup.videoDir);
                Helper.ConvertVideoToImages(Path);
                Console.WriteLine("Extraction complete!");
        }
        else {
            Console.WriteLine("The frames are already extracted, skipping extraction part!");
            File.Delete(AppSetup.videoDir+AppSetup.separator+"Temp.png");
        }
        }

        }
    }   
   }
}