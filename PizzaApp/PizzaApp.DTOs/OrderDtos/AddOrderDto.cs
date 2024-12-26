using PizzaApp.DTOs.PizzaDtos;

namespace PizzaApp.DTOs.OrderDtos
{
    public class AddOrderDto
    {
        public string AddressTo { get; set; }
        public string? Description { get; set; }
        public int OrderPrice { get; set; }
        public List<AddPizzaDto> Pizzas { get; set; }
    }
}
