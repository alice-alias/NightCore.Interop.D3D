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

        public override bool Equals(object? obj) => obj is D3DCompilerTarget o && o.TargetName == TargetName;
        public override int GetHashCode() => TargetName.GetHashCode();
        public static bool operator ==(D3DCompilerTarget left, D3DCompilerTarget right) => left.Equals(right);
        public static bool operator !=(D3DCompilerTarget left, D3DCompilerTarget right) => !(left == right);

        /// <summary>Direct3D 11.0/11.1 Compute Shader</summary>
        public static D3DCompilerTarget CS_5_0 => (D3DCompilerTarget)"cs_5_0";

        /// <summary>Direct3D 11.0/11.1 Domain Shader</summary>
        public static D3DCompilerTarget DS_5_0 => (D3DCompilerTarget)"ds_5_0";

        /// <summary>Direct3D 11.0/11.1 Geometry Shader</summary>
        public static D3DCompilerTarget GS_5_0 => (D3DCompilerTarget)"gs_5_0";

        /// <summary>Direct3D 11.0/11.1 Hull Shader</summary>
        public static D3DCompilerTarget HS_5_0 => (D3DCompilerTarget)"hs_5_0";

        /// <summary>Direct3D 11.0/11.1 Pixel Shader</summary>
        public static D3DCompilerTarget PS_5_0 => (D3DCompilerTarget)"ps_5_0";

        /// <summary>Direct3D 11.0/11.1 Vertex Shader</summary>
        public static D3DCompilerTarget VS_5_0 => (D3DCompilerTarget)"vs_5_0";

        /// <summary>Direct3D 10.1 Compute Shader</summary>
        public static D3DCompilerTarget CS_4_1 => (D3DCompilerTarget)"cs_4_1";

        /// <summary>Direct3D 10.1 Geometry Shader</summary>
        public static D3DCompilerTarget GS_4_1 => (D3DCompilerTarget)"gs_4_1";

        /// <summary>Direct3D 10.1 Pixel Shader</summary>
        public static D3DCompilerTarget PS_4_1 => (D3DCompilerTarget)"ps_4_1";

        /// <summary>Direct3D 10.1 Vertex Shader</summary>
        public static D3DCompilerTarget VS_4_1 => (D3DCompilerTarget)"vs_4_1";

        /// <summary>Direct3D 10.0 Compute Shader</summary>
        public static D3DCompilerTarget CS_4_0 => (D3DCompilerTarget)"cs_4_0";

        /// <summary>Direct3D 10.0 Geometry Shader</summary>
        public static D3DCompilerTarget GS_4_0 => (D3DCompilerTarget)"gs_4_0";

        /// <summary>Direct3D 10.0 Pixel Shader</summary>
        public static D3DCompilerTarget PS_4_0 => (D3DCompilerTarget)"ps_4_0";

        /// <summary>Direct3D 10.0 Vertex Shader</summary>
        public static D3DCompilerTarget VS_4_0 => (D3DCompilerTarget)"vs_4_0";
    }
}
