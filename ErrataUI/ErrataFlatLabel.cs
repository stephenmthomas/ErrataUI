using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataFlatLabel : Label
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

        private bool _preventEnumOverride = false;
        [Category("Misc")]
        [Description("Prevents style and type enums from reverting the control. Useful if you want to use type/style enums to set an appearance, and then modify it from there.")]
        public bool StyleOverride
        {
            get => _preventEnumOverride;
            set
            {
                _preventEnumOverride = value; Invalidate();
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

        [Category("Misc")]
        [Description("The text associated with this control.")]
        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                Invalidate(); // Redraw the control when text changes
            }
        }


        [Category("Misc")]
        public TextStyle Style
        {
            get => _style;
            set
            {
                _style = value;
                UpdateFont();
            }
        }

        private int _xoffset = 0;
        [Category("Misc")]
        public int OffsetX
        {
            get => _xoffset;
            set
            {
                _xoffset = value;
                UpdateFont();
            }
        }

        private int _yoffset = 0;
        [Category("Misc")]
        public int OffsetY
        {
            get => _yoffset;
            set
            {
                _yoffset = value;
                UpdateFont();
            }
        }



        private StringAlignment _textAlignment = StringAlignment.Near;
        private StringAlignment _lineAlignment = StringAlignment.Center;

        [Category("Misc")]
        [Description("Specifies the horizontal alignment of the text.")]
        public StringAlignment TextAlignment
        {
            get => _textAlignment;
            set
            {
                _textAlignment = value;
                Invalidate();
            }
        }

        [Category("Misc")]
        [Description("Specifies the vertical alignment of the text.")]
        public StringAlignment LineAlignment
        {
            get => _lineAlignment;
            set
            {
                _lineAlignment = value;
                Invalidate();
            }
        }


        [Category("Misc")]
        [DefaultValue(0f)]
        public float FontSpacing
        {
            get => _fontSpacing;
            set
            {
                _fontSpacing = value;
                Invalidate();
            }
        }

        public enum TextStyle
        {
            None,
            Display,
            Display2,
            Header1,
            Header2,
            Header3,
            Title,
            Subtitle,
            Body,
            BodyStrong,
            Caption,
            Small,
            VerySmall
        }


        #region FORE
        //FORE COLOR
        private UIRole _foreRole = UIRole.BodyTextL1;
        private ThemeColorShade _foreTheme = ThemeColorShade.Neutral_800;
        private Color _foreColor = Color.FromArgb(50, 49, 48);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole ForeRole { get => _foreRole; set { _foreRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade ForeTheme
        {
            get => _foreTheme; set
            {
                _foreTheme = value;
                if (!_ignoreTheme) { ForeColor = ThemeManager.Instance.GetThemeColorShade(_foreTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color ForeColor { get => _foreColor; set { _foreColor = value; Invalidate(); } }
        #endregion




        private TextStyle _style = TextStyle.Body;
        private float _fontSpacing = 0f;

        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                if (ThemeManager.Instance.IsDarkMode)
                {
                    ForeColor = ThemeManager.Instance.GetThemeColorShadeOffset(ForeTheme, -6);
                }
                
                ForeColor = ThemeManager.Instance.GetThemeColorShade(ForeTheme);
            }
        }

        public ErrataFlatLabel()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true); 
            BackColor = Color.Transparent;
            UpdateFont();
        }



        private void UpdateFont()
        {
            if (StyleOverride) { return; }

            switch (_style)
            {
                case TextStyle.None:
                    break;
                case TextStyle.Display:
                    Font = new Font("Segoe UI Semibold", 68, FontStyle.Regular);
                    //_foreTheme = ThemeColorShade.Neutral_800;
                    break;
                case TextStyle.Display2:
                    Font = new Font("Segoe UI Semibold", 40, FontStyle.Regular);
                    //_foreTheme = ThemeColorShade.Neutral_800;
                    break;
                case TextStyle.Header1:
                    Font = new Font("Segoe UI", 24, FontStyle.Bold);
                    //_foreTheme = ThemeColorShade.Neutral_800;
                    break;
                case TextStyle.Header2:
                    Font = new Font("Segoe UI", 20, FontStyle.Bold);
                    //_foreTheme = ThemeColorShade.Neutral_800;
                    break;
                case TextStyle.Header3:
                    Font = new Font("Segoe UI", 16, FontStyle.Bold);
                    //_foreTheme = ThemeColorShade.Neutral_800;
                    break;
                case TextStyle.Title:
                    Font = new Font("Segoe UI", 18, FontStyle.Regular);
                    //_foreTheme = ThemeColorShade.Neutral_800;
                    break;
                case TextStyle.Subtitle:
                    Font = new Font("Segoe UI", 14, FontStyle.Regular);
                    //_foreTheme = ThemeColorShade.Neutral_800;
                    break;
                case TextStyle.Body:
                    Font = new Font("Segoe UI", 12, FontStyle.Regular);
                    //_foreTheme = ThemeColorShade.Neutral_800;
                    break;
                case TextStyle.BodyStrong:
                    Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    //_foreTheme = ThemeColorShade.Neutral_800;
                    break;
                case TextStyle.Caption:
                    Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    //_foreTheme = ThemeColorShade.Neutral_800;
                    break;
                case TextStyle.Small:
                    Font = new Font("Segoe UI", 8, FontStyle.Regular);
                    //_foreTheme = ThemeColorShade.Neutral_700;
                    break;
                case TextStyle.VerySmall:
                    Font = new Font("Segoe UI", 7, FontStyle.Regular);
                    //_foreTheme = ThemeColorShade.Neutral_700;
                    break;
            }
            Invalidate();
        }

        // Override to prevent background painting and allow transparency
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if (_fontSpacing != 0f)
            {
                DrawTextWithSpacing(e.Graphics);
            }
            else
            {
                if (Font.Size > 12)
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                }
                
                // Draw the default text with alignment
                using (StringFormat stringFormat = new StringFormat())
                {
                    stringFormat.Alignment = _textAlignment;
                    stringFormat.LineAlignment = _lineAlignment;
                    stringFormat.Trimming = StringTrimming.Word;

                    using (SolidBrush brush = new SolidBrush(_foreColor))
                    {
                        e.Graphics.DrawString(
                            Text,
                            Font,
                            brush,
                            ClientRectangle,
                            stringFormat
                        );
                    }
                }
            }
        }





        private void DrawTextWithSpacing(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;



                using (StringFormat stringFormat = new StringFormat())
                {
                    stringFormat.Alignment = _textAlignment;
                    stringFormat.LineAlignment = _lineAlignment;

                    RectangleF layoutRect = ClientRectangle;
                    var textSize = g.MeasureString(Text, Font);
                    var textRect = new RectangleF(0,0, textSize.Width, textSize.Height);

                    float x_align = 0;
                    float x_adjust = 0;
                    float y_align = 0;
                    float y_adjust = 0;

                    foreach (char c in Text.Replace(" ", "  "))
                    {
                        string character = c.ToString();
                        SizeF charSize = g.MeasureString(character, Font);
                        x_adjust += charSize.Width + _fontSpacing;
                        y_adjust = Math.Max(charSize.Height,y_adjust);
                    }


                    if (_textAlignment == StringAlignment.Center ) {x_align = (layoutRect.Width / 2) - (x_adjust / 2) + (_fontSpacing);}
                    if (_textAlignment == StringAlignment.Near) { x_align = 0; }
                    if (_textAlignment == StringAlignment.Far) { x_align = (layoutRect.Width) - (x_adjust); }

                if (_lineAlignment == StringAlignment.Center) { y_align = (layoutRect.Height / 2); }
                if (_lineAlignment == StringAlignment.Near) { y_align = layoutRect.Height - y_adjust; }
                if (_lineAlignment == StringAlignment.Far) { y_align = y_adjust; }



                // Render each character with spacing
                using (SolidBrush brush = new SolidBrush(_foreColor))
                        {
                            float letterSpacing = _fontSpacing; // Adjust this value for desired spacing
                            float x = x_align + _xoffset; // Starting X position
                            float y = y_align + _yoffset;  // Starting Y position
                    
                            foreach (char c in Text.Replace(" ","  "))
                            {
                                string character = c.ToString();
                                // Measure the size of the current character
                                SizeF charSize = g.MeasureString(character, Font);

                                // Draw the character
                                g.DrawString(character, Font, brush, new PointF(x, y), stringFormat);

                                // Increment the x-position by the character width plus spacing
                                x += charSize.Width + letterSpacing;
                            }
                        }
                }
            
        }
    }
}