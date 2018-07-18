using ICOnBoardingV3DB;
using ICOnBoardingV3Knockout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICOnBoardingV3Knockout.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        OnBoardKeysEntities db = new OnBoardKeysEntities();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadAllSales()
        {
            var saleList = db.ProductSolds.Select(x => new SalesViewModel
            {
                Id = x.Id,
                SelectedProduct = new ProductsViewModel { Id = x.Product.Id, Name = x.Product.Name, Price = x.Product.Price },
                SelectedCustomer = new CustomerViewModel { Id = x.Customer.Id, Name = x.Customer.Name, Address = x.Customer.Address },
                SelectedStore = new StoreViewModel { Id = x.Store.Id, Name = x.Store.Name, Address = x.Store.Address },
                DateSold = x.DateSold
            }).ToList();
            return Json(saleList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveToDatabase(SalesViewModel model, int deleteFlag)
        {
            if (ModelState.IsValid)
            {
                var sale = db.ProductSolds.FirstOrDefault(x => x.Id == model.Id);
                if (deleteFlag == 1)
                {
                    db.ProductSolds.Remove(sale);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    if (model.Id == 0)
                    {
                        var newSale = new ProductSold
                        {
                            CustomerId = model.CustomerId,
                            ProductId = model.ProductId,
                            StoreId = model.StoreId,
                            DateSold = model.DateSold
                        };
                        try
                        {
                            db.ProductSolds.Add(newSale);
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
                            sale.CustomerId = model.CustomerId;
                            sale.ProductId = model.ProductId;
                            sale.StoreId = model.StoreId;
                            sale.DateSold = model.DateSold;
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