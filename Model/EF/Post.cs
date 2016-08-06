namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;
    [Table("Post")]
    public partial class Post
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề tin tức")]     
        [Display(Name = "Tiêu đề")]
        public string Name { get; set; }

        [StringLength(100)]
        public string Metatitle { get; set; }

        [Display(Name = "Loại tin")]
        public int CategoryID { get; set; }

        [Display(Name = "Ảnh sản phẩm")]
        [Required(ErrorMessage = "Vui lòng chọn ảnh")]     
        public string Image { get; set; }

        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập chi tiết")]
        [Display(Name = "Chi tiết")]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        [StringLength(500)]
        public string Tag { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Tạo bởi")]
        public string CreatedBy { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Cập nhật bởi")]
        public string UpdatedBy { get; set; }

        [Display(Name = "Lượt xem")]
        public int? ViewCount { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        public virtual PostCategory PostCategory { get; set; }
    }
}
