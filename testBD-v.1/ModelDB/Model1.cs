using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace testBD_v._1.ModelDB
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=ModelKeyBTcontextTest")
        {
        }

        public virtual DbSet<Журнал> Журнал { get; set; }
        public virtual DbSet<Справочник_ключи> Справочник_ключи { get; set; }
        public virtual DbSet<Справочник_районы> Справочник_районы { get; set; }
        public virtual DbSet<Справочник_расоложение_АВ> Справочник_расоложение_АВ { get; set; }
        public virtual DbSet<Справочник_сотрдники> Справочник_сотрдники { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
