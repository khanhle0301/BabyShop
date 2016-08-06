using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Dao
{
    public class OrderDetailDao
    {
        private BabyShopDbContext db = null;

        public OrderDetailDao()
        {
            db = new BabyShopDbContext();
        }
        public OrderDetail ViewDetail(int id)
        {
            return db.OrderDetails.Find(id);
        }

        public bool Delete(int id)
        {
            try
            {
                var Order = db.OrderDetails.Find(id);
                db.OrderDetails.Remove(Order);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<OrderDetail> ListAll()
        {
            return db.OrderDetails.OrderByDescending(x => x.OrderID).ToList();
        }

        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}