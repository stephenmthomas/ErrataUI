namespace ErrataUI
{
    partial class ErrataListBox
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
            listBox = new ListBox();
            SuspendLayout();
            // 
            // listBox
            // 
            listBox.BackColor = SystemColors.Control;
            listBox.BorderStyle = BorderStyle.None;
            listBox.Dock = DockStyle.Fill;
            listBox.Font = new Font("Segoe UI", 10F);
            listBox.FormattingEnabled = true;
            listBox.ItemHeight = 17;
            listBox.Location = new Point(2, 2);
            listBox.Margin = new Padding(0);
            listBox.Name = "listBox";
            listBox.Size = new Size(146, 82);
            listBox.TabIndex = 0;
            // 
            // ErrataListBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Margin = new Padding(0);
            Name = "ErrataListBox";
            Size = new Size(150, 86);
            Load += ErrataListBox_Load;
            ResumeLayout(false);
        }

        #endregion

        private ErrataPanelSimple pnlBorder;
        private ListBox listBox;
    }
}
