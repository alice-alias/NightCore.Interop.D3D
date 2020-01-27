using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace NightCore.Interop.D3D
{
    sealed class D3DShaderMacroCollection : IDisposable
    {
        public IntPtr Pointer => handle.Pointer;

        readonly List<StructHandle> handles = new List<StructHandle>();
        readonly StructHandle handle;

        public D3DShaderMacroCollection(IDictionary<string, string> data)
        {
            var pointers = new List<IntPtr>();

            foreach (var (key, value) in data)
            {
                pointers.Add(AllocString(key));
                pointers.Add(AllocString(value));
            }
            pointers.Add(IntPtr.Zero);
            pointers.Add(IntPtr.Zero);

            handle = StructHandle.Pin(pointers.ToArray());
        }

        IntPtr AllocString(string str)
        {
            var handle = StructHandle.Pin(Encoding.ASCII.GetBytes(str));
            handles.Add(handle);
            return handle.Pointer;
        }

        int locker;

        public void Dispose()
        {
            if (Interlocked.Exchange(ref locker, 1) == 0)
            {
                foreach (var i in handles)
                    i.Dispose();
                handle.Dispose();
            }
        }

        ~D3DShaderMacroCollection()
        {
            Dispose();
        }
    }
}
