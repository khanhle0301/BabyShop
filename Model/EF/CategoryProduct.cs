namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;
    [Table("CategoryProduct")]
    public partial class CategoryProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CategoryProduct()
        {
            Products = new HashSet<Product>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        [MaxLength(50, ErrorMessage = "Tên danh mục không được quá 50 ký tự")]
        [Display(Name = "Tên danh mục")]     
        public string Name { get; set; }

        [StringLength(50)]
        public string MetaTitle { get; set; }

        [Display(Name = "Tên danh mục cha")]
        public int? ParentID { get; set; }

        [Display(Name = "Thứ tự hiển thị")]
        public int? DisplayOrder { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Tạo bởi")]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Cập nhật bởi")]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [Display(Name= "Trạng thái")]
        public bool Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
