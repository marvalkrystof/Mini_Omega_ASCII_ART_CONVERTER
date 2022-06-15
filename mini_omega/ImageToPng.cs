using System.Configuration;
using System.Drawing;

namespace mini_omega
{
    public class ImageToPng : IOutputStrategy
    {

        string fileName;
        public ImageToPng (string fileName) {
            this.fileName = fileName;
        }
        public void Output(char[,] asciiImage)
        {
        string dirPath = AppSetup.asciiImagesDir;

        

        Bitmap bmp = new Bitmap(1000,1000);
           Graphics graphics = Graphics.FromImage(bmp);
           Font font = new Font(FontFamily.GenericMonospace, 32);
          graphics = Graphics.FromImage(bmp);

                    string txtWidth = "";
                    string txtHeight = "";
                      for (int i = 0; i < asciiImage.GetLength(0); i++)
                {
                     txtWidth = txtWidth + asciiImage[i,0];
                     txtWidth += " ";
                }
                    for (int i = 0; i < asciiImage.GetLength(1); i++)
                {
                     txtHeight = txtHeight + asciiImage[0,i];
                    txtHeight += "\n";
                    
                }
                
                 SizeF stringSizeWidth = graphics.MeasureString(txtWidth, font);
                 SizeF stringSizeHeight = graphics.MeasureString(txtHeight, font);

                 int newWidth = (int)stringSizeWidth.Width;
                 int newHeight = (int)stringSizeHeight.Height;


                     if(newWidth % 2 != 0) {
                    newWidth += 1;
                                        }

                    if(newHeight % 2 != 0) {
                    newHeight += 1;
                                            }

            bmp = new Bitmap(bmp, newWidth, newHeight);
            graphics = Graphics.FromImage(bmp);
            graphics.Clear(Color.Black);
                for (int i = 0; i < asciiImage.GetLength(1); i++)
                {
            for (int y = 0; y < asciiImage.GetLength(0); y++)
            {   
                float xPos = (stringSizeWidth.Width / asciiImage.GetLength(0)) * y;
                float yPos = (stringSizeHeight.Height /asciiImage.GetLength(1)) * i;
                graphics.DrawString(asciiImage[y,i].ToString(), font, Brushes.White, xPos, yPos);

                } 
        }            
            font.Dispose();
            graphics.Flush();
            graphics.Dispose();
        if(fileName == "output.mp4") {
             bmp.Save(ConfigurationManager.AppSettings.Get("PNGoutputDestination"),System.Drawing.Imaging.ImageFormat.Png);
        } 
        else {
            bmp.Save(dirPath+AppSetup.separator+$"output_"+fileName+".png",System.Drawing.Imaging.ImageFormat.Png);
        }    
        }

    
 
        }

    }