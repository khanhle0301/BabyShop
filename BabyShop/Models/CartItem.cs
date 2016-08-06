using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyShop.Models
{
    [Serializable]
    public class CartItem
    {
        public int ProductId { set; get; }
        public Product Product { set; get; }
        public int Quantity { set; get; }
        public string Size { set; get; }
    }
}