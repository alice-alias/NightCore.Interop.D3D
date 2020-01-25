using System;
using System.Collections.Generic;
using System.Text;

namespace NightCore.Interop.D3D
{
    public interface ID3DInclude
    {
        HRESULT Open(D3DIncludeType includeType, string fileName, IntPtr parentData, out IntPtr data, out int size);

        HRESULT Close(IntPtr data);
    }
}
