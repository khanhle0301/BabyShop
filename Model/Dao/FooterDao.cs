using Model.EF;
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
    }
}