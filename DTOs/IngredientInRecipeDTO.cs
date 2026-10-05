namespace DulcesPro.DTOs
{
    public class IngredientInRecipeDTO
    {
        public string Name { get; set; } = null!;
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }

        public string Unit { get; set; } = null!;
    }
}
