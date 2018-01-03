using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;
namespace BIZ
{
    public class TourBIZ
    {
        public IRepository<tour> tour;
        public Dictionary<string, string> validatedictionary = new Dictionary<string, string>();
        public TourBIZ()
        {
            tour = new GenericRepository<tour>();
        }

        public bool add(tour entity)
        {
            if (validate(entity))
                return tour.Add(entity);
            return false;
        }

        public bool update(tour entity)
        {
            if (validate(entity))
                return tour.Attach(entity);
            return false;
        }

        public List<tourdto> list()
        {
            return tour.GetAll().Select(c => new tourdto(c)).ToList();
        }

        public List<string> danhsachtour()
        {
            return tour.GetAll().Select(c => c.tentour).Distinct().ToList();
        }

        public List<string> danhsachtour1()
        {
            return tour.GetAll().Select(c => c.tentour).ToList();
        }

        public List<tourdto> timtour(string value)
        {
            return tour.Find(c => c.tentour == value).Select(c => new tourdto(c)).ToList();
        }

        public tour timtourtheoma(int value)
        {
            return tour.Find(c => c.id == value).FirstOrDefault();
        }

        public int getcurrentid()
        {
            try { return tour.GetQuery().Count(); }
            catch { return 0; }
        }

        public int laymatour(string value)
        {
            return tour.Find(c => c.tentour == value).FirstOrDefault().id;
        }


        public bool find(string value)
        {
            var a = tour.Find(c => c.tentour == value).ToList();
            if (a.Count > 0)
                return false;
            return true;
        }

        public tour laytoursuagia(int value)
        {
            tour a = new tour();
            a = tour.Find(c => c.id == value).FirstOrDefault();
            return a;
        }

        public bool validate(tour entity)
        {
            if (entity.diemden == entity.diemdi) validatedictionary.Add("DIEMDENDIEMDI", "Điểm đến không thể giống điểm đi");
            if (entity.giatour == 0) validatedictionary.Add("GIATOUR", "Chưa nhập giá tour");
            if (entity.dacdiem.Trim().Length == 0) validatedictionary.Add("DACDIEM", "Chưa nhập đặc điểm tour");
            if (entity.tentour.Trim().Length == 0) validatedictionary.Add("TENTOUR", "Chưa nhập tên tour");
            if (validatedictionary.Count() <= 0)
                return true;
            return false;
        }
        public Dictionary<int, string> Select()
        {
            Dictionary<int, string> select = tour.GetQuery().Select(c => new { c.id, c.tentour }).ToDictionary(x => x.id, x => x.tentour);
            return select;
        }
        public Dictionary<int, string> Selectwithgiave()
        {
            Dictionary<int, string> select = tour.GetQuery().Select(c => new { c.id, c.tentour,c.giatour }).ToDictionary(x => x.id, x => x.tentour+" / "+ x.giatour);
            return select;
        }
        public tour findEntry(int id)
        {
            return tour.Find(x => x.id == id).FirstOrDefault();
        }

        public double TienTour(int id)
        {
            return tour.Find(c => c.id == id).FirstOrDefault().giatour;
        }
    }
}
