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
    public class PostDao
    {
        BabyShopDbContext db = null;

        public PostDao()
        {
            db = new BabyShopDbContext();
        }      

        public void InsertTag(string id, string name)
        {
            var tag = new Tag();
            tag.ID = id;
            tag.Name = name;
            tag.Type = CommonConstants.PostTag;
            db.Tags.Add(tag);
            db.SaveChanges();
        }

        public void InsertPostTag(int postId, string tagId)
        {
            PostTag postTag = new PostTag();
            postTag.PostID = postId;
            postTag.TagID = tagId;
            db.PostTags.Add(postTag);
            db.SaveChanges();
        }

        public bool CheckTag(string id)
        {
            return db.Tags.Count(x => x.ID == id) > 0;
        }

        public int Insert(Post post)
        {
            try
            {
                db.Posts.Add(post);
                db.SaveChanges();
                if (!string.IsNullOrEmpty(post.Tag))
                {
                    string[] tags = post.Tag.Split(',');
                    foreach (var tag in tags)
                    {
                        var tagId = StringHelper.ToUnsignString(tag);
                        var existedTag = this.CheckTag(tagId);
                        //insert to to tag table
                        if (!existedTag)
                        {
                            this.InsertTag(tagId, tag);
                        }
                        //insert to product tag
                        this.InsertPostTag(post.ID, tagId);
                    }
                }
                return post.ID;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool Update(Post post)
        {
            try
            {
                var model = db.Posts.Find(post.ID);
                model.Name = post.Name;
                model.Metatitle = StringHelper.ToUnsignString(post.Name);
                model.CategoryID = post.CategoryID;
                model.Image = post.Image;              
                model.Description = post.Description;
                model.Detail = post.Detail;
                model.Tag = post.Tag;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = post.UpdatedBy;
                model.Status = post.Status;
                db.SaveChanges();
                //Xử lý tag
                this.RemoveAllContentTag(post.ID);
                if (!string.IsNullOrEmpty(post.Tag))
                {             
                    string[] tags = post.Tag.Split(',');
                    foreach (var tag in tags)
                    {
                        var tagId = StringHelper.ToUnsignString(tag);
                        var existedTag = this.CheckTag(tagId);
                        //insert to to tag table
                        if (!existedTag)
                        {
                            this.InsertTag(tagId, tag);
                        }
                        //insert to product tag
                        this.InsertPostTag(post.ID, tagId);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void RemoveAllContentTag(int id)
        {
            db.PostTags.RemoveRange(db.PostTags.Where(x => x.PostID == id));
            db.SaveChanges();
        }


        public Post ViewDetail(int id)
        {
            return db.Posts.Find(id);
        }
        public List<Post> ListAllPaging()
        {                    
            return db.Posts.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public bool Delete(int id)
        {
            try
            {
                var cate = db.Posts.Find(id);
                db.Posts.Remove(cate);
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
            return db.Posts.Any(x => x.Name == name);
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
