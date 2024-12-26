using PizzaApp.DTOs.PizzaDtos;
using System.Text.Json.Serialization;

namespace PizzaApp.DTOs.OrderDtos
{
    public class UpdateOrderDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        public string AddressTo { get; set; }
        public string? Description { get; set; }
        public int OrderPrice { get; set; }
        public List<PizzaDto> Pizzas { get; set; }

    }
}
