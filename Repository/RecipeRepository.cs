using DulcesPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DulcesPro.Repository
{
    public class RecipeRepository : IDulcesRepository<Recipe>
    {
        private readonly DulcesContext _context;
        public RecipeRepository(DulcesContext context)
        {
            _context = context;
        }
        public void Delete(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
        }

        public async Task<IEnumerable<Recipe>> Get() =>
            await _context.Recipes.ToListAsync();




        public async Task<Recipe?> GetByID(int id)
       => await _context.Recipes.FindAsync(id);

        public async Task Insert(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Recipe>> Search(Expression<Func<Recipe, bool>> filter)=>
           await _context.Recipes.Where(filter).ToListAsync();
        
            
        

        public void Update(Recipe recipe)
        {
            _context.Recipes.Attach(recipe);
            _context.Entry(recipe).State = EntityState.Modified;
        }
    }
}
