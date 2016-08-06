namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;
    [Table("Slide")]
    public partial class Slide
    {
        public int ID { get; set; }

        [Display(Name = "Hình ảnh")]
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
