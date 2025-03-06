using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ErrataUI;

namespace ErrataUI.ExampleForms
{
    public partial class frmExampleWinForms : ErrataForm
    {
        public frmExampleWinForms()
        {
            InitializeComponent();
        }

        private void frmExampleWinForms_Load(object sender, EventArgs e)
        {
            egpDragExample.Draggable(true);
        }
    }
}
