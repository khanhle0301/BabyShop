using Model.EF;
using System.Linq;

namespace Model.Dao
{
    public class ContactDao
    {
        private BabyShopDbContext db = null;

        public ContactDao()
        {
            db = new BabyShopDbContext();
        }

        public Contact GetActiveContact()
        {
            return db.Contacts.Single(x => x.Status == true);
        }

        public int InsertFeedBack(Feedback fb)
        {
            db.Feedbacks.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }
    }
}