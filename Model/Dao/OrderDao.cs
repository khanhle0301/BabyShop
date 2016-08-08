using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Dao
{
    public class OrderDao
    {
        private BabyShopDbContext db = null;

        public OrderDao()
        {
            db = new BabyShopDbContext();
        }

        public bool ChangeStatus(int id)
        {
            var order = db.Orders.Find(id);
            order.Status = !order.Status;
            db.SaveChanges();
            return order.Status;
        }

        public int Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }


        public Order ViewDetail(int id)
        {
            return db.Orders.Find(id);
        }

        public bool Delete(int id)
        {
            try
            {
                var Order = db.Orders.Find(id);
                db.Orders.Remove(Order);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Order> ListAll()
        {
            return db.Orders.OrderByDescending(x => x.CreatedDate).ToList();
        }
    }
}