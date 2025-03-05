using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    //[ToolboxItem(true)]
    //[Category("Errata UI")]
    //[DisplayName("Button [Standard]")]
    //[Description("A standard, modern button.")]
    public class ErrataButton : Button
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

        [Browsable(true)]
        [Category("Misc")]
        [Description("Gets or sets whether the control is selectable.")]
        public bool Selectable
        {
            get => (GetStyle(ControlStyles.Selectable));
            set
            {
                SetStyle(ControlStyles.Selectable, value);
                Invalidate(); // Force the control to redraw if needed
            }
        }

        private float _gradientAngle = 0F;
        [Category("Misc")]
        public float GradientAngle
        {
            get => _gradientAngle;
            set
            {
                _gradientAngle = value; Invalidate();
            }
        }


        private int _borderSize = 2;
        [Category("Misc")]
        public int BorderSize
        {
            get => _borderSize;
            set
            {
                _borderSize = value; Invalidate();
            }
        }

        private int _borderRadius = 0;
        [Category("Misc")]
        public int BorderRadius
        {
            get => _borderRadius;
            set
            {
                _borderRadius = value; Invalidate();
            }
        }

        private bool _gradientFill = true;
        [Category("Misc")]
        public bool GradientFill
        {
            get => _gradientFill;
            set
            {
                _gradientFill = value; Invalidate();
            }
        }












        private bool _ignoreMouseBackColor = false;
        [Category("Misc")]
        public bool IgnoreMouseBackColor
        {
            get => _ignoreMouseBackColor;
            set
            {
                _ignoreMouseBackColor = value;
            }
        }

        private bool _ignoreMouseForeColor = true;
        [Category("Misc")]
        public bool IgnoreMouseForeColor
        {
            get => _ignoreMouseForeColor;
            set
            {
                _ignoreMouseForeColor = value;
            }
        }




        #region MOUSEOVERBACKCOLOR
        //MOUSEOVERBACKCOLOR
        private UIRole _mouseOverBackColorRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _mouseOverBackColorTheme = ThemeColorShade.Primary_500;
        private Color _mouseOverBackColor = Color.FromArgb(0, 128, 200);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole MouseOverBackColorRole { get => _mouseOverBackColorRole; set { _mouseOverBackColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade MouseOverBackColorTheme
        {
            get => _mouseOverBackColorTheme; set
            {
                _mouseOverBackColorTheme = value;
                if (!_ignoreTheme) { MouseOverBackColor = ThemeManager.Instance.GetThemeColorShade(_mouseOverBackColorTheme); }
            }
        }
        [Category("Misc")]
        [Description("The background color of the button when the mouse hovers over it.")]
        public Color MouseOverBackColor
        {
            get => FlatAppearance.MouseOverBackColor;
            set => FlatAppearance.MouseOverBackColor = value;
        }
        #endregion
        #region MOUSEDOWNBACKCOLOR
        //MOUSEDOWNBACKCOLOR
        private UIRole _mouseDownBackColorRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _mouseDownBackColorTheme = ThemeColorShade.Primary_500;
        private Color _mouseDownBackColor = Color.FromArgb(0, 128, 200);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole MouseDownBackColorRole { get => _mouseDownBackColorRole; set { _mouseDownBackColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade MouseDownBackColorTheme
        {
            get => _mouseDownBackColorTheme; set
            {
                _mouseDownBackColorTheme = value;
                if (!_ignoreTheme) { MouseDownBackColor = ThemeManager.Instance.GetThemeColorShade(_mouseDownBackColorTheme); }
            }
        }
        [Category("Misc")]
        [Description("The background color of the button when the mouse is pressed.")]
        public Color MouseDownBackColor
        {
            get => FlatAppearance.MouseDownBackColor;
            set => FlatAppearance.MouseDownBackColor = value;
        }
        #endregion
        #region MOUSEOVERFORE
        //MOUSEOVERFORE COLOR
        private UIRole _mouseOverForeRole = UIRole.BodyTextL1;
        private ThemeColorShade _mouseOverForeTheme = ThemeColorShade.Neutral_400;
        private Color _mouseOverForeColor = Color.FromArgb(200, 200, 200);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole MouseOverForeRole { get => _mouseOverForeRole; set { _mouseOverForeRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade MouseOverForeTheme
        {
            get => _mouseOverForeTheme; set
            {
                _mouseOverForeTheme = value;
                if (!_ignoreTheme) { MouseOverForeColor = ThemeManager.Instance.GetThemeColorShade(_mouseOverForeTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color MouseOverForeColor { get => _mouseOverForeColor; set { _mouseOverForeColor = value; Invalidate(); } }
        #endregion
        #region MOUSEDOWNFORE
        //MOUSEDOWNFORE COLOR
        private UIRole _mouseDownForeRole = UIRole.BodyTextL1;
        private ThemeColorShade _mouseDownForeTheme = ThemeColorShade.Neutral_200;
        private Color _mouseDownForeColor = Color.FromArgb(200, 200, 200);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole MouseDownForeRole { get => _mouseDownForeRole; set { _mouseDownForeRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade MouseDownForeTheme
        {
            get => _mouseDownForeTheme; set
            {
                _mouseDownForeTheme = value;
                if (!_ignoreTheme) { MouseDownForeColor = ThemeManager.Instance.GetThemeColorShade(_mouseDownForeTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color MouseDownForeColor { get => _mouseDownForeColor; set { _mouseDownForeColor = value; Invalidate(); } }
        #endregion
        #region BORDER
        //BORDER COLOR
        private UIRole _borderRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _borderTheme = ThemeColorShade.Neutral_500;
        private Color _borderColor = Color.FromArgb(128, 128, 128);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole BorderRole { get => _borderRole; set { _borderRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade BorderTheme
        {
            get => _borderTheme; set
            {
                _borderTheme = value;
                if (!_ignoreTheme) { BorderColor = ThemeManager.Instance.GetThemeColorShade(_borderTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color BorderColor { get => _borderColor; set { _borderColor = value; Invalidate(); } }
        #endregion
        #region BACKCOLOR
        //BACKCOLOR
        private UIRole _backColorRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _backColorTheme = ThemeColorShade.Primary_500;
        private Color _backColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole BackColorRole { get => _backColorRole; set { _backColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade BackColorTheme
        {
            get => _backColorTheme; set
            {
                _backColorTheme = value;
                if (!_ignoreTheme) { BackColor = ThemeManager.Instance.GetThemeColorShade(_backColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color BackColor { get => _backColor; set { _backColor = value; base.BackColor = value; Invalidate(); } }
        #endregion
        #region FORECOLOR
        //TEXT COLOR
        private UIRole _foreColorRole = UIRole.PrimaryButtonText;
        private ThemeColorShade _foreColorTheme = ThemeColorShade.Neutral_100;
        private Color _textColor = Color.FromArgb(250, 250, 250);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole ForeColorRole { get => _foreColorRole; set { _foreColorRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade ForeColorTheme
        {
            get => _foreColorTheme; set
            {
                _foreColorTheme = value;
                if (!_ignoreTheme) { ForeColor = ThemeManager.Instance.GetThemeColorShade(_foreColorTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color ForeColor { get => _textColor; set { _textColor = value; base.ForeColor = value; Invalidate(); } }
        #endregion
        #region GRADIENTCOLOR
        //GRADIENTCOLOR
        private UIRole _gradientColorRole = UIRole.TitleBar;
        private ThemeColorShade _gradientColorTheme = ThemeColorShade.Primary_700;
        private Color _gradientColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_700);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole GradientColorRole { get => _gradientColorRole; set { _gradientColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade GradientColorTheme
        {
            get => _gradientColorTheme; set
            {
                _gradientColorTheme = value;
                if (!_ignoreTheme) { GradientColor = ThemeManager.Instance.GetThemeColorShade(_gradientColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color GradientColor { get => _gradientColor; set { _gradientColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion




        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                if (ThemeManager.Instance.IsDarkMode)
                {
                    BackColor = ThemeManager.Instance.GetThemeColorShadeOffset(BackColorTheme, -2);
                    GradientColor = ThemeManager.Instance.GetThemeColorShadeOffset(GradientColorTheme, 2);
                    BorderColor = ThemeManager.Instance.GetThemeColorShadeOffset(BorderTheme);
                    ForeColor = ThemeManager.Instance.GetThemeColorShadeOffset(ForeColorTheme, -8);
                    MouseDownBackColor = ThemeManager.Instance.GetThemeColorShadeOffset(MouseDownBackColorTheme, -2);
                    MouseOverBackColor = ThemeManager.Instance.GetThemeColorShadeOffset(MouseOverBackColorTheme, -2);
                    
                    return;
                }

                BackColor = ThemeManager.Instance.GetThemeColorShade(BackColorTheme);
                BorderColor = ThemeManager.Instance.GetThemeColorShade(BorderTheme);
                ForeColor = ThemeManager.Instance.GetThemeColorShade(ForeColorTheme);
                MouseDownBackColor = ThemeManager.Instance.GetThemeColorShade(MouseDownBackColorTheme);
                MouseOverBackColor = ThemeManager.Instance.GetThemeColorShade(MouseOverBackColorTheme);
                GradientColor = ThemeManager.Instance.GetThemeColorShadeOffset(GradientColorTheme);


                BackColorDarker = ControlPaint.Dark(ThemeManager.Instance.GetThemeColorShade(BackColorTheme),0.005f);
                BackColorLighter = ControlPaint.Light(ThemeManager.Instance.GetThemeColorShade(BackColorTheme),0.2f);
                GradColorDarker = ControlPaint.Dark(ThemeManager.Instance.GetThemeColorShade(GradientColorTheme), 0.15f);
                GradColorLighter = ControlPaint.Light(ThemeManager.Instance.GetThemeColorShade(GradientColorTheme), 0.05f);


            }
        }




        private ButtonType _buttonType;
        [Browsable(true)]
        [Category("Misc")]
        [Description("Sets the button type.")]
        public ButtonType Type
        {
            get => _buttonType;
            set
            {
                _buttonType = value;
                UpdateType();
            }
        }

        public enum ButtonType
        {
            None,
            Primary,
            Secondary,
            Outlined,
            Text,
            SemanticA,
            SemanticB,
            SemanticC
        }

        private void UpdateType()
        {
            // Update the Text property based on the ButtonType
            switch (_buttonType)
            {
                case ButtonType.None:
                    break;
                case ButtonType.Primary:
                    Padding = new Padding(0, 0, 0, 0);
                    BorderRadius = 0;
                    BorderSize = 0;
                    MouseDownBackColorTheme = ThemeColorShade.Primary_400;
                    BackColorTheme = ThemeColorShade.Primary_500;
                    MouseOverBackColorTheme = ThemeColorShade.Primary_600;
                    ForeColorTheme = ThemeColorShade.Neutral_100;
                    break;
                case ButtonType.Secondary:
                    Padding = new Padding(0, 0, 0, 0);
                    BorderRadius = 0;
                    BorderSize = 0;
                    MouseDownBackColorTheme = ThemeColorShade.Secondary_400;
                    BackColorTheme = ThemeColorShade.Secondary_500;
                    MouseOverBackColorTheme = ThemeColorShade.Secondary_600;                 
                    ForeColorTheme = ThemeColorShade.Neutral_100;
                    break;
                case ButtonType.SemanticA:
                    Padding = new Padding(0, 0, 0, 0);
                    BorderRadius = 0;
                    BorderSize = 0;
                    MouseDownBackColorTheme = ThemeColorShade.SemanticA_400;
                    BackColorTheme = ThemeColorShade.SemanticA_500;
                    MouseOverBackColorTheme = ThemeColorShade.SemanticA_600;
                    ForeColorTheme = ThemeColorShade.Neutral_100;
                    break;
                case ButtonType.SemanticB:
                    Padding = new Padding(0, 0, 0, 0);
                    BorderRadius = 0;
                    BorderSize = 0;
                    MouseDownBackColorTheme = ThemeColorShade.SemanticB_400;
                    BackColorTheme = ThemeColorShade.SemanticB_500;
                    MouseOverBackColorTheme = ThemeColorShade.SemanticB_600;
                    ForeColorTheme = ThemeColorShade.Neutral_100;
                    break;
                case ButtonType.SemanticC:
                    Padding = new Padding(0, 0, 0, 0);
                    BorderRadius = 0;
                    BorderSize = 0;
                    MouseDownBackColorTheme = ThemeColorShade.SemanticC_400;
                    BackColorTheme = ThemeColorShade.SemanticC_500;
                    MouseOverBackColorTheme = ThemeColorShade.SemanticC_600;
                    ForeColorTheme = ThemeColorShade.Neutral_100;
                    break;
                case ButtonType.Outlined:
                    Padding = new Padding(0, 0, 0, 0);
                    BorderRadius = 0;
                    BorderSize = 1;
                    BorderTheme = ThemeColorShade.Neutral_500;
                    BackColorTheme = ThemeColorShade.Transparent;
                    BackColor = Color.Transparent;
                    ForeColorTheme = ThemeColorShade.Primary_500;
                    break;
                case ButtonType.Text:
                    Padding = new Padding(0, 0, 0, 0);
                    BorderRadius = 0;
                    BorderSize = 0;
                    BorderTheme = ThemeColorShade.Neutral_500;
                    BackColorTheme = ThemeColorShade.Transparent;
                    BackColor = Color.Transparent;
                    ForeColorTheme = ThemeColorShade.Primary_500;
                    break;
            }
        }


        public Color BackColorDarker; public Color BackColorLighter;
        public Color GradColorDarker; public Color GradColorLighter;

        //Constructor
        public ErrataButton()
        {
            this.DoubleBuffered = true;
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular);
            Size = new Size(140, 35);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Resize += new EventHandler(Button_Resize);

            
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
            RectangleF rectangleBorder = new RectangleF(0,0, Width, Height);
            
            //FILL BACK COLOR

            if (GradientFill)
            {
                Color bgCol = _backColor;
                Color gradCol = _gradientColor;

                if (_isHovered) { bgCol = BackColorLighter; gradCol = GradColorDarker; }
                if (_isPressed) { bgCol = BackColorDarker; gradCol = GradColorLighter; }


                //LINEAR GRADIENT BRUSH
                LinearGradientBrush brushErrata = new LinearGradientBrush(this.ClientRectangle, bgCol, gradCol, this.GradientAngle);
                Graphics graphicsErrata = prevent.Graphics;
                graphicsErrata.FillRectangle(brushErrata, ClientRectangle);

                //TEXT BRUSH
                SolidBrush textBrush = new SolidBrush(this.ForeColor);
                StringFormat stringFormat = new StringFormat();

                // Set horizontal alignment
                if (TextAlign == ContentAlignment.MiddleLeft || TextAlign == ContentAlignment.TopLeft || TextAlign == ContentAlignment.BottomLeft)
                {
                    stringFormat.Alignment = StringAlignment.Near;
                }
                else if (TextAlign == ContentAlignment.MiddleCenter || TextAlign == ContentAlignment.TopCenter || TextAlign == ContentAlignment.BottomCenter)
                {
                    stringFormat.Alignment = StringAlignment.Center;
                }
                else
                {
                    stringFormat.Alignment = StringAlignment.Far;
                }

                // Set vertical alignment
                if (TextAlign == ContentAlignment.TopLeft || TextAlign == ContentAlignment.TopCenter || TextAlign == ContentAlignment.TopRight)
                {
                    stringFormat.LineAlignment = StringAlignment.Near;
                }
                else if (TextAlign == ContentAlignment.MiddleLeft || TextAlign == ContentAlignment.MiddleCenter || TextAlign == ContentAlignment.MiddleRight)
                {
                    stringFormat.LineAlignment = StringAlignment.Center;
                }
                else
                {
                    stringFormat.LineAlignment = StringAlignment.Far;
                }


                //fill the client area (the button) with the gradient brush... and text
                
                graphicsErrata.DrawString(this.Text, this.Font, textBrush, rectangleSurface, stringFormat);
            }

            //if the radius is above 1..the edges are trying to curve.
            using (Pen penBorder = new Pen(BorderColor, _borderSize))
            {
                penBorder.Alignment = PenAlignment.Inset;
                if (_borderRadius > 1)
                {
                    using (GraphicsPath graphicsPathSurface = GetFigurePath(rectangleSurface, _borderRadius))
                    using (GraphicsPath graphicsPathBorder = GetFigurePath(rectangleBorder, _borderRadius - 0.55F))
                    {
                        Region = new Region(graphicsPathSurface);
                        if (_borderSize > 0)
                        {
                            prevent.Graphics.DrawPath(penBorder, graphicsPathSurface);
                            prevent.Graphics.DrawPath(penBorder, graphicsPathBorder);
                        }
                    }
                }
                else if (_borderSize > 0) //if the button is a square...
                {
                    Region = new Region(rectangleSurface);
                    prevent.Graphics.DrawRectangle(penBorder, 0, 0, Width - 1, Height - 1);                 
                }
            }
            
            
        }

        private bool _isHovered = false;
        private bool _isPressed = false;

        protected override void OnMouseEnter(EventArgs e)
        {
            
            base.OnMouseEnter(e);
            _isHovered = true;
            Invalidate(); // Redraw the button
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            
            base.OnMouseLeave(e);
            _isHovered = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            
            base.OnMouseDown(mevent);
            _isPressed = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            
            base.OnMouseUp(mevent);
            _isPressed = false;
            Invalidate();
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
            if (_borderRadius > Height) _borderRadius = Height;
            Invalidate();
        }

    }
}
