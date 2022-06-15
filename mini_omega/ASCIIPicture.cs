using System.Configuration;
using System.Drawing;

namespace mini_omega
{
    public class ASCIIPicture : ASCII
    {   
        private string path;
        Bitmap bitmap;
        IOutputStrategy output;

        public string Path { get => path; set 
         {
             
         if(Helper.isPicture(value)) {
             path = value;
         }
         else {
            throw new WrongInputFileFormatException("The input picture isnt in a supported format, check supported formats in main menu: ", value);
         }
         } 
         }

        public ASCIIPicture(string path, int asciiImageWidth) {
            this.Path = path;
            this.bitmap = ConvertToGrayScale(Helper.Scale(new Bitmap(Path), asciiImageWidth));
            
        }
            public override void Output() {
                output.Output(PixelsArrayToAscii());
            }
            public override void setOutput(IOutputStrategy output) {
                this.output = output;
            }


        private int[,] GrayScaleBitmapToArray(Bitmap grayscale)
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
        
        private Bitmap ConvertToGrayScale(Bitmap original)
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
 
        private char[,] PixelsArrayToAscii()
        {
            int[,] pixels = GrayScaleBitmapToArray(bitmap);
            char[] charsToMap = ConfigurationManager.AppSettings.Get("Characters").ToCharArray();
            char[,] asciiImage = new char[pixels.GetLength(0), pixels.GetLength(1)];
 
             for(int i= 0; i<pixels.GetLength(0);i++) 
            {
                for (int y = 0; y < pixels.GetLength(1); y++)
                {
                    asciiImage[i, y] = Helper.MapPixelToChar(charsToMap, pixels[i, y]);
                }

            }
            return asciiImage;
        }
    }
}