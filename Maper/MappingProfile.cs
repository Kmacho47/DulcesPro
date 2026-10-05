using AutoMapper;
using DulcesPro.DTOs;
using DulcesPro.Models;

namespace DulcesPro.Maper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
        
        CreateMap<Ingredient, IngredientDTO>();
        CreateMap<IngredietUpdateDTO, Ingredient>();
        CreateMap<IngredientPostDTO, Ingredient>();


            CreateMap<Recipe, RecipeDTO>();
            CreateMap<RecipeUpdateDTO, Recipe>();
            CreateMap<RecipePostDTO, Recipe>();

            CreateMap<RecipeIngredientPostDTO, RecipeIngredient>();
            CreateMap<RecipeIngredientUpdateDTO, RecipeIngredient>();
            CreateMap<RecipeIngredient, RecipeIngredientDTO>();





        }
    }
}
