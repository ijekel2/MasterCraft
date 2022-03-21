using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Common.Interfaces
{
    public interface IFileStorage
    {
        Task<Uri> SaveFileAsync(byte[] file);

        Task<byte[]> LoadFileAsync(Uri uri);
    }
}
