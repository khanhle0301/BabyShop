using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace Model.Dao
{
    public class UserDao
    {
        private BabyShopDbContext db = null;

        public UserDao()
        {
            db = new BabyShopDbContext();
        }
        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }

        public bool ChangeStatus(int id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }


        public IEnumerable<User> ListAllPaging(string sortOrder, string searchString, int page, int pageSize)
        {
            IQueryable<User> query = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Name.Contains(searchString) || x.Email.Contains(searchString) || x.UserName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name":
                    query = query.OrderBy(x => x.Name);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(x => x.Name);
                    break;
                case "email":
                    query = query.OrderBy(x => x.Email);
                    break;
                case "email_desc":
                    query = query.OrderByDescending(x => x.Email);
                    break;
                case "username":
                    query = query.OrderBy(x => x.UserName);
                    break;
                case "username_desc":
                    query = query.OrderByDescending(x => x.UserName);
                    break;
                case "date":
                    query = query.OrderBy(x => x.CreatedDate);
                    break;
                case "date_desc":
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
                case "status":
                    query = query.OrderBy(x => x.Status);
                    break;
                case "status_desc":
                    query = query.OrderByDescending(x => x.Status);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }
            return query.ToPagedList(page, pageSize);
        }

        public int Login(string userName, string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
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

        public int InsertForFacebook(User entity)
        {
            try
            {
                var user = db.Users.SingleOrDefault(x => x.UserName == entity.UserName);
                if (user == null)
                {
                    db.Users.Add(entity);
                    db.SaveChanges();
                    return entity.ID;
                }
                else
                {
                    return user.ID;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                return 0;
            }
        }

        public User GetByID(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }


        public User GetID(int id)
        {
            return db.Users.SingleOrDefault(x => x.ID == id);
        }

        public bool CheckUserName(string userName)
        {
            return db.Users.Count(x => x.UserName == userName) > 0;
        }

        public bool CheckEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }

        public int Insert(User entity)
        {
            try
            {
                db.Users.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public bool Update(User entity)
        {
            try
            {
                var admin = db.Users.Find(entity.ID);
                admin.Name = entity.Name;
                admin.Address = entity.Address;
                admin.Email = entity.Email;
                admin.Phone = entity.Phone;
                admin.UpdatedDate = DateTime.Now;              
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
                var user = db.Users.Find(id);
                db.Users.Remove(user);
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