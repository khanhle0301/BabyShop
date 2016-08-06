using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Models
{
    public class ProductModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [MaxLength(100, ErrorMessage = "Tên sản phẩm không được quá 100 ký tự")]
        [Display(Name = "Tên sản phẩm")]
        [System.Web.Mvc.Remote("NameExists", "Product", ErrorMessage = "Tên sản phẩm đã tồn tại.")]
        public string Name { get; set; }
       
        public string Metatitle { get; set; }

        [Display(Name = "Loại sản phẩm")]
        public int CategoryID { get; set; }      

        [Display(Name = "Ảnh sản phẩm")]
        [Required(ErrorMessage = "Vui lòng chọn ảnh")]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Vui lòng nhập giá")]
        public decimal? Price { get; set; }

        [Display(Name = "Giá khuyến mãi")]       
        public decimal? PromotionPrice { get; set; }

        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        public int? Quantity { get; set; }

        public int? QuantitySold { get; set; }

        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập chi tiết sản phẩm")]
        [Display(Name = "Chi tiết")]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập size")]
        public string Size { get; set; }

        public string Tag { get; set; }

        public DateTime? CreatedDate { get; set; }
       
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
      
        public string UpdatedBy { get; set; }    

        public int? ViewCount { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

    }
}