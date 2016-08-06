using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
   public class AboutDao
    {
        private BabyShopDbContext db = null;

        public AboutDao()
        {
            db = new BabyShopDbContext();
        }

        public About ViewDetail(int id)
        {
            return db.Abouts.Find(id);
        }


        public int Insert(About entity)
        {
            try
            {
                db.Abouts.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public bool Update(About entity)
        {
            try
            {
                var about = db.Abouts.Find(entity.ID);
                about.Detail = entity.Detail;
                about.UpdatedDate = DateTime.Now;
                about.UpdatedBy = entity.UpdatedBy;
                about.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public bool Delete(int id)
        {
            try
            {
                var about = db.Abouts.Find(id);
                db.Abouts.Remove(about);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<About> ListAll()
        {
            return db.Abouts.ToList();
        }

        public About GetAbout()
        {
            return db.Abouts.SingleOrDefault(x => x.Status == true);
        }
    }
}
