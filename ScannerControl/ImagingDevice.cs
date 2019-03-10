using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIA;

namespace ScannerControl
{
    public class ImagingDevice
    {
        public String Name { get; set; }

        public String Description { get; set; }

        public String Port { get; set; }

        public String FilePath { get; set; }

        public DeviceInfo DeviceInfo { get; set; } 

        public Device ConnectedDevice { get; set; }

        public ImageFile ScannedImage { get; set; }

        public override string ToString()
        {
            String output = "Device Name: " + this.Name + "\r\n";
            output += "Description: " + this.Description + "\r\n";
            output += "Port: " + this.Port + "\r\n";

            return output;
        }
    }
}
