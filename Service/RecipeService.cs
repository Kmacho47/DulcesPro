using DulcesPro.DTOs;
using Microsoft.EntityFrameworkCore;
using DulcesPro.Controllers;
using DulcesPro.Models;
using Microsoft.AspNetCore.Mvc;
using DulcesPro.Repository;
using AutoMapper;

namespace DulcesPro.Service
{
    public class RecipeService : IDulcesService<RecipeDTO, RecipePostDTO, RecipeUpdateDTO>
    {
        
        private readonly IDulcesRepository<Recipe> _repository; 
        private readonly IMapper _mapper;
        private readonly IDulcesRepository<RecipeIngredient> _recipeIngredientRepository;
        public List<string> Errors { get; } 
       public RecipeService(IDulcesRepository<Recipe> dulcesRepository,
           IMapper mapper,
           IDulcesRepository<RecipeIngredient> dulcesRepository1) { 
            
            _repository = dulcesRepository;
            _mapper = mapper;
            Errors = new List<string>();
            _recipeIngredientRepository = dulcesRepository1;
        }
        
        public async Task<IEnumerable<RecipeDTO>> Get()
        {
            var recipes = await _repository.Get();
            return recipes.Select(x => _mapper.Map<RecipeDTO>(x));
            
        }
        
            
        

        public async Task<RecipeDTO?> GetById(int id)
        {

            var RecipeId = await _repository.GetByID(id);
            if (RecipeId == null) { return null; }
            var RecipeDto = _mapper.Map<RecipeDTO>(RecipeId);
            return RecipeDto;
        }


        

       

        public async Task<RecipeDTO> Insert(RecipePostDTO dto)
        {
            var recipe = _mapper.Map<Recipe>(dto);

            await _repository.Insert(recipe);
            await _repository.Save();
            var recipeDto = _mapper.Map<RecipeDTO>(recipe);
            return recipeDto;
        }

        public async Task<RecipeDTO?> Update(int id, RecipeUpdateDTO dto)
        {
            var recipe = await _repository.GetByID(id);
            if (recipe == null) { return null; };

            _mapper.Map(dto, recipe);
            
            _repository.Update(recipe);
            await _repository.Save();
            var recipeDto = _mapper.Map<RecipeDTO>(recipe);
            return recipeDto;

        }

        public async Task<bool> Delete(int id)
        {
            var recipe = await _repository.GetByID(id);
            if (recipe == null) { return false; }
            
            

            _repository.Delete(recipe);
            await _repository.Save();
            return true;
        }

        public async Task<bool> ValidateUpdate(RecipeUpdateDTO dto, int id)
        {
            if ((await _repository.Search(x => x.Name == dto.Name && x.Id != id)).Any()) 
            {
                Errors.Add("La receta ya existe");
            }
            
            if(Errors.Any())return false;
            return true;
        }

        public async Task<bool> ValidateInsert(RecipePostDTO dto)
        {
            if((await _repository.Search(x=> x.Name == dto.Name)).Any()) 
            {
                Errors.Add("El nombre ya existe");
            }
            if(Errors.Any())return false;
            return true;
        }

        public Task<bool> ValidateDelete(int id) => Task.FromResult(true);

       
    }


}
