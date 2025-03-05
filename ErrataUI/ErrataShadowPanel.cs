using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataShadowPanel : Panel
    {
        // Shadow properties
        private bool _enableShadow = true;
        private int _shadowDepth = 3;
        private Color _shadowColor = Color.FromArgb(60, 0, 0, 0);

        [Category("Shadow")]
        public bool EnableShadow
        {
            get => _enableShadow;
            set { _enableShadow = value; UpdatePadding(); Invalidate(); }
        }

        [Category("Shadow")]
        public int ShadowDepth
        {
            get => _shadowDepth;
            set { _shadowDepth = value; UpdatePadding(); Invalidate(); }
        }

        [Category("Shadow")]
        public Color ShadowColor
        {
            get => _shadowColor;
            set { _shadowColor = value; Invalidate(); }
        }

        private float _shadowFocus = 0.95f;
        [Category("Shadow")]
        public float ShadowFocus
        {
            get => _shadowFocus;
            set { _shadowFocus = value; Invalidate(); }
        }





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

        private string _groupBoxText = "Custom Panel";
        private Font _groupBoxFont = new Font("Segoe UI", 9, FontStyle.Bold);
        private int _borderWidth = 1;
        private int _headerHeight = 30;
        private int _buffer = 0;
        private bool _colorCaptionBar = true;
        private bool _gradientFill = false;
        private int _leftPadding = 8;
        private int _topPadding = 6;
        private int _captionLineWeight = 1;  // Height of the line under the title
        private int _captionLineOffset = 0;
        private float _letterSpacing = -3.5F;
        private float _gradientAngle = 0F;
        private bool _showCaptionLine = true;
        private bool _completeCaptionLine = true;

        #region DESIGNER OPTIONS
        [Category("Misc")]
        [Description("Select label font options.")]
        public Font GroupBoxFont
        {
            get => _groupBoxFont;
            set
            {
                _groupBoxFont = value;
                Invalidate(); // Repaint the control when text changes
            }
        }




        [Category("Misc")]
        [Description("Sets text.")]
        public string GroupBoxText
        {
            get => _groupBoxText;
            set
            {
                _groupBoxText = value;
                Invalidate(); // Repaint the control when text changes
            }
        }

        [Category("Misc")]
        [Description("Fills the caption bar with a gradient.")]
        public bool GradientFill
        {
            get => _gradientFill;
            set
            {
                _gradientFill = value;
                Invalidate(); // Repaint the control when color changes
            }
        }

        [Category("Misc")]
        [Description("Select to color the caption line area.")]
        public bool ColorCaptionLine
        {
            get => _colorCaptionBar;
            set
            {
                _colorCaptionBar = value;
                Invalidate(); // Repaint the control when color changes
            }
        }


        [Category("Misc")]
        [Description("Select to draw caption line entirely.")]
        public bool CompleteCaptionLine
        {
            get => _completeCaptionLine;
            set
            {
                _completeCaptionLine = value;
                Invalidate(); // Repaint the control when color changes
            }
        }

        [Category("Misc")]
        [Description("Select to draw caption line.")]
        public bool ShowCaptionLine
        {
            get => _showCaptionLine;
            set
            {
                _showCaptionLine = value;
                Invalidate(); // Repaint the control when color changes
            }
        }






        [Category("Misc.Lines")]
        [Description("Angle of the gradient.")]
        public float GradientAngle
        {
            get => _gradientAngle;
            set
            {
                _gradientAngle = value;
                Invalidate(); // Repaint the control when text changes
            }
        }


        [Category("Misc.Lines")]
        [Description("Sets the letter spacing.")]
        public float LetterSpacing
        {
            get => _letterSpacing;
            set
            {
                _letterSpacing = value;
                Invalidate(); // Repaint the control when text changes
            }
        }


        [Category("Misc.Lines")]
        [Description("Sets caption line height.")]
        public int CaptionLineWeight
        {
            get => _captionLineWeight;
            set
            {
                _captionLineWeight = value;
                Invalidate(); // Repaint the control when text changes
            }
        }

        [Category("Misc.Lines")]
        [Description("Adjusts the caption line height.")]
        public int CaptionLineOffset
        {
            get => _captionLineOffset;
            set
            {
                _captionLineOffset = value;
                Invalidate(); // Repaint the control when text changes
            }
        }


        [Category("Misc.Lines")]
        [Description("Sets the padding around the primary text.")]
        public int TopPadding
        {
            get => _topPadding;
            set
            {
                _topPadding = value;
                Invalidate(); // Repaint the control when text changes
            }
        }

        [Category("Misc.Lines")]
        [Description("Sets the padding around the primary text.")]
        public int LeftPadding
        {
            get => _leftPadding;
            set
            {
                _leftPadding = value;
                Invalidate(); // Repaint the control when text changes
            }
        }

        [Category("Misc.Lines")]
        [Description("Border width.")]
        public int GroupBoxBorderWidth
        {
            get => _borderWidth;
            set
            {
                _borderWidth = value;
                Invalidate(); // Repaint the control when border width changes
            }
        }
        #endregion



        #region PANELBACKCOLOR
        private ThemeColorShade _panelBackColorTheme = ThemeColorShade.Neutral_100;
        private Color _panelBackColor = Color.FromArgb(255, 255, 255);
        [Category("Theme Manager")]
        [Description("Sets caption bar shade.")]
        public ThemeColorShade PanelBackColorTheme
        {
            get => _panelBackColorTheme;
            set
            {
                _panelBackColorTheme = value;
                if (!_ignoreTheme) { PanelBackColor = ThemeManager.Instance.GetThemeColorShade(_panelBackColorTheme); }
            }
        }
        [Category("Misc.Color")] // Custom category
        [Description("Gets or sets the background color of the button.")]
        public new Color PanelBackColor
        {
            get => _panelBackColor;
            set => _panelBackColor = value;
        }
        #endregion
        #region BACKCOLOR
        private ThemeColorShade _backColorTheme = ThemeColorShade.Neutral_50;
        private Color _backColor = Color.FromArgb(255, 255, 255);
        [Category("Theme Manager")]
        [Description("Sets caption bar shade.")]
        public ThemeColorShade BackColorTheme
        {
            get => _backColorTheme;
            set
            {
                _backColorTheme = value;
                if (!_ignoreTheme) { BackColor = ThemeManager.Instance.GetThemeColorShade(_backColorTheme); }
            }
        }
        [Category("Misc.Color")] // Custom category
        [Description("Gets or sets the background color of the button.")]
        public new Color BackColor
        {
            get => _backColor;
            set => _backColor = value;
        }
        #endregion
        #region GRADIENT
        //GRADIENT COLOR
        private UIRole _gradientRole = UIRole.OverlayBackground;
        private ThemeColorShade _gradientTheme = ThemeColorShade.Primary_800;
        private Color _gradientColor = Color.FromArgb(0, 128, 200);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole GradientRole { get => _gradientRole; set { _gradientRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade GradientTheme
        {
            get => _gradientTheme; set
            {
                _gradientTheme = value;
                if (!_ignoreTheme) { GradientColor = ThemeManager.Instance.GetThemeColorShade(_gradientTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color GradientColor { get => _gradientColor; set { _gradientColor = value; Invalidate(); } }
        #endregion
        #region HEADERTEXT
        //HEADERTEXT COLOR
        private UIRole _headerTextRole = UIRole.PrimaryButtonText;
        private ThemeColorShade _headerTextTheme = ThemeColorShade.Neutral_100;
        private Color _groupBoxTextColor = Color.FromArgb(250, 250, 250);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole HeaderTextRole { get => _headerTextRole; set { _headerTextRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade HeaderTextTheme
        {
            get => _headerTextTheme; set
            {
                _headerTextTheme = value;
                if (!_ignoreTheme) { HeaderTextColor = ThemeManager.Instance.GetThemeColorShade(_headerTextTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color HeaderTextColor { get => _groupBoxTextColor; set { _groupBoxTextColor = value; Invalidate(); } }
        #endregion
        #region HEADERBACK
        //HEADERBACK COLOR
        private UIRole _headerBackRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _headerBackTheme = ThemeColorShade.Primary_500;
        private Color _captionBarColor = Color.FromArgb(0, 128, 200);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole HeaderBackRole { get => _headerBackRole; set { _headerBackRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade HeaderBackTheme
        {
            get => _headerBackTheme; set
            {
                _headerBackTheme = value;
                if (!_ignoreTheme) { HeaderBackColor = ThemeManager.Instance.GetThemeColorShade(_headerBackTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color HeaderBackColor { get => _captionBarColor; set { _captionBarColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region BORDER
        //BORDER COLOR
        private UIRole _borderRole = UIRole.GeneralBorders;
        private ThemeColorShade _borderTheme = ThemeColorShade.Neutral_600;
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


        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                BorderColor = ThemeManager.Instance.GetThemeColorShade(BorderTheme);
                HeaderBackColor = ThemeManager.Instance.GetThemeColorShade(HeaderBackTheme);
                HeaderTextColor = ThemeManager.Instance.GetThemeColorShade(HeaderTextTheme);
                GradientColor = ThemeManager.Instance.GetThemeColorShade(GradientTheme);
                PanelBackColor = ThemeManager.Instance.GetThemeColorShade(PanelBackColorTheme);
                BackColor = ThemeManager.Instance.GetThemeColorShade(BackColorTheme);
            }
        }

        public ErrataShadowPanel()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            Size = new Size(200, 150);
            UpdatePadding();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        [Category("Misc")]
        public int Buffer
        {
            get => _buffer;
            set { _buffer = value; Invalidate(); }
        }

        [Category("Misc")]
        public string HeaderText
        {
            get => _groupBoxText;
            set { _groupBoxText = value; Invalidate(); }
        }

        [Category("Misc")]
        public Font HeaderFont
        {
            get => _groupBoxFont;
            set { _groupBoxFont = value; Invalidate(); }
        }



        [Category("Misc")]
        public int BorderWidth
        {
            get => _borderWidth;
            set { _borderWidth = value; UpdatePadding(); Invalidate(); }
        }

        [Category("Misc")]
        public int HeaderHeight
        {
            get => _headerHeight;
            set { _headerHeight = value; UpdatePadding(); Invalidate(); }
        }

        [Category("Misc")]
        public bool ShowHeader
        {
            get => _colorCaptionBar;
            set { _colorCaptionBar = value; UpdatePadding(); Invalidate(); }
        }

        [Category("Misc")]
        public bool GradientHeader
        {
            get => _gradientFill;
            set { _gradientFill = value; Invalidate(); }
        }

        



        private void UpdatePadding()
        {
            int topPadding = _colorCaptionBar ? _headerHeight + _borderWidth : _borderWidth;
            int shadowPadding = _enableShadow ? _shadowDepth : 0;
            Padding = new Padding(
                _borderWidth + shadowPadding,
                topPadding + shadowPadding,
                _borderWidth + shadowPadding,
                _borderWidth + shadowPadding
            );
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int shadowOffset = _enableShadow ? _shadowDepth : 0;

           
            using (SolidBrush clientBrush = new SolidBrush(_backColor))
            {
                g.FillRectangle(clientBrush, ClientRectangle);
            }

            if (_enableShadow)
            {
                DrawShadow(g);
            }

            // Draw main panel
            Rectangle panelRect = new Rectangle(shadowOffset + _buffer, shadowOffset + _buffer, Width - 2 * shadowOffset - 1 - (2* _buffer), Height - 2 * shadowOffset - 1 - (2 * _buffer));
            using (SolidBrush panelBrush = new SolidBrush(_panelBackColor))
            {
                g.FillRectangle(panelBrush, panelRect);
            }

            // Draw border
            using (Pen borderPen = new Pen(_borderColor, _borderWidth))
            {
                g.DrawRectangle(borderPen, panelRect);
            }

            if (_colorCaptionBar)
            {
                // Draw header background
                Rectangle headerRect = new Rectangle(panelRect.X + _borderWidth, panelRect.Y + _borderWidth, panelRect.Width - 2 * _borderWidth, _headerHeight);
                if (_gradientFill)
                {
                    using (LinearGradientBrush brush = new LinearGradientBrush(headerRect, _captionBarColor, _gradientColor, LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(brush, headerRect);
                    }
                }
                else
                {
                    using (SolidBrush brush = new SolidBrush(_captionBarColor))
                    {
                        g.FillRectangle(brush, headerRect);
                    }
                }

                DrawGroupBoxText(g);

            }
        }

        private void DrawGroupBoxText(Graphics g)
        {
            // Define the area where the text will be drawn
            var textSize = g.MeasureString(_groupBoxText, _groupBoxFont);
            var textRect = new RectangleF(_leftPadding, _topPadding, textSize.Width, textSize.Height);

            // Create a solid brush with the text color
            using (Brush textBrush = new SolidBrush(_groupBoxTextColor))
            {

                float letterSpacing = _letterSpacing; // Adjust this value for desired spacing
                float x = _leftPadding; // Starting X position
                float y = _topPadding;  // Starting Y position

                foreach (char c in _groupBoxText)
                {
                    string character = c.ToString();
                    SizeF charSize = g.MeasureString(character, _groupBoxFont);
                    g.DrawString(character, _groupBoxFont, textBrush, new PointF(x, y));

                    // Adjust X position based on character width + spacing
                    x += charSize.Width + letterSpacing;

                    //g.DrawString(_groupBoxText, _groupBoxFont, textBrush, textRect);
                }
            }
        }

        private void DrawShadow(Graphics g)
        {
            // Set the shadow offset for down and to the right (i.e., the "depth" of the shadow)
            int shadowOffsetX = _shadowDepth; // The horizontal offset to the right
            int shadowOffsetY = _shadowDepth; // The vertical offset downwards


            using (GraphicsPath shadowPath = new GraphicsPath())
            {
                Rectangle shadowRect = new Rectangle(shadowOffsetX + _buffer, shadowOffsetY + _buffer, Width - _shadowDepth - (2 * _buffer), Height - _shadowDepth - (2 * _buffer));
                shadowPath.AddRectangle(shadowRect);

                using (PathGradientBrush shadowBrush = new PathGradientBrush(shadowPath))
                {
                    shadowBrush.CenterColor = _shadowColor;
                    shadowBrush.SurroundColors = new Color[] { Color.Transparent };
                    shadowBrush.FocusScales = new PointF(_shadowFocus, _shadowFocus);

                    g.FillPath(shadowBrush, shadowPath);
                }
            }
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            Invalidate();
        }
    }
}