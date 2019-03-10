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

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            this.Imaging.ConnectToScanner();
        }

        private void Scan_Click(object sender, RoutedEventArgs e)
        {
            ImageFile ImageFile = this.Imaging.ScanImage();

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
    }
}
