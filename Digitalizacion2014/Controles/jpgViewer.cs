using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Digitalizacion2014.Controles
{
    public partial class jpgViewer : UserControl
    {
        private Image image;
        private System.Windows.Forms.PictureBox pbox;
        private double xzoom;

        private bool isControlKeyDown;

        private int _xPos;
        private int _yPos;
        private bool _dragging;

        public jpgViewer()
        {
            InitializeComponent();

            pbox.MouseUp += (sender, args) =>
            {
                var c = sender as PictureBox;
                if (null == c) return;
                this.pbox.Cursor = Cursors.Default;
                _dragging = false;
            };

            pbox.MouseDown += (sender, args) =>
            {
                if (args.Button != MouseButtons.Left) return;
                this.pbox.Cursor = Cursors.NoMove2D;
                _dragging = true;
                _xPos = args.X;
                _yPos = args.Y;
            };

            pbox.MouseMove += (sender, args) =>
            {
                var c = sender as PictureBox;
                if (!_dragging || null == c) return;
                c.Top = args.Y + c.Top - _yPos;
                c.Left = args.X + c.Left - _xPos;
            };
        }

        public Image Image
        {
            set
            {
                if (value != null)
                {
                    image = value;
                    Zoom = 100;
                }
                else
                {
                    clearimage();
                    image = null;
                }
            }
        }

        public int ImageWidth
        {
            get
            {
                if (image != null)
                {
                    return image.Width;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int ImageHeight
        {
            get
            {
                if (image != null)
                {
                    return image.Height;
                }
                else
                {
                    return 0;
                }
            }
        }

        public double Zoom
        {
            set
            {
                if (image != null)
                {
                    xzoom = Math.Max(Math.Min(value, 1000), 10);
                    double displayWidth = image.Width * ((double)xzoom / 100);
                    double displayHeight = image.Height * ((double)xzoom / 100) * (image.HorizontalResolution / (double)image.VerticalResolution);
                    pbox.Image = image;
                    pbox.Width = (int)displayWidth;
                    pbox.Height = (int)displayHeight;
                    if (ZoomChanged != null)
                    {
                        ZoomChanged.Invoke(this, new EventArgs());
                    }
                }
            }
            get
            { return xzoom; }
        }

        public event EventHandler<EventArgs> ZoomChanged;

        private void clearimage()
        {
            pbox.Image = null;
            pbox.Width = 1;
            pbox.Height = 1;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (isControlKeyDown)
            {
                StepZoom(e.Delta / (double)SystemInformation.MouseWheelScrollDelta);
            }
            else
            {
                base.OnMouseWheel(e);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            isControlKeyDown = e.Control;
            switch (e.KeyCode)
            {
                case Keys.OemMinus:
                    if (e.Control)
                    {
                        StepZoom(-1);
                    }
                    break;
                case Keys.Oemplus:
                    if (e.Control)
                    {
                        StepZoom(1);
                    }
                    break;
            }
        } 

        public void StepZoom(double steps)
        {
            Zoom = Math.Round(Zoom * Math.Pow(1.2, steps));
        }

        private void jpgViewer_KeyDown(object sender, KeyEventArgs e)
        {
            isControlKeyDown = e.Control;
        }

        private void jpgViewer_KeyUp(object sender, KeyEventArgs e)
        {
            isControlKeyDown = e.Control;
        }

    }
}
