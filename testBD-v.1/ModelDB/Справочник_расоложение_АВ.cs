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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Справочник_расоложение_АВ()
        {
            Справочник_ключи = new HashSet<Справочник_ключи>();
        }

        [Key]
        [Column("Код расположения АВ")]
        public int Код_расположения_АВ { get; set; }

        [Column("Расположение АВ")]
        [Required]
        [StringLength(50)]
        public string Расположение_АВ { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Справочник_ключи> Справочник_ключи { get; set; }
    }
}
