using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace BIZ
{
    public class KhachsanBIZ
    {
        public IRepository<khachsan> khachsan;
        public KhachsanBIZ()
        {
            khachsan = new GenericRepository<khachsan>();
        }
        public Dictionary<int, string> Select()
        {
            Dictionary<int, string> select = khachsan.GetQuery().Select(c => new { c.id, c.tenkhachsan }).ToDictionary(c => c.id, c => c.tenkhachsan);
            return select;
        }
        public khachsan find(int id)
        {
            return khachsan.Find(c => c.id == id).FirstOrDefault();
        }
    }
    public class PhuongtienBIZ
    {
        public IRepository<phuongtien> phuongtien;
        public PhuongtienBIZ()
        {
            phuongtien = new GenericRepository<phuongtien>();
        }
        public Dictionary<int, string> Select()
        {
            Dictionary<int, string> select = phuongtien.GetQuery().Select(c => new { c.id, c.tenphuongtien }).ToDictionary(x => x.id, x => x.tenphuongtien);
            return select;
        }
        public phuongtien find(int id)
        {
            return phuongtien.Find(c => c.id == id).FirstOrDefault();
        }
    }
    public class BuaAnBIZ
    {
        public IRepository<chiphibuaan> chiphibuaan;
        public BuaAnBIZ()
        {
            chiphibuaan = new GenericRepository<chiphibuaan>();
        }
        public Dictionary<int, string> Select()
        {
            Dictionary<int, string> select = chiphibuaan.GetQuery().Select(c => new { c.id, c.buaan }).ToDictionary(x => x.id, x => x.buaan);
            return select;
        }
        public chiphibuaan find(int id)
        {
            return chiphibuaan.Find(c => c.id == id).FirstOrDefault();
        }
    }
    public class NhanVienBIZ
    {
        public IRepository<nhanvien> nhanvien;
        public NhanVienBIZ()
        {
            nhanvien = new GenericRepository<nhanvien>();
        }
        public Dictionary<int, string> Select()
        {
            Dictionary<int, string> select = nhanvien.GetQuery().Select(c => new { c.id, c.tennhanvien }).ToDictionary(x => x.id, x => x.tennhanvien);
            return select;
        }
        public nhanvien find(int id)
        {
            return nhanvien.Find(c => c.id == id).FirstOrDefault();
        }
    }
    public class NhiemVuBIZ
    {
        public IRepository<nhiemvu> nhiemvu;
        public NhiemVuBIZ()
        {
            nhiemvu = new GenericRepository<nhiemvu>();
        }

        public Dictionary<int, string> Select()
        {
            Dictionary<int, string> select = nhiemvu.GetQuery().Select(c => new { c.id, c.tennhiemvu }).ToDictionary(x => x.id, x => x.tennhiemvu);
            return select;
        }
        public nhiemvu find(int id)
        {
            return nhiemvu.Find(c => c.id == id).FirstOrDefault();
        }
    }

    public class KhachBIZ
    {
        public IRepository<khachhang> khachhang;
        public KhachBIZ()
        {
            khachhang = new GenericRepository<khachhang>();
        }
        public IList<khachhang> Getlist()
        {
            return khachhang.GetAll().OrderByDescending(c => c.id).ToList();
        }
        public khachhang find(int id)
        {
            return khachhang.Find(c => c.id == id).FirstOrDefault();
        }
        public bool Addnew(string name, string sdt, string cmt, string address, int sex)
        {
            khachhang khach = new khachhang();
            khach.tenkhachhang = name;
            khach.sodienthoai = sdt;
            khach.cmt = cmt;
            khach.diachi = address;
            khach.sex = sex;

            return khachhang.Add(khach);

        }
        public khachhang AddreturnEntry(string name, string sdt, string cmt, string address, int sex)
        {
            khachhang khach = new khachhang();
            khach.tenkhachhang = name;
            khach.sodienthoai = sdt;
            khach.cmt = cmt;
            khach.diachi = address;
            khach.sex = sex;
            khachhang.Add(khach);
            return khach;

        }
        public bool Update(int id, string name, string sdt, string cmt, string address, int sex)
        {
            khachhang khach = khachhang.Find(c => c.id == id).FirstOrDefault();
            khach.tenkhachhang = name;
            khach.sodienthoai = sdt;
            khach.cmt = cmt;
            khach.diachi = address;
            khach.sex = sex;

            return khachhang.Update(khach);

        }

        public bool Delete(int id)
        {
            khachhang khach = find(id);
            return khachhang.Delete(khach);
        }

    }


}
