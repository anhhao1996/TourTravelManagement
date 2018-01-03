using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace BIZ
{
    public class ThongKeTinhHinhTourBIZ
    {
        public IRepository<doandulich> ddl;
        public ThongKeTinhHinhTourBIZ()
        {
            ddl = new GenericRepository<doandulich>();
        }
        public int sodoandulichthamgia(List<doandulich> listddl)
        {
            return listddl.Count;
        }
        public double tienloimoitour(List<doandulich> listddl)
        {
            double tienkh = 0, tienan = 0, tienpt = 0, tienchiphikhac = 0, tongtiendoan = 0;
            double tienloi;
            foreach (doandulich item in listddl)
            {
                tienkh += item.tongtienkhachsan;
                tienpt += item.tongtienphuongtien;
                tienan += item.tongtienan;
                tienchiphikhac += item.tongtienchiphikhac;
                tongtiendoan += item.tongtientour;
            }
            tienloi = tongtiendoan - (tienkh + tienpt + tienan + tienchiphikhac);
            return tienloi;
        }
    }
}
