using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NightCore.Interop.D3D
{
    struct HRESULT
    {
        public int Value { get; }

        public HRESULT(int value) => Value = value;

        public override string ToString() => $"0x{Value:x8}";

        public void Check()
        {
            if (Value < 0)
                Marshal.ThrowExceptionForHR(Value);
        }

        public static implicit operator int(HRESULT v) => v.Value;
        public static explicit operator HRESULT(int v) => new HRESULT(v);
        public static explicit operator HRESULT(uint v) => new HRESULT((int)v);

        public static HRESULT E_FAIL => (HRESULT)0x80004005;
        public static HRESULT E_NOINTERFACE => (HRESULT)0x80004002;
    }
}
