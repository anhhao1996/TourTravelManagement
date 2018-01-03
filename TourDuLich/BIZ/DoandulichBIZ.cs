using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace BIZ
{
    public class DoandulichBIZ
    {
        public IRepository<doandulich> doandulich;
        public DoandulichBIZ()
        {
            doandulich = new GenericRepository<doandulich>();
        }
        public bool Addnew(int idtour , string name ,DateTime ngaykhoihanh,DateTime ngayketthuc,double tienve)
        {
            doandulich doan= new doandulich();
            doan.idtour = idtour;
            doan.ngaykhoihanh = ngaykhoihanh;
            doan.ngayketthuc = ngayketthuc;
            doan.ngaytao = DateTime.Now;
            doan.tendoan = name;
            doan.tienve = tienve;

            return doandulich.Add(doan);
        }
        public doandulich find(int id)
        {
            return doandulich.Find(c=>c.id==id).FirstOrDefault();
        }
        public bool UpdateBasic(int id, int idtour, string name, DateTime ngaykhoihanh, DateTime ngayketthuc, double tienve)
        {
            doandulich doan = doandulich.Find(c => c.id == id).FirstOrDefault();
            doan.idtour = idtour;
            doan.ngaykhoihanh = ngaykhoihanh;
            doan.ngayketthuc = ngayketthuc;
            doan.ngaytao = DateTime.Now;
            doan.tendoan = name;
            doan.tienve = tienve;

            return doandulich.Update(doan);
        }
        public bool Update(doandulich doan)
        {
            return doandulich.Update(doan);
        }
        public bool UpdateTongtienDoan(doandulich doan)
        {
            return doandulich.Attach(doan);
        }
        public bool delete(int id)
        {
            doandulich doan = doandulich.Find(x=>x.id == id).FirstOrDefault();
            return doandulich.Delete(doan);
        }
        public IList<doandulich> Getlist()
        {
            return doandulich.GetAll().OrderByDescending(c => c.id).ToList() ;
        }


        public Dictionary<int,string>Select()
        {
            Dictionary<int, string> select = doandulich.GetQuery().Select(c => new { c.id, c.tendoan }).ToDictionary(x => x.id, x => x.tendoan);
            return select; 
        }

        public IList<doandulich> GetbyTour(int id , DateTime start, DateTime end)
        {
            return doandulich.GetQuery().Where(x => x.idtour == id).Where(x => x.ngaykhoihanh >= start).Where(x => x.ngaykhoihanh <= end).ToList();
        }
        //Khang
        public List<doandulich> GetByTourId(int id)
        {
            return doandulich.GetQuery().Where(x => x.idtour == id).ToList();
        }
    }
}
