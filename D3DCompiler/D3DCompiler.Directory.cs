using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NightCore.Interop.D3D
{
    partial class D3DCompiler
    {
        class Directory : D3DCompiler
        {
            readonly string directory;
            public Directory(string directory)
            {
                this.directory = directory;
            }

            protected override byte[] OpenFile(string fileName, string parentFileName, D3DIncludeType includeType)
            {
                var dirname = Path.GetDirectoryName(parentFileName ?? ".");
                if (!string.IsNullOrEmpty(dirname))
                    fileName = Path.Combine(dirname, fileName);

                if (!string.IsNullOrEmpty(directory))
                    fileName = Path.Combine(directory, fileName);

                using (var reader = new StreamReader(fileName))
                    return Encoding.ASCII.GetBytes(reader.ReadToEnd());
            }
        }
    }
}
