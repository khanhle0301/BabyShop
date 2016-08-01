using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class PostDao
    {
        BabyShopDbContext db = null;

        public PostDao()
        {
            db = new BabyShopDbContext();
        }
        public IEnumerable<Post> ListAll(int page, int pageSize, out int totalRow)
        {
            var query = db.Posts.Where(x => x.Status);
            totalRow = query.Count();
            return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public Post GetByID(long id)
        {
            return db.Posts.Find(id);
        }


        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }

        public List<Tag> ListTag(int contentId)
        {
            var model = (from a in db.Tags
                         join b in db.PostTags
                         on a.ID equals b.TagID
                         where b.PostID == contentId
                         select new
                         {
                             ID = b.TagID,
                             Name = a.Name
                         }).AsEnumerable().Select(x => new Tag()
                         {
                             ID = x.ID,
                             Name = x.Name
                         });
            return model.ToList();
        }

        public IEnumerable<Post> ListAllByTag(string tag, int page, int pageSize, out int totalRow)
        {
            var model = (from a in db.Posts
                         join b in db.PostTags
                         on a.ID equals b.PostID
                         where b.TagID == tag
                         select new
                         {
                             Name = a.Name,
                             MetaTitle = a.Metatitle,
                             Image = a.Image,
                             Description = a.Description,
                             CreatedDate = a.CreatedDate,
                             CreatedBy = a.CreatedBy,
                             ID = a.ID

                         }).AsEnumerable().Select(x => new Post()
                         {
                             Name = x.Name,
                             Metatitle = x.MetaTitle,
                             Image = x.Image,
                             Description = x.Description,
                             CreatedDate = x.CreatedDate,
                             CreatedBy = x.CreatedBy,
                             ID = x.ID
                         });
            totalRow = model.Count();
            return model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }

    }
}
