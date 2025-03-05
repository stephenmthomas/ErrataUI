using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataMenuStrip : MenuStrip
    {
        private Color _menuBackColor = SystemColors.Control; // Default dark background
        private Color _menuForeColor = SystemColors.ControlText;               // Default light text
        private Color _menuHoverColor = SystemColors.GradientActiveCaption; // Hover effect
        private Color _menuHoverBorderColor = SystemColors.Highlight;
        public int _menuHoverBorderWeight = 1;

        public ErrataMenuStrip()
        {
            Renderer = new ModernToolStripRenderer(_menuBackColor, _menuForeColor, _menuHoverColor, _menuHoverBorderColor);
            BackColor = _menuBackColor;
            ForeColor = _menuForeColor;
            MenuHoverBorderColor = _menuHoverColor;
            HoverBorderWeight = 1;
            Font = new Font("Segoe UI", 9); // Modern font
            Padding = new Padding(4, 0, 0, 0);
            AutoSize = false;
            Height = 25;

        }


        // Expose properties to allow customization
        [Category("Misc.Lines")]
        [Description("Outdated - nonfunctional.")]
        public int HoverBorderWeight
        {
            get => _menuHoverBorderWeight;
            set
            {
                _menuHoverBorderWeight = value;
                Invalidate(); // Repaint the control when text changes
            }
        }


        public Color MenuBackColor
        {
            get => _menuBackColor;
            set
            {
                _menuBackColor = value;
                Renderer = new ModernToolStripRenderer(_menuBackColor, _menuForeColor, _menuHoverColor, _menuHoverBorderColor);
                Invalidate();
            }
        }

        public Color MenuForeColor
        {
            get => _menuForeColor;
            set
            {
                _menuForeColor = value;
                Renderer = new ModernToolStripRenderer(_menuBackColor, _menuForeColor, _menuHoverColor, _menuHoverBorderColor);
                Invalidate();
            }
        }

        public Color MenuHoverColor
        {
            get => _menuHoverColor;
            set
            {
                _menuHoverColor = value;
                Renderer = new ModernToolStripRenderer(_menuBackColor, _menuForeColor, _menuHoverColor,_menuHoverBorderColor);
                Invalidate();
            }
        }

        public Color MenuHoverBorderColor
        {
            get => _menuHoverBorderColor;
            set
            {
                _menuHoverBorderColor = value;
                Renderer = new ModernToolStripRenderer(_menuBackColor, _menuForeColor, _menuHoverColor, _menuHoverBorderColor);
                
                Invalidate();
            }
        }


    }

    public class ModernToolStripRenderer : ToolStripProfessionalRenderer
    {
        private readonly Color _backColor;
        private readonly Color _foreColor;
        private readonly Color _hoverColor;
        private readonly Color _hoverBorderColor;


        public ModernToolStripRenderer(Color backColor, Color foreColor, Color hoverColor, Color hoverBorderColor)
        {
            _backColor = backColor;
            _foreColor = foreColor;
            _hoverColor = hoverColor;
            _hoverBorderColor = hoverBorderColor;

        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {

            //bool isHovered = e.Item.Selected || e.Item.Pressed;
            bool isPressed = e.Item.Pressed;
            bool isSelected = e.Item.Selected;

            //trigger for any menu item
            if (e.Item.Selected)
            {
                
                //if the menu item has a visible drop down (so a top level menu item)
                // then draw the regular border and exit - no highlights
                if (e.Item is ToolStripMenuItem menuItem && menuItem.DropDown.Visible) 
                {
                    using (Pen borderPen = new Pen(Color.FromArgb(128, 128, 128)))
                    {
                        e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1));
                    }
                    return; 
                
                }

                //otherwise draw some hover highlights
                using (Brush b = new SolidBrush(_hoverColor))
                {
                    e.Graphics.FillRectangle(b, new Rectangle(1,0,e.Item.Width - 2, e.Item.Height - 1));
                }
                using (Pen hovBorder = new Pen(_hoverBorderColor,1 ))
                {
                    e.Graphics.DrawRectangle(hovBorder, new Rectangle(1, 0, e.Item.Width - 2, e.Item.Height - 1));
                }

                e.Item.ForeColor = _foreColor;
            }
            else
            {
                using (Brush b = new SolidBrush(_backColor))
                {
                    e.Graphics.FillRectangle(b, new Rectangle(-2,-2, e.Item.Width + 4, e.Item.Height + 4));
                }
                e.Item.ForeColor = _foreColor;
            }

            //keeps a border around top level menu item even as subsequent items are selected
            if (isPressed)
            {
                if (e.Item.Owner is MenuStrip)
                {
                    using (Pen borderPen = new Pen(Color.FromArgb(128, 128, 128),0.5f))
                    {
                        e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1));
                    }
                }
            }
            
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            using (Brush b = new SolidBrush(_backColor))
            {
                e.Graphics.FillRectangle(b, e.AffectedBounds);
            }
        }
    }
}