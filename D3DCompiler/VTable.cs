using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NightCore.Interop.D3D
{
    public class VTable : IReadOnlyCollection<IntPtr>
    {
        readonly List<IntPtr> pointers = new List<IntPtr>();

        public void Add(Delegate @delegate)
            => pointers.Add(Marshal.GetFunctionPointerForDelegate(@delegate));

        public IntPtr AllocCoTaskMem()
        {
            var array = pointers.ToArray();
            var ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf<IntPtr>() * array.Length);
            Marshal.Copy(array, 0, ptr, array.Length);
            return ptr;
        }

        public int Count => pointers.Count;
        public IEnumerator<IntPtr> GetEnumerator() => pointers.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
