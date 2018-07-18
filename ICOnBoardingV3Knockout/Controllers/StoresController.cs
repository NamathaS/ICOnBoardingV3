using ICOnBoardingV3DB;
using ICOnBoardingV3Knockout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICOnBoardingV3Knockout.Controllers
{
    public class StoresController : Controller
    {
        KeysOnBoardingEntities db = new KeysOnBoardingEntities();
        // GET: Stores
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadAllStores()
        {
            var storeList = db.Stores.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            }).ToList();
            return Json(storeList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveToDatabase(StoreViewModel model, int deleteFlag)
        {
            if (ModelState.IsValid)
            {
                var store = db.Stores.FirstOrDefault(x => x.Id == model.Id);
                if (deleteFlag == 1)
                {                  
                    db.Stores.Remove(store);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    if (model.Id == 0)
                    {
                        var newstore = new Store {
                            Name = model.Name.Trim(),
                            Address = model.Address.Trim()
                        };
                        try
                        {
                            db.Stores.Add(newstore);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            return Json(new { success = false });
                        }

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        try
                        {
                            store.Name = model.Name.Trim();
                            store.Address = model.Address.Trim();
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            return Json(new { success = false });
                        }
                        return RedirectToAction("Index");
                    }
                }
            }
            else
                return Json(new { success = false });
        }
    }
}