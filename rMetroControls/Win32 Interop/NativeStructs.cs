using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace RuerteControls.Win32Interop
{
    /// <summary>
    /// Provides structures to use with Win32 native methods
    /// </summary>
    public static class NativeStructs
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left, top, right, bottom;

            public RECT(Rectangle rc)
            {
                this.left = rc.Left;
                this.top = rc.Top;
                this.right = rc.Right;
                this.bottom = rc.Bottom;
            }

            public Rectangle ToRectangle()
            {
                return Rectangle.FromLTRB(left, top, right, bottom);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPOS
        {
            public IntPtr hWnd, hWndInsertAfter;
            public int x, y, cx, cy, flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NCCALCSIZE_PARAMS
        {
            public RECT rgrc0, rgrc1, rgrc2;
            public WINDOWPOS lppos;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public MARGINS(int left, int right, int top, int bottom)
            {
                this.leftWidth = left;
                this.rightWidth = right;
                this.topHeight = top;
                this.bottomHeight = bottom;
            }

            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x, y;

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SIZE
        {
            public int cx, cy;

            public SIZE(int cx, int cy)
            {
                this.cx = cx;
                this.cy = cy;
            }
        }
    }
}
