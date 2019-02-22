using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Digitalizacion2014.GDIDraw
{
    public enum drawingMode
    {
        R2_BLACK = 1,
        R2_NOTMERGEPEN = 2,
        R2_MASKNOTPEN = 3,
        R2_NOTCOPYPEN = 4,
        R2_MASKPENNOT = 5,
        R2_NOT = 6,
        R2_XORPEN = 7,
        R2_NOTMASKPEN = 8,
        R2_MASKPEN = 9,
        R2_NOTXORPEN = 10, // 0x0000000A
        R2_NOP = 11, // 0x0000000B
        R2_MERGENOTPEN = 12, // 0x0000000C
        R2_COPYPEN = 13, // 0x0000000D
        R2_MERGEPENNOT = 14, // 0x0000000E
        R2_MERGEPEN = 15, // 0x0000000F
        R2_LAST = 16, // 0x00000010
        R2_WHITE = 16, // 0x00000010
    }

    public enum PenStyles
    {
        PS_COSMETIC = 0,
        PS_ENDCAP_ROUND = 0,
        PS_JOIN_ROUND = 0,
        PS_SOLID = 0,
        PS_DASH = 1,
        PS_DOT = 2,
        PS_DASHDOT = 3,
        PS_DASHDOTDOT = 4,
        PS_NULL = 5,
        PS_INSIDEFRAME = 6,
        PS_USERSTYLE = 7,
        PS_ALTERNATE = 8,
        PS_STYLE_MASK = 15, // 0x0000000F
        PS_ENDCAP_SQUARE = 256, // 0x00000100
        PS_ENDCAP_FLAT = 512, // 0x00000200
        PS_ENDCAP_MASK = 3840, // 0x00000F00
        PS_JOIN_BEVEL = 4096, // 0x00001000
        PS_JOIN_MITER = 8192, // 0x00002000
        PS_JOIN_MASK = 61440, // 0x0000F000
        PS_GEOMETRIC = 65536, // 0x00010000
        PS_TYPE_MASK = 983040, // 0x000F0000
    }

    public class GDI
    {
        private IntPtr hdc;
        private Graphics grp;

        public GDI(IntPtr ptrHdc)
        {
            this.grp = (Graphics)null;
            this.hdc = ptrHdc;
        }

        public GDI(Graphics g)
        {
            this.grp = g;
            this.hdc = this.grp.GetHdc();
        }

        ~GDI()
        {
            if (this.grp == null)
                return;
            this.grp.ReleaseHdc(this.hdc);
        }

        public void DrawLine(Color color, Point p1, Point p2)
        {
            this.SetROP2(drawingMode.R2_XORPEN);
            IntPtr pen = this.CreatePEN(PenStyles.PS_SOLID, 2, GDI.RGB((int)color.R, (int)color.G, (int)color.B));
            IntPtr hgdiobj = this.SelectObject(pen);
            this.MoveTo(p1.X, p1.Y);
            this.LineTo(p2.X, p2.X);
            this.SelectObject(hgdiobj);
            this.DeleteOBJECT(pen);
        }

        public IntPtr CreatePEN(PenStyles fnPenStyle, int nWidth, int crColor)
        {
            return GDI.CreatePen(fnPenStyle, nWidth, crColor);
        }

        public bool DeleteOBJECT(IntPtr hObject)
        {
            return GDI.DeleteObject(hObject);
        }

        public IntPtr SelectObject(IntPtr hgdiobj)
        {
            return GDI.SelectObject(this.hdc, hgdiobj);
        }

        public void MoveTo(int X, int Y)
        {
            GDI.MoveToEx(this.hdc, X, Y, 0);
        }

        public void LineTo(int X, int Y)
        {
            GDI.LineTo(this.hdc, X, Y);
        }

        public int SetROP2(drawingMode fnDrawMode)
        {
            return GDI.SetROP2(this.hdc, fnDrawMode);
        }

        [DllImport("gdi32.dll")]
        public static extern int SetROP2(IntPtr hdc, drawingMode fnDrawMode);

        [DllImport("gdi32.dll")]
        public static extern bool MoveToEx(IntPtr hdc, int X, int Y, int oldp);

        [DllImport("gdi32.dll")]
        public static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreatePen(PenStyles fnPenStyle, int nWidth, int crColor);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        public static int RGB(int R, int G, int B)
        {
            return R | G << 8 | B << 16;
        }
    }

    public class Utils
    {
        public static Image Crop(
          Image imgPhoto,
          int Width,
          int Height,
          Utils.AnchorPosition Anchor)
        {
            int width1 = imgPhoto.Width;
            int height1 = imgPhoto.Height;
            int x1 = 0;
            int y1 = 0;
            int x2 = 0;
            int y2 = 0;
            float num1 = (float)Width / (float)width1;
            float num2 = (float)Height / (float)height1;
            float num3;
            if ((double)num2 < (double)num1)
            {
                num3 = num1;
                switch (Anchor)
                {
                    case Utils.AnchorPosition.Top:
                        y2 = 0;
                        break;
                    case Utils.AnchorPosition.Bottom:
                        y2 = (int)((double)Height - (double)height1 * (double)num3);
                        break;
                    default:
                        y2 = (int)(((double)Height - (double)height1 * (double)num3) / 2.0);
                        break;
                }
            }
            else
            {
                num3 = num2;
                switch (Anchor)
                {
                    case Utils.AnchorPosition.Left:
                        x2 = 0;
                        break;
                    case Utils.AnchorPosition.Right:
                        x2 = (int)((double)Width - (double)width1 * (double)num3);
                        break;
                    default:
                        x2 = (int)(((double)Width - (double)width1 * (double)num3) / 2.0);
                        break;
                }
            }
            int width2 = (int)((double)width1 * (double)num3);
            int height2 = (int)((double)height1 * (double)num3);
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            bitmap.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
            Graphics graphics = Graphics.FromImage((Image)bitmap);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(imgPhoto, new Rectangle(x2, y2, width2, height2), new Rectangle(x1, y1, width1, height1), GraphicsUnit.Pixel);
            graphics.Dispose();
            return (Image)bitmap;
        }

        public enum AnchorPosition
        {
            Top,
            Center,
            Bottom,
            Left,
            Right,
        }
    }

}
