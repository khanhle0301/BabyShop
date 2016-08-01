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

        [Required]
        [Display(Name= "Tên danh mục")]
        public string Name { get; set; }

        [StringLength(50)]
        public string MetaTitle { get; set; }

        [Display(Name = "Tên danh mục cha")]
        public int? ParentID { get; set; }

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