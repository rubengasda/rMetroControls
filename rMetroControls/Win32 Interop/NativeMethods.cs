using System;
using System.Runtime.InteropServices;

namespace rMetroControls.Win32Interop
{
    /// <summary>
    /// Provides access to some Win32 methods
    /// </summary>
    public static class NativeMethods
    {
        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        public static extern uint GetClassLongPtr32(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        public static extern IntPtr GetClassLongPtr64(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetClassLong")]
        public static extern uint SetClassLongPtr32(HandleRef hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetClassLongPtr")]
        public static extern IntPtr SetClassLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref NativeStructs.POINT pptDst, ref NativeStructs.SIZE psize, IntPtr hdcSrc, ref NativeStructs.POINT pprSrc, int crKey, ref NativeStructs.BLENDFUNCTION pblend, int dwFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);
        


        [DllImport("dwmapi.dll", PreserveSig = true)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll", PreserveSig = true)]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref NativeStructs.MARGINS margins);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(out bool enabled);



        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject(IntPtr hObject); 
    }
}
