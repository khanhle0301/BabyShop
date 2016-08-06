namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;
    [Table("Footer")]
    public partial class Footer
    {
        public int ID { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Nhập nội dung")]
        [Display(Name = "Nội dung")]
        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public bool Status { get; set; }
    }
}
