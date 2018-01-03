using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BIZ;

namespace TourDuLich_WEB.Controllers
{
    public class DoanKhachController : Controller
    {
        // GET: DoanKhach
        DoandulichBIZ doanbiz = new DoandulichBIZ();
        KhachBIZ khachbiz = new KhachBIZ();
        DoanKhachBIZ doankhachbiz = new DoanKhachBIZ();
        [HttpGet, OutputCache(NoStore = true, Duration = 1)]
        public ActionResult Index()
        {
            ViewBag.doandulich = doanbiz.Select();
            return View();
        }
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id > 0)
            {
                Session["doan"] = doanbiz.find((int)id);
            }
            else
            {
                Session["loi"] = "Bạn chưa chọn đoàn !";
                return RedirectToAction("index");
            }
            if (Session["flagkh"] == null) Session["flagkh"] = false;
                var doankh = doankhachbiz.getListbyDoan((int)id);
            if (Session["doankh"] == null && doankh.Count > 0)
            {
                Session["doankh"] = doankh;
            }
            else if (Session["doankh"] == null) Session["doankh"] = new List<doankhachhang>();

            Session["listkhachhang"] = khachbiz.Getlist();
            return View();

        }
        [HttpPost]
        public ActionResult Update(FormCollection formdetail)
        {
            List<doankhachhang> doankh = (List<doankhachhang>)Session["doankh"];
            doandulich doandulich = (doandulich)Session["doan"];
            if (Request.Form["addnew"] != null)
            {
                Session["flagkh"] = true;
                if(formdetail["name"] =="")
                {
                    Session["loi"] = "Bạn chưa nhập tên khách hàng !";
                    goto Back;
                }
                if (formdetail["sdt"] == "")
                {
                    Session["loi"] = "Bạn chưa nhập số điện thoại !";
                    goto Back;
                }
                if (formdetail["cmt"] == "")
                {
                    Session["loi"] = "Bạn chưa nhập số chứng minh thư !";
                    goto Back;
                }
                if (formdetail["address"] == "")
                {
                    ViewBag.loi = "Bạn chưa nhập địa chỉ !";
                    goto Back;
                }
                khachhang kh = khachbiz.AddreturnEntry(formdetail["name"], formdetail["sdt"], formdetail["cmt"], formdetail["address"], int.Parse(formdetail["sex"]));
               
                doankhachhang doankhtemp = new doankhachhang();
                doankhtemp.iddoan = doandulich.id;
                doankhtemp.idkhachhang = kh.id;
                doankhtemp.khachhang = kh;
                doankh.Add(doankhtemp);
                Session["doankh"] = doankh;
                goto Back;
            }
            if(Request.Form["luulist"]!=null)
            {
                if ((bool)Session["flagkh"] == true)
                {
                    doandulich doantemp = doanbiz.find(doandulich.id);
                    doankhachbiz.deleteByDoan(doandulich.id);
                    int numberkh = doankh.Count;
                    foreach (var item in doankh)
                    {
                        doankhachhang kh = new doankhachhang();
                        kh.iddoan = item.iddoan;
                        kh.idkhachhang = item.idkhachhang;
                        doankhachbiz.Add(kh);
                    }
                    double giatour = (new TourBIZ()).TienTour(doandulich.idtour);
                    double total = giatour * numberkh;
                    doantemp.tongtientour = total;
                    doanbiz.UpdateTongtienDoan(doantemp);
                    Session["thongbao"] = "Cập nhật thành công danh sách !";
                    return RedirectToAction("index");
                }
                goto Back;

            }
            if (Request.Form["cancel"] != null)
            {
                    return RedirectToAction("index");
            }
            Back:
            return RedirectToAction("Detail","DoanKhach",new { id= doandulich.id });
        }
        public ActionResult deleteCT(int iddoan, int idct)
        {
            if(idct > 0)
            {
                List<doankhachhang> doankh = (List<doankhachhang>)Session["doankh"];
                doankh.RemoveAt(idct - 1);
                Session["doankh"] = doankh;
                Session["flagkh"] = true;
            }
            return RedirectToAction("Detail", "DoanKhach", new { id = iddoan });
        }

        public ActionResult AddCT(int iddoan, int idct)
        {

            if (idct > 0 && iddoan > 0)
            {
                List<doankhachhang> doankh = (List<doankhachhang>)Session["doankh"];
                var khach = khachbiz.find(idct);
                bool answer = doankh.Any(x => x.idkhachhang == khach.id);
                if(answer==true)
                {
                    Session["loi"] = " Khách hàng "+khach.tenkhachhang + " đã có trong danh sách !";
                    goto Back;
                }
                doankhachhang temp = new doankhachhang();
                temp.iddoan = iddoan;
                temp.idkhachhang = khach.id;
                temp.khachhang = khach;
                doankh.Add(temp);
                Session["doankh"] = doankh;
                Session["flagkh"] = true;
            }
                Back:
                return RedirectToAction("Detail", "DoanKhach", new { id = iddoan });
        }
    }
   
}