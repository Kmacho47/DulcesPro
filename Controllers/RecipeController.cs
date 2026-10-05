using DulcesPro.DTOs;
using DulcesPro.Models;
using DulcesPro.Service;
using DulcesPro.Validetors;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DulcesPro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        
        private IValidator<RecipePostDTO> _RecipeInsertValidator;
        private IValidator<RecipeUpdateDTO> _RecipeUpdateValidator;
        private readonly IDulcesService<RecipeDTO, RecipePostDTO, RecipeUpdateDTO> _recipeService;
        public RecipeController (IValidator<RecipeUpdateDTO> recipeUpdateValidator, 
            IValidator<RecipePostDTO> recipeInsertValidator,
            IDulcesService<RecipeDTO, RecipePostDTO, RecipeUpdateDTO> recipeService)
        {
             
            _RecipeUpdateValidator = recipeUpdateValidator;
            _RecipeInsertValidator = recipeInsertValidator;
            _recipeService = recipeService;
        }

        [HttpGet]

         public async Task<IEnumerable<RecipeDTO>> Get() =>
            await _recipeService.Get();
            
            
        

        [HttpGet("{Id}")]
        public async Task<ActionResult<RecipeDTO>> GetById(int id) {

            var RecipeDto= await _recipeService.GetById(id);
            return  RecipeDto == null ? NotFound() : Ok(RecipeDto) ;

        
        
        }

        [HttpPost]
        public async Task<ActionResult<RecipeDTO>> InsertRecipe(RecipePostDTO dto)
        {
            var validationResult = await _RecipeInsertValidator.ValidateAsync(dto);
            if (!validationResult.IsValid) 
            { 
                return BadRequest(validationResult.Errors); 
            }
            var serviceValidation = await _recipeService.ValidateInsert(dto);
            if (!serviceValidation) return BadRequest(_recipeService.Errors);
            var RecipeDto= await _recipeService.Insert(dto);
            return Ok(RecipeDto);



        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<RecipeDTO?>> UpdateRecipe(int id, RecipeUpdateDTO dto)
        {
            var validationResult = await _RecipeUpdateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var serviceValidation = await _recipeService.ValidateUpdate(dto, id);
            if(!serviceValidation)return BadRequest(_recipeService.Errors);

            var recipeDto = await _recipeService.Update(id, dto);
            return recipeDto == null ? NotFound() : Ok(recipeDto) ;
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteRecipe(int id)
        {
            var recipeBool = await _recipeService.Delete(id);
            if (!recipeBool) {  return NotFound(); }
            return NoContent();
        }

    
    
    
    
    }
}
