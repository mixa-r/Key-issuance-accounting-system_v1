namespace testBD_v._1.ModelDB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Справочник сотрдники")]
    public partial class Справочник_сотрдники
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Справочник_сотрдники()
        {
            Журнал = new HashSet<Журнал>();
        }

        [Key]
        [Column("Код сотрудника")]
        public int Код_сотрудника { get; set; }

        [StringLength(150)]
        public string ФИО { get; set; }

        [StringLength(50)]
        public string Телефон { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Журнал> Журнал { get; set; }
    }
}
