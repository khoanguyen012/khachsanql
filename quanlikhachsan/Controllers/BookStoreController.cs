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
    public class BookStoreController : Controller
    {
        dbQLBansachDataContext data = new dbQLBansachDataContext();
        
        // GET: BookStore
        private List<SACH> Laysachmoi(int count)
        {
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        public ActionResult Index(int ? page)
        {
            int pagesize = 5;
            int pagenum = (page ?? 1);
            var sachmoi = Laysachmoi(5);
            return View(sachmoi);
        }
        private List<CHUDE> Laychude(int count)
        {
            return data.CHUDEs.Take(count).ToList();
        }
        public ActionResult Chude()
        {
            var chude = from cd in data.CHUDEs select cd;
            //var chude = Laychude(2);
            return PartialView(chude);
        }
        private List<NHAXUATBAN> Laynhaxuatban(int count)
        {
            return data.NHAXUATBANs.Take(count).ToList();
        }
        public ActionResult Nhaxuatban()
        {
            var nxb = from n in data.NHAXUATBANs select n;
            //var nxb = Laynhaxuatban(2);
            return PartialView(nxb);
        }
    }
}