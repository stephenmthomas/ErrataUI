using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace ErrataUI
{
    public class ErrataToggleButton : CheckBox
    {
        //properties
        private Color onBackColor = Color.MediumSlateBlue;
        private Color onForeColor = Color.WhiteSmoke;
        private Color offBackColor = Color.Gray;
        private Color offForeColor = Color.Gainsboro;
        private bool solidStyle = true;

        public Color OnBackColor
        {
            get
            {
                return onBackColor;
            }
            set
            {
                onBackColor = value;
                this.Invalidate();
            }
        }
        public Color OnForeColor
        {
            get
            {
                return onForeColor;
            }
            set
            {
                onForeColor = value;
                this.Invalidate();
            }
        }
        public Color OffBackColor
        {
            get
            {
                return offBackColor;
            }
            set
            {
                offBackColor = value;
                this.Invalidate();
            }
        }
        public Color OffForeColor
        {
            get
            {
                return offForeColor;
            }
            set
            {
                offForeColor = value;
                this.Invalidate();
            }
        }
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.Invalidate();
            }
        }
        [DefaultValue(true)]
        public bool SolidStyle
        {
            get
            {
                return solidStyle;
            }
            set
            {
                solidStyle = value;
                this.Invalidate();
            }
        }

        //constructor
        public ErrataToggleButton()
        {
            this.MinimumSize = new Size(45, 22);
        }
        private GraphicsPath GetFigurePath()
        {
            int arcSize = this.Height - 1;
            Rectangle leftArc = new Rectangle(0, 0, arcSize, arcSize);
            Rectangle rightArc = new Rectangle(this.Width - arcSize - 2, 0, arcSize, arcSize);

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(leftArc, 90, 180);
            path.AddArc(rightArc, 270, 180);
            path.CloseFigure();

            return path;
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            int toggleSize = this.Height - 5;
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.Clear(this.Parent.BackColor);

            if (this.Checked) //if ON
            {
                //Draw control surfce
                if (solidStyle)
                    pevent.Graphics.FillPath(new SolidBrush(onBackColor), GetFigurePath());
                else
                    pevent.Graphics.DrawPath(new Pen(onBackColor, 2), GetFigurePath());
                //Draw toggle
                pevent.Graphics.FillEllipse(new SolidBrush(onForeColor), new Rectangle(this.Width - this.Height + 1, 2, toggleSize, toggleSize));
            }
            else //if OFF
            {
                //Draw control surface
                if (solidStyle)
                    pevent.Graphics.FillPath(new SolidBrush(offBackColor), GetFigurePath());
                else pevent.Graphics.DrawPath(new Pen(offBackColor, 2), GetFigurePath());
                //Draw toggle
                pevent.Graphics.FillEllipse(new SolidBrush(offForeColor), new Rectangle(2, 2, toggleSize, toggleSize));
            }
        }
    }
}