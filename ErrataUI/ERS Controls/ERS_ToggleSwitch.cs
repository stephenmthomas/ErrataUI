using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ErrataUI
{
    public class ERS_ToggleSwitch : Control
    {
        private bool isChecked = false;

        public event EventHandler Toggled;

        public bool IsChecked
        {
            get => isChecked;
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    Toggled?.Invoke(this, EventArgs.Empty);
                    Invalidate(); // Redraw the control
                }
            }
        }
        private bool enableGeoControl = false;
        public bool EnableGeometricControl
        {
            get => enableGeoControl;
            set { enableGeoControl = value; Invalidate(); }
        }

        private Color offBackColor = Color.FromArgb(128, 128, 128);
        private Color onBackColor = Color.FromArgb(0, 128, 200);
        private Color thumbColor = Color.FromArgb(0, 90, 175);
        public Color OffBackColor
        {
            get => offBackColor;
            set { offBackColor = value; Invalidate(); }
        }

        public Color OnBackColor
        {
            get => onBackColor;
            set { onBackColor = value; Invalidate(); }
        }

        public Color ThumbColor
        {
            get => thumbColor;
            set { thumbColor = value; Invalidate(); }
        }




        private float trackX = 0;
        private float trackY = 0;
        private float trackWidth = 60;
        private float trackHeight = 30;
        private float trackRadius = 15;
        public float TrackX
        {
            get => trackX;
            set { trackX = value; Invalidate(); }
        }
        public float TrackY
        {
            get => trackY;
            set { trackY = value; Invalidate(); }
        }
        public float TrackWidth
        {
            get => trackWidth;
            set { trackWidth = value; Invalidate(); }
        }
        public float TrackHeight
        {
            get => trackHeight;
            set { trackHeight = value; Invalidate(); }
        }
        public float TrackRadius
        {
            get => trackRadius;
            set { trackRadius = value; Invalidate(); }
        }



        private int thumbX = 0;
        private int thumbY = 0;
        private int thumbWidth = 15;
        private int thumbHeight = 15;
        public int ThumbHeight
        {
            get => thumbHeight;
            set { thumbHeight = value; Invalidate(); }
        }
        public int ThumbWidth
        {
            get => thumbWidth;
            set { thumbWidth = value; Invalidate(); }
        }
        public int ThumbX
        {
            get => thumbX;
            set { thumbX = value; Invalidate(); }
        }
        public int ThumbY
        {
            get => thumbY;
            set { thumbY = value; Invalidate(); }
        }





        public ERS_ToggleSwitch()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            Size = new Size(60, 30); // Default size
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Define the track rectangle
            var trackRect = new RectangleF(0, Height / 4f, Width, Height / 2f);
            float radius = Height / 2f;

            if (enableGeoControl) { trackRect = new RectangleF(trackX, trackY, trackWidth, trackHeight); radius = trackRadius; }

            // Draw the track using arcs
            using (GraphicsPath trackPath = GetFigurePath(trackRect, radius))
            using (Brush trackBrush = new SolidBrush(IsChecked ? OnBackColor : OffBackColor))
            {
                g.FillPath(trackBrush, trackPath);
            }



            // Draw the thumb
            int thumbSize = Height - 6;
            int thumbXc = IsChecked ? Width - thumbSize - 3 : 3;
            var thumbRect = new Rectangle(thumbXc, (Height - thumbSize) / 2, thumbSize, thumbSize);
            if (enableGeoControl) { thumbRect = new Rectangle(thumbX, thumbY, thumbWidth, thumbHeight); }
            using (Brush thumbBrush = new SolidBrush(ThumbColor))
            {
                g.FillEllipse(thumbBrush, thumbRect);
            }
        }

        private GraphicsPath GetFigurePath(RectangleF rectangle, float radius)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.StartFigure();
            graphicsPath.AddArc(rectangle.X, rectangle.Y, radius, radius, 180, 90); // Top-left corner
            graphicsPath.AddArc(rectangle.Right - radius, rectangle.Y, radius, radius, 270, 90); // Top-right corner
            graphicsPath.AddArc(rectangle.Right - radius, rectangle.Bottom - radius, radius, radius, 0, 90); // Bottom-right corner
            graphicsPath.AddArc(rectangle.X, rectangle.Bottom - radius, radius, radius, 90, 90); // Bottom-left corner
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Left)
            {
                IsChecked = !IsChecked; // Toggle state
            }
        }
    }
}
