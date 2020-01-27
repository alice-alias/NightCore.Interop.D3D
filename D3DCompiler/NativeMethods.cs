using NightCore.Interop.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NightCore.Interop.D3D
{
    static class NativeMethods
    {
        [PreserveSig]
        [DllImport("D3DCompiler_47.dll")]
        public static extern HRESULT D3DCreateBlob(IntPtr size, out ID3DBlob? ppBlob);

#pragma warning disable CA2101
        [PreserveSig]
        [DllImport("D3DCompiler_47.dll", CharSet = CharSet.Ansi)]
        public static extern HRESULT D3DCompile(
            IntPtr pSrcData,
            IntPtr srcDataSize,
            string sourceName,
            IntPtr pDefines,
            IntPtr pInclude,
            string entryPoint,
            D3DCompilerTarget target,
            uint flags1,
            uint flags2,
            out ID3DBlob? code,
            out ID3DBlob? errorMsgs);
#pragma warning restore CA2101
    }
}
