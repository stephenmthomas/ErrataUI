


using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;


namespace ErrataUI
{

    public class ErrataTextBox : Control
    {

        private bool _ignoreRoles;
        [Category("Theme Manager"), Description("If true, role updates will be ignored, allowing individual theme application.")]
        public bool IgnoreRoles
        {
            get => _ignoreRoles;
            set
            {
                _ignoreRoles = value;
            }
        }

        private bool _ignoreTheme;
        [Category("Theme Manager"), Description("If true, color updates will be ignored, allowing manual color selection.")]
        public bool IgnoreTheme
        {
            get => _ignoreTheme;
            set
            {
                _ignoreTheme = value;
                UpdateColor();  // Update color immediately if the property changes
            }
        }


        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public override bool AutoSize
        {
            get => base.AutoSize;
            set
            {
                base.AutoSize = value;
                Invalidate();
            }
        }




        private readonly TextBoxErrataBase _baseTextBox = new()
        {
            BorderStyle = BorderStyle.None,
            ForeColor = Color.Black,
            BackColor = Color.White
        };

        public void SelectAll() { _baseTextBox.SelectAll(); }
        public void Clear() { _baseTextBox.Clear(); }
        public new void Focus() { _baseTextBox.Focus(); }

        public override Font Font
        {
            get => base.Font;
            set
            {
                base.Font = value;
                _baseTextBox.Font = value;
                Invalidate();
            }
        }

        private int _borderThickness = 2;
        [Category("Misc")]
        public int BorderThickness
        {
            get => _borderThickness;
            set
            {
                _borderThickness = value; Invalidate();
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

        #region FORECOLOR
        //FORECOLOR
        private UIRole _foreColorRole = UIRole.TitleBar;
        private ThemeColorShade _foreColorTheme = ThemeColorShade.Neutral_800;
        private Color _foreColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_800);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole ForeColorRole { get => _foreColorRole; set { _foreColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade ForeColorTheme
        {
            get => _foreColorTheme; set
            {
                _foreColorTheme = value;
                if (!_ignoreTheme) { ForeColor = ThemeManager.Instance.GetThemeColorShade(_foreColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color ForeColor { get => _foreColor; set { _foreColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region BACKCOLOR
        //BACKCOLOR
        private UIRole _backColorRole = UIRole.TitleBar;
        private ThemeColorShade _backColorTheme = ThemeColorShade.Neutral_100;
        private Color _backColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_100);

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
        #region BASECOLOR
        //BASECOLOR
        private UIRole _baseColorRole = UIRole.TitleBar;
        private ThemeColorShade _baseColorTheme = ThemeColorShade.Neutral_100;
        private Color _baseColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_100);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole BaseColorRole { get => _baseColorRole; set { _baseColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade BaseColorTheme
        {
            get => _baseColorTheme; set
            {
                _baseColorTheme = value;
                if (!_ignoreTheme) { BaseColor = ThemeManager.Instance.GetThemeColorShade(_baseColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color BaseColor { get => _baseColor; set { _baseColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region BORDERCOLORA
        //BORDERCOLORA
        private UIRole _borderColorARole = UIRole.TitleBar;
        private ThemeColorShade _borderColorATheme = ThemeColorShade.Primary_500;
        private Color _borderColorA = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole BorderColorARole { get => _borderColorARole; set { _borderColorARole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade BorderColorATheme
        {
            get => _borderColorATheme; set
            {
                _borderColorATheme = value;
                if (!_ignoreTheme) { BorderColorA = ThemeManager.Instance.GetThemeColorShade(_borderColorATheme); }
            }
        }

        [Category("Misc"), Description("Has focus color.")]
        public Color BorderColorA { get => _borderColorA; set { _borderColorA = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region BORDERCOLORB
        //BORDERCOLORB
        private UIRole _borderColorBRole = UIRole.TitleBar;
        private ThemeColorShade _borderColorBTheme = ThemeColorShade.Neutral_500;
        private Color _borderColorB = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole BorderColorBRole { get => _borderColorBRole; set { _borderColorBRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade BorderColorBTheme
        {
            get => _borderColorBTheme; set
            {
                _borderColorBTheme = value;
                if (!_ignoreTheme) { BorderColorB = ThemeManager.Instance.GetThemeColorShade(_borderColorBTheme); }
            }
        }

        [Category("Misc"), Description("Lost focus color.")]
        public Color BorderColorB { get => _borderColorB; set { _borderColorB = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion


        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                ForeColor = ThemeManager.Instance.GetThemeColorShadeOffset(ForeColorTheme);
                BackColor = ThemeManager.Instance.GetThemeColorShadeOffset(BackColorTheme);
                BaseColor = ThemeManager.Instance.GetThemeColorShadeOffset(BaseColorTheme);
                BorderColorA = ThemeManager.Instance.GetThemeColorShadeOffset(BorderColorATheme);
                BorderColorB = ThemeManager.Instance.GetThemeColorShadeOffset(BorderColorBTheme);
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            _baseTextBox.Location = new(12, 8);
            _baseTextBox.Width = Width - 24;
            _baseTextBox.Height = (Height - 16) > 0 ? (Height - 16) : 0;
            Height = _baseTextBox.Height + 16;

            Graphics g = e.Graphics;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(_backColor);

            using (var borderPen = new Pen(_baseTextBox.Focused ? _borderColorA : _borderColorB, _borderThickness))
            {
                if (_borderRadius > 0)
                {
                    GraphicsPath bg = RoundingPaths.CreateRoundRect(0.5f, 0.5f, Width - 1, Height - 1, _borderRadius);
                    g.FillPath(new SolidBrush(BackColor), bg);
                    g.DrawPath(new(_baseTextBox.Focused ? _borderColorA : _borderColorB, 0.5f), bg);
                }
                else
                {
                    var borderRectangle = new Rectangle(0, 0, Width, Height);
                    g.DrawRectangle(borderPen, borderRectangle);
                }
            }
            
            
        }




        public ErrataTextBox()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Font = new("Segoe UI", 10);
            ForeColor = Color.Black;
            BackColor = Color.White;

            if (!Controls.Contains(_baseTextBox) && !DesignMode)
            {
                Controls.Add(_baseTextBox);
            }

            _baseTextBox.GotFocus += _baseTextBox_GotFocus;
            _baseTextBox.LostFocus += _baseTextBox_LostFocus;
            _baseTextBox.KeyPress += _baseTextBox_KeyPress;
            _baseTextBox.TabStop = true;
            TabStop = false;
            Width = 120;
        }

        private void _baseTextBox_LostFocus(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void _baseTextBox_GotFocus(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void _baseTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\x1')
            {
                ((TextBox)sender).SelectAll();
                e.Handled = true;
            }
        }

        private class TextBoxErrataBase : TextBox
        {
            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
            private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

            private const int EM_SETCUEBANNER = 0x1501;
            private const char EmptyChar = (char)0;
            private const char VisualStylePasswordChar = '\u25CF';
            private const char NonVisualStylePasswordChar = '\u002A';

            private string hint = string.Empty;
            public string Hint
            {
                get => hint;
                set
                {
                    hint = value;
                    SendMessage(Handle, EM_SETCUEBANNER, (int)IntPtr.Zero, Hint);
                }
            }

            private char _passwordChar = EmptyChar;
            public new char PasswordChar
            {
                get => _passwordChar;
                set
                {
                    _passwordChar = value;
                    SetBasePasswordChar();
                }
            }

            public new void SelectAll()
            {
                BeginInvoke((MethodInvoker)delegate ()
                {
                    base.Focus();
                    base.SelectAll();
                });
            }

            public new void Focus()
            {
                BeginInvoke((MethodInvoker)delegate ()
                {
                    base.Focus();
                });
            }

            private char _useSystemPasswordChar = EmptyChar;
            public new bool UseSystemPasswordChar
            {
                get => _useSystemPasswordChar != EmptyChar;
                set
                {
                    if (value)
                    {
                        _useSystemPasswordChar = Application.RenderWithVisualStyles ? VisualStylePasswordChar : NonVisualStylePasswordChar;
                    }
                    else
                    {
                        _useSystemPasswordChar = EmptyChar;
                    }

                    SetBasePasswordChar();
                }
            }

#if NETCOREAPP3_1 || NET6_0 || NET7_0 || NET8_0 || NET9_0
            //public EventHandler ContextMenuChanged { get; internal set; }
            public event EventHandler ContextMenuChanged;
#endif

            private void SetBasePasswordChar()
            {
                base.PasswordChar = UseSystemPasswordChar ? _useSystemPasswordChar : _passwordChar;
            }

            public TextBoxErrataBase()
            {

            }
        }
    }

}