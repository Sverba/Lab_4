using DTPReg4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace DTPReg4.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        DTPContext db = new DTPContext();
        public ActionResult Index()
        {
            var regs = db.Registrations.Include(p => p.Type);
            return View(regs.ToList());
        }
        public ActionResult TypeDetails(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Models.Type type = db.Types.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            type.Registrations = db.Registrations.Where(m => m.TypeId == type.Id);
            return View(type);
        }
        public ActionResult TypeView(int? id)
        {
            IEnumerable<DTPReg4.Models.Type> types = db.Types;
            return View(types);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SelectList types = new SelectList(db.Types, "Id", "Name");
            ViewBag.Teams = types;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Registration reg)
        {
            // Додаємо порушника в таблицю 
            db.Registrations.Add(reg);
            db.SaveChanges();
            // перенаправляємо на головну сторінку 
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Registration reg = db.Registrations.Find(id);
            if (reg != null)
            {
                SelectList regs = new SelectList(db.Types, "Id", "Name", reg.TypeId);
                ViewBag.Teams = regs;
                return View(reg);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Registration reg)
        {
            db.Entry(reg).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Registration reg = db.Registrations.Find(id);
            if (reg == null)
            {
                return HttpNotFound();
            }
            return View(reg);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Registration reg = db.Registrations.Find(id);
            if (reg == null)
            {
                return HttpNotFound();
            }
            db.Registrations.Remove(reg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}