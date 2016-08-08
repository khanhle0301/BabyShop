using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Model.EF
{
    [Table("Slides")]
    public partial class Slide
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Hình ảnh")]
        [MaxLength(250)]
        [Required(ErrorMessage = "Vui lòng chọn hình")]
        public string URL { get; set; }

        [AllowHtml]
        [Display(Name = "Nội dung")]
        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}