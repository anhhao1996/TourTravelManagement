using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace BIZ
{
    public class ThongKeDiTourBIZ
    {
        public IRepository<doannhanvien> dnv;
        public IRepository<doandulich> ddl;
        public IRepository<nhiemvu> nv;
        public IRepository<nhanvien> nhanviendb;
        public Dictionary<string, string> validatedictionary = new Dictionary<string, string>();
        public ThongKeDiTourBIZ()
        {
            dnv = new GenericRepository<doannhanvien>();
            ddl = new GenericRepository<doandulich>();
            nv = new GenericRepository<nhiemvu>();
            nhanviendb = new GenericRepository<nhanvien>();
        }
        public List<doannhanvien> thongketheothoigian(DateTime tungay, DateTime denngay, int idNhanvien)
        {
            List<doannhanvien> listdoannv = new List<doannhanvien>();
            listdoannv = dnv.Find(c => (c.idnhanvien == idNhanvien && ((tungay <= c.doandulich.ngaykhoihanh) && (c.doandulich.ngayketthuc <= denngay)))).ToList();
            /*List<doandulich> listdoandulich = new List<doandulich>();
            foreach (doannhanvien doannv in listdoannv)
            {
                doandulich doandulich = new doandulich();
                doandulich = ddl.Find(b => b.id == doannv.iddoan).SingleOrDefault();
                listdoandulich.Add(doandulich);
            }*/
            return listdoannv;
        }
        public List<doannhanvien> thongketheothoigian(DateTime tungay, DateTime denngay, String tenNhanvien, int idNhanvien)
        {
            nhanvien nhvien = nhanviendb.Find(a => (a.id == idNhanvien || a.tennhanvien == tenNhanvien)).SingleOrDefault();
            List<doannhanvien> listdoannv = new List<doannhanvien>();
            listdoannv = dnv.Find(c => (c.idnhanvien == nhvien.id && ((tungay <= c.doandulich.ngaykhoihanh) && (c.doandulich.ngayketthuc <= denngay)))).ToList();
            return listdoannv;
        }
        public int solanditour(List<doannhanvien> listdnv)
        {
            List<doandulich> listdoandulich = new List<doandulich>();
            foreach (doannhanvien doannv in listdnv)
            {
                doandulich doandulich = new doandulich();
                doandulich = ddl.Find(b => b.id == doannv.iddoan).SingleOrDefault();
                listdoandulich.Add(doandulich);
            }
            return listdoandulich.Count;
        }
        public string laytennhiemvu(doandulich doandulich, int idnhanvien)
        {
            doannhanvien doannv = new doannhanvien();
            doannv = dnv.Single(c => (c.iddoan == doandulich.id && c.idnhanvien == idnhanvien));
            String tennhv = doannv.nhiemvu.tennhiemvu;
            return tennhv;
        }
    }
}
