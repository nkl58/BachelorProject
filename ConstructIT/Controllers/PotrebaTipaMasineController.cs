﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConstructIT.DAL;
using ConstructIT.DAL.Models;

namespace ConstructIT.Controllers
{
    public class PotrebaTipaMasineController : Controller
    {
        private ConstructITDBContext db = new ConstructITDBContext();

        // GET: PotrebaTipaMasine
        public async Task<ActionResult> Index()
        {
            var potrebeTipovaMasina = db.PotrebeTipovaMasina.Include(p => p.TipMasine).Include(p => p.Zadatak);
            return View(await potrebeTipovaMasina.ToListAsync());
        }

        // GET: PotrebaTipaMasine/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PotrebaTipaMasine potrebaTipaMasine = await db.PotrebeTipovaMasina.FindAsync(id);
            if (potrebaTipaMasine == null)
            {
                return HttpNotFound();
            }
            return View(potrebaTipaMasine);
        }

        // GET: PotrebaTipaMasine/Create
        public ActionResult Create()
        {
            ViewBag.TipMasineID = new SelectList(db.TipoviMasina, "TipMasineID", "TipMasineNaziv");
            ViewBag.ProjekatID = new SelectList(db.Zadaci, "ProjekatID", "ZadatakNaziv");
            return View();
        }

        // POST: PotrebaTipaMasine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PotrebaTipaMasineID,PotrTipaMasOdDatuma,PotrTipaMasDoDatuma,PotrTipaMasKolicina,ProjekatID,ZadatakID,TipMasineID")] PotrebaTipaMasine potrebaTipaMasine)
        {
            if (ModelState.IsValid)
            {
                db.PotrebeTipovaMasina.Add(potrebaTipaMasine);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TipMasineID = new SelectList(db.TipoviMasina, "TipMasineID", "TipMasineNaziv", potrebaTipaMasine.TipMasineID);
            ViewBag.ProjekatID = new SelectList(db.Zadaci, "ProjekatID", "ZadatakNaziv", potrebaTipaMasine.ProjekatID);
            return View(potrebaTipaMasine);
        }

        // GET: PotrebaTipaMasine/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PotrebaTipaMasine potrebaTipaMasine = await db.PotrebeTipovaMasina.FindAsync(id);
            if (potrebaTipaMasine == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipMasineID = new SelectList(db.TipoviMasina, "TipMasineID", "TipMasineNaziv", potrebaTipaMasine.TipMasineID);
            ViewBag.ProjekatID = new SelectList(db.Zadaci, "ProjekatID", "ZadatakNaziv", potrebaTipaMasine.ProjekatID);
            return View(potrebaTipaMasine);
        }

        // POST: PotrebaTipaMasine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PotrebaTipaMasineID,PotrTipaMasOdDatuma,PotrTipaMasDoDatuma,PotrTipaMasKolicina,ProjekatID,ZadatakID,TipMasineID")] PotrebaTipaMasine potrebaTipaMasine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(potrebaTipaMasine).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TipMasineID = new SelectList(db.TipoviMasina, "TipMasineID", "TipMasineNaziv", potrebaTipaMasine.TipMasineID);
            ViewBag.ProjekatID = new SelectList(db.Zadaci, "ProjekatID", "ZadatakNaziv", potrebaTipaMasine.ProjekatID);
            return View(potrebaTipaMasine);
        }

        // GET: PotrebaTipaMasine/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PotrebaTipaMasine potrebaTipaMasine = await db.PotrebeTipovaMasina.FindAsync(id);
            if (potrebaTipaMasine == null)
            {
                return HttpNotFound();
            }
            return View(potrebaTipaMasine);
        }

        // POST: PotrebaTipaMasine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PotrebaTipaMasine potrebaTipaMasine = await db.PotrebeTipovaMasina.FindAsync(id);
            db.PotrebeTipovaMasina.Remove(potrebaTipaMasine);
            await db.SaveChangesAsync();
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
