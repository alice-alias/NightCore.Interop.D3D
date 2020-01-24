using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NightCore.Interop.D3D
{
    public class D3DCompileResult
    {
        public byte[] Code { get; }
        public string ErrorMessage { get; }

        public int HResult { get; }

        public D3DCompileResult(byte[] code, string message, int hresult)
        {
            Code = code;
            ErrorMessage = message;
            HResult = hresult;
        }

        public void ThrowException()
        {
            if (HResult >= 0)
                return;

            if (ErrorMessage != null)
            {
                throw new D3DCompileException(HResult, ErrorMessage);
            }
            else
            {
                Marshal.ThrowExceptionForHR(HResult);
            }
        }
    }
}
