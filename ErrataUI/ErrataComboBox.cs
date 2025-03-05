using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ErrataUI;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataComboBox : ComboBox
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

        private int _borderWeight = 1;
        [Category("Misc")]
        public int BorderWeight
        {
            get => _borderWeight;
            set
            {
                _borderWeight = value;
                Invalidate();
            }
        }

        private int _underlineWeight = 2;
        [Category("Misc")]
        public int UnderlineWeight
        {
            get => _underlineWeight;
            set
            {
                _underlineWeight = value;
            }
        }

        private bool _drawUnderline = true;
        [Category("Misc")]
        public bool DrawUnderline
        {
            get => _drawUnderline;
            set
            {
                _drawUnderline = value;
            }
        }

        private Color currentArrowColor;

        #region BORDERCOLOR
        //BORDERCOLOR
        private UIRole _borderColorRole = UIRole.GeneralBorders;
        private ThemeColorShade _borderColorTheme = ThemeColorShade.Neutral_400;
        private Color _borderColor = Color.FromArgb(128, 128, 128);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole BorderColorRole { get => _borderColorRole; set { _borderColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade BorderColorTheme
        {
            get => _borderColorTheme; set
            {
                _borderColorTheme = value;
                if (!_ignoreTheme) { BorderColor = ThemeManager.Instance.GetThemeColorShade(_borderColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color BorderColor { get => _borderColor; set { _borderColor = value; Invalidate(); } }
        #endregion
        #region FOCUSEDBORDERCOLOR
        //FOCUSEDBORDERCOLOR
        private UIRole _focusedBorderColorRole = UIRole.EmphasizedBorders;
        private ThemeColorShade _focusedBorderColorTheme = ThemeColorShade.Neutral_500;
        private Color _focusedBorderColor = Color.FromArgb(0, 128, 200);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole FocusedBorderColorRole { get => _focusedBorderColorRole; set { _focusedBorderColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade FocusedBorderColorTheme
        {
            get => _focusedBorderColorTheme; set
            {
                _focusedBorderColorTheme = value;
                if (!_ignoreTheme) { FocusedBorderColor = ThemeManager.Instance.GetThemeColorShade(_focusedBorderColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color FocusedBorderColor { get => _focusedBorderColor; set { _focusedBorderColor = value; Invalidate(); } }
        #endregion
        #region UNDERLINEINACTIVE
        //UNDERLINEINACTIVE
        private UIRole _underlineInactiveRole = UIRole.AccentColor;
        private ThemeColorShade _underlineInactiveTheme = ThemeColorShade.Neutral_500;
        private Color _underlineInactive = Color.FromArgb(128, 128, 128);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole UnderlineInactiveRole { get => _underlineInactiveRole; set { _underlineInactiveRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade UnderlineInactiveTheme
        {
            get => _underlineInactiveTheme; set
            {
                _underlineInactiveTheme = value;
                if (!_ignoreTheme) { UnderlineInactive = ThemeManager.Instance.GetThemeColorShade(_underlineInactiveTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color UnderlineInactive { get => _underlineInactive; set { _underlineInactive = value; Invalidate(); } }
        #endregion
        #region UNDERLINEACTIVE
        //UNDERLINEACTIVE
        private UIRole _underlineActiveRole = UIRole.AccentColor;
        private ThemeColorShade _underlineActiveTheme = ThemeColorShade.Primary_700;
        private Color _underlineActive = Color.FromArgb(128, 128, 128);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole UnderlineActiveRole { get => _underlineActiveRole; set { _underlineActiveRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade UnderlineActiveTheme
        {
            get => _underlineActiveTheme; set
            {
                _underlineActiveTheme = value;
                if (!_ignoreTheme) { UnderlineActive = ThemeManager.Instance.GetThemeColorShade(_underlineActiveTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color UnderlineActive { get => _underlineActive; set { _underlineActive = value; Invalidate(); } }
        #endregion
        #region ARROWACTIVECOLOR
        //ARROWACTIVECOLOR
        private UIRole _arrowActiveColorRole = UIRole.AccentColor;
        private ThemeColorShade _arrowActiveColorTheme = ThemeColorShade.Primary_500;
        private Color _arrowActiveColor = Color.FromArgb(128, 128, 128);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole ArrowActiveColorRole { get => _arrowActiveColorRole; set { _arrowActiveColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade ArrowActiveColorTheme
        {
            get => _arrowActiveColorTheme; set
            {
                _arrowActiveColorTheme = value;
                if (!_ignoreTheme) { ArrowActiveColor = ThemeManager.Instance.GetThemeColorShade(_arrowActiveColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color ArrowActiveColor { get => _arrowActiveColor; set { _arrowActiveColor = value; Invalidate(); } }
        #endregion
        #region ARROWINACTIVECOLOR
        //ARROWINACTIVECOLOR
        private UIRole _arrowInactiveColorRole = UIRole.DisabledText;
        private ThemeColorShade _arrowInactiveColorTheme = ThemeColorShade.Neutral_400;
        private Color _arrowInactiveColor = Color.FromArgb(128, 128, 128);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole ArrowInactiveColorRole { get => _arrowInactiveColorRole; set { _arrowInactiveColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade ArrowInactiveColorTheme
        {
            get => _arrowInactiveColorTheme; set
            {
                _arrowInactiveColorTheme = value;
                if (!_ignoreTheme) { ArrowInactiveColor = ThemeManager.Instance.GetThemeColorShade(_arrowInactiveColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color ArrowInactiveColor { get => _arrowInactiveColor; set { _arrowInactiveColor = value; Invalidate(); } }
        #endregion


        #region HIGHLIGHTTEXTCOLOR
        //HIGHLIGHTTEXTCOLOR
        private UIRole _highlightTextColorRole = UIRole.MenuBar;
        private ThemeColorShade _highlightTextColorTheme = ThemeColorShade.Neutral_100;
        private Color _highlightTextColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_100);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole HighlightTextColorRole { get => _highlightTextColorRole; set { _highlightTextColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade HighlightTextColorTheme
        {
            get => _highlightTextColorTheme; set
            {
                _highlightTextColorTheme = value;
                if (!_ignoreTheme) { HighlightTextColor = ThemeManager.Instance.GetThemeColorShade(_highlightTextColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color HighlightTextColor { get => _highlightTextColor; set { _highlightTextColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion

        #region HIGHLIGHTBACKCOLOR
        //HIGHLIGHTBACKCOLOR
        private UIRole _highlightBackColorRole = UIRole.MenuBar;
        private ThemeColorShade _highlightBackColorTheme = ThemeColorShade.Primary_700;
        private Color _highlightBackColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_700);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole HighlightBackColorRole { get => _highlightBackColorRole; set { _highlightBackColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade HighlightBackColorTheme
        {
            get => _highlightBackColorTheme; set
            {
                _highlightBackColorTheme = value;
                if (!_ignoreTheme) { HighlightBackColor = ThemeManager.Instance.GetThemeColorShade(_highlightBackColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color HighlightBackColor { get => _highlightBackColor; set { _highlightBackColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion



        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                BorderColor = ThemeManager.Instance.GetThemeColorShade(BorderColorTheme);
                FocusedBorderColor = ThemeManager.Instance.GetThemeColorShade(FocusedBorderColorTheme);
                UnderlineInactive = ThemeManager.Instance.GetThemeColorShade(UnderlineInactiveTheme);
                UnderlineActive = ThemeManager.Instance.GetThemeColorShade(UnderlineActiveTheme);
                ArrowActiveColor = ThemeManager.Instance.GetThemeColorShade(ArrowActiveColorTheme);
                ArrowInactiveColor = ThemeManager.Instance.GetThemeColorShade(ArrowInactiveColorTheme);
                HighlightBackColor = ThemeManager.Instance.GetThemeColorShade(HighlightBackColorTheme);
                HighlightTextColor = ThemeManager.Instance.GetThemeColorShade(HighlightTextColorTheme);
            }
        }

        // Constructor
        public ErrataComboBox()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.FlatStyle = FlatStyle.Flat;
            Font = new Font("Segoe UI", 12, FontStyle.Regular);
            this.Width = 150;
            this.Height = 30; // Set default height
            this.SetStyle(ControlStyles.UserPaint, true);
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }

        public void AddItems(IEnumerable<string> items)
        {
            this.Items.Clear();
            foreach (var item in items)
            {
                this.Items.Add(item);
            }
        }

        // Override the OnPaint method to draw the border and background
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Determine the border color based on focus
            Color currentBorderColor = this.Focused ? FocusedBorderColor : BorderColor;
            Color currentUnderlineColor = this.Focused ? UnderlineActive : UnderlineInactive;
            currentArrowColor = this.Focused ? ArrowActiveColor : ArrowInactiveColor;

            // Create a graphics object and set up drawing parameters
            Graphics g = e.Graphics;
            

            // Define the bounds of the ComboBox for the border and background
            Rectangle borderRect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            

            // Fill the background of the ComboBox
            using (Brush backgroundBrush = new SolidBrush(this.BackColor))
            {
                g.FillRectangle(backgroundBrush, borderRect);
            }

            // Create a pen to draw the border (no rounded corners)
            using (Pen borderPen = new Pen(currentBorderColor, BorderWeight)) // Border width = 2
            {
                g.DrawRectangle(borderPen, borderRect);
            }

            using (Pen underLinePen = new Pen(currentUnderlineColor, UnderlineWeight)) 
            {
                g.DrawLine(underLinePen, borderRect.Left , borderRect.Bottom, borderRect.Right, borderRect.Bottom);
            }

            // Draw the text of the selected item
            if (this.SelectedIndex >= 0)
            {
                string text = this.GetItemText(this.SelectedItem);
                Rectangle textRect = new Rectangle(2, 2, this.Width - 22, this.Height - 4);

                // Draw the text with specified font and alignment
                TextRenderer.DrawText(
                    g,
                    text,
                    this.Font,
                    textRect,
                    this.ForeColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            }



            // Draw the drop-down arrow
            DrawDropDownButton(g);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            if (e.Index < 0) return;

            // Retrieve the item text
            string text = this.GetItemText(this.Items[e.Index]);

            // Determine if the item is selected
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;


            // Set custom background color
            using (Brush backgroundBrush = new SolidBrush(isSelected ? _highlightBackColor : this.BackColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
            }

            // Set custom text color
            using (Brush textBrush = new SolidBrush(isSelected ? _highlightTextColor : this.ForeColor))
            {
                e.Graphics.DrawString(text, this.Font, textBrush, e.Bounds.Left + 2, e.Bounds.Top + ((e.Bounds.Height - this.Font.Height) / 2));
            }


        }


        // Method to draw the drop-down button (the arrow)
        private void DrawDropDownButton(Graphics g)
        {
            // Use a simple arrow for the drop-down button
            Point[] arrowPoints = new Point[]
            {
                new Point(this.Width - 20, this.Height / 2 - 2),
                new Point(this.Width - 10, this.Height / 2 - 2),
                new Point(this.Width - 15, this.Height / 2 + 3)
            };

            using (Brush arrowBrush = new SolidBrush(currentArrowColor))
            {
                //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillPolygon(arrowBrush, arrowPoints);
            }
        }
    }
}
