using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Model.EF
{
    [Table("Footers")]
    public class Footer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [AllowHtml]
        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        [Display(Name = "Nội dung")]
        [Column(TypeName = "ntext")]
        public string Content { set; get; }

        public bool Status { set; get; }
    }
}