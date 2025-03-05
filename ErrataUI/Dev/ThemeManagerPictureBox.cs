using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static ErrataUI.ThemeManager;


//These pictureboxes are used in the ThemeManager - they have a theme & shade dropdown to easily color them.

namespace ErrataUI
{
    public class ThemeManagerPictureBox : PictureBox
    {
        private ThemeColor _themeColor = ThemeColor.Primary;
        private ThemeShade _themeShade = ThemeShade._500;

        // Property for ThemeColor Enum (e.g., Primary, Secondary, etc.)
        [Browsable(true)]
        [Category("ErrataUI")]
        [Description("Select the color theme for the picture box.")]
        public ThemeColor ThemeColor
        {
            get => _themeColor;
            set
            {
                _themeColor = value;
                UpdateColor();
               
            }
        }

        // Property for ThemeShade Enum (e.g., 50, 100, 200, etc.)
        [Browsable(true)]
        [Category("ErrataUI")]
        [Description("Select the shade of the color theme for the picture box.")]
        public ThemeShade ThemeShade
        {
            get => _themeShade;
            set
            {
                _themeShade = value;
                UpdateColor();
                
            }
        }

        public ThemeManagerPictureBox()
        {
            // Subscribe to the theme changed event
            ThemeManager.Instance.ThemeChanged += (s, e) => UpdateColor();

            // Initialize with the current theme
            UpdateColor();
        }

        // Method to update the picture box color based on ThemeColor and ThemeShade
        private void UpdateColor()
        {
            // Get the color based on the ThemeColor and ThemeShade
            // Assuming ThemeManager is a static class that gives the color
            var themeManager = ThemeManager.Instance;

            // Set the BackColor or other properties accordingly
            Color color = themeManager.GetColorForTheme(_themeColor, _themeShade);
            this.BackColor = color;

            this.Invalidate();
        }
    }
}
