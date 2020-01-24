using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NightCore.Interop.D3D
{
    public class D3DCompileException : Win32Exception
    {
        public D3DCompileException(int error, string message) : base(error, message) { }
    }
}
