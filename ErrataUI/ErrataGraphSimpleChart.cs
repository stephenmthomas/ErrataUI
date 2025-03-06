using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ErrataUI;
using static ErrataUI.ErrataGraphSimpleChart;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public class ErrataGraphSimpleChart : Control
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


        private int _chartInset = 10;
        [Category("Misc")]
        public int ChartInset
        {
            get => _chartInset;
            set
            {
                _chartInset = value;
                Invalidate();
            }
        }


        private float _verticalBuffer = 0.05F;
        [Category("Misc")]
        public float VerticalBuffer
        {
            get => _verticalBuffer;
            set
            {
                _verticalBuffer = value;
                Invalidate();
            }
        }


        private float _lineWeight = 2F;
        [Category("Misc")]
        public float LineWeight
        {
            get => _lineWeight;
            set
            {
                _lineWeight = value;
                Invalidate();
            }
        }

        private int _chartAreaBorderWeight = 1;
        [Category("Misc")]
        public int ChartAreaBorderWeight
        {
            get => _chartAreaBorderWeight;
            set
            {
                _chartAreaBorderWeight = value;
                Invalidate();
            }
        }
                                      //  0      1     2      3      4    5     6     7    8      9     10      11    12
                                      // 1000   900   800    750    700  600   500   400  300    200    150     100   50
        private float[] defaultData = { 0.20f, 0.3f, 0.55f, 0.6f, 0.75f, 0.9f, 1.0f, 1.1f, 1.2f, 1.3f, 1.35f, 1.4f, 1.5f };

        private double[] dataPoints;
        public double[] DataPoints
        {
            get => dataPoints;
            set
            {
                dataPoints = value;
                Invalidate(); // Redraw the control when data changes
            }
        }



        private double[] dataPoints2;
        public double[] DataPoints2
        {
            get => dataPoints2;
            set
            {
                dataPoints2 = value;
                Invalidate(); // Redraw the control when data changes
            }
        }



        public double[] ConvertThemeCurves(float[] curve)
        {
            if (curve == null)
            {
                throw new ArgumentNullException(nameof(curve), "Input curve cannot be null.");
            }

            double[] result = new double[curve.Length];
            for (int i = 0; i < curve.Length; i++)
            {
                result[i] = (double)curve[i];
            }

            return result;
        }

        public double[] CurveAbsFrom500(double[] curve) 
        {
            int mid = curve.Length / 2;
            double midValue = curve[mid];
            double[] result = new double[curve.Length];

            for (int i = 0; i < curve.Length; i++)
            {
                result[i] = Math.Abs(midValue - curve[i]);
            }

            return result;
        }


        #region LINECOLOR
        //LINECOLOR
        private UIRole _lineColorRole = UIRole.ChartPrimary;
        private ThemeColorShade _lineColorTheme = ThemeColorShade.Primary_500;
        private Color _lineColor = Color.FromArgb(0, 128, 200);

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
        #endregion

        #region MARKERCOLOR
        //MARKERCOLOR
        private UIRole _markerColorRole = UIRole.ChartSecondary;
        private ThemeColorShade _markerColorTheme = ThemeColorShade.Secondary_500;
        private Color _markerColor = Color.FromArgb(128, 128, 128);

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
        #endregion


        #region BACKCOLOR
        //BACKCOLOR
        private UIRole _backColorRole = UIRole.MainBackground;
        private ThemeColorShade _backColorTheme = ThemeColorShade.Neutral_50;
        private Color _backColor = Color.FromArgb(250, 250, 250);

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
        #endregion
        #region CHARTBACKCOLOR
        //CHARTBACKCOLOR
        private UIRole _chartBackColorRole = UIRole.MainBackground;
        private ThemeColorShade _chartBackColorTheme = ThemeColorShade.Neutral_150;
        private Color _chartBackColor = Color.FromArgb(250, 250, 250);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole ChartBackColorRole { get => _chartBackColorRole; set { _chartBackColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade ChartBackColorTheme
        {
            get => _chartBackColorTheme; set
            {
                _chartBackColorTheme = value;
                if (!_ignoreTheme) { ChartBackColor = ThemeManager.Instance.GetThemeColorShade(_chartBackColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color ChartBackColor { get => _chartBackColor; set { _chartBackColor = value; Invalidate(); } }
        #endregion
        #region CHARTAREABORDERCOLOR
        //CHARTAREABORDERCOLOR
        private UIRole _chartAreaBorderColorRole = UIRole.GeneralBorders;
        private ThemeColorShade _chartAreaBorderColorTheme = ThemeColorShade.Neutral_500;
        private Color _chartAreaBorderColor = Color.FromArgb(128, 128, 128);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole ChartAreaBorderColorRole { get => _chartAreaBorderColorRole; set { _chartAreaBorderColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade ChartAreaBorderColorTheme
        {
            get => _chartAreaBorderColorTheme; set
            {
                _chartAreaBorderColorTheme = value;
                if (!_ignoreTheme) { ChartAreaBorderColor = ThemeManager.Instance.GetThemeColorShade(_chartAreaBorderColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color ChartAreaBorderColor { get => _chartAreaBorderColor; set { _chartAreaBorderColor = value; Invalidate(); } }
        #endregion


        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                LineColor = ThemeManager.Instance.GetThemeColorShade(LineColorTheme);
                BackColor = ThemeManager.Instance.GetThemeColorShade(BackColorTheme);
                ChartBackColor = ThemeManager.Instance.GetThemeColorShade(ChartBackColorTheme);
                ChartAreaBorderColor = ThemeManager.Instance.GetThemeColorShade(ChartAreaBorderColorTheme);
                MarkerColor = ThemeManager.Instance.GetThemeColorShade(MarkerColorTheme);
            }
        }

        private double[] RandData()
        {
            Random random = new Random();
            double[] randomArray = new double[12];

            for (int i = 0; i < randomArray.Length; i++)
            {
                // Generate a random double between -1.5 and 1.5
                randomArray[i] = random.NextDouble() * 3.0 - 1.5;
            }

            // Sort the array to ensure sequential order
            Array.Sort(randomArray);

            return randomArray;
        }



        private ControlType _controlType;
        [Browsable(true)]
        [Category("Misc")]
        [Description("Sets the type.")]
        public ControlType Type
        {
            get => _controlType;
            set
            {
                _controlType = value;
                UpdateType();
            }
        }

        public enum ControlType
        {
            Line,
            Dot,
            LineDot,
            LineDualArray
        }

        private void UpdateType()
        {
            // Update the Text property based on the ButtonType
            switch (_controlType)
            {
                case ControlType.Line:
                    break;
                case ControlType.Dot:
                    //Do stuff
                    break;
                case ControlType.LineDot:
                    //Do stuff
                    break;
                case ControlType.LineDualArray:
                    //Do stuff
                    break;
            }
            Invalidate();
        }




        //constructor

        public ErrataGraphSimpleChart()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            this.DoubleBuffered = true; // Reduce flickering
            this.Resize += (s, e) => Invalidate(); // Redraw on resize
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //dataPoints = CurveAbsFrom500(ConvertThemeCurves(defaultData));
            dataPoints = RandData();
            dataPoints2 = RandData();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (dataPoints == null || dataPoints.Length < 2)
                return;

            var graphics = e.Graphics;
            graphics.Clear(BackColor);
            //graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Determine the chart area
            var chartArea = new Rectangle(_chartInset, _chartInset, this.Width - 2 * _chartInset, this.Height - 2 * _chartInset);
            var chartFrame = new Rectangle(_chartInset / 2, _chartInset / 2, this.Width - _chartInset, this.Height - _chartInset);

            using (Brush chartBrush = new SolidBrush(ChartBackColor))
            using (Pen chartBorder = new Pen(ChartAreaBorderColor, ChartAreaBorderWeight))
            {
                {
                    graphics.FillRectangle(chartBrush, chartFrame);
                    graphics.DrawRectangle(chartBorder, chartFrame);
                }
            }
                

            // Calculate scaling factors
            double maxDataPoint = Math.Max(1, dataPoints.Max());
            double minDataPoint = Math.Min(0, dataPoints.Min());
            maxDataPoint = DataPoints.Max() + (_verticalBuffer * DataPoints.Max());
            minDataPoint =- (_verticalBuffer * DataPoints.Max());

            double _range = maxDataPoint - minDataPoint;

            if (_range == 0) { _range = 1; }

            double verticalScale = chartArea.Height / (_range);
            double horizontalScale = chartArea.Width / (double)(dataPoints.Length - 1);

            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


            if (_controlType == ControlType.Line)
            {
                // Draw the lines connecting data points
                for (int i = 0; i < dataPoints.Length - 1; i++)
                {
                    var x1 = (float)(chartArea.Left + i * horizontalScale);
                    var y1 = (float)(chartArea.Bottom - (dataPoints[i] - minDataPoint) * verticalScale);
                    var x2 = (float)(chartArea.Left + (i + 1) * horizontalScale);
                    var y2 = (float)(chartArea.Bottom - (dataPoints[i + 1] - minDataPoint) * verticalScale);

                    using (Pen linePen = new Pen(_lineColor, _lineWeight))
                    { graphics.DrawLine(linePen, x1, y1, x2, y2); }

                }
            }
            else if (_controlType == ControlType.Dot)
            {
                // Draw the lines connecting data points
                for (int i = 0; i < dataPoints.Length - 1; i++)
                {
                    var x1 = (float)(chartArea.Left + i * horizontalScale);
                    var x2 = (float)(chartArea.Left + (i + 1) * horizontalScale);
                    var y1 = (float)(chartArea.Bottom - (dataPoints[i] - minDataPoint) * verticalScale);                  
                    var y2 = (float)(chartArea.Bottom - (dataPoints[i + 1] - minDataPoint) * verticalScale);

                    using (Pen linePen = new Pen(_markerColor, _lineWeight))
                    { graphics.DrawEllipse(linePen, new Rectangle((int)x1,(int)y1,3,3)); }

                }
            }
            else if (_controlType == ControlType.LineDot)
            {
                // Draw the lines connecting data points
                for (int i = 0; i < dataPoints.Length - 1; i++)
                {
                    var x1 = (float)(chartArea.Left + i * horizontalScale);
                    var x2 = (float)(chartArea.Left + (i + 1) * horizontalScale);
                    var y1 = (float)(chartArea.Bottom - (dataPoints[i] - minDataPoint) * verticalScale);
                    var y2 = (float)(chartArea.Bottom - (dataPoints[i + 1] - minDataPoint) * verticalScale);

                    using (Pen linePen = new Pen(_lineColor, _lineWeight))
                    { graphics.DrawLine(linePen, x1, y1, x2, y2); }
                    using (Brush markBrush = new SolidBrush(_markerColor))
                    { graphics.FillEllipse(markBrush, new Rectangle((int)x1, (int)y1-3, 6, 6)); }
                    
                }
            }

            else if (_controlType == ControlType.LineDualArray)
            {
                horizontalScale = chartArea.Width / dataPoints2.Sum();
                int startX = 0;
                // Draw the lines connecting data points
                for (int i = 0; i < dataPoints.Length - 1; i++)
                {
                    var x1 = (float)(chartArea.Left + 0 * horizontalScale);
                    var x2 = (float)(chartArea.Left + (dataPoints2[i]) * horizontalScale);

                    var y1 = (float)(chartArea.Bottom - (dataPoints[i] - minDataPoint) * verticalScale);
                    var y2 = y1;

                    using (Pen linePen = new Pen(_lineColor, _lineWeight))
                    { graphics.DrawLine(linePen, x1, y1, x2, y2); }

                    startX += (int)x2;

                }
            }
            else
            {
                // Draw the lines connecting data points
                for (int i = 0; i < dataPoints.Length - 1; i++)
                {
                    var x1 = (float)(chartArea.Left + i * horizontalScale);
                    var y1 = (float)(chartArea.Bottom - (dataPoints[i] - minDataPoint) * verticalScale);
                    var x2 = (float)(chartArea.Left + (i + 1) * horizontalScale);
                    var y2 = (float)(chartArea.Bottom - (dataPoints[i + 1] - minDataPoint) * verticalScale);

                    using (Pen linePen = new Pen(_lineColor, _lineWeight))
                    { graphics.DrawLine(linePen, x1, y1, x2, y2); }

                }
            }
            
        }
    }
}