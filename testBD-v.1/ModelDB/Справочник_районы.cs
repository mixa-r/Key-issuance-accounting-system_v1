namespace testBD_v._1.ModelDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Справочник районы")]
    public partial class Справочник_районы
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Справочник_районы()
        {
            Справочник_ключи = new HashSet<Справочник_ключи>();
        }

        [Key]
        [Column("Код района")]
        public int Код_района { get; set; }

        [Required]
        [StringLength(50)]
        public string Район { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Справочник_ключи> Справочник_ключи { get; set; }
    }
}
