namespace testBD_v._1.dbtest
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Журнал
    {
        public int id { get; set; }

        [Column("Код ключа")]
        public int Код_ключа { get; set; }

        [Column("Дата выдачи", TypeName = "date")]
        public DateTime Дата_выдачи { get; set; }

        [Column("Дата возврата", TypeName = "date")]
        public DateTime Дата_возврата { get; set; }

        [Column("Код сотрудника")]
        public int Код_сотрудника { get; set; }

        public bool Статус { get; set; }

        public string информация { get; set; }

        public virtual Справочник_ключи Справочник_ключи { get; set; }

        public virtual Справочник_сотрдники Справочник_сотрдники { get; set; }
    }
}
