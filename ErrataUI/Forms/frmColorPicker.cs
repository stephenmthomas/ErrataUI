using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErrataUI.Forms
{
    public partial class frmColorPicker : ErrataForm
    {
        private Bitmap swatchOriginal;

        // Helper method to calculate the displayed rectangle of the BackgroundImage
        private Rectangle GetBackgroundImageRectangle(PictureBox pictureBox)
        {
            Image bgImage = pictureBox.BackgroundImage;
            if (bgImage == null) return Rectangle.Empty;

            Size imgSize = bgImage.Size;
            Size clientSize = pictureBox.ClientSize;

            switch (pictureBox.BackgroundImageLayout)
            {
                case ImageLayout.None:
                    return new Rectangle(0, 0, imgSize.Width, imgSize.Height);

                case ImageLayout.Tile:
                    return new Rectangle(0, 0, clientSize.Width, clientSize.Height);

                case ImageLayout.Center:
                    int x = (clientSize.Width - imgSize.Width) / 2;
                    int y = (clientSize.Height - imgSize.Height) / 2;
                    return new Rectangle(x, y, imgSize.Width, imgSize.Height);

                case ImageLayout.Stretch:
                    return new Rectangle(0, 0, clientSize.Width, clientSize.Height);

                case ImageLayout.Zoom:
                    float scale = Math.Min((float)clientSize.Width / imgSize.Width, (float)clientSize.Height / imgSize.Height);
                    int zoomWidth = (int)(imgSize.Width * scale);
                    int zoomHeight = (int)(imgSize.Height * scale);
                    x = (clientSize.Width - zoomWidth) / 2;
                    y = (clientSize.Height - zoomHeight) / 2;
                    return new Rectangle(x, y, zoomWidth, zoomHeight);

                default:
                    return Rectangle.Empty;
            }
        }
        private void SetBackgroundImageAlpha(PictureBox pictureBox, float alpha)
        {
            // Ensure alpha is between 0 (fully transparent) and 1 (fully opaque)
            alpha = Math.Clamp(alpha, 0f, 1f);

            // Get the current background image
            if (pictureBox.BackgroundImage == null)
                return;


            Bitmap transparentImage = new Bitmap(swatchOriginal.Width, swatchOriginal.Height);

            using (Graphics g = Graphics.FromImage(transparentImage))
            {
                // Create a color matrix with the specified alpha
                ColorMatrix colorMatrix = new ColorMatrix
                {
                    Matrix33 = alpha // Set alpha value
                };

                // Create image attributes and apply the color matrix
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                // Draw the original image with the alpha adjustment
                Rectangle rect = new Rectangle(0, 0, swatchOriginal.Width, swatchOriginal.Height);
                g.DrawImage(swatchOriginal, rect, 0, 0, swatchOriginal.Width, swatchOriginal.Height, GraphicsUnit.Pixel, attributes);
            }

            // Set the new transparent image to the PictureBox's BackgroundImage
            pictureBox.BackgroundImage = transparentImage;
        }

        public Color dialogColor { get; set; }
        private static readonly Color DEFAULT_COLOR = Color.Red;
        public frmColorPicker() : this(DEFAULT_COLOR) { } // Overloaded constructor

        public frmColorPicker(Color initialColor)
        {
            InitializeComponent();
            colP.BackColor = initialColor;

            int A = initialColor.A;
            int R = initialColor.R; //to hex = .ToString("X")
            int G = initialColor.G;
            int B = initialColor.B;
            txtR.Text = R.ToString();
            txtG.Text = G.ToString();
            txtB.Text = B.ToString();
            txtA.Text = A.ToString();
        }

        private void frmToolsColorPicker_Load(object sender, EventArgs e)
        {
            DoColorUpdate(colP.BackColor);
            swatchOriginal = new Bitmap(pbSwatch.BackgroundImage);
            LoadFavorites();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            SaveFavorites();
            this.dialogColor = colP.BackColor;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmdHelp_Click(object sender, EventArgs e)
        {
            
        }

        private void SaveFavorites()
        {
            List<Color> colors = new List<Color> { };

            foreach (Control control in pnlFavorites.Controls)
            {
                if (control is PictureBox pictureBox)
                {
                    colors.Add(pictureBox.BackColor);
                }
            }
            ThemeManager.PaletteSaveToFile(colors, "colors.favorites");
        }

        private void LoadFavorites()
        {
            if (!File.Exists("colors.favorites")) { Debug.Print("No colors.favorites!"); return; }

            List<Color> colors = ThemeManager.PaletteLoadFromFile("colors.favorites");
            int line = 0;
            foreach (Control control in pnlFavorites.Controls)
            {
                if (control is PictureBox pictureBox)
                {
                    pictureBox.BackColor = colors[line];
                    line += 1;
                }
            }
        }
        private void pbSwatch_Click(object sender, EventArgs e)
        {

        }

        private void pbSwatch_MouseClick(object sender, MouseEventArgs e)
        {
            if (pbSwatch.BackgroundImage is Bitmap bitmap)
            {
                // Get the scaling and offset based on BackgroundImageLayout
                Rectangle imageRect = GetBackgroundImageRectangle(pbSwatch);

                // Check if the click is within the image bounds
                if (imageRect.Contains(e.Location))
                {
                    // Map the click coordinates to the image coordinates
                    int x = (int)((e.X - imageRect.X) * (float)bitmap.Width / imageRect.Width);
                    int y = (int)((e.Y - imageRect.Y) * (float)bitmap.Height / imageRect.Height);

                    // Ensure coordinates are within the valid range
                    if (x >= 0 && x < bitmap.Width && y >= 0 && y < bitmap.Height)
                    {
                        // Get the color of the pixel
                        Color pixelColor = bitmap.GetPixel(x, y);
                        DoColorUpdate(pixelColor);


                    }
                }
            }
        }

        private void DoColorUpdate(Color pixelColor)
        {
            int A = pixelColor.A;
            int R = pixelColor.R; //to hex = .ToString("X")
            int G = pixelColor.G;
            int B = pixelColor.B;

            var CMYK = ThemeManager.ConvertRGBToCMYK(pixelColor);
            double C = Math.Round(CMYK.C, 2);
            double M = Math.Round(CMYK.M, 2);
            double Y = Math.Round(CMYK.Y, 2);
            double K = Math.Round(CMYK.K, 2);

            var HSV = ThemeManager.ConvertRGBtoHSV(pixelColor);
            double H = Math.Round(HSV.H, 0);
            double S = Math.Round(HSV.S, 2);
            double V = Math.Round(HSV.V, 2);

            var HSL = ThemeManager.ConvertRGBtoHSL(pixelColor);
            //H same as HSV
            double S2 = Math.Round(HSL.S, 2);
            double L = Math.Round(HSL.L, 2);

            var HSLA = ThemeManager.ConvertRGBtoHSLA(pixelColor);
            double A2 = Math.Round(HSLA.A, 2);

            colP.BackColor = Color.FromArgb(A, R, G, B);

            lblHex.Text = $"#{R.ToString("X2")}{G.ToString("X2")}{B.ToString("X2")}";
            lblRGB.Text = $"{R},{G},{B}";
            lblARGB.Text = $"{A},{R},{G},{B}";
            lblCMYK.Text = $"{C},{M},{Y},{K}";
            lblHSV.Text = $"{H}, {S}, {V}";
            lblHSL.Text = $"{H}, {S2}, {L}";
            lblHSLA.Text = $"{H}, {S2}, {L}, {A2}";

            txtR.Text = R.ToString();
            txtG.Text = G.ToString();
            txtB.Text = B.ToString();
            txtA.Text = A.ToString();
        }

        private void txtR_TextChanged(object sender, EventArgs e)
        {
            TextBox tBox = sender as TextBox;

            if (tBox.Text == "") { return; }

            if (Convert.ToInt32(tBox.Text) > 255) { tBox.Text = "255"; }

            //if it cant convert to an integer, its wrong... so exit
            if (!int.TryParse(tBox.Text, out _))
            {
                return;
            }

            string alphaString = txtA.Text; // Alpha value (0-255)
            string redString = txtR.Text;    // Red value (0-255)
            string greenString = txtG.Text;     // Green value (0-255)
            string blueString = txtB.Text;      // Blue value (0-255)

            // Convert strings to integers
            int alpha = int.Parse(txtA.Text);
            int red = int.Parse(txtR.Text);
            int green = int.Parse(txtG.Text);
            int blue = int.Parse(txtB.Text);

            // Create a Color object from the RGBA values
            Color color = Color.FromArgb(alpha, red, green, blue);
            //colSender.BackColor = color;
            //colP.BackColor = color;

            tbR.Value = Convert.ToInt16(txtR.Text);
            tbG.Value = Convert.ToInt16(txtG.Text);
            tbB.Value = Convert.ToInt16(txtB.Text);
            tbA.Value = Convert.ToInt16(txtA.Text);

            DoColorUpdate(color);
        }

        private void tbR_Scroll(object sender, EventArgs e)
        {
            txtR.Text = tbR.Value.ToString();
            txtG.Text = tbG.Value.ToString();
            txtB.Text = tbB.Value.ToString();
            txtA.Text = tbA.Value.ToString();
        }

        private void tbAlpha_Scroll(object sender, EventArgs e)
        {
            float alpha = (float)tbAlpha.Value / 100;
            SetBackgroundImageAlpha(pbSwatch, 1 - alpha);
        }

        private void colP_BackColorChanged(object sender, EventArgs e)
        {
            Color col = Color.FromArgb(colP.BackColor.R, colP.BackColor.G, colP.BackColor.B);
            pbCM1.BackColor = col;
            pbCM2.BackColor = col;
            pbCM3.BackColor = col;
            pbCM4.BackColor = col;
            pbCM5.BackColor = col;
            pbCM6.BackColor = col;
            pbCM7.BackColor = col;

        }

        private void pbF1_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pBox = sender as PictureBox;

            if (e.Button == MouseButtons.Left)
            {
                //colSender = pBox;
                //colP.BackColor = pBox.BackColor;

                Color color = pBox.BackColor; // Example: Semi-transparent red

                // Decompose the color into RGBA components
                int alpha = color.A; // Alpha component
                int red = color.R;   // Red component
                int green = color.G; // Green component
                int blue = color.B;  // Blue component

                txtR.Text = $"{red}";
                txtG.Text = $"{green}";
                txtB.Text = $"{blue}";
                txtA.Text = $"{alpha}";

                return;
            }
            else if (e.Button == MouseButtons.Right)
            {
                pBox.BackColor = colP.BackColor;
                SaveFavorites();
            }
        }

        private void pbCM1_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pBox = sender as PictureBox;

            if (e.Button == MouseButtons.Right)
            {
                Color color = pBox.BackColor; // Example: Semi-transparent red

                // Decompose the color into RGBA components
                int alpha = color.A; // Alpha component
                int red = color.R;   // Red component
                int green = color.G; // Green component
                int blue = color.B;  // Blue component

                txtR.Text = $"{red}";
                txtG.Text = $"{green}";
                txtB.Text = $"{blue}";
                txtA.Text = $"{alpha}";
                return;
            }
        }

        private void pbCM1_BackColorChanged(object sender, EventArgs e)
        {
            pbCM1_1.BackColor = ThemeManager.ColorGetComplementary(pbCM1.BackColor);
        }

        private void pbCM2_BackColorChanged(object sender, EventArgs e)
        {
            var (col1, col2) = ThemeManager.ColorGetSplitComplementary(pbCM2.BackColor);
            pbCM2_1.BackColor = col1;
            pbCM2_2.BackColor = col2;
        }

        private void pbCM3_BackColorChanged(object sender, EventArgs e)
        {
            var (col1, col2) = ThemeManager.ColorGetTriadic(pbCM3.BackColor);
            pbCM3_1.BackColor = col2;
            pbCM3_2.BackColor = col1;
        }

        private void pbCM4_BackColorChanged(object sender, EventArgs e)
        {
            var (col1, col2, col3) = ThemeManager.ColorGetTetradic(pbCM4.BackColor);
            pbCM4_1.BackColor = col1;
            pbCM4_2.BackColor = col2;
            pbCM4_3.BackColor = col3;
        }

        private void pbCM5_BackColorChanged(object sender, EventArgs e)
        {
            var (col1, col2, col3) = ThemeManager.ColorGetSquare(pbCM5.BackColor);
            pbCM5_1.BackColor = col1;
            pbCM5_2.BackColor = col2;
            pbCM5_3.BackColor = col3;
        }

        private void pbCM6_BackColorChanged(object sender, EventArgs e)
        {
            var (col1, col2) = ThemeManager.ColorGetAnalogous(pbCM6.BackColor);
            pbCM6_1.BackColor = col1;
            pbCM6_2.BackColor = col2;
        }

        private void pbCM7_BackColorChanged(object sender, EventArgs e)
        {
            if (lblChroma.Text == "Monochromatica")
            {
                List<Color> colors = ThemeManager.ColorGenerateGradient(pbCM7.BackColor, Color.White, 4);
                pbCM7_1.BackColor = colors[1];
                pbCM7_2.BackColor = colors[2];
                pbCM7_3.BackColor = colors[3];
            }
            else
            {
                List<Color> colors = ThemeManager.ColorGenerateGradient(pbCM7.BackColor, Color.Black, 4);
                pbCM7_1.BackColor = colors[1];
                pbCM7_2.BackColor = colors[2];
                pbCM7_3.BackColor = colors[3];
            }

        }


        private void pbRandom_Click(object sender, EventArgs e)
        {
            Color rand = ThemeManager.GenerateRandomColor(chkRandA.Checked);
            txtA.Text = rand.A.ToString();
            txtR.Text = rand.R.ToString();
            txtG.Text = rand.G.ToString();
            txtB.Text = rand.B.ToString();
        }


        private void lblChroma_MouseEnter(object sender, EventArgs e)
        {
            lblChroma.ForeColor = SystemColors.Highlight;
        }

        private void lblChroma_MouseLeave(object sender, EventArgs e)
        {
            lblChroma.ForeColor = SystemColors.ControlText;
        }

        private void lblChroma_Click(object sender, EventArgs e)
        {
            if (lblChroma.Text == "Monochromatica") { lblChroma.Text = "Monochromatico"; } else { lblChroma.Text = "Monochromatica"; }

            if (lblChroma.Text == "Monochromatica")
            {
                List<Color> colors = ThemeManager.ColorGenerateGradient(pbCM7.BackColor, Color.White, 4);
                pbCM7_1.BackColor = colors[1];
                pbCM7_2.BackColor = colors[2];
                pbCM7_3.BackColor = colors[3];
            }
            else
            {
                List<Color> colors = ThemeManager.ColorGenerateGradient(pbCM7.BackColor, Color.Black, 4);
                pbCM7_1.BackColor = colors[1];
                pbCM7_2.BackColor = colors[2];
                pbCM7_3.BackColor = colors[3];
            }

        }

        private void cmdHex_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblHex.Text);
        }

        private void cmdRGB_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblRGB.Text);
        }

        private void cmdARGB_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblARGB.Text);
        }

        private void cmdCMYK_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblCMYK.Text);
        }

        private void cmdHSV_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblHSV.Text);
        }

        private void cmdHSL_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblHSL.Text);
        }

        private void cmdHSLA_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblHSLA.Text);
        }
    }
}
