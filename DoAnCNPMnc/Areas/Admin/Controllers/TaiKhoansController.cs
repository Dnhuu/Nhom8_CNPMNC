using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnCNPMnc.Models;

namespace DoAnCNPMnc.Areas.Admin.Controllers
{
    public class TaiKhoansController : Controller
    {
        private KhocHocGiangVienEntities db = new KhocHocGiangVienEntities();

        // GET: Admin/TaiKhoans
        public ActionResult Index()
        {
            var taiKhoans = db.TaiKhoans.Include(t => t.GiangVien).Include(t => t.PhanQuyen).Include(t => t.SinhVien);
            return View(taiKhoans.ToList());
        }

        // GET: Admin/TaiKhoans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Create
        public ActionResult Create()
        {
            ViewBag.MaGV = new SelectList(db.GiangViens, "MaGV", "TenGV");
            ViewBag.MaPB = new SelectList(db.PhanQuyens, "MaPQ", "TenQuyen");
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "Hoten");
            return View();
        }

        // POST: Admin/TaiKhoans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTaiKhoan,MaGV,MaSV,MaPQS,TenTaiKhoan,UserName,Matkhau,NgayTao")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaGV = new SelectList(db.GiangViens, "MaGV", "TenGV", taiKhoan.MaGV);
            ViewBag.MaPB = new SelectList(db.PhanQuyens, "MaPQ", "TenQuyen", taiKhoan.MaPQ);
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "Hoten", taiKhoan.MaSV);
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaGV = new SelectList(db.GiangViens, "MaGV", "TenGV", taiKhoan.MaGV);
            ViewBag.MaPB = new SelectList(db.PhanQuyens, "MaPQ", "TenQuyen", taiKhoan.MaPQ);
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "Hoten", taiKhoan.MaSV);
            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTaiKhoan,MaGV,MaSV,MaPQ,TenTaiKhoan,UserName,Matkhau,NgayTao")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaGV = new SelectList(db.GiangViens, "MaGV", "TenGV", taiKhoan.MaGV);
            ViewBag.MaPB = new SelectList(db.PhanQuyens, "MaPQ", "TenQuyen", taiKhoan.MaPQ);
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "Hoten", taiKhoan.MaSV);
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            db.TaiKhoans.Remove(taiKhoan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
