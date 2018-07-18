using ICOnBoardingV3DB;
using ICOnBoardingV3Knockout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICOnBoardingV3Knockout.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        OnBoardKeysEntities db = new OnBoardKeysEntities();

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadAllProducts()
        {
            var productList = db.Products.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            }).ToList();
            return Json(productList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveToDatabase(ProductsViewModel model, int deleteFlag)
        {
            if (ModelState.IsValid)
            {
                var product = db.Products.FirstOrDefault(x => x.Id == model.Id);
                if (deleteFlag == 1)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    if (model.Id == 0)
                    {
                        var newproduct = new Product
                        {
                            Name = model.Name.Trim(),
                            Price = model.Price
                        };
                        try
                        {
                            db.Products.Add(newproduct);
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
                            product.Name = model.Name;
                            product.Price = model.Price;
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            return Json( new { success = false });
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