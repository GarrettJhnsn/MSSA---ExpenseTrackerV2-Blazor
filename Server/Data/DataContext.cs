namespace MyApp.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemSet>().HasData(
                new ItemSet { Id = 1, Name = "Bill" },
                new ItemSet { Id = 2, Name = "Pleasure" },
                new ItemSet { Id = 3, Name = "Other" }
                );

            modelBuilder.Entity<Expense>().HasData(
                new Expense
                {
                    Id = 1,
                    Name = "Car",
                    Amount = 365,
                    ItemSetId = 1
                },
                new Expense
                {
                    Id=2,
                    Name = "Dine out",
                    Amount = 100,
                    ItemSetId = 2
                },
                new Expense
                {
                    Id=3,
                    Name = "Donation",
                    Amount = 20,
                    ItemSetId = 3
                }
                );
        }

        public DbSet<Expense> Expenses { get; set; }    
        public DbSet<ItemSet> ItemTypes { get; set; }  

    }
}
 