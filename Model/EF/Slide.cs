namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(250)]
        public string URL { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public int? DisplayOrder { get; set; }

        public bool Status { get; set; }
    }
}
