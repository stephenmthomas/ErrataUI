using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Drawing;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;
using System.ComponentModel;

namespace ErrataUI
{
    public class ErrataDateTimePicker : DateTimePicker
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

        private int _textShiftY = 5;
        [Category("Misc")]
        public int TextShiftY
        {
            get => _textShiftY;
            set
            {
                _textShiftY = value; Invalidate();
            }
        }
        private int _textShiftX = 8;
        [Category("Misc")]
        public int TextShiftX
        {
            get => _textShiftX;
            set
            {
                _textShiftX = value; Invalidate();
            }
        }

        private int _borderThickness = 1;
        [Category("Misc")]
        public int BorderThickness
        {
            get => _borderThickness;
            set
            {
                _borderThickness = value; Invalidate();
            }
        }

        private bool _checked = true;
        [Category("Misc")]
        public bool Checked
        {
            get => _checked;
            set
            {
                _checked = value; 
                //if (value == false) { this.Value = null; }
                Invalidate();
            }
        }

        private bool _nullable = true;
        [Category("Misc")]
        public bool Nullable
        {
            get => _nullable;
            set
            {
                _nullable = value; Invalidate();
            }
        }

        private float _checkBoxProportion = 0.65F;
        [Category("Misc")]
        public float CheckBoxProportion
        {
            get => _checkBoxProportion;
            set
            {
                _checkBoxProportion = value; Invalidate();
            }
        }

        #region BACKGROUNDCOLOR
        //BACKGROUNDCOLOR
        private UIRole _backgroundColorRole = UIRole.MainBackground;
        private ThemeColorShade _backgroundColorTheme = ThemeColorShade.Neutral_50;
        private Color _backgroundColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_50);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole BackgroundColorRole { get => _backgroundColorRole; set { _backgroundColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade BackgroundColorTheme
        {
            get => _backgroundColorTheme; set
            {
                _backgroundColorTheme = value;
                if (!_ignoreTheme) { BackgroundColor = ThemeManager.Instance.GetThemeColorShade(_backgroundColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color BackgroundColor { get => _backgroundColor; set { _backgroundColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region HOVERCOLOR
        //HOVERCOLOR
        private UIRole _hoverColorRole = UIRole.TitleBarText;
        private ThemeColorShade _hoverColorTheme = ThemeColorShade.Neutral_500;
        private Color _hoverColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole HoverColorRole { get => _hoverColorRole; set { _hoverColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade HoverColorTheme
        {
            get => _hoverColorTheme; set
            {
                _hoverColorTheme = value;
                if (!_ignoreTheme) { HoverColor = ThemeManager.Instance.GetThemeColorShade(_hoverColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color HoverColor { get => _hoverColor; set { _hoverColor = value; Invalidate(); } }

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
        public Color TextColor { get => _textColor; set { _textColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region BORDERCOLOR
        //BORDERCOLOR
        private UIRole _borderColorRole = UIRole.MenuBar;
        private ThemeColorShade _borderColorTheme = ThemeColorShade.Primary_500;
        private Color _borderColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);

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

        //ADD TO UPDATECOLOR METHOD
        ////
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




        

        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                BorderColor = ThemeManager.Instance.GetThemeColorShadeOffset(BorderColorTheme);
                ArrowActiveColor = ThemeManager.Instance.GetThemeColorShade(ArrowActiveColorTheme);
                ArrowInactiveColor = ThemeManager.Instance.GetThemeColorShade(ArrowInactiveColorTheme);
                TextColor = ThemeManager.Instance.GetThemeColorShadeOffset(TextColorTheme);
                HoverColor = ThemeManager.Instance.GetThemeColorShadeOffset(HoverColorTheme);
                BackgroundColor = ThemeManager.Instance.GetThemeColorShadeOffset(BackgroundColorTheme);
            }
        }




        public ErrataDateTimePicker()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.Font = new Font("Segoe UI", 10);
            this.BackColor = BackgroundColor;
            this.ForeColor = TextColor;
            this.Format = DateTimePickerFormat.Custom;
            this.CustomFormat = "yyyy-MM-dd HH:mm";
        }


        private Color currentArrowColor;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;



            



            using (Pen borderPen = new Pen(BorderColor, _borderThickness))
            using (SolidBrush textBrush = new SolidBrush(TextColor))
            using (SolidBrush backBrush = new SolidBrush(BackgroundColor))
            {
                e.Graphics.FillRectangle(backBrush, ClientRectangle);
                e.Graphics.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);

                string dateString = this.Value.ToString(this.CustomFormat);
                e.Graphics.DrawString(dateString, Font, textBrush, new PointF(_textShiftX, _textShiftY));
            }


            if (Nullable)
            {
                // Calculate checkbox dimensions based on control height
                int checkBoxSize = (int)(this.Height * CheckBoxProportion);
                int checkBoxX = 5; // Margin from left
                int checkBoxY = (this.Height - checkBoxSize) / 2;
                Rectangle checkBoxRect = new Rectangle(checkBoxX, checkBoxY, checkBoxSize, checkBoxSize);

                // Draw checkbox border

                if (Checked)
                {
                    using (Pen borderPen = new Pen(this.Focused ? ArrowActiveColor : ArrowInactiveColor, BorderThickness))
                    {
                        e.Graphics.DrawRectangle(borderPen, checkBoxRect);
                    }
                }
                else
                {
                    using (Pen borderPen = new Pen(BorderColor, BorderThickness))
                    {
                        e.Graphics.DrawRectangle(borderPen, checkBoxRect);
                    }
                }


                // Fill checkbox if checked
                if (this.Checked)
                {
                    int innerCheckSize = checkBoxSize - 4; // Padding inside the checkbox
                    Rectangle innerCheckRect = new Rectangle(
                        checkBoxRect.X + 2,
                        checkBoxRect.Y + 2,
                        innerCheckSize,
                        innerCheckSize);

                    using (SolidBrush checkedBrush = new SolidBrush(this.Focused ? ArrowActiveColor : ArrowInactiveColor))
                    {
                        e.Graphics.FillRectangle(checkedBrush, innerCheckRect);
                    }
                }
                else
                {
                    int innerCheckSize = checkBoxSize - 4; // Padding inside the checkbox
                    Rectangle innerCheckRect = new Rectangle(
                        checkBoxRect.X + 2,
                        checkBoxRect.Y + 2,
                        innerCheckSize,
                        innerCheckSize);

                    using (SolidBrush uncheckedBrush = new SolidBrush(BackgroundColor))
                    {
                        e.Graphics.FillRectangle(uncheckedBrush, innerCheckRect);
                    }
                }
            }


            // Create a graphics object and set up drawing parameters
            Graphics g = e.Graphics;
            currentArrowColor = this.Focused ? ArrowActiveColor : ArrowInactiveColor;
            // Draw the drop-down arrow
            DrawDropDownButton(g);
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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (Nullable)
            {
                // Calculate checkbox dimensions
                int checkBoxSize = (int)(this.Height * CheckBoxProportion);
                int checkBoxX = 5; // Margin from left
                int checkBoxY = (this.Height - checkBoxSize) / 2;
                Rectangle checkBoxRect = new Rectangle(checkBoxX, checkBoxY, checkBoxSize, checkBoxSize);

                // If the mouse click is within the checkbox area, toggle Checked
                if (checkBoxRect.Contains(e.Location))
                {
                    Checked = !Checked;
                    Invalidate(); // Redraw control to reflect changes
                }
            }
        }


    }
}
