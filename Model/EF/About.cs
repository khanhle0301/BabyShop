using Model.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Model.EF
{
    [Table("About")]
    public class About : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        [MaxLength(50)]
        public string Name { set; get; }

        [Display(Name = "Nội dung")]       
        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        [Column(TypeName = "ntext")]
        public string Detail { set; get; }
    }
}