using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CredentialDao
    {
        private BabyShopDbContext db = null;
        public CredentialDao()
        {
            db = new BabyShopDbContext();
        }     

        public List<CredentialViewModel> ListAllByID(string id)
        {
            var data = (from a in db.Credentials
                        join b in db.UserGroups on a.UserGroupID equals b.ID
                        join c in db.Roles on a.RoleID equals c.ID
                        where a.UserGroupID == id
                        select new
                        {
                            UserGroupID = a.UserGroupID,
                            RoleID = a.RoleID,
                            NameGroup = b.Name,
                            NameRole = c.Name,
                            Status = a.Status
                        }).AsEnumerable().Select(x => new CredentialViewModel()
                        {
                            UserGroupID = x.UserGroupID,
                            NameGroup = x.NameGroup,
                            RoleID = x.RoleID,
                            NameRole = x.NameRole,
                            Status = x.Status
                        });
            return data.ToList();
        }

        public bool ChangeStatus(string groupID, string roleID)
        {
            var user = db.Credentials.Single(x => x.UserGroupID == groupID && x.RoleID == roleID);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }

        public bool Insert(Credential entity)
        {
            try
            {
                db.Credentials.Add(entity);
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
