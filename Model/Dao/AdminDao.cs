using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class AdminDao
    {
        private BabyShopDbContext db = null;

        public AdminDao()
        {
            db = new BabyShopDbContext();
        }


        public List<string> GetListCredential(string userName)
        {
            var user = db.Admins.Single(x => x.UserName == userName);
            var data = (from a in db.Credentials
                        join b in db.AdminGroups on a.AdminGroupID equals b.ID
                        join c in db.Roles on a.RoleID equals c.ID
                        where b.ID == user.GroupID
                        select new
                        {
                            RoleID = a.RoleID,
                            UserGroupID = a.AdminGroupID
                        }).AsEnumerable().Select(x => new Credential()
                        {
                            RoleID = x.RoleID,
                            AdminGroupID = x.UserGroupID
                        });
            return data.Select(x => x.RoleID).ToList();

        }

        public bool ChangeStatus(int id)
        {
            var user = db.Admins.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }

        public int Insert(Admin entity)
        {
            try
            {
                db.Admins.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public bool Update(Admin entity)
        {
            try
            {
                var admin = db.Admins.Find(entity.ID);
                admin.Name = entity.Name;
                admin.Address = entity.Address;
                admin.Email = entity.Email;
                admin.Phone = entity.Phone;
                admin.UpdatedDate = DateTime.Now;
                admin.UpdatedBy = entity.UpdatedBy;
                admin.Status = entity.Status;
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
                var admin = db.Admins.Find(id);
                db.Admins.Remove(admin);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Admin> ListAllPaging()
        {
            return db.Admins.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public Admin ViewDetail(int id)
        {
            return db.Admins.Find(id);
        }

        public Admin GetByID(string userName)
        {
            return db.Admins.SingleOrDefault(x => x.UserName == userName);
        }

        public bool UserNameExists(string userName)
        {
            return db.Admins.Any(x => x.UserName == userName);
        }


        public bool EmailExists(string email)
        {
            return db.Admins.Any(x => x.Email == email);
        }


        public int Login(string userName, string passWord)
        {
            var result = db.Admins.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
                return 0;
            else
            {
                if (result.Status == false)
                    return -1;
                else
                {
                    if (result.Password == passWord)
                        return 1;
                    else
                        return -2;
                }
            }
        }
    }
}
