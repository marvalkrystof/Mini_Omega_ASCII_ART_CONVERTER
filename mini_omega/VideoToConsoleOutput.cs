using System;
using System.Threading;
// possibly replace characters instead of console.clear() might make it smoother?? https://stackoverflow.com/questions/5195692/is-there-a-way-to-delete-a-character-that-has-just-been-written-using-console-wr
namespace mini_omega
{
    public class VideoToConsoleOutput : IVideoOutputStrategy
    {

   public void Output(int width, double framerate)
        {

            for(int file=1;file<=Helper.getDirectoryFileCount(AppSetup.videoDir);file++) {
                        
                string fileName = AppSetup.separator+"Frame"+file+".png";        
                string filePath = AppSetup.videoDir + fileName;
                ASCIIPicture picture = new ASCIIPicture(filePath,width);
                picture.setOutput(new ConsoleOutput());
                picture.Output();
                int framerateCeiled = (int)Math.Ceiling(framerate);
                Thread.Sleep(1000/framerateCeiled);
                Console.Clear();
        }
          }
            }
    
                }       


