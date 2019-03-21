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
    public partial class FBarang : Form
    {
        String sql;

        public FBarang()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void tambahToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbID.Text = "";
            tbID.ReadOnly = false;

            tbBarang.Text = "";
            tbHarga.Text = "";
            tbMerek.Text = "";
            tbSeri.Text = "";
            nudStok.Value = 0;
            cbxJenis.SelectedIndex = -1;
            cbxRak.SelectedIndex = 0;//karena sudah berisi data dari rak
        }

        private void simpanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbID.ReadOnly == false)
                {
                    sql = "INSERT into barang VALUES ('" + tbID.Text + "', " + 
                        "'" + tbBarang.Text + "'," + nudStok.Value + "," +
                        "'" + tbMerek.Text + "','" + tbSeri.Text + "'," +
                        "'" + cbxJenis.SelectedItem.ToString() + "'," +
                        "'" + cbxRak.SelectedValue.ToString() + "'," +
                        "" + tbHarga.Text + ",'" + dtpExpiredDate.Value.ToString("yyyy-MM-dd") +
                        "')";

                }
                else
                {
                    sql = "UPDATE barang SET barang = '" + tbBarang.Text + "', stok = "+ nudStok.Value + ", " + 
                        "merek='" + tbMerek.Text + "', seri ='"+ tbSeri.Text + "', jenis ='" + cbxJenis.SelectedItem.ToString() + "', " +
                        "rak ='" + cbxRak.SelectedValue.ToString() + "', harga = " + tbHarga.Text + ", "+
                        "expired_date = '" + dtpExpiredDate.Value +"' where id_barang ='" +tbID.Text+"'";
                }
                DBClass db = new DBClass(); //panggil class db
                db.execute(sql);
                showGrid();
                tambahToolStripMenuItem_Click(null, null); //panggil tombol tambah
                MessageBox.Show(tbBarang.Text + " tersimpan"); //message box
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
        }

        private void hapusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbID.ReadOnly == true)
                {
                    DialogResult res = MessageBox.Show("Hapus " + tbID.Text + "?", "Perhatian",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Information);
                    if (res == DialogResult.OK)
                    {
                        sql = "delete from rak where id_rak = '" + tbID.Text + "'";
                        DBClass db = new DBClass(); //panggil class db
                        db.execute(sql);
                        showGrid();
                        MessageBox.Show(tbID.Text + " dihapus"); //message box
                        tambahToolStripMenuItem_Click(null, null); //panggil tombol tambah
                    }
                    else if (res == DialogResult.Cancel)
                    {
                        MessageBox.Show("Penghapusan data " + tbID.Text + " dibatalkan");
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

        private void showGrid()
        {
            try
            {
                string sql = "select * from barang order by id_barang desc";
                DBClass db = new DBClass();
                dgv.DataSource = db.read(sql); //set data gridview
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void FBarang_Load(object sender, EventArgs e)
        {
            loadBarang();
            loadRak();
        }

        private void loadRak()
        {
            try
            {
                String sql = "select * from rak order BY id_rak desc";
                DBClass db = new DBClass();

                DataTable dt = new DataTable();
                dt = db.read(sql);

                cbxRak.DataSource = new BindingSource(dt, null);
                cbxRak.DisplayMember = "rak";
                cbxRak.ValueMember = "id_rak";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        private void loadBarang()
        {
            try
            {
                String sql = "SELECT * FROM barang order BY id_barang desc";
                DBClass db = new DBClass();
                DataTable dt = new DataTable();
                dt = db.read(sql);
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        private void tbBarang_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgv.CurrentRow.Index;

            try
            {
                tbID.Text = dgv.Rows[i].Cells[0].Value.ToString();
                tbBarang.Text = dgv.Rows[i].Cells[1].Value.ToString();
                nudStok.Value = Int32.Parse(dgv.Rows[i].Cells[2].Value.ToString());
                tbMerek.Text = dgv.Rows[i].Cells[3].Value.ToString();
                tbSeri.Text = dgv.Rows[i].Cells[4].Value.ToString();
                cbxJenis.SelectedItem = dgv.Rows[i].Cells[5].Value.ToString();
                cbxRak.SelectedValue = dgv.Rows[i].Cells[6].Value.ToString();
                tbHarga.Text = dgv.Rows[i].Cells[7].Value.ToString().Remove(dgv.Rows[i].Cells[7].Value.ToString().Length - 4);
                dtpExpiredDate.Value = DateTime.Parse(dgv.Rows[i].Cells[8].Value.ToString());
                tbID.ReadOnly = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void tbHarga_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
