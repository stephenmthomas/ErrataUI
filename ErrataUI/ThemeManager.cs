using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ErrataUI;
using System.Text.Json;

namespace ErrataUI
{

    public class ThemeManager
    {
        private static ThemeManager _instance;
        public static ThemeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ThemeManager();
                }
                return _instance;
            }
        }

        public event EventHandler ThemeChanged;











        //Theme Manager enums and dictionaries to handle the enumes
        //These definitions control most of the behavior of ErrataUI
        #region EnumsDicts
        public static readonly Dictionary<UIRole, ThemeColorShade> LightModeRoleShades = new()
        {
            // Header and Navigation
            { UIRole.TitleBar, ThemeColorShade.Primary_500 },
            { UIRole.SubBar, ThemeColorShade.Primary_700 },
            { UIRole.MenuBar, ThemeColorShade.Primary_700 },
            { UIRole.Breadcrumbs, ThemeColorShade.Neutral_700 },
            { UIRole.NavigationBar, ThemeColorShade.Primary_600 },
            { UIRole.TabBar, ThemeColorShade.Primary_400 },

            // Backgrounds
            { UIRole.MainBackground, ThemeColorShade.Neutral_100 },
            { UIRole.SecondaryBackground, ThemeColorShade.Neutral_300 },
            { UIRole.TertiaryBackground, ThemeColorShade.Neutral_200 },
            { UIRole.CardBackground, ThemeColorShade.Neutral_50 },
            { UIRole.ModalBackground, ThemeColorShade.Neutral_800 },
            { UIRole.OverlayBackground, ThemeColorShade.Neutral_900 },

            // Text Levels
            { UIRole.BodyTextL1, ThemeColorShade.Neutral_900 },
            { UIRole.BodyTextL2, ThemeColorShade.Neutral_800 },
            { UIRole.BodyTextL3, ThemeColorShade.Neutral_700 },
            { UIRole.PlaceholderText, ThemeColorShade.Neutral_500 },
            { UIRole.DisabledText, ThemeColorShade.Neutral_400 },
            { UIRole.HeadingText, ThemeColorShade.Primary_800 },
            { UIRole.SubheadingText, ThemeColorShade.Primary_700 },
            { UIRole.TitleBarText, ThemeColorShade.Neutral_200 },

            // Borders and Dividers
            { UIRole.GeneralBorders, ThemeColorShade.Neutral_500 },
            { UIRole.EmphasizedBorders, ThemeColorShade.Primary_700 },
            { UIRole.SectionDivider, ThemeColorShade.Neutral_400 },
            { UIRole.ListDivider, ThemeColorShade.Neutral_300 },

            // Buttons
            { UIRole.PrimaryButtonBackground, ThemeColorShade.Primary_500 },
            { UIRole.PrimaryButtonText, ThemeColorShade.Neutral_50 },
            { UIRole.SecondaryButtonBackground, ThemeColorShade.Primary_700 },
            { UIRole.SecondaryButtonText, ThemeColorShade.Neutral_50 },
            { UIRole.DisabledButtonBackground, ThemeColorShade.Neutral_400 },
            { UIRole.DisabledButtonText, ThemeColorShade.Neutral_300 },

            // Input Fields
            { UIRole.InputBackground, ThemeColorShade.Neutral_50 },
            { UIRole.InputText, ThemeColorShade.Neutral_900 },
            { UIRole.InputPlaceholderText, ThemeColorShade.Neutral_500 },
            { UIRole.InputBorder, ThemeColorShade.Neutral_400 },
            { UIRole.InputFocusBorder, ThemeColorShade.Primary_500 },

            // Interactive States
            { UIRole.HoverBackground, ThemeColorShade.Primary_300 },
            { UIRole.SelectedBackground, ThemeColorShade.Primary_400 },
            { UIRole.FocusOutline, ThemeColorShade.Primary_500 },
            { UIRole.PressedBackground, ThemeColorShade.Primary_600 },

            // Alerts and Notifications
            { UIRole.SuccessText, ThemeColorShade.SemanticA_700 },
            { UIRole.SuccessBackground, ThemeColorShade.SemanticA_100 },
            { UIRole.WarningText, ThemeColorShade.SemanticB_700 },
            { UIRole.WarningBackground, ThemeColorShade.SemanticB_100 },
            { UIRole.ErrorText, ThemeColorShade.SemanticC_700 },
            { UIRole.ErrorBackground, ThemeColorShade.SemanticC_100 },
            { UIRole.InfoText, ThemeColorShade.Primary_700 },
            { UIRole.InfoBackground, ThemeColorShade.Primary_100 },

            // Data Visualizations
            { UIRole.ChartPrimary, ThemeColorShade.Primary_500 },
            { UIRole.ChartSecondary, ThemeColorShade.Secondary_500 },
            { UIRole.ChartTertiary, ThemeColorShade.SemanticA_500 },
            { UIRole.ChartHighlight, ThemeColorShade.SemanticB_500 },
            { UIRole.ChartBackground, ThemeColorShade.Neutral_50 },

            // Other Elements
            { UIRole.TooltipBackground, ThemeColorShade.Neutral_700 },
            { UIRole.TooltipText, ThemeColorShade.Neutral_50 },
            { UIRole.Shadows, ThemeColorShade.Neutral_900 },
            { UIRole.AccentColor, ThemeColorShade.Primary_500 },
            { UIRole.LinkText, ThemeColorShade.Primary_500 },
            { UIRole.DisabledLinkText, ThemeColorShade.Neutral_400 },
            { UIRole.ScrollbarThumb, ThemeColorShade.Neutral_500 },
            { UIRole.ScrollbarTrack, ThemeColorShade.Neutral_300 },

            // Specific Controls
            { UIRole.Drawer, ThemeColorShade.Primary_700 }
        };
        public static readonly Dictionary<UIRole, ThemeColorShade> DarkModeRoleShades = new()
        {
            // Header and Navigation
            { UIRole.TitleBar, ThemeColorShade.Primary_800 },
            { UIRole.SubBar, ThemeColorShade.Primary_900 },
            { UIRole.MenuBar, ThemeColorShade.Primary_900 },
            { UIRole.Breadcrumbs, ThemeColorShade.Neutral_300 },
            { UIRole.NavigationBar, ThemeColorShade.Primary_700 },
            { UIRole.TabBar, ThemeColorShade.Primary_600 },

            // Backgrounds
            { UIRole.MainBackground, ThemeColorShade.Neutral_900 },
            { UIRole.SecondaryBackground, ThemeColorShade.Neutral_800 },
            { UIRole.TertiaryBackground, ThemeColorShade.Neutral_700 },
            { UIRole.CardBackground, ThemeColorShade.Neutral_800 },
            { UIRole.ModalBackground, ThemeColorShade.Neutral_600 },
            { UIRole.OverlayBackground, ThemeColorShade.Neutral_500 },

            // Text Levels
            { UIRole.BodyTextL1, ThemeColorShade.Neutral_100 },
            { UIRole.BodyTextL2, ThemeColorShade.Neutral_200 },
            { UIRole.BodyTextL3, ThemeColorShade.Neutral_300 },
            { UIRole.PlaceholderText, ThemeColorShade.Neutral_400 },
            { UIRole.DisabledText, ThemeColorShade.Neutral_500 },
            { UIRole.HeadingText, ThemeColorShade.Primary_100 },
            { UIRole.SubheadingText, ThemeColorShade.Primary_200 },
            { UIRole.TitleBarText, ThemeColorShade.Neutral_200 },

            // Borders and Dividers
            { UIRole.GeneralBorders, ThemeColorShade.Neutral_600 },
            { UIRole.EmphasizedBorders, ThemeColorShade.Primary_700 },
            { UIRole.SectionDivider, ThemeColorShade.Neutral_500 },
            { UIRole.ListDivider, ThemeColorShade.Neutral_400 },

            // Buttons
            { UIRole.PrimaryButtonBackground, ThemeColorShade.Primary_600 },
            { UIRole.PrimaryButtonText, ThemeColorShade.Neutral_900 },
            { UIRole.SecondaryButtonBackground, ThemeColorShade.Primary_700 },
            { UIRole.SecondaryButtonText, ThemeColorShade.Neutral_900 },
            { UIRole.DisabledButtonBackground, ThemeColorShade.Neutral_600 },
            { UIRole.DisabledButtonText, ThemeColorShade.Neutral_500 },

            // Input Fields
            { UIRole.InputBackground, ThemeColorShade.Neutral_800 },
            { UIRole.InputText, ThemeColorShade.Neutral_100 },
            { UIRole.InputPlaceholderText, ThemeColorShade.Neutral_300 },
            { UIRole.InputBorder, ThemeColorShade.Neutral_500 },
            { UIRole.InputFocusBorder, ThemeColorShade.Primary_600 },

            // Interactive States
            { UIRole.HoverBackground, ThemeColorShade.Primary_700 },
            { UIRole.SelectedBackground, ThemeColorShade.Primary_800 },
            { UIRole.FocusOutline, ThemeColorShade.Primary_900 },
            { UIRole.PressedBackground, ThemeColorShade.Primary_900 },

            // Alerts and Notifications
            { UIRole.SuccessText, ThemeColorShade.SemanticA_300 },
            { UIRole.SuccessBackground, ThemeColorShade.SemanticA_900 },
            { UIRole.WarningText, ThemeColorShade.SemanticB_300 },
            { UIRole.WarningBackground, ThemeColorShade.SemanticB_900 },
            { UIRole.ErrorText, ThemeColorShade.SemanticC_300 },
            { UIRole.ErrorBackground, ThemeColorShade.SemanticC_900 },
            { UIRole.InfoText, ThemeColorShade.Primary_300 },
            { UIRole.InfoBackground, ThemeColorShade.Primary_900 },

            // Data Visualizations
            { UIRole.ChartPrimary, ThemeColorShade.Primary_900 },
            { UIRole.ChartSecondary, ThemeColorShade.Secondary_900 },
            { UIRole.ChartTertiary, ThemeColorShade.SemanticA_900 },
            { UIRole.ChartHighlight, ThemeColorShade.SemanticB_900 },
            { UIRole.ChartBackground, ThemeColorShade.Neutral_900 },

            // Other Elements
            { UIRole.TooltipBackground, ThemeColorShade.Neutral_300 },
            { UIRole.TooltipText, ThemeColorShade.Neutral_900 },
            { UIRole.Shadows, ThemeColorShade.Neutral_600 },
            { UIRole.AccentColor, ThemeColorShade.Primary_800 },
            { UIRole.LinkText, ThemeColorShade.Primary_600 },
            { UIRole.DisabledLinkText, ThemeColorShade.Neutral_400 },
            { UIRole.ScrollbarThumb, ThemeColorShade.Neutral_500 },
            { UIRole.ScrollbarTrack, ThemeColorShade.Neutral_400 },

            // Specific Controls
            { UIRole.Drawer, ThemeColorShade.Primary_700 }
        };
        public enum ThemeColor
        {
            Neutral,
            Primary,
            Secondary,
            SemanticA,
            SemanticB,
            SemanticC
        }
        public enum ThemeShade
        {
            _50 = 50,
            _100 = 100,
            _150 = 150,
            _200 = 200,
            _300 = 300,
            _400 = 400,
            _500 = 500,
            _600 = 600,
            _700 = 700,
            _750 = 750,
            _800 = 800,
            _900 = 900,
            _1000 = 1000,
        }
        public enum ThemeColorShade
        {
            Neutral_50,
            Neutral_100,
            Neutral_150,
            Neutral_200,
            Neutral_300,
            Neutral_400,
            Neutral_500,
            Neutral_600,
            Neutral_700,
            Neutral_750,
            Neutral_800,
            Neutral_900,
            Neutral_1000,

            Primary_50,
            Primary_100,
            Primary_150,
            Primary_200,
            Primary_300,
            Primary_400,
            Primary_500,
            Primary_600,
            Primary_700,
            Primary_750,
            Primary_800,
            Primary_900,
            Primary_1000,

            Secondary_50,
            Secondary_100,
            Secondary_150,
            Secondary_200,
            Secondary_300,
            Secondary_400,
            Secondary_500,
            Secondary_600,
            Secondary_700,
            Secondary_750,
            Secondary_800,
            Secondary_900,
            Secondary_1000,

            SemanticA_50,
            SemanticA_100,
            SemanticA_150,
            SemanticA_200,
            SemanticA_300,
            SemanticA_400,
            SemanticA_500,
            SemanticA_600,
            SemanticA_700,
            SemanticA_750,
            SemanticA_800,
            SemanticA_900,
            SemanticA_1000,

            SemanticB_50,
            SemanticB_100,
            SemanticB_150,
            SemanticB_200,
            SemanticB_300,
            SemanticB_400,
            SemanticB_500,
            SemanticB_600,
            SemanticB_700,
            SemanticB_750,
            SemanticB_800,
            SemanticB_900,
            SemanticB_1000,

            SemanticC_50,
            SemanticC_100,
            SemanticC_150,
            SemanticC_200,
            SemanticC_300,
            SemanticC_400,
            SemanticC_500,
            SemanticC_600,
            SemanticC_700,
            SemanticC_750,
            SemanticC_800,
            SemanticC_900,
            SemanticC_1000,
            None,
            Transparent
        }
        public enum UIRole
        {
            //None
            None,

            //ErrataSpecific
            CaptionBar,
            FormTitleBar,
            FormSubTitle,
            BackgroundFar,
            BackgroundMid,
            BackgroundNear,
            BackgroundTop,
            Drawer,

            // Header and Navigation
            TitleBar,
            SubBar,
            MenuBar,
            Breadcrumbs,
            NavigationBar,
            TabBar,

            // Backgrounds
            MainBackground,
            SecondaryBackground,
            TertiaryBackground,
            CardBackground,
            ModalBackground,
            OverlayBackground,

            // Text Levels
            BodyTextL1,
            BodyTextL2,
            BodyTextL3,
            PlaceholderText,
            DisabledText,
            HeadingText,
            SubheadingText,
            TitleBarText,

            // Borders and Dividers
            GeneralBorders,
            EmphasizedBorders,
            SectionDivider,
            ListDivider,

            // Buttons
            PrimaryButtonBackground,
            PrimaryButtonText,
            SecondaryButtonBackground,
            SecondaryButtonText,
            DisabledButtonBackground,
            DisabledButtonText,

            // Input Fields
            InputBackground,
            InputText,
            InputPlaceholderText,
            InputBorder,
            InputFocusBorder,

            // Interactive States
            HoverBackground,
            SelectedBackground,
            FocusOutline,
            PressedBackground,

            // Alerts and Notifications
            SuccessText,
            SuccessBackground,
            WarningText,
            WarningBackground,
            ErrorText,
            ErrorBackground,
            InfoText,
            InfoBackground,

            // Data Visualizations
            ChartPrimary,
            ChartSecondary,
            ChartTertiary,
            ChartHighlight,
            ChartBackground,

            // Other Elements
            TooltipBackground,
            TooltipText,
            Shadows,
            AccentColor,
            LinkText,
            DisabledLinkText,
            ScrollbarThumb,
            ScrollbarTrack

            //Specific Controls
            

        }
        #endregion




        //Single variable for dictating if we are in dark mode or not.
        public bool IsDarkMode { get; set; } = false;


        //Color palette and default values. All colors are generated from these colors based on reference curves.
        //These are the essential colors by which all other shades are generated
        public Color Neutral { get; set; } = Color.FromArgb(128, 128, 128);
        public Color Primary { get; set; } = Color.FromArgb(15, 150, 220);
        public Color Secondary { get; set; } = Color.FromArgb(110, 45, 245);
        public Color SemanticA { get; set; } = Color.FromArgb(25, 128, 56); //Green
        public Color SemanticB { get; set; } = Color.FromArgb(241, 194, 27);  //Yellow
        public Color SemanticC { get; set; } = Color.FromArgb(218, 30, 40);  //Red


        //Dictionaries to store the shades.
        public Dictionary<int, Color> NeutralSwatch { get; set; }
        public Dictionary<int, Color> PrimarySwatch { get; set; }
        public Dictionary<int, Color> SecondarySwatch { get; set; }
        public Dictionary<int, Color> SemanticASwatch { get; set; }
        public Dictionary<int, Color> SemanticBSwatch { get; set; }
        public Dictionary<int, Color> SemanticCSwatch { get; set; }


        //Reference Curves as arrays
        //in general, as shade level increases from 500 to 1000, brightness decreases and saturation increases
        //and as we get brighter toward a shade level of 50 (end of curves) brightness increases and saturation decreases

        //these arrays correspond to the following shades [1000, 900,  800, 750,  700,   600,  500,  400,  300,    200,   150,   100,    50]
        public float[] BrightnessCurve { get; set; } = { 0.12f, 0.25f, 0.5f, 0.6f, 0.75f, 0.9f, 1.0f, 1.09f, 1.15f, 1.45f, 1.66f, 1.95f, 2.2f };
        public float[] SaturationCurve { get; set; } = { 1.45f, 1.4f, 1.30f, 1.25f, 1.2f, 1.1f, 1.0f, 0.9f, 0.75f, 0.55f, 0.40f, 0.25f, 0.10f };


        //https://uxplanet.org/designing-systematic-colors-b5d2605b15c
        //Static Curves
        public static float[] DefaultBrightness = { 0.12f, 0.25f, 0.5f,  0.6f,  0.75f, 0.9f,  1.0f, 1.09f, 1.15f, 1.45f, 1.66f, 1.95f, 2.2f };
        public static float[] DefaultSaturation = { 1.45f, 1.4f,  1.30f, 1.25f, 1.2f,  1.1f,  1.0f, 0.9f,  0.75f, 0.55f, 0.40f, 0.25f, 0.10f };

        public static float[] VoloneBrightness =  { 0.20f, 0.3f,  0.55f, 0.6f,  0.75f, 0.9f,  1.0f, 1.1f,  1.2f,  1.3f,  1.35f, 1.4f,  1.5f };
        public static float[] VoloneSaturation =  { 1.45f, 1.4f,  1.30f, 1.25f, 1.2f,  1.1f,  1.0f, 0.9f,  0.8f,  0.6f,  0.5f,  0.4f,  0.3f };

        public static float[] PulseBrightness =   { 0.5f,  0.6f,  0.7f,  0.75f, 0.8f,  0.9f,  1.0f, 1.0f,  1.05f, 1.1f,  1.15f, 1.2f,  1.3f };
        public static float[] PulseSaturation =   { 1.4f,  1.4f,  1.3f,  1.25f, 1.2f,  1.1f,  1.0f, 1.0f,  0.90f, 0.8f,  0.75f, 0.7f,  0.6f };

        public static float[] FelizBrightness =   { 0.12f, 0.25f, 0.4f,  0.53f, 0.65f, 1.2f,  1.4f, 1.8f,  2.0f,  2.1f,  2.12f, 2.15f, 2.2f };
        public static float[] FelizSaturation =   { 1.6f,  1.4f,  1.2f,  1.14f, 1.06f, 1.0f,  0.8f, 0.6f,  0.45f, 0.35f, 0.33f, 0.3f,  0.25f };


        public List<string> curveNames = new List<string>
        {
            "DefaultBrightness",
            "DefaultSaturation",
            "VoloneBrightness",
            "VoloneSaturation",
            "PulseBrightness",
            "PulseSaturation",
            "FelizBrightness",
            "FelizSaturation"
        };





        //METHODS
        private ThemeManager()
        {
            // Generate the color swatches using GenerateShades method
            NeutralSwatch = GenerateShades(Neutral, BrightnessCurve, SaturationCurve);
            PrimarySwatch = GenerateShades(Primary, BrightnessCurve, SaturationCurve);
            SecondarySwatch = GenerateShades(Secondary, BrightnessCurve, SaturationCurve);
            SemanticASwatch = GenerateShades(SemanticA, BrightnessCurve, SaturationCurve);
            SemanticBSwatch = GenerateShades(SemanticB, BrightnessCurve, SaturationCurve);
            SemanticCSwatch = GenerateShades(SemanticC, BrightnessCurve, SaturationCurve);
        }


        #region ThemeFile
        public string GenerateThemeFileData()
        {
            string themeData = $@"
                                IsDarkMode={IsDarkMode}
                                Neutral={ColorToString(Neutral)}
                                Primary={ColorToString(Primary)}
                                Secondary={ColorToString(Secondary)}
                                SemanticA={ColorToString(SemanticA)}
                                SemanticB={ColorToString(SemanticB)}
                                SemanticC={ColorToString(SemanticC)}
                                BrightnessCurve={ArrayToString(BrightnessCurve)}
                                SaturationCurve={ArrayToString(SaturationCurve)}";

            return themeData.Trim();
        }

        // Helper method to convert float[] to a comma-separated string
        private string ArrayToString(float[] array)
        {
            return string.Join(",", array.Select(f => f.ToString("F2"))); // Format to 2 decimal places (optional)
        }

        // Helper method to convert Color to a string format
        private string ColorToString(Color color)
        {
            return $"{color.A},{color.R},{color.G},{color.B}"; // ARGB format
        }

        public void ThemeSaveToFile(string filePath)
        {
            string themeData = GenerateThemeFileData();

            File.WriteAllText(filePath, themeData.Trim()); // Save to file
        }

        public void ThemeLoadFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return; // Ensure file exists

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('='); // Split key and value
                if (parts.Length != 2) continue;

                string key = parts[0].Trim();
                string value = parts[1].Trim();

                switch (key)
                {
                    case "IsDarkMode":
                        IsDarkMode = bool.Parse(value);
                        break;
                    case "Neutral":
                        Neutral = StringToColor(value);
                        break;
                    case "Primary":
                        Primary = StringToColor(value);
                        break;
                    case "Secondary":
                        Secondary = StringToColor(value);
                        break;
                    case "SemanticA":
                        SemanticA = StringToColor(value);
                        break;
                    case "SemanticB":
                        SemanticB = StringToColor(value);
                        break;
                    case "SemanticC":
                        SemanticC = StringToColor(value);
                        break;
                    case "BrightnessCurve":
                        BrightnessCurve = StringToFloatArray(value);
                        break;
                    case "SaturationCurve":
                        SaturationCurve = StringToFloatArray(value);
                        break;
                }
            }

            //REFRESH 

            UpdateColor(ThemeColor.Neutral, Neutral, BrightnessCurve, SaturationCurve);
            UpdateColor(ThemeColor.Primary, Primary, BrightnessCurve, SaturationCurve);
            UpdateColor(ThemeColor.Secondary, Secondary, BrightnessCurve, SaturationCurve);
            UpdateColor(ThemeColor.SemanticA, SemanticA, BrightnessCurve, SaturationCurve);
            UpdateColor(ThemeColor.SemanticB, SemanticB, BrightnessCurve, SaturationCurve);
            UpdateColor(ThemeColor.SemanticC, SemanticC, BrightnessCurve, SaturationCurve);

        }

        public void RefreshTheme()
        {
            UpdateColor(ThemeColor.Neutral, Neutral, BrightnessCurve, SaturationCurve);
            UpdateColor(ThemeColor.Primary, Primary, BrightnessCurve, SaturationCurve);
            UpdateColor(ThemeColor.Secondary, Secondary, BrightnessCurve, SaturationCurve);
            UpdateColor(ThemeColor.SemanticA, SemanticA, BrightnessCurve, SaturationCurve);
            UpdateColor(ThemeColor.SemanticB, SemanticB, BrightnessCurve, SaturationCurve);
            UpdateColor(ThemeColor.SemanticC, SemanticC, BrightnessCurve, SaturationCurve);
        }

        // Helper method to convert string (A,R,G,B) back to Color
        private Color StringToColor(string colorString)
        {
            var components = colorString.Split(',');
            if (components.Length != 4) return Color.Black; // Default if invalid

            return Color.FromArgb(
                int.Parse(components[0]), // A
                int.Parse(components[1]), // R
                int.Parse(components[2]), // G
                int.Parse(components[3])  // B
            );
        }

        // Helper method to convert CSV string to float array
        private float[] StringToFloatArray(string csv)
        {
            return csv.Split(',')
                      .Select(value => float.Parse(value.Trim()))
                      .ToArray();
        }


        #endregion



        public ThemeColorShade GetShadeForRole(UIRole role)
        {
            if (role == UIRole.None) { return ThemeColorShade.None; }

            if (IsDarkMode)
            {
                // Return the corresponding shade from DarkModeRoleShades
                if (DarkModeRoleShades.ContainsKey(role))
                {
                    return DarkModeRoleShades[role];
                }
            }
            else
            {
                // Return the corresponding shade from LightModeRoleShades
                if (LightModeRoleShades.ContainsKey(role))
                {
                    return LightModeRoleShades[role];
                }
            }



            // Default if the role is not found
            throw new ArgumentException("UIRole not found", nameof(role));
        }
        public void UpdateColor(ThemeColor themeColor, Color newColor, float[] brightnessCurve = null, float[] saturationCurve = null, bool reversed = false)
        {
            // Update the specified color property based on the theme color enum
            switch (themeColor)
            {
                case ThemeColor.Neutral:
                    Neutral = newColor;
                    NeutralSwatch = GenerateShades(Neutral, brightnessCurve, saturationCurve, reversed);
                    break;

                case ThemeColor.Primary:
                    Primary = newColor;
                    PrimarySwatch = GenerateShades(Primary, brightnessCurve, saturationCurve, reversed);
                    break;

                case ThemeColor.Secondary:
                    Secondary = newColor;
                    SecondarySwatch = GenerateShades(Secondary, brightnessCurve, saturationCurve, reversed);
                    break;

                case ThemeColor.SemanticA:
                    SemanticA = newColor;
                    SemanticASwatch = GenerateShades(SemanticA, brightnessCurve, saturationCurve, reversed);
                    break;

                case ThemeColor.SemanticB:
                    SemanticB = newColor;
                    SemanticBSwatch = GenerateShades(SemanticB, brightnessCurve, saturationCurve, reversed);
                    break;

                case ThemeColor.SemanticC:
                    SemanticC = newColor;
                    SemanticCSwatch = GenerateShades(SemanticC, brightnessCurve, saturationCurve, reversed);
                    break;
            }

            // Trigger the event to notify other controls
            OnThemeChanged();
        }
        protected virtual void OnThemeChanged()
        {
            ThemeChanged?.Invoke(this, EventArgs.Empty);
        }


        public Dictionary<int, Color> GenerateShades(Color baseColor, float[] brightnessCurve = null, float[] saturationCurve = null, bool reversed = false )
        {
            if (brightnessCurve == null)
            {
                brightnessCurve = DefaultBrightness;
            }

            if (saturationCurve == null)
            {
                saturationCurve = DefaultSaturation;
            }

            var shades = new Dictionary<int, Color>();

            // Adjust saturation and brightness levels based on the shade
            int[] shadeLevels = { 1000, 900, 800, 750, 700, 600, 500, 400, 300, 200, 150, 100, 50 };

            for (int i = 0; i < shadeLevels.Length; i++)
            {

                if (reversed)
                {
                    // Reverse the index to apply lightest to darkest mapping
                    int reverseIndex = shadeLevels.Length - 1 - i;

                    float brightness_r = brightnessCurve[reverseIndex];
                    float saturation_r = saturationCurve[reverseIndex];

                    shades[shadeLevels[i]] = AdjustColorHsb(baseColor, saturation_r, brightness_r);
                }
                else
                {
                    float brightness = brightnessCurve[i];
                    float saturation = saturationCurve[i];

                    shades[shadeLevels[i]] = AdjustColorHsb(baseColor, saturation, brightness);
                }

                
            }

            return shades;
        }

        public Color GetThemeColorShadeOffset(ThemeColorShade themeColorShade, int offset = 0)
        {
            if (themeColorShade == ThemeColorShade.None) { return Color.Empty; }
            if (themeColorShade == ThemeColorShade.Transparent) { return Color.Transparent; }

            string[] colorShade = themeColorShade.ToString().Split("_");

            if (Enum.TryParse(colorShade[0], out ThemeColor themeColor) && int.TryParse(colorShade[1], out int currentShade))
            {
                int[] shadeLevels = { 1000, 900, 800, 750, 700, 600, 500, 400, 300, 200, 150, 100, 50 };

                Dictionary<int, Color> selectedSwatch = themeColor switch
                {
                    ThemeColor.Neutral => NeutralSwatch,
                    ThemeColor.Primary => PrimarySwatch,
                    ThemeColor.Secondary => SecondarySwatch,
                    ThemeColor.SemanticA => SemanticASwatch,
                    ThemeColor.SemanticB => SemanticBSwatch,
                    ThemeColor.SemanticC => SemanticCSwatch,
                    _ => null
                };

                if (selectedSwatch == null) return Color.Transparent;

                int currentIndex = Array.IndexOf(shadeLevels, currentShade);
                if (currentIndex == -1) return Color.Transparent; // Shade not found

                // Compute new index (higher = darker, lower = lighter)
                int newIndex = Math.Clamp(currentIndex + offset, 0, shadeLevels.Length - 1);
                int newShade = shadeLevels[newIndex];

                return selectedSwatch.TryGetValue(newShade, out Color color) ? color : Color.Transparent;
            }

            return Color.Transparent;
        }
        public Color GetThemeColorShade(ThemeColorShade themeColorShade)
        {
            if (themeColorShade == ThemeColorShade.None) { return Color.Empty; }
            if (themeColorShade == ThemeColorShade.Transparent) { return Color.Transparent; }

            // Split the ThemeColorShade (e.g., Primary_500) into ThemeColor and Shade
            string[] colorShade = themeColorShade.ToString().Split("_");

                // Get the ThemeColor from the first part (e.g., "Primary")
                if (Enum.TryParse(colorShade[0], out ThemeColor themeColor))
            {
                // Get the shade (e.g., "500") as an integer
                if (int.TryParse(colorShade[1], out int shade))
                {
                    // Use a switch statement to choose the correct dictionary based on the theme color
                    Dictionary<int, Color> selectedSwatch = themeColor switch
                    {
                        ThemeColor.Neutral => NeutralSwatch,
                        ThemeColor.Primary => PrimarySwatch,
                        ThemeColor.Secondary => SecondarySwatch,
                        ThemeColor.SemanticA => SemanticASwatch,
                        ThemeColor.SemanticB => SemanticBSwatch,
                        ThemeColor.SemanticC => SemanticCSwatch,
                        _ => null
                    };

                    // Check if the selected swatch is valid and return the color for the specified shade
                    if (selectedSwatch != null && selectedSwatch.TryGetValue(shade, out Color color))
                    {
                        return color;
                    }
                }
            }

            // Return a default color (e.g., Transparent) if not found
            return Color.Transparent;
        }


        public Rectangle ConvertLineToRectangle(Point start, Point end, int thickness)
        {
            // Determine the width and height of the rectangle
            int x = Math.Min(start.X, end.X); // x-coordinate of the top-left corner
            int y = Math.Min(start.Y, end.Y); // y-coordinate of the top-left corner

            int width = Math.Abs(end.X - start.X); // Width based on the horizontal distance between points
            int height = Math.Abs(end.Y - start.Y); // Height based on the vertical distance between points

            // If the line is horizontal, we want the height to be equal to the thickness
            if (start.Y == end.Y)
            {
                height = thickness;
            }
            // If the line is vertical, we want the width to be equal to the thickness
            else if (start.X == end.X)
            {
                width = thickness;
            }
            // If the line is diagonal, just cover the bounding box
            else
            {
                // Optionally, you can adjust the thickness for diagonals if needed
            }

            // Return the calculated rectangle
            return new Rectangle(x, y, width, height);
        }



        // AdjustColorHsb method to tweak both saturation and brightness
        private static Color AdjustColorHsb(Color baseColor, float saturationFactor, float brightnessFactor)
        {
            // Convert the color to HSV
            float hue, saturation, brightness;
            ColorToHsb(baseColor, out hue, out saturation, out brightness);

            // Apply the factors
            saturation = Math.Clamp(saturation * saturationFactor, 0f, 1f);
            brightness = Math.Clamp(brightness * brightnessFactor, 0f, 1f);

            // Convert back to Color
            return FromHsb(hue, saturation, brightness);
        }

        private static void ColorToHsb(Color color, out float hue, out float saturation, out float brightness)
        {
            float r = color.R / 255f;
            float g = color.G / 255f;
            float b = color.B / 255f;

            float max = Math.Max(r, Math.Max(g, b));
            float min = Math.Min(r, Math.Min(g, b));

            hue = 0f;
            if (max == min)
            {
                hue = 0f; // Undefined hue
            }
            else if (max == r)
            {
                hue = (60 * ((g - b) / (max - min)) + 360) % 360;
            }
            else if (max == g)
            {
                hue = (60 * ((b - r) / (max - min)) + 120) % 360;
            }
            else if (max == b)
            {
                hue = (60 * ((r - g) / (max - min)) + 240) % 360;
            }

            saturation = max == 0 ? 0 : (max - min) / max;
            brightness = max;
        }

        public Color GetColorForTheme(ThemeColor themeColor, ThemeShade themeShade)
        {
            Dictionary<int, Color> selectedSwatch = themeColor switch
            {
                ThemeColor.Neutral => NeutralSwatch,
                ThemeColor.Primary => PrimarySwatch,
                ThemeColor.Secondary => SecondarySwatch,
                ThemeColor.SemanticA => SemanticASwatch,
                ThemeColor.SemanticB => SemanticBSwatch,
                ThemeColor.SemanticC => SemanticCSwatch,
                _ => null
            };

            // If the selected swatch is valid, return the corresponding color
            if (selectedSwatch != null && selectedSwatch.ContainsKey((int)themeShade))
            {
                return selectedSwatch[(int)themeShade];
            }

            // Return a default color if the swatch or shade is not found
            return Color.Gray;  // You can adjust this to a more appropriate fallback color
        }

        private static Color AdjustColorBrightness(Color color, float targetBrightness)
        {
            float hue = color.GetHue();
            float saturation = color.GetSaturation();
            float brightness = color.GetBrightness();

            // Adjust brightness to the target level
            return FromHsb(hue, saturation, targetBrightness);
        }

        private static Color FromHsb(float hue, float saturation, float brightness)
        {
            // Clamp input values to their valid ranges
            hue = Math.Clamp(hue, 0, 360);
            saturation = Math.Clamp(saturation, 0, 1);
            brightness = Math.Clamp(brightness, 0, 1);

            float chroma = brightness * saturation;
            float x = chroma * (1 - Math.Abs((hue / 60) % 2 - 1));
            float m = brightness - chroma;

            float r = 0, g = 0, b = 0;

            if (hue >= 0 && hue < 60)
            {
                r = chroma; g = x; b = 0;
            }
            else if (hue >= 60 && hue < 120)
            {
                r = x; g = chroma; b = 0;
            }
            else if (hue >= 120 && hue < 180)
            {
                r = 0; g = chroma; b = x;
            }
            else if (hue >= 180 && hue < 240)
            {
                r = 0; g = x; b = chroma;
            }
            else if (hue >= 240 && hue < 300)
            {
                r = x; g = 0; b = chroma;
            }
            else if (hue >= 300 && hue <= 360)
            {
                r = chroma; g = 0; b = x;
            }

            // Calculate final RGB values
            int red = (int)Math.Round((r + m) * 255);
            int green = (int)Math.Round((g + m) * 255);
            int blue = (int)Math.Round((b + m) * 255);

            // Clamp final RGB values to prevent overflow
            red = Math.Clamp(red, 0, 255);
            green = Math.Clamp(green, 0, 255);
            blue = Math.Clamp(blue, 0, 255);

            return Color.FromArgb(red, green, blue);
        }

        public static bool IsColorDark(Color color, int Brightness = 128)
        {
            // Calculate brightness based on the relative luminance
            double brightness = (0.299 * color.R) + (0.587 * color.G) + (0.114 * color.B);
            return brightness < Brightness; // Dark if brightness is below 128
        }

        public static Bitmap OverlayImageColor(Bitmap original, Color overlayColor, float opacity = 0.5f)
        {
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                g.DrawImage(original, 0, 0);

                using (SolidBrush brush = new SolidBrush(Color.FromArgb((int)(opacity * 255), overlayColor)))
                {
                    g.FillRectangle(brush, new Rectangle(0, 0, newBitmap.Width, newBitmap.Height));
                }
            }
            return newBitmap;
        }

        public static Bitmap TintImage(Bitmap original, Color tint)
        {
            float r = tint.R / 255f;
            float g = tint.G / 255f;
            float b = tint.B / 255f;

            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {r, 0, 0, 0, 0},
                new float[] {0, g, 0, 0, 0},
                new float[] {0, 0, b, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });

            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);

            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            using (Graphics gfx = Graphics.FromImage(newBitmap))
            {
                gfx.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                    0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            }
            return newBitmap;
        }

        public static Bitmap ColorizeImage(Bitmap originalImage, Color tintColor)
        {
            //pictureBox = new PictureBox
            //{
            //    Dock = DockStyle.Fill,
            //    SizeMode = PictureBoxSizeMode.StretchImage
            //};
            //this.Controls.Add(pictureBox);

            //// Load an example image
            //Bitmap originalImage = new Bitmap("path_to_your_image.png");

            //// Apply a blue tint
            //Bitmap colorizedImage = ImageColorizer.ColorizeImage(originalImage, Color.Blue);

            //// Display the colorized image
            //pictureBox.Image = colorizedImage;




            Bitmap newImage = new Bitmap(originalImage.Width, originalImage.Height);

            using (Graphics g = Graphics.FromImage(newImage))
            {
                // Draw the original image on the new one
                g.DrawImage(originalImage, new Rectangle(0, 0, originalImage.Width, originalImage.Height));

                // Create a color matrix and set the tint color
                ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                {
                new float[] { tintColor.R / 255f, 0, 0, 0, 0 },
                new float[] { 0, tintColor.G / 255f, 0, 0, 0 },
                new float[] { 0, 0, tintColor.B / 255f, 0, 0 },
                new float[] { 0, 0, 0, 1, 0 },
                new float[] { 0, 0, 0, 0, 1 }
                });

                // Create image attributes and set the color matrix
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                // Draw the tinted image
                g.DrawImage(newImage,
                            new Rectangle(0, 0, newImage.Width, newImage.Height),
                            0, 0, originalImage.Width, originalImage.Height,
                            GraphicsUnit.Pixel,
                            attributes);
            }

            return newImage;
        }


       
        public PrivateFontCollection _fonts = new PrivateFontCollection();
        public void LoadCustomFont()
        {
            //"YourNamespace.Resources.MaterialIcons-Regular.ttf" = FontResource
            // Load the font from embedded resources
            var fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ErrataUI.Fonts.FluentSystemIcons-Regular.ttf");

            if (fontStream != null)
            {
                byte[] fontData = new byte[fontStream.Length];
                fontStream.Read(fontData, 0, (int)fontStream.Length);
                fontStream.Close();

                // Add font to the PrivateFontCollection
                IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
                System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
                _fonts.AddMemoryFont(fontPtr, fontData.Length);
                System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            }
        }

        public static Color GenerateRandomColor(bool WithAlpha = false)
        {
            Random random = new Random();
            int r = random.Next(0, 256); // Red value (0-255)
            int g = random.Next(0, 256); // Green value (0-255)
            int b = random.Next(0, 256); // Blue value (0-255)
            int a = random.Next(0, 256);

            if (!WithAlpha) { a = 255; }

            return Color.FromArgb(a, r, g, b); // Fully opaque color
        }

        public static void PaletteSaveToFile(List<Color> colors, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Color color in colors)
                {
                    // Write the color's ARGB values to the file
                    writer.WriteLine($"{color.A},{color.R},{color.G},{color.B}");
                }
            }
        }

        public static List<Color> PaletteLoad(string PaletteName)
        {
            if (PaletteName == "")
            {
                List<Color> colorList = new List<Color> { };
                for (int i = 0; i <= 8; i++)
                {
                    colorList.Add(GenerateRandomColor());
                }
                return colorList;
            }
            else
            {
                return PaletteLoadFromFile(@"Palettes\" + PaletteName);
            }

        }
        public static List<Color> PaletteLoadFromFile(string filePath)
        {
            List<Color> colors = new List<Color>();

            foreach (string line in File.ReadAllLines(filePath))
            {
                if (line.Contains("#")) { colors.Add(HexToArgb(line)); }
                else
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 4 &&
                        int.TryParse(parts[0], out int a) &&
                        int.TryParse(parts[1], out int r) &&
                        int.TryParse(parts[2], out int g) &&
                        int.TryParse(parts[3], out int b))
                    {
                        colors.Add(Color.FromArgb(a, r, g, b));
                    }

                }
            }

            return colors;
        }

        public static Color HexToArgb(string hex)
        {
            // Remove the '#' character if it exists
            if (hex.StartsWith("#"))
            {
                hex = hex.Substring(1);
            }

            // Ensure the hex string is 6 characters long
            if (hex.Length != 6)
            {
                throw new ArgumentException("Hex color must be 6 characters long.");
            }

            // Parse the hex values
            int r = Convert.ToInt32(hex.Substring(0, 2), 16);
            int g = Convert.ToInt32(hex.Substring(2, 2), 16);
            int b = Convert.ToInt32(hex.Substring(4, 2), 16);

            // Set alpha to 255 (fully opaque)
            int a = 255;

            // Return the Color in ARGB format
            return Color.FromArgb(a, r, g, b);
        }

        public static (double C, double M, double Y, double K) ConvertRGBToCMYK(Color col)
        {
            int r = col.R;
            int g = col.G;
            int b = col.B;

            // Normalize RGB values to the range [0, 1]
            double rNorm = r / 255.0;
            double gNorm = g / 255.0;
            double bNorm = b / 255.0;

            // Calculate the key (K) component
            double k = 1 - Math.Max(rNorm, Math.Max(gNorm, bNorm));

            // Avoid division by zero when K is 1
            if (k == 1)
            {
                return (0, 0, 0, 1);
            }

            // Calculate C, M, Y
            double c = (1 - rNorm - k) / (1 - k);
            double m = (1 - gNorm - k) / (1 - k);
            double y = (1 - bNorm - k) / (1 - k);

            return (c, m, y, k);
        }
        public static (double H, double S, double V) ConvertRGBtoHSV(Color col)
        {
            int r = col.R;
            int g = col.G;
            int b = col.B;

            double rNorm = r / 255.0;
            double gNorm = g / 255.0;
            double bNorm = b / 255.0;

            double max = Math.Max(rNorm, Math.Max(gNorm, bNorm));
            double min = Math.Min(rNorm, Math.Min(gNorm, bNorm));
            double delta = max - min;

            // Calculate Hue
            double h = 0;
            if (delta != 0)
            {
                if (max == rNorm)
                    h = 60 * (((gNorm - bNorm) / delta) % 6);
                else if (max == gNorm)
                    h = 60 * (((bNorm - rNorm) / delta) + 2);
                else
                    h = 60 * (((rNorm - gNorm) / delta) + 4);
            }
            h = (h + 360) % 360; // Ensure Hue is non-negative

            // Calculate Saturation
            double s = (max == 0) ? 0 : delta / max;

            // Calculate Value
            double v = max;

            return (h, s, v);
        }
        public static (double H, double S, double L) ConvertRGBtoHSL(Color col)
        {
            int r = col.R;
            int g = col.G;
            int b = col.B;

            double rNorm = r / 255.0;
            double gNorm = g / 255.0;
            double bNorm = b / 255.0;

            double max = Math.Max(rNorm, Math.Max(gNorm, bNorm));
            double min = Math.Min(rNorm, Math.Min(gNorm, bNorm));
            double delta = max - min;

            // Calculate Lightness
            double l = (max + min) / 2;

            // Calculate Hue
            double h = 0;
            if (delta != 0)
            {
                if (max == rNorm)
                    h = 60 * (((gNorm - bNorm) / delta) % 6);
                else if (max == gNorm)
                    h = 60 * (((bNorm - rNorm) / delta) + 2);
                else
                    h = 60 * (((rNorm - gNorm) / delta) + 4);
            }
            h = (h + 360) % 360; // Ensure Hue is non-negative

            // Calculate Saturation
            double s = (delta == 0) ? 0 : delta / (1 - Math.Abs(2 * l - 1));

            return (h, s, l);
        }
        public static (double H, double S, double L, double A) ConvertRGBtoHSLA(Color col)
        {
            int r = col.R;
            int g = col.G;
            int b = col.B;

            var (h, s, l) = ConvertRGBtoHSL(col);
            return (h, s, l, col.A / 255.0);
        }

        public static Color ColorGetComplementary(Color color)
        {
            // Convert the base color to HSV
            var (hue, saturation, value) = GSC_RGBtoHSV(color);

            // Calculate the triadic hues by adding and subtracting 120° from the original hue
            float hue1 = (hue + 120) % 360;

            // Convert back to RGB for the triadic colors
            Color complement = GSC_HSVtoRGB(hue1, saturation, value);

            return complement;
        }
        public static (Color, Color) ColorGetSplitComplementary(Color baseColor, int rotation = 30)
        {
            // Convert the base color to HSV
            var (hue, saturation, value) = GSC_RGBtoHSV(baseColor);

            // Calculate the complementary hue (+180 degrees, modulo 360)
            float complementHue = (hue + 180) % 360;

            // Split complementary colors (±rotation°)
            float hue1 = (complementHue + rotation) % 360;
            float hue2 = (complementHue - rotation) % 360;

            Debug.Print($"SplitCompliment: cH={complementHue}, h+r={hue1}, h-r={hue2}");

            // Convert back to RGB for the split-complementary colors
            Color splitComp1 = GSC_HSVtoRGB(hue1, saturation, value);
            Color splitComp2 = GSC_HSVtoRGB(hue2, saturation, value);

            return (splitComp1, splitComp2);
        }
        public static (Color, Color) ColorGetTriadic(Color baseColor)
        {
            // Convert the base color to HSV
            var (hue, saturation, value) = GSC_RGBtoHSV(baseColor);

            // Calculate the triadic hues by adding and subtracting 120° from the original hue
            float hue1 = (hue + 120) % 360;
            float hue2 = (hue - 120) % 360; //float hue2 = (hue - 120 + 360) % 360;

            // Convert back to RGB for the triadic colors
            Color triadic1 = GSC_HSVtoRGB(hue1, saturation, value);
            Color triadic2 = GSC_HSVtoRGB(hue2, saturation, value);

            return (triadic1, triadic2);
        }
        public static (Color, Color, Color) ColorGetTetradic(Color baseColor)
        {
            var (hue, saturation, value) = GSC_RGBtoHSV(baseColor);

            // Calculate split hues
            float hue_c = (hue + 90) % 360;
            float hue_1 = (hue + 180) % 360;
            float hue_2 = (hue + 270) % 360;

            // Convert back to RGB for the tetradic colors
            Color tetra_c = GSC_HSVtoRGB(hue_c, saturation, value);
            Color tetra_1 = GSC_HSVtoRGB(hue_1, saturation, value);
            Color tetra_2 = GSC_HSVtoRGB(hue_2, saturation, value);

            return (tetra_c, tetra_1, tetra_2);
        }
        public static (Color, Color, Color) ColorGetSquare(Color baseColor)
        {

            var (hue, saturation, value) = GSC_RGBtoHSV(baseColor);

            // Calculate split hues
            float hue_c = (hue + 60) % 360;
            float hue_1 = (hue + 180) % 360;
            float hue_2 = (hue + 240) % 360;

            // Convert back to RGB for the square colors
            Color square_c = GSC_HSVtoRGB(hue_c, saturation, value);
            Color square_1 = GSC_HSVtoRGB(hue_1, saturation, value);
            Color square_2 = GSC_HSVtoRGB(hue_2, saturation, value);

            return (square_c, square_1, square_2);
        }
        public static (Color, Color) ColorGetAnalogous(Color baseColor)
        {

            var (hue, saturation, value) = GSC_RGBtoHSV(baseColor);

            // Calculate split hues
            float hue_1 = (hue + 30) % 360;
            float hue_2 = (hue - 30) % 360;

            // Convert back to RGB for the square colors
            Color analog1 = GSC_HSVtoRGB(hue_1, saturation, value);
            Color analog2 = GSC_HSVtoRGB(hue_2, saturation, value);

            return (analog1, analog2);
        }
        public static (float hue, double saturation, double value) GSC_RGBtoHSV(Color color)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            float hue = color.GetHue();
            double saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            double value = max / 255d;

            return (hue, saturation, value);
        }
        public static Color GSC_HSVtoRGB(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        public static List<Color> ColorGenerateGradient(Color startColor, Color endColor, int steps)
        {
            List<Color> colors = new List<Color>();

            // Calculate the step size for each channel
            float stepA = (endColor.A - startColor.A) / (float)(steps - 1);
            float stepR = (endColor.R - startColor.R) / (float)(steps - 1);
            float stepG = (endColor.G - startColor.G) / (float)(steps - 1);
            float stepB = (endColor.B - startColor.B) / (float)(steps - 1);

            // Generate the colors
            for (int i = 0; i < steps; i++)
            {
                int a = (int)(startColor.A + stepA * i);
                int r = (int)(startColor.R + stepR * i);
                int g = (int)(startColor.G + stepG * i);
                int b = (int)(startColor.B + stepB * i);

                colors.Add(Color.FromArgb(a, r, g, b));
            }

            return colors;
        }

        public static List<Color> ColorGenerateGradient3(Color start, Color middle, Color end, int totalColors)
        {
            //List<Color> gradient = ColorGenerateGradient3(startColor, middleColor, endColor, 9);

            List<Color> gradient = new List<Color>();
            int colorsOut = totalColors; ;

            // If totalColors is even, add 1 to ensure the correct distribution
            if (Math.Floor((totalColors - 1) / 2.0) + Math.Floor((totalColors - 1) / 2.0) < totalColors)
            {
            colorsOut += 1;
            }

            // Calculate how many colors in each segment
            int segmentColors = (colorsOut - 1) / 2;
            int gap = 0;


            // First segment: start to middle
            for (int i = 0; i <= segmentColors; i++)
            {
                float t = (float)i / segmentColors;
                gradient.Add(InterpolateColor(start, middle, t));
            }

            // Second segment: middle to end
            for (int i = 1; i <= segmentColors; i++) // Skip middle color to avoid duplication
            {
                float t = (float)i / segmentColors;
                gradient.Add(InterpolateColor(middle, end, t));
            }

            return gradient;
        }
        public static List<Color> ColorGenerateGradient5(List<Color> baseColors, int totalColors)
        {


            // There will be 9 colors in total
            Color[] gradientColors = new Color[9];

            // Assign the base colors to their respective positions
            gradientColors[0] = baseColors[0]; // Position 1
            gradientColors[2] = baseColors[1]; // Position 3
            gradientColors[4] = baseColors[2]; // Position 5
            gradientColors[6] = baseColors[3]; // Position 7
            gradientColors[8] = baseColors[4]; // Position 9

            // Interpolate colors for positions 2, 4, 6, 8
            for (int i = 0; i < 4; i++)
            {
                gradientColors[i * 2 + 1] = InterpolateColor(gradientColors[i * 2], gradientColors[i * 2 + 2], 0.5f);
            }

            return gradientColors.ToList();

        }
        public static Color InterpolateColor(Color color1, Color color2, float t)
        {
            // Linear interpolation between two colors
            int r = Math.Clamp((int)(color1.R + (color2.R - color1.R) * t), 0 , 255);
            int g = Math.Clamp((int)(color1.G + (color2.G - color1.G) * t), 0, 255);
            int b = Math.Clamp((int)(color1.B + (color2.B - color1.B) * t), 0, 255);

            return Color.FromArgb(r, g, b);
        }



    }
}
