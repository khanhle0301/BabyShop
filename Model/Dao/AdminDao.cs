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

        public bool ChangeStatus(int id)
        {
            var admin = db.Admins.Find(id);
            admin.Status = !admin.Status;
            db.SaveChanges();
            return admin.Status;
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
