#region Imports


using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;

#endregion

namespace ErrataUI
{
    

    public class ErrataFlatMenuStrip : MenuStrip
    {
        private bool _ignoreRoles;
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

        #region BACKCOLOR
        //BACKCOLOR
        private UIRole _backColorRole = UIRole.MenuBar;
        private ThemeColorShade _backColorTheme = ThemeColorShade.Primary_500;
        private Color _backColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);

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
        public Color BackColor { get => _backColor; set { _backColor = value; RefreshUI(); Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion

        #region SELECTEDBACKCOLOR
        //SELECTEDBACKCOLOR
        private UIRole _selectedBackColorRole = UIRole.MenuBar;
        private ThemeColorShade _selectedBackColorTheme = ThemeColorShade.Primary_700;
        private Color _selectedBackColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_700);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole SelectedBackColorRole { get => _selectedBackColorRole; set { _selectedBackColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade SelectedBackColorTheme
        {
            get => _selectedBackColorTheme; set
            {
                _selectedBackColorTheme = value;
                if (!_ignoreTheme) { SelectedBackColor = ThemeManager.Instance.GetThemeColorShade(_selectedBackColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color SelectedBackColor { get => _selectedBackColor; set { _selectedBackColor = value; RefreshUI(); Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion

        #region HOVERBACKCOLOR
        //HOVERBACKCOLOR
        private UIRole _hoverBackColorRole = UIRole.MenuBar;
        private ThemeColorShade _hoverBackColorTheme = ThemeColorShade.Primary_300;
        private Color _hoverBackColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_300);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole HoverBackColorRole { get => _hoverBackColorRole; set { _hoverBackColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade HoverBackColorTheme
        {
            get => _hoverBackColorTheme; set
            {
                _hoverBackColorTheme = value;
                if (!_ignoreTheme) { HoverBackColor = ThemeManager.Instance.GetThemeColorShade(_hoverBackColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color HoverBackColor { get => _hoverBackColor; set { _hoverBackColor = value; RefreshUI(); Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion

        #region TEXTCOLOR
        //TEXTCOLOR
        private UIRole _textColorRole = UIRole.TitleBarText;
        private ThemeColorShade _textColorTheme = ThemeColorShade.Neutral_700;
        private Color _textColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_700);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole TextColorRole { get => _textColorRole; set { _textColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade TextColorTheme
        {
            get => _textColorTheme; set
            {
                _textColorTheme = value;
                if (!_ignoreTheme) { TextColor = ThemeManager.Instance.GetThemeColorShade(_textColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color TextColor { get => _textColor; set { _textColor = value; RefreshUI(); Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion

        #region HOVERTEXTCOLOR
        //HOVERTEXTCOLOR
        private UIRole _hoverTextColorRole = UIRole.TitleBarText;
        private ThemeColorShade _hoverTextColorTheme = ThemeColorShade.Neutral_500;
        private Color _hoverTextColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole HoverTextColorRole { get => _hoverTextColorRole; set { _hoverTextColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade HoverTextColorTheme
        {
            get => _hoverTextColorTheme; set
            {
                _hoverTextColorTheme = value;
                if (!_ignoreTheme) { HoverTextColor = ThemeManager.Instance.GetThemeColorShade(_hoverTextColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color HoverTextColor { get => _hoverTextColor; set { _hoverTextColor = value; RefreshUI(); Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion

        #region SELECTEDTEXTCOLOR
        //SELECTEDTEXTCOLOR
        private UIRole _selectedTextColorRole = UIRole.TitleBarText;
        private ThemeColorShade _selectedTextColorTheme = ThemeColorShade.Neutral_800;
        private Color _selectedTextColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_800);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole SelectedTextColorRole { get => _selectedTextColorRole; set { _selectedTextColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade SelectedTextColorTheme
        {
            get => _selectedTextColorTheme; set
            {
                _selectedTextColorTheme = value;
                if (!_ignoreTheme) { SelectedTextColor = ThemeManager.Instance.GetThemeColorShade(_selectedTextColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color SelectedTextColor { get => _selectedTextColor; set { _selectedTextColor = value; RefreshUI(); Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion

        #region SEPARATORCOLOR
        //SEPARATORCOLOR
        private UIRole _separatorColorRole = UIRole.EmphasizedBorders;
        private ThemeColorShade _separatorColorTheme = ThemeColorShade.Neutral_100;
        private Color _separatorColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_100);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole SeparatorColorRole { get => _separatorColorRole; set { _separatorColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade SeparatorColorTheme
        {
            get => _separatorColorTheme; set
            {
                _separatorColorTheme = value;
                if (!_ignoreTheme) { SeparatorColor = ThemeManager.Instance.GetThemeColorShade(_separatorColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color SeparatorColor { get => _separatorColor; set { _separatorColor = value;  Invalidate(); RefreshUI(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion

        #region HEADERCOLOR
        //HEADERCOLOR
        private UIRole _headerColorRole = UIRole.MenuBar;
        private ThemeColorShade _headerColorTheme = ThemeColorShade.Primary_500;
        private Color _headerColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole HeaderColorRole { get => _headerColorRole; set { _headerColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade HeaderColorTheme
        {
            get => _headerColorTheme; set
            {
                _headerColorTheme = value;
                if (!_ignoreTheme) { HeaderColor = ThemeManager.Instance.GetThemeColorShade(_headerColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color HeaderColor { get => _headerColor; set { _headerColor = value; Invalidate(); RefreshUI(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion

        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                BackColor = ThemeManager.Instance.GetThemeColorShadeOffset(BackColorTheme);
                SelectedBackColor = ThemeManager.Instance.GetThemeColorShadeOffset(SelectedBackColorTheme);
                HoverBackColor = ThemeManager.Instance.GetThemeColorShadeOffset(HoverBackColorTheme);
                TextColor = ThemeManager.Instance.GetThemeColorShadeOffset(TextColorTheme);
                HoverTextColor = ThemeManager.Instance.GetThemeColorShadeOffset(HoverTextColorTheme);
                SelectedTextColor = ThemeManager.Instance.GetThemeColorShadeOffset(SelectedTextColorTheme);
                SeparatorColor = ThemeManager.Instance.GetThemeColorShadeOffset(SeparatorColorTheme);
                HeaderColor = ThemeManager.Instance.GetThemeColorShadeOffset(HeaderColorTheme);
            }
        }

        

        private void RefreshUI()
        {
            base.Renderer = new ErrataMenuStripRenderer(HeaderColor, BackColor, SelectedBackColor, HoverBackColor, TextColor, HoverTextColor, SelectedTextColor, SeparatorColor);
        }


        public ErrataFlatMenuStrip()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            base.Renderer = new ErrataMenuStripRenderer(HeaderColor, BackColor, SelectedBackColor, HoverBackColor, TextColor, HoverTextColor, SelectedTextColor, SeparatorColor);
            
        }

    }



    public class ErrataMenuStripRenderer : System.Windows.Forms.ToolStripRenderer
    {
        public ErrataMenuStripRenderer(Color hc, Color bc, Color sbc, Color hbc, Color tc, Color htc, Color stc, Color sc)
        {
            headerColor = hc;
            backColor = bc;
            selectedBackColor = sbc;
            hoverBackColor = hbc;
            textColor = tc;
            hoverTextColor = htc;
            selectedTextColor = stc;
            separatorColor = sc;
        }

        // Set the menu background color
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);
            using (SolidBrush brush = new(headerColor))
            {
                e.Graphics.FillRectangle(brush, e.AffectedBounds);
            }
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderMenuItemBackground(e);
            if (e.Item.Enabled)
            {
                if (!e.Item.IsOnDropDown && e.Item.Selected)
                {
                    Rectangle rect = new(0, 0, e.Item.Width, e.Item.Height);
                    e.Graphics.FillRectangle(new SolidBrush(hoverBackColor), rect);
                    e.Item.ForeColor = hoverTextColor;
                }
                else
                {
                    e.Item.ForeColor = textColor;
                }
                if (e.Item.IsOnDropDown && e.Item.Selected)
                {
                    Rectangle rect2 = new(1, -4, e.Item.Width + 5, e.Item.Height + 4);
                    e.Graphics.FillRectangle(new SolidBrush(hoverBackColor), rect2);
                    e.Item.ForeColor = textColor;
                }
                if ((e.Item as ToolStripMenuItem).DropDown.Visible && !e.Item.IsOnDropDown)
                {
                    Rectangle rect3 = new(0, 0, e.Item.Width + 3, e.Item.Height);
                    e.Graphics.FillRectangle(new SolidBrush(selectedBackColor), rect3);
                    e.Item.ForeColor = selectedTextColor;
                }
            }
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            base.OnRenderSeparator(e);
            SolidBrush brush = new(separatorColor);
            Rectangle rect = new(1, 3, e.Item.Width, 1);
            e.Graphics.FillRectangle(brush, rect);
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemCheck(e);
            if (e.Item.Selected)
            {
                Rectangle rect = new(4, 2, 18, 18);
                Rectangle rect2 = new(5, 3, 16, 16);
                SolidBrush brush = new(selectedTextColor);
                SolidBrush brush2 = new(selectedBackColor);
                e.Graphics.FillRectangle(brush, rect);
                e.Graphics.FillRectangle(brush2, rect2);
                e.Graphics.DrawImage(e.Image, new Point(5, 3));
                return;
            }
            Rectangle rect3 = new(4, 2, 18, 18);
            Rectangle rect4 = new(5, 3, 16, 16);
            SolidBrush brush3 = new(textColor);
            SolidBrush brush4 = new(backColor);
            e.Graphics.FillRectangle(brush3, rect3);
            e.Graphics.FillRectangle(brush4, rect4);
            e.Graphics.DrawImage(e.Image, new Point(5, 3));
        }

        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            base.OnRenderImageMargin(e);
            Rectangle rect = new(0, 0, e.ToolStrip.Width, e.ToolStrip.Height);
            e.Graphics.FillRectangle(new SolidBrush(backColor), rect);
            SolidBrush brush = new(backColor);
            Rectangle rect2 = new(0, 0, 26, e.AffectedBounds.Height);
            e.Graphics.FillRectangle(brush, rect2);
            e.Graphics.DrawLine(new Pen(new SolidBrush(backColor)), 28, 0, 28, e.AffectedBounds.Height);
        }

        public Color headerColor;

        public Color backColor;

        public Color selectedBackColor;

        public Color hoverBackColor;

        public Color textColor;

        public Color hoverTextColor;

        public Color selectedTextColor;

        public Color separatorColor;
    }


}