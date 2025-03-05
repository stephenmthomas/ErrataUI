using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{




    public class ErrataGraphPie : Control
    {
        private ObservableCollection<int> _data = new ObservableCollection<int>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObservableCollection<int> Data
        {
            get => _data;
            set
            {
                if (_data != value)
                {
                    if (_data != null)
                        _data.CollectionChanged -= OnValuesChanged; // Unsubscribe from old collection

                    _data = value ?? new ObservableCollection<int>(); // Prevent null issues
                    _data.CollectionChanged += OnValuesChanged; // Subscribe to new collection

                    // Generate gradient colors when data updates
                    _Colors = ThemeManager.ColorGenerateGradient3(ColorGradientStart, ColorGradientMid, ColorGradientEnd, _data.Count);

                    Invalidate();
                }
            }
        }

        private void OnValuesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ValuesChanged?.Invoke(this, EventArgs.Empty);

            // Update colors when data changes
            _Colors = ThemeManager.ColorGenerateGradient3(ColorGradientStart, ColorGradientMid, ColorGradientEnd, _data.Count);

            Invalidate(); // Redraw the pie chart
        }

        public event EventHandler ValuesChanged;


        private static Random rnd = new Random();

        private bool _ignoreRoles;
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

        private int _pieInset = 10;
        [Category("Misc")]
        public int PieInset
        {
            get => _pieInset;
            set
            {
                _pieInset = value; Invalidate();
            }
        }



        #region COLORGRADIENTSTART
        //COLORGRADIENTSTART
        private UIRole _colorGradientStartRole = UIRole.TitleBar;
        private ThemeColorShade _colorGradientStartTheme = ThemeColorShade.Primary_500;
        private Color _colorGradientStart = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole ColorGradientStartRole { get => _colorGradientStartRole; set { _colorGradientStartRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade ColorGradientStartTheme
        {
            get => _colorGradientStartTheme; set
            {
                _colorGradientStartTheme = value;
                if (!_ignoreTheme) { ColorGradientStart = ThemeManager.Instance.GetThemeColorShade(_colorGradientStartTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color ColorGradientStart { get => _colorGradientStart; set { _colorGradientStart = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region COLORGRADIENTMID
        //COLORGRADIENTMID
        private UIRole _colorGradientMidRole = UIRole.MainBackground;
        private ThemeColorShade _colorGradientMidTheme = ThemeColorShade.SemanticA_500;
        private Color _colorGradientMid = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.SemanticA_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole ColorGradientMidRole { get => _colorGradientMidRole; set { _colorGradientMidRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade ColorGradientMidTheme
        {
            get => _colorGradientMidTheme; set
            {
                _colorGradientMidTheme = value;
                if (!_ignoreTheme) { ColorGradientMid = ThemeManager.Instance.GetThemeColorShade(_colorGradientMidTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color ColorGradientMid { get => _colorGradientMid; set { _colorGradientMid = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region COLORGRADIENTEND
        //COLORGRADIENTEND
        private UIRole _colorGradientEndRole = UIRole.TitleBar;
        private ThemeColorShade _colorGradientEndTheme = ThemeColorShade.Secondary_500;
        private Color _colorGradientEnd = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Secondary_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole ColorGradientEndRole { get => _colorGradientEndRole; set { _colorGradientEndRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade ColorGradientEndTheme
        {
            get => _colorGradientEndTheme; set
            {
                _colorGradientEndTheme = value;
                if (!_ignoreTheme) { ColorGradientEnd = ThemeManager.Instance.GetThemeColorShade(_colorGradientEndTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color ColorGradientEnd { get => _colorGradientEnd; set { _colorGradientEnd = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region BACKCOLOR
        //BACKCOLOR
        private UIRole _backColorRole = UIRole.TitleBar;
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

        [Category("Misc"), Description("Color.")]
        public Color BackColor { get => _backColor; set { _backColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion



        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                BackColor = ThemeManager.Instance.GetThemeColorShadeOffset(BackColorTheme);
                ColorGradientEnd = ThemeManager.Instance.GetThemeColorShadeOffset(ColorGradientEndTheme);
                ColorGradientStart = ThemeManager.Instance.GetThemeColorShadeOffset(ColorGradientStartTheme);
                ColorGradientMid = ThemeManager.Instance.GetThemeColorShadeOffset(ColorGradientMidTheme);
                _Colors = ThemeManager.ColorGenerateGradient3(ColorGradientStart, ColorGradientMid, ColorGradientEnd, _data.Count);
            }
        }


        public ErrataGraphPie()
        {
            _data.CollectionChanged += OnValuesChanged;
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true); // Enable double buffering
            base.Size = new Size(100, 100);

            for (int i = 0; i < 13; i++)
            {
                Data.Add(rnd.Next(100));
            }
        }

        private SmoothingMode _SmoothingType = SmoothingMode.AntiAlias;
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
                Invalidate();
            }
        }

        private List<Color> _Colors;
        public List<Color> Colors
        {
            get => _Colors;
            set
            {
                _Colors = value;
                Invalidate();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e); // Call base method to ensure default behavior
            Height = Width;
            Invalidate(); // Redraw the control when resized
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e); // Call base OnPaint first

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (Data == null || Data.Count == 0 || Colors == null || Colors.Count < Data.Count)
                return; // Prevent errors if data is missing

            if (_Colors == null || _Colors.Count != Data.Count)
                _Colors = ThemeManager.ColorGenerateGradient3(ColorGradientStart, ColorGradientMid, ColorGradientEnd, Data.Count);

            int total = Data.Sum(); // Sum of all values to calculate proportions
            if (total == 0) return; // Avoid division by zero

            float startAngle = 0;
            for (int i = 0; i < Data.Count; i++)
            {
                float sweepAngle = (Data[i] / (float)total) * 360; // Calculate the angle for this slice

                using (SolidBrush brush = new SolidBrush(Colors[i % Colors.Count])) // Ensure valid index
                {
                    e.Graphics.FillPie(brush, _pieInset, _pieInset, Width - 2 * _pieInset, Height - 2 * _pieInset, startAngle, sweepAngle);
                }

                startAngle += sweepAngle; // Move to the next slice
            }
        }
    }


}