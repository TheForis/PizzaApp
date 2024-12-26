using PizzaApp.DomainModels.Enums;

namespace PizzaApp.DTOs.PizzaDtos
{
    public class AddPizzaDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public List<string> Ingredients { get; set; }
    }
}
