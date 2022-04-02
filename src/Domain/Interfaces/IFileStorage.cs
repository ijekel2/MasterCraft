using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Interfaces
{
    public interface IFileStorage
    {
        Task<Uri> SaveFileAsync(Stream input);

        FileStream OpenRead(Uri uri);

        FileStream OpenRead(string uri);
    }
}
