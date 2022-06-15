using FFMpegCore;
using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace mini_omega
{
    public class VideoToMP4 : IVideoOutputStrategy
    {
        public void Output(int width, double framerate)
        {   
            Console.WriteLine("Converting to ASCII! This action may take a lot of time, please wait!");

            string videoDir = AppSetup.videoDir;
            string dir = AppSetup.asciiImagesDir;
            ImageInfo[] images = new ImageInfo[Helper.getDirectoryFileCount(videoDir)-1];


            if(!Helper.isDirectoryEmpty(dir)) {
                ASCIIPicture ascii = new ASCIIPicture(videoDir+AppSetup.separator+"Frame1.png",width);
                ascii.setOutput(new ImageToPng("temp"));
                ascii.Output();
                if(Helper.sameFiles(dir + AppSetup.separator + $"output_temp.png", dir + AppSetup.separator + "output_1.png")) {
                    Console.WriteLine("Skipping conversion to ascii. The file is already converted in the installation directory");
                    File.Delete(dir+AppSetup.separator+"output_temp.png");
                } 
                else {
                    Helper.deleteFilesInDirectory(dir);
                       MultiThreadedAsciiToPng(width);

                }
            
            }
            else {

               MultiThreadedAsciiToPng(width);
            }           
            
            Console.WriteLine("Outputting images to mp4 file");           
            for(int i = 1; i<Helper.getDirectoryFileCount(dir); i++) {
                ImageInfo image = new ImageInfo(dir+AppSetup.separator+"output_"+i+".png");
                images[i-1] = image; 
            }
            
            
            FFMpeg.JoinImageSequence(ConfigurationManager.AppSettings.Get("MP4outputDestination"), frameRate: framerate, images);
            Console.WriteLine("Done!");            
        }
 
 

            private void MultiThreadedAsciiToPng(int width) {
                int outputsToMake = (int)Math.Ceiling(Helper.getDirectoryFileCount(AppSetup.videoDir) / 4.0);
             
                bool isDivisible = true;
                if(Helper.getDirectoryFileCount(AppSetup.videoDir) % 4.0 != 0) {
                    isDivisible = false;
                }
        
                    Thread thread1 = new Thread(() => this.AsciiToPngThread(1,outputsToMake,isDivisible,width));
                    Thread thread2 = new Thread(() => this.AsciiToPngThread(2,outputsToMake,isDivisible,width));
                    Thread thread3 = new Thread(() => this.AsciiToPngThread(3,outputsToMake,isDivisible,width));
                    Thread thread4 = new Thread(() => this.AsciiToPngThread(4,outputsToMake,isDivisible,width));


                    thread1.Start();
                    thread2.Start();
                    thread3.Start();
                    thread4.Start();
                    
                    thread1.Join();
                    thread2.Join();
                    thread3.Join();
                    thread4.Join();
                    
                               
                }
               
            private void AsciiToPngThread(int threadNumber,int outputsToMake, bool isDivisible, int width) {
                int minus = 0;
                if(!isDivisible && threadNumber == 4 ) {
                    minus = 1;
                }
                
                for(int i = 1+(outputsToMake*(threadNumber-1)); i<=outputsToMake*threadNumber - minus; i++) {
                ASCIIPicture ascii = new ASCIIPicture(AppSetup.videoDir+AppSetup.separator+"Frame"+i+".png",width);
                ascii.setOutput(new ImageToPng(i.ToString()));
                ascii.Output();
                
                Console.WriteLine("Frame " +i+ " was processed." );

            
            }

           
            }
 
           

        }
 
    }
