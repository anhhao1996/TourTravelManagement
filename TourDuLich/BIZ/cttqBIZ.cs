using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MODEL;
using System.ComponentModel;

namespace BIZ
{
    public class cttqBIZ
    {
        tourdulichEntities db = new tourdulichEntities();
        public IRepository<ctthamquan> cttq;
        public cttqBIZ()
        {
            cttq = new GenericRepository<ctthamquan>();
        }

        public List<cttqdto> listdanhsachthamquan(int value)
        {
            return cttq.Find(c => c.idtour == value).Select(c => new cttqdto(c)).ToList();
        }

        public List<suacttqdto> listdanhsachdesua(int value)
        {
            return cttq.Find(c => c.idtour == value).Select(c => new suacttqdto(c)).ToList();
        }

        public void Delete(int idtour)
        {
            var xoa = from c in db.ctthamquans
                      where c.idtour == idtour
                      select c;
            foreach (var detail in xoa)
            {
                db.ctthamquans.Remove(detail);
            }
            db.SaveChanges();
        }
    }
}
