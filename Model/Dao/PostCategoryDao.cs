using Common;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class PostCategoryDao
    {
        private BabyShopDbContext db = null;
        public PostCategoryDao()
        {
            db = new BabyShopDbContext();
        }

        public bool ChangeStatus(int id)
        {
            var cate = db.PostCategories.Find(id);
            cate.Status = !cate.Status;
            db.SaveChanges();
            return cate.Status;
        }

        public PostCategory ViewDetail(int id)
        {
            return db.PostCategories.Find(id);
        }
        public IEnumerable<PostCategory> ListAllPaging()
        {          
            return db.PostCategories.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public bool Delete(int id)
        {
            try
            {
                var cate = db.PostCategories.Find(id);
                db.PostCategories.Remove(cate);
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
            return db.PostCategories.Any(x => x.Name == name);
        }

        public int Insert(PostCategory entity)
        {
            db.PostCategories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(PostCategory entity)
        {
            try
            {
                var model = db.PostCategories.Find(entity.ID);
                model.Name = entity.Name;
                model.Metatitle = StringHelper.ToUnsignString(entity.Name);
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = entity.UpdatedBy;
                model.Metakeyword = entity.Metakeyword;
                model.Description = entity.Description;
                model.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<PostCategory> ListAll()
        {
            return db.PostCategories.ToList();
        }

        public List<PostCategory> ListAllHome()
        {
            return db.PostCategories.Where(x=>x.Status).ToList();
        }
    }
}
