using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NightCore.Interop.D3D
{
    public static class Extensions
    {
        internal static byte[] GetBytes(this ID3DBlob blob)
        {
            if (blob == null)
                return null;

            var buf = new byte[blob.GetBufferSize().ToInt32()];
            var ptr = blob.GetBufferPointer();
            Marshal.Copy(ptr, buf, 0, buf.Length);
            return buf;
        }
    }
}
