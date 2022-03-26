﻿using MasterCraft.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Infrastructure.FileStorage
{
    public class LocalFileStorage : IFileStorage
    {
        public FileStream OpenRead(Uri uri)
        {
            return OpenRead(uri.AbsolutePath);
        }

        public FileStream OpenRead(string uri)
        {
            return File.OpenRead(uri);
        }

        public async Task<Uri> SaveFileAsync(Stream input)
        {
            string filePath = Path.GetTempFileName();
            using FileStream fileStream = File.Create(filePath);
            input.Seek(0, SeekOrigin.Begin);
            await input.CopyToAsync(fileStream);

            return new Uri(filePath);
        }
    }
}
