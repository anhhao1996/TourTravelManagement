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
    public partial class LoaiTour_Form : Form
    {        
        public LoaiTour_Form()
        {
            InitializeComponent();
        }
        private void clearform()
        {
            LoaiTourBIZ ltb = new LoaiTourBIZ();
            textBox2.Clear();
            textBox1.Text = (ltb.getcurrentid() + 1).ToString();
            this.ActiveControl = textBox2;
            textBox2.SelectAll();
        }
        private void tablewidth()
        {
            dataGridView1.Columns[0].Width = 130;
            dataGridView1.Columns[1].Width = 175;
        }

        public void ViewErrors(Dictionary<string, string> Dictionary)
        {
            errorProvider1.Clear();
            foreach (var entry in Dictionary)
            {
                switch (entry.Key)
                {
                    case "TENLOAI":
                        errorProvider1.SetError(textBox2, entry.Value);
                        break;
                    case "DATONTAI":
                        errorProvider1.SetError(textBox1, entry.Value);
                        break;
                    default:
                        break;
                }
            }
        }

        private void LoaiTour_Form_Load(object sender, EventArgs e)
        {
            LoaiTourBIZ ltb = new LoaiTourBIZ();
            textBox1.Enabled = false;
            textBox1.Text = (ltb.getcurrentid() + 1).ToString();
            dataGridView1.DataSource = ltb.list();
            tablewidth();
        }

        //ADD
        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            LoaiTourBIZ ltb = new LoaiTourBIZ();
            loaitour entity = new loaitour();
            entity.id = Int32.Parse(textBox1.Text);
            entity.tenloaitour = textBox2.Text;
            bool check = ltb.add(entity);
            if (check)
            {
                MessageBox.Show("Thêm Thành Công");
                dataGridView1.DataSource = ltb.list();
                clearform();
            }
            else
            {
                MessageBox.Show("Thêm Thất Bại");
                ViewErrors(ltb.validatedictionary);
            }                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
