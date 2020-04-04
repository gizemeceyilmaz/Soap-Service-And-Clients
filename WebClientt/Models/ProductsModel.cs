using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebClientt.ServiceProduct;

namespace WebClientt.Models
{
    public class ProductsModel
    {
        public Urunler SecUrun { get; set; }
        public List<Urunler> ulist { get; set; }
        public Urunler Urun { get; set; }

    }
}