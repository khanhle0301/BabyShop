using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class TagDao
    {
        BabyShopDbContext db = null;
        public TagDao()
        {
            db = new BabyShopDbContext();
        }
    }
}
