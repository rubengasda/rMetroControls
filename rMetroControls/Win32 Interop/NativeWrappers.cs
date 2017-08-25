using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace rMetroControls.Win32Interop
{
    /// <summary>
    /// Provides wrappers for Win32 native methods.
    /// </summary>
    public static class NativeWrappers
    {
        public static IntPtr GetClassLongPtr(HandleRef hWnd, int nIndex)
        {
            if (IntPtr.Size > 4)
                return NativeMethods.GetClassLongPtr64(hWnd, nIndex);
            else
                return new IntPtr(NativeMethods.GetClassLongPtr32(hWnd, nIndex));
        }

        public static IntPtr SetClassLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size > 4)
                return NativeMethods.SetClassLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(NativeMethods.SetClassLongPtr32(hWnd, nIndex, unchecked((uint)dwNewLong.ToInt32())));
        }

        public static bool DwmIsCompositionEnabled()
        {
            bool result;
            NativeMethods.DwmIsCompositionEnabled(out result);
            return result;
        }
    }
}
