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
            using var src = StructHandle.Pin(Encoding.ASCII.GetBytes(source));
            using var macros = new D3DShaderMacroCollection(Macros);

            var includeVtbl = new D3DIncludeVTable(Include);

            var hresult = NativeMethods.D3DCompile(
                src.Pointer,
                (IntPtr)src.Size,
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
}
