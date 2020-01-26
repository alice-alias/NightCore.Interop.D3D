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
        public IntPtr Pointer { get; private set; }

        public D3DShaderMacroCollection(IDictionary<string, string> data)
        {
            var stride = Marshal.SizeOf<IntPtr>();

            var pointers = new List<IntPtr>();

            foreach (var (key, value) in data)
            {
                pointers.Add(AllocString(key));
                pointers.Add(AllocString(value));
            }
            pointers.Add(IntPtr.Zero);
            pointers.Add(IntPtr.Zero);

            var pointersArray = pointers.ToArray();
            Pointer = Marshal.AllocCoTaskMem(stride * pointersArray.Length);
            Marshal.Copy(pointersArray, 0, Pointer, pointersArray.Length);
        }

        static IntPtr AllocString(string str)
        {
            var bytes = Encoding.ASCII.GetBytes(str);
            var ptr = Marshal.AllocCoTaskMem(bytes.Length);
            Marshal.Copy(bytes, 0, ptr, bytes.Length);
            return ptr;
        }

        int locker;

        public void Dispose()
        {
            if (Interlocked.Exchange(ref locker, 1) == 0)
            {
                var current = Pointer;
                var stride = Marshal.SizeOf<IntPtr>();
                while (true)
                {
                    var ptr = Marshal.ReadIntPtr(current);
                    if (ptr == IntPtr.Zero)
                        break;
                    Marshal.FreeCoTaskMem(ptr);
                    current = IntPtr.Add(current, stride);
                }
                Marshal.FreeCoTaskMem(Pointer);
                Pointer = IntPtr.Zero;
            }
        }

        ~D3DShaderMacroCollection()
        {
            Dispose();
        }
    }
}
