using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataProgressSpinner : Control
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


        private Timer _timer;
        private float _currentAngle;



        #region BACKGROUNDCIRCLE
        //BACKGROUNDCIRCLE COLOR
        private UIRole _backgroundCircleRole = UIRole.ScrollbarTrack;
        private ThemeColorShade _backgroundCircleTheme = ThemeColorShade.Neutral_300;
        private Color _backgroundCircleColor = Color.FromArgb(128, 128, 128);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole BackgroundCircleRole { get => _backgroundCircleRole; set { _backgroundCircleRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade BackgroundCircleTheme
        {
            get => _backgroundCircleTheme; set
            {
                _backgroundCircleTheme = value;
                if (!_ignoreTheme) { BackgroundCircleColor = ThemeManager.Instance.GetThemeColorShade(_backgroundCircleTheme); }
            }
        }
        [Category("Misc")]
        [Description("The color of the unfilled part of the spinner.")]
        public Color BackgroundCircleColor { get => _backgroundCircleColor; set { _backgroundCircleColor = value; Invalidate(); } }
        #endregion
        #region SPINNER
        //SPINNER COLOR
        private UIRole _spinnerRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _spinnerTheme = ThemeColorShade.Primary_500;
        private Color _spinnerColor = Color.FromArgb(0, 128, 200);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole SpinnerRole { get => _spinnerRole; set { _spinnerRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade SpinnerTheme
        {
            get => _spinnerTheme; set
            {
                _spinnerTheme = value;
                if (!_ignoreTheme) { SpinnerColor = ThemeManager.Instance.GetThemeColorShade(_spinnerTheme); }
            }
        }
        [Category("Misc")]
        [Description("The color of the spinner.")]
        public Color SpinnerColor { get => _spinnerColor; set { _spinnerColor = value; Invalidate(); } }
        #endregion
        

        private bool _timerEnabled = true;
        [Category("Misc")]
        [Description("Toggle the timer.")]
        public bool TimerEnabled
        {
            get => _timerEnabled;
            set
            {
                _timerEnabled = value;
                if (!_timerEnabled) {_timer.Enabled = false;}
                else { _timer.Enabled = true;}
                Invalidate();
            }
        }




        private float _thickness = 4f;
        [Category("Misc")]
        [Description("The thickness of the spinner arc.")]
        public float Thickness
        {
            get => _thickness;
            set
            {
                _thickness = Math.Max(1, value); // Ensure the thickness is at least 1
                Invalidate();
            }
        }

        private int _rotationSpeed = 4;
        [Category("Misc")]
        [Description("The speed of the spinner rotation. Higher values are faster.")]
        public int RotationSpeed
        {
            get => _rotationSpeed;
            set
            {
                _rotationSpeed = Math.Max(1, value); // Minimum speed of 1
                Invalidate();
            }
        }

        private bool _showFullCircle = true;
        [Category("Misc")]
        [Description("Determines if the full circle is visible behind the spinner.")]
        public bool ShowFullCircle
        {
            get => _showFullCircle;
            set
            {
                _showFullCircle = value;
                Invalidate();
            }
        }

        private float _swoopIntensity = 2f;
        [Category("Misc")]
        [Description("Controls the intensity of the swooping effect.")]
        public float SwoopIntensity
        {
            get => _swoopIntensity;
            set
            {
                _swoopIntensity = Math.Max(0, value);
                Invalidate();
            }
        }

        private float _sweepAngle = 90f; // Fixed sweep angle for the spinner
        [Category("Misc")]
        [Description("Set range of spinner.")]
        public float SweepAngle
        {
            get => _sweepAngle;
            set
            {
                _sweepAngle = Math.Max(1, value);
                Invalidate();
            }
        }

        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                BackgroundCircleColor = ThemeManager.Instance.GetThemeColorShade(BackgroundCircleTheme);
                SpinnerColor = ThemeManager.Instance.GetThemeColorShade(SpinnerTheme);
            }
        }



        public ErrataProgressSpinner()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;

            _timer = new Timer { Interval = 15 }; // Timer for smooth animation
            _timer.Tick += (s, e) =>
            {
                UpdateAngle();
                Invalidate();
            };
            _timer.Start();
        }

        private void UpdateAngle()
        {
            const float baseSpeed = 2f; // Base speed of rotation

            // Use a sinusoidal function to modulate speed
            float modulation = (float)(1 + Math.Sin(_currentAngle * Math.PI / 180) * _swoopIntensity);

            // Update the angle with modulation applied
            _currentAngle += _rotationSpeed * modulation + 1;

            // Wrap angle within [0, 360)
            if (_currentAngle >= 360f)
            {
                _currentAngle -= 360f;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            float centerX = Width / 2f;
            float centerY = Height / 2f;
            float radius = Math.Min(Width, Height) / 2f - _thickness;

            // Define the spinner bounds
            RectangleF spinnerBounds = new RectangleF(
                centerX - radius,
                centerY - radius,
                2 * radius,
                2 * radius
            );

            // Draw the full circle if enabled
            if (_showFullCircle)
            {
                using (var backgroundPen = new Pen(_backgroundCircleColor, _thickness))
                {
                    e.Graphics.DrawArc(backgroundPen, spinnerBounds, 0, 360);
                }
            }

            // Draw the spinner arc
            using (var spinnerPen = new Pen(_spinnerColor, _thickness))
            {
                spinnerPen.StartCap = LineCap.Round;
                spinnerPen.EndCap = LineCap.Round;

                // Draw the spinner arc with a fixed sweep angle
                e.Graphics.DrawArc(spinnerPen, spinnerBounds, _currentAngle, SweepAngle);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
