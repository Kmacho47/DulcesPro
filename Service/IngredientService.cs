using DulcesPro.DTOs;
using Microsoft.EntityFrameworkCore;
using DulcesPro.Controllers;
using DulcesPro.Models;
using Microsoft.AspNetCore.Mvc;
using DulcesPro.Repository;
using AutoMapper;
namespace DulcesPro.Service
{
    public class IngredientService : IDulcesService<IngredientDTO, IngredientPostDTO,IngredietUpdateDTO>
    {

        
        private readonly IDulcesRepository<Ingredient> _ingredientRepository;
        private readonly IDulcesRepository<RecipeIngredient> _recipeIngredientRepository;
        private readonly IMapper _mapper;
        public List<string> Errors { get; }
        public IngredientService(IDulcesRepository<Ingredient> ingredientRepository
            , IMapper mapper 
            , IDulcesRepository<RecipeIngredient> dulcesRepository)
        {
            
            _ingredientRepository= ingredientRepository;
            _mapper=mapper;
            Errors = new List<string>();
            _recipeIngredientRepository = dulcesRepository;
        }

        public async Task<IEnumerable<IngredientDTO>> Get()
        {
            var ingredients = await _ingredientRepository.Get();
            return ingredients.Select(b => _mapper.Map<IngredientDTO>(b));
        }    
                
         public async Task<IngredientDTO?> GetById(int id)
        {


            var ingredientID = await _ingredientRepository.GetByID(id);
            if (ingredientID != null)
            {

                var IngredientDto = _mapper.Map<IngredientDTO>(ingredientID);
                return IngredientDto;
            }
            return null;
        }



        public async Task<IngredientDTO?> Update(int id, IngredietUpdateDTO dto)
        {
            var IngredientID = await _ingredientRepository.GetByID(id);
            if (IngredientID == null)
            {
                return null;
            }


            _mapper.Map(dto, IngredientID);

            _ingredientRepository.Update(IngredientID);
            await _ingredientRepository.Save();

            var IngredientDto = _mapper.Map<IngredientDTO>(IngredientID);

            return IngredientDto;
        }

         public async Task<IngredientDTO> Insert(IngredientPostDTO PostDto)
        {
            var ingredient = _mapper.Map<Ingredient>(PostDto);

            await _ingredientRepository.Insert(ingredient);
            await _ingredientRepository.Save();
            var IngredientDto = _mapper.Map<IngredientDTO>(ingredient);
            return IngredientDto;
        }

       
        public async Task<bool> Delete(int Id)
        {
            var ingredient = await _ingredientRepository.GetByID(Id);
            if (ingredient == null)
            {
                return false;
            }
            _ingredientRepository.Delete(ingredient);
            await _ingredientRepository.Save();
            return true;
        }
        public async Task<bool> ValidateUpdate(IngredietUpdateDTO dto, int id)
        {
            if(( await _ingredientRepository.Search(x => x.Name == dto.Name 
            && x.Id != id)).Any())
            {
                Errors.Add("El nombre ya pertenece a otro ingrediente");
            }
            
            
            if(Errors.Any())return false;
            return true;
        }
       public async Task <bool> ValidateInsert(IngredientPostDTO dto) { 
             if(( await _ingredientRepository.Search(x => x.Name == dto.Name)).Any())
            {
                Errors.Add("El nombre ya pertenece a otro ingrediente");
            }

            if (Errors.Any()) return false;
            return true; 
        }

        public async Task<bool> ValidateDelete(int id)
        {


            if ((await _recipeIngredientRepository.Search(x => x.Id == id)).Any())
            {
                Errors.Add("No se puede eliminar este ingrediente porque es usado por una receta");
            }

            if (Errors.Any()) return false;
            return true;
        }

        
    }
}
