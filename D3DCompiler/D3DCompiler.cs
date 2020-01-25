﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NightCore.Interop.D3D
{
    public static class D3DCompiler
    {
        public static D3DCompileResult Compile(string source, string name, string entrypoint, D3DCompilerTarget target)
            => Compile(source, name, entrypoint, target, null);

        public static D3DCompileResult Compile(string source, string name, string entrypoint, D3DCompilerTarget target, ID3DInclude include)
        {
            var src = Encoding.ASCII.GetBytes(source);
            var pSrc = GCHandle.Alloc(src, GCHandleType.Pinned);
            var includeVtbl = new D3DIncludeVTable(include);
            try
            {
                var hresult = NativeMethods.D3DCompile(
                    pSrc.AddrOfPinnedObject(),
                    (IntPtr)src.Length,
                    name,
                    IntPtr.Zero,
                    include != null ? includeVtbl.Pointer : IntPtr.Zero,
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
            finally
            {
                pSrc.Free();
            }
        }
    }
}
