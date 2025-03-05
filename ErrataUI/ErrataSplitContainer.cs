using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ErrataUI;
using static ErrataUI.ThemeManager;
using Timer = System.Windows.Forms.Timer;

namespace ErrataUI
{
    public class ErrataSplitContainer : SplitContainer
    {
        private bool _ignoreRoles = true;
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



        private Timer animationTimer;
        private int _animationStep = 15;
        [Category("Misc"), Description("Pixels per animation tick.")]
        public int AnimationStep
        {
            get => _animationStep;
            set
            {
                _animationStep = value;
            }
        }

        private int _animationSpeed = 5;
        [Category("Misc"), Description("General animation speed. Lower = faster.")]
        public int AnimationSpeed
        {
            get => _animationSpeed;
            set
            {
                _animationSpeed = value;
                animationTimer.Interval = value;
            }
        }

        private bool _positiveTravel = true;
        [Category("Misc")]
        public bool PositiveTravel
        {
            get => _positiveTravel;
            set
            {
                _positiveTravel = value;
            }
        }

        private int _maxDistance = 100;
        [Category("Misc")]
        public int MaxDistance
        {
            get => _maxDistance;
            set
            {
                _maxDistance = value;
            }
        }

        private int _splitterThickness = 2;
        [Category("Misc"), Description("Thickness of the splitter.")]
        public int SplitterThickness
        {
            get => _splitterThickness;
            set
            {
                if (value > 0)
                {
                    _splitterThickness = value;
                    SplitterWidth = value; // Update the actual splitter width
                    Invalidate();
                }
            }
        }

        private bool _running = true;
        [Category("Misc")]
        public bool Running
        {
            get => _running;
            set
            {
                _running = value;
                if (_running) { animationTimer.Enabled = true; }
            }
        }


        #region SPLITTERCOLOR
        //SPLITTERCOLOR
        private UIRole _splitterColorRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _splitterColorTheme = ThemeColorShade.Primary_500;
        private Color _splitterColor = Color.FromArgb(128, 128, 128);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole SplitterColorRole { get => _splitterColorRole; set { _splitterColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade SplitterColorTheme
        {
            get => _splitterColorTheme; set
            {
                _splitterColorTheme = value;
                if (!_ignoreTheme) { SplitterColor = ThemeManager.Instance.GetThemeColorShade(_splitterColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color SplitterColor { get => _splitterColor; set { _splitterColor = value; Invalidate(); } }
        #endregion




        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                SplitterColor = ThemeManager.Instance.GetThemeColorShade(SplitterColorTheme);
            }
        }

        public ErrataSplitContainer()
        {
            // Initialize the default splitter width
            SplitterWidth = _splitterThickness;



            // Remove padding and spacing
            Panel1.Margin = new Padding(0);
            Panel1.Padding = new Padding(0);
            Panel2.Margin = new Padding(0);
            Panel2.Padding = new Padding(0);

            // Set minimum size to zero to allow panels to be fully collapsed if needed
            Panel1MinSize = 0;
            Panel2MinSize = 0;

            animationTimer = new Timer();
            animationTimer.Interval = AnimationSpeed;
            animationTimer.Tick += AnimationTimer_Tick;

        }
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            int step = PositiveTravel ? 1 : -1;

            SplitterDistance += step;
            
            if (SplitterDistance > _maxDistance)
            {
                SplitterDistance -= 5;
                PositiveTravel = false;
                //animationTimer.Stop();
                
            }

            Invalidate();

        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Get the bounds of the splitter
            Rectangle splitterRect = new Rectangle(
                SplitterDistance, 0,
                Orientation == Orientation.Vertical ? SplitterThickness : Width,
                Orientation == Orientation.Horizontal ? SplitterThickness : Height
            );

            // Fill the splitter area with the custom color
            using (Brush brush = new SolidBrush(_splitterColor))
            {
                e.Graphics.FillRectangle(brush, splitterRect);
            }

            // Optionally, add a border or additional styles
            using (Pen pen = new Pen(Color.DarkGray))
            {
                if (Orientation == Orientation.Vertical)
                {
                    e.Graphics.DrawLine(pen, splitterRect.Left, splitterRect.Top, splitterRect.Left, splitterRect.Bottom);
                    e.Graphics.DrawLine(pen, splitterRect.Right - 1, splitterRect.Top, splitterRect.Right - 1, splitterRect.Bottom);
                }
                else
                {
                    e.Graphics.DrawLine(pen, splitterRect.Left, splitterRect.Top, splitterRect.Right, splitterRect.Top);
                    e.Graphics.DrawLine(pen, splitterRect.Left, splitterRect.Bottom - 1, splitterRect.Right, splitterRect.Bottom - 1);
                }
            }
        }


    }
}
