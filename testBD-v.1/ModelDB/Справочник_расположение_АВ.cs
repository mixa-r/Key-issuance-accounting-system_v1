namespace testBD_v._1.ModelDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Справочник расоложение АВ")]
    public partial class Справочник_расоложение_АВ
    {

        [Key]
        [Column("Код расположения АВ")]
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Код_расположения_АВ { get; set; }

        [Column("Расположение АВ")]
        [Required]
        [StringLength(50)]
        public string Расположение_АВ { get; set; }

    }
}
