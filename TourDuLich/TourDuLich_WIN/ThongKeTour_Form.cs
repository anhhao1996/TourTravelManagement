using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIZ;
using MODEL;

namespace TourDuLich_WIN
{
    public partial class ThongKeTour_Form : Form
    {
        public ThongKeTour_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TourBIZ tb = new TourBIZ();
            dataGridView1.DataSource = tb.timtour(comboBox1.SelectedItem.ToString());
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 195;
        }

        private void ThongKeTour_Form_Load(object sender, EventArgs e)
        {
            //combobox tour
            TourBIZ tb = new TourBIZ();
            List<string> tour = tb.danhsachtour();
            List<string> tour1 = tb.danhsachtour();
            comboBox1.DataSource = tour;
            comboBox2.DataSource = tour1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime tungay;
            DateTime denngay;
            if (dateTimePicker1.Value.Date <= dateTimePicker2.Value.Date)
            {
                LichSuGiaTourBIZ lsgb = new LichSuGiaTourBIZ();
                tungay = dateTimePicker1.Value.Date;
                denngay = dateTimePicker2.Value.Date.AddDays(1);
                dataGridView1.DataSource = lsgb.thongketheothoigian(tungay,denngay,comboBox1.SelectedItem.ToString());
                dataGridView1.Columns[0].Width = 70;
                dataGridView1.Columns[1].Width = 170;
                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[3].Width = 160;
                dataGridView1.Columns[4].Width = 160;
            }
            else
            {
                MessageBox.Show("Ngày chọn không hợp lệ");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime tungay;
            DateTime denngay;
            if (dateTimePicker3.Value.Date <= dateTimePicker4.Value.Date)
            {
                ThongKeDoanhThuBIZ tkb = new ThongKeDoanhThuBIZ();
                tungay = dateTimePicker3.Value.Date;
                denngay = dateTimePicker4.Value.Date.AddDays(1);
                List<doanhthutourdto> listdoanhthu = tkb.thongketheothoigian(tungay, denngay, comboBox2.SelectedItem.ToString());
                dataGridView2.DataSource = listdoanhthu;
                dataGridView2.Columns[0].Width = 70;
                dataGridView2.Columns[1].Width = 170;
                dataGridView2.Columns[2].Width = 150;
                dataGridView2.Columns[3].Width = 160;
                dataGridView2.Columns[4].Width = 160;
                numericUpDown1.Value = Decimal.Parse(tkb.tinhtong(listdoanhthu).ToString());
            }
            else
            {
                MessageBox.Show("Ngày chọn không hợp lệ");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
