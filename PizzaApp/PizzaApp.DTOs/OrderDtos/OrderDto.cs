﻿

using PizzaApp.DTOs.PizzaDtos;

namespace PizzaApp.DTOs.OrderDtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string AddressTo { get; set; }
        public string? Description { get; set; }
        public int OrderPrice { get; set; }
        public List<PizzaDto>? Pizzas { get; set; }
    }
}
