using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIZ;

namespace TourDuLich_WEB.Controllers
{
    public class TinhHinhTourController : Controller
    {
        // GET: HoatDongTour
        private tourdulichEntities db = new tourdulichEntities();
        DoandulichBIZ doanbiz = new DoandulichBIZ();
        TourBIZ tourbiz = new TourBIZ();
        ThongKeTinhHinhTourBIZ tktinhhinhtour = new ThongKeTinhHinhTourBIZ();
        LichSuGiaTourBIZ lsgiatour = new LichSuGiaTourBIZ();
        public ActionResult Index()
        {
            ViewBag.tour = tourbiz.Select();
            if (Session["listdoandulich"] == null)
                Session["listdoandulich"] = new List<doandulich>();
            return View();
        }
        [HttpPost]
        public ActionResult Statistics(FormCollection Statisticsform)
        {
            if (Request.Form["statistics"] != null)
            {
                if (int.Parse(Statisticsform["tour"]) == -1)
                {
                    Session["loi"] = " Bạn chưa chọn tour !";
                    goto back;
                }
                int id = int.Parse(Statisticsform["tour"]);
                Session["id"] = id;
                var listdoan = doanbiz.GetByTourId(id);
                int matour = tourbiz.findEntry(id).idtour;
                Session["tentour"] = tourbiz.findEntry(id).tentour;
                /*Session["starttour"] = lsgiatour.find(matour).ngaybatdau;
                Session["endtour"] = lsgiatour.find(matour).ngayketthuc;*/
                Session["listdoandulich"] = listdoan;
                Session["sodoanthamgia"] = tktinhhinhtour.sodoandulichthamgia(listdoan);
                Session["tienloimoitour"] = tktinhhinhtour.tienloimoitour(listdoan);
            }

        back:
            return RedirectToAction("index");
        }
    }
}