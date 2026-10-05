namespace DulcesPro.DTOs
{
    public class RecipeIngredientUpdateDTO
    {
        
        public decimal Quantity { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
    }
}
