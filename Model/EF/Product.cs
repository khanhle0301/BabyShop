using Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Model.EF
{
    [Table("Products")]
    public class Product : Auditable
    {       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID {set; get;}

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [MaxLength(100, ErrorMessage = "Tên sản phẩm không được quá 100 ký tự")]
        [Display(Name = "Tên sản phẩm")]
        public string Name {set; get;}

        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string Metatitle {set; get;}

        [Display(Name = "Từ khóa")]
        [Required(ErrorMessage = "Vui lòng nhập từ khóa")]
        [MaxLength(200)]        
        public string Metakeyword { set; get; }

        [Display(Name = "Loại sản phẩm")]
        public int CategoryID {set; get;}

        [Display(Name = "Ảnh sản phẩm")]
        [MaxLength(250)]
        [Required(ErrorMessage = "Vui lòng chọn ảnh")]
        public string Image {set; get;}

        [Column(TypeName = "xml")]
        public string MoreImage {set; get;}

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Vui lòng nhập giá")]
        public decimal Price {set; get;}

        [Display(Name = "Giá khuyến mãi")]
        public decimal? PromotionPrice {set; get;}

        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        public int Quantity {set; get;}

        [Display(Name = "Số lượng đã bán")]       
        public int? QuantitySold {set; get;}

        [Display(Name = "Mô tả")]
        [MaxLength(500)]
        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public string Description {set; get;}

        [Required(ErrorMessage = "Vui lòng nhập chi tiết sản phẩm")]
        [Display(Name = "Chi tiết")]      
        [Column(TypeName = "ntext")]
        public string Detail {set; get;}

        [MaxLength(250)]
        [Required(ErrorMessage = "Vui lòng nhập size")]
        public string Size {set; get;}

        [MaxLength(250)]
        public string Tag {set; get;}

        [Display(Name = "Hiển thị trang chủ")]
        public bool HomeFlag {set; get;}

        [Display(Name = "Sản phẩm Hot")]
        public bool HotFlag { set; get; }

        [Display(Name = "Sản phẩm khuyến mãi")]
        public bool PromotionFlag { set; get;}

        [Display(Name = "Lượt xem")]
        public int ViewCount {set; get;}

        [MaxLength(1000)]
        public string TTTT { set; get; }

        [ForeignKey("CategoryID")]
        public virtual ProductCategory ProductCategory {set; get;}

        public virtual IEnumerable<ProductTag> ProductTags {set; get;}

        public virtual IEnumerable<ProductSize> ProductSizes { set; get;}
    }
}