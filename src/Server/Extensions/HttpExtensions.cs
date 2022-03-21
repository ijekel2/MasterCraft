using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Server.Extensions
{
    public static class HttpExtensions
    {
        public async static Task<byte[]> ToByteArray(this IFormFile formFile)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);
                return stream.ToArray();
            }
        }
    }
}
