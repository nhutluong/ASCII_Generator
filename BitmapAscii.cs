using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;


namespace ASCII_Generator {
    class BitmapAscii {
        //Fields
        private int _kernelHeight;
        private int _kernelWidth;
        private BitmapMaker _imgPath;

        #region PROPERTIES
        public int KernelHeight {
            get { return _kernelHeight; }
        }//end property

        public int KernelWidth {
            get { return _kernelWidth; }
        }//end property

        public BitmapMaker ImgPath {
            get { return _imgPath; }
        }//end property
        #endregion

        #region CONSTRUCTOR
        public BitmapAscii(BitmapMaker imgPath, int kernelWidth, int kernelHeight) {
            _kernelWidth = kernelWidth;
            _kernelHeight = kernelHeight;
            _imgPath = imgPath;
        }//end constructor 

        public BitmapAscii() {
            //parameterless constructor 
        }//end constructor
        #endregion

        #region METHOD
        public BitmapMaker ConvertToGreyScale(string imageFilePath) {
            BitmapMaker greyBitmap = new BitmapMaker(imageFilePath);
            double luminosity;//avg rgb value of luminosity
            Color currentColor;

            //Loop through each pixel to convert orginal rgb to grey value 
            for (int y = 0; y < greyBitmap.Height; y++) {
                for (int x = 0; x < greyBitmap.Width; x++) {
                    //Get pixel color
                    currentColor = greyBitmap.GetPixelColor(x, y);

                    //Pass color values to get rgb's avg through luminosity method
                    luminosity = AveragePixel(currentColor);

                    //Set greyscale pixel
                    greyBitmap.SetPixel(x, y, Color.FromArgb(currentColor.A, (byte)luminosity, (byte)luminosity, (byte)luminosity));
                }//end for
            }//end for

            return greyBitmap;
        }//end method 

        public override string ToString() {
            string finalAsciiString = Asciitize(ImgPath, KernelWidth, KernelHeight);
            return finalAsciiString;
        }//end method 

        private string Asciitize(BitmapMaker imgSource, int kernelWidth, int kernelHeight) {
            //Contains the ascii text version of the picture
            string finalAsciiString = String.Empty;
            double normalizedVal;
            string asciiCharacter;

            //Convert normalized values to ascii set 
            for (int y = 0; y < imgSource.Height; y += kernelHeight) {
                for (int x = 0; x < imgSource.Width; x += kernelWidth) {
                    //Get current pixel color
                    Color pixelColor = imgSource.GetPixelColor(x, y);

                    //Get avg pixel value and return normalized value 
                    normalizedVal = AverageColor(pixelColor);

                    //Convert normalized value to char symbol
                    asciiCharacter = GreyToString(normalizedVal);

                    //Add symbol to ascii set 
                    finalAsciiString += asciiCharacter;
                }//end for
                //Creates new line when reaching end of width 
                finalAsciiString += "\n";
            }//end for

            return finalAsciiString;
        }//end method 

        private double AveragePixel(Color rgb) {
            //Luminosity algorithm (the perceived brightness of a colour)
            //Green is weighted more for perception            
            return ((.21 * rgb.R) + (.72 * rgb.G) + (.07 * rgb.B));
        }//end method

        private double AverageColor(Color pixelColors) {
            double normalizedKernelValue = 0.0;
            //Returns normalized value from the avg pixel 
            normalizedKernelValue = (AveragePixel(pixelColors)) / 255;

            return normalizedKernelValue;
        }//end method       

        private string GreyToString(double normalizedKernelValue) {
            //Normalized Values to Char Symbols
            string asciiString = string.Empty;

            if (normalizedKernelValue == 0) {
                //Fixes the whitespace from turning black 
                //rgb.A was never changed in averagepixel() so it remained the same in terms of normalized scale 
                asciiString = " ";
            } else if (normalizedKernelValue < 0.16) {
                asciiString = "@";
            } else if (normalizedKernelValue >= 0.16 && normalizedKernelValue < 0.33) {
                asciiString = "%";
            } else if (normalizedKernelValue >= 0.33 && normalizedKernelValue < 0.48) {
                asciiString = "+";
            } else if (normalizedKernelValue >= 0.48 && normalizedKernelValue < 0.64) {
                asciiString = ":";
            } else if (normalizedKernelValue >= 0.64 && normalizedKernelValue < 0.8) {
                asciiString = ".";
            } else if (normalizedKernelValue >= 0.8 && normalizedKernelValue < 1) {
                asciiString = " ";
            }//end if

            return asciiString;
        }//end method
        #endregion 
    }//end class
}//end namespace