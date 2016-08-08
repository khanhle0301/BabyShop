using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Dao
{
    public class SlideDao
    {
        private BabyShopDbContext db = null;

        public SlideDao()
        {
            db = new BabyShopDbContext();
        }

        public bool ChangeStatus(int id)
        {
            var slide = db.Slides.Find(id);
            slide.Status = !slide.Status;
            db.SaveChanges();
            return slide.Status;
        }

        public List<Slide> SelectAll()
        {
            return db.Slides.Where(x => x.Status == true).OrderByDescending(x => x.ID).ToList();
        }

        public List<Slide> ListAll()
        {
            return db.Slides.OrderByDescending(x => x.ID).ToList();
        }

        public Slide ViewDetail(int id)
        {
            return db.Slides.Find(id);
        }


        public int Insert(Slide entity)
        {
            try
            {
                db.Slides.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public bool Update(Slide entity)
        {
            try
            {
                var Slide = db.Slides.Find(entity.ID);
                Slide.Content = entity.Content;
                Slide.Status = entity.Status;
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
                var Slide = db.Slides.Find(id);
                db.Slides.Remove(Slide);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}