using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIA;

namespace ScannerControl
{
    public class Imaging
    {
        public ImagingDevice ActiveDevice;

        public Imaging()
        {
            DeviceManager DeviceManager = new DeviceManager();
            for (int i = 1; i <= DeviceManager.DeviceInfos.Count; i++)
            {
                if (DeviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                    continue;

                DeviceInfo CurrentDevice = DeviceManager.DeviceInfos[i];
                this.ActiveDevice = new ImagingDevice
                {
                    Name = CurrentDevice.Properties["Name"].get_Value().ToString(),
                    Description = CurrentDevice.Properties["Description"].get_Value().ToString(),
                    Port = CurrentDevice.Properties["Port"].get_Value().ToString()
                };

                Console.WriteLine(this.ActiveDevice.ToString());
            }
        }
    }
}
