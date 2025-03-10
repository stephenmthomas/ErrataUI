using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using static ErrataUI.ThemeManager;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.IO;
using System.Drawing.Design;

namespace ErrataUI
{
    public class ErrataTurnKnob : Control
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



        private Image _dialTexture;
        [Category("Textures")]
        [Description("The texture for the dial face.")]
        [Editor(typeof(System.Drawing.Design.ImageEditor), typeof(UITypeEditor))] // This makes it editable in the designer
        public Image WoodTexture
        {
            get { return _dialTexture; }
            set
            {
                _dialTexture = value;
                Invalidate(); // Redraw the control when the image is changed
            }
        }






        private bool _isMouseDown = false; // Track mouse down state
        private Point _lastMousePos; // Last mouse position


        private float _value; // Current value of the knob (0-100 range)
        public float Value
        {
            get { return _value; }
            set
            {
                _value = Math.Max(0, Math.Min(100, value)); // Keep value within 0 to 100 range
                Invalidate(); // Redraw control when value changes
            }
        }

        private float _mouseSensitivity = 0.5F;
        [Category("Misc")]
        public float MouseSensitivity
        {
            get => _mouseSensitivity;
            set
            {
                _mouseSensitivity = value > 0 ? value : 1;  // Ensure sensitivity is positive
            }
        }

        private int _needleThickness = 3;
        [Category("Misc.Needle")]
        public int NeedleThickness
        {
            get => _needleThickness;
            set
            {
                _needleThickness = value; Invalidate();
            }
        }

        private int _dialBorderWidth = 2;
        [Category("Misc.Dial")]
        public int DialBorderWidth
        {
            get => _dialBorderWidth;
            set
            {
                _dialBorderWidth = value; Invalidate();
            }
        }

        private int _knobBorderWidth = 2;
        [Category("Misc.Knob")]
        public int KnobBorderWidth
        {
            get => _knobBorderWidth;
            set
            {
                _knobBorderWidth = value; Invalidate();
            }
        }

        private int _dialPadding = 2;
        [Category("Misc.Dial")]
        public int DialPadding
        {
            get => _dialPadding;
            set
            {
                _dialPadding = value; Invalidate();
            }
        }

        private bool _dialBorderOnTop = false;
        [Category("Misc.Dial")]
        public bool DialBorderOnTop
        {
            get => _dialBorderOnTop;
            set
            {
                _dialBorderOnTop = value; Invalidate();
            }
        }

        private bool _dialGradient = false;
        [Category("Misc.Dial")]
        public bool DialGradient
        {
            get => _dialGradient;
            set
            {
                _dialGradient = value; Invalidate();
            }
        }


        private float _controlRotation = 0F;
        [Category("Misc")]
        public float ControlRotation
        {
            get => _controlRotation;
            set
            {
                _controlRotation = value; Invalidate();
            }
        }

        private bool _textVisible = true;
        [Category("Misc")]
        public bool TextVisible
        {
            get => _textVisible;
            set
            {
                _textVisible = value; Invalidate();
            }
        }

        private float _startAngle = 45F;
        [Category("Misc")]
        [Description("The angle the minimum value occurs at.")]
        public float StartAngle
        {
            get => _startAngle;
            set
            {
                _startAngle = value; Invalidate();
            }
        }

        private int _knobSize = 20;
        [Category("Misc.Knob")]
        public int KnobSize
        {
            get => _knobSize;
            set
            {
                _knobSize = value; Invalidate();
            }
        }

        private float _endAngle = 315F;
        [Description("The angle the maximum value occurs at.")]
        public float EndAngle
        {
            get => _endAngle;
            set
            {
                _endAngle = value; Invalidate();
            }
        }

        private int _needleLength = 10;
        [Category("Misc.Needle")]
        [Description("Length of the needle from the point it starts.")]
        public int NeedleLength
        {
            get => _needleLength;
            set
            {
                _needleLength = value; Invalidate();
            }
        }

        private int _needleOffset = 5;
        [Category("Misc.Needle")]
        [Description("Distance from center the needle starts.")]
        public int NeedleOffset
        {
            get => _needleOffset;
            set
            {
                _needleOffset = value; Invalidate();
            }
        }

        private bool _needleDot = true;
        [Category("Misc.Needle"), Description("Turns the needle in to a dot.")]
        public bool NeedleDot
        {
            get => _needleDot;
            set
            {
                _needleDot = value; Invalidate();
            }
        }

        private bool _tickEnabled = true;
        [Category("Misc.Ticks")]
        public bool TickEnabled
        {
            get => _tickEnabled;
            set
            {
                _tickEnabled = value; Invalidate();
            }
        }

        private int _tickThickness = 2;
        [Category("Misc.Ticks")]
        public int TickThickness
        {
            get => _tickThickness;
            set
            {
                _tickThickness = value; Invalidate();
            }
        }

        private int _tickCount = 10;
        [Category("Misc.Ticks")]
        public int TickCount
        {
            get => _tickCount;
            set
            {
                _tickCount = value; Invalidate();
            }
        }

        private int _tickLength = 3;
        [Category("Misc.Ticks")]
        public int TickLength
        {
            get => _tickLength;
            set
            {
                _tickLength = value; Invalidate();
            }
        }

        private bool _tickDot = true;
        [Category("Misc.Ticks")]
        public bool TickDot
        {
            get => _tickDot;
            set
            {
                _tickDot = value; Invalidate();
            }
        }

        private int _tickOffset = 15;
        [Category("Misc.Ticks")]
        public int TickOffset
        {
            get => _tickOffset;
            set
            {
                _tickOffset = value; Invalidate();
            }
        }

        private int _progressRingThickness = 3;
        [Category("Misc.Ring")]
        public int ProgressRingThickness
        {
            get => _progressRingThickness;
            set
            {
                _progressRingThickness = value; Invalidate();
            }
        }

        private float[] defaultData = { 0.00f, 0.3f, 0.55f, 0.6f, 0.75f, 0.9f, 1.0f };

        private float[] dialGradientPositions;
        [Category("Misc.Dial")]
        public float[] DialGradientPositions
        {
            get => dialGradientPositions;
            set
            {
                dialGradientPositions = value;
                Invalidate(); // Redraw the control when data changes
            }
        }



        private float[] dialGradientFactors;
        [Category("Misc.Dial")]
        public float[] DialGradientFactors
        {
            get => dialGradientFactors;
            set
            {
                dialGradientFactors = value;
                Invalidate(); // Redraw the control when data changes
            }
        }


        private int _progressRingPadding = 2;
        [Category("Misc.Ring")]
        public int ProgressRingPadding
        {
            get => _progressRingPadding;
            set
            {
                _progressRingPadding = value; Invalidate();
            }
        }


        private bool _centeredMode = false;
        [Description("Sets the knob to a positive/negative mode.")]
        [Category("Misc")]
        public bool CenteredMode
        {
            get => _centeredMode;
            set
            {
                _centeredMode = value; Invalidate();
            }
        }

        #region KNOBBACKGROUNDCOLOR
        //KNOBBACKGROUNDCOLOR
        private UIRole _knobBackgroundColorRole = UIRole.TitleBar;
        private ThemeColorShade _knobBackgroundColorTheme = ThemeColorShade.Neutral_100;
        private Color _knobBackgroundColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_100);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole KnobBackgroundColorRole { get => _knobBackgroundColorRole; set { _knobBackgroundColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade KnobBackgroundColorTheme
        {
            get => _knobBackgroundColorTheme; set
            {
                _knobBackgroundColorTheme = value;
                if (!_ignoreTheme) { KnobBackgroundColor = ThemeManager.Instance.GetThemeColorShade(_knobBackgroundColorTheme); }
            }
        }

        [Category("Color"), Description("Color.")]
        public Color KnobBackgroundColor { get => _knobBackgroundColor; set { _knobBackgroundColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region KNOBBORDERCOLOR
        //KNOBBORDERCOLOR
        private UIRole _knobBorderColorRole = UIRole.TitleBar;
        private ThemeColorShade _knobBorderColorTheme = ThemeColorShade.Neutral_400;
        private Color _knobBorderColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_400);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole KnobBorderColorRole { get => _knobBorderColorRole; set { _knobBorderColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade KnobBorderColorTheme
        {
            get => _knobBorderColorTheme; set
            {
                _knobBorderColorTheme = value;
                if (!_ignoreTheme) { KnobBorderColor = ThemeManager.Instance.GetThemeColorShade(_knobBorderColorTheme); }
            }
        }

        [Category("Color"), Description("Color.")]
        public Color KnobBorderColor { get => _knobBorderColor; set { _knobBorderColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region NEEDLECOLOR
        //NEEDLECOLOR
        private UIRole _needleColorRole = UIRole.TitleBar;
        private ThemeColorShade _needleColorTheme = ThemeColorShade.Primary_500;
        private Color _needleColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole NeedleColorRole { get => _needleColorRole; set { _needleColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade NeedleColorTheme
        {
            get => _needleColorTheme; set
            {
                _needleColorTheme = value;
                if (!_ignoreTheme) { NeedleColor = ThemeManager.Instance.GetThemeColorShade(_needleColorTheme); }
            }
        }

        [Category("Color"), Description("Color.")]
        public Color NeedleColor { get => _needleColor; set { _needleColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region DIALFACECOLOR
        //DIALFACECOLOR
        private UIRole _dialFaceColorRole = UIRole.TitleBar;
        private ThemeColorShade _dialFaceColorTheme = ThemeColorShade.Neutral_300;
        private Color _dialFaceColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_300);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole DialFaceColorRole { get => _dialFaceColorRole; set { _dialFaceColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade DialFaceColorTheme
        {
            get => _dialFaceColorTheme; set
            {
                _dialFaceColorTheme = value;
                if (!_ignoreTheme) { DialFaceColor = ThemeManager.Instance.GetThemeColorShade(_dialFaceColorTheme); }
            }
        }

        [Category("Color"), Description("Color.")]
        public Color DialFaceColor { get => _dialFaceColor; set { _dialFaceColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region DIALBORDERCOLOR
        //DIALBORDERCOLOR
        private UIRole _dialBorderColorRole = UIRole.TitleBar;
        private ThemeColorShade _dialBorderColorTheme = ThemeColorShade.Primary_700;
        private Color _dialBorderColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_700);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole DialBorderColorRole { get => _dialBorderColorRole; set { _dialBorderColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade DialBorderColorTheme
        {
            get => _dialBorderColorTheme; set
            {
                _dialBorderColorTheme = value;
                if (!_ignoreTheme) { DialBorderColor = ThemeManager.Instance.GetThemeColorShade(_dialBorderColorTheme); }
            }
        }

        [Category("Color"), Description("Color.")]
        public Color DialBorderColor { get => _dialBorderColor; set { _dialBorderColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region TEXTCOLOR
        //TEXTCOLOR
        private UIRole _textColorRole = UIRole.TitleBar;
        private ThemeColorShade _textColorTheme = ThemeColorShade.Neutral_800;
        private Color _textColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_800);

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

        [Category("Color"), Description("Color.")]
        public Color TextColor { get => _textColor; set { _textColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region TICKCOLOR
        //TICKCOLOR
        private UIRole _tickColorRole = UIRole.TitleBar;
        private ThemeColorShade _tickColorTheme = ThemeColorShade.Neutral_600;
        private Color _tickColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_600);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole TickColorRole { get => _tickColorRole; set { _tickColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade TickColorTheme
        {
            get => _tickColorTheme; set
            {
                _tickColorTheme = value;
                if (!_ignoreTheme) { TickColor = ThemeManager.Instance.GetThemeColorShade(_tickColorTheme); }
            }
        }

        [Category("Color"), Description("Color.")]
        public Color TickColor { get => _tickColor; set { _tickColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region PROGRESSRINGCOLOR
        //PROGRESSRINGCOLOR
        private UIRole _progressRingColorRole = UIRole.EmphasizedBorders;
        private ThemeColorShade _progressRingColorTheme = ThemeColorShade.Primary_500;
        private Color _progressRingColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole ProgressRingColorRole { get => _progressRingColorRole; set { _progressRingColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade ProgressRingColorTheme
        {
            get => _progressRingColorTheme; set
            {
                _progressRingColorTheme = value;
                if (!_ignoreTheme) { ProgressRingColor = ThemeManager.Instance.GetThemeColorShade(_progressRingColorTheme); }
            }
        }

        [Category("Color"), Description("Color.")]
        public Color ProgressRingColor { get => _progressRingColor; set { _progressRingColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region PROGRESSRINGTRACKCOLOR
        //PROGRESSRINGTRACKCOLOR
        private UIRole _progressRingTrackColorRole = UIRole.EmphasizedBorders;
        private ThemeColorShade _progressRingTrackColorTheme = ThemeColorShade.Neutral_800;
        private Color _progressRingTrackColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_800);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole ProgressRingTrackColorRole { get => _progressRingTrackColorRole; set { _progressRingTrackColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade ProgressRingTrackColorTheme
        {
            get => _progressRingTrackColorTheme; set
            {
                _progressRingTrackColorTheme = value;
                if (!_ignoreTheme) { ProgressRingTrackColor = ThemeManager.Instance.GetThemeColorShade(_progressRingTrackColorTheme); }
            }
        }

        [Category("Color"), Description("Color.")]
        public Color ProgressRingTrackColor { get => _progressRingTrackColor; set { _progressRingTrackColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion

        #region BACKCOLOR
        //BACKCOLOR
        private UIRole _backColorRole = UIRole.MainBackground;
        private ThemeColorShade _backColorTheme = ThemeColorShade.Neutral_100;
        private Color _backColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_100);

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

        [Category("Color"), Description("Color.")]
        public Color BackColor { get => _backColor; set { _backColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////BackColor = ThemeManager.Instance.GetThemeColorShadeOffset(BackColorTheme);
        #endregion


        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                DialFaceColor = ThemeManager.Instance.GetThemeColorShadeOffset(DialFaceColorTheme);
                NeedleColor = ThemeManager.Instance.GetThemeColorShadeOffset(NeedleColorTheme);
                KnobBackgroundColor = ThemeManager.Instance.GetThemeColorShadeOffset(KnobBackgroundColorTheme);
                DialBorderColor = ThemeManager.Instance.GetThemeColorShadeOffset(DialBorderColorTheme);
                KnobBorderColor = ThemeManager.Instance.GetThemeColorShadeOffset(KnobBorderColorTheme);
                TextColor = ThemeManager.Instance.GetThemeColorShadeOffset(TextColorTheme);
                TickColor = ThemeManager.Instance.GetThemeColorShadeOffset(TickColorTheme);
                ProgressRingColor = ThemeManager.Instance.GetThemeColorShadeOffset(ProgressRingColorTheme);
                ProgressRingTrackColor = ThemeManager.Instance.GetThemeColorShadeOffset(ProgressRingTrackColorTheme);
            }
        }


        public ErrataTurnKnob()
        {
            dialGradientPositions = defaultData;
            Array.Reverse(defaultData);
            dialGradientFactors = defaultData;

            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            _value = 50; // Default value (50% of the range)
            this.Cursor = Cursors.Hand; // Set a hand cursor when hovering over the knob
            this.BackColor = Color.White; // Make background transparent to allow custom styling
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;




            // DIAL FACE

            //If an image is loaded for the dial...
            if (_dialTexture != null )
            {
                using (TextureBrush dialText = new TextureBrush(_dialTexture))
                {
                    g.FillEllipse(dialText, _dialPadding, _dialPadding, this.Width - 2 * _dialPadding, this.Height - 2 * _dialPadding);
                }
            }           
            else if (_dialGradient)//GRADIENT BRUSH
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(_dialPadding, _dialPadding, this.Width - 2 * _dialPadding, this.Height - 2 * _dialPadding);
                    using (PathGradientBrush brush = new PathGradientBrush(path))
                    {
                        // Set center and surrounding colors
                        brush.CenterColor = _dialFaceColor;  // Inner color (bright center)
                        brush.SurroundColors = new Color[] { _dialBorderColor }; // Outer color
                        // Create a smooth blend transition
                        Blend blend = new Blend()
                        {
                            Factors = dialGradientFactors,
                            Positions = dialGradientPositions
                        };                       
                        brush.Blend = blend;
                        // Fill the ellipse with the radial gradient
                        g.FillEllipse(brush, _dialPadding, _dialPadding, this.Width - 2 * _dialPadding, this.Height - 2 * _dialPadding);
                    }
                }
            }
            else //REGULAR FLAT DIAL
            {
                using (Brush backBrush = new SolidBrush(_dialFaceColor))
                {
                    g.FillEllipse(backBrush, _dialPadding, _dialPadding, this.Width - 2 * _dialPadding, this.Height - 2 * _dialPadding);
                }

                if (_dialBorderWidth > 0)
                {
                    using (Pen borderPen = new Pen(_dialBorderColor, _dialBorderWidth))
                    {
                        g.DrawEllipse(borderPen, _dialPadding, _dialPadding, this.Width - 2 * _dialPadding, this.Height - 2 * _dialPadding);
                    }
                }
            }
            

            


            // PROGRESS RING (FULL TRACK)
            float progressThickness = _progressRingThickness;
            float progressPadding = _progressRingPadding;
            float progressSize = this.Width - 2 * (progressPadding + _dialPadding);

            using (Pen trackPen = new Pen(_progressRingTrackColor, progressThickness)) // Background track color
            {
                trackPen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);
                g.DrawArc(trackPen, progressPadding + _dialPadding, progressPadding + _dialPadding,
                          progressSize, progressSize, 0, 360); // Full circle track
            }



            // PROGRESSIVE RING
            float sweepAngle;
            float halfRange = (_endAngle - _startAngle) / 2f; // Half of the full range

            if (_centeredMode)
            {
                float normalizedValue = (Value - 50) / 50f; // Convert value range (0-100) to (-1 to 1)
                sweepAngle = halfRange * normalizedValue;
            }
            else
            {
                sweepAngle = (_endAngle - _startAngle) * (Value / 100f);
            }

            using (Pen progressPen = new Pen(_progressRingColor, progressThickness))
            {
                progressPen.SetLineCap(LineCap.Round, LineCap.Round, DashCap.Round);
                float arcStartAngle = _startAngle + _controlRotation - 90;
                if (_centeredMode)
                    arcStartAngle = _startAngle + halfRange + _controlRotation - 90;

                g.DrawArc(progressPen, progressPadding + _dialPadding, progressPadding + _dialPadding,
                          progressSize, progressSize, arcStartAngle, sweepAngle);
            }


            // Draw tick marks (dashes)
            if (_tickEnabled)
            {
                DrawTicks(g);
            }



            //NEEDLE
            float angle = _startAngle + (Value / 100f) * (_endAngle - _startAngle);

            g.TranslateTransform(this.Width / 2, this.Height / 2); // Move origin to the center of the knob
            
            g.RotateTransform(angle + _controlRotation); // Rotate the drawing context by the calculated angle

            if (_needleDot)
            {
                using (Brush dotBrush = new SolidBrush(_needleColor))
                {
                    int dotSize = _needleThickness * 2;  // Dot size based on thickness
                    float dotX = 0 - (dotSize / 2);
                    float dotY = -(_needleOffset + _needleLength) - (dotSize / 2);
                    g.FillEllipse(dotBrush, dotX, dotY, dotSize, dotSize);
                }
            }
            else
            {
                using (Pen needlePen = new Pen(_needleColor, _needleThickness))
                {
                    float start = _needleOffset;  // Offset from the center
                    float end = _needleOffset + _needleLength;  // Total length from center
                    g.DrawLine(needlePen, 0, -start, 0, -end);
                }
            }

           
            g.ResetTransform(); // Reset the transform




            // KNOB [CENTER OF DIAL]
            using (Brush centerBrush = new SolidBrush(_knobBackgroundColor))
            {
                g.FillEllipse(centerBrush, this.Width / 2 - (_knobSize / 2), this.Height / 2 - (_knobSize / 2), _knobSize, _knobSize);
            }


            //KNOB BORDER
            if (_knobBorderWidth > 0)
            {
                using (Pen borderPen = new Pen(_knobBorderColor, _knobBorderWidth))
                {
                    g.DrawEllipse(borderPen, this.Width / 2 - (_knobSize / 2), this.Height / 2 - (_knobSize / 2), _knobSize, _knobSize);
                }
            }
            



            //TEXT
            if (_textVisible)
            {
                // Draw value text in the center
                using (Font font = new Font("Segoe UI", 8, FontStyle.Regular))
                using (Brush textBrush = new SolidBrush(_textColor))
                {
                    g.DrawString($"{Value:F0}", font, textBrush, this.Width / 2 - 10, this.Height / 2 - 8);
                }
            }

            


            if (_dialBorderWidth > 0 && _dialBorderOnTop)
            {
                using (Pen borderPen = new Pen(_dialBorderColor, _dialBorderWidth))
                {
                    g.DrawEllipse(borderPen, _dialPadding, _dialPadding, this.Width - 2 * _dialPadding, this.Height - 2 * _dialPadding);
                }
            }

        }


        private void DrawTicks(Graphics g)
        {

            int centerX = this.Width / 2;
            int centerY = this.Height / 2;
            float radius = (this.Width / 2) - _tickOffset; // Outer edge radius

            using (Pen tickPen = new Pen(_tickColor, _tickThickness))
            using (Brush tickBrush = new SolidBrush(_tickColor))
            {
                float angleStep = (_endAngle - _startAngle) / (_tickCount - 1); // Step size per tick

                for (int i = 0; i < _tickCount; i++)
                {
                    float angle = _startAngle + (i * angleStep); // Angle in degrees
                    float radians = (angle - 90 + _controlRotation) * (float)(Math.PI / 180.0); // Convert to radians & rotate -90° (GDI+ fix)

                    // Calculate start and end points for the tick
                    float xStart = centerX + (radius - _tickLength) * (float)Math.Cos(radians);
                    float yStart = centerY + (radius - _tickLength) * (float)Math.Sin(radians);
                    float xEnd = centerX + radius * (float)Math.Cos(radians);
                    float yEnd = centerY + radius * (float)Math.Sin(radians);

                    if (_tickDot)
                    {
                        int dotSize = _tickThickness * 2;  // Dot size based on thickness

                        // Draw dot centered at (xEnd, yEnd)
                        g.FillEllipse(tickBrush, xEnd - (dotSize / 2), yEnd - (dotSize / 2), dotSize, dotSize);
                    }
                    else
                    {
                        g.DrawLine(tickPen, xStart, yStart, xEnd, yEnd);
                    }
                }
            }
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _isMouseDown = true;
            _lastMousePos = e.Location; // Capture initial mouse position
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_isMouseDown)
            {
                // Calculate the angle of movement
                Point delta = new Point(e.X - _lastMousePos.X, e.Y - _lastMousePos.Y);
                float angleChange = delta.Y / (float)this.Height * 360; // Based on mouse movement (up/down)
                Value -= (angleChange / 2) * _mouseSensitivity; // Adjust value based on angle change (divide by 2 for slower adjustment)
                _lastMousePos = e.Location; // Update the last mouse position
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isMouseDown = false; // Stop rotation when mouse button is released
        }
    }
}