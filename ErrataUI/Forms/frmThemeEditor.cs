using ErrataUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;

namespace ErrataUI.Forms
{
    public partial class frmThemeEditor : ErrataForm
    {
        public frmThemeEditor()
        {
            InitializeComponent();
        }

        private void frmToolsThemeEditor_Load(object sender, EventArgs e)
        {
            pbNeutral.BackColor = ThemeManager.Instance.Neutral;
            pbPrimary.BackColor = ThemeManager.Instance.Primary;
            pbSecondary.BackColor = ThemeManager.Instance.Secondary;
            pbSemanticA.BackColor = ThemeManager.Instance.SemanticA;
            pbSemanticB.BackColor = ThemeManager.Instance.SemanticB;
            pbSemanticC.BackColor = ThemeManager.Instance.SemanticC;

            escBrightness.DataPoints = escBrightness.ConvertThemeCurves(ThemeManager.Instance.BrightnessCurve);
            escSaturation.DataPoints = escSaturation.ConvertThemeCurves(ThemeManager.Instance.SaturationCurve);


            cbBrightness.AddItems(ThemeManager.Instance.curveNames);
            cbBrightness.SelectedIndex = 0;
            cbSaturation.AddItems(ThemeManager.Instance.curveNames);
            cbSaturation.SelectedIndex = 1;
        }



        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void pbNeutral_Click(object sender, EventArgs e)
        {
            using (var colorPicker = new frmColorPicker(pbNeutral.BackColor))  // Pass current color
            {
                if (colorPicker.ShowDialog() == DialogResult.OK)
                {
                    if (sender is System.Windows.Forms.Control control)
                    {


                        var themeManager = ThemeManager.Instance;
                        themeManager.UpdateColor(ThemeColor.Neutral, colorPicker.dialogColor);

                        control.BackColor = themeManager.Neutral;

                    }
                }
            }
        }

        private void pbPrimary_Click(object sender, EventArgs e)
        {
            using (var colorPicker = new frmColorPicker(pbPrimary.BackColor))  // Pass current color
            {
                if (colorPicker.ShowDialog() == DialogResult.OK)
                {
                    if (sender is System.Windows.Forms.Control control)
                    {
                        var themeManager = ThemeManager.Instance;
                        themeManager.UpdateColor(ThemeColor.Primary, colorPicker.dialogColor);

                        control.BackColor = themeManager.Primary;
                    }
                }
            }


            //using (ColorDialog colorDialog = new ColorDialog())
            //{
            //    // Show the ColorDialog
            //    if (colorDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        if (sender is System.Windows.Forms.Control control)
            //        {


            //            var themeManager = ThemeManager.Instance;
            //            themeManager.UpdateColor(ThemeColor.Primary, colorDialog.Color);

            //            control.BackColor = themeManager.Primary;

            //        }
            //    }
            //}
        }

        private void pbSecondary_Click(object sender, EventArgs e)
        {

            using (var colorPicker = new frmColorPicker(pbSecondary.BackColor))  // Pass current color
            {
                if (colorPicker.ShowDialog() == DialogResult.OK)
                {
                    if (sender is System.Windows.Forms.Control control)
                    {
                        var themeManager = ThemeManager.Instance;
                        themeManager.UpdateColor(ThemeColor.Secondary, colorPicker.dialogColor);

                        control.BackColor = themeManager.Secondary;
                    }
                }
            }

        }

        private void pbSemanticA_Click(object sender, EventArgs e)
        {
            using (var colorPicker = new frmColorPicker(pbSemanticA.BackColor))  // Pass current color
            {
                if (colorPicker.ShowDialog() == DialogResult.OK)
                {
                    if (sender is System.Windows.Forms.Control control)
                    {


                        var themeManager = ThemeManager.Instance;
                        themeManager.UpdateColor(ThemeColor.SemanticA, colorPicker.dialogColor);

                        control.BackColor = themeManager.SemanticA;

                    }
                }
            }
        }

        private void pbSemanticB_Click(object sender, EventArgs e)
        {
            using (var colorPicker = new frmColorPicker(pbSemanticB.BackColor))  // Pass current color
            {
                if (colorPicker.ShowDialog() == DialogResult.OK)
                {
                    if (sender is System.Windows.Forms.Control control)
                    {


                        var themeManager = ThemeManager.Instance;
                        themeManager.UpdateColor(ThemeColor.SemanticB, colorPicker.dialogColor);

                        control.BackColor = themeManager.SemanticB;

                    }
                }
            }
        }

        private void pbSemanticC_Click(object sender, EventArgs e)
        {
            using (var colorPicker = new frmColorPicker(pbSemanticC.BackColor))  // Pass current color
            {
                if (colorPicker.ShowDialog() == DialogResult.OK)
                {
                    if (sender is System.Windows.Forms.Control control)
                    {


                        var themeManager = ThemeManager.Instance;
                        themeManager.UpdateColor(ThemeColor.SemanticC, colorPicker.dialogColor);

                        control.BackColor = themeManager.SemanticC;

                    }
                }
            }
        }

        private void errataFlatLabel4_Click(object sender, EventArgs e)
        {

        }

        private void cbBrightness_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected text from the ComboBox
            string selectedArrayName = cbBrightness.SelectedItem.ToString();

            // Use reflection to get the float array from ThemeManager
            var arrayField = typeof(ThemeManager).GetField(selectedArrayName,
                              System.Reflection.BindingFlags.Public |
                              System.Reflection.BindingFlags.Static);

            if (arrayField != null)
            {
                // Retrieve the array value
                float[] selectedArray = arrayField.GetValue(null) as float[];

                // Ensure the array is not null before using it
                if (selectedArray != null)
                {
                    // Call ConvertThemeCurves with the selected array
                    escBrightness.DataPoints = escBrightness.ConvertThemeCurves(selectedArray);
                    ThemeManager.Instance.BrightnessCurve = selectedArray;
                    updateAllShades();
                    Invalidate();
                }
            }
        }

        private void cbSaturation_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected text from the ComboBox
            string selectedArrayName = cbSaturation.SelectedItem.ToString();

            // Use reflection to get the float array from ThemeManager
            var arrayField = typeof(ThemeManager).GetField(selectedArrayName,
                              System.Reflection.BindingFlags.Public |
                              System.Reflection.BindingFlags.Static);

            if (arrayField != null)
            {
                Debug.Print($"ArrayField");
                // Retrieve the array value
                float[] selectedArray = arrayField.GetValue(null) as float[];

                // Ensure the array is not null before using it
                if (selectedArray != null)
                {
                    Debug.Print($"selectedArray");
                    // Call ConvertThemeCurves with the selected array
                    escSaturation.DataPoints = escSaturation.ConvertThemeCurves(selectedArray);
                    ThemeManager.Instance.SaturationCurve = selectedArray;
                    updateAllShades();
                    Invalidate();
                }
            }
        }

        private void updateAllShades()
        {
            ThemeManager.Instance.NeutralSwatch = ThemeManager.Instance.GenerateShades(ThemeManager.Instance.Neutral, ThemeManager.Instance.BrightnessCurve, ThemeManager.Instance.SaturationCurve);
            ThemeManager.Instance.PrimarySwatch = ThemeManager.Instance.GenerateShades(ThemeManager.Instance.Primary, ThemeManager.Instance.BrightnessCurve, ThemeManager.Instance.SaturationCurve);
            ThemeManager.Instance.SecondarySwatch = ThemeManager.Instance.GenerateShades(ThemeManager.Instance.Secondary, ThemeManager.Instance.BrightnessCurve, ThemeManager.Instance.SaturationCurve);
            ThemeManager.Instance.SemanticASwatch = ThemeManager.Instance.GenerateShades(ThemeManager.Instance.SemanticA, ThemeManager.Instance.BrightnessCurve, ThemeManager.Instance.SaturationCurve);
            ThemeManager.Instance.SemanticBSwatch = ThemeManager.Instance.GenerateShades(ThemeManager.Instance.SemanticB, ThemeManager.Instance.BrightnessCurve, ThemeManager.Instance.SaturationCurve);
            ThemeManager.Instance.SemanticCSwatch = ThemeManager.Instance.GenerateShades(ThemeManager.Instance.SemanticC, ThemeManager.Instance.BrightnessCurve, ThemeManager.Instance.SaturationCurve);
        }

        private void updateAllColors()
        {
            ThemeManager.Instance.UpdateColor(ThemeColor.Neutral, pbNeutral.BackColor);
            ThemeManager.Instance.UpdateColor(ThemeColor.Primary, pbPrimary.BackColor);
            ThemeManager.Instance.UpdateColor(ThemeColor.Secondary, pbSecondary.BackColor);
            ThemeManager.Instance.UpdateColor(ThemeColor.SemanticA, pbSemanticA.BackColor);
            ThemeManager.Instance.UpdateColor(ThemeColor.SemanticB, pbSemanticB.BackColor);
            ThemeManager.Instance.UpdateColor(ThemeColor.SemanticC, pbSemanticC.BackColor);
        }

        private void errataButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadTheme_Click(object sender, EventArgs e)
        {
            // Create OpenFileDialog instance
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Set file filter to show only SBMC files
                openFileDialog.Filter = "EUI Files (*.eui)|*.eui";
                openFileDialog.DefaultExt = "eui";
                openFileDialog.Multiselect = false;  // Allow only one file selection
                openFileDialog.InitialDirectory = Application.ExecutablePath;

                // Show dialog and check if the user selected a file
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ThemeManager.Instance.ThemeLoadFromFile(openFileDialog.FileName);

                    pbNeutral.BackColor = ThemeManager.Instance.Neutral;
                    pbPrimary.BackColor = ThemeManager.Instance.Primary;
                    pbSecondary.BackColor = ThemeManager.Instance.Secondary;
                    pbSemanticA.BackColor = ThemeManager.Instance.SemanticA;
                    pbSemanticB.BackColor = ThemeManager.Instance.SemanticB;
                    pbSemanticC.BackColor = ThemeManager.Instance.SemanticC;

                }
            }
        }

        private void btnSaveTheme_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Set default file extension
                saveFileDialog.Filter = "EUI Files (*.eui)|*.eui";
                saveFileDialog.DefaultExt = "eui";
                saveFileDialog.AddExtension = true;
                saveFileDialog.InitialDirectory = Application.ExecutablePath;
                saveFileDialog.FileName = "NewTheme";  // Set default filename

                // Show the dialog and check if the user selected a file
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ThemeManager.Instance.ThemeSaveToFile(saveFileDialog.FileName);
                }
            }
        }

        private void btnGenerateShades_Click(object sender, EventArgs e)
        {
            updateAllShades();
            pbNeutral.BackColor = ThemeManager.Instance.Neutral;
            pbPrimary.BackColor = ThemeManager.Instance.Primary;
            pbSecondary.BackColor = ThemeManager.Instance.Secondary;
            pbSemanticA.BackColor = ThemeManager.Instance.SemanticA;
            pbSemanticB.BackColor = ThemeManager.Instance.SemanticB;
            pbSemanticC.BackColor = ThemeManager.Instance.SemanticC;
            updateAllColors();
        }

        private void togDarkMode_Toggled(object sender, EventArgs e)
        {
            ThemeManager.Instance.IsDarkMode = togDarkMode.IsChecked;

            if (ThemeManager.Instance.IsDarkMode) 
            {
                ThemeManager.Instance.UpdateColor(ThemeColor.Neutral, pbNeutral.BackColor, null, null, true);
            }
            else
            {
                ThemeManager.Instance.UpdateColor(ThemeColor.Neutral, pbNeutral.BackColor, null, null, false);
            }
            
        }

        private void togDarkMode_Click(object sender, EventArgs e)
        {

        }
    }
}
