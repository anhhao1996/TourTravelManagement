using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace BIZ
{
    public class DoanKhachSanBIZ
    {
        public IRepository<doankhachsan> doankhachsan;
        public DoanKhachSanBIZ()
        {
            doankhachsan = new GenericRepository<doankhachsan>();
        }
        public IList<doankhachsan> findByDoan(int id)
        {
            return doankhachsan.GetQuery().Where(c=>c.iddoan==id).ToList();
        }
        public bool Add(doankhachsan doan)
        {
            return doankhachsan.Add(doan);
        }
        public void deleteByDoan(int id)
        {
            doankhachsan.GetQuery().Where(c => c.iddoan == id).ToList().ForEach(p => doankhachsan.Delete(p));
        }
    }
    public class DoanphuongtienBIZ
    {
        public IRepository<doanphuongtien> doanpt;
        public DoanphuongtienBIZ()
        {
            doanpt = new GenericRepository<doanphuongtien>();
        }
        public IList<doanphuongtien> findByDoan(int id)
        {
            return doanpt.GetQuery().Where(c => c.iddoan == id).ToList();
        }
        public bool Add(doanphuongtien doan)
        {
            return doanpt.Add(doan);
        }
        public void deleteByDoan(int id)
        {
            doanpt.GetQuery().Where(c => c.iddoan == id).ToList().ForEach(p => doanpt.Delete(p));
        }
        
    }
    public class DoanBuaAnBIZ
    {
        public IRepository<doanbuaan> doanbuaan;
        public DoanBuaAnBIZ()
        {
            doanbuaan = new GenericRepository<doanbuaan>();
        }
        public IList<doanbuaan> findByDoan(int id)
        {
            return doanbuaan.GetQuery().Where(c => c.iddoan == id).ToList();
        }
        public bool Add(doanbuaan doan)
        {
            return doanbuaan.Add(doan);
        }
        public void deleteByDoan(int id)
        {
            doanbuaan.GetQuery().Where(c => c.iddoan == id).ToList().ForEach(p => doanbuaan.Delete(p));
        }

    }
    public class DoanchiphikhacBIZ
    {
        public IRepository<doanphikhac> doanchiphikhac;
        public DoanchiphikhacBIZ()
        {
            doanchiphikhac = new GenericRepository<doanphikhac>();
        }
        public IList<doanphikhac> findByDoan(int id)
        {
            return doanchiphikhac.GetQuery().Where(c => c.iddoan == id).ToList();
        }
        public bool Add(doanphikhac doan)
        {
            return doanchiphikhac.Add(doan);
        }
        public void deleteByDoan(int id)
        {
            doanchiphikhac.GetQuery().Where(c => c.iddoan == id).ToList().ForEach(p => doanchiphikhac.Delete(p));
        }
    }
    public class DoannhanvienBIZ
    {
        public IRepository<doannhanvien> doannv;
        public DoannhanvienBIZ()
        {
            doannv = new GenericRepository<doannhanvien>();
        }
        public IList<doannhanvien> findByDoan(int id)
        {
            return doannv.GetQuery().Where(c => c.iddoan == id).ToList();
        }
        public bool Add(doannhanvien doan)
        {
            return doannv.Add(doan);
        }
        public void deleteByDoan(int id)
        {
            doannv.GetQuery().Where(c => c.iddoan == id).ToList().ForEach(p => doannv.Delete(p));
        }
    }
    
    public class DoanKhachBIZ
    {
        public IRepository<doankhachhang> doankhachhang;
        public DoanKhachBIZ()
        {
            doankhachhang = new GenericRepository<doankhachhang>();
        }

        public doankhachhang find(int id)
        {
            return doankhachhang.Find(c => c.id == id).FirstOrDefault();
        }
        public IList<doankhachhang> findByDoan(int id)
        {
            return doankhachhang.GetQuery().Where(c => c.iddoan == id).ToList();
        }
        public IList<doankhachhang> findByKhach(int id)
        {
            return doankhachhang.GetQuery().Where(c => c.idkhachhang == id).ToList();
        }
        public IList<doankhachhang> getListbyDoan(int id)
        {
            
            return doankhachhang.GetQuery().Where(c=>c.iddoan==id).ToList();
           
        }
        public void deleteByDoan(int id)
        {
            doankhachhang.GetQuery().Where(c => c.iddoan == id).ToList().ForEach(p => doankhachhang.Delete(p));
        }
        public bool Add(doankhachhang doan)
        {
            return doankhachhang.Add(doan);
        }

    }
}
