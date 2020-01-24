using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NightCore.Interop.D3D
{
    [Guid("8BA5FB08-5195-40e2-AC58-0D989C3A0102")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface ID3DBlob
    {
        [PreserveSig]
        IntPtr GetBufferPointer();

        [PreserveSig]
        IntPtr GetBufferSize();
    }
}
