using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrataUI
{
    internal class RoundingPaths
    {
        public static GraphicsPath RoundRect(Rectangle Rectangle, int Curve)
        {
            GraphicsPath GP = new();

            int ArcRectangleWidth = Curve * 2;

            GP.AddArc(new(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90);
            GP.AddArc(new(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90);
            GP.AddArc(new(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90);
            GP.AddArc(new(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90);
            GP.AddLine(new Point(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y), new Point(Rectangle.X, Curve + Rectangle.Y));

            return GP;
        }

        public static GraphicsPath RoundRect(int X, int Y, int Width, int Height, int Curve)
        {
            Rectangle Rectangle = new(X, Y, Width, Height);

            GraphicsPath GP = new();

            int EndArcWidth = Curve * 2;

            GP.AddArc(new(Rectangle.X, Rectangle.Y, EndArcWidth, EndArcWidth), -180, 90);
            GP.AddArc(new(Rectangle.Width - EndArcWidth + Rectangle.X, Rectangle.Y, EndArcWidth, EndArcWidth), -90, 90);
            GP.AddArc(new(Rectangle.Width - EndArcWidth + Rectangle.X, Rectangle.Height - EndArcWidth + Rectangle.Y, EndArcWidth, EndArcWidth), 0, 90);
            GP.AddArc(new(Rectangle.X, Rectangle.Height - EndArcWidth + Rectangle.Y, EndArcWidth, EndArcWidth), 90, 90);
            GP.AddLine(new Point(Rectangle.X, Rectangle.Height - EndArcWidth + Rectangle.Y), new Point(Rectangle.X, Curve + Rectangle.Y));

            return GP;
        }

        public static GraphicsPath RoundedTopRect(Rectangle Rectangle, int Curve)
        {
            GraphicsPath GP = new();

            int ArcRectangleWidth = Curve * 2;

            GP.AddArc(new(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90);
            GP.AddArc(new(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90);
            GP.AddLine(new Point(Rectangle.X + Rectangle.Width, Rectangle.Y + ArcRectangleWidth), new Point(Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height - 1));
            GP.AddLine(new Point(Rectangle.X, Rectangle.Height - 1 + Rectangle.Y), new Point(Rectangle.X, Rectangle.Y + Curve));

            return GP;
        }

        public static GraphicsPath CreateRoundRect(float X, float Y, float Width, float Height, float Radius)
        {
            GraphicsPath GP = new();
            GP.AddLine(X + Radius, Y, X + Width - (Radius * 2), Y);
            GP.AddArc(X + Width - (Radius * 2), Y, Radius * 2, Radius * 2, 270, 90);

            GP.AddLine(X + Width, Y + Radius, X + Width, Y + Height - (Radius * 2));
            GP.AddArc(X + Width - (Radius * 2), Y + Height - (Radius * 2), Radius * 2, Radius * 2, 0, 90);

            GP.AddLine(X + Width - (Radius * 2), Y + Height, X + Radius, Y + Height);
            GP.AddArc(X, Y + Height - (Radius * 2), Radius * 2, Radius * 2, 90, 90);

            GP.AddLine(X, Y + Height - (Radius * 2), X, Y + Radius);
            GP.AddArc(X, Y, Radius * 2, Radius * 2, 180, 90);

            GP.CloseFigure();

            return GP;
        }

        public static GraphicsPath CreateUpRoundRect(float X, float Y, float Width, float Height, float Radius)
        {
            GraphicsPath GP = new();

            GP.AddLine(X + Radius, Y, X + Width - (Radius * 2), Y);
            GP.AddArc(X + Width - (Radius * 2), Y, Radius * 2, Radius * 2, 270, 90);

            GP.AddLine(X + Width, Y + Radius, X + Width, Y + Height - (Radius * 2) + 1);
            GP.AddArc(X + Width - (Radius * 2), Y + Height - (Radius * 2), Radius * 2, 2, 0, 90);

            GP.AddLine(X + Width, Y + Height, X + Radius, Y + Height);
            GP.AddArc(X, Y + Height - (Radius * 2) + 1, Radius * 2, 1, 90, 90);

            GP.AddLine(X, Y + Height, X, Y + Radius);
            GP.AddArc(X, Y, Radius * 2, Radius * 2, 180, 90);

            GP.CloseFigure();

            return GP;
        }

        public static GraphicsPath CreateLeftRoundRect(float X, float Y, float Width, float Height, float Radius)
        {
            GraphicsPath GP = new();
            GP.AddLine(X + Radius, Y, X + Width - (Radius * 2), Y);
            GP.AddArc(X + Width - (Radius * 2), Y, Radius * 2, Radius * 2, 270, 90);

            GP.AddLine(X + Width, Y + 0, X + Width, Y + Height);
            GP.AddArc(X + Width - (Radius * 2), Y + Height - 1, Radius * 2, 1, 0, 90);

            GP.AddLine(X + Width - (Radius * 2), Y + Height, X + Radius, Y + Height);
            GP.AddArc(X, Y + Height - (Radius * 2), Radius * 2, Radius * 2, 90, 90);

            GP.AddLine(X, Y + Height - (Radius * 2), X, Y + Radius);
            GP.AddArc(X, Y, Radius * 2, Radius * 2, 180, 90);

            GP.CloseFigure();

            return GP;
        }

        public static Color BlendColor(Color BackgroundColor, Color FrontColor)
        {
            double Ratio = 0 / 255d;
            double InvRatio = 1d - Ratio;

            int R = (int)((BackgroundColor.R * InvRatio) + (FrontColor.R * Ratio));
            int G = (int)((BackgroundColor.G * InvRatio) + (FrontColor.G * Ratio));
            int B = (int)((BackgroundColor.B * InvRatio) + (FrontColor.B * Ratio));

            return Color.FromArgb(R, G, B);
        }

        public static Color BackColor = ColorTranslator.FromHtml("#DADCDF"); //BCBFC4
        public static Color DarkBackColor = ColorTranslator.FromHtml("#90949A");
        public static Color LightBackColor = ColorTranslator.FromHtml("#F5F5F5");
    }
}

