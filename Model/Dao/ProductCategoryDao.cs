using Model.EF;
using PagedList;
using System.Collections.Generic;
using System.Linq;

namespace Model.Dao
{
    public class ProductCategoryDao
    {
        private BabyShopDbContext db = null;

        public ProductCategoryDao()
        {
            db = new BabyShopDbContext();
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
                model.ParentID = entity.ParentID;
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

        public IEnumerable<CategoryProduct> ListAllPaging(string sortOrder, string searchString, int page, int pageSize)
        {
            IQueryable<CategoryProduct> query = db.CategoryProducts;
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name":
                    query = query.OrderBy(x => x.Name);
                    break;
                case "name_desc":
                    query = query.OrderByDescending(x => x.Name);
                    break;
                case "id":
                    query = query.OrderBy(x => x.ID);
                    break;
                case "id_desc":
                    query = query.OrderByDescending(x => x.ID);
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

    }
}