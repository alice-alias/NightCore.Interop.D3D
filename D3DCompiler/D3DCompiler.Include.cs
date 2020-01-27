using NightCore.Interop.Win32;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NightCore.Interop.D3D
{
    partial class D3DCompiler
    {
        sealed class Include : ID3DInclude, IDisposable
        {
            readonly D3DCompiler parent;
            readonly string rootFile;

            public Include(D3DCompiler parent, string rootFile)
            {
                this.parent = parent;
                this.rootFile = rootFile;
            }

            HRESULT ID3DInclude.Close(IntPtr data)
            {
                if (handles.Remove(data, out var handle))
                {
                    handle.Dispose();
                }
                return HRESULT.S_OK;
            }

            HRESULT ID3DInclude.Open(D3DIncludeType includeType, string fileName, IntPtr parentData, out IntPtr data, out int size)
            {
                string parentFileName = rootFile;
                if (handles.TryGetValue(parentData, out var h))
                    parentFileName = h.Name;

                var handle = Open(fileName, parentFileName, includeType);
                data = handle.Pointer;
                size = handle.Size;
                return HRESULT.S_OK;
            }

            public StructHandle OpenRootFile() => Open(rootFile, null, D3DIncludeType.Local);
            readonly ConcurrentDictionary<IntPtr, SourceCodeHandle> handles = new ConcurrentDictionary<IntPtr, SourceCodeHandle>();

            StructHandle Open(string fileName, string? parentFileName, D3DIncludeType includeType)
            {
                var handle = StructHandle.Pin(parent.OpenFile(fileName, parentFileName ?? rootFile, includeType));
                handles[handle.Pointer] = (fileName, handle);
                return handle;
            }

            public void Dispose()
            {
                while (handles.Count > 0)
                {
                    if (handles.Remove(handles.FirstOrDefault().Key, out var handle))
                    {
                        handle.Dispose();
                    }
                }
            }
        }

        sealed class SourceCodeHandle : IDisposable
        {
            public string Name { get; }
            readonly StructHandle handle;

            public SourceCodeHandle(string name, StructHandle handle)
                => (Name, this.handle) = (name, handle);
            
            public static implicit operator SourceCodeHandle((string, StructHandle) v) => new SourceCodeHandle(v.Item1, v.Item2);

            public IntPtr Pointer => handle.Pointer;
            public int Size => handle.Size;

            public void Dispose()
            {
                handle.Dispose();
            }
        }
    }
}
