using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ErrataUI;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    [ToolboxItem(true)]
    public class ErrataButtonRound : Button
    {

        //Fields
        private int borderSize = 0;
        private int borderRadius = 0;
        private Color gradientColor = Color.Gainsboro;
        private bool gradientFill = false;

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



        //Properties
        public int BorderSize
        {
            get => borderSize;
            set { borderSize = value; Invalidate(); }
        }
        public int BorderRadius
        {
            get => borderRadius;
            set { borderRadius = value; Invalidate(); }//(value <= Height) ? value : Height; Invalidate(); }
        }
        public bool GradientFill
        {
            get => gradientFill;
            set { gradientFill = value; this.Invalidate(); }
        }


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
        #region BACKGROUND
        //BACKGROUND COLOR
        private UIRole _backgroundRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _backgroundTheme = ThemeColorShade.Primary_500;
        private Color _backgroundColor = Color.FromArgb(0, 128, 200);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole BackgroundRole { get => _backgroundRole; set { _backgroundRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade BackgroundTheme
        {
            get => _backgroundTheme; set
            {
                _backgroundTheme = value;
                if (!_ignoreTheme) { BackgroundColor = ThemeManager.Instance.GetThemeColorShade(_backgroundTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color BackgroundColor { get => _backgroundColor; set { _backgroundColor = value; BackColor = _backgroundColor; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////BackgroundColor = ThemeManager.Instance.GetThemeColorShade(BackgroundTheme);
        #endregion
        #region TEXT
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

        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                BackgroundColor = ThemeManager.Instance.GetThemeColorShade(BackgroundTheme);
                BorderColor = ThemeManager.Instance.GetThemeColorShade(BorderTheme);
                ForeColor = ThemeManager.Instance.GetThemeColorShade(ForeColorTheme);

            }
        }


        public Color GradientColor
        {
            get => gradientColor;
            set { gradientColor = value; this.Invalidate(); }
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
            None
        }

        private void UpdateType()
        {
            // Update the Text property based on the ButtonType
            switch (_buttonType)
            {
                case ButtonType.None:
                    break;
                
            }
        }



        //Constructor
        public ErrataButtonRound()
        {
            this.DoubleBuffered = true;
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            Size = new Size(140, 35);
            FlatAppearance.BorderSize = 0;
            Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular);
            FlatStyle = FlatStyle.Flat;
            BackgroundColor = Color.FromArgb(0, 128, 200);
            ForeColor = Color.White;
            BorderColor = Color.DimGray;
            Resize += new EventHandler(Button_Resize);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        private void Button_Resize(object sender, EventArgs e)
        {
            
        }




        protected override void OnPaint(PaintEventArgs prevent)
        {
            base.OnPaint(prevent);
            Graphics g = prevent.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var rectContourSmooth = Rectangle.Inflate(this.ClientRectangle, -1, -1);
            var rectBorder = Rectangle.Inflate(rectContourSmooth, -borderSize, -borderSize);
            var smoothSize = borderSize > 0 ? borderSize * 3 : 1;
            using (var borderGColor = new LinearGradientBrush(rectBorder, Color.Transparent, BorderColor, 90F))
            using (var pathRegion = new GraphicsPath())
            using (var penSmooth = new Pen(BackColor, smoothSize))
            using (var penBorder = new Pen(borderGColor, BorderSize))
            {
                
                
                penBorder.DashStyle = DashStyle.Solid;
                penBorder.DashCap = DashCap.Flat;;
                pathRegion.AddEllipse(rectContourSmooth);
                //Set rounded region 
                this.Region = new Region(pathRegion);

                //Drawing
                g.DrawEllipse(penSmooth, rectContourSmooth);//Draw contour smoothing

                if (borderSize > 0){g.DrawEllipse(penBorder, rectBorder);} 
            }

        }

    }
}

