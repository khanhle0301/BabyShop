using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class RoleDao
    {
        private BabyShopDbContext db = null;

        public RoleDao()
        {
            db = new BabyShopDbContext();
        }

        public List<Role> ListAll(string userGroupID)
        {
            var query = db.Credentials.Where(x => x.UserGroupID == userGroupID).Select(x => x.RoleID).ToList();           
            var data = (from a in db.Roles                     
                        where !query.Contains(a.ID)
                        select new
                        {
                            RoleID = a.ID,
                            Name = a.Name
                        }).AsEnumerable().Select(x => new Role()
                        {
                            ID = x.RoleID,
                            Name = x.Name
                        });
            return data.ToList();
        }
    }
}
