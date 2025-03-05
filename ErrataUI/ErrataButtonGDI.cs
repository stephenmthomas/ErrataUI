using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataButtonGDI : Button
    {
        private int radius = 0;
        [DefaultValue(0)]
        public int Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                this.RecreateRegion();
            }
        }



        //Fields

        private Color _hoverBackColor = Color.FromArgb(16, 110, 190);
        private Color _hoverForeColor = Color.White;
        private Color _borderColor = Color.FromArgb(128, 128, 128); // Default border color
        private Color _backColor = Color.FromArgb(0, 128, 200);
        private Color _foreColor = Color.White;
        private bool _isMouseOver = false;
        private int _borderSize = 1;



        //Properties





        // Properties to customize hover and default colors
        public Color HoverBackColor
        {
            get => _hoverBackColor;
            set => _hoverBackColor = value;
        }


        public Color HoverForeColor
        {
            get => _hoverForeColor;
            set => _hoverForeColor = value;
        }

        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        public Color BackgroundColor
        {
            get => _backColor; set { _backColor = value; Invalidate(); }
        }
        public Color TextColor
        {
            get => _foreColor; set { _foreColor = value; Invalidate(); }
        }

        public int BorderSize
        {
            get => _borderSize;
            set { _borderSize = value; Invalidate(); }
        }


        //Constructor
        public ErrataButtonGDI()
        {
            Size = new Size(140, 35);
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            BackColor = Color.FromArgb(0, 128, 200);
            FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 90, 158);
            ForeColor = Color.White;
            Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular);
            Padding = new Padding(0, 0, 0, 3);

        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void RecreateRegion()
        {
            var bounds = ClientRectangle;
            //using (var path = GetRoundRectagle(bounds, this.Radius))
            //    this.Region = new Region(path);

            //Better round rectangle
            this.Region = Region.FromHrgn(CreateRoundRectRgn(bounds.Left, bounds.Top,
                bounds.Right, bounds.Bottom, Radius, radius));
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Set smoothing mode for anti-aliased edges
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //BorderRadius
            RectangleF rectangleF = new RectangleF(-1, -1, this.Width, this.Height);
            e.Graphics.DrawRectangle(new Pen(_borderColor), rectangleF);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.RecreateRegion();
        }


        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isMouseOver = true;
            BackColor = _hoverBackColor;
            ForeColor = _hoverForeColor;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isMouseOver = false;
            BackColor = _backColor;
            ForeColor = _foreColor;
        }


    }

    


}

