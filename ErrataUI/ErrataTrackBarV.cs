using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataTrackBarV : Control
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



        private int _minimum = 0;
        private int _maximum = 100;
        private int _value = 50;
        private int _thumbHeight = 20;
        private bool thumbCircle = true;
        private int trackWidth = 10;
        private int borderWidth = 2;  // Default border width
        private int trackSize = 4;
        private int _thumbSize = 10;
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
        public bool CirclularThumb
        {
            get => thumbCircle;
            set
            {
                thumbCircle = value;
                Invalidate();
            }
        }


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

        
        public int TrackSize
        {
            get => trackSize;
            set { trackSize = value; Invalidate(); }
        }
        public int BorderWidth
        {
            get => borderWidth;
            set { borderWidth = value; Invalidate(); }
        }

        public int ThumbSize
        {
            get => _thumbSize;
            set { _thumbSize = value; Invalidate(); }
        }

        #region BORDER
        //BORDER COLOR
        private UIRole _borderRole = UIRole.EmphasizedBorders;
        private ThemeColorShade _borderTheme = ThemeColorShade.Primary_700;
        private Color _borderColor = Color.FromArgb(0, 90, 175);
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
        public ThemeColorShade TrackTheme
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



        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                ThumbColor = ThemeManager.Instance.GetThemeColorShade(ThumbTheme);
                ProgressColor = ThemeManager.Instance.GetThemeColorShade(ProgressTheme);
                TrackColor = ThemeManager.Instance.GetThemeColorShade(TrackTheme);
                BorderColor = ThemeManager.Instance.GetThemeColorShade(BorderTheme);
            }
        }

        public ErrataTrackBarV()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            this.DoubleBuffered = true;
            Width = 40;
            Height = 150;
            TrackSize = 5;
            ThumbSize = 15;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }





        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Draw the track
            int trackX = this.Width / 2 - (trackSize / 2);
            Rectangle trackRect = new Rectangle(trackX, _thumbSize / 2, trackSize, this.Height - _thumbSize - (borderWidth));
            using (Brush trackBrush = new SolidBrush(TrackColor))
            {
                g.FillRectangle(trackBrush, trackRect);
            }

            // Draw the progress
            // The progress height should decrease as the value increases, so we reverse the calculation
            int progressHeight = (int)((float)(Value - Minimum) / (_maximum - _minimum) * (this.Height - _thumbSize - borderWidth));
            Rectangle progressRect = new Rectangle(trackX, this.Height - progressHeight - borderWidth, trackRect.Width, progressHeight );

            using (Brush progressBrush = new SolidBrush(ProgressColor))
            {
                g.FillRectangle(progressBrush, progressRect);
            }

            // Draw the thumb
            // Adjust the thumb's Y position to match the progress position
            int thumbY = this.Height - progressHeight - _thumbSize - borderWidth;
            thumbY = Math.Max(thumbY, _thumbSize / 2);  // Make sure it does not go below the bottom



            Rectangle thumbRect = new Rectangle(Width / 2 - (_thumbSize / 2), thumbY, _thumbSize, _thumbSize);
            using (Brush thumbBrush = new SolidBrush(ThumbColor))
            {
                if (thumbCircle)
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillEllipse(thumbBrush, thumbRect);
                }
                else
                {
                    g.FillRectangle(thumbBrush, thumbRect);
                }
            }

            // Draw the border
            using (Pen borderPen = new Pen(BorderColor, borderWidth))
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


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                UpdateValueFromMouse(e.Y);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                UpdateValueFromMouse(e.Y);
            }
        }

        private void UpdateValueFromMouse(int mouseY)
        {
            var newValue = Maximum - (int)((float)mouseY / Height * (Maximum - Minimum));
            Value = Math.Max(Minimum, Math.Min(Maximum, newValue));
        }
    }
}