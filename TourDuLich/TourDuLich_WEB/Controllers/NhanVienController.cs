using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIZ;

namespace TourDuLich_WEB.Controllers
{
    public class NhanVienController : Controller
    {
        // GET: NhanVien
        private tourdulichEntities db = new tourdulichEntities();
        ThongKeDiTourBIZ tkditourbiz = new ThongKeDiTourBIZ();
        public ActionResult Index()
        {
            /*ViewBag.danhsach = new SelectList(db.nhanviens, "id", "tennhanvien");
            if (Session["listnhanvien"] == null)
                Session["listnhanvien"] = new List<nhanvien>();*/
            if (Session["listdoannhanvien"] == null)
                Session["listdoannhanvien"] = new List<doannhanvien>();
            return View();
        }
        [HttpPost]
        public ActionResult Statistics(FormCollection Statisticsform)
        {
            if (Request.Form["statistics"] != null)
            {
                int idNhanVien;
                if (Statisticsform["NhanVien"] == "")
                {
                    Session["loi"] = " Chưa nhập mã nhân viên ";
                    goto back;
                }
                else
                {
                    idNhanVien = int.Parse(Statisticsform["NhanVien"]);
                    Session["id"] = idNhanVien;
                }
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
                if (start > end)
                {
                    Session["loi"] = " Ngày kết thúc phải lớn hơn ngày bắt đầu !";
                    goto back;
                }
                Session["enddate"] = end;
                Session["startdate"] = start;
                end = new DateTime(end.Year, end.Month, end.Day, 23, 59, 59);
                var listdnv = tkditourbiz.thongketheothoigian(start, end, idNhanVien);
                var nhvien = db.nhanviens.SingleOrDefault(c => c.id == idNhanVien);
                if (listdnv.Count > 0)
                { 
                    Session["listdoannhanvien"] = listdnv;
                    Session["solanditour"] = tkditourbiz.solanditour(listdnv);
                    Session["tennhanvien"] = nhvien.tennhanvien;
                }
                else
                {
                    if (nhvien != null)
                    {
                        Session["solanditour"] = 0;
                        Session["tennhanvien"] = nhvien.tennhanvien;
                    }
                    else
                    {
                        Session["loi"] = " Không tồn tại mã nhân viên !";
                        goto back;
                    }
                }
            }
          back:
          return RedirectToAction("index");
        }

    }
}