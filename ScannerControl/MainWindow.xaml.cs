using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WIA;

namespace ScannerControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Imaging Imaging;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Initialise_Click(object sender, RoutedEventArgs e)
        {
            this.Imaging = new Imaging();
        }

        private void Scan_Click(object sender, RoutedEventArgs e)
        {
            ImageFile ImageFile = this.Imaging.ScanImage();
            this.Imaging.ActiveDevice.ScannedImage = ImageFile;

            // Generate a filename in temporary storage and if, by some miracle,
            // there's a file already there by that name we'll delete it
            String TempPath = System.IO.Path.GetTempFileName();
            File.Delete(TempPath);

            // Save the new image to the temporary path we generated above,
            // create a BitmapFrame that WPF Image controls can display
            ImageFile.SaveFile(TempPath);
            BitmapFrame Img;

            // Read in the file from temporary storage, populate the BitmapFrame
            using (FileStream stream = File.OpenRead(TempPath))
            {
                Img = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                stream.Close();
            }

            File.Delete(TempPath);
            this.ScannerPreview.Source = Img;
        }

        private void Destination_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog Dialog = new FolderBrowserDialog();
            DialogResult Result = Dialog.ShowDialog();

            if (Result == System.Windows.Forms.DialogResult.OK)
            {
                this.ScannerDestination.Text = Dialog.SelectedPath;
                this.Imaging.ActiveDevice.FilePath = Dialog.SelectedPath;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            this.Imaging.SaveImage(this.DestinationFilename.Text.ToString());
        }
    }
}
