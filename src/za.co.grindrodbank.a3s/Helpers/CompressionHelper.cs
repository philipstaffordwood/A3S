/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using za.co.grindrodbank.a3s.Exceptions;

namespace za.co.grindrodbank.a3s.Helpers
{
    public static class CompressionHelper
    {
        public static void ExtractTarGz(string filename, string outputDir)
        {
            using var stream = File.OpenRead(filename);
            ExtractTarGz(stream, outputDir);
        }

        public static void ExtractTarGz(Stream stream, string outputDir)
        {
            // A GZipStream is not seekable, so copy it first to a MemoryStream
            using var gzip = new GZipStream(stream, CompressionMode.Decompress);
            const int chunk = 4096;
            using var memStr = new MemoryStream();
            int read;
            var buffer = new byte[chunk];

            do
            {
                read = gzip.Read(buffer, 0, chunk);
                memStr.Write(buffer, 0, read);
            } while (read == chunk);

            memStr.Seek(0, SeekOrigin.Begin);

            ExtractTar(memStr, outputDir);
        }

        public static void ExtractTar(string filename, string outputDir)
        {
            using var stream = File.OpenRead(filename);
            ExtractTar(stream, outputDir);
        }

        public static void ExtractTar(Stream stream, string outputDir)
        {
            var buffer = new byte[100];

            while (true)
            {
                stream.Read(buffer, 0, 100);
                var name = Encoding.ASCII.GetString(buffer).Trim('\0');
                if (string.IsNullOrWhiteSpace(name))
                    break;

                stream.Seek(24, SeekOrigin.Current);
                stream.Read(buffer, 0, 12);
                var size = Convert.ToInt64(Encoding.ASCII.GetString(buffer, 0, 12).Trim(), 8);

                stream.Seek(376L, SeekOrigin.Current);

                var output = Path.Combine(outputDir, name);
                if (!Directory.Exists(Path.GetDirectoryName(output)))
                    Directory.CreateDirectory(Path.GetDirectoryName(output));

                using (var str = File.Open(output, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    var buf = new byte[size];
                    stream.Read(buf, 0, buf.Length);
                    str.Write(buf, 0, buf.Length);
                }

                if (stream.Position > stream.Length)
                    throw new ArchiveException("There was an error processing the tar ball.");

                var offset = 512 - (stream.Position % 512);
                if (offset == 512)
                    offset = 0;

                stream.Seek(offset, SeekOrigin.Current);
            }
        }
    }
}
