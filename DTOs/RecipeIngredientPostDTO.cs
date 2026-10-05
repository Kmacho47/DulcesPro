namespace DulcesPro.DTOs
{
    public class RecipeIngredientPostDTO
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public decimal Quantity { get; set; }
    }
}
