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
    public class ErrataPanelArc : Panel
    {
        private int radius = 180;
        [DefaultValue(180)]
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


        private Color _backColor = Color.FromArgb(0, 128, 200);



        public Color BackgroundColor
        {
            get => _backColor; set { _backColor = value; Invalidate(); }
        }




        //Constructor
        public ErrataPanelArc()
        {
            Size = new Size(100,100);
            BackColor = Color.FromArgb(0, 128, 200);
            ForeColor = Color.White;
            Padding = new Padding(0, 0, 0, 0);

        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);


        private void RecreateRegion()
        {
            var bounds = ClientRectangle;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(bounds.Left, bounds.Top,bounds.Right, bounds.Bottom, Radius, radius));
            this.Invalidate();
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);



        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.RecreateRegion();
        }




    }
}
