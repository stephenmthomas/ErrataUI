using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection.Metadata;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Font = System.Drawing.Font;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataGroupBox : Control
    {
        private string _groupBoxText = "GroupBox";
        private Font _groupBoxFont = new Font("Segoe UI Semibold", 9, FontStyle.Regular);
        private Color _groupBoxTextColor = Color.Black;
        private Color _backgroundColor = Color.White;
        private Color _borderColor = Color.Gray;  // Color of the border
        private Color _captionLineColor = Color.Gray;  // Color of the line under the title
        private Color _captionBarColor = SystemColors.GradientActiveCaption;
        private Color _gradientColor = SystemColors.GradientInactiveCaption;

        private int _borderWidth = 1;  // Border width
        private int _leftPadding = 8;
        private int _topPadding = 6;
        private int _captionLineWeight = 1;  // Height of the line under the title
        private int _captionLineOffset = 0;
        private float _letterSpacing = -3.5F;
        private float _gradientAngle = 0F;

        private bool _showCaptionLine = true;
        private bool _completeCaptionLine = true;
        private bool _colorCaptionBar = true;
        private bool _gradientFill = true;




        #region DESIGNER OPTIONS
        [Category("Misc")]
        [Description("Select label font options.")]
        public Font GroupBoxFont
        {
            get => _groupBoxFont;
            set
            {
                _groupBoxFont = value;
                Invalidate(); // Repaint the control when text changes
            }
        }




        [Category("Misc")]
        [Description("Sets text.")]
        public string GroupBoxText
        {
            get => _groupBoxText;
            set
            {
                _groupBoxText = value;
                Invalidate(); // Repaint the control when text changes
            }
        }

        [Category("Misc")]
        [Description("Fills the caption bar with a gradient.")]
        public bool GradientFill
        {
            get => _gradientFill;
            set
            {
                _gradientFill = value;
                Invalidate(); // Repaint the control when color changes
            }
        }

        [Category("Misc")]
        [Description("Select to color the caption line area.")]
        public bool ColorCaptionLine
        {
            get => _colorCaptionBar;
            set
            {
                _colorCaptionBar = value;
                Invalidate(); // Repaint the control when color changes
            }
        }


        [Category("Misc")]
        [Description("Select to draw caption line entirely.")]
        public bool CompleteCaptionLine
        {
            get => _completeCaptionLine;
            set
            {
                _completeCaptionLine = value;
                Invalidate(); // Repaint the control when color changes
            }
        }

        [Category("Misc")]
        [Description("Select to draw caption line.")]
        public bool ShowCaptionLine
        {
            get => _showCaptionLine;
            set
            {
                _showCaptionLine = value;
                Invalidate(); // Repaint the control when color changes
            }
        }

        [Category("Misc.Color")]
        [Description("Sets caption bar color.")]
        public Color CaptionBarColor
        {
            get => _captionBarColor;
            set
            {
                _captionBarColor = value;
                Invalidate(); // Repaint the control when color changes
            }
        }

        [Category("Misc.Color")]
        [Description("Sets caption gradient.")]
        public Color GradientColor
        {
            get => _gradientColor;
            set
            {
                _gradientColor = value;
                Invalidate(); // Repaint the control when color changes
            }
        }


        [Category("Misc.Color")]
        [Description("Sets caption line color.")]
        public Color CaptionLineColor
        {
            get => _captionLineColor;
            set
            {
                _captionLineColor = value;
                Invalidate(); // Repaint the control when color changes
            }
        }

        [Category("Misc.Color")]
        [Description("Sets text color.")]
        public Color GroupBoxTextColor
        {
            get => _groupBoxTextColor;
            set
            {
                _groupBoxTextColor = value;
                Invalidate(); // Repaint the control when color changes
            }
        }

        [Browsable(true)]
        [Category("Misc.Color")] // Custom category
        [Description("Gets or sets the background color of the button.")]
        public new Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        [Category("Misc.Color")]
        [Description("Border color.")]
        public Color GroupBoxBorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                Invalidate(); // Repaint the control when border color changes
            }
        }


        [Category("Misc.Lines")]
        [Description("Angle of the gradient.")]
        public float GradientAngle
        {
            get => _gradientAngle;
            set
            {
                _gradientAngle = value;
                Invalidate(); // Repaint the control when text changes
            }
        }


        [Category("Misc.Lines")]
        [Description("Sets the letter spacing.")]
        public float LetterSpacing
        {
            get => _letterSpacing;
            set
            {
                _letterSpacing = value;
                Invalidate(); // Repaint the control when text changes
            }
        }


        [Category("Misc.Lines")]
        [Description("Sets caption line height.")]
        public int CaptionLineWeight
        {
            get => _captionLineWeight;
            set
            {
                _captionLineWeight = value;
                Invalidate(); // Repaint the control when text changes
            }
        }

        [Category("Misc.Lines")]
        [Description("Adjusts the caption line height.")]
        public int CaptionLineOffset
        {
            get => _captionLineOffset;
            set
            {
                _captionLineOffset = value;
                Invalidate(); // Repaint the control when text changes
            }
        }


        [Category("Misc.Lines")]
        [Description("Sets the padding around the primary text.")]
        public int TopPadding
        {
            get => _topPadding;
            set
            {
                _topPadding = value;
                Invalidate(); // Repaint the control when text changes
            }
        }

        [Category("Misc.Lines")]
        [Description("Sets the padding around the primary text.")]
        public int LeftPadding
        {
            get => _leftPadding;
            set
            {
                _leftPadding = value;
                Invalidate(); // Repaint the control when text changes
            }
        }

        [Category("Misc.Lines")]
        [Description("Border width.")]
        public int GroupBoxBorderWidth
        {
            get => _borderWidth;
            set
            {
                _borderWidth = value;
                Invalidate(); // Repaint the control when border width changes
            }
        }
        #endregion


        // Constructor
        public ErrataGroupBox()
        {
            // Set the control style to prevent painting of the background and borders.
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            Size = new Size(200, 100);  // Default size
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Graphics g = e.Graphics)
            {
                g.Clear(BackColor);
                if (_colorCaptionBar) { ShadeCaptionBar(g); }
                DrawGroupBoxText(g);
                DrawBorder(g);
                if (_showCaptionLine) { DrawCaptionLine(g); }
            }
        }

        private void ShadeCaptionBar(Graphics g)
        {
            float lineYPosition = _topPadding + g.MeasureString(_groupBoxText, _groupBoxFont).Height + 3;
            Rectangle rect = new Rectangle(0,0,Width, (int)lineYPosition );
            if (_gradientFill)
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(rect, _captionBarColor, _gradientColor, _gradientAngle))
                {
                    //g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.FillRectangle(brush, rect);
                }
            }
            else
            {
                using (Brush brush = new SolidBrush(_captionBarColor))
                {
                    g.FillRectangle(brush, rect);
                }
            }    
        }

        private void DrawCaptionLine(Graphics g)
        {
            // Position the line just below the text
            float lineYPosition = _topPadding + g.MeasureString(_groupBoxText, _groupBoxFont).Height + 3;
            lineYPosition += _captionLineOffset;

            using (Pen linePen = new Pen(_captionLineColor, _captionLineWeight))
            {
                // Draw the horizontal line
                if (_completeCaptionLine) 
                { 
                    g.DrawLine(linePen, 0, lineYPosition, Width, lineYPosition); 
                }
                else
                {
                    g.DrawLine(linePen, _topPadding, lineYPosition, Width - _topPadding, lineYPosition);
                }
            }
        }

        private void DrawGroupBoxText(Graphics g)
        {
            // Define the area where the text will be drawn
            var textSize = g.MeasureString(_groupBoxText, _groupBoxFont);
            var textRect = new RectangleF(_leftPadding, _topPadding, textSize.Width, textSize.Height);

            // Create a solid brush with the text color
            using (Brush textBrush = new SolidBrush(_groupBoxTextColor))
            {

                float letterSpacing = _letterSpacing; // Adjust this value for desired spacing
                float x = _leftPadding; // Starting X position
                float y = _topPadding;  // Starting Y position

                foreach (char c in _groupBoxText)
                {
                    string character = c.ToString();
                    SizeF charSize = g.MeasureString(character, _groupBoxFont);
                    g.DrawString(character, _groupBoxFont, textBrush, new PointF(x, y));

                    // Adjust X position based on character width + spacing
                    x += charSize.Width + letterSpacing;

                    //g.DrawString(_groupBoxText, _groupBoxFont, textBrush, textRect);
                }
            }
        }


        private void DrawBorder(Graphics g)
        {
            using (Pen borderPen = new Pen(_borderColor, _borderWidth))
            {
                Rectangle borderRect = new Rectangle(0, 0, Width - 1, Height - 1);
                g.DrawRectangle(borderPen, borderRect);
            }
        }

        // Override the OnLayout method to ensure the size and layout of the children are handled properly
        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            // In this case, we're not doing any custom layout, so just call base method.
        }

        // Override the OnSizeChanged method to handle resizing
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            // Invalidate the control to force a repaint when the size changes
            Invalidate();
        }
    }
}