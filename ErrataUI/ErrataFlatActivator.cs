using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataFlatActivator : Button
    {

        // Customizable properties
        private Color _hoverBackColor = Color.LightGray;
        private Color _defaultBackColor = Color.White;
        private Color _defaultForeColor = Color.Black;
        private Color _hoverForeColor = Color.Black;
        private bool _isMouseOver = false;

        public ErrataFlatActivator()
        {
            // Default styles
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Font = new Font("Segoe MDL2 Assets", 7F, FontStyle.Regular, GraphicsUnit.Point);
            BackColor = _defaultBackColor;
            ForeColor = _defaultForeColor;
            Height = 20;
            Width = 20;
            Text = "\uE8BB";


        }

        // Properties to customize hover and default colors
        public Color HoverBackColor
        {
            get => _hoverBackColor;
            set => _hoverBackColor = value;
        }

        public Color DefaultBackColor
        {
            get => _defaultBackColor;
            set
            {
                _defaultBackColor = value;
                BackColor = value;
            }
        }

        public Color HoverForeColor
        {
            get => _hoverForeColor;
            set => _hoverForeColor = value;
        }

        public Color DefaultForeColor
        {
            get => _defaultForeColor;
            set
            {
                _defaultForeColor = value;
                ForeColor = value;
            }
        }

        // Override mouse events for hover effects
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isMouseOver = true;
            BackColor = _hoverBackColor;
            ForeColor = _hoverForeColor;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isMouseOver = false;
            BackColor = _defaultBackColor;
            ForeColor = _defaultForeColor;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            // Optional: Custom border or effects
            if (_isMouseOver)
            {
                using (Pen pen = new Pen(Color.DarkGray, 1))
                {
                    pevent.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
                }
            }
        }
    }
}

