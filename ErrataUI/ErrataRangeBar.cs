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
    public class ErrataRangeBar : Control
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
        
        
        private bool thumbCircle = true;
        private int thumbSize = 10;
        private int trackHeight = 4;
        private int borderWidth = 2;  // Default border width
        private bool draggingMin = false;
        private bool draggingMax = false;
        private int _trackWidth = 0;










        public bool CirclularThumb
        {
            get => thumbCircle;
            set
            {
                thumbCircle = value;
                Invalidate();
            }
        }

        private int _pinch = 3;
        public int Pinch
        {
            get => _pinch;
            set
            {
                _pinch = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Range")]
        [Description("Minimum value of the track bar.")]
        private int selectedMin;
        public int Minimum
        {
            get => minimum;
            set
            {
                if (value >= maximum) throw new ArgumentOutOfRangeException(nameof(Minimum));
                minimum = value;
                if (selectedMin < minimum) selectedMin = minimum;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Range")]
        [Description("Maximum value of the track bar.")]
        private int selectedMax;
        public int Maximum
        {
            get => maximum;
            set
            {
                if (value <= minimum) throw new ArgumentOutOfRangeException(nameof(Maximum));
                maximum = value;
                if (selectedMax > maximum) selectedMax = maximum;
                Invalidate();
            }
        }

        public event EventHandler MinChanged;
        public event EventHandler MaxChanged;
        [Browsable(true)]
        [Category("Range")]
        [Description("Selected minimum value.")]
        public int SelectedMin
        {
            get => selectedMin;
            set
            {
                if (value < minimum || value > selectedMax) throw new ArgumentOutOfRangeException(nameof(SelectedMin));
                selectedMin = value;
                MinChanged?.Invoke(this, EventArgs.Empty); // Raise MinChanged event
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Range")]
        [Description("Selected maximum value.")]
        public int SelectedMax
        {
            get => selectedMax;
            set
            {
                if (value < selectedMin || value > maximum) throw new ArgumentOutOfRangeException(nameof(SelectedMax));
                selectedMax = value;
                MaxChanged?.Invoke(this, EventArgs.Empty); // Raise MaxChanged event
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

        public ErrataRangeBar()
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

            int minB = minimum + 1;

            // Draw the track
            int trackY = this.Height / 2 - (trackSize / 2);
            int trackX = thumbSize / 2;
            //_trackWidth = this.Width - 2 * trackX;
            _trackWidth = this.Width - thumbSize - borderWidth; 

            Rectangle trackRect = new Rectangle(trackX, trackY, _trackWidth, trackSize);
            using (Brush trackBrush = new SolidBrush(TrackColor))
            {
                g.FillRectangle(trackBrush, trackRect);
            }

            // Draw the progress
            int minThumbX = (int)((float)(selectedMin - minimum) / (maximum - minimum) * (_trackWidth));
            int maxThumbX = (int)((float)(selectedMax - minimum) / (maximum - minimum) * (_trackWidth));
            Rectangle selectedRangeRect = new Rectangle(minThumbX + thumbSize / 2, trackY, maxThumbX - minThumbX, trackRect.Height);
            using (Brush progressBrush = new SolidBrush(ProgressColor))
            {
                g.FillRectangle(progressBrush, selectedRangeRect);
            }

            // Draw the minimum thumb
            Rectangle minThumbRect = new Rectangle(minThumbX + 2, Height / 2 - (thumbSize / 2), thumbSize, thumbSize);
            using (Brush thumbBrush = new SolidBrush(ThumbColor))
            {
                if (thumbCircle)
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillEllipse(thumbBrush, minThumbRect);
                }
                else
                {
                    g.FillRectangle(thumbBrush, minThumbRect);
                }
            }

            // Draw the maximum thumb
            Rectangle maxThumbRect = new Rectangle(maxThumbX - 1, Height / 2 - (thumbSize / 2), thumbSize, thumbSize);
            using (Brush thumbBrush = new SolidBrush(ThumbColor))
            {
                if (thumbCircle)
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillEllipse(thumbBrush, maxThumbRect);
                }
                else
                {
                    g.FillRectangle(thumbBrush, maxThumbRect);
                }
            }

            // Draw the thumb borders
            using (Pen borderPen = new Pen(BorderColor, borderWidth))
            {
                if (thumbCircle)
                {
                    g.DrawEllipse(borderPen, maxThumbRect);
                    g.DrawEllipse(borderPen, minThumbRect);
                }
                else
                {
                    g.DrawRectangle(borderPen, maxThumbRect);
                    g.DrawRectangle(borderPen, minThumbRect);
                }
            }
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            int minThumbX = (int)((float)(selectedMin - minimum) / (maximum - minimum) * (this.Width - thumbSize));
            int maxThumbX = (int)((float)(selectedMax - minimum) / (maximum - minimum) * (this.Width - thumbSize));
            Rectangle minThumbRect = new Rectangle(minThumbX, this.Height / 2 - thumbSize / 2, thumbSize, thumbSize);
            Rectangle maxThumbRect = new Rectangle(maxThumbX, this.Height / 2 - thumbSize / 2, thumbSize, thumbSize);

            if (minThumbRect.Contains(e.Location))
            {
                draggingMin = true;
            }
            else if (maxThumbRect.Contains(e.Location))
            {
                draggingMax = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int trackMaxX = thumbSize / 2 + (2 * borderWidth);
            int trackMinX = this.Width - thumbSize;

            if (draggingMin)
            {

                //int relativeX = Math.Max(thumbSize / 2, Math.Min(e.X, _trackWidth));
                int relativeX = Math.Max(thumbSize / 2, Math.Min(e.X - thumbSize / 2, this.Width - thumbSize)) - 2 * borderWidth;
                Debug.Print($"relativeX={relativeX} , e.X={e.X}, e.X-trackX={e.X - trackMinX}, _trackWidth={_trackWidth}");
                SelectedMin = minimum + (int)((float)(relativeX) / (this.Width - thumbSize) * (maximum - minimum));

            }
            else if (draggingMax)
            {
                //int relativeX = Math.Max(thumbSize / 2, Math.Min(e.X, _trackWidth));
                int relativeX = Math.Max(0, Math.Min(e.X - trackMaxX, _trackWidth));
                Debug.Print($"relativeX={relativeX} , e.X={e.X}, e.X-trackX={e.X - trackMaxX}, _trackWidth={_trackWidth}");
                SelectedMax = minimum + (int)((float)relativeX / _trackWidth * (maximum - minimum));
                //0 + [ ] / 90 * 100
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            draggingMin = false;
            draggingMax = false;
        }

        private void UpdateValueFromMouse(int mouseX)
        {
            int trackWidth = this.Width - thumbSize;
            int relativeX = Math.Max(0, Math.Min(mouseX - thumbSize / 2, trackWidth));
            //Value = minimum + (int)((float)relativeX / trackWidth * (maximum - minimum));
        }
    }
}
