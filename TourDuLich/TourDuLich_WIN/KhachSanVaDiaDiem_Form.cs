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
    public partial class KhachSanVaDiaDiem_Form : Form
    {
        public KhachSanVaDiaDiem_Form()
        {
            InitializeComponent();
        }

        private void KhachSanVaDiaDiem_Form_Load(object sender, EventArgs e)
        {
            //combobox tỉnh
            TinhThanhBIZ ttb = new TinhThanhBIZ();
            List<string> tinh = ttb.danhsachtinh();
            comboBox1.DataSource = tinh;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TinhThanhBIZ ttb = new TinhThanhBIZ();
            dataGridView1.DataSource = ttb.danhsachkstheotinh(ttb.laymatinh(comboBox1.SelectedItem.ToString()));
            dataGridView1.Columns[1].Width = 155;      
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DiaDiemBIZ ddb = new DiaDiemBIZ();
            TinhThanhBIZ ttb = new TinhThanhBIZ();
            dataGridView1.DataSource = ddb.listchitietdiadiemtheotinh(ttb.laymatinh(comboBox1.SelectedItem.ToString()));
            dataGridView1.Columns[1].Width = 155;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
