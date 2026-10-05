using DulcesPro.DTOs;

namespace DulcesPro.Service
{
    public interface IRecipeIngredientService : IDulcesService<RecipeIngredientDTO, RecipeIngredientPostDTO, RecipeIngredientUpdateDTO>
    {
        public Task<IEnumerable<IngredientInRecipeDTO>> GetIngredientInRecipe(int id);

        public Task<RecipeCostDTO?> GetRecipeCost(int id);
        public Task<IEnumerable<RecipeDTO?>> GetRecipesWhithIngredient(int id);

        public Task<RecipeNecessaryDTO?> GetRecipeNecessary(int id, int portions);

        public Task<IEnumerable<IngredientNecessaryDTO>?> GetIngredientsNecessarys(int id, int portions);
    }
}
