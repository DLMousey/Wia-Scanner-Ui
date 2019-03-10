using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerControl
{
    public class ImagingDevice
    {
        public String Name { get; set; }

        public String Description { get; set; }

        public String Port { get; set; }

        public override string ToString()
        {
            return "Device Name : " + this.Name + " | Description : " + this.Description + " | Port : " + this.Port;
        }
    }
}
