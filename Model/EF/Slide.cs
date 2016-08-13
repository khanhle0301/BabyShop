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

        [Required(ErrorMessage = "Vui lòng nhập tên slide")]
        [MaxLength(50)]
        [Display(Name = "Tên slide")]
        public string Name { set; get; }

        [Display(Name = "Hình ảnh")]
        [MaxLength(250)]
        [Required(ErrorMessage = "Vui lòng chọn hình")]
        public string URL { get; set; }  

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}