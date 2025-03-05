using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ErrataUI
{
    public class ErrataFlatLinkLabel : LinkLabel
    {
        private string _url;

        public ErrataFlatLinkLabel()
        {
            Font = new Font("Segoe UI", 10, FontStyle.Underline);
            ForeColor = Color.Blue;
            LinkColor = Color.Blue;
            ActiveLinkColor = Color.DarkBlue;
            VisitedLinkColor = Color.Purple;
            AutoSize = true;

            LinkBehavior = LinkBehavior.HoverUnderline;

            // Handle Click Event
            this.LinkClicked += ErrataFlatLinkLabel_LinkClicked;
        }

        public new string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                _url = value; // Set URL when text is set
            }
        }

        public string Url
        {
            get => _url;
            set => _url = value;
        }

        private void ErrataFlatLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_url))
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = _url,
                        UseShellExecute = true // Opens in default browser
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to open the link: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
