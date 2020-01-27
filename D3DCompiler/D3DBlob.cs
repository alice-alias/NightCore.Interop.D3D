using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace NightCore.Interop.D3D
{
    public class D3DBlob
    {
        readonly ID3DBlob ptr;

        internal D3DBlob(ID3DBlob ptr)
        {
            this.ptr = ptr;
        }

        public D3DBlob(int size)
        {
#nullable disable
            NativeMethods.D3DCreateBlob((IntPtr)size, out ptr).ThrowIfFailed();
#nullable restore
        }

        public int Size => ptr.GetBufferSize().ToInt32();
        public long LongSize => ptr.GetBufferSize().ToInt64();
        public IntPtr Pointer => ptr.GetBufferPointer();
    }
}
