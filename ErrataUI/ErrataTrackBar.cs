using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataTrackBar : Control
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



        private int minimum = 0;
        private int maximum = 100;
        private int value = 50;
        private bool thumbCircle = true;
        private int thumbSize = 10;
        private int trackHeight = 4;
        private int borderWidth = 2;  // Default border width
        public event EventHandler ValueChanged;

        public int Minimum
        {
            get => minimum;
            set
            {
                if (value >= maximum) throw new ArgumentOutOfRangeException(nameof(Minimum));
                minimum = value;
                if (this.value < minimum) this.value = minimum;
                Invalidate();
            }
        }

        public int Maximum
        {
            get => maximum;
            set
            {
                if (value <= minimum) throw new ArgumentOutOfRangeException(nameof(Maximum));
                maximum = value;
                if (this.value > maximum) this.value = maximum;
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


        private int _value = 0;
        public int Value
        {
            get => value;
            set
            {
                if (value < minimum || value > maximum) throw new ArgumentOutOfRangeException(nameof(Value));
                if (_value != value)
                {
                    _value = value;
                    //OnScroll(EventArgs.Empty); // Trigger the Scroll event
                }
                this.value = value;
                OnScroll(EventArgs.Empty); // Trigger the Scroll event
                ValueChanged?.Invoke(this, EventArgs.Empty);
                Invalidate();
            }
        }





        public int ThumbSize
        {
            get => thumbSize;
            set { thumbSize = value; Invalidate(); }
        }
        public int BorderWidth
        {
            get => borderWidth;
            set { borderWidth = value; Invalidate(); }
        }

        private int trackSize = 10;
        public int TrackSize
        {
            get => trackSize;
            set { trackSize = value; Invalidate(); }
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

        public ErrataTrackBar()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            this.DoubleBuffered = true;
            this.Size = new Size(200, 30);
            this.Cursor = Cursors.Default;
            TrackSize = 5;
            ThumbSize = 15;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }





        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Draw the track
            int trackY = this.Height / 2 - (trackSize / 2);
            Rectangle trackRect = new Rectangle(thumbSize / 2, trackY, this.Width - thumbSize - borderWidth, trackSize);
            using (Brush trackBrush = new SolidBrush(TrackColor))
            {
                g.FillRectangle(trackBrush, trackRect);
            }

            // Draw the progress
            int progressWidth = (int)((float)(value - minimum) / (maximum - minimum) * (this.Width - thumbSize - borderWidth));
            Rectangle progressRect = new Rectangle(trackRect.X, trackY, progressWidth + borderWidth, trackRect.Height);
            using (Brush progressBrush = new SolidBrush(ProgressColor))
            {
                g.FillRectangle(progressBrush, progressRect);
            }

            // Draw the thumb
            int thumbX = progressWidth + (thumbSize / 2) - (thumbSize / 2); // Adjust for thumb centering
            thumbX = Math.Max(thumbX, thumbSize / 2); // Ensure thumb doesn't go beyond the track



            Rectangle thumbRect = new Rectangle(thumbX, Height / 2 - (thumbSize / 2), thumbSize, thumbSize);
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
                UpdateValueFromMouse(e.X);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                UpdateValueFromMouse(e.X);
            }
        }

        private void UpdateValueFromMouse2(int mouseX)
        {
            int trackWidth = this.Width - thumbSize - borderWidth;
            

            //int relativeX = Math.Max(0, Math.Min(mouseX - thumbSize / 2, trackWidth));
            
            int relativeX = Math.Max(0, Math.Min(mouseX - thumbSize - borderWidth + 2, trackWidth));
            Debug.Print($"relX=Max(0, Min({mouseX - thumbSize - borderWidth + 2}, {trackWidth}))");
            Debug.Print($"trackW={trackWidth}; eui-track,mouseX={mouseX}; relativeX={relativeX}");
            //Value = minimum + (int)((float)relativeX / trackWidth * (maximum - minimum));
            if (relativeX <= 0)
            {
                Debug.Print($"relX <= 0, val={minimum}");
                Value = minimum;
            }
            else if (relativeX >= 100)
            {
                Debug.Print($"relX >= 100, val={maximum}");
                Value = maximum;
            }
            else
            {
                Value = Math.Clamp(minimum + (int)((float)relativeX / 100 * (maximum - minimum)), minimum, maximum);
            }
            
        }

        private void UpdateValueFromMouse(int mouseX)
        {
            int trackWidth = this.Width - thumbSize - borderWidth;

            // Ensure mouse position is within valid track range
            int relativeX = Math.Max(0, Math.Min(mouseX - thumbSize / 2, trackWidth));

            // Correct mapping of relativeX to the value range
            float normalizedPosition = (float)relativeX / trackWidth; // Normalized between 0 and 1
            int newValue = (int)Math.Round(minimum + normalizedPosition * (maximum - minimum));

            // Ensure we reach min/max correctly
            Value = Math.Clamp(newValue, minimum, maximum);
        }



    }
}
