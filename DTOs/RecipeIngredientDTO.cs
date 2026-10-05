namespace DulcesPro.DTOs
{
    public class RecipeIngredientDTO
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public decimal Quantity { get; set; }
    }
}
