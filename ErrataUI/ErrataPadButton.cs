using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataPadButton : Control
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

        private bool _invertedAction = false;
        [Category("Misc")]
        public bool InvertedAction
        {
            get => _invertedAction;
            set
            {
                _invertedAction = value; Invalidate();
            }
        }

        private bool isPressed = false;
        public event EventHandler<bool> StateChanged;

        private int _glowAlpha = 50;
        [Category("Misc")]
        public int GlowAlpha
        {
            get => _glowAlpha;
            set
            {
                if(value < 0) { value = 0; }
                if(value > 255) { value = 255; }
                _glowAlpha = value; Invalidate();
            }
        }

        private int _borderX = 1;
        [Category("Misc")]
        public int BorderX
        {
            get => _borderX;
            set
            {
                _borderX = value; Invalidate();
            }
        }

        private int _borderY = 1;
        [Category("Misc")]
        public int BorderY
        {
            get => _borderY;
            set
            {
                _borderY = value; Invalidate();
            }
        }

        private int _borderH = 3;
        [Category("Misc")]
        public int BorderH
        {
            get => _borderH;
            set
            {
                _borderH = value; Invalidate();
            }
        }

        private int _borderW = 3;
        [Category("Misc")]
        public int BorderW
        {
            get => _borderW;
            set
            {
                _borderW = value; Invalidate();
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


        #region PRESSEDCOLOR
        //PRESSEDCOLOR
        private UIRole _pressedColorRole = UIRole.TitleBar;
        private ThemeColorShade _pressedColorTheme = ThemeColorShade.Primary_500;
        private Color _pressedColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole PressedColorRole { get => _pressedColorRole; set { _pressedColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade PressedColorTheme
        {
            get => _pressedColorTheme; set
            {
                _pressedColorTheme = value;
                if (!_ignoreTheme) { PressedColor = ThemeManager.Instance.GetThemeColorShade(_pressedColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color PressedColor { get => _pressedColor; set { _pressedColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion


        #region UNPRESSEDCOLOR
        //UNPRESSEDCOLOR
        private UIRole _unpressedColorRole = UIRole.TitleBar;
        private ThemeColorShade _unpressedColorTheme = ThemeColorShade.Neutral_800;
        private Color _unpressedColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_800);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole UnpressedColorRole { get => _unpressedColorRole; set { _unpressedColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade UnpressedColorTheme
        {
            get => _unpressedColorTheme; set
            {
                _unpressedColorTheme = value;
                if (!_ignoreTheme) { UnpressedColor = ThemeManager.Instance.GetThemeColorShade(_unpressedColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color UnpressedColor { get => _unpressedColor; set { _unpressedColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////UnpressedColor = ThemeManager.Instance.GetThemeColorShadeOffset(UnpressedColorTheme);
        #endregion


        #region BORDERCOLOR
        //BORDERCOLOR
        private UIRole _borderColorRole = UIRole.TitleBar;
        private ThemeColorShade _borderColorTheme = ThemeColorShade.Neutral_700;
        private Color _borderColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_700);

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

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion






        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                PressedColor = ThemeManager.Instance.GetThemeColorShadeOffset(PressedColorTheme);
                UnpressedColor = ThemeManager.Instance.GetThemeColorShadeOffset(UnpressedColorTheme);
                BorderColor = ThemeManager.Instance.GetThemeColorShadeOffset(BorderColorTheme);
            }
        }

        public ErrataPadButton()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            DoubleBuffered = true;
            Size = new Size(60, 60);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            isPressed = InvertedAction ? false : true;
            Invalidate();
            StateChanged?.Invoke(this, isPressed);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isPressed = InvertedAction ? true : false;
            Invalidate();
            StateChanged?.Invoke(this, isPressed);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Background
            Color fillColor = isPressed ? _pressedColor : _unpressedColor;
            using (SolidBrush brush = new SolidBrush(fillColor))
            {
                g.FillRectangle(brush, ClientRectangle);
            }

            // Border
            using (Pen pen = new Pen(_borderColor, _borderThickness))
            {
                g.DrawRectangle(pen, _borderX, _borderY, Width - _borderW, Height - _borderH);
            }

            // Glow effect (subtle)
            if (isPressed)
            { //Color.FromArgb(_glowAlpha, originalColor);
                using (SolidBrush glowBrush = new SolidBrush(Color.FromArgb(_glowAlpha, _pressedColor)))
                {
                    g.FillRectangle(glowBrush, ClientRectangle);
                }
            }
        }
    }
}
