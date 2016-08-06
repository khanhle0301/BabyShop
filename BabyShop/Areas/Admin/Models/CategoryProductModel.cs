using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BabyShop.Areas.Admin.Models
{
    public class CategoryProductModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        [MaxLength(50, ErrorMessage = "Tên danh mục không được quá 50 ký tự")]
        [Display(Name= "Tên danh mục")]
        [System.Web.Mvc.Remote("CateNameExists", "CategoryProduct", ErrorMessage = "Tên danh mục đã tồn tại.")]
        public string Name { get; set; }

        [StringLength(50)]
        public string MetaTitle { get; set; }

        [Display(Name = "Tên danh mục cha")]
        public int? ParentID { get; set; }

        [Display(Name = "Thứ tự hiển thị")]
        public int? DisplayOrder { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}