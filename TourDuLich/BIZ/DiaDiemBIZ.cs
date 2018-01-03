using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace BIZ
{
    public class DiaDiemBIZ
    {
        public IRepository<diadiem> diadiem;
        public Dictionary<string, string> validatedictionary = new Dictionary<string, string>();
        public DiaDiemBIZ()
        {
            diadiem = new GenericRepository<diadiem>();
        }

        public bool add(diadiem entity)
        {
            if(validate(entity))          
                return diadiem.Add(entity);
            return false;
        }

        public List<diadiemdto> list()
        {
            return diadiem.GetAll().Select(c => new diadiemdto(c)).ToList();
        }

        public List<string> listdiadiemtheotinh(int value)
        {
            return diadiem.Find(c => c.idtinh == value).Select(c => c.tendiadiem).ToList();
        }

        public List<diadiemdto> listchitietdiadiemtheotinh(int value)
        {
            return diadiem.Find(c => c.idtinh == value).Select(c => new diadiemdto(c)).ToList();
        }

        public int layiddiadiem(string value)
        {
            return diadiem.Find(c => c.tendiadiem == value).FirstOrDefault().id;
        }

        public int getcurrentid()
        {
            try { return diadiem.GetQuery().Count(); }
            catch { return 0; }
        }

        public bool find(string value, int value2)
        {
            var a = diadiem.Find(c => c.tendiadiem == value && c.idtinh == value2).ToList();
            if (a.Count > 0)
                return false;
            return true;
        }

        public bool validate(diadiem entity)
        {
            if (entity.tendiadiem.Trim().Length == 0) validatedictionary.Add("TENDIADIEM", "Không được để trống tên địa điểm");
            if (entity.idtinh == 0) validatedictionary.Add("TENTINH", "Chưa chọn tên tỉnh");
            if (find(entity.tendiadiem, entity.idtinh) == false) validatedictionary.Add("DATONTAI", "Đã tồn tại địa điểm trong CSDL");
            if (validatedictionary.Count() <= 0)
                return true;
            return false;
        }
    }
}
