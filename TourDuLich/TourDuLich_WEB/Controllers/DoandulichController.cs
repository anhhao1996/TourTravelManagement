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
    public class DoandulichController : Controller
    {
        DoandulichBIZ doanbiz = new DoandulichBIZ();
        DoanKhachSanBIZ doanksbiz = new DoanKhachSanBIZ();
        DoanKhachBIZ doankhach = new DoanKhachBIZ();
        DoanphuongtienBIZ doanptbiz = new DoanphuongtienBIZ();
        DoanBuaAnBIZ doanbabiz = new DoanBuaAnBIZ();
        DoanchiphikhacBIZ doanchiphikhacbiz = new DoanchiphikhacBIZ();
        DoannhanvienBIZ doannvbiz = new DoannhanvienBIZ();
        TourBIZ tourbiz = new TourBIZ();
        // GET: DoanKhach
        [HttpGet, OutputCache(NoStore = true, Duration = 1)]
        public ActionResult Index()
        {

            ViewBag.listdoandulich = doanbiz.Getlist();
            ViewBag.tour = tourbiz.Selectwithgiave();
            return View();
        }
        [HttpPost]
        public ActionResult CreateDoandulich(FormCollection formdoandulich)
        {
            string thongbao = string.Empty;

            if (Request.Form["update"] != null)
            {
                
                if (formdoandulich["name"] == "")
                {
                    thongbao = " Bạn chưa nhập tên đoàn !";
                    goto back;
                }
                string name = (formdoandulich["name"]).ToString();
                if (formdoandulich["start_at"] == "")
                {
                    thongbao = " Bạn chưa chọn ngày khởi hành !";
                    goto back;
                }
                DateTime ngaykhoihanh = DateTime.Parse(formdoandulich["start_at"]);
                if (formdoandulich["finish_at"] == "")
                {
                    thongbao = " Bạn chưa chọn ngày kết thúc !";
                    goto back;
                }
                DateTime ngayketthuc = DateTime.Parse(formdoandulich["finish_at"]);
                if (ngaykhoihanh > ngayketthuc)
                {
                    thongbao = " Ngày kết thúc phải lớn hơn ngày bắt đầu !";
                    goto back;
                }
                //if (formdoandulich["money"] == "")
                //{
                //    thongbao = " Bạn chưa nhập số tiền vé !";
                //    goto back;
                //}
                //double tienve = double.Parse(formdoandulich["money"]);
                
                if (formdoandulich["id"]=="")
                {
                    int idtour = int.Parse(formdoandulich["tour"]);
                    double tienve = tourbiz.TienTour(idtour);
                    if (doanbiz.Addnew(idtour,name,ngaykhoihanh, ngayketthuc,tienve))
                    {
                        thongbao = " Thêm thành công ! ";
                    }
                    else
                    {
                        thongbao = "Thêm thất bại vui lòng kiểm tra lại ! ";
                    }
                    
                    goto back;
                }
                else
                {
                    int id = int.Parse(formdoandulich["id"]);
                    int idtour = 0;
                    if (formdoandulich["tour"] != null)
                    {
                        idtour = int.Parse(formdoandulich["tour"]);
                    }
                    else
                        idtour = doanbiz.find(id).idtour;
                    
                    double tienve = tourbiz.TienTour(idtour);
                    if (doanbiz.UpdateBasic(id,idtour, name, ngaykhoihanh, ngayketthuc, tienve))
                    {
                        thongbao = "Cập nhật thành công ! ";
                    }
                    else
                    {
                        thongbao = "Cập nhật thất bại vui lòng kiểm tra lại ! ";
                    }
                    goto back;
                }
                
            }
            back:
            Session["thongbao"] = thongbao;
            return RedirectToAction("index");
        }
        public ActionResult ChitietDoandulich(int? id)
        {
            Session.Timeout = 120;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.khachsan = (new KhachsanBIZ()).Select();
            ViewBag.buaan = (new BuaAnBIZ()).Select();
            ViewBag.phuongtien =( new PhuongtienBIZ()).Select();
            ViewBag.nhanvien = (new NhanVienBIZ()).Select();
            ViewBag.nhiemvu =( new NhiemVuBIZ()).Select();
            Session["doandulich"] = doanbiz.find((int)id);
            var doankhachsan = doanksbiz.findByDoan((int)id);
            if (Session["doankhachsan"] == null )
            {
                if(doankhachsan.Count != 0)
                {
                    Session["doankhachsan"] = doankhachsan;
                }
                else
                    Session["doankhachsan"] = new List<doankhachsan>();

            }
            else if (Session["doankhachsan"] != null)
            {
                if ( ((List<doankhachsan>)Session["doankhachsan"]).Count > 0 && ( (List<doankhachsan>)Session["doankhachsan"])[0].iddoan!=id )
                    Session["doankhachsan"] = new List<doankhachsan>();
            } 
            var doanpt = doanptbiz.findByDoan((int)id);
            if (Session["doanphuongtien"] == null )
            {
                if (doanpt.Count != 0)
                {
                    Session["doanphuongtien"] = doanpt;
                }
                else
                    Session["doanphuongtien"] = new List<doanphuongtien>();
            }
            else if (Session["doanphuongtien"] != null)
            {
                if(((List<doanphuongtien>)Session["doanphuongtien"]).Count >0 && ((List<doanphuongtien>)Session["doanphuongtien"])[0].iddoan != id)
                Session["doanphuongtien"] = new List<doanphuongtien>();
            }
            var doanbuaan = doanbabiz.findByDoan((int)id);
            if (Session["doanbuaan"] == null )
            {
                if(doanbuaan.Count != 0)
                {
                    Session["doanbuaan"] = doanbuaan;
                }
                else
                    Session["doanbuaan"] = new List<doanbuaan>();
            }
            else if (Session["doanbuaan"] != null )
            {
                if(((List<doanbuaan>)Session["doanbuaan"]).Count >0 && ((List<doanbuaan>)Session["doanbuaan"])[0].iddoan != id)
                Session["doanbuaan"] = new List<doanbuaan>();
            }
            var doanchiphikhac = doanchiphikhacbiz.findByDoan((int)id);
            if (Session["doanchiphikhac"] == null )
            {
                if(doanchiphikhac.Count != 0)
                {
                    Session["doanchiphikhac"] = doanchiphikhac;
                }
                else
                {
                    Session["doanchiphikhac"] = new List<doanphikhac>();
                }
            }
            else if (Session["doanchiphikhac"] != null )
            {
                if(((List<doanphikhac>)Session["doanchiphikhac"]).Count >0 && ((List<doanphikhac>)Session["doanchiphikhac"])[0].iddoan != id)
                Session["doanchiphikhac"] = new List<doanphikhac>();
            }
            var doannv = doannvbiz.findByDoan((int)id);
            if (Session["doannhanvien"] == null )
            {
                if (doannv.Count != 0)
                {
                    Session["doannhanvien"] = doannv;
                }
                else
                    Session["doannhanvien"] = new List<doannhanvien>();
            }
            else if (Session["doannhanvien"] != null )
            {
                if(((List<doannhanvien>)Session["doannhanvien"]).Count>0 && ((List<doannhanvien>)Session["doannhanvien"])[0].iddoan != id)
                Session["doannhanvien"] = new List<doannhanvien>();
            }
               

            if (Session["flagkhachsan"] == null) Session["flagkhachsan"] = false;
            if (Session["flagbuaan"] == null) Session["flagbuaan"] = false;
            if (Session["flagphuongtien"] == null) Session["flagphuongtien"] = false;
            if (Session["flagnhanvien"] == null) Session["flagnhanvien"] = false;
            if (Session["flagphikhac"] == null) Session["flagphikhac"] = false;
            if (Session["flaghanhkhach"] == null) Session["flaghanhkhach"] = false;
            return View();
        }
        [HttpPost]
        public ActionResult Update(FormCollection formupdatedoan)
        {
            doandulich doandulich = (doandulich)Session["doandulich"];
            List<doankhachsan> doanks = (List<doankhachsan>)Session["doankhachsan"];
            List<doanphuongtien> doanpt = (List<doanphuongtien>)Session["doanphuongtien"];
            List<doanbuaan> doanba = (List<doanbuaan>)Session["doanbuaan"];
            List<doanphikhac> doanchiphikhac = (List<doanphikhac>)Session["doanchiphikhac"];
            List<doannhanvien> doannv = (List<doannhanvien>)Session["doannhanvien"];
            string loi = string.Empty;
            if(doandulich != null)
            {
                //------------------------------------------------------------------------------------
                if (Request.Form["luukhachsan"] != null)
                {
                    Session["flagkhachsan"] = true;
                    int idkhachsantemp = int.Parse(formupdatedoan["khachsan"]);
                    double sotien;
                    DateTime ngaykhachsantemp;
                    if (idkhachsantemp == -1)
                    {
                        loi = "Bạn chưa chọn khách sạn !";
                        goto Back;
                    }
                    if (formupdatedoan["khachsanmoney"] == "")
                    {
                        loi = "Bạn chưa nhập số tiền ở khách sạn !";
                        goto Back;
                    }
                    sotien = double.Parse(formupdatedoan["khachsanmoney"]);
                    if (formupdatedoan["datearrive"] == "")
                    {
                        loi = "Bạn chưa chọn ngày !";
                        goto Back;
                    }
                    ngaykhachsantemp = DateTime.Parse(formupdatedoan["datearrive"]);
                    if (ngaykhachsantemp < doandulich.ngaykhoihanh || ngaykhachsantemp > doandulich.ngayketthuc)
                    {
                        loi = "Ngày tới khách sạn phải từ " + doandulich.ngaykhoihanh + " -> " + doandulich.ngayketthuc;
                        goto Back;
                    }
                    if (doanks.Where(c => c.idkhachsan == idkhachsantemp).FirstOrDefault() != null)
                    {
                        loi = "Khách sạn này đã có sẵn!";
                        goto Back;
                    }

                    doankhachsan doankstemp = new doankhachsan();
                    doankstemp.idkhachsan = idkhachsantemp;
                    doankstemp.iddoan = doandulich.id;
                    doankstemp.sotien = sotien;
                    doankstemp.ngayden = ngaykhachsantemp;
                    doankstemp.khachsan = (new KhachsanBIZ()).find(idkhachsantemp);
                    doanks.Add(doankstemp);
                    Session["doankhachsan"] = doanks;
                    goto Back;
                }
                if (Request.Form["luuphuongtien"] != null)
                {
                    Session["flagphuongtien"] = true;
                    int idphuongtientemp = int.Parse(formupdatedoan["phuongtien"]);
                    if (idphuongtientemp == -1)
                    {
                        loi = "Bạn chưa chọn  phương tiện!";
                        goto Back;
                    }
                    if (formupdatedoan["phuongtienmoney"] == "")
                    {
                        loi = "Bạn chưa nhập số tiền cho phương tiện !";
                        goto Back;
                    }
                    double sotien = double.Parse(formupdatedoan["phuongtienmoney"]);
                    if (formupdatedoan["dateusedayuse"] == "")
                    {
                        loi = "Bạn chưa chọn ngày sử dụng phương tiện !";
                        goto Back;
                    }
                    DateTime ngayphuongtientemp = DateTime.Parse(formupdatedoan["dateusedayuse"]);
                    if (ngayphuongtientemp < doandulich.ngaykhoihanh || ngayphuongtientemp > doandulich.ngayketthuc)
                    {
                        loi = "Ngày sử dụng phương tiện phải từ " + doandulich.ngaykhoihanh + " -> " + doandulich.ngayketthuc;
                        goto Back;
                    }
                    if (doanpt.Where(c => c.idphuongtien == idphuongtientemp && c.ngay == ngayphuongtientemp).FirstOrDefault() != null)
                    {
                        loi = "Phương tiện và giờ này đã có sẵn!";
                        goto Back;
                    }

                    doanphuongtien doanpttemp = new doanphuongtien();
                    doanpttemp.iddoan = doandulich.id;
                    doanpttemp.ngay = ngayphuongtientemp;
                    doanpttemp.sotien = sotien;
                    doanpttemp.idphuongtien = idphuongtientemp;
                    doanpttemp.phuongtien = (new PhuongtienBIZ()).find(idphuongtientemp);
                    doanpt.Add(doanpttemp);
                    Session["doanphuongtien"] = doanpt;
                    goto Back;
                }
                if (Request.Form["luubuaan"] != null)
                {
                    Session["flagbuaan"] = true;
                    int idbuaantemp = int.Parse(formupdatedoan["buaan"]);
                    if (idbuaantemp == -1)
                    {
                        loi = "Bạn chưa chọn bữa ăn!";
                        goto Back;
                    }
                    if (formupdatedoan["bamoney"] == "")
                    {
                        loi = "Bạn chưa nhập số tiền cho bữa ăn!";
                        goto Back;
                    }
                    double sotien = double.Parse(formupdatedoan["bamoney"]);
                    if (formupdatedoan["ngayeat"] == "")
                    {
                        loi = "Bạn chưa chọn ngày ăn!";
                        goto Back;
                    }
                    DateTime ngayantemp = DateTime.Parse(formupdatedoan["ngayeat"]);
                    if (ngayantemp < doandulich.ngaykhoihanh || ngayantemp > doandulich.ngayketthuc)
                    {
                        loi = "Ngày ăn phải từ " + doandulich.ngaykhoihanh + " -> " + doandulich.ngayketthuc;
                        goto Back;
                    }
                    if (doanpt.Where(c => c.idphuongtien == idbuaantemp && c.ngay == ngayantemp).FirstOrDefault() != null)
                    {
                        loi = "Bữa ăn và ngày ăn này đã có sẵn!";
                        goto Back;
                    }
                    doanbuaan doanbantemp = new doanbuaan();
                    doanbantemp.iddoan = doandulich.id;
                    doanbantemp.idbuaan = idbuaantemp;
                    doanbantemp.ngay = ngayantemp;
                    doanbantemp.sotien = sotien;
                    doanbantemp.chiphibuaan = (new BuaAnBIZ()).find(idbuaantemp);
                    doanba.Add(doanbantemp);
                    Session["doanbuaan"] = doanba;
                    goto Back;
                }
                if (Request.Form["luuchiphikhac"] != null)
                {
                    Session["flagphikhac"] = true;
                    if (formupdatedoan["phikhac"] == "")
                    {
                        loi = "Bạn chưa nhập phí khác!";
                        goto Back;
                    }
                    string namechiphi = formupdatedoan["phikhac"];
                    if (formupdatedoan["phikhacmoney"] == "")
                    {
                        loi = "Bạn chưa nhập số tiền cho phí khác!";
                        goto Back;
                    }
                    double sotien = double.Parse(formupdatedoan["phikhacmoney"]);
                    if (formupdatedoan["datephikhac"] == "")
                    {
                        loi = "Bạn chưa chọn ngày sử dụng phí khác!";
                        goto Back;
                    }
                    DateTime ngayphikhac = DateTime.Parse(formupdatedoan["datephikhac"]);
                    if (ngayphikhac < doandulich.ngaykhoihanh || ngayphikhac > doandulich.ngayketthuc)
                    {
                        loi = "Ngày sử dụng phí khác phải từ " + doandulich.ngaykhoihanh + " -> " + doandulich.ngayketthuc;
                        goto Back;
                    }
                    if (doanchiphikhac.Where(c => c.name == namechiphi && c.ngay == ngayphikhac).FirstOrDefault() != null)
                    {
                        loi = "Phí  này và ngày đã có sẵn!";
                        goto Back;
                    }
                    doanphikhac doanphikhac = new doanphikhac();
                    doanphikhac.iddoan = doandulich.id;
                    doanphikhac.name = namechiphi;
                    doanphikhac.ngay = ngayphikhac;
                    doanphikhac.sotien = sotien;
                    doanphikhac.note = formupdatedoan["note"];
                    doanchiphikhac.Add(doanphikhac);
                    Session["doanchiphikhac"] = doanchiphikhac;
                    goto Back;

                }
                if (Request.Form["luunhanvien"] != null)
                {
                    Session["flagnhanvien"] = true;
                    if (int.Parse(formupdatedoan["nhanvien"]) == -1)
                    {
                        loi = "Bạn chưa chọn nhân viên!";
                        goto Back;
                    }
                    int idnhanvientemp = int.Parse(formupdatedoan["nhanvien"]);
                    if (int.Parse(formupdatedoan["nhiemvu"]) == -1)
                    {
                        loi = "Bạn chưa chọn nhiệm vụ cho nhân viên!";
                        goto Back;
                    }
                    int idnhiemvutemp = int.Parse(formupdatedoan["nhiemvu"]);
                    if (doannv.Where(c => c.idnhanvien == idnhanvientemp && c.idnhiemvu == idnhiemvutemp).FirstOrDefault() != null)
                    {
                        loi = " Nhân viên với nhiệm vụ này đã có tồn tại!";
                        goto Back;
                    }
                    doannhanvien doannvtemp = new doannhanvien();
                    doannvtemp.iddoan = doandulich.id;
                    doannvtemp.idnhanvien = idnhanvientemp;
                    doannvtemp.idnhiemvu = idnhiemvutemp;
                    doannvtemp.nhanvien = (new NhanVienBIZ()).find(idnhanvientemp);
                    doannvtemp.nhiemvu = (new NhiemVuBIZ()).find(idnhiemvutemp);
                    doannv.Add(doannvtemp);
                    Session["doannhanvien"] = doannv;
                    goto Back;
                }
                if (Request.Form["luu"] != null)
                {
                    double tongtienkhachsan = 0, tongtienan = 0, tongtienphuongtien = 0, tongtienchiphikhac = 0;
                    doandulich doantemp = doanbiz.find(doandulich.id);
                    if ((bool)Session["flagkhachsan"] == true)
                    {
                        doanksbiz.deleteByDoan(doandulich.id);
                        foreach (var item in doanks)
                        {
                            doankhachsan ks = new doankhachsan();
                            ks.iddoan = item.iddoan;
                            ks.ngayden = item.ngayden;
                            ks.sotien = item.sotien;
                            ks.idkhachsan = item.idkhachsan;
                            doanksbiz.Add(ks);
                            tongtienkhachsan += item.sotien;
                        }
                        doantemp.tongtienkhachsan = tongtienkhachsan;
                    }
                    if ((bool)Session["flagphuongtien"] == true)
                    {
                        doanptbiz.deleteByDoan(doandulich.id);
                        foreach (var item in doanpt)
                        {
                            doanphuongtien pt = new doanphuongtien();
                            pt.idphuongtien = item.idphuongtien;
                            pt.iddoan = item.iddoan;
                            pt.sotien = item.sotien;
                            pt.ngay = item.ngay;
                            doanptbiz.Add(pt);
                            tongtienphuongtien += item.sotien;
                        }
                        doantemp.tongtienphuongtien = tongtienphuongtien;

                    }
                    if ((bool)Session["flagbuaan"] == true)
                    {
                        doanbabiz.deleteByDoan(doandulich.id);
                        foreach (var item in doanba)
                        {
                            doanbuaan ba = new doanbuaan();
                            ba.idbuaan = item.idbuaan;
                            ba.iddoan = item.iddoan;
                            ba.sotien = item.sotien;
                            ba.ngay = item.ngay;
                            doanbabiz.Add(ba);
                            tongtienan += item.sotien;
                        }
                        doantemp.tongtienan = tongtienan;
                    }
                    if ((bool)Session["flagphikhac"] == true)
                    {
                        doanchiphikhacbiz.deleteByDoan(doandulich.id);
                        foreach (var item in doanchiphikhac)
                        {
                            doanphikhac phikhac = new doanphikhac();
                            phikhac.iddoan = item.iddoan;
                            phikhac.name = item.name;
                            phikhac.ngay = item.ngay;
                            phikhac.note = item.note;
                            phikhac.sotien = item.sotien;
                            doanchiphikhacbiz.Add(phikhac);
                            tongtienchiphikhac += item.sotien;
                        }
                        doantemp.tongtienchiphikhac = tongtienchiphikhac;
                    }
                    if ((bool)Session["flagnhanvien"] == true)
                    {
                        doannvbiz.deleteByDoan(doandulich.id);
                        foreach (var item in doannv)
                        {
                            doannhanvien nv = new doannhanvien();
                            nv.iddoan = item.iddoan;
                            nv.idnhanvien = item.idnhanvien;
                            nv.idnhiemvu = item.idnhiemvu;
                            doannvbiz.Add(nv);
                        }
                    }
                    if (doanbiz.Update(doantemp))
                        Session["thongbao"] = " Cập nhật thành công !";
                    else Session["thongbao"] = " Cập nhật thất bại !";

                }
                return RedirectToAction("index");

                Back:
                Session["loi"] = loi;
                return RedirectToAction("ChitietDoandulich", "Doandulich", new { @id = doandulich.id });
            }
            return Redirect(Request.UrlReferrer.ToString());


        }
        public ActionResult infordoandulich(int id)
        {
            return View();
        }
        public ActionResult deletechitiet(int id, string detail, int idct)
        {
            if (idct > 0)
            {
                if (detail.Equals("khachsan"))
                {
                    List<doankhachsan> doanks = (List<doankhachsan>)Session["doankhachsan"];
                    doanks.RemoveAt(idct - 1);
                    Session["doankhachsan"] = doanks;
                    Session["flagkhachsan"] = true;
                    goto Back;
                }
                if (detail.Equals("phuongtien"))
                {
                    List<doanphuongtien> doanpt = (List<doanphuongtien>)Session["doanphuongtien"];
                    doanpt.RemoveAt(idct-1);
                    Session["doanphuongtien"] = doanpt;
                    Session["flagphuongtien"] = true;

                    goto Back;
                }
                if (detail.Equals("buaan"))
                {
                    List<doanbuaan> doanba = (List<doanbuaan>)Session["doanbuaan"];
                    doanba.RemoveAt(idct - 1);
                    Session["doanbuaan"] = doanba;
                    Session["flagbuaan"] = true;

                    goto Back;
                }
                if (detail.Equals("phikhac"))
                {
                    List<doanphikhac> doanchiphikhac = (List<doanphikhac>)Session["doanchiphikhac"];
                    doanchiphikhac.RemoveAt(idct - 1);
                    Session["doanchiphikhac"] = doanchiphikhac;
                    Session["flagphikhac"] = true;
                    goto Back;
                }
                if (detail.Equals("nhanvien"))
                {
                    List<doannhanvien> doannv = (List<doannhanvien>)Session["doannhanvien"];
                    doannv.RemoveAt(idct - 1);
                    Session["doannhanvien"] = doannv;
                    Session["flagnhanvien"] = true ;
                    goto Back;
                }
            }
            if (Session["flagkhachsan"] == null) Session["flagkhachsan"] = false;
            if (Session["flagbuaan"] == null) Session["flagbuaan"] = false;
            if (Session["flagphuongtien"] == null) Session["flagphuongtien"] = false;
            if (Session["flagnhanvien"] == null) Session["flagnhanvien"] = false;
            if (Session["flagphikhac"] == null) Session["flagphikhac"] = false;
            if (Session["flaghanhkhach"] == null) Session["flaghanhkhach"] = false;
            if (Session["loi"] == null) Session["loi"] = string.Empty;
            Back:
            return RedirectToAction("ChitietDoandulich", "Doandulich", new { @id = id });
        }
        public ActionResult Createkhach()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Createkhach(FormCollection formcreatedkhach)
        {
            return RedirectToAction("index");
        }

        public ActionResult delete(int id)
        {
            Session["thongbao"] = "";
            if ( doanksbiz.findByDoan(id).Count() > 0 || doanptbiz.findByDoan(id).Count() > 0 || doanbabiz.findByDoan(id).Count() > 0 || doankhach.findByDoan(id).Count() > 0 || doannvbiz.findByDoan(id).Count() >0)
            {
                Session["thongbao"] = "Đã có dữ liệu liên quan về (Hành khách , khách sạn ,phương tiện ,nhân viên ....) , không thể xoá . Vui lòng kiểm tra lại !";
            }
            else
            {
                if(doanbiz.delete(id))
                    Session["thongbao"] = "Xoá thành công ! ";
                else
                    Session["thongbao"] = "Xoá thất bại . Vui lòng kiểm tra lại ! ";
            }
            return RedirectToAction("index");
        }

    }
}