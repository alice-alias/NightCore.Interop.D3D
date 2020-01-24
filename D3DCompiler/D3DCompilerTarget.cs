using System;
using System.Collections.Generic;
using System.Text;

namespace NightCore.Interop.D3D
{
    public struct D3DCompilerTarget
    {
        public string TargetName { get; }

        public D3DCompilerTarget(string targetName) => TargetName = targetName;

        public static explicit operator D3DCompilerTarget(string targetName) => new D3DCompilerTarget(targetName);
    }
}
