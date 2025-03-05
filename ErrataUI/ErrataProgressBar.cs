using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataProgressBar : Control
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


        private int _progress = 0;
        [Category("Misc")]
        [Description("The current progress value (0-100).")]
        public int Progress
        {
            get => _progress;
            set
            {
                _progress = Math.Max(0, Math.Min(100, value)); // Clamp between 0 and 100
                Invalidate(); // Trigger a repaint
            }
        }

        private bool _isVertical = false;
        [Category("Misc")]
        [Description("Set the orientation of the progress bar.")]
        public bool isVertical
        {
            get => _isVertical;
            set
            {
                _isVertical = value;
                if (_isVertical) { this.Size = new Size(Height, Width); }
                if (!_isVertical) { this.Size = new Size(Height, Width); }
                Invalidate();
            }

        }


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
        [Description("The color of the progress bar.")]
        public Color ProgressColor { get => _progressColor; set { _progressColor = value; Invalidate(); } }
        #endregion
        #region BACKGROUND
        //BACKGROUND COLOR
        private UIRole _backgroundRole = UIRole.SecondaryBackground;
        private ThemeColorShade _backgroundTheme = ThemeColorShade.Neutral_200;
        private Color _backgroundColor = Color.FromArgb(220, 220, 220);
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
        [Description("The background color of the progress bar.")]
        public Color BackgroundColor { get => _backgroundColor; set { _backgroundColor = value; Invalidate(); } }
        #endregion

        

        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                BackgroundColor = ThemeManager.Instance.GetThemeColorShade(BackgroundTheme);
                ProgressColor = ThemeManager.Instance.GetThemeColorShade(ProgressTheme);
            }
        }

        public ErrataProgressBar()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            DoubleBuffered = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Size = new Size(100, 2);
            AntiAlias = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            Graphics g = e.Graphics;

            if (AntiAlias)
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
            }
            else
            {
                g.SmoothingMode = SmoothingMode.Default;
            }

            // Calculate bounds
            var rect = ClientRectangle;

            // Draw background
            using (Brush backgroundBrush = new SolidBrush(BackgroundColor))
            {
                g.FillRectangle(backgroundBrush, rect);
            }

            // Draw progress
            float progressWidth = _isVertical
                ? rect.Height * (_progress / 100f)
                : rect.Width * (_progress / 100f);

            RectangleF progressRect = _isVertical
                ? new RectangleF(0, rect.Height - progressWidth, rect.Width, progressWidth)
                : new RectangleF(0, 0, progressWidth, rect.Height);

            using (Brush progressBrush = new SolidBrush(ProgressColor))
            {
                g.FillRectangle(progressBrush, progressRect);
            }

            // Draw border (optional)
            //using (Pen borderPen = new Pen(ForeColor, 1))
            //{
            //    g.DrawRectangle(borderPen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            //}
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}
