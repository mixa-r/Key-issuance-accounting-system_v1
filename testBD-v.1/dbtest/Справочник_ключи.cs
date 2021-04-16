namespace testBD_v._1.dbtest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Справочник ключи")]
    public partial class Справочник_ключи
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Справочник_ключи()
        {
            Журнал = new HashSet<Журнал>();
        }

        [Key]
        [Column("Код ключа")]
        public int Код_ключа { get; set; }

        [Required]
        [StringLength(50)]
        public string Адрес { get; set; }

        [Column("Код расположения АВ")]
        public int Код_расположения_АВ { get; set; }

        public int? Подъезд { get; set; }

        [Column("Код района")]
        public int Код_района { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Журнал> Журнал { get; set; }

        public virtual Справочник_районы Справочник_районы { get; set; }

        public virtual Справочник_расоложение_АВ Справочник_расоложение_АВ { get; set; }
    }
}
