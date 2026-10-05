using DulcesPro.DTOs;
using DulcesPro.Models;
using DulcesPro.Service;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections;

namespace DulcesPro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientController : ControllerBase
    {
        
        private IValidator<IngredientPostDTO> _IngredientInsertValidator;
        private IValidator<IngredietUpdateDTO> _IngredietUpdateValidator;
        private readonly IDulcesService<IngredientDTO, IngredientPostDTO, IngredietUpdateDTO> _IngrdientService;
        public IngredientController(
            IValidator<IngredientPostDTO> validator, 
            IValidator<IngredietUpdateDTO> ingredietUpdateValidator,
            IDulcesService<IngredientDTO, IngredientPostDTO, IngredietUpdateDTO> ingrdientService)
        {
            
            _IngredientInsertValidator = validator;
            _IngredietUpdateValidator = ingredietUpdateValidator;
            _IngrdientService = ingrdientService;
        }


        [HttpGet]
        public async Task<IEnumerable<IngredientDTO>> Get()=>
            await _IngrdientService.Get();
           


        [HttpGet("{Id}")]
        public async Task <ActionResult<IngredientDTO>> GetById(int id)
        {

            var ingredientDto =await _IngrdientService.GetById(id);
            return ingredientDto == null ? NotFound() : Ok(ingredientDto);

            
        }

        [HttpPut("{Id}")]
        public async Task <ActionResult<IngredientDTO>> UpdateIngredient(int id, IngredietUpdateDTO dto)
        {
            var validationResult = await _IngredietUpdateValidator.ValidateAsync(dto);
            if(!validationResult.IsValid) {
            return BadRequest(validationResult.Errors);
            }
            if (!await _IngrdientService.ValidateUpdate(dto, id)) 
            { 
                return BadRequest(_IngrdientService.Errors); 
            }
           var ingredientDto = await _IngrdientService.Update(id, dto);
            if(ingredientDto == null) { return NotFound(); }
            return Ok(ingredientDto);
         
           

        }

        [HttpPost]
        public async Task<ActionResult<IngredientDTO>> InsertIngredient(IngredientPostDTO PostDto)
        {
           var validationResult =  await _IngredientInsertValidator.ValidateAsync(PostDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if  (! await _IngrdientService.ValidateInsert(PostDto)) 
            {
                 return BadRequest(_IngrdientService.Errors); 
            }
            
            var ingredientDto = await _IngrdientService.Insert(PostDto);
            return Ok(ingredientDto);

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteIngredient(int Id)
        {
            if (!await _IngrdientService.ValidateDelete(Id))
            {
                return BadRequest(_IngrdientService.Errors);
            }
            var testDelete = await _IngrdientService.Delete(Id);
            if(!testDelete) { return NotFound();}
           
            return NoContent();


        }

        



    }
}
