using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NightCore.Interop.D3D
{
    public class D3DIncludeVTable : ComCallback<ID3DInclude>
    {
        public D3DIncludeVTable(ID3DInclude target) : base(target) { }

        delegate HRESULT OpenDelegate(IntPtr @this, D3DIncludeType includeType, [MarshalAs(UnmanagedType.LPStr)] string fileName, IntPtr parentData, out IntPtr data, out int size);
        delegate HRESULT CloseDelegate(IntPtr @this, IntPtr data);

        static HRESULT Open(IntPtr @this, D3DIncludeType includeType, string fileName, IntPtr parentData, out IntPtr data, out int size)
            => Get(@this).Open(includeType, fileName, parentData, out data, out size);

        static HRESULT Close(IntPtr @this, IntPtr data)
            => Get(@this).Close(data);

        protected override void Build(VTable vtable)
        {
            vtable.Add((OpenDelegate)Open);
            vtable.Add((CloseDelegate)Close);
        }
    }
}
