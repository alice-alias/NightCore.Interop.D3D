using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NightCore.Interop.D3D
{
    public class D3DCompiler
    {
        public ID3DInclude Include { get; set; }

        public Dictionary<string, string> Macros { get; } = new Dictionary<string, string>();

        public D3DCompileResult Compile(string source, string name, string entrypoint, D3DCompilerTarget target)
        {
            var src = Encoding.ASCII.GetBytes(source);
            var pSrc = GCHandle.Alloc(src, GCHandleType.Pinned);
            var includeVtbl = new D3DIncludeVTable(Include);
            try
            {
                using (var macros = new D3DShaderMacroCollection(Macros))
                {
                    var hresult = NativeMethods.D3DCompile(
                        pSrc.AddrOfPinnedObject(),
                        (IntPtr)src.Length,
                        name,
                        macros.Pointer,
                        Include != null ? includeVtbl.Pointer : IntPtr.Zero,
                        entrypoint,
                        target,
                        0,
                        0,
                        out var code,
                        out var err);
                    var errBuf = err.GetBytes();
                    var errStr = errBuf != null ? Encoding.ASCII.GetString(errBuf) : null;
                    return new D3DCompileResult(code.GetBytes(), errStr, hresult);
                }
            }
            finally
            {
                pSrc.Free();
            }
        }
    }
}
