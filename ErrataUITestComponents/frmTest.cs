using ErrataUI;

namespace ErrataUITestComponents
{
    public partial class frmTest : ErrataForm
    {
        public frmTest()
        {
            InitializeComponent();
        }

        private void errataDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmTest_Load(object sender, EventArgs e)
        {




            //var _form3 = new ErrataUI.ExampleForms.frmExample_ButtonsFocus();
            //_form3.Show();
        }

        private void btnThemeMgr_Click(object sender, EventArgs e)
        {
            var _form = new ErrataUI.Forms.frmThemeEditor();
            _form.Show();
        }

        private void btnDrawerToggle_Click(object sender, EventArgs e)
        {
            eDrawer.BringToFront();
            eDrawer.Toggle();


        }

        private void errataPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void errataButton6_Click(object sender, EventArgs e)
        {

        }

        private void btnFocusButtons_Click(object sender, EventArgs e)
        {
            var _form2 = new ErrataUI.ExampleForms.frmExampleWinForms();
            _form2.Show();
        }

        private void errataFlatMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
