using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataRadioButton : RadioButton
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



        private Color _disabled = Color.FromArgb(128, 128, 128);
        private float _borderThickness = 1.5f;
        private float _checkboxProportion = 0.65f;
        private bool _enabled = true;

        [Category("Misc")]
        [Description("Enable or disable.")]
        public bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;
                _enabled = value;
                if (base.Enabled == false)
                {
                    ForeTheme = ThemeColorShade.Neutral_500;
                }
                else if (base.Enabled == true)
                {

                    ForeTheme = ThemeColorShade.Neutral_900;
                }
                UpdateColor();
                Invalidate();
            }
        }

        #region FORE
        //FORE COLOR
        private UIRole _foreRole = UIRole.InputText;
        private ThemeColorShade _foreTheme = ThemeColorShade.Neutral_900;
        private Color _foreColor = Color.FromArgb(50, 49, 48);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole ForeRole { get => _foreRole; set { _foreRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade ForeTheme
        {
            get => _foreTheme; set
            {
                _foreTheme = value;
                if (!_ignoreTheme) { ForeColor = ThemeManager.Instance.GetThemeColorShade(_foreTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color ForeColor { get => _foreColor; set { _foreColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////ForeColor = ThemeManager.Instance.GetThemeColorShade(ForeTheme);
        #endregion

        #region UNCHECKED
        //UNCHECKED COLOR
        private UIRole _uncheckedRole = UIRole.None;
        private ThemeColorShade _uncheckedTheme = ThemeColorShade.Transparent;
        private Color _uncheckedColor = Color.Transparent;
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole UncheckedRole { get => _uncheckedRole; set { _uncheckedRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade UncheckedTheme
        {
            get => _uncheckedTheme; set
            {
                _uncheckedTheme = value;
                if (!_ignoreTheme) { UncheckedColor = ThemeManager.Instance.GetThemeColorShade(_uncheckedTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color UncheckedColor { get => _uncheckedColor; set { _uncheckedColor = value; Invalidate(); } }

        #endregion

        #region CHECKED
        //CHECKED COLOR
        private UIRole _checkedRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _checkedTheme = ThemeColorShade.Primary_500;
        private Color _checkedColor = Color.FromArgb(0, 128, 200);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole CheckedRole { get => _checkedRole; set { _checkedRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade CheckedTheme
        {
            get => _checkedTheme; set
            {
                _checkedTheme = value;
                if (!_ignoreTheme) { CheckedColor = ThemeManager.Instance.GetThemeColorShade(_checkedTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color CheckedColor { get => _checkedColor; set { _checkedColor = value; Invalidate(); } }
        #endregion

        #region BORDER
        //BORDER COLOR
        private UIRole _borderRole = UIRole.GeneralBorders;
        private ThemeColorShade _borderTheme = ThemeColorShade.Neutral_500;
        private Color _borderColor = Color.FromArgb(128, 128, 128);
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

        #region CHECKEDBORDER
        //CHECKEDBORDER COLOR
        private UIRole _checkedBorderRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _checkedBorderTheme = ThemeColorShade.Primary_500;
        private Color _checkedBorderColor = Color.FromArgb(0, 128, 200);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole CheckedBorderRole { get => _checkedBorderRole; set { _checkedBorderRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade CheckedBorderTheme
        {
            get => _checkedBorderTheme; set
            {
                _checkedBorderTheme = value;
                if (!_ignoreTheme) { CheckedBorderColor = ThemeManager.Instance.GetThemeColorShade(_checkedBorderTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color CheckedBorderColor { get => _checkedBorderColor; set { _checkedBorderColor = value; Invalidate(); } }
        #endregion




        public float CircleThickness
        {
            get => _borderThickness; set { _borderThickness = value; Invalidate(); }
        }
        public float CircleProportion
        {
            get => _checkboxProportion; set { _checkboxProportion = value; Invalidate(); }
        }

        [DefaultValue(ContentAlignment.MiddleLeft)]
        public override ContentAlignment TextAlign { get => base.TextAlign; set { base.TextAlign = value; Invalidate(); }}





        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                ForeColor = ThemeManager.Instance.GetThemeColorShade(ForeTheme);
                UncheckedColor = ThemeManager.Instance.GetThemeColorShade(UncheckedTheme);
                CheckedColor = ThemeManager.Instance.GetThemeColorShade(CheckedTheme);
                BorderColor = ThemeManager.Instance.GetThemeColorShade(BorderTheme);
                CheckedBorderColor = ThemeManager.Instance.GetThemeColorShade(CheckedBorderTheme);
            }

            if (!_enabled)
            {
                ForeColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_500);
                BorderColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_500);
                CheckedBorderColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_500);
                CheckedColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_500);
            }
        }





        public ErrataRadioButton()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            BackColor = Color.Transparent;
            AutoSize = false; // Allow manual size control
            TextAlign = ContentAlignment.MiddleLeft;
            Width = 150; // Default width
            Height = 30; // Default height
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }










        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate(); // Redraw the control when resized
            
        }

        //protected override void OnPaintBackground(PaintEventArgs e)
        //{   
        //    // Use solid background color
        //    using (SolidBrush backgroundBrush = new SolidBrush(Parent.BackColor))
        //        {
        //            e.Graphics.FillRectangle(backgroundBrush, this.ClientRectangle);
        //        }
            
        //}

        protected override void OnPaint(PaintEventArgs e)
        {

            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.Clear(this.BackColor);

            base.OnPaintBackground(e);

            // Calculate circle dimensions based on control height
            int circleSize = (int)(this.Height * CircleProportion);
            int circleX = 5; // Margin from left
            int circleY = (this.Height - circleSize) / 2;
            Rectangle circleRect = new Rectangle(circleX, circleY, circleSize, circleSize);



            // Draw circle border

            if (Checked)
            {
                using (Pen borderPen = new Pen(CheckedBorderColor, CircleThickness))
                {
                    e.Graphics.DrawEllipse(borderPen, circleRect);
                }
            }
            else
            {
                using (Pen borderPen = new Pen(BorderColor, CircleThickness))
                {
                    e.Graphics.DrawEllipse(borderPen, circleRect);
                }
            }


            // Fill circle if checked
            if (this.Checked)
            {
                int innerCircleSize = circleSize - 6; // Padding inside the circle
                Rectangle innerCircleRect = new Rectangle(
                    circleRect.X + 3,
                    circleRect.Y + 3,
                    innerCircleSize,
                    innerCircleSize);

                using (SolidBrush checkedBrush = new SolidBrush(CheckedColor))
                {
                    e.Graphics.FillEllipse(checkedBrush, innerCircleRect);
                }
            }
            else
            {
                int innerCircleSize = circleSize - 6; // Padding inside the circle
                Rectangle innerCircleRect = new Rectangle(
                    circleRect.X + 3,
                    circleRect.Y + 3,
                    innerCircleSize,
                    innerCircleSize);

                using (SolidBrush UncheckedBrush = new SolidBrush(UncheckedColor))
                {
                    e.Graphics.FillEllipse(UncheckedBrush, innerCircleRect);
                }
            }

            // Draw the text
            using (SolidBrush textBrush = new SolidBrush(this.ForeColor))
            {
                StringFormat stringFormat = new StringFormat();

                // Set horizontal alignment
                if (TextAlign == ContentAlignment.MiddleLeft ||
                    TextAlign == ContentAlignment.TopLeft ||
                    TextAlign == ContentAlignment.BottomLeft)
                {
                    stringFormat.Alignment = StringAlignment.Near;
                }
                else if (TextAlign == ContentAlignment.MiddleCenter ||
                         TextAlign == ContentAlignment.TopCenter ||
                         TextAlign == ContentAlignment.BottomCenter)
                {
                    stringFormat.Alignment = StringAlignment.Center;
                }
                else
                {
                    stringFormat.Alignment = StringAlignment.Far;
                }

                // Set vertical alignment
                if (TextAlign == ContentAlignment.TopLeft ||
                    TextAlign == ContentAlignment.TopCenter ||
                    TextAlign == ContentAlignment.TopRight)
                {
                    stringFormat.LineAlignment = StringAlignment.Near;
                }
                else if (TextAlign == ContentAlignment.MiddleLeft ||
                         TextAlign == ContentAlignment.MiddleCenter ||
                         TextAlign == ContentAlignment.MiddleRight)
                {
                    stringFormat.LineAlignment = StringAlignment.Center;
                }
                else
                {
                    stringFormat.LineAlignment = StringAlignment.Far;
                }

                // Adjust text position to the right of the circle
                int textX = circleRect.Right + 10; // Spacing between circle and text
                Rectangle textRect = new Rectangle(textX, 0, this.Width - textX, this.Height);

                e.Graphics.DrawString(this.Text, this.Font, textBrush, textRect, stringFormat);
            }
        }
    }
}
