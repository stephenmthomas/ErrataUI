#region Imports


using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ErrataUI
{
    

    public class ErrataFlatMenuStrip : MenuStrip
    {
        public ErrataFlatMenuStrip()
        {
            base.Renderer = new ErrataMenuStripRenderer(base.BackColor, backColor, selectedBackColor, hoverBackColor, textColor, hoverTextColor, selectedTextColor, separatorColor);
            base.BackColor = Color.DodgerBlue;
        }

        private void RefreshUI()
        {
            base.Renderer = new ErrataMenuStripRenderer(base.BackColor, backColor, selectedBackColor, hoverBackColor, textColor, hoverTextColor, selectedTextColor, separatorColor);
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Item background color")]
        public Color ItemBackColor
        {
            get => backColor;
            set
            {
                backColor = value;
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Selected item background color")]
        public Color SelectedBackColor
        {
            get => selectedBackColor;
            set
            {
                selectedBackColor = value;
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Hover item background color")]
        public Color HoverBackColor
        {
            get => hoverBackColor;
            set
            {
                hoverBackColor = value;
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Item text color")]
        public Color TextColor
        {
            get => textColor;
            set
            {
                textColor = value;
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Hover item text color")]
        public Color HoverTextColor
        {
            get => hoverTextColor;
            set
            {
                hoverTextColor = value;
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Selected item text color")]
        public Color SelectedTextColor
        {
            get => selectedTextColor;
            set
            {
                selectedTextColor = value;
                RefreshUI();
            }
        }

        [Category("Parrot")]
        [Browsable(true)]
        [Description("Separator color")]
        public Color SeparatorColor
        {
            get => separatorColor;
            set
            {
                separatorColor = value;
                RefreshUI();
            }
        }

        private Color backColor = Color.DodgerBlue;

        private Color selectedBackColor = Color.DarkOrchid;

        private Color hoverBackColor = Color.RoyalBlue;

        private Color textColor = Color.White;

        private Color hoverTextColor = Color.White;

        private Color selectedTextColor = Color.White;

        private Color separatorColor = Color.White;
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