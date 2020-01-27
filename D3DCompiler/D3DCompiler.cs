using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NightCore.Interop.D3D
{
    public abstract partial class D3DCompiler
    {
        public Dictionary<string, string> Macros { get; } = new Dictionary<string, string>();

        public D3DCompileResult Compile(string fileName, string entrypoint, D3DCompilerTarget target)
        {
            using var macros = new D3DShaderMacroCollection(Macros);
            using var include = new Include(this, fileName);
            var includeVtbl = new D3DIncludeVTable(include);
            var src = include.OpenRootFile();

            var hresult = NativeMethods.D3DCompile(
                src.Pointer,
                (IntPtr)src.Size,
                fileName,
                macros.Pointer,
                include != null ? includeVtbl.Pointer : IntPtr.Zero,
                entrypoint,
                target,
                0,
                0,
                out var code,
                out var err);

            var errBuf = err?.GetBytes();
            var errStr = errBuf != null ? Encoding.ASCII.GetString(errBuf) : null;
            return new D3DCompileResult(code?.GetBytes() ?? Array.Empty<byte>(), errStr, hresult);
        }

        public static D3DCompiler FromDirectory(string directory) => new Directory(directory);

        protected abstract byte[] OpenFile(string fileName, string parentFileName, D3DIncludeType includeType);
    }
}
