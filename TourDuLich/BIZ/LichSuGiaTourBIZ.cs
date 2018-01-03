using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace BIZ
{
    public class LichSuGiaTourBIZ
    {
        public IRepository<lichsugiatour> lichsu;
        public LichSuGiaTourBIZ()
        {
            lichsu = new GenericRepository<lichsugiatour>();
        }

        public bool add(lichsugiatour entity)
        {
            return lichsu.Add(entity);
        }

        public bool update(lichsugiatour entity)
        {
            return lichsu.Attach(entity);
        }

        public void capnhatgiatour(tour tourcapnhatgia)
        {
            lichsugiatour lsgt = new lichsugiatour();
            lsgt = find(tourcapnhatgia.id);
            if (lsgt.giatour != tourcapnhatgia.giatour)
            {
                lsgt.ngayketthuc = tourcapnhatgia.ngaycapnhat;
                update(lsgt);
                lichsugiatour ls = new lichsugiatour();
                ls.idtour = tourcapnhatgia.id;
                ls.giatour = tourcapnhatgia.giatour;
                ls.ngaybatdau = tourcapnhatgia.ngaycapnhat;
                ls.ngayketthuc = null;
                add(ls);
            }
        }

        public lichsugiatour find(int value)
        {
            lichsugiatour a = new lichsugiatour();
            a = lichsu.Find(c => c.idtour == value).OrderByDescending(c => c.id).FirstOrDefault();
            return a;
        }

        public List<lichsudto> thongketheothoigian(DateTime tungay, DateTime denngay,string tentour)
        {
            List<lichsudto> a = lichsu.Find(c => (c.tour.tentour == tentour && (c.ngayketthuc >= tungay && c.ngaybatdau <= denngay)) || (c.tour.tentour == tentour && (denngay >= c.ngaybatdau && c.ngayketthuc == null))).Select(c => new lichsudto(c)).OrderBy(c => c.matour).ToList();
            return a;
        }
    }
}
