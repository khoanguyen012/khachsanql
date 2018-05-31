using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MBT.Models;
using PagedList;
using PagedList.Mvc;

namespace MBT.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        dbQLBansachDataContext data = new dbQLBansachDataContext();
        private List<SACH> Laysachmoi(int count)
        {
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        public ActionResult Index()
        {
            var sachmoi = Laysachmoi(5);
            return View(sachmoi);
        }
        public ActionResult Sach(int ?page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            //var sachmoi = Laysachmoi(5);
            //return View(sachmoi);
            //return View(data.SACHes.ToList());
            return View(data.SACHes.ToList().OrderBy(n => n.Masach).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Themmoisach()
        {
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View();
        }
        public ActionResult Login()
        {
            var sachmoi = Laysachmoi(5);
            return View(sachmoi);
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if(String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if(String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                // Gán giá trị cho đối tượng tạo mới
                Admin ad = data.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                {
                    if(ad!=null)
                    {
                        Session["Taikhoanadmin"] = ad;
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                    }
                }
                   
            }

            return View();
        }
    }
}