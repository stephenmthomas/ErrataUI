using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;


namespace ErrataUI
{


    public class ErrataGraphBar : Control
    {
        private static Random rnd = new Random();


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



        private ObservableCollection<int> _data = new ObservableCollection<int>();
        [Category("Misc")]
        [Browsable(true)]
        [Description("The color of the text when the tab is selected")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObservableCollection<int> Data
        {
            get => _data;
            set
            {
                _data = value;
                Refresh();
            }
        }

        private void OnValuesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ValuesChanged?.Invoke(this, EventArgs.Empty);
            Invalidate(); // Redraw the pie chart
        }

        public event EventHandler ValuesChanged;

        



        [Category("Misc")]
        [Browsable(true)]
        [Description("The item sorting style")]
        public SortStyle Sorting
        {
            get => sorting;
            set
            {
                sorting = value;
                Invalidate();
            }
        }

        [Category("Misc")]
        [Browsable(true)]
        [Description("The text aligning")]
        public Aligning TextAlignment
        {
            get => textAlignment;
            set
            {
                textAlignment = value;
                Invalidate();
            }
        }

        [Category("Misc")]
        [Browsable(true)]
        [Description("The orientation of the graph")]
        public Orientation GraphOrientation
        {
            get => graphOrientation;
            set
            {
                graphOrientation = value;
                Invalidate();
            }
        }

        [Category("Misc")]
        [Browsable(true)]
        [Description("The style of the graph")]
        public Style GraphStyle
        {
            get => graphStyle;
            set
            {
                graphStyle = value;
                Invalidate();
            }
        }

        [Category("Misc")]
        [Browsable(true)]
        [Description("Show the item grid")]
        public bool ShowGrid
        {
            get => showGrid;
            set
            {
                showGrid = value;
                Invalidate();
            }
        }

        private PixelOffsetMode _PixelOffsetType = PixelOffsetMode.HighQuality;
        [Category("Misc")]
        [Browsable(true)]
        public PixelOffsetMode PixelOffsetType
        {
            get => _PixelOffsetType;
            set
            {
                _PixelOffsetType = value;
                Invalidate();
            }
        }

        private TextRenderingHint _TextRenderingType = TextRenderingHint.ClearTypeGridFit;
        [Category("Misc")]
        [Browsable(true)]
        public TextRenderingHint TextRenderingType
        {
            get => _TextRenderingType;
            set
            {
                _TextRenderingType = value;
                Invalidate();
            }
        }

        public void ClearItems()
        {
            _data = null;
        }

        private float _scaleMultiplier = 0.75F;
        [Category("Misc")]
        public float ScaleMultiplier
        {
            get => _scaleMultiplier;
            set
            {
                _scaleMultiplier = value; Invalidate();
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
        ////ColorGradientEnd = ThemeManager.Instance.GetThemeColorShadeOffset(ColorGradientEndTheme);
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
        #region TEXTCOLOR
        //TEXTCOLOR
        private UIRole _textColorRole = UIRole.MainBackground;
        private ThemeColorShade _textColorTheme = ThemeColorShade.Neutral_500;
        private Color _textColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_500);

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

        [Category("Misc"), Description("Color.")]
        public Color TextColor { get => _textColor; set { _textColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region SPLITTERCOLOR
        //SPLITTERCOLOR
        private UIRole _splitterColorRole = UIRole.MainBackground;
        private ThemeColorShade _splitterColorTheme = ThemeColorShade.Neutral_700;
        private Color _splitterColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_700);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole SplitterColorRole { get => _splitterColorRole; set { _splitterColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade SplitterColorTheme
        {
            get => _splitterColorTheme; set
            {
                _splitterColorTheme = value;
                if (!_ignoreTheme) { SplitterColor = ThemeManager.Instance.GetThemeColorShade(_splitterColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color SplitterColor { get => _splitterColor; set { _splitterColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region GRIDCOLOR
        //GRIDCOLOR
        private UIRole _gridColorRole = UIRole.MainBackground;
        private ThemeColorShade _gridColorTheme = ThemeColorShade.Neutral_800;
        private Color _gridColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_800);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole GridColorRole { get => _gridColorRole; set { _gridColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade GridColorTheme
        {
            get => _gridColorTheme; set
            {
                _gridColorTheme = value;
                if (!_ignoreTheme) { GridColor = ThemeManager.Instance.GetThemeColorShade(_gridColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color GridColor { get => _gridColor; set { _gridColor = value; Invalidate(); } }

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
                TextColor = ThemeManager.Instance.GetThemeColorShadeOffset(TextColorTheme);
                SplitterColor = ThemeManager.Instance.GetThemeColorShadeOffset(SplitterColorTheme);
                GridColor = ThemeManager.Instance.GetThemeColorShadeOffset(GridColorTheme);
            }
        }



        public ErrataGraphBar()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            _data.CollectionChanged += OnValuesChanged;
            _data.Clear();

            for (int i = 0; i < 25; i++)
            {
                _data.Add(rnd.Next(100));
            }
            
            base.Size = new Size(294, 200);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (_data == null || _data.Count == 0)
            {
                using (SolidBrush brush = new(_backColor))
                {
                    e.Graphics.FillRectangle(brush, 0, 0, Width, Height);
                }
                return;
            }

            List<int> sortedData = SortData(_data);

            switch (graphStyle)
            {
                case Style.Flat:
                    DrawFlatGraph(e.Graphics, sortedData);
                    break;
                case Style.Material:
                    DrawMaterialGraph(e.Graphics, sortedData);
                    break;
                case Style.Bootstrap:
                    DrawBootstrapGraph(e.Graphics, sortedData);
                    break;
            }
        }

        private List<int> SortData(IEnumerable<int> data)
        {
            return sorting switch
            {
                SortStyle.Normal => data.ToList(),
                SortStyle.Ascending => data.OrderBy(p => p).ToList(),
                SortStyle.Descending => data.OrderByDescending(p => p).ToList(),
                _ => data.ToList()
            };
        }

        private void DrawFlatGraph(Graphics g, List<int> data)
        {
            using (SolidBrush bgBrush = new(_colorGradientStart))
                g.FillRectangle(bgBrush, 0, 0, Width, Height);

            int pos = 0;
            int elementSize = graphOrientation == Orientation.Horizontal ? Height / data.Count : Width / data.Count;
            //decimal scale = (graphOrientation == Orientation.Horizontal ? Width : Height) / (decimal)data.Max();
            float scale = _scaleMultiplier * ((graphOrientation == Orientation.Vertical ? Height : Width) / data.Max());

            foreach (int value in data)
            {
                RectangleF rect = graphOrientation == Orientation.Horizontal
                    ? new(0, pos, (int)(value * scale), elementSize)
                    : new(pos, Height - (int)(value * scale), elementSize, Height);

                using (SolidBrush barBrush = new(_backColor))
                    g.FillRectangle(barBrush, rect);

                DrawText(g, value.ToString(), rect);
                pos += elementSize;
            }

            if (showGrid) DrawGrid(g, data, elementSize);
        }

        private void DrawMaterialGraph(Graphics g, List<int> data)
        {
            using (SolidBrush bgBrush = new(_backColor))
                g.FillRectangle(bgBrush, 0, 0, Width, Height);

            List<Color> colors = ThemeManager.ColorGenerateGradient(ColorGradientStart, ColorGradientEnd, data.Count);

            int pos = 0;
            int elementSize = graphOrientation == Orientation.Vertical ? Width / data.Count : Height / data.Count;
            float scale = _scaleMultiplier * ((graphOrientation == Orientation.Vertical ? Height : Width) / data.Max());
            int colorIndex = 0;

            foreach (int value in data)
            {
                RectangleF rect = graphOrientation == Orientation.Vertical
                    ? new(pos, Height - (value * scale), elementSize, Height)
                    : new(0, pos, value * scale, elementSize);

                using (SolidBrush barBrush = new(colors[colorIndex]))
                    g.FillRectangle(barBrush, rect);

                DrawText(g, value.ToString(), rect, colors[colorIndex]);

                pos += elementSize;
                colorIndex = (colorIndex + 1) % colors.Count;
            }
        }

        private void DrawBootstrapGraph(Graphics g, List<int> data)
        {
            using (SolidBrush bgBrush = new(_backColor))
                g.FillRectangle(bgBrush, 0, 0, Width, Height);

            int pos = 0;
            int elementSize = graphOrientation == Orientation.Horizontal ? Height / data.Count : Width / data.Count;
            //decimal scale = (graphOrientation == Orientation.Horizontal ? Width : Height) / (decimal)data.Max();
            float scale = _scaleMultiplier * ((graphOrientation == Orientation.Vertical ? Height : Width) / data.Max());

            foreach (int value in data)
            {
                RectangleF rect = graphOrientation == Orientation.Horizontal
                    ? new(0, pos, (int)(value * scale), elementSize)
                    : new(pos, Height - (int)(value * scale), elementSize, Height);

                using (SolidBrush barBrush = new(ControlPaint.Dark(GenerateRandomColor(), 0.2f)))
                    g.FillRectangle(barBrush, rect);

                DrawText(g, value.ToString(), rect, _textColor);
                pos += elementSize;
            }

            if (showGrid) DrawGrid(g, data, elementSize, _gridColor);
        }

        private void DrawText(Graphics g, string text, RectangleF rect, Color? textColor = null)
        {
            using (SolidBrush brush = new(TextColor))  // Default to white for visibility
            {
                StringFormat format = new()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                float x0, y0;
                float width, height; 

                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                if (GraphOrientation == Orientation.Horizontal)
                {
                    x0 = (int)rect.Right;
                    y0 = (int)rect.Top;
                    width = Font.Size * 2f;
                    height = rect.Height;
                }
                else
                {
                    x0 = (int)rect.Left;
                    y0 = (int)rect.Top - Font.Height ;
                    width = rect.Width;
                    height = Font.Size * 2f;
                }
                var barArea = new RectangleF(x0,y0,width,height);
                g.DrawString(text, Font, brush, barArea, format);
            }
        }

        private void DrawGrid(Graphics g, List<int> data, int elementSize, Color? gridColor = null)
        {
            using Pen gridPen = new(gridColor ?? _splitterColor, 1f);
            int pos = 0;

            foreach (int _ in data)
            {
                if (graphOrientation == Orientation.Horizontal)
                    g.DrawRectangle(gridPen, 0, pos, Width, pos + elementSize);
                else
                    g.DrawRectangle(gridPen, pos, 0, pos + elementSize, Height);

                pos += elementSize;
            }

            g.DrawRectangle(gridPen, 1, 1, Width, Height);
        }







        private SortStyle sorting = SortStyle.Normal;

        private Aligning textAlignment = Aligning.Far;

        private Orientation graphOrientation = Orientation.Vertical;

        private Style graphStyle = Style.Material;

        private bool showGrid;

        public enum SortStyle
        {
            Ascending,
            Descending,
            Normal
        }

        public enum Aligning
        {
            Near,
            Center,
            Far
        }

        public enum Orientation
        {
            Horizontal,
            Vertical
        }

        public enum Style
        {
            Flat,
            Material,
            Bootstrap
        }
    }


}