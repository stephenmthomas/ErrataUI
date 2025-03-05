namespace ErrataUI
{
    partial class ErrataTextboxFlutro
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox = new ErrataTextBox();
            separator = new ErrataSeparator();
            SuspendLayout();
            // 
            // textBox
            // 
            textBox.BackColor = Color.White;
            textBox.Font = new Font("Segoe UI", 12F);
            textBox.ForeColor = Color.Black;
            textBox.Location = new Point(3, 3);
            textBox.Name = "textBox";
            textBox.Size = new Size(188, 22);
            textBox.TabIndex = 2;
            textBox.TextChanged += textBox_TextChanged;
            // 
            // separator
            // 
            separator.IgnoreRoles = false;
            separator.IgnoreTheme = false;
            separator.isVertical = false;
            separator.LineColor = Color.FromArgb(0, 128, 200);
            separator.linePattern = ErrataSeparator.LinePattern.Solid;
            separator.LineRole = ThemeManager.UIRole.SectionDivider;
            separator.LineStack = ErrataSeparator.lineStack.Single;
            separator.LineTheme = ThemeManager.ThemeColorShade.Primary_500;
            separator.Location = new Point(3, 23);
            separator.MinimumSize = new Size(1, 1);
            separator.Name = "separator";
            separator.Offset = 2;
            separator.Size = new Size(188, 10);
            separator.TabIndex = 1;
            separator.Text = "errataSeparator1";
            separator.Thickness = 1.5F;
            // 
            // ErrataTextboxFlutro
            // 
            Controls.Add(separator);
            Controls.Add(textBox);
            Name = "ErrataTextboxFlutro";
            Size = new Size(194, 31);
            Load += ErrataTextboxFlutro_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ErrataTextBox textBox;
        private ErrataSeparator separator;
    }
}
