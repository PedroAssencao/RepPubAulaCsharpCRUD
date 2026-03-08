using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaAcademico.API.Models;

namespace SistemaAcademico.API.Controllers
{
    public class BoletosController : Controller
    {
        private DbSistemaAcademicoEntities db = new DbSistemaAcademicoEntities();

        // GET: Boletos
        public async Task<ActionResult> Index()
        {
            var boleto = db.Boleto.Include(b => b.Matricula);
            return View(await boleto.ToListAsync());
        }

        // GET: Boletos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boleto boleto = await db.Boleto.FindAsync(id);
            if (boleto == null)
            {
                return HttpNotFound();
            }
            return View(boleto);
        }

        // GET: Boletos/Create
        public ActionResult Create()
        {
            ViewBag.IdMatricula = new SelectList(db.Matricula, "IdMatricula", "Status");
            return View();
        }

        // POST: Boletos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdBoleto,IdMatricula,Valor,DataVencimento,Status")] Boleto boleto)
        {
            if (ModelState.IsValid)
            {
                db.Boleto.Add(boleto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdMatricula = new SelectList(db.Matricula, "IdMatricula", "Status", boleto.IdMatricula);
            return View(boleto);
        }

        // GET: Boletos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boleto boleto = await db.Boleto.FindAsync(id);
            if (boleto == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMatricula = new SelectList(db.Matricula, "IdMatricula", "Status", boleto.IdMatricula);
            return View(boleto);
        }

        // POST: Boletos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdBoleto,IdMatricula,Valor,DataVencimento,Status")] Boleto boleto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boleto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdMatricula = new SelectList(db.Matricula, "IdMatricula", "Status", boleto.IdMatricula);
            return View(boleto);
        }

        // GET: Boletos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boleto boleto = await db.Boleto.FindAsync(id);
            if (boleto == null)
            {
                return HttpNotFound();
            }
            return View(boleto);
        }

        // POST: Boletos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Boleto boleto = await db.Boleto.FindAsync(id);
            db.Boleto.Remove(boleto);
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
