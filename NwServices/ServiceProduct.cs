using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Data;

namespace NwServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.  
    public class ServiceProduct : IServiceProduct
    {
        NorthwindEntities  db = new NorthwindEntities();

        public void DeleteProducts(int id)
        {
            Products p = db.Products.Find(id);
            p.Discontinued = true;
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void UpdatePriceANDName(int id, decimal fiyat, string name)
        {
            Products p = db.Products.Find(id);
            p.UnitPrice = fiyat;
            p.ProductName = name;
            db.SaveChanges();
        }
        public Urunler FindProducts(int id)
        {
            Urunler u = new Urunler();
            Products p = db.Products.Find(id);
            u.UrunId = p.ProductID;
            u.UrunAd = p.ProductName;
            u.KategoriAd = p.Categories.CategoryName;
            u.TedarikciAd = p.Suppliers.CompanyName;
            u.BirimFiyat =(decimal) p.UnitPrice;
            u.Satılamaz = p.Discontinued;
            return u;
        }
        public ICollection<Urunler> GetProducts(int SuplierId, string Role)
        {
            if (Role == "Kamu")
            {
                return db.Products.Select(x => new Urunler
                {
                    UrunId = x.ProductID,
                    KategoriAd = x.Categories.CategoryName,
                    TedarikciAd = x.Suppliers.CompanyName,
                    UrunAd = x.ProductName,
                    BirimFiyat = (decimal)x.UnitPrice,
                    Satılamaz = x.Discontinued,
                    TedarikciId = (int)x.SupplierID
                }).Where(x => x.Satılamaz == false).ToList();
            }
            else
            {
                return db.Products.Select(x => new Urunler
                {
                    UrunId = x.ProductID,
                    KategoriAd = x.Categories.CategoryName,
                    TedarikciAd = x.Suppliers.CompanyName,
                    UrunAd = x.ProductName,
                    BirimFiyat = (decimal)x.UnitPrice,
                    Satılamaz = x.Discontinued,
                    TedarikciId = (int)x.SupplierID
                }).Where(x => x.Satılamaz == false && x.TedarikciId == SuplierId).ToList();
            }
        }
        public Urunler[] GetProductsWin()
        {
            Urunler[] ulist = db.Products.Select(x => new Urunler
            {
                Satılamaz = x.Discontinued,
                KategoriAd = x.Categories.CategoryName,
                TedarikciId = (int)x.SupplierID,
                TedarikciAd = x.Suppliers.CompanyName,
                BirimFiyat = (decimal)x.UnitPrice,
                UrunAd = x.ProductName,
                UrunId = x.ProductID

            }).OrderBy(y => y.UrunId).ToArray(); //array her yerde geçerli
            return ulist;

        }
        public UserDTO Login(string Email, string Password)
        {
            UserDTO dTO = new UserDTO();
            try
            {
                Users user = db.Users.Find(Email);
                if (user.Password == Password && user.UserId == Email)
                {
                    dTO.UserId = user.UserId;
                    dTO.Role = user.Role;
                    dTO.SupplierId = user.SupplierId;
                    dTO.UserName = user.UserName;
                    return dTO;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

    }
}
