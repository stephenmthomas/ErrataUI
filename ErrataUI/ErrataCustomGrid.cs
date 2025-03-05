using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{


    public class ErrataCustomGrid : Control
    {
        private int _rows = 5;
        private int _cols = 5;
        private Dictionary<(int, int), Color> _cellColors = new();
        private Dictionary<(int, int), string> _cellValues = new();
        private Font _cellFont = new Font("Segoe UI Semibold", 10, FontStyle.Regular);

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



        private bool _centered = true;
        [Category("Misc")]
        public bool Centered
        {
            get => _centered;
            set
            {
                _centered = value; Invalidate();
            }
        }


        private bool _rowHeaderDraw = true;
        [Category("Misc")]
        public bool RowHeaderDraw
        {
            get => _rowHeaderDraw;
            set
            {
                _rowHeaderDraw = value;
                if (_rowHeaderDraw == false) { _rootCell = false; }
                Invalidate();
            }
        }

        private BindingList<string> _rowHeaders = new BindingList<string>();
        // Row Headers collection exposed to designer
        [Browsable(true)]
        [Category("Misc")]
        [Description("Collection of row headers")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BindingList<string> RowHeaders
        {
            get { return _rowHeaders; }
            set { _rowHeaders = value; Invalidate(); }
        }




        private bool _colHeaderDraw = true;
        [Category("Misc")]
        public bool ColHeaderDraw
        {
            get => _colHeaderDraw;
            set
            {
                _colHeaderDraw = value; 
                if (_colHeaderDraw == false) { _rootCell = false; }
                Invalidate();
            }
        }

        private BindingList<string> _colHeaders = new BindingList<string>();
        // Column Headers collection exposed to designer
        [Browsable(true)]
        [Category("Misc")]
        [Description("Collection of column headers")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BindingList<string> ColHeaders
        {
            get { return _colHeaders; }
            set { _colHeaders = value; Invalidate(); }
        }


        private bool _rootCell = true;
        [Category("Misc")]
        [Description("Draw the root cell? Only works if both row and column headers are enabled.")]
        public bool RootCell
        {
            get => _rootCell;
            set
            {
                if (_colHeaderDraw == false || _rowHeaderDraw == false)
                {
                    _rootCell = false; Invalidate();
                }
                else
                {
                    _rootCell = value; Invalidate();
                }
                
            }
        }

        private string _rootBoxText = "Grid Text";
        [Category("Misc")]
        [Description("Sets text.")]
        public string RootCellText
        {
            get => _rootBoxText;
            set
            {
                _rootBoxText = value;
                Invalidate(); // Repaint the control when text changes
            }
        }


        #region ROOTCELLBACKCOLOR
        //ROOTCELLBACKCOLOR
        private UIRole _rootCellBackColorRole = UIRole.TitleBar;
        private ThemeColorShade _rootCellBackColorTheme = ThemeColorShade.Primary_900;
        private Color _rootCellBackColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_900);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole RootCellBackColorRole { get => _rootCellBackColorRole; set { _rootCellBackColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade RootCellBackColorTheme
        {
            get => _rootCellBackColorTheme; set
            {
                _rootCellBackColorTheme = value;
                if (!_ignoreTheme) { RootCellBackColor = ThemeManager.Instance.GetThemeColorShade(_rootCellBackColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color RootCellBackColor { get => _rootCellBackColor; set { _rootCellBackColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region ROOTCELLFORECOLOR
        //ROOTCELLFORECOLOR
        private UIRole _rootCellForeColorRole = UIRole.TitleBar;
        private ThemeColorShade _rootCellForeColorTheme = ThemeColorShade.Neutral_200;
        private Color _rootCellForeColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_200);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole RootCellForeColorRole { get => _rootCellForeColorRole; set { _rootCellForeColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade RootCellForeColorTheme
        {
            get => _rootCellForeColorTheme; set
            {
                _rootCellForeColorTheme = value;
                if (!_ignoreTheme) { RootCellForeColor = ThemeManager.Instance.GetThemeColorShade(_rootCellForeColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color RootCellForeColor { get => _rootCellForeColor; set { _rootCellForeColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion


        #region CELLDEFAULTBACKCOLOR
        //CELLDEFAULTBACKCOLOR
        private UIRole _cellDefaultBackColorRole = UIRole.TitleBar;
        private ThemeColorShade _cellDefaultBackColorTheme = ThemeColorShade.Primary_500;
        private Color _cellDefaultBackColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_500);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole CellDefaultBackColorRole { get => _cellDefaultBackColorRole; set { _cellDefaultBackColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade CellDefaultBackColorTheme
        {
            get => _cellDefaultBackColorTheme; set
            {
                _cellDefaultBackColorTheme = value;
                if (!_ignoreTheme) { CellDefaultBackColor = ThemeManager.Instance.GetThemeColorShade(_cellDefaultBackColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color CellDefaultBackColor { get => _cellDefaultBackColor; set { _cellDefaultBackColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region CELLBORDERCOLOR
        //CELLBORDERCOLOR
        private UIRole _cellBorderColorRole = UIRole.TitleBar;
        private ThemeColorShade _cellBorderColorTheme = ThemeColorShade.Neutral_700;
        private Color _cellBorderColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_700);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole CellBorderColorRole { get => _cellBorderColorRole; set { _cellBorderColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade CellBorderColorTheme
        {
            get => _cellBorderColorTheme; set
            {
                _cellBorderColorTheme = value;
                if (!_ignoreTheme) { CellBorderColor = ThemeManager.Instance.GetThemeColorShade(_cellBorderColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color CellBorderColor { get => _cellBorderColor; set { _cellBorderColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region CELLFORECOLOR
        //CELLFORECOLOR
        private UIRole _cellForeColorRole = UIRole.TitleBar;
        private ThemeColorShade _cellForeColorTheme = ThemeColorShade.Neutral_200;
        private Color _cellForeColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_200);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole CellForeColorRole { get => _cellForeColorRole; set { _cellForeColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade CellForeColorTheme
        {
            get => _cellForeColorTheme; set
            {
                _cellForeColorTheme = value;
                if (!_ignoreTheme) { CellForeColor = ThemeManager.Instance.GetThemeColorShade(_cellForeColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color CellForeColor { get => _cellForeColor; set { _cellForeColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion

        #region ROWHEADERFORECOLOR
        //ROWHEADERFORECOLOR
        private UIRole _rowHeaderForeColorRole = UIRole.TitleBar;
        private ThemeColorShade _rowHeaderForeColorTheme = ThemeColorShade.Neutral_200;
        private Color _rowHeaderForeColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_200);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole RowHeaderForeColorRole { get => _rowHeaderForeColorRole; set { _rowHeaderForeColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade RowHeaderForeColorTheme
        {
            get => _rowHeaderForeColorTheme; set
            {
                _rowHeaderForeColorTheme = value;
                if (!_ignoreTheme) { RowHeaderForeColor = ThemeManager.Instance.GetThemeColorShade(_rowHeaderForeColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color RowHeaderForeColor { get => _rowHeaderForeColor; set { _rowHeaderForeColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region ROWHEADERBACKCOLOR
        //ROWHEADERBACKCOLOR
        private UIRole _rowHeaderBackColorRole = UIRole.TitleBar;
        private ThemeColorShade _rowHeaderBackColorTheme = ThemeColorShade.Primary_800;
        private Color _rowHeaderBackColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_800);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole RowHeaderBackColorRole { get => _rowHeaderBackColorRole; set { _rowHeaderBackColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade RowHeaderBackColorTheme
        {
            get => _rowHeaderBackColorTheme; set
            {
                _rowHeaderBackColorTheme = value;
                if (!_ignoreTheme) { RowHeaderBackColor = ThemeManager.Instance.GetThemeColorShade(_rowHeaderBackColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color RowHeaderBackColor { get => _rowHeaderBackColor; set { _rowHeaderBackColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion

        #region COLHEADERFORECOLOR
        //COLHEADERFORECOLOR
        private UIRole _colHeaderForeColorRole = UIRole.TitleBar;
        private ThemeColorShade _colHeaderForeColorTheme = ThemeColorShade.Neutral_200;
        private Color _colHeaderForeColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Neutral_200);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole ColHeaderForeColorRole { get => _colHeaderForeColorRole; set { _colHeaderForeColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade ColHeaderForeColorTheme
        {
            get => _colHeaderForeColorTheme; set
            {
                _colHeaderForeColorTheme = value;
                if (!_ignoreTheme) { ColHeaderForeColor = ThemeManager.Instance.GetThemeColorShade(_colHeaderForeColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color ColHeaderForeColor { get => _colHeaderForeColor; set { _colHeaderForeColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region COLHEADERBACKCOLOR
        //COLHEADERBACKCOLOR
        private UIRole _colHeaderBackColorRole = UIRole.TitleBar;
        private ThemeColorShade _colHeaderBackColorTheme = ThemeColorShade.Primary_800;
        private Color _colHeaderBackColor = ThemeManager.Instance.GetThemeColorShade(ThemeColorShade.Primary_800);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole ColHeaderBackColorRole { get => _colHeaderBackColorRole; set { _colHeaderBackColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade ColHeaderBackColorTheme
        {
            get => _colHeaderBackColorTheme; set
            {
                _colHeaderBackColorTheme = value;
                if (!_ignoreTheme) { ColHeaderBackColor = ThemeManager.Instance.GetThemeColorShade(_colHeaderBackColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color ColHeaderBackColor { get => _colHeaderBackColor; set { _colHeaderBackColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion

        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                CellDefaultBackColor = ThemeManager.Instance.GetThemeColorShadeOffset(CellDefaultBackColorTheme);
                CellBorderColor = ThemeManager.Instance.GetThemeColorShadeOffset(CellBorderColorTheme);
                CellForeColor = ThemeManager.Instance.GetThemeColorShadeOffset(CellForeColorTheme);
                RowHeaderForeColor = ThemeManager.Instance.GetThemeColorShadeOffset(RowHeaderForeColorTheme);
                RowHeaderBackColor = ThemeManager.Instance.GetThemeColorShadeOffset(RowHeaderBackColorTheme);
                ColHeaderForeColor = ThemeManager.Instance.GetThemeColorShadeOffset(ColHeaderForeColorTheme);
                ColHeaderBackColor = ThemeManager.Instance.GetThemeColorShadeOffset(ColHeaderBackColorTheme);
                RootCellBackColor = ThemeManager.Instance.GetThemeColorShadeOffset(RootCellBackColorTheme);
                RootCellForeColor = ThemeManager.Instance.GetThemeColorShadeOffset(RootCellForeColorTheme);
            }
        }

        public ErrataCustomGrid()
        {
            this.DoubleBuffered = true;
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular);
        }

        public int Rows
        {
            get => _rows;
            set { _rows = Math.Max(1, value); Invalidate(); }
        }

        public int Columns
        {
            get => _cols;
            set { _cols = Math.Max(1, value); Invalidate(); }
        }

        public void SetCellColor(int row, int col, Color color)
        {
            _cellColors[(row, col)] = color;
            Invalidate();
        }

        public void SetCellValue(int row, int col, string value)
        {
            _cellValues[(row, col)] = value;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            int _colAdd = _rowHeaderDraw ? 1 : 0;
            int _rowAdd = _colHeaderDraw ? 1 : 0;

            int cellWidth = Width / (_cols + _colAdd);
            int cellHeight = Height / (_rows + _rowAdd);

            int widthRemainder = Width - (cellWidth * (_cols + _colAdd));
            int heightRemainder = Height - (cellHeight * (_rows + _rowAdd));

            int x_offset = 0;
            int y_offset = 0;
            

            if (_centered)
            {
                x_offset = widthRemainder / 2;
                y_offset = heightRemainder / 2;
            }


            using (Pen borderPen = new Pen(CellBorderColor))
            {
                // Draw column headers

                if (ColHeaderDraw)
                {
                    for (int j = 0; j < _colHeaders.Count; j++)
                    {
                        Rectangle rect = new Rectangle((j + _colAdd) * cellWidth + x_offset, 0 + y_offset, cellWidth, cellHeight);

                        using (Brush brush = new SolidBrush(ColHeaderBackColor))
                        {
                            g.FillRectangle(brush, rect);
                        }

                        g.DrawRectangle(borderPen, rect);

                        string text = _colHeaders[j];
                        SizeF textSize = g.MeasureString(text, Font);
                        PointF textPos = new PointF(
                            rect.X + (cellWidth - textSize.Width) / 2,
                            rect.Y + (cellHeight - textSize.Height) / 2);
                        using (Brush textBrush = new SolidBrush(ColHeaderForeColor))
                        {
                            g.DrawString(text, Font, textBrush, textPos);
                        }
                    }
                }

                

                // Draw row headers
                if (RowHeaderDraw)
                {
                    for (int i = 0; i < _rowHeaders.Count; i++)
                    {
                        Rectangle rect = new Rectangle(0 + x_offset,(i + _rowAdd) * cellHeight + y_offset,cellWidth, cellHeight);

                        using (Brush brush = new SolidBrush(RowHeaderBackColor))
                        {
                            g.FillRectangle(brush, rect);
                        }

                        g.DrawRectangle(borderPen, rect);

                        string text = _rowHeaders[i];
                        SizeF textSize = g.MeasureString(text, Font);
                        PointF textPos = new PointF(
                            rect.X + (cellWidth - textSize.Width) / 2,
                            rect.Y + (cellHeight - textSize.Height) / 2);
                        using (Brush textBrush = new SolidBrush(RowHeaderForeColor))
                        {
                            g.DrawString(text, Font, textBrush, textPos);
                        }
                    }
                }
                



                //Draw OriginBox
                // Now handle the (0,0) box (top-left corner intersection)
                if (RootCell)
                {
                    Rectangle topLeftRect = new Rectangle(
                                        0 + x_offset,
                                        0 + y_offset,
                                        cellWidth, cellHeight);

                    using (Brush topLeftBrush = new SolidBrush(RootCellBackColor))  // Color for the (0,0) cell
                    {
                        g.FillRectangle(topLeftBrush, topLeftRect);
                    }

                    g.DrawRectangle(borderPen, topLeftRect);  // Border for the (0,0) cell

                    // Optionally, you can add a label to the (0,0) box if needed (like a blank space or label)
                    string text = _rootBoxText;
                    SizeF textSize = g.MeasureString(text, Font);
                    PointF textPos = new PointF(
                        topLeftRect.X + (cellWidth - textSize.Width) / 2,
                        topLeftRect.Y + (cellHeight - textSize.Height) / 2);
                    using (Brush textBrush = new SolidBrush(RootCellForeColor))
                    {
                        g.DrawString(text, Font, textBrush, textPos);
                    }
                }
                



                //DRAW THE MAIN GRID
                for (int i = 0; i < _rows; i++)
                {
                    for (int j = 0; j < _cols; j++)
                    {
                        // Apply the offset to center the grid
                        Rectangle rect = new Rectangle(
                            (j + _colAdd) * cellWidth + x_offset,  // Adjust X position with offset
                            (i + _rowAdd) * cellHeight + y_offset, // Adjust Y position with offset
                            cellWidth, cellHeight);

                        Color cellColor = _cellColors.ContainsKey((i, j)) ? _cellColors[(i, j)] : CellDefaultBackColor;
                        using (Brush brush = new SolidBrush(cellColor))
                        {
                            g.FillRectangle(brush, rect);
                        }

                        // Draw the border using the custom pen
                        g.DrawRectangle(borderPen, rect);

                        if (_cellValues.ContainsKey((i, j)))
                        {
                            string text = _cellValues[(i, j)];
                            SizeF textSize = g.MeasureString(text, _cellFont);
                            PointF textPos = new PointF(
                                rect.X + (cellWidth - textSize.Width) / 2,
                                rect.Y + (cellHeight - textSize.Height) / 2);
                            using (Brush textBrush = new SolidBrush(CellForeColor))
                            {
                                g.DrawString(text, _cellFont, textBrush, textPos);
                            }
                        }
                    }
                }
            }
        }
    }
}
