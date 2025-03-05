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
    public class ErrataPanel:Panel
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


        private PanelType _panelType;
        [Browsable(true)]
        [Category("Misc")]
        [Description("Specifies the type of button.")]
        public PanelType Type
        {
            get => _panelType;
            set
            {
                _panelType = value;
                UpdateType();
            }
        }




        private int _borderRadius = 0;
        [Category("Misc"), Description("Sets corner curvature.")]
        public int BorderRadius
        {
            get => _borderRadius;
            set { _borderRadius = value; this.Invalidate(); }
        }


        private int _borderWidth = 0;
        [Category("Misc"), Description("Border width.")]
        public int BorderWidth
        {
            get => _borderWidth;
            set { _borderWidth = value; this.Invalidate(); }
        }


        private float _gradientAngle = 0F;
        [Category("Misc"), Description("The direction of the fade.")]
        public float GradientAngle
        {
            get => _gradientAngle;
            set
            {
                _gradientAngle = value;
                Invalidate();
            }
        }

        private bool _gradient = true;
        [Category("Misc"), Description("Enable gradient painting.")]
        public bool Gradient
        {
            get => _gradient;
            set
            {
                _gradient = value;
                Invalidate();
            }
        }


        #region RADIUSBORDER
        //RADIUSBORDER COLOR
        private UIRole _radiusBorderRole = UIRole.GeneralBorders;
        private ThemeColorShade _radiusBorderTheme = ThemeColorShade.Neutral_600;
        private Color _radiusBorderColor = Color.FromArgb(128, 128, 128);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole RadiusBorderRole { get => _radiusBorderRole; set { _radiusBorderRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade RadiusBorderTheme
        {
            get => _radiusBorderTheme; set
            {
                _radiusBorderTheme = value;
                if (!_ignoreTheme) { RadiusBorderColor = ThemeManager.Instance.GetThemeColorShade(_radiusBorderTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color RadiusBorderColor { get => _radiusBorderColor; set { _radiusBorderColor = value; Invalidate(); } }
        #endregion
        #region BACKCOLOR
        //BACKCOLOR
        private UIRole _backColorRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _backColorTheme = ThemeColorShade.Primary_800;
        private Color _backColor = Color.FromArgb(0, 128, 200);

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
        #region GRADIENTCOLOR
        //GRADIENTCOLOR
        private UIRole _gradientColorRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _gradientColorTheme = ThemeColorShade.Primary_600;
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
        #endregion

        public enum PanelType
        {
            None,
            Neutral,
            NeutralDark,
            NeutralDarker,
            Primary,
            PrimaryDark,
            Secondary,
            SecondaryDark,
            SemanticA,
            SemanticADark,
            SemanticB,
            SemanticBDark,
            SemanticC,
            SemanticCDark,
            NeutralBase,
            NeutralContent,
            NeutralElements,
            NeutralModal,
            PrimaryBase,
            PrimaryContent,
            PrimaryElements,
            PrimaryModal,
            SecondaryBase,
            SecondaryContent,
            SecondaryElements,
            SecondaryModal,
            SemanticABase,
            SemanticAContent,
            SemanticAElements,
            SemanticAModal,
            SemanticBBase,
            SemanticBContent,
            SemanticBElements,
            SemanticBModal,
            SemanticCBase,
            SemanticCContent,
            SemanticCElements,
            SemanticCModal



        }


        private void UpdateType()
        {
            // Update the Text property based on the ButtonType
            switch (_panelType)
            {
                case PanelType.None:
                    break;
                case PanelType.Neutral:
                    BackColorTheme = ThemeColorShade.Neutral_100;
                    Gradient = false;
                    break;
                case PanelType.NeutralDark:
                    BackColorTheme = ThemeColorShade.Neutral_200;
                    Gradient = false;
                    break;
                case PanelType.NeutralDarker:
                    BackColorTheme = ThemeColorShade.Neutral_300;
                    Gradient = false;
                    break;
                case PanelType.Primary:
                    BackColorTheme = ThemeColorShade.Primary_500;
                    Gradient = false;
                    break;
                case PanelType.PrimaryDark:
                    BackColorTheme = ThemeColorShade.Primary_700;
                    Gradient = false;
                    break;
                case PanelType.Secondary:
                    BackColorTheme = ThemeColorShade.Secondary_500;
                    Gradient = false;
                    break;
                case PanelType.SecondaryDark:
                    BackColorTheme = ThemeColorShade.Secondary_700;
                    Gradient = false;
                    break;
                case PanelType.SemanticA:
                    BackColorTheme = ThemeColorShade.SemanticA_500;
                    Gradient = false;
                    break;
                case PanelType.SemanticADark:
                    BackColorTheme = ThemeColorShade.SemanticA_700;
                    Gradient = false;
                    break;
                case PanelType.SemanticB:
                    BackColorTheme = ThemeColorShade.SemanticB_500;
                    Gradient = false;
                    break;
                case PanelType.SemanticBDark:
                    BackColorTheme = ThemeColorShade.SemanticB_700;
                    Gradient = false;
                    break;
                case PanelType.SemanticC:
                    BackColorTheme = ThemeColorShade.SemanticC_500;
                    Gradient = false;
                    break;
                case PanelType.SemanticCDark:
                    BackColorTheme = ThemeColorShade.SemanticC_700;
                    Gradient = false;
                    break;
                case PanelType.NeutralBase:
                    BackColorTheme = ThemeColorShade.Neutral_50;
                    GradientColorTheme = ThemeColorShade.Neutral_300;
                    Gradient = true;
                    break;
                case PanelType.NeutralContent:
                    BackColorTheme = ThemeColorShade.Neutral_50;
                    GradientColorTheme = ThemeColorShade.Neutral_400;
                    Gradient = true;
                    break;
                case PanelType.NeutralElements:
                    BackColorTheme = ThemeColorShade.Neutral_100;
                    GradientColorTheme = ThemeColorShade.Neutral_500;
                    Gradient = true;
                    break;
                case PanelType.NeutralModal:
                    BackColorTheme = ThemeColorShade.Neutral_100;
                    GradientColorTheme = ThemeColorShade.Neutral_600;
                    Gradient = true;
                    break;
                case PanelType.PrimaryBase:
                    BackColorTheme = ThemeColorShade.Primary_500;
                    GradientColorTheme = ThemeColorShade.Primary_600;
                    Gradient = true;
                    break;
                case PanelType.PrimaryContent:
                    BackColorTheme = ThemeColorShade.Primary_500;
                    GradientColorTheme = ThemeColorShade.Primary_700;
                    Gradient = true;
                    break;
                case PanelType.PrimaryElements:
                    BackColorTheme = ThemeColorShade.Primary_500;
                    GradientColorTheme = ThemeColorShade.Primary_800;
                    Gradient = true;
                    break;
                case PanelType.PrimaryModal:
                    BackColorTheme = ThemeColorShade.Primary_500;
                    GradientColorTheme = ThemeColorShade.Primary_900;
                    Gradient = true;
                    break;
                case PanelType.SecondaryBase:
                    BackColorTheme = ThemeColorShade.Secondary_500;
                    GradientColorTheme = ThemeColorShade.Secondary_600;
                    Gradient = true;
                    break;
                case PanelType.SecondaryContent:
                    BackColorTheme = ThemeColorShade.Secondary_500;
                    GradientColorTheme = ThemeColorShade.Secondary_700;
                    Gradient = true;
                    break;
                case PanelType.SecondaryElements:
                    BackColorTheme = ThemeColorShade.Secondary_500;
                    GradientColorTheme = ThemeColorShade.Secondary_800;
                    Gradient = true;
                    break;
                case PanelType.SecondaryModal:
                    BackColorTheme = ThemeColorShade.Secondary_500;
                    GradientColorTheme = ThemeColorShade.Secondary_900;
                    Gradient = true;
                    break;
                case PanelType.SemanticABase:
                    BackColorTheme = ThemeColorShade.SemanticA_500;
                    GradientColorTheme = ThemeColorShade.SemanticA_600;
                    Gradient = true;
                    break;
                case PanelType.SemanticAContent:
                    BackColorTheme = ThemeColorShade.SemanticA_500;
                    GradientColorTheme = ThemeColorShade.SemanticA_700;
                    Gradient = true;
                    break;
                case PanelType.SemanticAElements:
                    BackColorTheme = ThemeColorShade.SemanticA_500;
                    GradientColorTheme = ThemeColorShade.SemanticA_800;
                    Gradient = true;
                    break;
                case PanelType.SemanticAModal:
                    BackColorTheme = ThemeColorShade.SemanticA_500;
                    GradientColorTheme = ThemeColorShade.SemanticA_900;
                    Gradient = true;
                    break;
                case PanelType.SemanticBBase:
                    BackColorTheme = ThemeColorShade.SemanticB_500;
                    GradientColorTheme = ThemeColorShade.SemanticB_600;
                    Gradient = true;
                    break;
                case PanelType.SemanticBContent:
                    BackColorTheme = ThemeColorShade.SemanticB_500;
                    GradientColorTheme = ThemeColorShade.SemanticB_700;
                    Gradient = true;
                    break;
                case PanelType.SemanticBElements:
                    BackColorTheme = ThemeColorShade.SemanticB_500;
                    GradientColorTheme = ThemeColorShade.SemanticB_800;
                    Gradient = true;
                    break;
                case PanelType.SemanticBModal:
                    BackColorTheme = ThemeColorShade.SemanticB_500;
                    GradientColorTheme = ThemeColorShade.SemanticB_900;
                    Gradient = true;
                    break;
                case PanelType.SemanticCBase:
                    BackColorTheme = ThemeColorShade.SemanticC_500;
                    GradientColorTheme = ThemeColorShade.SemanticC_600;
                    Gradient = true;
                    break;
                case PanelType.SemanticCContent:
                    BackColorTheme = ThemeColorShade.SemanticC_500;
                    GradientColorTheme = ThemeColorShade.SemanticC_700;
                    Gradient = true;
                    break;
                case PanelType.SemanticCElements:
                    BackColorTheme = ThemeColorShade.SemanticC_500;
                    GradientColorTheme = ThemeColorShade.SemanticC_800;
                    Gradient = true;
                    break;
                case PanelType.SemanticCModal:
                    BackColorTheme = ThemeColorShade.SemanticC_500;
                    GradientColorTheme = ThemeColorShade.SemanticC_900;
                    Gradient = true;
                    break;

            }
        }


        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                RadiusBorderColor = ThemeManager.Instance.GetThemeColorShade(RadiusBorderTheme);
                BackColor = ThemeManager.Instance.GetThemeColorShade(BackColorTheme);
                GradientColor = ThemeManager.Instance.GetThemeColorShade(GradientColorTheme);
            }
        }


        //Constructor
        public ErrataPanel()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            this.BackColor = Color.FromArgb(0, 128, 200);
            this.ForeColor = Color.FromArgb(217,223,236);
            this.Size = new Size(250, 100);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            
        }


        //Properties








        //Methods
        private GraphicsPath GetCornerPath(RectangleF rectangle, float radius)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.StartFigure();
            graphicsPath.AddArc(rectangle.Width - radius, rectangle.Height - radius, radius, radius, 0, 90);
            graphicsPath.AddArc(rectangle.X, rectangle.Height - radius, radius, radius, 90, 90);
            graphicsPath.AddArc(rectangle.X, rectangle.Y, radius, radius, 180, 90);
            graphicsPath.AddArc(rectangle.Width - radius, rectangle.Y, radius, radius, 270, 90);
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        //Overridden Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            //Gradient 
            

            if (Gradient == true)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                LinearGradientBrush brushErrata = new LinearGradientBrush(ClientRectangle, BackColor, GradientColor, GradientAngle);
                e.Graphics.FillRectangle(brushErrata, ClientRectangle);
            }
            else
            {
                e.Graphics.SmoothingMode = SmoothingMode.Default;
                e.Graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);
            }


            //BorderRadius
            RectangleF rectangleF = new RectangleF(0,0,Width,Height);
            if (_borderRadius > 2)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath graphicsPath = GetCornerPath(rectangleF, _borderRadius))
                using (Pen pen = new Pen(_radiusBorderColor, _borderWidth))
                {
                    this.Region = new Region(graphicsPath);
                    e.Graphics.DrawPath(pen, graphicsPath);

                }
            }
            else 
            {
                e.Graphics.SmoothingMode = SmoothingMode.Default;
                this.Region = new Region(rectangleF);
            }
            
        }



    }
}
