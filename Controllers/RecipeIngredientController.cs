
    using DulcesPro.DTOs;
using DulcesPro.Models;
using DulcesPro.Service;
using DulcesPro.Validetors;
using FluentValidation;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections;
namespace DulcesPro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeIngredientController : ControllerBase
    {
        private readonly IRecipeIngredientService _service;
        private readonly IValidator<RecipeIngredientPostDTO> _validatorI;
        private readonly IValidator<RecipeIngredientUpdateDTO> _validatorU;
        public RecipeIngredientController( IRecipeIngredientService service, IValidator<RecipeIngredientUpdateDTO> validatorU,
            IValidator<RecipeIngredientPostDTO> validatorI
           )
        {
            _service = service;
            _validatorU = validatorU;
            _validatorI = validatorI;

        }




        [HttpGet]
        public async Task<IEnumerable<RecipeIngredientDTO>> Get() =>
            await _service.Get();

        [HttpGet("{Id}")]
        public async Task<ActionResult<RecipeIngredientDTO>> GetById(int id)
        {
            var dto = await _service.GetById(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpGet("{id}/Ingredients")]
        public async Task<ActionResult<IEnumerable<IngredientInRecipeDTO>>>GetIngredientsInRecipe(int id)
            {
                var result = await _service.GetIngredientInRecipe(id);
            if(result == null)return NotFound();
            return Ok(result);
            }

        [HttpGet("{id}/Cost")]
        public async Task<ActionResult<RecipeCostDTO>> GetRecipeCost(int id) 
        {
        var dto = await (_service.GetRecipeCost(id));
            if(dto == null)return NotFound();
            return Ok(dto);
        
        }

        [HttpGet("{id}/Recipes")]
        public async Task<ActionResult<IEnumerable<RecipeDTO>>> GetRecipeWithIngredient(int id) 
        {
            var dto =await _service.GetRecipesWhithIngredient(id);
            if(dto == null)return NotFound();
            return Ok(dto);
           
        
        
        }

        [HttpGet("{id}/Production/{portions}")]
        public async Task<ActionResult<RecipeNecessaryDTO>> GetRecipeNecessary(int id, int portions) 
        {
            if(portions <= 0) return BadRequest("Las porciones deben ser postivas");
            var dto = await _service.GetRecipeNecessary(id, portions);
            if (dto == null) return NotFound();
            return Ok(dto);
        
        
        }

        [HttpGet("{id}/Ingredients/Production/{portions}")]
        public async Task<ActionResult<IEnumerable<IngredientNecessaryDTO>>> GetIngredientsNecessary(int id, int portions) 
        {
            if (portions <= 0) return BadRequest("Las porciones deben ser positivas");
            var dto = await _service.GetIngredientsNecessarys(id, portions);

            if (dto == null) return NotFound();
            return Ok(dto);






        }
        
        [HttpPost]
        public async Task<ActionResult<RecipeIngredientDTO>> Insert(RecipeIngredientPostDTO dto) 
        {
            var ri = await _validatorI.ValidateAsync(dto);
            if (!ri.IsValid) return BadRequest(_service.Errors);
            if(! await _service.ValidateInsert(dto))return BadRequest(ri.Errors);
           var validation = await _service.Insert(dto);
            return Ok(validation);
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RecipeIngredientDTO>> Udpate(RecipeIngredientUpdateDTO dto, int id) 
        {
            var fluentValidation = await _validatorU.ValidateAsync(dto);
            if(!fluentValidation.IsValid) return BadRequest(fluentValidation.Errors);
            if (!await _service.ValidateUpdate(dto, id)) return BadRequest(_service.Errors);
            var Dto = await _service.Update(id,dto);
            return Ok(Dto);

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<RecipeIngredientDTO>> Delete(int id) 
        {
            var testDelete = await _service.Delete(id);
            if(!testDelete)return NotFound();
            return NoContent();
        }
            

    
    }
}
