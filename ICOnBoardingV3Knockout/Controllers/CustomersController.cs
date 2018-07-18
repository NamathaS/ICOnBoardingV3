using ICOnBoardingV3DB;
using ICOnBoardingV3Knockout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICOnBoardingV3Knockout.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customer
        KeysOnBoardingEntities db = new KeysOnBoardingEntities();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadAllCustomers()
        {
            var customerList = db.Customers.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            }).ToList();
            return Json(customerList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveToDatabase(CustomerViewModel model, int deleteFlag)
        {
            if (ModelState.IsValid)
            {
                var customer = db.Customers.FirstOrDefault(x => x.Id == model.Id);
                if (deleteFlag == 1)
                {
                    db.Customers.Remove(customer);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    if (model.Id == 0)
                    {
                        var newcustomer = new Customer
                        {
                            Name = model.Name.Trim(),
                            Address = model.Address.Trim()
                        };
                        try
                        {
                            db.Customers.Add(newcustomer);
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
                            customer.Name = model.Name.Trim();
                            customer.Address = model.Address.Trim();
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