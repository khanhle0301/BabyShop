using Common;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Model.Dao
{
    public class ProductDao
    {
        BabyShopDbContext db = null;

        public ProductDao()
        {
            db = new BabyShopDbContext();
        }

        public List<Product> ListAllHotFlag(int top)
        {
            return db.Products.Where(x => x.Status && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public void UpdateImages(long productId, string images)
        {
            var product = db.Products.Find(productId);
            product.MoreImage = images;
            db.SaveChanges();
        }

        public bool Delete(int id)
        {
            try
            {
                var cate = db.Products.Find(id);
                db.Products.Remove(cate);
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
            return db.Products.Any(x => x.Name == name);
        }

        public List<Product> ListAllPaging()
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).ToList();
        }

        public int Product(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public IEnumerable<Product> ListAllByTag(string tag, int page, int pageSize, out int totalRow)
        {
            var model = (from a in db.Products
                         join b in db.ProductTags
                         on a.ID equals b.ProductID
                         where b.TagID == tag
                         select new
                         {
                             ID = a.ID,
                             Name = a.Name,
                             MetaTitle = a.Metatitle,
                             Image = a.Image,
                             MoreImage = a.MoreImage,
                             Price = a.Price,
                             PromotionPrice = a.PromotionPrice,
                             Quantity = a.Quantity,
                             Description = a.Description,
                             Detail = a.Detail,
                             CreatedDate = a.CreatedDate,
                             CreatedBy = a.CreatedBy,
                             UpdatedDate = a.UpdatedDate,
                             UpdatedBy = a.UpdatedBy,
                             ViewCount = a.ViewCount,
                             Status = a.Status

                         }).AsEnumerable().Select(x => new Product()
                         {
                             ID = x.ID,
                             Name = x.Name,
                             Metatitle = x.MetaTitle,
                             Image = x.Image,
                             MoreImage = x.MoreImage,
                             Price = x.Price,
                             PromotionPrice = x.PromotionPrice,
                             Quantity = x.Quantity,
                             Description = x.Description,
                             Detail = x.Detail,
                             CreatedDate = x.CreatedDate,
                             CreatedBy = x.CreatedBy,
                             UpdatedDate = x.UpdatedDate,
                             UpdatedBy = x.UpdatedBy,
                             ViewCount = x.ViewCount,
                             Status = x.Status
                         });
            totalRow = model.Count();
            return model.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }

        public List<Product> ListNewProduct(int top)
        {
            return db.Products.Where(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public List<Product> ListPromotionProduct(int top)
        {
            return db.Products.Where(x => x.Status && x.PromotionPrice.HasValue && x.PromotionPrice > 0).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public List<Product> ListHotProduct(int top)
        {
            return db.Products.Where(x => x.Status && x.QuantitySold > 0).OrderByDescending(x => x.QuantitySold).Take(top).ToList();
        }

        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }

        public List<Product> ListRelatedProducts(int productId)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        }

        public IEnumerable<Product> ListAllProduct(int page, int pageSize, string sort, out int totalRow)
        {
            var query = db.Products.Where(x => x.Status);
            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }
            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = db.Products.Where(x => x.Status && x.CategoryID == categoryId);

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = db.Products.Where(x => x.Status && x.Name.Contains(keyword));

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> ListAllSaleOffProduct(int page, int pageSize, string sort, out int totalRow)
        {
            var query = db.Products.Where(x => x.Status && x.PromotionPrice.HasValue && x.PromotionPrice > 0);
            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> ListAllHotProduct(int page, int pageSize, string sort, out int totalRow)
        {
            var query = db.Products.Where(x => x.Status && x.QuantitySold > 0);
            switch (sort)
            {
                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public List<string> MoreImage(int id)
        {
            ProductDao dao = new ProductDao();
            var product = dao.ViewDetail(id);
            var images = product.MoreImage;
            List<string> listImagesReturn = new List<string>();
            if (!string.IsNullOrEmpty(images))
            {
                XElement xImages = XElement.Parse(images);
                foreach (XElement element in xImages.Elements())
                {
                    listImagesReturn.Add(element.Value);
                }
            }
            return listImagesReturn;
        }

        public bool UpdateViewCount(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                product.ViewCount = product.ViewCount + 1;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Tag> ListTag(int contentId)
        {
            var model = (from a in db.Tags
                         join b in db.ProductTags
                         on a.ID equals b.TagID
                         where b.ProductID == contentId
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

        public List<Size> ListSize(int id)
        {
            var model = (from a in db.Sizes
                         join b in db.ProductSizes
                         on a.ID equals b.SizeID
                         where b.ProductID == id
                         select new
                         {
                             ID = b.SizeID,
                             Name = a.Name
                         }).AsEnumerable().Select(x => new Size()
                         {
                             ID = x.ID,
                             Name = x.Name
                         });
            return model.ToList();
        }


        public void InsertTag(string id, string name)
        {
            var tag = new Tag();
            tag.ID = id;
            tag.Name = name;
            tag.Type = CommonConstants.ProductTag;
            db.Tags.Add(tag);
            db.SaveChanges();
        }

        public void InsertProductTag(int productId, string tagId)
        {
            ProductTag productTag = new ProductTag();
            productTag.ProductID = productId;
            productTag.TagID = tagId;
            db.ProductTags.Add(productTag);
            db.SaveChanges();
        }

        public bool CheckTag(string id)
        {
            return db.Tags.Count(x => x.ID == id) > 0;
        }

        public void InsertSize(string id, string name)
        {
            var size = new Size();
            size.ID = id;
            size.Name = name;
            db.Sizes.Add(size);
            db.SaveChanges();
        }

        public void InsertProductSize(int productId, string sizeId)
        {
            ProductSize productSize = new ProductSize();
            productSize.ProductID = productId;
            productSize.SizeID = sizeId;
            db.ProductSizes.Add(productSize);
            db.SaveChanges();
        }

        public bool CheckSize(string id)
        {
            return db.Sizes.Count(x => x.ID == id) > 0;
        }


        public int Insert(Product product)
        {
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
                if (!string.IsNullOrEmpty(product.Tag))
                {
                    string[] tags = product.Tag.Split(',');
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
                        this.InsertProductTag(product.ID, tagId);
                    }
                }

                if (!string.IsNullOrEmpty(product.Size))
                {
                    string[] sizes = product.Size.Split(',');
                    foreach (var size in sizes)
                    {
                        var sizeID = StringHelper.ToUnsignString(size);
                        var existedSize = this.CheckSize(sizeID);
                        //insert to to size table
                        if (!existedSize)
                        {
                            this.InsertSize(sizeID, size);
                        }
                        //insert to product size
                        this.InsertProductSize(product.ID, sizeID);
                    }
                }
                return product.ID;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool Update(Product product)
        {
            try
            {
                var model = db.Products.Find(product.ID);
                model.Name = product.Name;
                model.Metatitle = StringHelper.ToUnsignString(product.Name);
                model.CategoryID = product.CategoryID;
                model.Image = product.Image;
                model.Price = product.Price;
                model.PromotionPrice = product.PromotionPrice;
                model.Quantity = product.Quantity;
                model.Description = product.Description;
                model.Detail = product.Detail;
                model.Tag = product.Tag;
                model.Size = product.Size;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = product.UpdatedBy;
                model.HotFlag = product.HotFlag;
                model.Status = product.Status;
                db.SaveChanges();
                this.RemoveAllContentTag(product.ID);
                //Xử lý tag
                if (!string.IsNullOrEmpty(product.Tag))
                {
                    string[] tags = product.Tag.Split(',');
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
                        this.InsertProductTag(product.ID, tagId);
                    }
                }

                this.RemoveAllProductSize(product.ID);
                //Xử lý Size
                if (!string.IsNullOrEmpty(product.Size))
                {
                    string[] sizes = product.Size.Split(',');
                    foreach (var size in sizes)
                    {
                        var sizeID = StringHelper.ToUnsignString(size);
                        var existedSize = this.CheckSize(sizeID);
                        //insert to to Size table
                        if (!existedSize)
                        {
                            this.InsertSize(sizeID, size);
                        }
                        //insert to product Size
                        this.InsertProductSize(product.ID, sizeID);
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
            db.ProductTags.RemoveRange(db.ProductTags.Where(x => x.ProductID == id));
            db.SaveChanges();
        }


        public void RemoveAllProductSize(int id)
        {
            db.ProductSizes.RemoveRange(db.ProductSizes.Where(x => x.ProductID == id));
            db.SaveChanges();
        }

    }
}