using Model.EF;

namespace Model.Dao
{
    public class OrderDao
    {
        private BabyShopDbContext db = null;

        public OrderDao()
        {
            db = new BabyShopDbContext();
        }

        public int Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
    }
}