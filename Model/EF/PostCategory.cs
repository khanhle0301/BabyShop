using Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.EF
{
    [Table("PostCategories")]
    public class PostCategory : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        [Display(Name = "Tên danh mục")]
        [MaxLength(50)]
        public string Name { set; get; }

        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string Metatitle { set; get; }

        public virtual IEnumerable<Post> Posts { set; get; }
    }
}