using Microsoft.EntityFrameworkCore;
using MyToDoApi.Entity;

namespace MyToDoApi.Context
{
    public class MyToDoDBContext:DbContext
    {
        private readonly DbContextOptions<MyToDoDBContext> options;
        public MyToDoDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; private set; }//不要忘了写set，否则拿到的DbContext的Categories为null
        public DbSet<Memo> Memos { get; private set; }
        public DbSet<ToDo> ToDos { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
