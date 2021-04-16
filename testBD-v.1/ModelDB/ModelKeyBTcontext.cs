using System.Data.Entity;

namespace testBD_v._1.ModelDB
{
    public partial class ModelKeyBTcontext : DbContext
    {
        public ModelKeyBTcontext()
            : base("name=ModelKeyBD")
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
