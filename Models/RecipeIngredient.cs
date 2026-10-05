using System.ComponentModel.DataAnnotations.Schema;

namespace DulcesPro.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }
        
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public decimal Quantity { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; } = null!;
        [ForeignKey("IngredientId")]
        public Ingredient Ingredient { get; set; } = null!;
       

    }
}
