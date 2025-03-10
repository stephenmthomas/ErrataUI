using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace ErrataUI
{
    public class ErrataTrackBarV : Control
    {
        //EMPTY DEFINITIONS FOR REMOVED CONTROL FEATURES
        public UIRole BorderRole = ThemeManager.UIRole.None;
        public ThemeColorShade BorderTheme = ThemeColorShade.None;
        public int BorderWidth = 0;
        public ThemeColorShade ThumbTheme = ThemeColorShade.None;
        public ThemeColorShade TrackTheme = ThemeColorShade.None;






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

        private bool _thumbLinearize = true;
        [Category("Misc.Thumb")]
        [Description("Dynamically adjusts the thumb y placement so that it fits within the bounds of the track.")]
        public bool ThumbLinearize
        {
            get => _thumbLinearize;
            set
            {
                _thumbLinearize = value; Invalidate();
            }
        }

        public int ThumbSize = 0;
        private int _thumbSize = 0;
        private int _thumbHeight = 25;
        [Category("Misc.Thumb")]
        public int ThumbHeight
        {
            get => _thumbHeight;
            set
            {
                _thumbHeight = value; Invalidate();
            }
        }


        private int _minimum = 0;
        private int _maximum = 100;
        private int _value = 50;
        
        private int trackWidth = 10;


        public event EventHandler ValueChanged;

        public int Minimum
        {
            get => _minimum;
            set
            {
                if (value >= _maximum) throw new ArgumentException("Minimum must be less than Maximum.");
                _minimum = value;
                if (_value < _minimum) Value = _minimum;
                Invalidate();
            }
        }

        public int Maximum
        {
            get => _maximum;
            set
            {
                if (value <= _minimum) throw new ArgumentException("Maximum must be greater than Minimum.");
                _maximum = value;
                if (_value > _maximum) Value = _maximum;
                Invalidate();
            }
        }


        #region THUMB_VARS
        private int _thumbBorderWidth = 2;  // Default border width
        [Category("Misc.Thumb")]
        public int ThumbBorderWidth
        {
            get => _thumbBorderWidth;
            set { _thumbBorderWidth = value; Invalidate(); }
        }

        

        private bool _thumbGrip = true;
        [Category("Misc.Thumb")]
        public bool ThumbGrip
        {
            get => _thumbGrip;
            set
            {
                _thumbGrip = value; Invalidate();
            }
        }

        private bool _thumbGradient = false;
        [Category("Misc.Thumb")]
        public bool ThumbGradient
        {
            get => _thumbGradient;
            set
            {
                _thumbGradient = value; Invalidate();
            }
        }

        private float _thumbGradientAngle = 0F;
        [Category("Misc.Thumb")]
        public float ThumbGradientAngle
        {
            get => _thumbGradientAngle;
            set
            {
                _thumbGradientAngle = value; Invalidate();
            }
        }

        private int _thumbWidth = 15;
        [Category("Misc.Thumb")]
        public int ThumbWidth
        {
            get => _thumbWidth;
            set
            {
                _thumbWidth = value; Invalidate();
            }
        }

        private int _thumbShiftX = 0;
        [Category("Misc.Thumb")]
        public int ThumbShiftX
        {
            get => _thumbShiftX;
            set
            {
                _thumbShiftX = value; Invalidate();
            }
        }

        private int _thumbShiftY = 0;
        [Category("Misc.Thumb")]
        public int ThumbShiftY
        {
            get => _thumbShiftY;
            set
            {
                _thumbShiftY = value; Invalidate();
            }
        }



        private int _thumbGripSpacing = 6;
        [Category("Misc.Thumb")]
        public int ThumbGripSpacing
        {
            get => _thumbGripSpacing;
            set
            {
                _thumbGripSpacing = value; Invalidate();
            }
        }

        private int _thumbGripThickness = 2;
        [Category("Misc.Thumb")]
        public int ThumbGripThickness
        {
            get => _thumbGripThickness;
            set
            {
                _thumbGripThickness = value; Invalidate();
            }
        }

        private bool thumbCircle = true;
        [Category("Misc.Thumb")]
        public bool CirclularThumb
        {
            get => thumbCircle;
            set
            {
                thumbCircle = value;
                Invalidate();
            }
        }

        private bool _thumbGripCircular = false;
        [Category("Misc.Thumb")]
        public bool ThumbGripCircular
        {
            get => _thumbGripCircular;
            set
            {
                _thumbGripCircular = value; Invalidate();
            }
        }


        #endregion

        // Declare the Scroll event
        public event EventHandler Scroll;

        // Method to trigger the Scroll event
        protected virtual void OnScroll(EventArgs e)
        {
            Scroll?.Invoke(this, e);
        }


        public int Value
        {
            get => _value;
            set
            {
                if (value < _minimum || value > _maximum) throw new ArgumentOutOfRangeException();
                _value = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
                Invalidate();
            }
        }

        private int trackSize = 4;
        [Category("Misc.Track")]
        public int TrackSize
        {
            get => trackSize;
            set { if (value <= 0) { value = 1; };  trackSize = value; Invalidate(); }
        }

        private int _trackBorderSize = 2;
        [Category("Misc.Track")]
        public int TrackBorderSize
        {
            get => _trackBorderSize;
            set
            {
                _trackBorderSize = value; Invalidate();
            }
        }

        private bool _trackGradient = false;
        [Category("Misc.Track")]
        public bool TrackGradient
        {
            get => _trackGradient;
            set
            {
                _trackGradient = value; Invalidate();
            }
        }

        private float _trackGradientAngle = 0F;
        [Category("Misc.Track")]
        public float TrackGradientAngle
        {
            get => _trackGradientAngle;
            set
            {
                _trackGradientAngle = value; Invalidate();
            }
        }

        private bool _progressGradient = false;
        [Category("Misc.Track")]
        public bool ProgressGradient
        {
            get => _progressGradient;
            set
            {
                _progressGradient = value; Invalidate();
            }
        }

        private float _progressGradientAngle = 0F;
        [Category("Misc.Track")]
        public float ProgressGradientAngle
        {
            get => _progressGradientAngle;
            set
            {
                _progressGradientAngle = value; Invalidate();
            }
        }

        private bool _isHorizontal = false;
        [Category("Misc.Orientation")]
        public bool IsHorizontal
        {
            get => _isHorizontal;
            set
            {
                SwapOrientation();  _isHorizontal = value;  Invalidate();
            }
        }

        private void SwapOrientation()
        {
            int temp = this.Width;
            this.Width = this.Height;
            this.Height = temp;

            int _tH = _thumbHeight;
            _thumbHeight = _thumbWidth;
            _thumbWidth = _tH;

            if (!_isHorizontal) //if you're vertical, going to horizontal... add 90 to all gradient angles
            {
                _progressGradientAngle += 90F;
                _thumbGradientAngle += 90F;
                _trackGradientAngle += 90F;
            }
            else
            {
                _progressGradientAngle -= 90F;
                _thumbGradientAngle -= 90F;
                _trackGradientAngle -= 90F;
            }
            

        }


        #region THUMBBORDER
        //BORDER COLOR
        private UIRole _thumbBorderColorRole = UIRole.EmphasizedBorders;
        private ThemeColorShade _thumbBorderColorTheme = ThemeColorShade.Primary_700;
        private Color _thumbBorderColor = Color.FromArgb(0, 90, 175);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole ThumbBorderRole { get => _thumbBorderColorRole; set { _thumbBorderColorRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade ThumbBorderTheme
        {
            get => _thumbBorderColorTheme; set
            {
                _thumbBorderColorTheme = value;
                if (!_ignoreTheme) { BorderColor = ThemeManager.Instance.GetThemeColorShade(_thumbBorderColorTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color BorderColor { get => _thumbBorderColor; set { _thumbBorderColor = value; Invalidate(); } }
        #endregion
        #region TRACK
        //TRACK COLOR
        private UIRole _trackRole = UIRole.ScrollbarTrack;
        private ThemeColorShade _trackTheme = ThemeColorShade.Neutral_500;
        private Color _trackColor = Color.FromArgb(128, 128, 128);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole TrackRole { get => _trackRole; set { _trackRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade TrackColorTheme
        {
            get => _trackTheme; set
            {
                _trackTheme = value;
                if (!_ignoreTheme) { TrackColor = ThemeManager.Instance.GetThemeColorShade(_trackTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color TrackColor { get => _trackColor; set { _trackColor = value; Invalidate(); } }
        #endregion
        #region TRACKCOLORFADE
        //TRACKCOLORFADE
        private UIRole _trackColorFadeRole = UIRole.SectionDivider;
        private ThemeColorShade _trackColorFadeTheme = ThemeColorShade.Neutral_800;
        private Color _trackColorFade = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_800);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole TrackColorFadeRole { get => _trackColorFadeRole; set { _trackColorFadeRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade TrackColorFadeTheme
        {
            get => _trackColorFadeTheme; set
            {
                _trackColorFadeTheme = value;
                if (!_ignoreTheme) { TrackColorFade = ThemeManager.Instance.GetThemeColorShade(_trackColorFadeTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color TrackColorFade { get => _trackColorFade; set { _trackColorFade = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region PROGRESS
        //PROGRESS COLOR
        private UIRole _progressRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _progressTheme = ThemeColorShade.Primary_500;
        private Color _progressColor = Color.FromArgb(0, 128, 200);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole ProgressRole { get => _progressRole; set { _progressRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade ProgressTheme
        {
            get => _progressTheme; set
            {
                _progressTheme = value;
                if (!_ignoreTheme) { ProgressColor = ThemeManager.Instance.GetThemeColorShade(_progressTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color ProgressColor { get => _progressColor; set { _progressColor = value; Invalidate(); } }
        #endregion
        #region TRACKPROGRESSFADE
        //TRACKPROGRESSFADE
        private UIRole _trackProgressFadeRole = UIRole.SectionDivider;
        private ThemeColorShade _trackProgressFadeTheme = ThemeColorShade.Primary_700;
        private Color _trackProgressFade = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_700);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole TrackProgressFadeRole { get => _trackProgressFadeRole; set { _trackProgressFadeRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade TrackProgressFadeTheme
        {
            get => _trackProgressFadeTheme; set
            {
                _trackProgressFadeTheme = value;
                if (!_ignoreTheme) { TrackProgressFade = ThemeManager.Instance.GetThemeColorShade(_trackProgressFadeTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color TrackProgressFade { get => _trackProgressFade; set { _trackProgressFade = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region THUMB
        //THUMB COLOR
        private UIRole _thumbRole = UIRole.ScrollbarThumb;
        private ThemeColorShade _thumbTheme = ThemeColorShade.Neutral_100;
        private Color _thumbColor = Color.FromArgb(250, 250, 250);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole ThumbRole { get => _thumbRole; set { _thumbRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade ThumbColorTheme
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
        #region THUMBGRIPCOLOR
        //THUMBGRIPCOLOR
        private UIRole _thumbGripColorRole = UIRole.SectionDivider;
        private ThemeColorShade _thumbGripColorTheme = ThemeColorShade.Neutral_500;
        private Color _thumbGripColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole ThumbGripColorRole { get => _thumbGripColorRole; set { _thumbGripColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade ThumbGripColorTheme
        {
            get => _thumbGripColorTheme; set
            {
                _thumbGripColorTheme = value;
                if (!_ignoreTheme) { ThumbGripColor = ThemeManager.Instance.GetThemeColorShade(_thumbGripColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color ThumbGripColor { get => _thumbGripColor; set { _thumbGripColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region TRACKBORDER
        //TRACKBORDER
        private UIRole _trackBorderRole = UIRole.EmphasizedBorders;
        private ThemeColorShade _trackBorderTheme = ThemeColorShade.Neutral_700;
        private Color _trackBorder = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_700);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole TrackBorderRole { get => _trackBorderRole; set { _trackBorderRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade TrackBorderTheme
        {
            get => _trackBorderTheme; set
            {
                _trackBorderTheme = value;
                if (!_ignoreTheme) { TrackBorder = ThemeManager.Instance.GetThemeColorShade(_trackBorderTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color TrackBorder { get => _trackBorder; set { _trackBorder = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region THUMBCOLORFADE
        //THUMBCOLORFADE
        private UIRole _thumbColorFadeRole = UIRole.SectionDivider;
        private ThemeColorShade _thumbColorFadeTheme = ThemeColorShade.Neutral_500;
        private Color _thumbColorFade = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole ThumbColorFadeRole { get => _thumbColorFadeRole; set { _thumbColorFadeRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade ThumbColorFadeTheme
        {
            get => _thumbColorFadeTheme; set
            {
                _thumbColorFadeTheme = value;
                if (!_ignoreTheme) { ThumbColorFade = ThemeManager.Instance.GetThemeColorShade(_thumbColorFadeTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color ThumbColorFade { get => _thumbColorFade; set { _thumbColorFade = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion


        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                ThumbColor = ThemeManager.Instance.GetThemeColorShade(ThumbColorTheme);
                ProgressColor = ThemeManager.Instance.GetThemeColorShade(ProgressTheme);
                TrackColor = ThemeManager.Instance.GetThemeColorShade(TrackColorTheme);
                BorderColor = ThemeManager.Instance.GetThemeColorShade(ThumbBorderTheme);
                ThumbGripColor = ThemeManager.Instance.GetThemeColorShadeOffset(ThumbGripColorTheme);
                ThumbColorFade = ThemeManager.Instance.GetThemeColorShadeOffset(ThumbColorFadeTheme);
                TrackColorFade = ThemeManager.Instance.GetThemeColorShadeOffset(TrackColorFadeTheme);
                TrackProgressFade = ThemeManager.Instance.GetThemeColorShadeOffset(TrackProgressFadeTheme);
            }
        }

        public ErrataTrackBarV()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            this.DoubleBuffered = true;
            Width = 40;
            Height = 150;
            TrackSize = 5;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }




        private Rectangle trackRect;
        private Rectangle thumbRect;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;


            if (_isHorizontal) 
            { 
                DrawTrackH(g);
                DrawProgressH(g);
                DrawTrackBorderH(g);
                DrawThumbH(g);
                DrawThumbGripH(g);
            } 
            else 
            { 
                DrawTrack(g); 
                DrawProgress(g);
                DrawTrackBorder(g);
                DrawThumb(g);
                DrawThumbGrip(g);
            }
            //DrawProgress(g);
            //DrawTrackBorder(g);
            //DrawThumb(g);
            //DrawThumbGrip(g);
            
        }

        // Method to draw the horizontal track
        private void DrawTrackH(Graphics g)
        {
            // Horizontal track: the width of the track will span the entire width of the control
            int trackX = _thumbWidth / 2; // Set starting point on the X axis
            int trackY = this.Height / 2 - (trackSize / 2); // Center the track vertically
            int trackWidth = this.Width - _thumbWidth - _thumbBorderWidth;
            trackRect = new Rectangle(trackX, trackY, trackWidth, trackSize);

            LinearGradientBrush trackGradientBrush = new LinearGradientBrush(trackRect, TrackColor, _trackColorFade, _trackGradientAngle);

            using (Brush trackBrush = new SolidBrush(TrackColor))
            {
                g.FillRectangle(_trackGradient ? trackGradientBrush : trackBrush, trackRect);
            }
        }
        // Method to draw the track
        private void DrawTrack(Graphics g)
        {
            // Draw the track
            int trackX = this.Width / 2 - (trackSize / 2);
            int trackY = _thumbHeight / 2;
            int trackHeight = this.Height - _thumbHeight - _thumbBorderWidth;
            trackRect = new Rectangle(trackX, trackY, trackSize, trackHeight);
            Rectangle trackBorder = new Rectangle(trackRect.X - _trackBorderSize, trackRect.Y - _trackBorderSize, trackRect.Width + 2 * _trackBorderSize, trackRect.Height + 2 * _trackBorderSize);

            LinearGradientBrush trackGradientBrush = new LinearGradientBrush(trackRect,    // Start Point
                                TrackColor, _trackColorFade, _trackGradientAngle);

            using (Brush trackBrush = new SolidBrush(TrackColor))
            {
                g.FillRectangle(_trackGradient ? trackGradientBrush : trackBrush, trackRect);
            }

        }
        private void DrawProgress(Graphics g)
        {
            // Draw the progress
            // The progress height should decrease as the value increases, so we reverse the calculation
            int progressHeight = (int)((float)(Value - Minimum) / (_maximum - _minimum) * (trackRect.Height));
            int progressY = this.Height - progressHeight - _thumbHeight - _thumbBorderWidth;
            int trackX = this.Width / 2 - (trackSize / 2);

            // Create the progress rectangle
            Rectangle progressRect = new Rectangle(trackX, progressY + trackRect.Y, trackRect.Width, Math.Max(progressHeight, 1));

            LinearGradientBrush progressGradientBrush = new LinearGradientBrush(progressRect,    // Start Point
                                _progressColor, _trackProgressFade, _progressGradientAngle);

            using (Brush progressBrush = new SolidBrush(ProgressColor))
            {
                g.FillRectangle(_progressGradient ? progressGradientBrush : progressBrush, progressRect);
            }
        }
        // Method to draw the progress in horizontal mode
        private void DrawProgressH(Graphics g)
        {
            int progressWidth = (int)((float)(Value - Minimum) / (_maximum - _minimum) * trackRect.Width);
            int progressX = trackRect.X;
            int progressY = trackRect.Y;

            if (progressWidth <= 0) { progressWidth = 1; }

            Rectangle progressRect = new Rectangle(progressX, progressY, progressWidth, trackRect.Height);

            LinearGradientBrush progressGradientBrush = new LinearGradientBrush(progressRect, _progressColor, _trackProgressFade, _progressGradientAngle);

            using (Brush progressBrush = new SolidBrush(ProgressColor))
            {
                g.FillRectangle(_progressGradient ? progressGradientBrush : progressBrush, progressRect);
            }
        }
        private void DrawTrackBorder(Graphics g)
        {
            //Draw the track border
            if (_trackBorderSize > 0)
            {
                using (Pen trackBorderBrush = new Pen(_trackBorder, TrackBorderSize))
                {
                    g.DrawRectangle(trackBorderBrush, trackRect);
                }
            }
        }
        // Method to draw the track border in horizontal mode
        private void DrawTrackBorderH(Graphics g)
        {
            // Draw the track border in horizontal mode
            if (_trackBorderSize > 0)
            {
                using (Pen trackBorderPen = new Pen(_trackBorder, _trackBorderSize))
                {
                    g.DrawRectangle(trackBorderPen, trackRect);
                }
            }
        }
        private void DrawThumb(Graphics g)
        {
            //DRAW THUMB
            float thumbAdjustPct = ((float)Value - (float)Minimum) / ((float)_maximum - (float)_minimum);
            //0 at bottom, 1.0 at top

            //linearly scale thumb offset
            float thumbOffset = _thumbHeight / 2 + _thumbBorderWidth / 2;
            float thumbPosition = thumbAdjustPct * (2 * thumbOffset) - thumbOffset;
            int progressHeight = (int)((float)(Value - Minimum) / (_maximum - _minimum) * (trackRect.Height));

            if (!ThumbLinearize) { thumbPosition = 0; }

            // Draw the thumb
            // Adjust the thumb's Y position to match the progress position
            int thumbY = this.Height - progressHeight - _thumbHeight - _thumbBorderWidth;


            thumbRect = new Rectangle(Width / 2 - (_thumbWidth / 2) + ThumbShiftX, thumbY + ThumbShiftY + (int)thumbPosition, _thumbWidth, _thumbHeight);

            LinearGradientBrush brushGradient = new LinearGradientBrush(thumbRect,    // Start Point
                                _thumbColor, _thumbColorFade, _thumbGradientAngle);



            using (Brush thumbBrush = new SolidBrush(ThumbColor))
            {
                if (thumbCircle)
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    g.FillEllipse(_thumbGradient ? brushGradient : thumbBrush, thumbRect);
                }
                else
                {
                    g.FillRectangle(_thumbGradient ? brushGradient : thumbBrush, thumbRect);
                }
            }

            // Draw the THUMB border
            if (_thumbBorderWidth > 0)
            {
                using (Pen borderPen = new Pen(BorderColor, _thumbBorderWidth))
                {
                    if (thumbCircle)
                    {
                        g.DrawEllipse(borderPen, thumbRect);
                    }
                    else
                    {
                        g.DrawRectangle(borderPen, thumbRect);
                    }
                }
            }
        }
        // Method to draw the thumb in horizontal mode
        private void DrawThumbH(Graphics g)
        {
            float thumbAdjustPct = ((float)Value - (float)Minimum) / ((float)_maximum - (float)_minimum);
            float thumbOffset = _thumbWidth / 2 - _thumbBorderWidth / 2;
            float thumbPosition = thumbAdjustPct * (2 * thumbOffset) - thumbOffset;


            int progressWidth = (int)((float)(Value - Minimum) / (_maximum - _minimum) * (trackRect.Width));
            if (progressWidth <= 0) { progressWidth = 1; }

            if (!ThumbLinearize) { thumbPosition = 0; }

            int thumbX = progressWidth;
            //Debug.Print($"thumbX = track.X[{trackRect.X}] +  progressW[{progressWidth}]@{thumbAdjustPct}%OF{trackRect.Width}width + _thumbW[{_thumbWidth}] + _thumbBW[{_thumbBorderWidth}]");
            
            thumbRect = new Rectangle(
                        thumbX + _thumbShiftX - (int)thumbPosition , 
                        Height / 2 - (_thumbHeight / 2) + ThumbShiftY, 
                        _thumbWidth, 
                        _thumbHeight);

            LinearGradientBrush brushGradient = new LinearGradientBrush(thumbRect, _thumbColor, _thumbColorFade, _thumbGradientAngle);

            using (Brush thumbBrush = new SolidBrush(ThumbColor))
            {
                if (thumbCircle)
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.FillEllipse(_thumbGradient ? brushGradient : thumbBrush, thumbRect);
                }
                else
                {
                    g.FillRectangle(_thumbGradient ? brushGradient : thumbBrush, thumbRect);
                }
            }

            // Draw the thumb border
            if (_thumbBorderWidth > 0)
            {
                using (Pen borderPen = new Pen(BorderColor, _thumbBorderWidth))
                {
                    if (thumbCircle)
                    {
                        g.DrawEllipse(borderPen, thumbRect);
                    }
                    else
                    {
                        g.DrawRectangle(borderPen, thumbRect);
                    }
                }
            }
        }

        private void DrawThumbGrip(Graphics g)
        {
            // Draw the horizontal line in the middle of the thumb
            // Calculate the position for the horizontal line
            if (ThumbGrip)
            {
                int lineY = thumbRect.Top + (thumbRect.Height / 2);
                int xNudge = 0;
                if (ThumbBorderWidth % 2 != 0) { xNudge = 1; }
                Brush ellipseBrush = new SolidBrush(_thumbGripColor);
                using (Pen linePen = new Pen(_thumbGripColor, _thumbGripThickness)) // Set your line color and width here
                {
                    if (!_thumbGripCircular)
                    {
                        g.DrawLine(linePen, thumbRect.Left + _thumbGripSpacing + xNudge, lineY, thumbRect.Right - _thumbGripSpacing, lineY); // Draw the line horizontally

                    }
                    else
                    {
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        // If circular grip is enabled, draw a circle (ellipse) with thumbGripSpacing determining its size
                        float gripWidth = thumbRect.Width - 2 * _thumbGripSpacing;  // Width of the ellipse (reduced by spacing)
                        float gripHeight = thumbRect.Height - 2 * _thumbGripSpacing; // Height of the ellipse (reduced by spacing)

                        // Ensure the ellipse is centered inside the thumbRect
                        float ellipseX = thumbRect.Left + _thumbGripSpacing;
                        float ellipseY = thumbRect.Top + _thumbGripSpacing;

                        // Fill the ellipse in the middle of the thumb
                        g.FillEllipse(ellipseBrush, ellipseX, ellipseY, gripWidth, gripHeight);
                    }

                }
            }
        }
        // Method to draw the thumb grip in horizontal mode
        private void DrawThumbGripH(Graphics g)
        {
            if (ThumbGrip)
            {
                int lineY = thumbRect.Top + (thumbRect.Height / 2);
                int lineX = thumbRect.Left + (thumbRect.Width / 2);
                int xNudge = (ThumbBorderWidth % 2 != 0) ? 1 : 0;

                Brush ellipseBrush = new SolidBrush(_thumbGripColor);
                using (Pen linePen = new Pen(_thumbGripColor, _thumbGripThickness))
                {
                    if (!_thumbGripCircular)
                    {
                        g.DrawLine(linePen, lineX, thumbRect.Top + _thumbGripSpacing + xNudge, lineX, thumbRect.Bottom - _thumbGripSpacing); // Draw the line horizontally
                    }
                    else
                    {
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        float gripWidth = thumbRect.Width - 2 * _thumbGripSpacing;
                        float gripHeight = thumbRect.Height - 2 * _thumbGripSpacing;

                        float ellipseX = thumbRect.Left + _thumbGripSpacing;
                        float ellipseY = thumbRect.Top + _thumbGripSpacing;

                        g.FillEllipse(ellipseBrush, ellipseX, ellipseY, gripWidth, gripHeight);
                    }
                }
            }
        }




        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                UpdateValueFromMouse(e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                UpdateValueFromMouse(e);
            }
        }

        private void UpdateValueFromMouse(MouseEventArgs e)
        {
            int mousePosition;

            if (_isHorizontal)
            {
                mousePosition = e.X;
                // Adjust the calculation to make rightward movement increase the value
                var newValue = Minimum + (int)((float)mousePosition / Width * (Maximum - Minimum));
                Value = Math.Max(Minimum, Math.Min(Maximum, newValue));
            }
            else
            {
                mousePosition = e.Y;
                // The existing calculation remains the same for vertical movement
                var newValue = Maximum - (int)((float)mousePosition / Height * (Maximum - Minimum));
                Value = Math.Max(Minimum, Math.Min(Maximum, newValue));
            }
        }
    }
}