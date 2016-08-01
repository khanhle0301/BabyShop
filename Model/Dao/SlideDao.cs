using Model.EF;
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

        public List<Slide> SelectAll()
        {
            return db.Slides.Where(x => x.Status == true).OrderByDescending(x => x.DisplayOrder).ToList();
        }
    }
}