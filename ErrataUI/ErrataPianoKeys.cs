using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataPianoKeys : Control
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

        [Category("Misc"), Description("Gets the total number of keys in the control.")]
        public int KeyCount => _keys.Count;


        private List<PianoKey> _keys = new List<PianoKey>();
        private Dictionary<Keys, string> _keyMapping;
        public event EventHandler<string> KeyPressed;
        public event EventHandler<string> KeyReleased;

        private bool _isMouseDown = false;

        private bool _keyPressColorGradient = true;
        [Category("Misc")]
        public bool KeyPressColorGradient
        {
            get => _keyPressColorGradient;
            set
            {
                _keyPressColorGradient = value; Invalidate();
            }
        }

        private List<Color> _keyPressColors;
        public List<Color> KeyPressColors
        {
            get => _keyPressColors;
            set
            {
                _keyPressColors = value;
                Invalidate();
            }
        }

        private int _octaves = 2;
        [Category("Misc")]
        public int Octaves
        {
            get => _octaves;
            set
            {
                _octaves = value; GenerateKeys(); Invalidate();
            }
        }

        private int _whiteKeyHeightTrim = 2;
        [Category("Misc")]
        public int WhiteKeyHeightTrim
        {
            get => _whiteKeyHeightTrim;
            set
            {
                _whiteKeyHeightTrim = value; Invalidate();
            }
        }

        #region WHITEKEYCOLOR
        //WHITEKEYCOLOR
        private UIRole _whiteKeyColorRole = UIRole.TitleBar;
        private ThemeColorShade _whiteKeyColorTheme = ThemeColorShade.Neutral_50;
        private Color _whiteKeyColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_50);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole WhiteKeyColorRole { get => _whiteKeyColorRole; set { _whiteKeyColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade WhiteKeyColorTheme
        {
            get => _whiteKeyColorTheme; set
            {
                _whiteKeyColorTheme = value;
                if (!_ignoreTheme) { WhiteKeyColor = ThemeManager.Instance.GetThemeColorShade(_whiteKeyColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color WhiteKeyColor { get => _whiteKeyColor; set { _whiteKeyColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region BLACKKEYCOLOR
        //BLACKKEYCOLOR
        private UIRole _blackKeyColorRole = UIRole.TitleBar;
        private ThemeColorShade _blackKeyColorTheme = ThemeColorShade.Neutral_1000;
        private Color _blackKeyColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_1000);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole BlackKeyColorRole { get => _blackKeyColorRole; set { _blackKeyColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade BlackKeyColorTheme
        {
            get => _blackKeyColorTheme; set
            {
                _blackKeyColorTheme = value;
                if (!_ignoreTheme) { BlackKeyColor = ThemeManager.Instance.GetThemeColorShade(_blackKeyColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color BlackKeyColor { get => _blackKeyColor; set { _blackKeyColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region PRESSEDKEYCOLOR
        //PRESSEDKEYCOLOR
        private UIRole _pressedKeyColorRole = UIRole.TitleBar;
        private ThemeColorShade _pressedKeyColorTheme = ThemeColorShade.Primary_500;
        private Color _pressedKeyColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole PressedKeyColorRole { get => _pressedKeyColorRole; set { _pressedKeyColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade PressedKeyColorTheme
        {
            get => _pressedKeyColorTheme; set
            {
                _pressedKeyColorTheme = value;
                if (!_ignoreTheme) { PressedKeyColor = ThemeManager.Instance.GetThemeColorShade(_pressedKeyColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color PressedKeyColor { get => _pressedKeyColor; set { _pressedKeyColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region KEYPRESSGRADIENTCOLORLOW
        //KEYPRESSGRADIENTCOLORLOW
        private UIRole _keyPressGradientColorLowRole = UIRole.MainBackground;
        private ThemeColorShade _keyPressGradientColorLowTheme = ThemeColorShade.Primary_500;
        private Color _keyPressGradientColorLow = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole KeyPressGradientColorLowRole { get => _keyPressGradientColorLowRole; set { _keyPressGradientColorLowRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade KeyPressGradientColorLowTheme
        {
            get => _keyPressGradientColorLowTheme; set
            {
                _keyPressGradientColorLowTheme = value;
                if (!_ignoreTheme) { KeyPressGradientColorLow = ThemeManager.Instance.GetThemeColorShade(_keyPressGradientColorLowTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color KeyPressGradientColorLow { get => _keyPressGradientColorLow; set { _keyPressGradientColorLow = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region KEYPRESSGRADIENTCOLORHIGH
        //KEYPRESSGRADIENTCOLORHIGH
        private UIRole _keyPressGradientColorHighRole = UIRole.MainBackground;
        private ThemeColorShade _keyPressGradientColorHighTheme = ThemeColorShade.Secondary_500;
        private Color _keyPressGradientColorHigh = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Secondary_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole KeyPressGradientColorHighRole { get => _keyPressGradientColorHighRole; set { _keyPressGradientColorHighRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade KeyPressGradientColorHighTheme
        {
            get => _keyPressGradientColorHighTheme; set
            {
                _keyPressGradientColorHighTheme = value;
                if (!_ignoreTheme) { KeyPressGradientColorHigh = ThemeManager.Instance.GetThemeColorShade(_keyPressGradientColorHighTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color KeyPressGradientColorHigh { get => _keyPressGradientColorHigh; set { _keyPressGradientColorHigh = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion

        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                WhiteKeyColor = ThemeManager.Instance.GetThemeColorShadeOffset(WhiteKeyColorTheme);
                BlackKeyColor = ThemeManager.Instance.GetThemeColorShadeOffset(BlackKeyColorTheme);
                PressedKeyColor = ThemeManager.Instance.GetThemeColorShadeOffset(PressedKeyColorTheme);
                KeyPressGradientColorLow = ThemeManager.Instance.GetThemeColorShadeOffset(KeyPressGradientColorLowTheme);
                KeyPressGradientColorHigh = ThemeManager.Instance.GetThemeColorShadeOffset(KeyPressGradientColorHighTheme);
                _keyPressColors = ThemeManager.ColorGenerateGradient(KeyPressGradientColorLow, KeyPressGradientColorHigh, KeyCount);
                for (int i = 0; i < _keys.Count; i++)
                {
                    _keys[i].PressedColor = _keyPressColors[i];
                }
            }
        }



        public ErrataPianoKeys()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            this.DoubleBuffered = true;
            GenerateKeys();
            SetupKeyMapping();
            this.KeyDown += PianoKeyboard_KeyDown;
            this.KeyUp += PianoKeyboard_KeyUp;
            this.Focus();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GenerateKeys();
            Invalidate();  // Force repaint
        }

        private void GenerateKeys()
        {
            _keys.Clear();
            int whiteKeyWidth = Width / (_octaves * 7);
            int whiteKeyHeight = Height - _whiteKeyHeightTrim;
            int blackKeyWidth = whiteKeyWidth / 2;
            int blackKeyHeight = (int)(Height * 0.6);
            int x = 0;

            string[] whiteNotes = { "C", "D", "E", "F", "G", "A", "B" };
            string[] blackNotes = { "C#", "D#", "", "F#", "G#", "A#", "" };

            for (int octave = 0; octave < _octaves; octave++)
            {
                for (int i = 0; i < 7; i++)
                {
                    _keys.Add(new PianoKey(new Rectangle(x, 0, whiteKeyWidth, whiteKeyHeight), whiteNotes[i] + (octave + 1), false));
                    x += whiteKeyWidth;
                }
            }

            x = whiteKeyWidth - (blackKeyWidth / 2);
            for (int octave = 0; octave < _octaves; octave++)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (!string.IsNullOrEmpty(blackNotes[i]))
                    {
                        _keys.Add(new PianoKey(new Rectangle(x, 0, blackKeyWidth, blackKeyHeight), blackNotes[i] + (octave + 1), true));
                    }
                    x += whiteKeyWidth;
                }
            }
        }

        private void SetupKeyMapping()
        {
            _keyMapping = new Dictionary<Keys, string>
        {
            { Keys.A, "C1" }, { Keys.W, "C#1" },
            { Keys.S, "D1" }, { Keys.E, "D#1" },
            { Keys.D, "E1" },
            { Keys.F, "F1" }, { Keys.T, "F#1" },
            { Keys.G, "G1" }, { Keys.Y, "G#1" },
            { Keys.H, "A1" }, { Keys.U, "A#1" },
            { Keys.J, "B1" },
            { Keys.K, "C2" }
        };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw white keys first
            foreach (var key in _keys.Where(k => !k.IsBlack))
            {
                using (Brush brush = new SolidBrush(key.IsPressed ? key.PressedColor : key.IsBlack ? _blackKeyColor : _whiteKeyColor))
                {
                    e.Graphics.FillRectangle(brush, key.Bounds);
                }
                e.Graphics.DrawRectangle(Pens.Black, key.Bounds);
            }

            // Draw black keys second (so they overlap correctly)
            foreach (var key in _keys.Where(k => k.IsBlack))
            {
                using (Brush brush = new SolidBrush(key.IsPressed ? key.PressedColor : key.IsBlack ? _blackKeyColor : _whiteKeyColor))
                {
                    e.Graphics.FillRectangle(brush, key.Bounds);
                }
                e.Graphics.DrawRectangle(Pens.Black, key.Bounds);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _isMouseDown = true;
            HandleMouseInteraction(e.Location);
          
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_isMouseDown) // Only trigger if mouse is held down
            {
                HandleMouseInteraction(e.Location);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isMouseDown = false;

            // Release all keys
            foreach (var key in _keys)
            {
                key.IsPressed = false;
            }

            Invalidate();  // Redraw the keyboard
        }

        private void HandleMouseInteraction(Point location)
        {
            // Reset all keys first
            foreach (var key in _keys)
            {
                key.IsPressed = false;
            }

            // Check black keys first (since they overlap white keys)
            var blackKey = _keys.FirstOrDefault(k => k.IsBlack && k.Bounds.Contains(location));
            if (blackKey != null)
            {
                blackKey.IsPressed = true;
                KeyPressed?.Invoke(this, blackKey.Note);
                Invalidate();
                return;  // Stop further checking
            }

            // Check white keys
            var whiteKey = _keys.FirstOrDefault(k => !k.IsBlack && k.Bounds.Contains(location));
            if (whiteKey != null)
            {
                whiteKey.IsPressed = true;
                KeyPressed?.Invoke(this, whiteKey.Note);
                Invalidate();
            }
        }


        private void PianoKeyboard_KeyDown(object sender, KeyEventArgs e)
        {

            base.OnKeyDown(e);

            if (_keyMapping.TryGetValue(e.KeyCode, out string note))
            {
                foreach (var key in _keys)
                {
                    if (key.Note == note)
                    {
                        key.IsPressed = true;
                        KeyPressed?.Invoke(this, key.Note);
                        Invalidate();
                        break;
                    }
                }
            }
        }

        private void PianoKeyboard_KeyUp(object sender, KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (_keyMapping.TryGetValue(e.KeyCode, out string note))
            {
                foreach (var key in _keys)
                {
                    if (key.Note == note)
                    {
                        key.IsPressed = false;
                        KeyReleased?.Invoke(this, key.Note);
                        Invalidate();
                        break;
                    }
                }
            }
        }


    }

    public class PianoKey
    {
        public Rectangle Bounds { get; }
        public string Note { get; }
        public bool IsBlack { get; }
        public bool IsPressed { get; set; }
        public Color PressedColor { get; set; }

        public PianoKey(Rectangle bounds, string note, bool isBlack)
        {
            Bounds = bounds;
            Note = note;
            IsBlack = isBlack;
            IsPressed = false;
        }
    }
}