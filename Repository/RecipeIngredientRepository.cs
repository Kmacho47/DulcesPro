using DulcesPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DulcesPro.Repository
{
    public class RecipeIngredientRepository : IDulcesRepository<RecipeIngredient>

    {

        public readonly DulcesContext _context;
        public RecipeIngredientRepository(DulcesContext context)
        {
            _context = context;
        }
        public  void Delete(RecipeIngredient tdulces)
        {
            _context.RecipeIngredients.Remove(tdulces);
        }

        public async Task<IEnumerable<RecipeIngredient>> Get()=>
           await _context.RecipeIngredients.ToListAsync();
        
           
        

        public async Task<RecipeIngredient?> GetByID(int id)=>
            await _context.RecipeIngredients.FindAsync(id);
        
            
        

        public async Task Insert(RecipeIngredient tdulces)=>
         await _context.RecipeIngredients.AddAsync(tdulces);
        

        public async Task Save()=>
            await _context.SaveChangesAsync();
        

      

        public  void Update(RecipeIngredient tdulces)
        {
            _context.Attach(tdulces);
            _context.Entry(tdulces).State = EntityState.Modified;
        }
             
       

        public async Task<IEnumerable<RecipeIngredient>> Search(Expression<Func<RecipeIngredient, bool>> filter)=>
        
            await _context.RecipeIngredients.Where(filter).ToListAsync();
        
    }
}
