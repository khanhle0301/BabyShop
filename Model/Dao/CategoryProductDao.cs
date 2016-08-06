using Common;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Dao
{
    public class CategoryProductDao
    {
        private BabyShopDbContext db = null;

        public CategoryProductDao()
        {
            db = new BabyShopDbContext();
        }


        public bool Delete(int id)
        {
            try
            {
                var cate = db.CategoryProducts.Find(id);
                db.CategoryProducts.Remove(cate);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool NameExists(string name)
        {
            return db.CategoryProducts.Any(x => x.Name == name);
        }

        public int Insert(CategoryProduct entity)
        {
            db.CategoryProducts.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(CategoryProduct entity)
        {
            try
            {
                var model = db.CategoryProducts.Find(entity.ID);
                model.Name = entity.Name;
                model.MetaTitle = StringHelper.ToUnsignString(entity.Name);
                model.ParentID = entity.ParentID;
                model.DisplayOrder = entity.DisplayOrder;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = entity.UpdatedBy;
                model.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public CategoryProduct ViewDetail(int id)
        {
            return db.CategoryProducts.Find(id);
        }

        public List<CategoryProduct> ListAll()
        {
            return db.CategoryProducts.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

        public List<CategoryProduct> ListByID(int parentId)
        {
            return db.CategoryProducts.Where(x => x.ParentID == parentId & x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

        public List<CategoryProduct> ListAllPaging()
        {
            return db.CategoryProducts.OrderByDescending(x => x.CreatedDate).ToList();
        }

    }
}