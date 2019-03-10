using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIA;
using System.IO;

namespace ScannerControl
{
    public class Imaging
    {
        public ImagingDevice ActiveDevice;
        public DeviceManager DeviceManager;

        public Imaging()
        {
            this.DeviceManager = new DeviceManager();
            for (int i = 1; i <= this.DeviceManager.DeviceInfos.Count; i++)
            {
                if (this.DeviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                    continue;

                DeviceInfo CurrentDevice = DeviceManager.DeviceInfos[i];
                this.ActiveDevice = new ImagingDevice
                {
                    Name = CurrentDevice.Properties["Name"].get_Value().ToString(),
                    Description = CurrentDevice.Properties["Description"].get_Value().ToString(),
                    Port = CurrentDevice.Properties["Port"].get_Value().ToString(),
                    DeviceInfo = CurrentDevice
                };

                Console.WriteLine(this.ActiveDevice.ToString());
            }
        }

        public void ConnectToScanner()
        {
            this.ActiveDevice.ConnectedDevice = this.ActiveDevice.DeviceInfo.Connect();
        }

        public ImageFile ScanImage()
        {
            Item ScannerItem = this.ActiveDevice.ConnectedDevice.Items[1];
            ImageFile ScannedImage = (ImageFile)ScannerItem.Transfer(FormatID.wiaFormatJPEG);

            return ScannedImage;
            //String FilePath = @"D:\Development\Dotnet\WPF\ScannedImages\scan.jpeg";

            //if (File.Exists(FilePath))
            //    File.Delete(FilePath);

            //ScannedImage.SaveFile(FilePath);
            //ImageFile ScannedImage = (ImageFile) this.ActiveDevice.ConnectedDevice
        }
    }
}
