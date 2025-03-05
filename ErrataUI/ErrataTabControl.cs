using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataTabControl : TabControl
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


        private int _flatSpace = 2;
        private int _hoveredTabIndex = -1;  // Index of the tab under the mouse


        [Category("Misc")]
        [Description("Adjusts the tab page and frame color spacing.")]
        public int FlatSpace
        {
            get => _flatSpace;
            set
            {
                _flatSpace = value;
                Invalidate();
            }
        }


        private int _tabUnderlineYOffset = 3;
        [Category("Misc")]
        public int TabUnderlineYOffset
        {
            get => _tabUnderlineYOffset;
            set
            {
                _tabUnderlineYOffset = value;
                Invalidate();
            }
        }

        private int _tabUnderlineWeight = 2;
        [Category("Misc")]
        public int TabUnderlineWeight
        {
            get => _tabUnderlineWeight;
            set
            {
                _tabUnderlineWeight = value;
                Invalidate();
            }
        }

        #region TABUNDERLINE
        //TABUNDERLINE COLOR
        private UIRole _tabUnderlineRole = UIRole.EmphasizedBorders;
        private ThemeColorShade _tabUnderlineTheme = ThemeColorShade.Primary_500;
        private Color _tabUnderlineColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole TabUnderlineRole { get => _tabUnderlineRole; set { _tabUnderlineRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade TabUnderlineTheme
        {
            get => _tabUnderlineTheme; set
            {
                _tabUnderlineTheme = value;
                if (!_ignoreTheme) { TabUnderlineColor = ThemeManager.Instance.GetThemeColorShade(_tabUnderlineTheme); }
            }
        }
        [Category("Misc")]
        [Description("Sets the color of the line under the active tab.")]
        public Color TabUnderlineColor { get => _tabUnderlineColor; set { _tabUnderlineColor = value; Invalidate(); } }
        #endregion

        #region CONTROLBACK
        //CONTROLBACK COLOR
        private UIRole _controlBackRole = UIRole.MainBackground;
        private ThemeColorShade _controlBackTheme = ThemeColorShade.Neutral_100;
        private Color _controlBackColor = Color.FromArgb(250, 250, 250);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole ControlBackRole { get => _controlBackRole; set { _controlBackRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade ControlBackTheme
        {
            get => _controlBackTheme; set
            {
                _controlBackTheme = value;
                if (!_ignoreTheme) { ControlBackColor = ThemeManager.Instance.GetThemeColorShade(_controlBackTheme); }
            }
        }
        [Category("Misc")]
        [Description("Background color of the entire tab control.")]
        public Color ControlBackColor { get => _controlBackColor; set { _controlBackColor = value; Invalidate(); } }
        #endregion

        #region HEADERBACK
        //HEADERBACK COLOR
        private UIRole _headerBackRole = UIRole.MainBackground;
        private ThemeColorShade _headerBackTheme = ThemeColorShade.Neutral_100;
        private Color _headerBackColor = Color.FromArgb(250, 250, 250);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole HeaderBackRole { get => _headerBackRole; set { _headerBackRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade HeaderBackTheme
        {
            get => _headerBackTheme; set
            {
                _headerBackTheme = value;
                if (!_ignoreTheme) { HeaderBackColor = ThemeManager.Instance.GetThemeColorShade(_headerBackTheme); }
            }
        }
        [Category("Misc")]
        [Description("Background color of the tab header.")]
        public Color HeaderBackColor { get => _headerBackColor; set { _headerBackColor = value; Invalidate(); } }
        #endregion

        #region FRAMEBACK
        //FRAMEBACK COLOR
        private UIRole _frameBackRole = UIRole.MainBackground;
        private ThemeColorShade _frameBackTheme = ThemeColorShade.Neutral_100;
        private Color _frameBackColor = Color.FromArgb(250, 250, 250);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole FrameBackRole { get => _frameBackRole; set { _frameBackRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade FrameBackTheme
        {
            get => _frameBackTheme; set
            {
                _frameBackTheme = value;
                if (!_ignoreTheme) { FrameBackColor = ThemeManager.Instance.GetThemeColorShade(_frameBackTheme); }
            }
        }
        [Category("Misc")]
        [Description("Background color of the primary frame.")]
        public Color FrameBackColor { get => _frameBackColor; set { _frameBackColor = value; Invalidate(); } }
        #endregion

        #region HOVERTEXT
        //HOVERTEXT COLOR
        private UIRole _hoverTextRole = UIRole.HeadingText;
        private ThemeColorShade _hoverTextTheme = ThemeColorShade.Primary_500;
        private Color _hoverTextColor = Color.FromArgb(0, 128, 200);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole HoverTextRole { get => _hoverTextRole; set { _hoverTextRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade HoverTextTheme
        {
            get => _hoverTextTheme; set
            {
                _hoverTextTheme = value;
                if (!_ignoreTheme) { HoverTextColor = ThemeManager.Instance.GetThemeColorShade(_hoverTextTheme); }
            }
        }
        [Category("Misc")]
        [Description("Tab text color during mouse hover.")]
        public Color HoverTextColor { get => _hoverTextColor; set { _hoverTextColor = value; Invalidate(); } }

        #endregion

        #region SELECTEDTABTEXT
        //SELECTEDTABTEXT COLOR
        private UIRole _selectedTabTextRole = UIRole.BodyTextL3;
        private ThemeColorShade _selectedTabTextTheme = ThemeColorShade.Neutral_700;
        private Color _selectedTabTextColor = Color.FromArgb(50, 49, 48);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole SelectedTabTextRole { get => _selectedTabTextRole; set { _selectedTabTextRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade SelectedTabTextTheme
        {
            get => _selectedTabTextTheme; set
            {
                _selectedTabTextTheme = value;
                if (!_ignoreTheme) { SelectedTabTextColor = ThemeManager.Instance.GetThemeColorShade(_selectedTabTextTheme); }
            }
        }
        [Category("Misc")]
        [Description("Text color of the selected tab.")]
        public Color SelectedTabTextColor { get => _selectedTabTextColor; set { _selectedTabTextColor = value; Invalidate(); } }
        #endregion

        #region UNSELECTEDTABTEXT
        //UNSELECTEDTABTEXT COLOR
        private UIRole _unselectedTabTextRole = UIRole.DisabledText;
        private ThemeColorShade _unselectedTabTextTheme = ThemeColorShade.Neutral_600;
        private Color _unselectedTabTextColor = Color.FromArgb(128, 128, 128);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole UnselectedTabTextRole { get => _unselectedTabTextRole; set { _unselectedTabTextRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade UnselectedTabTextTheme
        {
            get => _unselectedTabTextTheme; set
            {
                _unselectedTabTextTheme = value;
                if (!_ignoreTheme) { UnselectedTabTextColor = ThemeManager.Instance.GetThemeColorShade(_unselectedTabTextTheme); }
            }
        }
        [Category("Misc")]
        [Description("Text color of unselected tabs.")]
        public Color UnselectedTabTextColor { get => _unselectedTabTextColor; set { _unselectedTabTextColor = value; Invalidate(); } }
        #endregion

        #region SELECTEDTABCOLOR
        //SELECTEDTAB COLOR
        private UIRole _selectedTabRole = UIRole.TabBar;
        private ThemeColorShade _selectedTabTheme = ThemeColorShade.Neutral_100;
        private Color _selectedTabColor = Color.FromArgb(250, 250, 250);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole SelectedTabRole { get => _selectedTabRole; set { _selectedTabRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade SelectedTabTheme
        {
            get => _selectedTabTheme; set
            {
                _selectedTabTheme = value;
                if (!_ignoreTheme) { SelectedTabColor = ThemeManager.Instance.GetThemeColorShade(_selectedTabTheme); }
            }
        }
        [Category("Misc")]
        [Description("Background color of the selected tab.")]
        public Color SelectedTabColor { get => _selectedTabColor; set { _selectedTabColor = value; Invalidate(); } }
        #endregion

        #region UNSELECTEDTABCOLOR
        //UNSELECTEDTAB COLOR
        private UIRole _unselectedTabRole = UIRole.TabBar;
        private ThemeColorShade _unselectedTabTheme = ThemeColorShade.Neutral_100;
        private Color _unselectedTabColor = Color.FromArgb(250, 250, 250);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole UnselectedTabRole { get => _unselectedTabRole; set { _unselectedTabRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade UnselectedTabTheme
        {
            get => _unselectedTabTheme; set
            {
                _unselectedTabTheme = value;
                if (!_ignoreTheme) { UnselectedTabColor = ThemeManager.Instance.GetThemeColorShade(_unselectedTabTheme); }
            }
        }
        [Category("Misc")]
        [Description("Background color of unselected tabs.")]
        public Color UnselectedTabColor { get => _unselectedTabColor; set { _unselectedTabColor = value; Invalidate(); } }

        #endregion

        #region BORDERCOLOR
        //BORDER COLOR
        private UIRole _borderRole = UIRole.GeneralBorders;
        private ThemeColorShade _borderTheme = ThemeColorShade.Neutral_600;
        private Color _borderColor = Color.FromArgb(0, 128, 128, 128);
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
        [Description("Border color of the tab control.")]
        public Color BorderColor { get => _borderColor; set { _borderColor = value; Invalidate(); } }
        #endregion



        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                if (ThemeManager.Instance.IsDarkMode)
                {
                    UnselectedTabColor = ThemeManager.Instance.GetThemeColorShadeOffset(UnselectedTabTheme);
                    SelectedTabColor = ThemeManager.Instance.GetThemeColorShadeOffset(SelectedTabTheme);
                    UnselectedTabTextColor = ThemeManager.Instance.GetThemeColorShadeOffset(UnselectedTabTextTheme);
                    SelectedTabTextColor = ThemeManager.Instance.GetThemeColorShadeOffset(SelectedTabTextTheme);
                    HoverTextColor = ThemeManager.Instance.GetThemeColorShadeOffset(HoverTextTheme);
                    FrameBackColor = ThemeManager.Instance.GetThemeColorShadeOffset(FrameBackTheme);
                    HeaderBackColor = ThemeManager.Instance.GetThemeColorShadeOffset(HeaderBackTheme);
                    ControlBackColor = ThemeManager.Instance.GetThemeColorShadeOffset(ControlBackTheme);
                    TabUnderlineColor = ThemeManager.Instance.GetThemeColorShadeOffset(TabUnderlineTheme, -2);
                    ApplyTabPageBackColor();
                    return;
                }

                //BorderColor = ThemeManager.Instance.GetThemeColorShade(BorderTheme);
                UnselectedTabColor = ThemeManager.Instance.GetThemeColorShade(UnselectedTabTheme);
                SelectedTabColor = ThemeManager.Instance.GetThemeColorShade(SelectedTabTheme);
                UnselectedTabTextColor = ThemeManager.Instance.GetThemeColorShade(UnselectedTabTextTheme);
                SelectedTabTextColor = ThemeManager.Instance.GetThemeColorShade(SelectedTabTextTheme);
                HoverTextColor = ThemeManager.Instance.GetThemeColorShade(HoverTextTheme);
                FrameBackColor = ThemeManager.Instance.GetThemeColorShade(FrameBackTheme);
                HeaderBackColor = ThemeManager.Instance.GetThemeColorShade(HeaderBackTheme);
                ControlBackColor = ThemeManager.Instance.GetThemeColorShade(ControlBackTheme);
                TabUnderlineColor = ThemeManager.Instance.GetThemeColorShade(TabUnderlineTheme);
                ApplyTabPageBackColor();

                

            }


        }



        public ErrataTabControl()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            DrawMode = TabDrawMode.OwnerDrawFixed;
            Alignment = TabAlignment.Top;
            SizeMode = TabSizeMode.Fixed;
            ItemSize = new Size(100, 25); // Default tab size
            this.MouseMove += ErrataTabControl2_MouseMove; // Subscribe to MouseMove event
            this.MouseLeave += ErrataTabControl2_MouseLeave; // Subscribe to MouseLeave event
            FlatSpace = 0;
            ApplyTabPageBackColor();
        }

        private void ApplyTabPageBackColor()
        {
            foreach (TabPage page in TabPages)
            {
                page.BackColor = ThemeManager.Instance.GetThemeColorShade(ControlBackTheme);
            }
        }

        // Event handler to track mouse movement over the tabs
        private void ErrataTabControl2_MouseMove(object sender, MouseEventArgs e)
        {
            int hoveredTabIndex = -1;

            for (int i = 0; i < TabCount; i++)
            {
                Rectangle tabRect = GetTabRect(i);
                if (tabRect.Contains(e.Location))
                {
                    hoveredTabIndex = i;
                    break;
                }
            }
            // Update hovered tab index if it has changed
            if (_hoveredTabIndex != hoveredTabIndex)
            {
                _hoveredTabIndex = hoveredTabIndex;
                //Invalidate();  // Request a repaint of the control
            }
        }

        // Event handler for when the mouse leaves the tab control
        private void ErrataTabControl2_MouseLeave(object sender, EventArgs e)
        {
            _hoveredTabIndex = -1;
            Invalidate();  // Request a repaint when mouse leaves
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            // Draw the tab headers
            using (Graphics g = e.Graphics)
            {
                g.Clear(_controlBackColor);

                for (int i = 0; i < TabCount; i++)
                {
                    Rectangle tabRect = GetTabRect(i);
                    bool isSelected = (SelectedIndex == i);

                    // Background color for the tab
                    Color backColor = isSelected ? _selectedTabColor : _unselectedTabColor;
                    using (Brush backBrush = new SolidBrush(backColor))
                    {
                        g.FillRectangle(backBrush, tabRect);
                    }

                    // Foreground text color for the tab
                    string tabText = TabPages[i].Text;
                    Color textColor = isSelected ? _selectedTabTextColor : _unselectedTabTextColor;

                    // Check if the mouse is hovering over this tab
                    if (_hoveredTabIndex == i)
                    {
                        textColor = _hoverTextColor;  // Use the highlight color when hovered
                    }
                    else
                    {
                        textColor = isSelected ? _selectedTabTextColor : _unselectedTabTextColor;
                    }

                    // Create the Segoe UI font with SemiBold for the selected tab
                    Font tabFont = isSelected
                        ? new Font("Segoe UI Semibold", Font.Size, FontStyle.Regular) // SemiBold effect with Bold style
                        : new Font("Segoe UI", Font.Size, FontStyle.Regular);

                    // Draw the text with the appropriate font
                    using (Brush textBrush = new SolidBrush(textColor))
                    using (StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    })
                    {
                        g.DrawString(tabText, tabFont, textBrush, tabRect, sf);
                    }

                    // Draw a blue line under the selected tab (Fluent UI style)
                    if (isSelected)
                    {
                        // Adjust the line's Y position just below the tab text area
                        int lineY = tabRect.Bottom - _tabUnderlineYOffset;
                        using (Pen linePen = new Pen(_tabUnderlineColor, _tabUnderlineWeight))  // Blue line with a thickness of 2
                        {
                            g.DrawLine(linePen, tabRect.Left, lineY, tabRect.Right, lineY);
                        }
                    }
                }

                // Draw the border below the tabs
                using (Pen borderPen = new Pen(_borderColor))
                {
                    g.DrawLine(borderPen, new Point(0, ItemSize.Height), new Point(Width, ItemSize.Height));
                }

                if (SelectedTab != null)
                {
                    // Fill the content area of the selected tab with the background color
                    Rectangle tabContentBounds = new Rectangle(_flatSpace, ItemSize.Height, Width - (2 * _flatSpace), Height - ItemSize.Height);
                    g.FillRectangle(new SolidBrush(_frameBackColor), tabContentBounds);

                }
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            // Prevent default rendering to achieve a fully flat look
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();

            // Remove default tab 3D border
            SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Prevent default background painting
        }
    }
}
