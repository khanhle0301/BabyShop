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

        [Display(Name = "Nội dung")]
        [AllowHtml]
        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        [Column(TypeName = "ntext")]
        public string Detail { set; get; }
    }
}