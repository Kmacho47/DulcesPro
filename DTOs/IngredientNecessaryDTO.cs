namespace DulcesPro.DTOs
{
    public class IngredientNecessaryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Cant {  get; set; }
        public decimal Cost { get; set; }
    }
}
