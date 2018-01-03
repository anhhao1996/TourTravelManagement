using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TourDuLich_WIN
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DiaDiem_Form form = new DiaDiem_Form();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoaiTour_Form form = new LoaiTour_Form();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TinhThanh_Form form = new TinhThanh_Form();
            form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tour_Form form = new Tour_Form();
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ThongKeTour_Form form = new ThongKeTour_Form();
            form.Show();
        }
    }
}
