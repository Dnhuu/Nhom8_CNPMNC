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
    public class KhoaHocsController : Controller
    {
        private KhocHocGiangVienEntities db = new KhocHocGiangVienEntities();

        // GET: Admin/KhoaHocs
        public ActionResult Index()
        {
            var khoaHocs = db.KhoaHocs.Include(k => k.GiangVien).Include(k => k.SinhVien);
            return View(khoaHocs.ToList());
        }

        // GET: Admin/KhoaHocs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhoaHoc khoaHoc = db.KhoaHocs.Find(id);
            if (khoaHoc == null)
            {
                return HttpNotFound();
            }
            return View(khoaHoc);
        }

        // GET: Admin/KhoaHocs/Create
        public ActionResult Create()
        {
            ViewBag.MaGV = new SelectList(db.GiangViens, "MaGV", "TenGV");
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "Hoten");
            return View();
        }

        // POST: Admin/KhoaHocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKhoaHoc,TenKhoaHoc,SoluongSV,MaGV,MaSV,NgayBatDau,SoTiet")] KhoaHoc khoaHoc)
        {
            if (ModelState.IsValid)
            {
                db.KhoaHocs.Add(khoaHoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaGV = new SelectList(db.GiangViens, "MaGV", "TenGV", khoaHoc.MaGV);
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "Hoten", khoaHoc.MaSV);
            return View(khoaHoc);
        }

        // GET: Admin/KhoaHocs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhoaHoc khoaHoc = db.KhoaHocs.Find(id);
            if (khoaHoc == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaGV = new SelectList(db.GiangViens, "MaGV", "TenGV", khoaHoc.MaGV);
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "Hoten", khoaHoc.MaSV);
            return View(khoaHoc);
        }

        // POST: Admin/KhoaHocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKhoaHoc,TenKhoaHoc,SoluongSV,MaGV,MaSV,NgayBatDau,SoTiet")] KhoaHoc khoaHoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khoaHoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaGV = new SelectList(db.GiangViens, "MaGV", "TenGV", khoaHoc.MaGV);
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "Hoten", khoaHoc.MaSV);
            return View(khoaHoc);
        }

        // GET: Admin/KhoaHocs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhoaHoc khoaHoc = db.KhoaHocs.Find(id);
            if (khoaHoc == null)
            {
                return HttpNotFound();
            }
            return View(khoaHoc);
        }

        // POST: Admin/KhoaHocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhoaHoc khoaHoc = db.KhoaHocs.Find(id);
            db.KhoaHocs.Remove(khoaHoc);
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
