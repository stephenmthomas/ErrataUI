using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;
using System.IO;

namespace ErrataUI
{
    public class ErrataDataGridView : DataGridView
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

        public enum CustomCellBorderStyle
        {
            None,
            Single,
            SingleHorizontal,
            SingleVertical,
            Dashed,
            Dotted
        }

        private CustomCellBorderStyle _cellBorderStyle = CustomCellBorderStyle.Single;
        [Category("Misc"), Description("Select the border style for the cells.")]
        public CustomCellBorderStyle CellBorderStyleSelection
        {
            get => _cellBorderStyle;
            set
            {
                _cellBorderStyle = value;
                ApplyCellBorderStyle();
            }
        }


        private Font _cellFont = new Font("Consolas", 8F, FontStyle.Regular); // Default font
        [Category("Misc"), Description("Select the font for the grid cells.")]
        public Font CellFont
        {
            get => _cellFont;
            set
            {
                _cellFont = value;
                ApplyFont();
            }
        }


        #region SECONDARYCOLOR
        //SECONDARYCOLOR
        private UIRole _secondaryColorRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _secondaryColorTheme = ThemeColorShade.Secondary_500;
        private Color _secondaryColor = Color.FromArgb(0, 128, 200);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole SecondaryColorRole { get => _secondaryColorRole; set { _secondaryColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade SecondaryColorTheme
        {
            get => _secondaryColorTheme; set
            {
                _secondaryColorTheme = value;
                if (!_ignoreTheme) { SecondaryColor = ThemeManager.Instance.GetThemeColorShade(_secondaryColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color SecondaryColor { get => _secondaryColor; set { _secondaryColor = value; Invalidate(); } }
        #endregion
        #region HEADERCOLOR
        //PRIMARYCOLOR
        private UIRole _headerColorRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _headerColorTheme = ThemeColorShade.Primary_500;
        private Color _headerColor = Color.FromArgb(0, 128, 200);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole HeaderColorRole { get => _headerColorRole; set { _headerColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade HeaderColorTheme
        {
            get => _headerColorTheme; set
            {
                _headerColorTheme = value;
                if (!_ignoreTheme) { PrimaryColor = ThemeManager.Instance.GetThemeColorShade(_headerColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color PrimaryColor { get => _headerColor; set { _headerColor = value; Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region ALTERNATEROWCOLOR
        //ALTERNATEROWCOLOR
        private UIRole _alternateRowColorRole = UIRole.HeadingText;
        private ThemeColorShade _alternateRowColorTheme = ThemeColorShade.Neutral_300;
        private Color _alternateRowColor = Color.FromArgb(250, 250, 250);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole AlternateRowColorRole { get => _alternateRowColorRole; set { _alternateRowColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade AlternateRowColorTheme
        {
            get => _alternateRowColorTheme; set
            {
                _alternateRowColorTheme = value;
                if (!_ignoreTheme) { AlternateRowColor = ThemeManager.Instance.GetThemeColorShade(_alternateRowColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color AlternateRowColor { get => _alternateRowColor; set { _alternateRowColor = value; Invalidate(); } }
        #endregion
        #region ROWHEADERBACKCOLOR
        //HEADERBACKCOLOR
        private UIRole _rowHeaderBackColorRole = UIRole.HeadingText;
        private ThemeColorShade _rowHeaderBackColorTheme = ThemeColorShade.Neutral_500;
        private Color _rowHeaderBackColor = Color.FromArgb(128, 128, 128);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole HeaderBackColorRole { get => _rowHeaderBackColorRole; set { _rowHeaderBackColorRole = value; } }

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
        #endregion
        #region GRIDLINECOLOR
        //GRIDLINECOLOR
        private UIRole _gridLineColorRole = UIRole.HeadingText;
        private ThemeColorShade _gridLineColorTheme = ThemeColorShade.Neutral_500;
        private Color _gridLineColor = Color.FromArgb(128, 128, 128);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole GridLineColorRole { get => _gridLineColorRole; set { _gridLineColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade GridLineColorTheme
        {
            get => _gridLineColorTheme; set
            {
                _gridLineColorTheme = value;
                if (!_ignoreTheme) { GridLineColor = ThemeManager.Instance.GetThemeColorShade(_gridLineColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color GridLineColor { get => _gridLineColor; set { _gridLineColor = value; ApplyStyles(); Invalidate(); } }
        #endregion
        #region CELLBACKGROUNDCOLOR
        //CELLBACKGROUNDCOLOR
        private UIRole _cellBackgroundColorRole = UIRole.MainBackground;
        private ThemeColorShade _cellBackgroundColorTheme = ThemeColorShade.Neutral_50;
        private Color _cellBackgroundColor = Color.FromArgb(250, 250, 250);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole CellBackgroundColorRole { get => _cellBackgroundColorRole; set { _cellBackgroundColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade CellBackgroundColorTheme
        {
            get => _cellBackgroundColorTheme; set
            {
                _cellBackgroundColorTheme = value;
                if (!_ignoreTheme) { CellBackgroundColor = ThemeManager.Instance.GetThemeColorShade(_cellBackgroundColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color CellBackgroundColor { get => _cellBackgroundColor; set { _cellBackgroundColor = value; ApplyStyles(); Invalidate(); } }

        //ADD TO UPDATECOLOR METHOD
        ////
        #endregion
        #region CELLTEXTCOLOR
        //CELLTEXTCOLOR
        private UIRole _cellTextColorRole = UIRole.BodyTextL3;
        private ThemeColorShade _cellTextColorTheme = ThemeColorShade.Neutral_600;
        private Color _cellTextColor = Color.FromArgb(50, 49, 48);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole CellTextColorRole { get => _cellTextColorRole; set { _cellTextColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade CellTextColorTheme
        {
            get => _cellTextColorTheme; set
            {
                _cellTextColorTheme = value;
                if (!_ignoreTheme) { CellTextColor = ThemeManager.Instance.GetThemeColorShade(_cellTextColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color CellTextColor { get => _cellTextColor; set { _cellTextColor = value; ApplyStyles();  Invalidate(); } }
        #endregion
        #region SELECTIONBACKGROUNDCOLOR
        //SELECTIONBACKGROUNDCOLOR
        private UIRole _selectionBackgroundColorRole = UIRole.PrimaryButtonBackground;
        private ThemeColorShade _selectionBackgroundColorTheme = ThemeColorShade.Primary_500;
        private Color _selectionBackgroundColor = Color.FromArgb(0, 128, 200);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole SelectionBackgroundColorRole { get => _selectionBackgroundColorRole; set { _selectionBackgroundColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade SelectionBackgroundColorTheme
        {
            get => _selectionBackgroundColorTheme; set
            {
                _selectionBackgroundColorTheme = value;
                if (!_ignoreTheme) { SelectionBackgroundColor = ThemeManager.Instance.GetThemeColorShade(_selectionBackgroundColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color SelectionBackgroundColor { get => _selectionBackgroundColor; set { _selectionBackgroundColor = value; Invalidate(); } }
        #endregion
        #region SELECTIONTEXTCOLOR
        //SELECTIONTEXTCOLOR
        private UIRole _selectionTextColorRole = UIRole.DisabledText;
        private ThemeColorShade _selectionTextColorTheme = ThemeColorShade.Neutral_300;
        private Color _selectionTextColor = Color.FromArgb(50, 49, 48);

        [Category("UIRole"), Description("Implements role type.")]
        public UIRole SelectionTextColorRole { get => _selectionTextColorRole; set { _selectionTextColorRole = value; } }

        [Category("Theme Manager"), Description("Theme shade.")]
        public ThemeColorShade SelectionTextColorTheme
        {
            get => _selectionTextColorTheme; set
            {
                _selectionTextColorTheme = value;
                if (!_ignoreTheme) { SelectionTextColor = ThemeManager.Instance.GetThemeColorShade(_selectionTextColorTheme); }
            }
        }

        [Category("Misc"), Description("Color.")]
        public Color SelectionTextColor { get => _selectionTextColor; set { _selectionTextColor = value; ApplyStyles();  Invalidate(); } }
        #endregion




        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                SelectionBackgroundColor = ThemeManager.Instance.GetThemeColorShade(SelectionBackgroundColorTheme);
                SelectionTextColor = ThemeManager.Instance.GetThemeColorShade(SelectionTextColorTheme);
                CellTextColor = ThemeManager.Instance.GetThemeColorShade(CellTextColorTheme);
                CellBackgroundColor = ThemeManager.Instance.GetThemeColorShade(CellBackgroundColorTheme);
                GridLineColor = ThemeManager.Instance.GetThemeColorShade(GridLineColorTheme);
                RowHeaderBackColor = ThemeManager.Instance.GetThemeColorShade(RowHeaderBackColorTheme);
                AlternateRowColor = ThemeManager.Instance.GetThemeColorShade(AlternateRowColorTheme);
                PrimaryColor = ThemeManager.Instance.GetThemeColorShade(HeaderColorTheme);
                SecondaryColor = ThemeManager.Instance.GetThemeColorShade(SecondaryColorTheme);
            }
        }


        public ErrataDataGridView()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            // Basic properties
            this.BorderStyle = BorderStyle.None;
            this.BackgroundColor = Color.FromArgb(240, 240, 240);
            this.EnableHeadersVisualStyles = false;
            this.GridColor = Color.FromArgb(200, 200, 200);
            
            LoadCsvToDataGridView("C:\\Users\\Stephen\\source\\repos\\ErrataUI\\ErrataUI\\csv_sample.csv");
            // Apply the initial styles
            ApplyStyles();

        }

        private void LoadCsvToDataGridView(string filePath)
        {
            DataTable dt = new DataTable();
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 0)
                {
                    string[] headers = lines[0].Split(',');
                    foreach (string header in headers)
                    {
                        dt.Columns.Add(header);
                    }
                    for (int i = 1; i < lines.Length; i++) // Start from 1 to skip the header row
                    {
                        string[] row = lines[i].Split(',');
                        dt.Rows.Add(row);
                    }
                }
                this.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading CSV: " + ex.Message);
            }
        }


        private void ApplyCellBorderStyle()
        {
            // Map CustomCellBorderStyle enum to DataGridViewCellBorderStyle
            switch (_cellBorderStyle)
            {
                case CustomCellBorderStyle.None:
                    this.CellBorderStyle = DataGridViewCellBorderStyle.None;
                    break;
                case CustomCellBorderStyle.Single:
                    this.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                    break;
                case CustomCellBorderStyle.SingleHorizontal:
                    this.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    break;
                case CustomCellBorderStyle.SingleVertical:
                    this.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
                    break;
                case CustomCellBorderStyle.Dashed:
                    // Unfortunately, DataGridView does not directly support dashed borders.
                    // You can use custom painting if needed, but for simplicity, we will set it to Single.
                    this.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                    break;
                case CustomCellBorderStyle.Dotted:
                    // Similar to Dashed, dotted borders need custom painting.
                    this.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                    break;
            }
        }


        private void ApplyFont()
        {
            this.ColumnHeadersDefaultCellStyle.Font = _cellFont;
            this.DefaultCellStyle.Font = _cellFont;
            this.RowHeadersDefaultCellStyle.Font = _cellFont;
            this.AlternatingRowsDefaultCellStyle.Font = _cellFont;
            this.Refresh();
            Invalidate();
        }


        private void ApplyStyles()
        {
            ApplyFont();
            ApplyCellBorderStyle();
            // Column headers style
            this.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = _headerColor,
                ForeColor = _cellTextColor,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0) // Add padding for a modern look
            };

            // Default cell style
            this.DefaultCellStyle = new DataGridViewCellStyle
            {

                BackColor = _cellBackgroundColor,
                ForeColor = _cellTextColor,
                SelectionBackColor = _selectionBackgroundColor,
                SelectionForeColor = _selectionTextColor,
                Alignment = DataGridViewContentAlignment.MiddleLeft

            };

            // Row headers style
            this.RowHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = _rowHeaderBackColor,
                ForeColor = _cellTextColor,
                SelectionBackColor = _selectionBackgroundColor,
                SelectionForeColor = _selectionTextColor,

            };

            this.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.GridColor = _gridLineColor;

            // Alternating rows style
            this.RowTemplate.Height = 30;

            // Refresh the control to apply changes
            this.Refresh();
            Invalidate();
        }




       
    }
}
