using PizzaApp.DomainModels.Enums;

namespace PizzaApp.DTOs.PizzaDtos
{
    public class PizzaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public string UserId { get; set; }
        public int? OrderId { get; set; }
        public List<string> Ingredients { get; set; }
    }
}
