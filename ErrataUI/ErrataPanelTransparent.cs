using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{

    public class ErrataPanelTransparent : Panel
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Prevent base class from painting the background
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Custom painting logic if needed
            // Call base.OnPaint(e) if you want default painting behavior
            base.OnPaint(e);
        }

        public void RefreshTransparency()
        {
            // Ensure the parent repaints to reflect transparency changes
            this.Parent?.Invalidate(this.Bounds, true);
        }
    }
}