using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection.Metadata;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Font = System.Drawing.Font;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataGroupPanelShadow : Panel
    {
        // Shadow properties
        private bool _enableShadow = true;
        private int _shadowDepth = 3;
        private int shadowOffset = 0;
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

        private int _buffer = 0;
        [Category("Shadow")]
        public int Buffer
        {
            get => _buffer;
            set { _buffer = value; Invalidate(); }
        }

        private void UpdatePadding()
        {
            int topPadding = _colorCaptionBar ? _topPadding + _borderWidth : _borderWidth;
            int shadowPadding = _enableShadow ? _shadowDepth : 0;
            Padding = new Padding(
                _borderWidth + shadowPadding + _buffer,
                topPadding + shadowPadding + _buffer,
                _borderWidth + shadowPadding + _buffer,
                _borderWidth + shadowPadding + _buffer
            );
        }





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

        #region UNDERCOLOR
        //UNDERCOLOR
        private UIRole _underColorRole = UIRole.MainBackground;
        private ThemeColorShade _underColorTheme = ThemeColorShade.Neutral_100;
        private Color _underColor = Color.FromArgb(250, 250, 250);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole UnderColorRole { get => _underColorRole; set { _underColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade UnderColorTheme
        {
            get => _underColorTheme; set
            {
                _underColorTheme = value;
                if (!_ignoreTheme) { UnderColor = ThemeManager.Instance.GetThemeColorShade(_underColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color UnderColor { get => _underColor; set { _underColor = value; Invalidate(); } }
        #endregion
        #region BACKCOLOR
        //BACKCOLOR
        private UIRole _backColorRole = UIRole.MainBackground;
        private ThemeColorShade _backColorTheme = ThemeColorShade.Neutral_100;
        private Color _backColor = Color.FromArgb(250, 250, 250);

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
        public Color BackColor { get => _backColor; set { _backColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region BORDERCOLOR
        //BORDERCOLOR
        private UIRole _borderColorRole = UIRole.GeneralBorders;
        private ThemeColorShade _borderColorTheme = ThemeColorShade.Neutral_500;
        private Color _borderColor = Color.FromArgb(128, 128, 128);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole BorderColorRole { get => _borderColorRole; set { _borderColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade BorderColorTheme
        {
            get => _borderColorTheme; set
            {
                _borderColorTheme = value;
                if (!_ignoreTheme) { BorderColor = ThemeManager.Instance.GetThemeColorShade(_borderColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color BorderColor { get => _borderColor; set { _borderColor = value; Invalidate(); } }
        #endregion
        #region CAPTIONLINECOLOR
        //CAPTIONLINECOLOR
        private UIRole _captionLineColorRole = UIRole.GeneralBorders;
        private ThemeColorShade _captionLineColorTheme = ThemeColorShade.Neutral_500;
        private Color _captionLineColor = Color.FromArgb(128, 128, 128);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole CaptionLineColorRole { get => _captionLineColorRole; set { _captionLineColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade CaptionLineColorTheme
        {
            get => _captionLineColorTheme; set
            {
                _captionLineColorTheme = value;
                if (!_ignoreTheme) { CaptionLineColor = ThemeManager.Instance.GetThemeColorShade(_captionLineColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color CaptionLineColor { get => _captionLineColor; set { _captionLineColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region CAPTIONBARCOLOR
        //CAPTIONBARCOLOR
        private UIRole _captionBarColorRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _captionBarColorTheme = ThemeColorShade.Primary_500;
        private Color _captionBarColor = Color.FromArgb(0, 128, 200);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole CaptionBarColorRole { get => _captionBarColorRole; set { _captionBarColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade CaptionBarColorTheme
        {
            get => _captionBarColorTheme; set
            {
                _captionBarColorTheme = value;
                if (!_ignoreTheme) { CaptionBarColor = ThemeManager.Instance.GetThemeColorShade(_captionBarColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color CaptionBarColor { get => _captionBarColor; set { _captionBarColor = value; Invalidate(); } }
        #endregion
        #region GRADIENTCOLOR
        //GRADIENTCOLOR
        private UIRole _gradientColorRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _gradientColorTheme = ThemeColorShade.Primary_700;
        private Color _gradientColor = Color.FromArgb(0, 128, 200);

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
        #region GROUPBOXTEXTCOLOR
        //GROUPBOXTEXTCOLOR
        private UIRole _groupBoxTextColorRole = UIRole.PrimaryButtonText;
        private ThemeColorShade _groupBoxTextColorTheme = ThemeColorShade.Neutral_100;
        private Color _groupBoxTextColor = Color.FromArgb(250, 250, 250);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole GroupBoxTextColorRole { get => _groupBoxTextColorRole; set { _groupBoxTextColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade GroupBoxTextColorTheme
        {
            get => _groupBoxTextColorTheme; set
            {
                _groupBoxTextColorTheme = value;
                if (!_ignoreTheme) { GroupBoxTextColor = ThemeManager.Instance.GetThemeColorShade(_groupBoxTextColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color GroupBoxTextColor { get => _groupBoxTextColor; set { _groupBoxTextColor = value; Invalidate(); } }
        #endregion

        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                UnderColor = ThemeManager.Instance.GetThemeColorShade(UnderColorTheme);
                BackColor = ThemeManager.Instance.GetThemeColorShade(BackColorTheme);
                BorderColor = ThemeManager.Instance.GetThemeColorShade(BorderColorTheme);
                GradientColor = ThemeManager.Instance.GetThemeColorShade(GradientColorTheme);
                GroupBoxTextColor = ThemeManager.Instance.GetThemeColorShade(GroupBoxTextColorTheme);
                CaptionBarColor = ThemeManager.Instance.GetThemeColorShade(CaptionBarColorTheme);
                CaptionLineColor = ThemeManager.Instance.GetThemeColorShade(CaptionLineColorTheme);
            }
        }



        private bool _ignoreTheme = false;  // New property to ignore theme updates
        private bool _ignoreRoles = true;
        private string _groupBoxText = "GroupBox";
        private Font _groupBoxFont = new Font("Segoe UI Semibold", 9, FontStyle.Regular);
        private int _borderWidth = 1;  // Border width
        private int _leftPadding = 8;
        private int _topPadding = 6;
        private int _captionLineWeight = 1;  // Height of the line under the title
        private int _captionLineOffset = 0;
        private float _letterSpacing = -3.5F;
        private float _gradientAngle = 0F;
        private bool _showCaptionLine = true;
        private bool _completeCaptionLine = true;
        private bool _colorCaptionBar = true;
        private bool _gradientFill = false;




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


        // Constructor 
        public ErrataGroupPanelShadow()
        {

            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            // Set the control style to prevent painting of the background and borders.
            Size = new Size(200, 100);  // Default size
            BackColor = Color.White;  // Transparent background by default
            Padding = new Padding(0, 26, 0, 0);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            shadowOffset = _enableShadow ? _shadowDepth : 0;

            base.OnPaint(e);
            using (Graphics g = e.Graphics)
            {
                g.Clear(UnderColor);
                
                if (_enableShadow)
                {
                    DrawShadow(g);
                }

                Rectangle panelRect = new Rectangle(shadowOffset + _buffer, shadowOffset + _buffer, Width - 2 * shadowOffset - 1 - (2 * _buffer), Height - 2 * shadowOffset - 1 - (2 * _buffer));
                using (SolidBrush panelBrush = new SolidBrush(BackColor))
                {
                    g.FillRectangle(panelBrush, panelRect);
                }


                if (_colorCaptionBar) { ShadeCaptionBar(g); }
                DrawGroupBoxText(g);
                DrawBorder(g);
                if (_showCaptionLine) { DrawCaptionLine(g); }
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

        private void ShadeCaptionBar(Graphics g)
        {
            float lineYPosition = _topPadding + g.MeasureString(_groupBoxText, _groupBoxFont).Height + 3;
            Rectangle rect = new Rectangle(shadowOffset + _buffer, shadowOffset + _buffer, Width - 2 * shadowOffset - 1 - (2 * _buffer), (int)lineYPosition);
            if (_gradientFill)
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(rect, _captionBarColor, _gradientColor, _gradientAngle))
                {
                    //g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.FillRectangle(brush, rect);
                }
            }
            else
            {
                using (Brush brush = new SolidBrush(_captionBarColor))
                {
                    g.FillRectangle(brush, rect);
                }
            }
        }

        private void DrawCaptionLine(Graphics g)
        {
            // Position the line just below the text
            float lineYPosition = shadowOffset + _buffer + _topPadding + g.MeasureString(_groupBoxText, _groupBoxFont).Height + 3;
            lineYPosition += _captionLineOffset;

            using (Pen linePen = new Pen(_captionLineColor, _captionLineWeight))
            {
                // Draw the horizontal line
                if (_completeCaptionLine)
                {
                    g.DrawLine(linePen, shadowOffset + _buffer, lineYPosition, Width - shadowOffset - _buffer - 1, lineYPosition);
                }
                else
                {
                    g.DrawLine(linePen, _topPadding, lineYPosition, Width - _topPadding, lineYPosition);
                }
            }
        }

        private void DrawGroupBoxText(Graphics g)
        {
            // Define the area where the text will be drawn
            var textSize = g.MeasureString(_groupBoxText, _groupBoxFont);
            
            // Create a solid brush with the text color
            using (Brush textBrush = new SolidBrush(_groupBoxTextColor))
            {

                float letterSpacing = _letterSpacing; // Adjust this value for desired spacing
                float x = _leftPadding + shadowOffset + _buffer; // Starting X position
                float y = _topPadding + shadowOffset + _buffer;  // Starting Y position

                foreach (char c in _groupBoxText)
                {
                    string character = c.ToString();
                    SizeF charSize = g.MeasureString(character, _groupBoxFont);
                    g.DrawString(character, _groupBoxFont, textBrush, new PointF(x, y));

                    // Adjust X position based on character width + spacing
                    x += charSize.Width + letterSpacing;

                }
            }
        }


        private void DrawBorder(Graphics g)
        {
            using (Pen borderPen = new Pen(_borderColor, _borderWidth))
            {
                Rectangle borderRect = new Rectangle(shadowOffset + _buffer, shadowOffset + _buffer, Width - 2 * shadowOffset - 1 - (2 * _buffer), Height - 2 * shadowOffset - 1 - (2 * _buffer));
                g.DrawRectangle(borderPen, borderRect);
            }
        }

        // Override the OnLayout method to ensure the size and layout of the children are handled properly
        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            // In this case, we're not doing any custom layout, so just call base method.
        }

        // Override the OnSizeChanged method to handle resizing
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

        }
    }
}