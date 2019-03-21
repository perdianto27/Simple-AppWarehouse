using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppWarehouse
{
    public partial class FMenu : Form
    {
        public FMenu()
        {
            InitializeComponent();
        }

        private void FMenu_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
        }

        private void rakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRak f = new FRak();
            f.Show();
            f.MdiParent = this;
        }

        private void barangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FBarang f = new FBarang();
            f.Show();
            f.MdiParent = this;
        }

        private void keluarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FPrint f = new FPrint();
            f.Show();
            f.MdiParent = this;
        }
    }
}
