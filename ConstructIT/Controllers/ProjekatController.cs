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
    public class ProjekatController : Controller
    {
        private ConstructITDBContext db = new ConstructITDBContext();

        // GET: Projekat
        public async Task<ActionResult> Index()
        {
            return View(await db.Projekti.ToListAsync());
        }

        // GET: Projekat/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projekat projekat = await db.Projekti.Include(p => p.Zadaci).Where(p => p.ProjekatID == id).FirstOrDefaultAsync();
            if (projekat == null)
            {
                return HttpNotFound();
            }
            return View(projekat);
        }

        // GET: Projekat/Create
        public ActionResult Create()
        {
            ViewData["korisnici"] = db.Korisnici.Where(k => (k.KorisnikTip == "Klijent" && k.KlijentovProjekatID == null) || k.KorisnikTip == "Tehn. Osoblje").ToList();
            return View();
        }

        // POST: Projekat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProjekatID,ProjekatNaziv,ProjekatOpis,ProjekatAdresa,OdabraniKorisnici")] Projekat projekat)
        {

            Projekat p = db.Projekti.Where(pr => pr.ProjekatNaziv == projekat.ProjekatNaziv).FirstOrDefault();

            if(p != null)
            {
                ModelState.AddModelError("ProjekatNaziv", "Već postoji projekat sa unetim nazivom!");
            }

            if (ModelState.IsValid)
            {
                projekat.Korisnici = new List<Korisnik>();
                projekat.Klijenti = new LinkedList<Korisnik>();

                foreach (int korisnikID in projekat.OdabraniKorisnici)
                {
                    projekat.Korisnici.Add(db.Korisnici.Where(k => k.KorisnikID == korisnikID && k.KorisnikTip == "Tehn. Osoblje").FirstOrDefault());

                    projekat.Klijenti.Add(db.Korisnici.Where(k => k.KorisnikID == korisnikID && k.KorisnikTip == "Klijent").FirstOrDefault());
                }

                db.Projekti.Add(projekat);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(projekat);
        }

        // GET: Projekat/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projekat projekat = await db.Projekti.Where(p => p.ProjekatID == id).Include(p => p.Korisnici).Include(p => p.Klijenti).FirstOrDefaultAsync();
            if (projekat == null)
            {
                return HttpNotFound();
            }
            ViewData["korisnici"] = db.Korisnici.Where(k => (k.KorisnikTip == "Klijent" && k.KlijentovProjekatID == id ) || (k.KorisnikTip == "Klijent" && k.KlijentovProjekatID == null) || k.KorisnikTip == "Tehn. Osoblje").ToList();
            return View(projekat);
        }

        // POST: Projekat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProjekatID,ProjekatNaziv,ProjekatOpis,ProjekatAdresa,OdabraniKorisnici")] Projekat projekat)
        {
            Projekat p = db.Projekti.Where(pr => pr.ProjekatNaziv == projekat.ProjekatNaziv && pr.ProjekatID != projekat.ProjekatID).FirstOrDefault();

            if (p != null)
            {
                ModelState.AddModelError("ProjekatNaziv", "Već postoji projekat sa unetim nazivom!");
            }

            if (ModelState.IsValid)
            {
                db.Entry(projekat).State = EntityState.Modified;
                db.SaveChanges();

                Projekat dbProjekat = db.Projekti.Where(pr => pr.ProjekatID == projekat.ProjekatID).Include(pr => pr.Korisnici).Include(pr => pr.Klijenti).FirstOrDefault();
                
                if(dbProjekat.Korisnici == null)
                {
                    dbProjekat.Korisnici = new List<Korisnik>();
                }

                if(dbProjekat.Klijenti == null)
                {
                    dbProjekat.Klijenti = new List<Korisnik>();
                }

                dbProjekat.Korisnici.Clear();
                dbProjekat.Klijenti.Clear();

                foreach (int korisnikID in projekat.OdabraniKorisnici)
                {
                    dbProjekat.Korisnici.Add(db.Korisnici.Where(k => k.KorisnikID == korisnikID && k.KorisnikTip == "Tehn. Osoblje").FirstOrDefault());

                    dbProjekat.Klijenti.Add(db.Korisnici.Where(k => k.KorisnikID == korisnikID && k.KorisnikTip == "Klijent").FirstOrDefault());

                }

                await db.SaveChangesAsync();

                return RedirectToAction("Details", new { id = projekat.ProjekatID });
            }
            return View(projekat);
        }

        // GET: Projekat/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projekat projekat = await db.Projekti.FindAsync(id);
            if (projekat == null)
            {
                return HttpNotFound();
            }
            return View(projekat);
        }

        // POST: Projekat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Projekat projekat = await db.Projekti.FindAsync(id);
            db.Projekti.Remove(projekat);
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
