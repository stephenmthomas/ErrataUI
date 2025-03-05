using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;

namespace ErrataUI
{
    public partial class ErrataListBox : UserControl
    {
        private bool _ignoreRoles = true;
        [Browsable(true)]
        [Category("Theme Manager")]
        [Description("If true, role updates will be ignored, allowing individual theme application.")]
        public bool IgnoreRoles
        {
            get => _ignoreRoles;
            set
            {
                _ignoreRoles = value;
            }
        }

        private bool _ignoreTheme;
        [Browsable(true)]
        [Category("Theme Manager")]
        [Description("If true, color updates will be ignored, allowing manual color selection.")]
        public bool IgnoreTheme
        {
            get => _ignoreTheme;
            set
            {
                _ignoreTheme = value;
                UpdateColor();  // Update color immediately if the property changes
            }
        }



        #region BORDER
        //BORDER COLOR
        private UIRole _borderRole = UIRole.EmphasizedBorders;
        private ThemeColorShade _borderTheme = ThemeColorShade.Primary_500;
        private Color _borderColor = Color.FromArgb(0, 128, 200);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole BorderRole { get => _borderRole; set { _borderRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade BorderTheme
        {
            get => _borderTheme; set
            {
                _borderTheme = value;
                if (!_ignoreTheme) { BorderColor = ThemeManager.Instance.GetThemeColorShade(_borderTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color BorderColor
        {
            get => pnlBorder.BorderColor;
            set
            {
                pnlBorder.BorderColor = value;
                Invalidate();
            }
        }

        #endregion

        #region LISTBOXBACK
        //LISTBOXBACK COLOR
        private UIRole _listBoxBackRole = UIRole.InputBackground;
        private ThemeColorShade _listBoxBackTheme = ThemeColorShade.Neutral_100;
        private Color _listBoxBackColor = Color.FromArgb(250, 250, 250);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole ListBoxBackRole { get => _listBoxBackRole; set { _listBoxBackRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade ListBoxBackTheme
        {
            get => _listBoxBackTheme; set
            {
                _listBoxBackTheme = value;
                if (!_ignoreTheme) { ListBoxBackColor = ThemeManager.Instance.GetThemeColorShade(_listBoxBackTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color ListBoxBackColor { get => listBox.BackColor; set { listBox.BackColor = value; Invalidate(); } }
        #endregion

        #region LISTBOXFORE
        //LISTBOXFORE COLOR
        private UIRole _listBoxForeRole = UIRole.InputText;
        private ThemeColorShade _listBoxForeTheme = ThemeColorShade.Neutral_900;
        private Color _listBoxForeColor = Color.FromArgb(50, 49, 48);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole ListBoxForeRole { get => _listBoxForeRole; set { _listBoxForeRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade ListBoxForeTheme
        {
            get => _listBoxForeTheme; set
            {
                _listBoxForeTheme = value;
                if (!_ignoreTheme) { ListBoxForeColor = ThemeManager.Instance.GetThemeColorShade(_listBoxForeTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color ListBoxForeColor { get => listBox.ForeColor; set { listBox.ForeColor = value; Invalidate(); } }
        #endregion

        #region SELECTEDITEMBACK
        //SELECTEDITEMBACK COLOR
        private UIRole _selectedItemBackRole = UIRole.SelectedBackground;
        private ThemeColorShade _selectedItemBackTheme = ThemeColorShade.Primary_500;
        private Color _selectedItemBackColor = Color.FromArgb(0, 128, 200);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole SelectedItemBackRole { get => _selectedItemBackRole; set { _selectedItemBackRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade SelectedItemBackTheme
        {
            get => _selectedItemBackTheme; set
            {
                _selectedItemBackTheme = value;
                if (!_ignoreTheme) { SelectedItemBackColor = ThemeManager.Instance.GetThemeColorShade(_selectedItemBackTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color SelectedItemBackColor { get => _selectedItemBackColor; set { _selectedItemBackColor = value; Invalidate(); } }

        #endregion

        #region SELECTEDITEMFORE
        //SELECTEDITEMFORE COLOR
        private UIRole _selectedItemForeRole = UIRole.PrimaryButtonText;
        private ThemeColorShade _selectedItemForeTheme = ThemeColorShade.Neutral_100;
        private Color _selectedItemForeColor = Color.FromArgb(250, 250, 250);
        [Category("UIRole")]
        [Description("Implements role type.")]
        public UIRole SelectedItemForeRole { get => _selectedItemForeRole; set { _selectedItemForeRole = value; } }
        [Category("Theme Manager")]
        [Description("Theme shade.")]
        public ThemeColorShade SelectedItemForeTheme
        {
            get => _selectedItemForeTheme; set
            {
                _selectedItemForeTheme = value;
                if (!_ignoreTheme) { SelectedItemForeColor = ThemeManager.Instance.GetThemeColorShade(_selectedItemForeTheme); }
            }
        }
        [Category("Misc")]
        [Description("Color.")]
        public Color SelectedItemForeColor { get => _selectedItemForeColor; set { _selectedItemForeColor = value; Invalidate(); } }

        #endregion





        [Category("Misc")]
        [Description("Gets or sets the selected index.")]
        public int BorderSize
        {
            get => pnlBorder.BorderThickness;
            set
            {
                pnlBorder.BorderThickness = value;
                pnlBorder.Padding = new Padding(value);
            }
        }

        private void UpdateColor()
        {
            if (!_ignoreTheme)
            {
                BorderColor = ThemeManager.Instance.GetThemeColorShade(BorderTheme);
                ListBoxBackColor = ThemeManager.Instance.GetThemeColorShade(ListBoxBackTheme);
                ListBoxForeColor = ThemeManager.Instance.GetThemeColorShade(ListBoxForeTheme);
                SelectedItemForeColor = ThemeManager.Instance.GetThemeColorShade(SelectedItemForeTheme);
                SelectedItemBackColor = ThemeManager.Instance.GetThemeColorShade(SelectedItemBackTheme);
            }
        }


        public ErrataListBox()
        {
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();
            InitializeComponent();
            pnlBorder.Padding = new Padding(2);
            this.Padding = new Padding(0);
            listBox.SelectedIndexChanged += (s, e) => SelectedIndexChanged?.Invoke(this, e);
            listBox.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            listBox.DrawMode = DrawMode.OwnerDrawFixed; // Enable custom drawing.
            listBox.DrawItem += ListBox_DrawItem;
        }

        private void ListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            ListBox listBox = sender as ListBox;

            // Determine if the item is selected.
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            // Background color.
            using (Brush backgroundBrush = new SolidBrush(isSelected ? _selectedItemBackColor : listBox.BackColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);
            }

            // Foreground color.
            using (Brush textBrush = new SolidBrush(isSelected ? _selectedItemForeColor : listBox.ForeColor))
            {
                e.Graphics.DrawString(
                    listBox.Items[e.Index].ToString(),
                    e.Font,
                    textBrush,
                    e.Bounds,
                    StringFormat.GenericDefault
                );
            }

            // Draw focus rectangle if necessary.
            e.DrawFocusRectangle();
        }











        [Category("Misc")]
        [Description("Gets or sets the padding for the list box.")]
        public Padding ListBoxPadding
        {
            get => listBox.Padding;
            set => listBox.Padding = value;
        }

        [Category("Misc")]
        [Description("Gets or sets the padding for the border.")]
        public Padding BorderPadding
        {
            get => pnlBorder.Padding;
            set => pnlBorder.Padding = value;
        }










        [Category("Behavior")]
        [Description("Gets or sets the selected index.")]
        public int SelectedIndex
        {
            get => listBox.SelectedIndex;
            set => listBox.SelectedIndex = value;
        }

        [Category("Behavior")]
        [Description("Gets the selected item.")]
        public object SelectedItem => listBox.SelectedItem;

        [Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Gets the collection of items in the list box.")]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor))]
        [MergableProperty(false)]
        public ListBox.ObjectCollection Items => listBox.Items;




        [Category("Behavior")]
        [Description("Occurs when the selected index changes.")]
        public event EventHandler SelectedIndexChanged;

        public void AddItem(object item)
        {
            listBox.Items.Add(item);
        }

        public void RemoveItem(object item)
        {
            listBox.Items.Remove(item);
        }

        public void ClearItems()
        {
            listBox.Items.Clear();
        }

        private void ErrataListBox_Load(object sender, EventArgs e)
        {

        }
    }
}
