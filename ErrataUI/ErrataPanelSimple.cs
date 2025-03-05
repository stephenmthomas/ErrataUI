using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;

namespace ErrataUI    
{
    public class ErrataPanelSimple : Panel
    {
        private Color _borderColor = Color.Black;
        private int _borderThickness = 1;

        public ErrataPanelSimple()
        {
            // Enable double buffering for smoother rendering
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
        }

        [Category("Appearance")]
        [Description("The color of the panel's border.")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                Invalidate(); // Redraw the panel when the border color changes
            }
        }

        [Category("Appearance")]
        [Description("The thickness of the panel's border.")]
        [DefaultValue(1)]
        public int BorderThickness
        {
            get => _borderThickness;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Border thickness must be non-negative.");
                _borderThickness = value;
                Invalidate(); // Redraw the panel when the border thickness changes
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw the border
            if (_borderThickness > 0)
            {
                using (var pen = new Pen(_borderColor, _borderThickness))
                {
                    // Adjust the rectangle to account for the border thickness
                    var rect = new Rectangle(
                        _borderThickness / 2,
                        _borderThickness / 2,
                        Width - _borderThickness,
                        Height - _borderThickness
                    );
                    e.Graphics.DrawRectangle(pen, rect);
                }
            }
        }
    }
}
