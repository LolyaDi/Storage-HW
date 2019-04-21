namespace StorageSystem.DataAccess
{
    using StorageSystem.Models;
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DataContext>());
        }
        
        public virtual DbSet<Toy> Toys { get; set; }
    }
}