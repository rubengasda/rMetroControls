
namespace RuerteControls.Win32Interop
{
    /// <summary>
    /// Provides constants for use with Win32 native methods
    /// </summary>
    public static class NativeConstants
    {
        public const int GCL_STYLE = -26;
        public const int GWL_STYLE = -16;

        public const int CS_DROPSHADOW = 0x20000;

        public const int HT_CAPTION = 0x2;
        public const int HT_CLIENT = 0x1;

        public const int WM_NCCALCSIZE = 0x83;
        public const int WM_NCHITTEST = 0x84;
        public const int WM_NCACTIVATE = 0x86;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int WM_SYSCOMMAND = 0x112;
        public const int WM_SIZE = 0x5;

        public const int SC_MOVE = 0xF010;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;

        public const int WS_EX_TRANSPARENT = 0x20;
        public const int WS_EX_LAYERED = 0x80000;
        public const int WS_EX_NOACTIVATE = 0x8000000;
        public const int WS_CAPTION = 0xC00000;

        public const int ULW_ALPHA = 0x02;

        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;

        public const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
        public const int TVM_GETEXTENDEDSTYLE = 0x1100 + 45;
        public const int TVS_EX_DOUBLEBUFFER = 0x0004;

        public const int SWP_FRAMECHANGED = 0x20;
        public const int SWP_NOZORDER = 0x4;
        public const int SWP_NOSIZE = 0x1;
        public const int SWP_NOMOVE = 0x2;
    }
}
