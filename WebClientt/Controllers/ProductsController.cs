using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebClientt.Models;
using WebClientt.ServiceProduct;

namespace WebClientt.Controllers
{
    public class ProductsController : Controller
    {
        ProductsModel pmodel = new ProductsModel();
        ServiceProductClient service = new ServiceProductClient();
        // GET: Products

        [UserAuthentication] //login olmadan göremesin diye
        public ActionResult Products()
        {
            pmodel.ulist = service.GetProducts(Convert.ToInt32(Session["Supplier"]),
                Session["Role"].ToString()).ToList();
            return View(pmodel);
        }
        
        public ActionResult Guncelle(int id,decimal fiyat,string name)
        {
           service.UpdatePriceANDName(id,fiyat,name);
            return View();
        }
        public ActionResult Sil(int id)
        {
            service.DeleteProducts(id);
            return View();
        }
        public ActionResult Ara(int id)
        {
            service.FindProducts(id);
            return View();
        }
    }
}