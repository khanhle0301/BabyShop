using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Dao
{
    public class FooterDao
    {
        private BabyShopDbContext db = null;

        public FooterDao()
        {
            db = new BabyShopDbContext();
        }

        public Footer GetFooter()
        {
            return db.Footers.SingleOrDefault(x => x.Status == true);
        }

        public Footer ViewDetail(int id)
        {
            return db.Footers.Find(id);
        }


        public int Insert(Footer entity)
        {
            try
            {
                db.Footers.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public bool Update(Footer entity)
        {
            try
            {
                var Footer = db.Footers.Find(entity.ID);
                Footer.Content = entity.Content;             
                Footer.Status = entity.Status;
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
                var Footer = db.Footers.Find(id);
                db.Footers.Remove(Footer);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Footer> ListAll()
        {
            return db.Footers.ToList();
        }
    }
}