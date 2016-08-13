using Model.EF;
using Model.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace Model.Dao
{
    public class CustomerDao
    {
        private BabyShopDbContext db = null;

        public CustomerDao()
        {
            db = new BabyShopDbContext();
        }

        public Customer ViewDetail(int id)
        {
            return db.Customers.Find(id);
        }

        public UserEditModel ViewDetailEdit(int id)
        {
            var model = (from a in db.Customers
                         where a.ID == id
                         select new
                         {
                             ID = a.ID,
                             Name = a.Name,
                             Address = a.Address,
                             Email = a.Email,
                             Phone = a.Phone
                         }).AsEnumerable().Select(x => new UserEditModel()
                         {
                             ID = x.ID,
                             Name = x.Name,
                             Address = x.Address,
                             Email = x.Email,
                             Phone = x.Phone
                         });
            return model.FirstOrDefault();
        }

        public bool ChangePass(Customer entity)
        {
            try
            {
                var admin = db.Customers.Find(entity.ID);
                admin.Password = entity.Password;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Forgotpassword(Customer entity)
        {
            try
            {
                var admin = db.Customers.SingleOrDefault(x => x.Email == entity.Email);
                admin.Password = entity.Password;
                db.SaveChanges();
                return true;
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
                return false;
            }
        }

        public bool ChangeStatus(int id)
        {
            var user = db.Customers.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }

        public List<Customer> ListAll()
        {
            return db.Customers.ToList();
        }

        public int Login(string userName, string passWord)
        {
            var result = db.Customers.SingleOrDefault(x => x.UserName == userName);
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

        public int InsertForFacebook(Customer entity)
        {
            try
            {
                var user = db.Customers.SingleOrDefault(x => x.UserName == entity.UserName);
                if (user == null)
                {
                    db.Customers.Add(entity);
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

        public Customer GetByID(string userName)
        {
            return db.Customers.SingleOrDefault(x => x.UserName == userName);
        }

        public Customer GetID(int id)
        {
            return db.Customers.SingleOrDefault(x => x.ID == id);
        }

        public bool CheckUserName(string userName)
        {
            return db.Customers.Count(x => x.UserName == userName) > 0;
        }

        public bool CheckEmail(string email)
        {
            return db.Customers.Count(x => x.Email == email) > 0;
        }

        public int Insert(Customer entity)
        {
            try
            {
                db.Customers.Add(entity);
                db.SaveChanges();
                return entity.ID;
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

        public bool Update(Customer entity)
        {
            try
            {
                var admin = db.Customers.Find(entity.ID);
                admin.Name = entity.Name;
                admin.Address = entity.Address;
                admin.Email = entity.Email;
                admin.Phone = entity.Phone;
                admin.UpdatedDate = DateTime.Now;
                admin.Status = true;
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
                var user = db.Customers.Find(id);
                db.Customers.Remove(user);
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