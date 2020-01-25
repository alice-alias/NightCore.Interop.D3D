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

        public HRESULT HResult { get; }

        public D3DCompileResult(byte[] code, string message, HRESULT hresult)
        {
            Code = code;
            ErrorMessage = message;
            HResult = hresult;
        }

        public void ThrowException()
        {
            if (HResult.Succeed)
                return;

            if (ErrorMessage != null)
            {
                throw new D3DCompileException((int)HResult, ErrorMessage);
            }
            else
            {
                Marshal.ThrowExceptionForHR((int)HResult);
            }
        }
    }
}
