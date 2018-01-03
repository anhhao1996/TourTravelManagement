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
    public partial class SuaGia_Form : Form
    {
        tourdulichEntities db = new tourdulichEntities();
        public SuaGia_Form(tour tourduocchon)
        {
            InitializeComponent();
            //load combobox loaitour & diem di diem den
            LoaiTourBIZ ltb = new LoaiTourBIZ();
            TinhThanhBIZ ttb = new TinhThanhBIZ();
            List<string> loaitour = ltb.danhsachloaitour();
            List<string> tinhdiemdi = ttb.danhsachtinh();
            List<string> tinhdiemden = ttb.danhsachtinh();
            comboBox1.DataSource = tinhdiemdi;
            comboBox2.DataSource = tinhdiemden;
            comboBox3.DataSource = loaitour;
            //đưa giá trị từ tour đã chọn lên form
            textBox1.Text = tourduocchon.id.ToString();
            textBox4.Text = tourduocchon.tentour.ToString();
            numericUpDown1.Value = Decimal.Parse(tourduocchon.giatour.ToString());
            comboBox3.Text = tourduocchon.loaitour.tenloaitour;
            comboBox1.Text = ttb.laytentinh(tourduocchon.diemdi);
            comboBox2.Text = ttb.laytentinh(tourduocchon.diemden);
            richTextBox1.Text = tourduocchon.dacdiem;
            //load combobox tinh thanh
            DiaDiemBIZ ddb = new DiaDiemBIZ();
            List<string> tinhthamquan = ttb.danhsachtinh();
            comboBox5.DataSource = tinhthamquan;
            //combobox địa điểm tham quan
            List<string> diadiemtheotinh = ddb.listdiadiemtheotinh(ttb.laymatinh(comboBox5.SelectedItem.ToString()));
            comboBox4.DataSource = diadiemtheotinh;
            //load danh sach dia diem
            cttqBIZ ctb = new cttqBIZ();
            List<suacttqdto> temp = ctb.listdanhsachdesua(Int32.Parse(textBox1.Text));
            foreach (var item in temp)
            {
                dataGridView1.Rows.Add(dataGridView1.RowCount, item.matour,item.tendiadiem);
            }
            //set enable button luu
            if (dataGridView1.RowCount <= 1)
            {
                button5.Enabled = false;
            }
        }

        public void ViewErrors(Dictionary<string, string> Dictionary)
        {
            errorProvider1.Clear();
            foreach (var entry in Dictionary)
            {
                switch (entry.Key)
                {
                    case "DIEMDENDIEMDI":
                        errorProvider1.SetError(comboBox1, entry.Value);
                        errorProvider1.SetError(comboBox2, entry.Value);
                        break;
                    case "GIATOUR":
                        errorProvider1.SetError(numericUpDown1, entry.Value);
                        break;
                    case "DACDIEM":
                        errorProvider1.SetError(richTextBox1, entry.Value);
                        break;
                    case "TENTOUR":
                        errorProvider1.SetError(textBox4, entry.Value);
                        break;
                    case "DATONTAI":
                        errorProvider1.SetError(textBox1, entry.Value);
                        break;
                    case "CTTQ":
                        errorProvider1.SetError(dataGridView1, entry.Value);
                        break;
                    default:
                        break;
                }
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.DataSource != null)
            {
                DiaDiemBIZ ddb = new DiaDiemBIZ();
                TinhThanhBIZ ttb = new TinhThanhBIZ();
                List<string> diadiemtheotinh = ddb.listdiadiemtheotinh(ttb.laymatinh(comboBox5.SelectedItem.ToString()));
                comboBox4.DataSource = diadiemtheotinh;
                comboBox4.Refresh();
            }
            else
            {

            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    //Tao menu ngu canh
                    ContextMenuStrip menu = new ContextMenuStrip();
                    menu.Items.Add("Xóa", null, new EventHandler(xoa));
                    //Di chuyen den dong hien hanh
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    //hien thi menu
                    Point pt = dataGridView1.PointToClient(Control.MousePosition);
                    menu.Show(dataGridView1, pt.X, pt.Y);
                    
                }
            }
        }

        private void xoa(object sender, EventArgs args)
        {
            try
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = i+1;
                }
                if (dataGridView1.RowCount <= 1)
                    button5.Enabled = false;
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool check = true;
            if (comboBox4.SelectedItem == null)
            {
                MessageBox.Show("Chưa có địa điểm tham quan");
            }
            else
            {
                if (dataGridView1.RowCount > 1)
                {
                    for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[2].Value.ToString() == comboBox4.SelectedItem.ToString())
                        {
                            MessageBox.Show("Đã thêm địa điểm này trước đó");
                            check = false;
                            break;
                        }
                    }
                    if (check)
                    {
                        dataGridView1.Rows.Add(dataGridView1.RowCount, textBox1.Text, comboBox4.SelectedItem.ToString());
                    }
                    else
                    { }
                }
                else
                {
                    dataGridView1.Rows.Add(dataGridView1.RowCount, textBox1.Text, comboBox4.SelectedItem.ToString());
                    button5.Enabled = true;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            TinhThanhBIZ ttb = new TinhThanhBIZ();
            LoaiTourBIZ ltb = new LoaiTourBIZ();
            DiaDiemBIZ ddb = new DiaDiemBIZ();
            TourBIZ tb = new TourBIZ();
            cttqBIZ ctb = new cttqBIZ();
            tour tour = new tour();
            //lưu thông tin tour
            tour = tb.laytoursuagia(Int32.Parse(textBox1.Text));
            tour.tentour = textBox4.Text;
            tour.diemden = ttb.laymatinh(comboBox2.SelectedItem.ToString());
            tour.diemdi = ttb.laymatinh(comboBox1.SelectedItem.ToString());
            tour.dacdiem = richTextBox1.Text;
            tour.giatour = Double.Parse(numericUpDown1.Value.ToString());
            tour.idtour = ltb.layidloaitour(comboBox3.SelectedItem.ToString());
            tour.ngaycapnhat = DateTime.Now;

            //Kiểm tra hàm upadte và cập nhật lại giá tour
            bool check = tb.update(tour);
            if (check)
            {
                // Xóa các địa điểm tham quan của tour trước đó
                ctb.Delete(Int32.Parse(textBox1.Text));
                //Thêm lại các địa điểm tham quan mới
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    ctthamquan ct = new ctthamquan();
                    ct.idtour = Int32.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    ct.iddiadiem = ddb.layiddiadiem(dataGridView1.Rows[i].Cells[2].Value.ToString());
                    tour.ctthamquans.Add(ct);
                }
                tb.update(tour);
                LichSuGiaTourBIZ lsgtb = new LichSuGiaTourBIZ();
                lsgtb.capnhatgiatour(tour);
                MessageBox.Show("Cập nhật thành công");
                this.Close();
            }
            else
            {
                MessageBox.Show("Cập nhật tour thất bại");
                ViewErrors(tb.validatedictionary);
            }
         }
        #region backup
        //sua gia
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    if (Double.Parse(label4.Text) >= Double.Parse(numericUpDown1.Value.ToString()))
        //    {
        //        DialogResult result = MessageBox.Show("Giá mới thấp hơn hoặc bàng giá cũ, bạn có muốn sửa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        //        if (result.Equals(DialogResult.OK))
        //        {
        //            TourBIZ tb = new TourBIZ();
        //            tour tour = new tour();
        //            tour = tb.laytoursuagia(Int32.Parse(textBox1.Text));
        //            tour.giatour = Double.Parse(numericUpDown1.Value.ToString());
        //            tour.ngaycapnhat = DateTime.Now;
        //            bool check = tb.update(tour);
        //            if (check)
        //            {
        //                LichSuGiaTourBIZ lsgtb = new LichSuGiaTourBIZ();
        //                LichSuGiaTourBIZ lsgtb1 = new LichSuGiaTourBIZ();
        //                lichsugiatour ls = new lichsugiatour();
        //                ls = lsgtb.find(tour.id);
        //                ls.ngayketthuc = tour.ngaycapnhat;
        //                lsgtb.update(ls);
        //                lichsugiatour ls1 = new lichsugiatour();
        //                ls1.idtour = tour.id;
        //                ls1.giatour = tour.giatour;
        //                ls1.ngaybatdau = tour.ngaycapnhat;
        //                ls1.ngayketthuc = null;
        //                lsgtb1.add(ls1);
        //                MessageBox.Show("Cập nhật giá thành công");
        //                this.Close();
        //            }
        //            else
        //            {
        //                MessageBox.Show("Cập nhật thất bại");
        //            }
        //        }
        //        else
        //        {

        //        }
        //    }
        //    else
        //    {
        //        TourBIZ tb = new TourBIZ();
        //        tour tour = new tour();
        //        tour = tb.laytoursuagia(Int32.Parse(textBox1.Text));
        //        tour.giatour = Double.Parse(numericUpDown1.Value.ToString());
        //        bool check = tb.update(tour);
        //        if (check)
        //        {
        //            LichSuGiaTourBIZ lsgtb = new LichSuGiaTourBIZ();
        //            LichSuGiaTourBIZ lsgtb1 = new LichSuGiaTourBIZ();
        //            lichsugiatour ls = new lichsugiatour();
        //            ls = lsgtb.find(tour.id);
        //            ls.ngayketthuc = tour.ngaycapnhat;
        //            lsgtb.update(ls);
        //            lichsugiatour ls1 = new lichsugiatour();
        //            ls1.idtour = tour.id;
        //            ls1.giatour = tour.giatour;
        //            ls1.ngaybatdau = tour.ngaycapnhat;
        //            ls1.ngayketthuc = null;
        //            lsgtb1.add(ls1);
        //            MessageBox.Show("Cập nhật giá thành công");
        //            this.Close();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Cập nhật thất bại");
        //        }
        //    }
        //}
        #endregion
    }
}
