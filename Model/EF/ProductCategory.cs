using Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.EF
{
    [Table("ProductCategories")]
    public class ProductCategory : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID {set; get; }

        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        [MaxLength(50, ErrorMessage = "Tên danh mục không được quá 50 ký tự")]
        [Display(Name = "Tên danh mục")]
        public string Name {set; get; }

        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string MetaTitle {set; get; }

        [Display(Name = "Tên danh mục cha")]
        public int? ParentID {set; get; }

        [Required(ErrorMessage = "Vui lòng nhập thứ tự hiển thị")]
        [Display(Name = "Thứ tự hiển thị")]
        public int DisplayOrder {set; get; }

        public virtual IEnumerable<Product> Products {set; get; }
    }
}