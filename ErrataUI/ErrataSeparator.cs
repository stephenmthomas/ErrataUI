using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using static ErrataUI.ThemeManager;



namespace ErrataUI

{

        public class ErrataSeparator : Control
    {
        private bool _ignoreRoles = true;
        [Browsable(true)]
        [Category("Theme Manager")]
        [Description("If true, role updates will be ignored, allowing individual theme application.")]
        public bool IgnoreRoles
        {
            get => _ignoreRoles;
            set
            {
                _ignoreRoles = value;
            }
        }

        private bool _ignoreTheme;
        [Browsable(true)]
        [Category("Theme Manager")]
        [Description("If true, color updates will be ignored, allowing manual color selection.")]
        public bool IgnoreTheme
        {
            get => _ignoreTheme;
            set
            {
                _ignoreTheme = value;
                UpdateColor();  // Update color immediately if the property changes
            }
        }





        public enum LinePattern
        {
            Solid,
            Dash,
            Dot,
            DashDot,
            DashDotDot,
            DoubleEdgeFaded,
            LeftEdgeFaded,
            RightEdgeFaded
        }
        public enum lineStack
        {
            Single,
            Double,
            Triple,
        }

   

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            Invalidate();
        }



        private bool _isVertical;
        [Category("Misc")]
        public bool isVertical
        {
            get => _isVertical;
            set
            {
                _isVertical = value;
                CalcVertMult();
                if (_isVertical) { this.Size = new Size((int)vertMult, Width); }
                if (!_isVertical) { this.Size = new Size(Height,(int)vertMult); }
                Invalidate();
            }

        }

        private float vertMult = 0;
        public lineStack _lineStack = lineStack.Single;
        [Category("Misc")]
        public lineStack LineStack
        {
            get => _lineStack;
            set 
            { 
                _lineStack = value; 
                CalcVertMult();
                Invalidate(); 
            }
        }

        public LinePattern _linePattern = LinePattern.Solid;
        [Category("Misc")]
        public LinePattern linePattern
        {
            get => _linePattern;
            set { _linePattern = value; CalcVertMult(); Invalidate(); }
        }


        private float _thickness = 2f;
        [Category("Misc")]
        public float Thickness
        {
            get => _thickness;
            set
            {
                _thickness = value;
                CalcVertMult();
                if (Height < (int)_thickness)
                    Height = (int)_thickness;
                else
                    Invalidate();
            }
        }

        private int _offset = 2;
        [Category("Misc")]
        public int Offset
        {
            get => _offset;
            set
            {
                _offset = value;
                 Invalidate();
            }
        }





        #region LINE
        //LINE COLOR
        private UIRole _lineRole = UIRole.SectionDivider;
        private ThemeColorShade _lineTheme = ThemeColorShade.Primary_500;
        private Color _lineColor = Color.FromArgb(0, 128, 200);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole LineRole { get => _lineRole; set { _lineRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade LineTheme
        {
            get => _lineTheme; set
            {
                _lineTheme = value;
                if (!_ignoreTheme) { LineColor = ThemeManager.Instance.GetThemeColorShade(_lineTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color LineColor { get => _lineColor; set { _lineColor = value; Invalidate(); } }
        #endregion

        private void CalcVertMult()
        {
           

            switch (_lineStack)
            {
                case lineStack.Single:
                    vertMult = (int)_thickness;
                    break;
                case lineStack.Double:
                    vertMult = (int)_thickness * _offset + (int)_thickness;
                    break;
                case lineStack.Triple:
                    vertMult = (int)_thickness * _offset * 2 + (int)_thickness;
                    break;
            }

        }

        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                LineColor = ThemeManager.Instance.GetThemeColorShade(LineTheme);
            }
        }

        public ErrataSeparator()
        {
            MinimumSize = new Size(1, 1);
            Width = 50;
            Height = 5;
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e); // Don't paint the background
        }
        // Optionally, override OnResize to handle resizing events
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // You can also handle logic here if needed
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var sz = _isVertical ? Height / 2 : Width / 2;
            e.Graphics.TranslateTransform(Width / 2f, Height / 2f);

            // Define the pen style based on the LinePattern
            using (var pen = new Pen(LineColor, _thickness))
            {
                switch (_linePattern)
                {
                    case LinePattern.Dash:
                        pen.DashStyle = DashStyle.Dash;
                        break;
                    case LinePattern.Dot:
                        pen.DashStyle = DashStyle.Dot;
                        break;
                    case LinePattern.DashDot:
                        pen.DashStyle = DashStyle.DashDot;
                        break;
                    case LinePattern.DashDotDot:
                        pen.DashStyle = DashStyle.DashDotDot;
                        break;
                    default:
                        pen.DashStyle = DashStyle.Solid;
                        break;
                }

                // Handle LineStack: Single, Double, Triple
                switch (_lineStack)
                {
                    case lineStack.Single:
                        DrawSingleLine(e.Graphics, pen, sz);
                        break;
                    case lineStack.Double:
                        DrawDoubleLines(e.Graphics, pen, sz);
                        break;
                    case lineStack.Triple:
                        DrawTripleLines(e.Graphics, pen, sz);
                        break;
                }
            }
        }

        private void DrawSingleLine(Graphics g, Pen pen, int sz)
        {
            if (!_isVertical)
            {
                g.DrawLine(pen, -sz + Padding.Left, 0, sz - Padding.Right, 0);
            }
            else
            {
                g.DrawLine(pen, 0, -sz + Padding.Top, 0, sz - Padding.Bottom);
            }
        }

        private void DrawDoubleLines(Graphics g, Pen pen, int sz)
        {
            float offset = _thickness * _offset; // Distance between lines
            if (!_isVertical)
            {
                g.DrawLine(pen, -sz + Padding.Left, -offset / 2, sz - Padding.Right, -offset / 2);
                g.DrawLine(pen, -sz + Padding.Left, offset / 2, sz - Padding.Right, offset / 2);
            }
            else
            {
                g.DrawLine(pen, -offset / 2, -sz + Padding.Top, -offset / 2, sz - Padding.Bottom);
                //g.DrawLine(pen, 0, -sz + Padding.Top, 0, sz - Padding.Bottom);
                g.DrawLine(pen, offset / 2, -sz + Padding.Top, offset / 2, sz - Padding.Bottom);
            }
        }

        private void DrawTripleLines(Graphics g, Pen pen, int sz)
        {
            float offset = _thickness * _offset; // Distance between lines
            if (!_isVertical)
            {
                g.DrawLine(pen, -sz + Padding.Left, -offset, sz - Padding.Right, -offset);
                g.DrawLine(pen, -sz + Padding.Left, 0, sz - Padding.Right, 0);
                g.DrawLine(pen, -sz + Padding.Left, offset, sz - Padding.Right, offset);
                
            }
            else
            {
                g.DrawLine(pen, -offset, -sz + Padding.Top, -offset, sz - Padding.Bottom);
                g.DrawLine(pen, 0, -sz + Padding.Top, 0, sz - Padding.Bottom);
                g.DrawLine(pen, offset, -sz + Padding.Top, offset, sz - Padding.Bottom);
                
            }
        }
    }
}