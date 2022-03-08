using System.Windows;
using System.Drawing;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;

namespace ASCII_Generator {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        //Global struct       
        public string filepath = "";
        public int kernelWidth = 0;
        public int kernelHeight = 0;
        

        public MainWindow() {
            InitializeComponent();
        }//end main window

        //BitmapAscii newBitmap = new BitmapAscii();
        private void MenuItem_OpenClick_1(object sender, RoutedEventArgs e) {
            
            //create instance of openfile dialog class
            OpenFileDialog ofdTemp = new OpenFileDialog();

            //filters only the desired file types 
            ofdTemp.Filter = "PNG Files | *.png; | JPEG Files | *.jpg; jpeg;";

            //displays dialog box and checks if actual is opened 
            bool fileSelected = ofdTemp.ShowDialog() == true;

            //keep record of file path 
            filepath = ofdTemp.FileName;

            //if user selected a file then open the image and send to imagebox
            if (fileSelected) {//then
                //create instance of bitmapmaker
                BitmapMaker originalBitmap = new BitmapMaker(ofdTemp.FileName);

                //set and display source of image box to bitmapmaker
                imgTestImage.Source = originalBitmap.MakeBitmap();

                
            }//end if

        }//end event
        
        private void MenuItem_SaveClick_1(object sender, RoutedEventArgs e) {
            //Create instance of openfile dialog class
            SaveFileDialog saveFile = new SaveFileDialog();

            //Saves file to specific file type 
            saveFile.Filter = "Text Files|*.txt";

            //Validates if filepath is established
            if (saveFile.ShowDialog() == true) {
                //assign to global filepath 
                filepath = saveFile.FileName;

                //Creates ascii file  
                StreamWriter writer = new StreamWriter(filepath);
                writer.Write(txtOutput.Text);
                writer.Close();
            }//end if             
        }//end event
        private void Button_ClickAsciitize(object sender, RoutedEventArgs e) {
            if (filepath != string.Empty) {
                //Convert text input to arguments for kernel  
                int.TryParse(kWidthTxtBox.Text, out kernelWidth);
                int.TryParse(kHeightTxtBox.Text, out kernelHeight);

                //Declare bitmap and ascii object
                BitmapMaker uploadedImage = new BitmapMaker(filepath);
                BitmapAscii asciiImg = new BitmapAscii(uploadedImage, kernelWidth, kernelHeight);

                //Valid input: Must enter value to begin asciify process
                if (kernelWidth > 0 && kernelHeight > 0) {
                    //Initializes ascii processing and outputs ascii to gui 
                    txtOutput.Text = asciiImg.ToString();
                } else {
                    MessageBox.Show("Enter integral values (greater than 0) for kernel width and height");
                }//end else 
            } else {
                MessageBox.Show("Must upload image to asciitize");
            }//end else 
        }//end event 

        
        private void about_Click(object sender, RoutedEventArgs e) {
            //Retrieve documentation on ascii art 

            //Declare Process object - enables start or stop task processing 
            Process webDoc = new Process();

            //Specifies os shell should be used to start process
            webDoc.StartInfo.UseShellExecute = true;

            //Reference file path 
            webDoc.StartInfo.FileName = "https://en.wikipedia.org/wiki/ASCII_art";

            //Run task 
            webDoc.Start();
        }//end event 

        private void MenuItem_ExitClick(object sender, RoutedEventArgs e) {
            this.Close();
        }//end event

        private void Button_ClickGreyTheImage(object sender, RoutedEventArgs e) {
            if (filepath != string.Empty) {
                //new instance of bitmap          
                BitmapMaker greyBitmap = new BitmapMaker(filepath);

                //new instance of bitmap ascii
                BitmapAscii tempBitmap = new BitmapAscii();

                //set greybitmap to our bitmapascii convert to grey scale method
                greyBitmap = tempBitmap.ConvertToGreyScale(filepath);

                //set greyimage.source to makebitmap method 
                greyImage.Source = greyBitmap.MakeBitmap();
            } else {
                MessageBox.Show("Must upload image to convert to greyscale");
            }//end else
        }//end event

    }//end class
}//end namespace