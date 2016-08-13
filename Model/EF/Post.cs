using Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Model.EF
{
    [Table("Posts")]
    public class Post : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID {  set; get; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề tin tức")]
        [MaxLength(50)]
        [Display(Name = "Tiêu đề")]
        public string Name {  set; get; }

        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string Metatitle {  set; get; }

        [Required(ErrorMessage = "Vui lòng nhập từ khóa")]
        [Display(Name = "Từ khóa")]
        [MaxLength(200)]
        public string Metakeyword { set; get; }

        [Display(Name = "Loại tin")]
        public int CategoryID {  set; get; }

        [Display(Name = "Ảnh sản phẩm")]
        [MaxLength(250)]
        [Required(ErrorMessage = "Vui lòng chọn ảnh")]
        public string Image {  set; get; }

        [Display(Name = "Mô tả")]
        [MaxLength(500)]
        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public string Description {  set; get; }

        [Required(ErrorMessage = "Vui lòng nhập chi tiết")]
        [Display(Name = "Chi tiết")]   
        [Column(TypeName = "ntext")]
        public string Detail {  set; get; }

        [MaxLength(500)]
        public string Tag {  set; get; }

        [ForeignKey("CategoryID")]
        public virtual PostCategory PostCategory { set; get; }

        public virtual IEnumerable<PostTag> PostTags { set; get; }
    }
}