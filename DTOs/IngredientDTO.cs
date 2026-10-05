namespace DulcesPro.DTOs
{
    public class IngredientDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }

        public string Unit { get; set; } = null!;
    }
}
