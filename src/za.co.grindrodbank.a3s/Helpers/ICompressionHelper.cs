using System;
using System.IO;

namespace za.co.grindrodbank.a3s.Helpers
{
    public interface ICompressionHelper
    {
        void ExtractTarGz(string filename, string outputDir);
        void ExtractTarGz(Stream stream, string outputDir);
        void ExtractTar(string filename, string outputDir);
        void ExtractTar(Stream stream, string outputDir);
    }
}
