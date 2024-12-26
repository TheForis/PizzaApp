using AutoMapper;
using PizzaApp.DataAccess.Repositories.Interfaces;
using PizzaApp.DomainModels.Entites;
using PizzaApp.DTOs.OrderDtos;
using PizzaApp.Services.OrderServices.Interfaces;
using PizzaApp.Shared.CustomExceptions.OrderExceptions;
using PizzaApp.Shared.Responses;

namespace PizzaApp.Services.OrderServices.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<CustomResponse<OrderDto>> CreateOrder(string userId, AddOrderDto createOrderDto)
        {
            try
            {
                var order = _mapper.Map<Order>(createOrderDto);
                order.UserId = userId;
                await _orderRepository.Add(order);
                var orderDtoResult = _mapper.Map<OrderDto>(order);
                return new CustomResponse<OrderDto>(orderDtoResult);
            }
            catch (Exception ex)
            {

                throw new OrderDataException($"Unexpected error while creating the order {ex.Message}");
            }
        }

        public async Task<CustomResponse> DeleteOrder(string userId, int id)
        {
            try
            {
                var order = await _orderRepository.GetByIdInt(id);
                if(order is null)
                    throw new OrderNotFoundException("There is no order with that id");
                if (order.UserId != userId)
                    return new CustomResponse("You dont have permission to delete this order");
                await _orderRepository.Delete(order);
                return new CustomResponse(true);
            }
            catch (Exception ex)
            {

                throw new OrderDataException($"Unexpected error while deleting the order {ex.Message}");
            }
        }

        public async Task<CustomResponse<List<OrderDto>>> GetAllOrders()
        {
            try
            {
                var result = await _orderRepository.GetAll();
                if (result.Count < 1)
                    throw new OrderDataException("There are no orders");
                List<OrderDto> orders= _mapper.Map<List<OrderDto>>(result);
                return new CustomResponse<List<OrderDto>>(orders);
            }
            catch (Exception ex)
            {

                throw new OrderDataException($"Unexpected error while getting the orders {ex.Message}");
            }
        }

        public async Task<CustomResponse<OrderDto>> GetOrderById(int id)
        {
            try
            {
                var order = await _orderRepository.GetByIdInt(id);
                if (order is null)
                    throw new OrderNotFoundException("There is no order with that id");
                var orderResult = _mapper.Map<OrderDto>(order);
                return new CustomResponse<OrderDto>(orderResult);
            }
            catch (Exception ex)
            {

                throw new OrderDataException($"Unexpected error while getting the order. {ex.Message}");
            }
        }

        public async Task<CustomResponse<OrderDto>> UpdateOrder(string userId, int orderId, UpdateOrderDto updateOrderDto)
        {
            try
            {
                var order = await _orderRepository.GetByIdInt(orderId);
                if (order is null)
                    throw new OrderNotFoundException("There is no order with that id");
                if (order.UserId != userId)
                    return new CustomResponse<OrderDto>("You dont have permission to delete this order");
                var updatedMapperOrder = _mapper.Map(updateOrderDto, order);
                updatedMapperOrder.UserId = userId;
                await _orderRepository.Update(updatedMapperOrder);
                var mappedOrder = _mapper.Map<OrderDto>(order);
                return new CustomResponse<OrderDto>(mappedOrder);
            }
            catch (Exception ex)
            {

                throw new OrderDataException($"Unexpected error while updating the order {ex.Message}");
            }
        }
    }
}
