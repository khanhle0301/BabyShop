using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
  public  class CategoryDao
    {
        private BabyShopDbContext db = null;

        public CategoryDao()
        {
            db = new BabyShopDbContext();
        }
        public CategoryProduct ViewDetail(long id)
        {
            return db.CategoryProducts.Find(id);
        }      

    }
}
