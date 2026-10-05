using Microsoft.EntityFrameworkCore;

namespace DulcesPro.Models
{
    public class DulcesContext : DbContext
    {
        public DulcesContext(DbContextOptions<DulcesContext> dulces) 
        : base(dulces)
        {}
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(x => x.Recipe)
                .WithMany()
                .HasForeignKey(x => x.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RecipeIngredient>()
               .HasOne(x => x.Ingredient)
               .WithMany()
               .HasForeignKey(x => x.IngredientId);
        }
    }
}
