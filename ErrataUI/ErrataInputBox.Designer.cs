namespace ErrataUI
{
    partial class ErrataInputBox
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnOK = new ErrataButton();
            btnCancel = new ErrataButton();
            lblPrompt = new ErrataFlatLabel();
            txtInput = new TextBox();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.BackColor = Color.FromArgb(25, 128, 56);
            btnOK.BackColorRole = ThemeManager.UIRole.PrimaryButtonBackground;
            btnOK.BackColorTheme = ThemeManager.ThemeColorShade.SemanticA_500;
            btnOK.BorderColor = Color.FromArgb(128, 128, 128);
            btnOK.BorderRadius = 0;
            btnOK.BorderRole = ThemeManager.UIRole.PrimaryButtonBackground;
            btnOK.BorderSize = 0;
            btnOK.BorderTheme = ThemeManager.ThemeColorShade.Neutral_500;
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 150, 220);
            btnOK.FlatAppearance.MouseOverBackColor = Color.FromArgb(15, 150, 220);
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.Font = new Font("Segoe UI Semibold", 10F);
            btnOK.ForeColor = Color.FromArgb(250, 250, 250);
            btnOK.ForeColorRole = ThemeManager.UIRole.PrimaryButtonText;
            btnOK.ForeColorTheme = ThemeManager.ThemeColorShade.Neutral_100;
            btnOK.GradientAngle = 90F;
            btnOK.GradientColor = Color.FromArgb(0, 77, 23);
            btnOK.GradientColorRole = ThemeManager.UIRole.TitleBar;
            btnOK.GradientColorTheme = ThemeManager.ThemeColorShade.SemanticA_750;
            btnOK.GradientFill = true;
            btnOK.IgnoreMouseBackColor = false;
            btnOK.IgnoreMouseForeColor = true;
            btnOK.IgnoreRoles = true;
            btnOK.IgnoreTheme = false;
            btnOK.Location = new Point(349, 120);
            btnOK.MouseDownBackColor = Color.FromArgb(15, 150, 220);
            btnOK.MouseDownBackColorRole = ThemeManager.UIRole.PrimaryButtonBackground;
            btnOK.MouseDownBackColorTheme = ThemeManager.ThemeColorShade.Primary_500;
            btnOK.MouseDownForeColor = Color.FromArgb(186, 186, 186);
            btnOK.MouseDownForeRole = ThemeManager.UIRole.BodyTextL1;
            btnOK.MouseDownForeTheme = ThemeManager.ThemeColorShade.Neutral_200;
            btnOK.MouseOverBackColor = Color.FromArgb(15, 150, 220);
            btnOK.MouseOverBackColorRole = ThemeManager.UIRole.PrimaryButtonBackground;
            btnOK.MouseOverBackColorTheme = ThemeManager.ThemeColorShade.Primary_500;
            btnOK.MouseOverForeColor = Color.FromArgb(140, 140, 140);
            btnOK.MouseOverForeRole = ThemeManager.UIRole.BodyTextL1;
            btnOK.MouseOverForeTheme = ThemeManager.ThemeColorShade.Neutral_400;
            btnOK.Name = "btnOK";
            btnOK.Selectable = true;
            btnOK.Size = new Size(110, 28);
            btnOK.TabIndex = 4;
            btnOK.Text = "OK";
            btnOK.Type = ErrataButton.ButtonType.None;
            btnOK.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(15, 150, 220);
            btnCancel.BackColorRole = ThemeManager.UIRole.PrimaryButtonBackground;
            btnCancel.BackColorTheme = ThemeManager.ThemeColorShade.Primary_500;
            btnCancel.BorderColor = Color.FromArgb(128, 128, 128);
            btnCancel.BorderRadius = 0;
            btnCancel.BorderRole = ThemeManager.UIRole.PrimaryButtonBackground;
            btnCancel.BorderSize = 0;
            btnCancel.BorderTheme = ThemeManager.ThemeColorShade.Neutral_500;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.MouseDownBackColor = Color.FromArgb(15, 150, 220);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(15, 150, 220);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 10F);
            btnCancel.ForeColor = Color.FromArgb(250, 250, 250);
            btnCancel.ForeColorRole = ThemeManager.UIRole.PrimaryButtonText;
            btnCancel.ForeColorTheme = ThemeManager.ThemeColorShade.Neutral_100;
            btnCancel.GradientAngle = 90F;
            btnCancel.GradientColor = Color.FromArgb(0, 109, 165);
            btnCancel.GradientColorRole = ThemeManager.UIRole.TitleBar;
            btnCancel.GradientColorTheme = ThemeManager.ThemeColorShade.Primary_700;
            btnCancel.GradientFill = true;
            btnCancel.IgnoreMouseBackColor = false;
            btnCancel.IgnoreMouseForeColor = true;
            btnCancel.IgnoreRoles = true;
            btnCancel.IgnoreTheme = false;
            btnCancel.Location = new Point(236, 120);
            btnCancel.MouseDownBackColor = Color.FromArgb(15, 150, 220);
            btnCancel.MouseDownBackColorRole = ThemeManager.UIRole.PrimaryButtonBackground;
            btnCancel.MouseDownBackColorTheme = ThemeManager.ThemeColorShade.Primary_500;
            btnCancel.MouseDownForeColor = Color.FromArgb(186, 186, 186);
            btnCancel.MouseDownForeRole = ThemeManager.UIRole.BodyTextL1;
            btnCancel.MouseDownForeTheme = ThemeManager.ThemeColorShade.Neutral_200;
            btnCancel.MouseOverBackColor = Color.FromArgb(15, 150, 220);
            btnCancel.MouseOverBackColorRole = ThemeManager.UIRole.PrimaryButtonBackground;
            btnCancel.MouseOverBackColorTheme = ThemeManager.ThemeColorShade.Primary_500;
            btnCancel.MouseOverForeColor = Color.FromArgb(140, 140, 140);
            btnCancel.MouseOverForeRole = ThemeManager.UIRole.BodyTextL1;
            btnCancel.MouseOverForeTheme = ThemeManager.ThemeColorShade.Neutral_400;
            btnCancel.Name = "btnCancel";
            btnCancel.Selectable = true;
            btnCancel.Size = new Size(110, 28);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.Type = ErrataButton.ButtonType.None;
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // lblPrompt
            // 
            lblPrompt.BackColor = Color.Transparent;
            lblPrompt.Font = new Font("Segoe UI", 10F);
            lblPrompt.ForeRole = ThemeManager.UIRole.BodyTextL1;
            lblPrompt.ForeTheme = ThemeManager.ThemeColorShade.Neutral_800;
            lblPrompt.IgnoreRoles = true;
            lblPrompt.IgnoreTheme = false;
            lblPrompt.LineAlignment = StringAlignment.Center;
            lblPrompt.Location = new Point(10, 26);
            lblPrompt.Name = "lblPrompt";
            lblPrompt.OffsetX = 0;
            lblPrompt.OffsetY = 0;
            lblPrompt.Size = new Size(449, 61);
            lblPrompt.Style = ErrataFlatLabel.TextStyle.Caption;
            lblPrompt.TabIndex = 6;
            lblPrompt.Text = "Prompt";
            lblPrompt.TextAlignment = StringAlignment.Center;
            // 
            // txtInput
            // 
            txtInput.Location = new Point(10, 93);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(449, 23);
            txtInput.TabIndex = 7;
            txtInput.Text = "Output";
            txtInput.TextAlign = HorizontalAlignment.Right;
            txtInput.WordWrap = false;
            // 
            // ErrataInputBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(468, 158);
            ControlBox = false;
            Controls.Add(txtInput);
            Controls.Add(lblPrompt);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            FormTitle = "InputBox";
            MaximizeBox = false;
            Name = "ErrataInputBox";
            Padding = new Padding(1, 20, 1, 1);
            ShowIcon = false;
            Text = "InputBox";
            TitleBarHeight = 20;
            TitleExtensionColor = Color.FromArgb(15, 150, 220);
            TitleExtensionColorGradient = Color.FromArgb(0, 109, 165);
            TitleExtensionWidth = 20;
            Load += ErrataInputBox_Load;
            Controls.SetChildIndex(btnOK, 0);
            Controls.SetChildIndex(btnCancel, 0);
            Controls.SetChildIndex(lblPrompt, 0);
            Controls.SetChildIndex(txtInput, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public ErrataButton btnOK;
        public ErrataButton btnCancel;
        public ErrataFlatLabel lblPrompt;
        public TextBox txtInput;
    }
}