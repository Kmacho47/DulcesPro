using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DulcesPro.Models
{
    public class Ingredient
    {
        
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }

        public string Unit { get; set; } = null!;

    
    
    }
}
