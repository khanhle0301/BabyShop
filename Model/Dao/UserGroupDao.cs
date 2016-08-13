using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserGroupDao
    {

        private BabyShopDbContext db = null;

        public UserGroupDao()
        {
            db = new BabyShopDbContext();
        }

        public List<UserGroup> CheckUserGroupID(string userGroupID)
        {
            return db.UserGroups.Where(x => x.ID == userGroupID).ToList();
        }       

        public bool CheckID(string id)
        {
            return db.UserGroups.Any(x => x.ID == id);
        }

        public List<UserGroup> ListAll()
        {
            return db.UserGroups.Where(x => x.ID != "ADMIN").ToList();
        }

        public List<UserGroup> List_All()
        {
            return db.UserGroups.ToList();
        }

        public UserGroup ViewDetail(string id)
        {
            return db.UserGroups.Find(id);
        }

        public bool Insert(UserGroup entity)
        {
            try
            {
                db.UserGroups.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(UserGroup entity)
        {
            try
            {
                var UserGroup = db.UserGroups.Find(entity.ID);
                UserGroup.Name = entity.Name;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                var UserGroup = db.UserGroups.Find(id);
                db.UserGroups.Remove(UserGroup);
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
