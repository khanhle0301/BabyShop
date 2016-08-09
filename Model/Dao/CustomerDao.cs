using Model.EF;
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

        public IEnumerable<Customer> ListAllPaging(string sortOrder, string searchString, int page, int pageSize)
        {
            IQueryable<Customer> query = db.Customers;
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
            catch (Exception)
            {
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