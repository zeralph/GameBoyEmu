
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Gameboy.Visual
{

    public static class ExternalStuff
    {

        [DllImport("user32")]
        private static extern int GetScrollInfo(IntPtr hwnd, int nBar, ref SCROLLINFO scrollInfo);
        [DllImport("user32")]
        static extern int SetScrollInfo(IntPtr hwnd, int fnBar, [In] ref SCROLLINFO scrollInfo, bool fRedraw);

        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;

        private const int SB_HORZ = 0;
        private const int SB_VERT = 1;

        private const int SB_LINELEFT = 0;
        private const int SB_LINERIGHT = 1;
        private const int SB_PAGELEFT = 2;
        private const int SB_PAGERIGHT = 3;
        private const int SB_THUMBPOSITION = 4;
        private const int SB_THUMBTRACK = 5;
        private const int SB_LEFT = 6;
        private const int SB_RIGHT = 7;
        private const int SB_ENDSCROLL = 8;

        private const int SIF_TRACKPOS = 0x10;
        private const int SIF_RANGE = 0x1;
        private const int SIF_POS = 0x4;
        private const int SIF_PAGE = 0x2;
        private const int SIF_ALL = SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS;

        public struct SCROLLINFO
        {
            public int cbSize;
            public int fMask;
            public int min;
            public int max;
            public int nPage;
            public int nPos;
            public int nTrackPos;
        }

        public static bool ReachedBottom(this RichTextBox rtb)
        {
            SCROLLINFO scrollInfo = new SCROLLINFO();
            scrollInfo.cbSize = Marshal.SizeOf(scrollInfo);
            //SIF_RANGE = 0x1, SIF_TRACKPOS = 0x10,  SIF_PAGE= 0x2
            scrollInfo.fMask = SIF_ALL;// 0x10 | 0x1 | 0x2;
            GetScrollInfo(rtb.Handle, SB_VERT, ref scrollInfo);//nBar = 1 -> VScrollbar
            return scrollInfo.max == scrollInfo.nTrackPos + scrollInfo.nPage;
        }

        public static double GetPositionPercentage( this RichTextBox rtb)
        {
            SCROLLINFO scrollInfo = new SCROLLINFO();
            scrollInfo.cbSize = Marshal.SizeOf(scrollInfo);
            //SIF_RANGE = 0x1, SIF_TRACKPOS = 0x10,  SIF_PAGE= 0x2
            scrollInfo.fMask = SIF_ALL;// 0x10 | 0x1 | 0x2;
            GetScrollInfo(rtb.Handle, 1, ref scrollInfo);//nBar = 1 -> VScrollbar
            return (double)scrollInfo.nTrackPos / (double)scrollInfo.max;
        }

        public static void SetPositionPercentage( this RichTextBox rtb, double Percentage )
        {
            SCROLLINFO scrollInfo = new SCROLLINFO();
            scrollInfo.cbSize = Marshal.SizeOf(scrollInfo);
            //SIF_RANGE = 0x1, SIF_TRACKPOS = 0x10,  SIF_PAGE= 0x2
            scrollInfo.fMask = SIF_ALL;// 0x10 | 0x1 | 0x2;
            GetScrollInfo(rtb.Handle, 1, ref scrollInfo);//nBar = 1 -> VScrollbar
            scrollInfo.nPos = (int)(Percentage * scrollInfo.max);
            int result = SetScrollInfo(rtb.Handle, SB_VERT, ref scrollInfo, true);
        }
    }
}
