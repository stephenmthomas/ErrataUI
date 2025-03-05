using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErrataUI
{
    public partial class ErrataInputBox : ErrataForm
    {
        public string UserInput { get; private set; } = string.Empty; // Stores user input

        public ErrataInputBox(string prompt, string title = "Input", string defaultValue = "")
        {
            InitializeComponent();

            // Form properties
            FormTitle = title;
            

            // Label

            lblPrompt.Text = prompt; 

            // TextBox
            txtInput.Text = defaultValue;

            // OK Button
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Click += (sender, e) => { UserInput = txtInput.Text; Close(); };

            // Cancel Button
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Click += (sender, e) => Close();
            Controls.Add(btnCancel);

            AcceptButton = btnOK;
            CancelButton = btnCancel;
        }

        // Static method to show the dialog and return the input
        public static string ShowInputBox(string prompt, string title = "Input", string defaultValue = "")
        {
            using (var form = new ErrataInputBox(prompt, title, defaultValue))
            {
                return form.ShowDialog() == DialogResult.OK ? form.UserInput : string.Empty;
            }
        }

        public ErrataInputBox()
        {
            InitializeComponent();
        }

        private void ErrataInputBox_Load(object sender, EventArgs e)
        {

        }
    }
}
