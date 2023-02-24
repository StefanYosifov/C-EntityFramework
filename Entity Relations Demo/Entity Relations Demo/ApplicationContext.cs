namespace Entity_Relations_Demo
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationContext:DbContext
    {

        public DbSet<Car> Cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=RelationsDemo;Integrated Security=TRUE");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

        }

    }
}
