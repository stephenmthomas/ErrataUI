using System.Drawing.Drawing2D;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataButtonRadiant : Button
    {

        //Fields
        private int borderSize = 2;
        private int borderRadius = 280;
        private Color borderColor = Color.DimGray;
        private float gradientAngle = 0F;
        private Color gradientColor = Color.Gainsboro;
        private bool gradientFill = true;

        //Properties
        public int BorderSize
        {
            get => borderSize;
            set { borderSize = value; Invalidate(); }
        }
        public int BorderRadius
        {
            get => borderRadius;
            set { borderRadius = (value <= Height) ? value : Height; Invalidate(); }
        }
        public float GradientAngle
        {
            get => gradientAngle;
            set { gradientAngle = value; this.Invalidate(); }
        }

        public bool GradientFill
        {
            get => gradientFill;
            set { gradientFill = value; this.Invalidate(); }
        }


        public Color BorderColor
        {
            get => borderColor; set { borderColor = value; Invalidate(); }
        }

        public Color BackgroundColor
        {
            get => BackColor; set { BackColor = value; Invalidate(); }
        }
        public Color TextColor
        {
            get => ForeColor; set { ForeColor = value; Invalidate(); }
        }

        public Color GradientColor
        {
            get => gradientColor;
            set { gradientColor = value; this.Invalidate(); }
        }



        //Constructor
        public ErrataButtonRadiant()
        {

            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            BackColor = Color.FromArgb(0, 128, 200); ;
            ForeColor = Color.White;
            BorderColor = Color.DimGray;
            Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular);


            Resize += new EventHandler(Button_Resize);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            this.UpdateStyles();
        }




        //Methods for drawing curved corners
        private GraphicsPath GetFigurePath(RectangleF Rect, float radius)
        {
            float m = 2.75F;
            float r2 = radius / 2f;
            GraphicsPath GraphPath = new GraphicsPath();

            GraphPath.AddArc(Rect.X + m, Rect.Y + m, radius, radius, 180, 90);
            GraphPath.AddLine(Rect.X + r2 + m, Rect.Y + m, Rect.Width - r2 - m, Rect.Y + m);
            GraphPath.AddArc(Rect.X + Rect.Width - radius - m, Rect.Y + m, radius, radius, 270, 90);
            GraphPath.AddLine(Rect.Width - m, Rect.Y + r2, Rect.Width - m, Rect.Height - r2 - m);
            GraphPath.AddArc(Rect.X + Rect.Width - radius - m,
                           Rect.Y + Rect.Height - radius - m, radius, radius, 0, 90);
            GraphPath.AddLine(Rect.Width - r2 - m, Rect.Height - m, Rect.X + r2 - m, Rect.Height - m);
            GraphPath.AddArc(Rect.X + m, Rect.Y + Rect.Height - radius - m, radius, radius, 90, 90);
            GraphPath.AddLine(Rect.X + m, Rect.Height - r2 - m, Rect.X + m, Rect.Y + r2 + m);

            GraphPath.CloseFigure();
            return GraphPath;
        }




        protected override void OnPaint(PaintEventArgs prevent)
        {
            base.OnPaint(prevent);
            prevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            //rectangles
            RectangleF rectangleSurface = new RectangleF(0, 0, Width, Height);
            RectangleF rectangleBorder = new RectangleF(0, 0, Width, Height);

            //FILL BACK COLOR

            if (GradientFill)
            {
                Graphics graphicsErrata = prevent.Graphics;
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(rectangleSurface);
                //path.AddRectangle(rectangleBorder);
                PathGradientBrush pthGrBrush = new PathGradientBrush(path);
                pthGrBrush.CenterColor = Color.FromArgb(255, 255, 0, 0);

                // Set the colors of the points in the array.
                Color[] colors = {

                                   Color.FromArgb(255, 255, 255, 255),
                                   Color.FromArgb(255, 0, 0, 0),
 
                                   Color.FromArgb(255, 0, 0, 0),
                                   Color.FromArgb(255, 0, 255, 0)};

                pthGrBrush.SurroundColors = colors;


                //LINEAR GRADIENT BRUSH
                //LinearGradientBrush brushErrata = new LinearGradientBrush(this.ClientRectangle, this.BackColor, this.gradientColor, this.GradientAngle);
                graphicsErrata.FillRectangle(pthGrBrush, ClientRectangle);


                //NOW CULL EDGES
                prevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath graphicsPathSurface = GetFigurePath(rectangleSurface, borderRadius))
                using (GraphicsPath graphicsPathBorder = GetFigurePath(rectangleBorder, borderRadius - 0.55F))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    prevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    penBorder.Alignment = PenAlignment.Inset;
                    Region = new Region(graphicsPathSurface);
                    prevent.Graphics.DrawPath(penBorder, graphicsPathSurface);

                    if (borderSize >= 1) prevent.Graphics.DrawPath(penBorder, graphicsPathBorder);
                }
            }

       
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }

        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            if (DesignMode) Invalidate();
        }

        private void Button_Resize(object sender, EventArgs e)
        {
            if (borderRadius > Height) borderRadius = Height;
        }

    }
}
