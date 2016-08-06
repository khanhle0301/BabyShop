using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
   public class FeedbackDao
    {
        private BabyShopDbContext db = null;

        public FeedbackDao()
        {
            db = new BabyShopDbContext();
        }
        public int Insert(Feedback fb)
        {
            db.Feedbacks.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }


        public bool Delete(int id)
        {
            try
            {
                var feedback = db.Feedbacks.Find(id);
                db.Feedbacks.Remove(feedback);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Feedback ViewDetail(int id)
        {
            return db.Feedbacks.Find(id);
        }

        public List<Feedback> ListAll()
        {
            return db.Feedbacks.OrderByDescending(x=>x.CreateDate).ToList();
        }
    }
}
