using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels
{
    public class UploadFileVm
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public double Size { get; set; }
        public MemoryStream Stream { get; set; }
    }
}
