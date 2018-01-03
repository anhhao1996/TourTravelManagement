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
    public partial class DiaDiem_Form : Form
    {
        public DiaDiem_Form()
        {
            InitializeComponent();
        }

        private void tablewidth()
        {
            dataGridView1.Columns[0].Width = 87;
            dataGridView1.Columns[1].Width = 160;
            dataGridView1.Columns[2].Width = 85;
        }

        public void ViewErrors(Dictionary<string, string> Dictionary)
        {
            errorProvider1.Clear();
            foreach (var entry in Dictionary)
            {
                switch (entry.Key)
                {
                    case "TENDIADIEM":
                        errorProvider1.SetError(textBox2, entry.Value);
                        break;
                    case "TENTINH":
                        errorProvider1.SetError(comboBox1, entry.Value);
                        break;
                    case "DATONTAI":
                        errorProvider1.SetError(textBox1, entry.Value);
                        break;
                    default:
                        break;
                }
            }
        }
        private void clearform()
        {
            DiaDiemBIZ ddb = new DiaDiemBIZ();
            textBox2.Clear();
            textBox1.Text = (ddb.getcurrentid() + 1).ToString();
            comboBox1.SelectedIndex = 0;
            this.ActiveControl = textBox2;
            textBox2.SelectAll();
        }
        private void DiaDiem_Form_Load(object sender, EventArgs e)
        {
            DiaDiemBIZ ddb = new DiaDiemBIZ();
            //combobox tỉnh
            TinhThanhBIZ ttb = new TinhThanhBIZ();
            List<string> tinh = ttb.danhsachtinh();
            comboBox1.DataSource = tinh;
            dataGridView1.DataSource = ddb.list();
            textBox1.Enabled = false;
            textBox1.Text = (ddb.getcurrentid() + 1).ToString();
            tablewidth();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                errorProvider1.Clear();
                DiaDiemBIZ ddb = new DiaDiemBIZ();
                TinhThanhBIZ ttb = new TinhThanhBIZ();
                diadiem entity = new diadiem();
                entity.id = Int32.Parse(textBox1.Text);
                entity.tendiadiem = textBox2.Text;
                entity.idtinh = ttb.laymatinh(comboBox1.SelectedItem.ToString());
                bool check = ddb.add(entity);
                if (check)
                {
                     MessageBox.Show("Thêm Thành Công");
                     dataGridView1.DataSource = ddb.list();
                     clearform();
                }
                else
                {
                     MessageBox.Show("Thêm Thất Bại");
                     ViewErrors(ddb.validatedictionary);
                }        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
