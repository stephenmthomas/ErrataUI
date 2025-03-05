using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using System.Collections.Generic;
using System.Linq;
using static ErrataUI.ThemeManager;



namespace ErrataUI
{



    public class ErrataDrawer : Panel
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


        
        private int drawerWidthExpanded = 250; // Width when expanded
        private int drawerWidthCollapsed = 50; // Width when collapsed
        private bool isExpanded = true;



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

        private float _gradientAngle = 90F;
        [Category("Misc")]
        public float GradientAngle
        {
            get => _gradientAngle;
            set
            {
                _gradientAngle = value;
                Invalidate();
            }
        }


        private bool _activeSurface = true;
        [Category("Misc"), Description("If true, clicking the drawer anywhere will toggle it open or closed.")]
        public bool ActiveSurface
        {
            get => _activeSurface;
            set
            {
                _activeSurface = value;
            }
        }


        [Category("Misc"), Description("Width of the drawer when expanded.")]
        public int ExpandedWidth
        {
            get => drawerWidthExpanded;
            set
            {
                drawerWidthExpanded = value;
                if (isExpanded)
                    this.Width = value;
            }
        }

        [Category("Misc"), Description("Width of the drawer when collapsed.")]
        public int CollapsedWidth
        {
            get => drawerWidthCollapsed;
            set
            {
                drawerWidthCollapsed = value;
                if (!isExpanded)
                    this.Width = value;
            }
        }

        [Category("Misc"), Description("Determines whether the drawer is expanded.")]
        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                if (value != isExpanded)
                    Toggle();
            }
        }


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


        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                BackColor = ThemeManager.Instance.GetThemeColorShade(BackColorTheme);
                GradientColor = ThemeManager.Instance.GetThemeColorShade(GradientColorTheme);
            }
        }


        public ErrataDrawer()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            // Default properties
            this.Width = drawerWidthExpanded;
            this.BackColor = Color.FromArgb(45, 45, 48); // Dark gray
            this.Dock = DockStyle.Left;
            this.DoubleBuffered = true;

            // Initialize animation timer
            animationTimer = new Timer();
            animationTimer.Interval = AnimationSpeed;
            animationTimer.Tick += AnimationTimer_Tick;

            // Add an example label
            //var label = new Label
            //{
            //    Text = "",
            //    ForeColor = Color.White,
            //    BackColor = Color.Transparent,
            //    Dock = DockStyle.Top,
            //    Font = new Font("Segoe MDL2 Assets", 15, FontStyle.Bold),
            //    TextAlign = ContentAlignment.MiddleLeft
            //};
            //this.Controls.Add(label);
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            int targetWidth = isExpanded ? drawerWidthExpanded : drawerWidthCollapsed;
            if (this.Width == targetWidth)
            {
                animationTimer.Stop();
                Invalidate();
                return;
            }

            int step = isExpanded ? AnimationStep : -AnimationStep;
            this.Width = Math.Max(drawerWidthCollapsed, Math.Min(drawerWidthExpanded, this.Width + step));
        }

        public void Toggle()
        {
            this.BringToFront();
            isExpanded = !isExpanded;
            animationTimer.Start();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Left)
            {
                if (ActiveSurface) { Toggle(); }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Gradient == true)
            {
                using (var shadowBrush = new LinearGradientBrush(new Rectangle(0, 0, this.Width, this.Height),
                                BackColor, GradientColor, GradientAngle))
                {
                    e.Graphics.FillRectangle(shadowBrush, this.ClientRectangle);
                }
            }
            else
            {
                using (var flatBrush = new SolidBrush(BackColor))
                {
                    e.Graphics.FillRectangle(flatBrush, this.ClientRectangle);
                }
            }
            
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.Parent != null)
            {
                // Set the top position to 0 to override form's padding
                this.Top = 0;

                // If you want to ensure it is not affected by form padding
                this.Left = this.Parent.ClientRectangle.Left;
            }
        }
    }
}