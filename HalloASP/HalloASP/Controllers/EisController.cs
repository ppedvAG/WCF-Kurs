using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HalloASP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HalloASP.Controllers
{
    public class EisController : Controller
    {

        EisDataService data = new EisDataService();

        // GET: Eis
        public ActionResult Index()
        {
            return View(data.GetEisListe());
        }

        // GET: Eis/Details/5
        public ActionResult Details(int id)
        {
            return View(data.GetById(id));
        }

        // GET: Eis/Create
        public ActionResult Create()
        {
            return View(new Eis() { Name="NEU" });
        }

        // POST: Eis/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Eis eis)
        {
            try
            {

                data.AddEis(eis);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Eis/Edit/5
        public ActionResult Edit(int id)
        {
            return View(data.GetById(id));
        }

        // POST: Eis/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Eis eis)
        {
            try
            {
                data.UpdateEis(eis);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Eis/Delete/5
        public ActionResult Delete(int id)
        {
            return View(data.GetById(id));

        }

        // POST: Eis/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Eis eis)
        {
            try
            {
                // TODO: Add delete logic here
                data.DeleteEis(eis);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}