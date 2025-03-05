using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataGraphLine : Control
    {
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

        #region GRAPHTITLECOLOR
        //GRAPHTITLECOLOR
        private UIRole _graphTitleColorRole = UIRole.MainBackground;
        private ThemeColorShade _graphTitleColorTheme = ThemeColorShade.Primary_500;
        private Color _graphTitleColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole GraphTitleColorRole { get => _graphTitleColorRole; set { _graphTitleColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade GraphTitleColorTheme
        {
            get => _graphTitleColorTheme; set
            {
                _graphTitleColorTheme = value;
                if (!_ignoreTheme) { GraphTitleColor = ThemeManager.Instance.GetThemeColorShade(_graphTitleColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color GraphTitleColor { get => _graphTitleColor; set { _graphTitleColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region BORDERCOLOR
        //BORDERCOLOR
        private UIRole _borderColorRole = UIRole.MainBackground;
        private ThemeColorShade _borderColorTheme = ThemeColorShade.Neutral_100;
        private Color _borderColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_100);

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
        ////
        #endregion
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
        #region LINECOLOR
        //LINECOLOR
        private UIRole _lineColorRole = UIRole.TitleBar;
        private ThemeColorShade _lineColorTheme = ThemeColorShade.Primary_500;
        private Color _lineColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole LineColorRole { get => _lineColorRole; set { _lineColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade LineColorTheme
        {
            get => _lineColorTheme; set
            {
                _lineColorTheme = value;
                if (!_ignoreTheme) { LineColor = ThemeManager.Instance.GetThemeColorShade(_lineColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color LineColor { get => _lineColor; set { _lineColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region VERTICALLINECOLOR
        //VERTICALLINECOLOR
        private UIRole _verticalLineColorRole = UIRole.MainBackground;
        private ThemeColorShade _verticalLineColorTheme = ThemeColorShade.Secondary_500;
        private Color _verticalLineColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Secondary_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole VerticalLineColorRole { get => _verticalLineColorRole; set { _verticalLineColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade VerticalLineColorTheme
        {
            get => _verticalLineColorTheme; set
            {
                _verticalLineColorTheme = value;
                if (!_ignoreTheme) { VerticalLineColor = ThemeManager.Instance.GetThemeColorShade(_verticalLineColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color VerticalLineColor { get => _verticalLineColor; set { _verticalLineColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion

        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                LineColor = ThemeManager.Instance.GetThemeColorShadeOffset(LineColorTheme);
                BackColor = ThemeManager.Instance.GetThemeColorShadeOffset(BackColorTheme);
                ColorGradientEnd = ThemeManager.Instance.GetThemeColorShadeOffset(ColorGradientEndTheme);
                ColorGradientStart = ThemeManager.Instance.GetThemeColorShadeOffset(ColorGradientStartTheme);
                BorderColor = ThemeManager.Instance.GetThemeColorShadeOffset(BorderColorTheme);
                GraphTitleColor = ThemeManager.Instance.GetThemeColorShadeOffset(GraphTitleColorTheme);
                VerticalLineColor = ThemeManager.Instance.GetThemeColorShadeOffset(VerticalLineColorTheme);
            }
        }

        public ErrataGraphLine()
        {
            _data.CollectionChanged += OnValuesChanged;
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            DoubleBuffered = true;
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            base.Size = new Size(200, 100);
            for (int i = 0; i < 25; i++)
            {
                _data.Add(rnd.Next(100));
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
        [Description("The color of the text when the tab is selected")]
        public bool ShowVerticalLines
        {
            get => showVerticalLines;
            set
            {
                showVerticalLines = value;
                Refresh();
            }
        }







        [Category("Misc")]
        [Browsable(true)]
        [Description("The of the graph")]
        public string GraphTitle
        {
            get => graphTitle;
            set
            {
                graphTitle = value;
                Refresh();
            }
        }

        [Category("Misc")]
        [Browsable(true)]
        [Description("Draw the title on the control")]
        public bool ShowTitle
        {
            get => showTitle;
            set
            {
                showTitle = value;
                Refresh();
            }
        }

        [Category("Misc")]
        [Browsable(true)]
        [Description("Draw the border on the control")]
        public bool ShowBorder
        {
            get => showBorder;
            set
            {
                showBorder = value;
                Refresh();
            }
        }

        [Category("Misc")]
        [Browsable(true)]
        [Description("Draw the points on each value")]
        public bool ShowPoints
        {
            get => showPoints;
            set
            {
                showPoints = value;
                Refresh();
            }
        }

        [Category("Misc")]
        [Browsable(true)]
        [Description("The point size")]
        public int PointSize
        {
            get => pointSize;
            set
            {
                pointSize = value;
                Refresh();
            }
        }

        [Category("Misc")]
        [Browsable(true)]
        [Description("The title alignment")]
        public StringAlignment TitleAlignment
        {
            get => titleAlignment;
            set
            {
                titleAlignment = value;
                Refresh();
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
                Refresh();
            }
        }

        private SmoothingMode _SmoothingType = SmoothingMode.HighQuality;
        [Category("Misc")]
        [Browsable(true)]
        public SmoothingMode SmoothingType
        {
            get => _SmoothingType;
            set
            {
                _SmoothingType = value;
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

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingType;

            Pen pen = new(_lineColor, 1f);
            Pen pen2 = new(_verticalLineColor, 1f);

            if (graphStyle == Style.Material)
            {
                e.Graphics.FillRectangle(new SolidBrush(_backColor), new Rectangle(0, 0, base.Width, base.Height));
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(_backColor), new Rectangle(0, 0, base.Width, base.Height));
            }

            int total = _data.ToArray().Max();
            int num = base.Width / _data.Count;
            int num2 = 0;
            int num3 = base.Height;
            int num4 = num;
            int num5 = 0;

            List<PointF> list = new()
            {
                new Point(1, base.Height)
            };

            foreach (int num6 in _data)
            {
                if (num6 > 0)
                {
                    int num7 = Percentage.IntToPercent(num6, total);

                    if (num7 > 97)
                    {
                        num5 = base.Height - Percentage.PercentToInt(97, base.Height);
                    }
                    else if (num7 < 3)
                    {
                        num5 = base.Height - Percentage.PercentToInt(3, base.Height);
                    }
                    else
                    {
                        num5 = base.Height - (num7 * base.Height / 100);
                    }

                    list.Add(new Point(num4 - 1, num5 - 1));

                    num2 = num4;
                    num3 = num5;
                    num4 += num;
                }
            }

            list.Add(new Point(base.Width, num5 - 1));

            if (graphStyle != Style.Curved)
            {
                list.Add(new Point(base.Width, base.Height));

                if (graphStyle == Style.Flat)
                {
                    SolidBrush brush = new(_backColor);
                    e.Graphics.FillPolygon(brush, list.ToArray());
                }
                else
                {
                    LinearGradientBrush brush2 = new(new Rectangle(0, 0, base.Width, base.Height), ColorGradientStart, ColorGradientEnd, 1f);
                    e.Graphics.FillPolygon(brush2, list.ToArray());
                }

                num2 = 1;
                num3 = base.Height;
                num4 = num;
                num5 = 0;

                int num8 = 0;

                foreach (int number in _data)
                {
                    int num9 = Percentage.IntToPercent(number, total);

                    if (num9 > 97)
                    {
                        num5 = base.Height - Percentage.PercentToInt(97, base.Height);
                    }
                    else if (num9 < 3)
                    {
                        num5 = base.Height - Percentage.PercentToInt(3, base.Height);
                    }
                    else
                    {
                        num5 = base.Height - (num9 * base.Height / 100);
                    }

                    if (graphStyle == Style.Flat && showVerticalLines)
                    {
                        num8++;

                        if (num8 != _data.ToArray().Length && num4 != 0 && num4 != base.Width)
                        {
                            e.Graphics.DrawLine(pen2, num4, base.Height, num4, 0);
                        }
                    }

                    e.Graphics.DrawLine(pen, num2 - 1, num3 - 1, num4 - 1, num5 - 1);

                    if (showPoints)
                    {
                        if (num5 - (pointSize / 2) - 1 < 0)
                        {
                            e.Graphics.FillEllipse(new SolidBrush(_lineColor), new RectangleF(num4 - (pointSize / 2) - 1, -1f, pointSize, pointSize));
                        }
                        else if (num5 - (pointSize / 2) - 1 + pointSize > base.Height)
                        {
                            e.Graphics.FillEllipse(new SolidBrush(_lineColor), new RectangleF(num4 - (pointSize / 2) - 1, base.Height - pointSize + 1, pointSize, pointSize));
                        }
                        else
                        {
                            e.Graphics.FillEllipse(new SolidBrush(_lineColor), new RectangleF(num4 - (pointSize / 2) - 1, num5 - (pointSize / 2) - 1, pointSize, pointSize));
                        }
                    }

                    num2 = num4;
                    num3 = num5;
                    num4 += num;
                }

                e.Graphics.DrawLine(pen, num2, num3, base.Width, num3);
            }
            else
            {
                if (showPoints)
                {
                    foreach (PointF pointF in list)
                    {
                        if (pointF.Y - (pointSize / 2) - 1f < 0f)
                        {
                            e.Graphics.FillEllipse(new SolidBrush(_lineColor), new RectangleF(pointF.X - (pointSize / 2) - 1f, -1f, pointSize, pointSize));
                        }
                        else if (pointF.Y - (pointSize / 2) - 1f + pointSize > Height)
                        {
                            e.Graphics.FillEllipse(new SolidBrush(_lineColor), new RectangleF(pointF.X - (pointSize / 2) - 1f, base.Height - pointSize + 1, pointSize, pointSize));
                        }
                        else
                        {
                            e.Graphics.FillEllipse(new SolidBrush(_lineColor), new RectangleF(pointF.X - (pointSize / 2) - 1f, pointF.Y - (pointSize / 2) - 1f, pointSize, pointSize));
                        }
                    }
                }

                e.Graphics.DrawCurve(pen, list.ToArray());
            }

            if (graphStyle != Style.Material && showBorder)
            {
                e.Graphics.DrawRectangle(new Pen(_borderColor, 2f), new Rectangle(0, 0, base.Width - 1, base.Height - 1));
            }

            if (showTitle)
            {
                StringFormat stringFormat = new()
                {
                    LineAlignment = StringAlignment.Near,
                    Alignment = titleAlignment
                };

                Font font = new("Arial", 14f);
                SolidBrush brush3 = new(_graphTitleColor);
                RectangleF layoutRectangle = new(0f, 0f, Width, Height);

                e.Graphics.PixelOffsetMode = PixelOffsetType;
                e.Graphics.TextRenderingHint = TextRenderingType;

                e.Graphics.DrawString(graphTitle, font, brush3, layoutRectangle, stringFormat);
            }
            base.OnPaint(e);
        }



        private bool showVerticalLines;

        private bool showBorder;

        private bool showTitle;

        private bool showPoints = true;

        private StringAlignment titleAlignment;

        private int pointSize = 7;

        



        private string graphTitle = "Errata Line Graph";

        private Style graphStyle = Style.Material;

        public enum Style
        {
            Flat,
            Material,
            Curved
        }
    }

    public static class Percentage
    {
        public static int IntToPercent(int number, int total)
        {
            return Convert.ToInt32(Math.Round(100 * number / (double)total));
        }

        public static int PercentToInt(int number, int total)
        {
            return Convert.ToInt32(Math.Round(total / 100 * (double)number));
        }
    }

}
