using ErrataUI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataFretboard : Control
    {
        public delegate void FrettedNoteEventHandler(object sender, EventArgs e);

        [Category("Custom Events")] // Makes it appear under a category in the Designer
        [Description("Fires when a string is fretted.")] // Tooltip in the Properties window
        public event FrettedNoteEventHandler FrettedNote;

        protected virtual void OnFrettedNote(EventArgs e)
        {
            FrettedNote?.Invoke(this, e);
        }

        // Override the OnClick method to include custom event triggering
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            OnFrettedNote(EventArgs.Empty); // Fire the custom event when button is clicked
        }


        private bool isMousePressed = false;  // Flag to track if mouse is pressed

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


        private Dictionary<(int, int), bool> _pressedFrets = new Dictionary<(int, int), bool>(); // Track pressed frets



        private Image _woodTexture;
        [Category("Textures")]
        [Description("The texture for the fretboard.")]
        [Editor(typeof(System.Drawing.Design.ImageEditor), typeof(UITypeEditor))] // This makes it editable in the designer
        public Image WoodTexture
        {
            get { return _woodTexture; }
            set
            {
                _woodTexture = value;
                Invalidate(); // Redraw the control when the image is changed
            }
        }



        private int _fretTextureWidth = 5;
        [Category("Textures")]
        public int FretTextureWidth
        {
            get => _fretTextureWidth;
            set
            {
                _fretTextureWidth = value; Invalidate();
            }
        }

        private Image _fretTexture;
        [Category("Textures")]
        [Description("The texture for the metal frets.")]
        [Editor(typeof(System.Drawing.Design.ImageEditor), typeof(UITypeEditor))] // This makes it editable in the designer
        public Image FretTexture
        {
            get { return _fretTexture; }
            set
            {
                _fretTexture = value;
                Invalidate(); // Redraw the control when the image is changed
            }
        }

        private Image _stringTexture;
        [Category("Textures")]
        [Description("The texture for the strings.")]
        [Editor(typeof(System.Drawing.Design.ImageEditor), typeof(UITypeEditor))] // This makes it editable in the designer
        public Image StringTexture
        {
            get { return _stringTexture; }
            set
            {
                _stringTexture = value;
                Invalidate(); // Redraw the control when the image is changed
            }
        }

        private Image _headTexture;
        [Category("Textures")]
        [Description("The texture for the headstock.")]
        [Editor(typeof(System.Drawing.Design.ImageEditor), typeof(UITypeEditor))] // This makes it editable in the designer
        public Image HeadTexture
        {
            get { return _headTexture; }
            set
            {
                _headTexture = value;
                Invalidate(); // Redraw the control when the image is changed
            }
        }



        private BindingList<string> _tuning = new BindingList<string> { "E", "B", "G", "D", "A", "E" };
        // Row Headers collection exposed to designer
        [Browsable(true)]
        [Category("Strings")]
        [Description("String tuning")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BindingList<string> Tuning
        {
            get { return _tuning; }
            set { _tuning = value; Invalidate(); }
        }


        private int _numberOfStrings = 6;
        [Category("Strings"), Description("Increasing the strings beyond the tuning limit will cause a crash.")]
        public int NumberOfStrings
        {
            get => _numberOfStrings;
            set
            {
                if (_tuning.Count < value) { _numberOfStrings = _tuning.Count; Invalidate(); return; }
                _numberOfStrings = value; Invalidate();
            }
        }

        private int _numberOfFrets = 12;
        [Category("Misc")]
        public int NumberOfFrets
        {
            get => _numberOfFrets;
            set
            {
                _numberOfFrets = value; Invalidate();
            }
        }

        private bool _clearDragMarkers = true;
        [Category("Misc")]
        public bool ClearDragMarkers
        {
            get => _clearDragMarkers;
            set
            {
                _clearDragMarkers = value; Invalidate();
            }
        }

        private bool _fretInlays = true;
        [Category("Misc")]
        public bool FretInlays
        {
            get => _fretInlays;
            set
            {
                _fretInlays = value; Invalidate();
            }
        }

        private int _fretInlayAlpha = 128;
        [Category("Misc")]
        public int FretInlayAlpha
        {
            get => _fretInlayAlpha;
            set
            {
                _fretInlayAlpha = value; Invalidate();
            }
        }

        private int _fretInlayDiameter = 10;
        [Category("Misc")]
        public int FretInlayDiameter
        {
            get => _fretInlayDiameter;
            set
            {
                _fretInlayDiameter = value; Invalidate();
            }
        }


        private bool _stringGauges = true;
        [Category("Strings")]
        public bool StringGauges
        {
            get => _stringGauges;
            set
            {
                _stringGauges = value; Invalidate();
            }
        }

        private float _stringGaugeIncrement = 0.4f;
        [Category("Strings")]
        public float StringGaugeIncrement
        {
            get => _stringGaugeIncrement;
            set
            {
                _stringGaugeIncrement = value; Invalidate();
            }
        }

        private int _stringGaugeInitial = 2;
        [Category("Strings")]
        public int StringGaugeInitial
        {
            get => _stringGaugeInitial;
            set
            {
                _stringGaugeInitial = value; Invalidate();
            }
        }

        private int _stringTextPadding = 10;
        [Category("Strings")]
        public int StringTextPadding
        {
            get => _stringTextPadding;
            set
            {
                _stringTextPadding = value; Invalidate();
            }
        }

        private int _borderWidth = 2;
        [Category("Misc")]
        public int BorderWidth
        {
            get => _borderWidth;
            set
            {
                _borderWidth = value;
            }
        }


        #region FRETCOLOR
        //FRETCOLOR
        private UIRole _fretColorRole = UIRole.TitleBar;
        private ThemeColorShade _fretColorTheme = ThemeColorShade.Neutral_200;
        private Color _fretColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_200);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole FretColorRole { get => _fretColorRole; set { _fretColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade FretColorTheme
        {
            get => _fretColorTheme; set
            {
                _fretColorTheme = value;
                if (!_ignoreTheme) { FretColor = ThemeManager.Instance.GetThemeColorShade(_fretColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color FretColor { get => _fretColor; set { _fretColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region STRINGCOLOR
        //STRINGCOLOR
        private UIRole _stringColorRole = UIRole.TitleBar;
        private ThemeColorShade _stringColorTheme = ThemeColorShade.Primary_100;
        private Color _stringColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_100);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole StringColorRole { get => _stringColorRole; set { _stringColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade StringColorTheme
        {
            get => _stringColorTheme; set
            {
                _stringColorTheme = value;
                if (!_ignoreTheme) { StringColor = ThemeManager.Instance.GetThemeColorShade(_stringColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color StringColor { get => _stringColor; set { _stringColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region NECKCOLOR
        //NECKCOLOR
        private UIRole _neckColorRole = UIRole.TitleBar;
        private ThemeColorShade _neckColorTheme = ThemeColorShade.SemanticB_800;
        private Color _neckColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.SemanticB_800);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole NeckColorRole { get => _neckColorRole; set { _neckColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade NeckColorTheme
        {
            get => _neckColorTheme; set
            {
                _neckColorTheme = value;
                if (!_ignoreTheme) { NeckColor = ThemeManager.Instance.GetThemeColorShade(_neckColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color NeckColor { get => _neckColor; set { _neckColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region FORECOLOR
        //FORECOLOR
        private UIRole _foreColorRole = UIRole.TitleBar;
        private ThemeColorShade _foreColorTheme = ThemeColorShade.Neutral_50;
        private Color _foreColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_50);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole ForeColorRole { get => _foreColorRole; set { _foreColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade ForeColorTheme
        {
            get => _foreColorTheme; set
            {
                _foreColorTheme = value;
                if (!_ignoreTheme) { ForeColor = ThemeManager.Instance.GetThemeColorShade(_foreColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color ForeColor { get => _foreColor; set { _foreColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region MARKERCOLOR
        //MARKERCOLOR
        private UIRole _markerColorRole = UIRole.TitleBar;
        private ThemeColorShade _markerColorTheme = ThemeColorShade.SemanticC_500;
        private Color _markerColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.SemanticC_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole MarkerColorRole { get => _markerColorRole; set { _markerColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade MarkerColorTheme
        {
            get => _markerColorTheme; set
            {
                _markerColorTheme = value;
                if (!_ignoreTheme) { MarkerColor = ThemeManager.Instance.GetThemeColorShade(_markerColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color MarkerColor { get => _markerColor; set { _markerColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region HEADSTOCKCOLOR
        //HEADSTOCKCOLOR
        private UIRole _headStockColorRole = UIRole.TitleBar;
        private ThemeColorShade _headStockColorTheme = ThemeColorShade.Neutral_800;
        private Color _headStockColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_800);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole HeadStockColorRole { get => _headStockColorRole; set { _headStockColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade HeadStockColorTheme
        {
            get => _headStockColorTheme; set
            {
                _headStockColorTheme = value;
                if (!_ignoreTheme) { HeadStockColor = ThemeManager.Instance.GetThemeColorShade(_headStockColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color HeadStockColor { get => _headStockColor; set { _headStockColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region BORDERCOLOR
        //BORDERCOLOR
        private UIRole _borderColorRole = UIRole.TitleBar;
        private ThemeColorShade _borderColorTheme = ThemeColorShade.Neutral_700;
        private Color _borderColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_700);

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
        ///
        #endregion
        #region FRETINLAYCOLOR
        //FRETINLAYCOLOR
        private UIRole _fretInlayColorRole = UIRole.TitleBar;
        private ThemeColorShade _fretInlayColorTheme = ThemeColorShade.Neutral_100;
        private Color _fretInlayColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_100);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole FretInlayColorRole { get => _fretInlayColorRole; set { _fretInlayColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade FretInlayColorTheme
        {
            get => _fretInlayColorTheme; set
            {
                _fretInlayColorTheme = value;
                if (!_ignoreTheme) { FretInlayColor = ThemeManager.Instance.GetThemeColorShade(_fretInlayColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color FretInlayColor { get => _fretInlayColor; set { _fretInlayColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion

        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                FretColor = ThemeManager.Instance.GetThemeColorShadeOffset(FretColorTheme);
                StringColor = ThemeManager.Instance.GetThemeColorShadeOffset(StringColorTheme);
                NeckColor = ThemeManager.Instance.GetThemeColorShadeOffset(NeckColorTheme);
                ForeColor = ThemeManager.Instance.GetThemeColorShadeOffset(ForeColorTheme);
                MarkerColor = ThemeManager.Instance.GetThemeColorShadeOffset(MarkerColorTheme);
                HeadStockColor = ThemeManager.Instance.GetThemeColorShadeOffset(HeadStockColorTheme);
                BorderColor = ThemeManager.Instance.GetThemeColorShadeOffset(BorderColorTheme);
                FretInlayColor = ThemeManager.Instance.GetThemeColorShadeOffset(FretInlayColorTheme);
            }
        }

        public ErrataFretboard()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            DoubleBuffered = true;
            this.TabStop = true; // Allow focus for key events
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            int width = Width;
            int height = Height;

            int fretWidth = width / (_numberOfFrets + 1);  // +1 for open string area
            int stringSpacing = height / (_numberOfStrings + 1);

            // Draw fretboard background
            if (_woodTexture != null) 
            {
                using (TextureBrush woodText = new TextureBrush(WoodTexture))
                {
                    g.FillRectangle(woodText,0,0,width,height);
                }
            }
            else
            {
                using (Brush woodBrush = new SolidBrush(_neckColor)) //Color.SaddleBrown
                {
                    g.FillRectangle(woodBrush, 0, 0, width, height);
                }
            }



            if (_headTexture != null) 
            {
                using (TextureBrush headText = new TextureBrush(HeadTexture))
                {
                    g.FillRectangle(headText, 0, 0, fretWidth, height);
                }
            }
            else
            {
                using (Brush headStock = new SolidBrush(_headStockColor))
                {
                    g.FillRectangle(headStock, 0, 0, fretWidth, height);
                }
            }
            

            using (Pen fretPen = new Pen(_fretColor, 2))
            using (Pen stringPen = new Pen(_stringColor,_stringGaugeInitial))
            using (Font font = new Font("Segoe UI Semibold", 10, FontStyle.Regular))
            using (Brush textBrush = new SolidBrush(_foreColor))
            {
                // Draw frets
                for (int i = 1; i <= _numberOfFrets; i++)
                {
                    int x = i * fretWidth;

                    if (_fretInlays)
                    {
                        // Check if the fret is one of the positions for fret inlays (e.g., 3, 5, 7, 9, 12, etc.)
                        if (i - 1 == 3 || i - 1 == 5 || i - 1 == 7 || i - 1 == 9 || i - 1 == 15 || i - 1 == 17)
                        {
                            int inlayX = x - fretWidth / 2; // Adjust position for the circle in the middle of the fret
                            int inlayY = height / 2; // You can adjust this based on the string count and desired height of inlays
                            int inlayDiameter = _fretInlayDiameter; // Set the diameter of the inlay circle

                            using (Brush inlayBrush = new SolidBrush(Color.FromArgb(_fretInlayAlpha, _fretInlayColor))) // Define the color for the inlay
                            {
                                g.FillEllipse(inlayBrush, inlayX - inlayDiameter / 2, inlayY - inlayDiameter / 2, inlayDiameter, inlayDiameter);
                            }
                        }

                        if (i - 1 == 12)
                        {
                            int doubleDotX1 = x - fretWidth / 2; // Adjust left circle position
                            int doubleDotY1 = height / 2 - (height / 5);
                            int doubleDotY2 = height / 2 + (height / 5);
                            int dotDiameter = _fretInlayDiameter; // Set the diameter of each dot

                            using (Brush doubleDotBrush = new SolidBrush(Color.FromArgb(_fretInlayAlpha, _fretInlayColor))) // Use the same inlay color for double dots
                            {
                                // Draw the left circle (dot)
                                g.FillEllipse(doubleDotBrush, doubleDotX1 - dotDiameter / 2, doubleDotY1 - dotDiameter / 2, dotDiameter, dotDiameter);
                                // Draw the right circle (dot)
                                g.FillEllipse(doubleDotBrush, doubleDotX1 - dotDiameter / 2, doubleDotY2 - dotDiameter / 2, dotDiameter, dotDiameter);
                            }
                        }
                    }
                    



                    if (_fretTexture != null)
                    {
                        using (TextureBrush fretText = new TextureBrush(FretTexture))
                        {
                            Point start = new Point(x, 0);
                            Point end = new Point(x, height);
                            Rectangle fretRect = ThemeManager.Instance.ConvertLineToRectangle(start,end, _fretTextureWidth);
                            g.FillRectangle(fretText, fretRect);
                        }
                    }
                    else
                    { 
                        g.DrawLine(fretPen, x, 0, x, height);
                    }
                    
                }

                // Draw strings and labels
                for (int i = 0; i < _numberOfStrings; i++)
                {
                    int y = (i + 1) * stringSpacing;

                    if (_stringTexture != null)
                    {
                        using (TextureBrush stringText = new TextureBrush(StringTexture))
                        {
                            Point start = new Point(0 + fretWidth, y);
                            Point end = new Point(width, y);
                            Rectangle stringRect = ThemeManager.Instance.ConvertLineToRectangle(start, end, (int)stringPen.Width);
                            g.FillRectangle(stringText, stringRect);
                        }
                    }
                    else
                    {
                        g.DrawLine(stringPen, 0 + fretWidth, y, width, y); // Draw string
                    }

                    
                    if (_stringGauges) { stringPen.Width += _stringGaugeIncrement; }



                    // Draw tuning label
                    g.DrawString(_tuning[i], font, textBrush, new PointF(5 + _stringTextPadding, y - 10));
                }

                // Define the color and width of the border
                Color borderColor = _borderColor;
                int borderWidth = _borderWidth;

                // Create a Pen object with the desired color and width
                using (Pen borderPen = new Pen(borderColor, borderWidth))
                {
                    // Draw the border around the control
                    e.Graphics.DrawRectangle(borderPen, 1, 1, Width - 2, Height - 2);
                }




                // Draw pressed frets
                foreach (var fret in _pressedFrets)
                {
                    int stringIndex = fret.Key.Item1;
                    int fretIndex = fret.Key.Item2;

                    if (fretIndex > 0) // Don't draw for open strings
                    {
                        int x = fretIndex * fretWidth - fretWidth / 2;
                        int y = (stringIndex + 1) * stringSpacing;

                        using (Brush pressBrush = new SolidBrush(_markerColor))
                        {
                            g.FillEllipse(pressBrush, x - 5, y - 5, 10, 10);
                        }
                    }
                }
            }
        }

        

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            isMousePressed = true;  // Start dragging
            UpdatePressedFret(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (isMousePressed)  // Only update when dragging
            {
                UpdatePressedFret(e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isMousePressed = false;  // Stop dragging
            _pressedFrets.Clear();
            Invalidate();
        }

        private void UpdatePressedFret(MouseEventArgs e)
        {
            int width = Width;
            int height = Height;

            int fretWidth = width / (_numberOfFrets + 1);  // +1 accounts for open string area
            int stringSpacing = height / (_numberOfStrings + 1);

            // Ensure click is within valid bounds (vertical)
            if (e.Y < stringSpacing / 2 || e.Y > height - stringSpacing / 2)
                return;

            // Define bounds for each fret and string
            var bounds = new Rectangle[_numberOfStrings, _numberOfFrets];

            for (int i = 0; i < _numberOfStrings; i++)
            {
                for (int j = 1; j <= _numberOfFrets; j++)  // Starting from 1 to _numberOfFrets
                {
                    int fretX = j * fretWidth;
                    int stringY = i * stringSpacing + stringSpacing / 2;  // Adjust for centering

                    // Define the area for the current fret/string
                    bounds[i, j - 1] = new Rectangle(fretX, stringY, fretWidth, stringSpacing);
                }
            }

            // Check if the mouse click is within any of the defined bounds
            for (int i = 0; i < _numberOfStrings; i++)
            {
                for (int j = 0; j < _numberOfFrets; j++)
                {
                    if (bounds[i, j].Contains(e.X, e.Y))
                    {
                        // Update the pressed fret while dragging

                        if (_clearDragMarkers) { _pressedFrets.Clear(); }

                        OnFretPressed(i, j + 2);
                        _pressedFrets[(i, j + 2)] = true;  // Add 2 to map to actual fret number
                        Invalidate();
                        return;
                    }
                }
            }
        }


        // Step 1: Define custom EventArgs to hold fret and string info
        public class FretPressedEventArgs : EventArgs
        {
            public int StringIndex { get; }
            public int Fret { get; }

            public FretPressedEventArgs(int stringIndex, int fret)
            {
                StringIndex = stringIndex;
                Fret = fret;
            }
        }

        // Step 2: Declare delegate
        public delegate void FretPressedEventHandler(object sender, FretPressedEventArgs e);

        // Step 3: Define the event
        [Category("Custom Events")] // Group in Properties window
        [Description("Fires when a new fret is pressed.")] // Tooltip in the Designer
        public event FretPressedEventHandler FretPressed;

        // Step 4: Raise the event when a new fret is pressed
        protected virtual void OnFretPressed(int stringIndex, int fret)
        {
            FretPressed?.Invoke(this, new FretPressedEventArgs(stringIndex, fret));
        }



    }
}