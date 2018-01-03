using BIZ;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TourDuLich_WEB.Controllers
{
    public class KhachController : Controller
    {
        // GET: Khach
        KhachBIZ khachbiz = new KhachBIZ();
        DoanKhachBIZ doankhach = new DoanKhachBIZ();
        public ActionResult Index()
        {
            ViewBag.khach = khachbiz.Getlist() ;
            return View();
        }
        [HttpPost]
        public ActionResult Update(FormCollection formkhach)
        {
            if(Request.Form["save"]!=null)
            {
                if(formkhach["name"]== "")
                {
                    Session["loi"] = "Bạn chưa nhập tên khách hàng !";
                    goto Back;
                }
                string name = formkhach["name"];
                if (formkhach["sdt"] == "")
                {
                    Session["loi"] = "Bạn chưa số điện thoại của khách hàng !";
                    goto Back;
                }
                string sdt = formkhach["sdt"];
                if (formkhach["cmt"] == "")
                {
                    Session["loi"] = "Bạn chưa nhập chứng minh thư của khách hàng !";
                    goto Back;
                }
                string cmt = formkhach["cmt"];
                if (formkhach["address"] == "")
                {
                    Session["loi"] = "Bạn chưa nhập địa chỉ của khách hàng !";
                    goto Back;
                }
                string address = formkhach["address"];
                int sex = int.Parse(formkhach["sex"]);
                if (formkhach["id"]!="")
                {
                    int id = int.Parse(formkhach["id"]);
                    if(khachbiz.Update(id,name,sdt,cmt,address,sex))
                    {
                        Session["thongbao"] = "Cập nhật thành công !";
                    }
                    else Session["thongbao"] = "Cập nhật thất bại !";

                    goto Back;
                }
                else
                {

                    if(khachbiz.Addnew(name, sdt, cmt, address, sex))
                    {
                        Session["thongbao"] = " Thêm thành công !";
                    }
                    else
                        Session["thongbao"] = " Thêm thất bại !";
                    goto Back;
                }
            }
            Back:
            return RedirectToAction("Index");
        }
        public ActionResult deletekhach(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var khach = doankhach.findByKhach((int)id);
            var kh = khachbiz.find((int)id);
            if (khach.Count() > 0)
            {
                Session["loi"] = "Không thể xoá . Vì " + kh.tenkhachhang + " đã có trong dữ liệu của  đoàn du lịch nào đó ! ";
                return RedirectToAction("Index");
            }
            else
            {
                if(khachbiz.Delete((int)id))
                {
                    Session["thongbao"] = "Xoá thành công !";
                }
                else
                    Session["loi"] = "Xoá  thất bại !";

                return RedirectToAction("Index");
            }
        }
    }
}