using AutoMapper;
using DulcesPro.DTOs;
using DulcesPro.Models;
using DulcesPro.Repository;
using FluentValidation;

namespace DulcesPro.Service
{
    public class RecipeIngredientService : IRecipeIngredientService
    {
        public List<string> Errors {  get;  }
        public readonly IDulcesRepository<RecipeIngredient> _repository;
        public readonly IMapper _mapper;
        public readonly IDulcesRepository<Ingredient> _ingredientRepository;
        public readonly IDulcesRepository<Recipe> _recipeRepository;
        

        public RecipeIngredientService(IDulcesRepository<RecipeIngredient> dulcesRepository,
            IMapper mapper,
            IDulcesRepository<Ingredient> ir,
            IDulcesRepository<Recipe> rr) 
        
        
        { 
        Errors = new List<string>();
            _repository = dulcesRepository;
            _mapper = mapper;
            _ingredientRepository = ir;
            _recipeRepository = rr;
        
        }
        
        
        public async Task<bool> Delete(int Id)
        {
            var recipeIngredient = await _repository.GetByID(Id);
            if(recipeIngredient == null) { return false; }
            _repository.Delete(recipeIngredient);
            await _repository.Save();
            return true;

        }

        public async Task<IEnumerable<RecipeIngredientDTO>> Get() 
        {
           var recipeIngredients = await _repository.Get();
            return recipeIngredients.Select(x => _mapper.Map<RecipeIngredientDTO>(x));
        }
            

        
            
        

        public async Task<RecipeIngredientDTO?> GetById(int id)
        {
            var recipeIngredient = await _repository.GetByID(id);
            if(recipeIngredient == null) { return null; }
            var riDTO = _mapper.Map<RecipeIngredientDTO>(recipeIngredient);
            return riDTO;
        }

        public async Task<RecipeIngredientDTO> Insert(RecipeIngredientPostDTO PostDto)
        {
            var ri =   _mapper.Map<RecipeIngredient>(PostDto);
            await _repository.Insert(ri);
            await _repository.Save();
            var riDTO= _mapper.Map<RecipeIngredientDTO>(ri);
            return riDTO;
        }

        public async Task<RecipeIngredientDTO?> Update(int id, RecipeIngredientUpdateDTO dto)
        {
            var ri = await _repository.GetByID(id);
            if(ri == null) { return null; };
            _mapper.Map(dto, ri); 
            _repository.Update(ri);
            await _repository.Save();
            var riDTO = _mapper.Map<RecipeIngredientDTO?>(ri);
            return riDTO;
        }

        

        public  Task<bool> ValidateDelete(int id)=> Task.FromResult(true);
        public async Task<bool> ValidateInsert(RecipeIngredientPostDTO dto)
        {
           //no agregar una relacion existente
            if((await _repository.Search(x=>x.IngredientId==dto.IngredientId && x.RecipeId == dto.RecipeId)).Any())
            {
                Errors.Add("El ingrediente ya pertenece a la receta");
            }
            
            //no agregar una relacion con un id incorrecto de recipe o ingredient
            var ingredientId = await _ingredientRepository.GetByID(dto.IngredientId);
            var recipeId = await _recipeRepository.GetByID(dto.RecipeId);
            if(ingredientId == null) { Errors.Add("El ingrediente no existe"); }
            if (recipeId == null) { Errors.Add("La receta no existe"); }
            
            
            if (Errors.Any()) return false;
            return true;
        }

        public async Task<bool> ValidateUpdate(RecipeIngredientUpdateDTO dto, int id)
        {
            //no cambiar a una relacion existente
            if ((await _repository.Search(x => x.IngredientId == dto.IngredientId && x.RecipeId == dto.RecipeId && x.Quantity == dto.Quantity)).Any())
            {
                Errors.Add("La relacion esta ya existe");
            }

            //no cambiar a una relacion con un id incorrecto de recipe o ingredient
            var ingredientId = await _ingredientRepository.GetByID(dto.IngredientId);
            var recipeId = await _recipeRepository.GetByID(dto.RecipeId);
            if (ingredientId == null) { Errors.Add("El ingrediente no existe"); }
            if (recipeId == null) { Errors.Add("La receta no existe"); }

            if (Errors.Any())return false;
            return true;
        }
    
    //FUNCIONES LOGICA DE NEGOCIO
    public async Task<IEnumerable<IngredientInRecipeDTO>> GetIngredientInRecipe(int id) 
        {


            var recipes = await _repository.Search(x => x.RecipeId == id);
            var ingredients= new List<IngredientInRecipeDTO>();
            foreach(var recipe in recipes) 
            {
                var ingredient = await _ingredientRepository.GetByID(recipe.IngredientId);
                if(ingredient != null)
                {
                    var dto = new IngredientInRecipeDTO
                    {
                        Id = ingredient.Id,
                        Name = ingredient.Name,
                        Unit = ingredient.Unit,
                        Quantity = recipe.Quantity,
                        Cost = ingredient.Price * recipe.Quantity

                    };
                    ingredients.Add(dto);
                    
                }
                
            }
            return ingredients;
            
                
                        
                
            
                
        
        }

        public async Task<RecipeCostDTO?> GetRecipeCost(int id)
        {
            var ingredients = await GetIngredientInRecipe(id);
            decimal cost = 0;
            foreach (var ingredient in ingredients) 
            {
                cost += ingredient.Cost;
            
            }
            var recipe = await _recipeRepository.GetByID(id);

            if (recipe == null) return null;
            
                var dto = new RecipeCostDTO { Cost = cost, Id = id, Name = recipe.Name };
            return dto;
            
            
        }

        public async Task<IEnumerable<RecipeDTO?>>GetRecipesWhithIngredient(int id)
        {
            
            var ri = await _repository.Search(x => x.IngredientId == id);
            if(ri == null)return null;
            List<RecipeDTO> recipeDTOs = new List<RecipeDTO>();
            foreach(var recipe in ri) 
            {
                   var i = (await _recipeRepository.GetByID(recipe.RecipeId));
                recipeDTOs.Add(_mapper.Map<RecipeDTO>(i));
            }
            return recipeDTOs;

        }

        public async Task<RecipeNecessaryDTO?> GetRecipeNecessary(int id, int portions)
        {
            var recipe = await _recipeRepository.GetByID(id);
            if (recipe == null) return null;
            var exactRecipes = (decimal)portions / recipe.Yield;
            var realsRecipes = (int)Math.Ceiling(exactRecipes);
            var cost = await GetRecipeCost(id);
            var exactCost = cost.Cost * exactRecipes;
            var realCost = cost.Cost * realsRecipes;
           var portionsRest = recipe.Yield * realsRecipes - portions;

            RecipeNecessaryDTO dto = new RecipeNecessaryDTO
            {
                RecipesNecessary = exactRecipes,
                RecipesReals = realsRecipes,
                PortionsRest = portionsRest,
                RealCost = realCost,
                ExactCost = exactCost

            };

            return dto;
            
        }

        public async Task<IEnumerable<IngredientNecessaryDTO>?> GetIngredientsNecessarys(int id, int portions)
        {
            var recipe = await _recipeRepository.GetByID(id);
            if (recipe == null) return null;
            var exactRecipes = (decimal)portions / recipe.Yield;
            var realsRecipes = (int)Math.Ceiling(exactRecipes);
            var ingredients = await GetIngredientInRecipe(id);
            var dtos = new List<IngredientNecessaryDTO>();
            foreach (var ingredient in ingredients)
            {
                var dto = new IngredientNecessaryDTO();
                dto.Id = ingredient.Id;
                dto.Name = ingredient.Name;
                dto.Cant = ingredient.Quantity * realsRecipes;
                dto.Cost = ingredient.Cost * realsRecipes;

                dtos.Add(dto);

            }

            
            return dtos;

        }
    }
}
