using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace NightCore.Interop.D3D
{
    public abstract class ComCallback<T> : ComCallback
    {
        protected ComCallback(T target) : base(target) { }

        protected static new T Get(IntPtr @this) => (T)ComCallback.Get(@this);
    }

    public abstract class ComCallback
    {
        static readonly ConcurrentDictionary<IntPtr, ComCallback> objects = new ConcurrentDictionary<IntPtr, ComCallback>();

        object target;
        int refCount;

        protected ComCallback(object target)
        {
            this.target = target;
            var list = new VTable();
            Build(list);
            list.Add((QueryInterfaceDelegate)QueryInterface);
            list.Add((AddRefDelegate)AddRef);
            list.Add((AddRefDelegate)Release);

            var vtable = list.AllocCoTaskMem();

            var stride = Marshal.SizeOf<IntPtr>();
            Pointer = Marshal.AllocCoTaskMem(stride * (list.Count + 1));
            Marshal.WriteIntPtr(Pointer, vtable);
            objects[Pointer] = this;
        }

        public IntPtr Pointer { get; private set; }

        delegate HRESULT QueryInterfaceDelegate(IntPtr @this, ref Guid guid, out IntPtr data);
        delegate int AddRefDelegate(IntPtr @this);

        protected abstract void Build(VTable vtable);

        protected static object Get(IntPtr @this) => objects[@this].target;

        protected virtual HRESULT QueryInterface(ref Guid guid, out IntPtr data)
        {
            data = IntPtr.Zero;
            return HRESULT.E_NOINTERFACE;
        }

        protected virtual int AddRef()
        {
            return Interlocked.Increment(ref refCount);
        }

        protected virtual int Release()
        {
            var result = Interlocked.Decrement(ref refCount);
            if (result == 0)
            {
                objects.Remove(Pointer, out _);
                target = default;
                var vtable = Marshal.ReadIntPtr(Pointer);
                Marshal.FreeCoTaskMem(Pointer);
                Marshal.FreeCoTaskMem(vtable);
                Pointer = IntPtr.Zero;
            }
            return result;
        }

        static HRESULT QueryInterface(IntPtr @this, ref Guid guid, out IntPtr data)
            => objects[@this].QueryInterface(ref guid, out data);

        static int AddRef(IntPtr @this)
            => objects[@this].AddRef();

        static int Release(IntPtr @this)
            => objects[@this].Release();
    }
}
