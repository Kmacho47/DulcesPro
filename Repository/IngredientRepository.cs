using DulcesPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DulcesPro.Repository
{
    public class IngredientRepository : IDulcesRepository<Ingredient>
    {
        private readonly DulcesContext _context;
        public IngredientRepository(DulcesContext context)
        {
            _context = context;
        }
        public void Delete(Ingredient ingredient)
        {
            _context.Ingredients.Remove(ingredient);
        }

        public async Task<IEnumerable<Ingredient>> Get()=>
            await _context.Ingredients.ToListAsync();




        public async Task<Ingredient?> GetByID(int id)
       => await _context.Ingredients.FindAsync(id);

        public async Task Insert(Ingredient ingredient)
        {
           await _context.Ingredients.AddAsync(ingredient);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Ingredient ingredient)
        {
            _context.Ingredients.Attach(ingredient);
            _context.Entry(ingredient).State = EntityState.Modified;
        }

        public async Task <IEnumerable<Ingredient>> Search(Expression<Func<Ingredient, bool>> filter)=>
       await _context.Ingredients.Where(filter).ToListAsync();
        
        
    }


}
