using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace BIZ
{
    public class LoaiTourBIZ
    {
        public IRepository<loaitour> loaitour;
        public Dictionary<string, string> validatedictionary = new Dictionary<string, string>();
        public LoaiTourBIZ()
        {
            loaitour = new GenericRepository<loaitour>();
        }

        public bool add(loaitour entity)
        {
            if(validate(entity))
                return loaitour.Add(entity);
            return false;
        }

        public List<loaitourdto> list()
        {
            return loaitour.GetAll().Select(c => new loaitourdto(c)).ToList();
        }

        public List<string> danhsachloaitour()
        {
            return loaitour.GetAll().Select(c => c.tenloaitour).Distinct().ToList();
        }

        public int layidloaitour(string value)
        {
            return loaitour.Find(c => c.tenloaitour == value).FirstOrDefault().id;
        }

        public int getcurrentid()
        {
            try { return loaitour.GetQuery().Count(); }
            catch { return 0; }
        }

        public bool find(string value)
        {
            var a = loaitour.Find(c => c.tenloaitour == value).ToList();
            if(a.Count > 0 )
                return false;
            return true;
        }

        public bool validate(loaitour entity)
        {
            if (entity.tenloaitour.Trim().Length == 0) validatedictionary.Add("TENLOAI", "Không được để trống tên loại tour");
            if (find(entity.tenloaitour) == false) validatedictionary.Add("DATONTAI", "Đã tồn tại địa điểm trong CSDL");
            if (validatedictionary.Count() <= 0)
                return true;
            return false;
        }
    }
}
