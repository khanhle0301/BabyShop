namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;
    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [MaxLength(100, ErrorMessage = "Tên sản phẩm không được quá 100 ký tự")]
        [Display(Name = "Tên sản phẩm")]       
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

        public virtual CategoryProduct CategoryProduct { get; set; }

        public virtual IEnumerable<SizeProduct> SizeProduct { set; get; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
