namespace DulcesPro.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Yield { get; set; }
    }
}
