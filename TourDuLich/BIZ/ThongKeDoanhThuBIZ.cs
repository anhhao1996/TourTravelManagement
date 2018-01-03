using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;
namespace BIZ
{
    public class ThongKeDoanhThuBIZ
    {
        public IRepository<doandulich> ddl;
        public ThongKeDoanhThuBIZ()
        {
            ddl = new GenericRepository<doandulich>();
        }

        public List<doanhthutourdto> thongketheothoigian(DateTime tungay,DateTime denngay,string tentour)
        {
            return ddl.Find(c => (c.tour.tentour == tentour && ((tungay <= c.ngaytao) && (c.ngaytao <= denngay)))).Select(c => new doanhthutourdto(c)).ToList();
        }

        public Double tinhtong(List<doanhthutourdto> listdoanhthu)
        {
            Double tong = 0;
            foreach (var item in listdoanhthu)
            {
                tong += item.tongthu;
            }
            return tong;
        }
    }
}
