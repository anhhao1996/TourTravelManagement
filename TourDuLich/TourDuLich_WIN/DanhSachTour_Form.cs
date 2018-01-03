using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MODEL;
using BIZ;

namespace TourDuLich_WIN
{
    public partial class DanhSachTour_Form : Form
    {
        public DanhSachTour_Form()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DanhSachTour_Form_Load(object sender, EventArgs e)
        {
            //combobox tên tour
            TourBIZ tb = new TourBIZ();
            List<string> tour = tb.danhsachtour();
            comboBox1.DataSource = tour;
            dataGridView1.DataSource = tb.list();
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 180;
            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[6].Width = 150;
        }


        private void sua (object sender, EventArgs e)
        {
            try
            {
                TourBIZ tb = new TourBIZ();
                tour temp = new tour();
                temp = tb.timtourtheoma(Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                SuaGia_Form form = new SuaGia_Form(temp);
                form.Show();
            }
            catch { }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                TourBIZ tb = new TourBIZ();
                cttqBIZ cttqb = new cttqBIZ();
                dataGridView2.DataSource = cttqb.listdanhsachthamquan(Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                dataGridView2.Columns[0].Width = 80;
                dataGridView2.Columns[1].Width = 150;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TourBIZ tb = new TourBIZ();
            dataGridView1.DataSource = tb.timtour(comboBox1.SelectedItem.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TourBIZ tb = new TourBIZ();
            dataGridView1.DataSource = tb.list();
            dataGridView2.DataSource = null;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    //Tao menu ngu canh
                    ContextMenuStrip menu = new ContextMenuStrip();
                    menu.Items.Add("Sửa thông tin", null, new EventHandler(sua));
                    //Di chuyen den dong hien hanh
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    //hien thi menu
                    Point pt = dataGridView1.PointToClient(Control.MousePosition);
                    menu.Show(dataGridView1, pt.X, pt.Y);
                }
            }
        }
    }
}
