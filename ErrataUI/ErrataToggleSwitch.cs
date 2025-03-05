using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataToggleSwitch : Control
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


        private bool isChecked = false;

        public event EventHandler Toggled;

        public bool IsChecked
        {
            get => isChecked;
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    Toggled?.Invoke(this, EventArgs.Empty);
                    Invalidate(); // Redraw the control
                }
            }
        }


        private bool squareSlider = false;
        public bool SquareSlider
        {
            get => squareSlider;
            set
            {
                squareSlider = value;
                Invalidate(); // Redraw the control     
            }
        }

        private bool antiAlias = true;
        public bool AntiAlias
        {
            get => antiAlias;
            set
            {
                antiAlias = value;
                Invalidate(); // Redraw the control     
            }
        }








        #region OFFBACK
        //OFFBACK COLOR
        private UIRole _offBackRole = UIRole.ModalBackground;
        private ThemeColorShade _offBackTheme = ThemeColorShade.Neutral_400;
        private Color _offBackColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_400);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole OffBackRole { get => _offBackRole; set { _offBackRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade OffBackTheme
        {
            get => _offBackTheme; set
            {
                _offBackTheme = value;
                if (!_ignoreTheme) { OffBackColor = ThemeManager.Instance.GetThemeColorShade(_offBackTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color OffBackColor { get => _offBackColor; set { _offBackColor = value; Invalidate(); } }
        #endregion


        #region ONBACK
        //ONBACK COLOR
        private UIRole _onBackRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _onBackTheme = ThemeColorShade.Primary_500;
        private Color _onBackColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole OnBackRole { get => _onBackRole; set { _onBackRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade OnBackTheme
        {
            get => _onBackTheme; set
            {
                _onBackTheme = value;
                if (!_ignoreTheme) { OnBackColor = ThemeManager.Instance.GetThemeColorShade(_onBackTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color OnBackColor { get => _onBackColor; set { _onBackColor = value; Invalidate(); } }
        #endregion

        #region THUMBCOLORON
        //THUMBCOLORON
        private UIRole _thumbColorOnRole = UIRole.TitleBar;
        private ThemeColorShade _thumbColorOnTheme = ThemeColorShade.Primary_500;
        private Color _thumbColorOn = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole ThumbColorOnRole { get => _thumbColorOnRole; set { _thumbColorOnRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade ThumbColorOnTheme
        {
            get => _thumbColorOnTheme; set
            {
                _thumbColorOnTheme = value;
                if (!_ignoreTheme) { ThumbColorOn = ThemeManager.Instance.GetThemeColorShade(_thumbColorOnTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color ThumbColorOn { get => _thumbColorOn; set { _thumbColorOn = value; Invalidate(); } }
        #endregion
        #region THUMBCOLOROFF
        //THUMBCOLOROFF
        private UIRole _thumbColorOffRole = UIRole.TitleBar;
        private ThemeColorShade _thumbColorOffTheme = ThemeColorShade.Primary_800;
        private Color _thumbColorOff = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_800);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole ThumbColorOffRole { get => _thumbColorOffRole; set { _thumbColorOffRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade ThumbColorOffTheme
        {
            get => _thumbColorOffTheme; set
            {
                _thumbColorOffTheme = value;
                if (!_ignoreTheme) { ThumbColorOff = ThemeManager.Instance.GetThemeColorShade(_thumbColorOffTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color ThumbColorOff { get => _thumbColorOff; set { _thumbColorOff = value; Invalidate(); } }

        #endregion


        #region THUMB
        //THUMB COLOR
        private UIRole _thumbRole = UIRole.MainBackground;
        private ThemeColorShade _thumbTheme = ThemeColorShade.Neutral_100;
        private Color _thumbColor = Color.FromArgb(250, 250, 250);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole ThumbRole { get => _thumbRole; set { _thumbRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade ThumbTheme
        {
            get => _thumbTheme; set
            {
                _thumbTheme = value;
                if (!_ignoreTheme) { ThumbColor = ThemeManager.Instance.GetThemeColorShade(_thumbTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color ThumbColor { get => _thumbColor; set { _thumbColor = value; Invalidate(); } }
        #endregion



        #region THUMBBORDER
        //THUMBBORDER COLOR
        private UIRole _thumbBorderRole = UIRole.GeneralBorders;
        private ThemeColorShade _thumbBorderTheme = ThemeColorShade.Primary_700;
        private Color _thumbBorderColor = Color.FromArgb(128, 128, 128);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole ThumbBorderRole { get => _thumbBorderRole; set { _thumbBorderRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade ThumbBorderTheme
        {
            get => _thumbBorderTheme; set
            {
                _thumbBorderTheme = value;
                if (!_ignoreTheme) { ThumbBorderColor = ThemeManager.Instance.GetThemeColorShade(_thumbBorderTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color ThumbBorderColor { get => _thumbBorderColor; set { _thumbBorderColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region TRACKBORDER
        //TRACKBORDER COLOR
        private UIRole _trackBorderRole = UIRole.ModalBackground;
        private ThemeColorShade _trackBorderTheme = ThemeColorShade.Neutral_600;
        private Color _trackBorderColor = Color.FromArgb(128, 128, 128);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole TrackBorderRole { get => _trackBorderRole; set { _trackBorderRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade TrackBorderTheme
        {
            get => _trackBorderTheme; set
            {
                _trackBorderTheme = value;
                if (!_ignoreTheme) { TrackBorderColor = ThemeManager.Instance.GetThemeColorShade(_trackBorderTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color TrackBorderColor { get => _trackBorderColor; set { _trackBorderColor = value; Invalidate(); } }
        #endregion

        private int _thumbBorderSize = 2;
        public int ThumbBorderSize
        {
            get => _thumbBorderSize;
            set { _thumbBorderSize = value; Invalidate(); }
        }





        #region TRACK OPTIONS

        private int _trackBorderSize = 2;
        public int TrackBorderSize
        {
            get => _trackBorderSize;
            set { _trackBorderSize = value; Invalidate(); }
        }



        private int paddingTrackL = 1;
        [Category("Track")]
        [Description("Track Setting")]
        public int PaddingLeftTrack
        {
            get => paddingTrackL;
            set { paddingTrackL = value; Invalidate(); }
        }

        private int paddingTrackT = 1;
        [Category("Track")]
        [Description("Track Setting")]
        public int PaddingTopTrack
        {
            get => paddingTrackT;
            set { paddingTrackT = value; Invalidate(); }
        }

        private int paddingTrackR = 1;
        [Category("Track")]
        [Description("Track Setting")]
        public int PaddingRightTrack
        {
            get => paddingTrackR;
            set { paddingTrackR = value; Invalidate(); }
        }

        private int paddingTrackB = 1;
        [Category("Track")]
        [Description("Track Setting")]
        public int PaddingBottomTrack
        {
            get => paddingTrackB;
            set { paddingTrackB = value; Invalidate(); }
        }

        private int paddingDecomp = 2;
        [Category("Track")]
        [Description("Track Setting")]
        public int PaddingDecompensator
        {
            get => paddingDecomp;
            set { paddingDecomp = value; Invalidate(); }
        }

        private int trackWeight = 1;
        [Category("Track")]
        [Description("Track Setting")]
        public int TrackWeight
        {
            get => trackWeight;
            set { trackWeight = value; Invalidate(); }
        }

        private int trackRadius = 10;
        [Category("Track")]
        [Description("Track Setting")]
        public int TrackRadius
        {
            get => trackRadius;
            set { trackRadius = value; Invalidate(); }
        }

        private int checkedNudge = 1;
        [Category("Misc")]
        [Description("Nudges the thumb on the x-axis when checked.")]
        public int CheckedNudge
        {
            get => checkedNudge;
            set { checkedNudge = value; Invalidate(); }
        }


        #endregion


        #region THUMB OPTIONS
        private int ratioThumb = 8;
        [Category("Thumb")]
        [Description("Thumb Setting")]
        public int ThumbHeightRatio
        {
            get => ratioThumb;
            set { ratioThumb = value; Invalidate(); }
        }


        private int thumbWeight = 1;
        [Category("Thumb")]
        [Description("Thumb Setting")]
        public int ThumbWeight
        {
            get => thumbWeight;
            set { thumbWeight = value; Invalidate(); }
        }

        private int divisorThumb = 2;
        [Category("Thumb")]
        [Description("Thumb Setting")]
        public int ThumbHeightDivisor
        {
            get => divisorThumb;
            set { divisorThumb = value; if (divisorThumb == 0) { divisorThumb = -1; } Invalidate(); }
        }


        private int paddingThumbL = 0;
        [Category("Thumb")]
        [Description("Thumb Setting")]
        public int PaddingLeftThumb
        {
            get => paddingThumbL;
            set { paddingThumbL = value; Invalidate(); }
        }


        private int paddingThumbT = 0;
        [Category("Thumb")]
        [Description("Thumb Setting")]
        public int PaddingTopThumb
        {
            get => paddingThumbT;
            set { paddingThumbT = value; Invalidate(); }
        }



        private int paddingThumbR = 0;
        [Category("Thumb")]
        [Description("Thumb Setting")]
        public int PaddingRightThumb
        {
            get => paddingThumbR;
            set { paddingThumbR = value; Invalidate(); }
        }

        private int paddingThumbB = 0;
        [Category("Thumb")]
        [Description("Thumb Setting")]
        public int PaddingBottomThumb
        {
            get => paddingThumbB;
            set { paddingThumbB = value; Invalidate(); }
        }
        #endregion


        private bool trackFillOn = true;


        private SwitchType _switchType;
        [Browsable(true)]
        [Category("Misc")]
        [Description("Sets the button type.")]
        public SwitchType Type
        {
            get => _switchType;
            set
            {
                _switchType = value;
                UpdateType();
            }
        }

        public enum SwitchType
        {
            None,
            Fluid,
            Pillbox,
            Square
        }

        private void UpdateType()
        {
            // Update the Text property based on the ButtonType
            switch (_switchType)
            {
                case SwitchType.None:
                    break;
                case SwitchType.Fluid:
                    PaddingLeftThumb = 1;
                    ThumbHeightDivisor = 2;
                    ThumbHeightRatio = 9;
                    PaddingDecompensator = 3;
                    TrackRadius = 22;
                    CheckedNudge = 2;
                    ThumbBorderSize = 1;
                    TrackBorderSize = 1;
                    PaddingTopTrack = 1;
                    PaddingBottomTrack = 1;
                    IgnoreRoles = true;
                    ThumbBorderColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_700);
                    ThumbColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_100);
                    OnBackColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);
                    OffBackColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_400);
                    TrackBorderColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_600);
                    break;
                case SwitchType.Pillbox:
                    PaddingLeftThumb = 2;
                    ThumbHeightDivisor = 2;
                    ThumbHeightRatio = 6;
                    PaddingDecompensator = 2;
                    TrackRadius = 10;
                    CheckedNudge = 4;
                    ThumbBorderSize = 2;
                    TrackBorderSize = 0;
                    PaddingTopTrack = 8;
                    PaddingBottomTrack = 8;
                    IgnoreRoles = true;
                    ThumbBorderColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_700);
                    ThumbColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_100);
                    OnBackColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);
                    OffBackColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_400);
                    TrackBorderColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_600);
                    break;
                case SwitchType.Square:
                    PaddingLeftThumb = 1;
                    ThumbHeightDivisor = 2;
                    ThumbHeightRatio = 7;
                    PaddingDecompensator = 2;
                    TrackRadius = 0;
                    CheckedNudge = 1;
                    ThumbBorderSize = 2;
                    TrackBorderSize = 1;
                    PaddingTopTrack = 1;
                    PaddingBottomTrack = 1;
                    PaddingTopThumb = 1;
                    AntiAlias = false;
                    SquareSlider = true;
                    IgnoreRoles = true;
                    ThumbBorderColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_700);
                    ThumbColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_100);
                    OnBackColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);
                    OffBackColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_400);
                    TrackBorderColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_600);
                    break;
                    break;
            }
        }

        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                ThumbBorderColor = ThemeManager.Instance.GetThemeColorShade(ThumbBorderTheme);
                ThumbColor = ThemeManager.Instance.GetThemeColorShade(ThumbTheme);
                OnBackColor = ThemeManager.Instance.GetThemeColorShade(OnBackTheme);
                OffBackColor = ThemeManager.Instance.GetThemeColorShade(OffBackTheme);
                TrackBorderColor = ThemeManager.Instance.GetThemeColorShade(TrackBorderTheme);
                ThumbColorOn = ThemeManager.Instance.GetThemeColorShade(ThumbColorOnTheme);
                ThumbColorOff = ThemeManager.Instance.GetThemeColorShade(ThumbColorOffTheme);
            }
        }

        public ErrataToggleSwitch()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            Size = new Size(50, 25); // Default size
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;

            if (AntiAlias)
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
            }
            else
            {
                g.SmoothingMode = SmoothingMode.Default;
            }
            

            // Define the track rectangle
            //var trackRect = new RectangleF(0, Height / 4f, Width, Height / 2f);
            var trackRect = new Rectangle(paddingTrackL, paddingTrackT, Width - paddingTrackR * paddingDecomp, Height - paddingTrackB * paddingDecomp);

            //float radius = Height / 2f;
            float radius = trackRadius;

            if (radius == 0) 
            {
                // Draw the track using arcs
                using (Brush trackBrush = new SolidBrush(IsChecked ? OnBackColor : OffBackColor))
                {
                    g.FillRectangle(trackBrush, trackRect);
                    using (var borderPen = new Pen(_trackBorderColor, _trackBorderSize))
                    {
                        if (_trackBorderSize > 0) { g.DrawRectangle(borderPen, trackRect); }

                    }
                }
            }

            else if (radius > 0)
            {
                // Draw the track using arcs
                using (GraphicsPath trackPath = GetFigurePath(trackRect, radius))
                using (Brush trackBrush = new SolidBrush(IsChecked ? OnBackColor : OffBackColor))
                {
                    g.FillPath(trackBrush, trackPath);
                    using (var borderPen = new Pen(_trackBorderColor, _trackBorderSize))
                    {
                        if (_trackBorderSize > 0) { g.DrawPath(borderPen, trackPath); }

                    }
                }
            }
            
            


            // Draw the thumb
            int tHalf = ThumbHeightRatio / divisorThumb;
            int thumbSize = Height - ThumbHeightRatio;
            int thumbXPos = IsChecked ? Width - thumbSize - tHalf - checkedNudge : tHalf;
            //var thumbRect = new Rectangle(thumbXPos, (Height - thumbSize) / 2, thumbSize, thumbSize);
            var thumbRect = new Rectangle(thumbXPos + paddingThumbL, tHalf + paddingThumbT, thumbSize + paddingThumbR, thumbSize + paddingThumbB);
            using (Brush thumbBrush = new SolidBrush(IsChecked ? ThumbColorOn : ThumbColorOff))
            {
                if (SquareSlider)
                {
                    g.FillRectangle(thumbBrush, thumbRect);
                }
                else
                {
                    g.FillEllipse(thumbBrush, thumbRect);
                }
                
            }

            // Optional: Draw a border for the toggle (optional style tweak)
            Color _thumbCol = IsChecked ? ThumbColorOn : ThumbColorOff;
            using (var borderPen = new Pen(_thumbBorderColor, _thumbBorderSize))
            {
                if (_thumbBorderSize > 0) 
                { 
                    if (SquareSlider)
                    {
                        g.DrawRectangle(borderPen, thumbRect);
                    }
                    else
                    {
                        g.DrawEllipse(borderPen, thumbRect);
                    }
                     
                
                }
                
            }
        }

        private GraphicsPath GetFigurePath(RectangleF rectangle, float radius)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.StartFigure();
            graphicsPath.AddArc(rectangle.X, rectangle.Y, radius, radius, 180, 90); // Top-left corner
            graphicsPath.AddArc(rectangle.Right - radius, rectangle.Y, radius, radius, 270, 90); // Top-right corner
            graphicsPath.AddArc(rectangle.Right - radius, rectangle.Bottom - radius, radius, radius, 0, 90); // Bottom-right corner
            graphicsPath.AddArc(rectangle.X, rectangle.Bottom - radius, radius, radius, 90, 90); // Bottom-left corner
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Left)
            {
                IsChecked = !IsChecked; // Toggle state
            }
        }
    }
}
