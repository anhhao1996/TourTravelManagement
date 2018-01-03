using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace BIZ
{
    public class TinhThanhBIZ
    {
        public IRepository<tinh> tinhrp;
        public IRepository<khachsan> ksrp;
        public Dictionary<string, string> validatedictionary = new Dictionary<string, string>();
        public TinhThanhBIZ()
        {
            tinhrp = new GenericRepository<tinh>();
            ksrp = new GenericRepository<khachsan>();
        }

        public bool add(tinh entity)
        {
            if(validate(entity))
             return tinhrp.Add(entity);
            return false;
        }

        public List<tinhdto> list()
        {
            return tinhrp.GetAll().Select(c => new tinhdto(c)).ToList();
        }
        public int getcurrentid()
        {
            try { return tinhrp.GetQuery().Count(); }
            catch { return 0; }
        }
        public bool find(string value)
        {
            var a = tinhrp.Find(c => c.tentinh == value).ToList();
            if (a.Count > 0)
                return false;
            return true;
        }

        public List<string> danhsachtinh()
        {
            return tinhrp.GetAll().Select(c => c.tentinh).Distinct().ToList();
        }

        public List<khachsandto> danhsachkstheotinh(int value)
        {
            return ksrp.Find(c => c.idtinh == value).Select(c => new khachsandto(c)).ToList();
        }

        public int laymatinh(string value)
        {
            return tinhrp.Find(c => c.tentinh == value).FirstOrDefault().id;
        }

        public string laytentinh(int value)
        {
            return tinhrp.Find(c => c.id == value).FirstOrDefault().tentinh;
        }

        public bool validate(tinh entity)
        {
            if (entity.tentinh.Trim().Length == 0) validatedictionary.Add("TENTINH", "Không được để trống tên tỉnh");
            if (find(entity.tentinh) == false) validatedictionary.Add("DATONTAI", "Đã tồn tại tỉnh trong CSDL");
            if (validatedictionary.Count() <= 0)
                return true;
            return false;
        }
    }
}
