using Model.EF;
using System.Collections.Generic;
using System.Linq;

namespace Model.Dao
{
    public class MenuDao
    {
        private BabyShopDbContext db = null;

        public MenuDao()
        {
            db = new BabyShopDbContext();
        }

        public List<Menu> ListByGroupId(int groupId)
        {
            return db.Menus.Where(x => x.TypeID == groupId && x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}