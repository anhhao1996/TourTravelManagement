using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIZ;

namespace TourDuLich_WEB.Controllers
{
    public class ChiphiTourController : Controller
    {
        // GET: ChiphiTour
        private tourdulichEntities db = new tourdulichEntities();
        DoandulichBIZ doanbiz = new DoandulichBIZ();
        TourBIZ tourbiz = new TourBIZ();
        public ActionResult Index()
        {
            ViewBag.tour = tourbiz.Select();
            if(Session["listdoandulich"]==null)
                Session["listdoandulich"] = new List<doandulich>();
            return View();
        }
        [HttpPost]
        public ActionResult Statistics(FormCollection Statisticsform)
        {
            if(Request.Form["statistics"]!=null)
            {
                if(int.Parse(Statisticsform["tour"])==-1)
                {
                    Session["loi"] = " Bạn chưa chọn tour !";
                    goto back;
                }
                int id = int.Parse(Statisticsform["tour"]);
                Session["id"] = id;
                if (Statisticsform["datefrom"] == "")
                {
                    Session["loi"] = " Bạn chưa chọn ngày bắt đầu !";
                    goto back;
                }
                DateTime start = DateTime.Parse(Statisticsform["datefrom"]);

                start = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0);
                
                if (Statisticsform["dateend"] == "")
                {
                    Session["loi"] = " Bạn chưa chọn ngày kết thúc !";
                    goto back;
                }

                DateTime end = DateTime.Parse(Statisticsform["dateend"]);
                if(start>end)
                {
                    Session["loi"] = " Ngày kết thúc phải lới hơn ngày bắt đầu !";
                    goto back;
                }
                Session["end"] = end;
                Session["start"] = start;
                end = new DateTime(end.Year, end.Month, end.Day, 23, 59, 59);
                double tienkh = 0, tienan = 0, tienpt = 0, tienchiphikhac = 0;
                var listdoan = doanbiz.GetbyTour(id,start,end);
                foreach ( var item in listdoan)
                {
                    tienkh +=item.tongtienkhachsan;
                    tienpt += item.tongtienphuongtien;
                    tienan += item.tongtienan;
                    tienchiphikhac += item.tongtienchiphikhac;
                }
                Session["listdoandulich"] = listdoan;
                Session["tongtienks"] = tienkh;
                Session["tongtienpt"] = tienpt;
                Session["tongtienan"] = tienan;
                Session["tongtienchiphikhac"] = tienchiphikhac;
                Session["tentour"] = tourbiz.findEntry(id).tentour;
            }
            back:
            return RedirectToAction("index");
        }
    }
}