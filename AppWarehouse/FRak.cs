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
    public partial class FRak : Form
    {
        String sql = "";
        public FRak()
        {
            InitializeComponent();
        }

        private void tambahToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tb_id_rak.Text = "";
            tb_rak.Text = "";
            nudbaris.Value = 1;
        }

        private void simpanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_id_rak.ReadOnly == false)
                {
                    sql = "insert into rak values ('" + tb_id_rak.Text + "', '" + tb_rak.Text + "'," +
                                   nudbaris.Value + ")";

                }
                else
                {
                    sql = "update rak set rak = '" + tb_rak.Text + "', baris = " + nudbaris.Value + " " +
                        "where id_rak = '" + tb_id_rak.Text + "'";
                }
                DBClass db = new DBClass(); //panggil class db
                db.execute(sql);
                showGrid();
                tambahToolStripMenuItem_Click(null, null); //panggil tombol tambah
                MessageBox.Show(tb_rak.Text + " tersimpan"); //message box
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
        }

        private void showGrid()
        {
            try
            {
                string sql = "select * from rak order by id_rak desc";
                DBClass db = new DBClass();
                dgv.DataSource = db.read(sql); //set data gridview
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message );
            }
        }

        private void hapusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_id_rak.ReadOnly == true)
                {
                    DialogResult res = MessageBox.Show("Hapus " + tb_id_rak.Text + "?", "Perhatian",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Information);
                    if (res == DialogResult.OK)
                    {
                        sql = "delete from rak where id_rak = '" + tb_id_rak.Text + "'";
                        DBClass db = new DBClass(); //panggil class db
                        db.execute(sql);
                        showGrid();
                        MessageBox.Show(tb_rak.Text + " dihapus"); //message box
                        tambahToolStripMenuItem_Click(null, null); //panggil tombol tambah
                    }
                    else if (res == DialogResult.Cancel)
                    {
                        MessageBox.Show("Penghapusan data " + tb_rak.Text + " dibatalkan");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            showGrid();
        }

        private void cariToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "select * from rak where rak " +
                    "like '%" + toolStripTextBox1.Text + "%' " +
                    "order by id_rak desc";
                DBClass db = new DBClass();
                dgv.DataSource = db.read(sql); //set data gridview
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgv.CurrentRow.Index;

            try
            {
                tb_id_rak.Text = dgv.Rows[i].Cells[0].Value.ToString();
                tb_rak.Text = dgv.Rows[i].Cells[1].Value.ToString();
                nudbaris.Value = Int32.Parse(dgv.Rows[i].Cells[2].Value.ToString());

                tb_id_rak.ReadOnly = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
