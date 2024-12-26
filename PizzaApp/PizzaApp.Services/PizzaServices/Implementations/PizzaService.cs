using AutoMapper;
using PizzaApp.DataAccess.Repositories.Interfaces;
using PizzaApp.DomainModels.Entites;
using PizzaApp.DTOs.PizzaDtos;
using PizzaApp.Services.PizzaServices.Interfaces;
using PizzaApp.Shared.CustomExceptions.PizzaExceptions;
using PizzaApp.Shared.Responses;

namespace PizzaApp.Services.PizzaServices.Implementations
{
    public class PizzaService : IPizzaService
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IMapper _mapper;
        public PizzaService(IPizzaRepository pizzaRepository, IMapper mapper)
        {
            _pizzaRepository = pizzaRepository;
            _mapper = mapper;
        }
        public async Task<CustomResponse<PizzaDto>> CreatePizza(string userId, AddPizzaDto addPizzaDto)
        {
            try
            {
                var pizza = _mapper.Map<Pizza>(addPizzaDto);
                pizza.UserId = userId;
                await _pizzaRepository.Add(pizza);
                var pizzaDto = _mapper.Map<PizzaDto>(pizza);
                return new CustomResponse<PizzaDto>(pizzaDto);
            }
            catch (PizzaDataException ex)
            {

                throw new PizzaDataException($"Unexpected error while adding new pizza. {ex.Message}") ;
            }
        }

        public async Task<CustomResponse> DeletePizza(string userId, int pizzaId)
        {
            try
            {
                var pizza = await _pizzaRepository.GetByIdInt(pizzaId);
                if (pizza == null)
                    return new CustomResponse("Pizza not found");
                if (pizza.UserId != userId)
                    return new CustomResponse("You do not have permission to delete this pizza");
                await _pizzaRepository.Delete(pizza);
                return new CustomResponse(true);
            }
            catch (PizzaDataException ex)
            {

                throw new PizzaDataException($"Unexpected error while deleting pizza. {ex.Message}");
            }
        }

        public async Task<CustomResponse<List<PizzaDto>>> GetAllPizzas()
        {
            try
            {
                var pizzas = await _pizzaRepository.GetAll();
                if (pizzas.Count < 1)
                    throw new PizzaDataException("No pizzas found");
                var pizzaDtos = _mapper.Map<List<PizzaDto>>(pizzas);
                return new CustomResponse<List<PizzaDto>>(pizzaDtos);

            }
            catch (PizzaDataException ex)
            {

                throw new PizzaDataException($"Unexpected error while deleting pizza. {ex.Message}");
            }
        }

        public async Task<CustomResponse<PizzaDto>> GetPizzaById(int id)
        {
            try
            {
                var pizza = await _pizzaRepository.GetByIdInt(id);
                if (pizza == null)
                    return new CustomResponse<PizzaDto>("Pizza not found");
                var pizzaDto = _mapper.Map<PizzaDto>(pizza);
                return new CustomResponse<PizzaDto>(pizzaDto);
            }
            catch (PizzaDataException ex)
            {

                throw new PizzaDataException($"Unexpected error while deleting pizza. {ex.Message}");
            }
        }

        public async Task<CustomResponse<PizzaDto>> UpdatePizza(string userId, int pizzaId, UpdatePizzaDto updatePizzaDto)
        {
            try
            {
                var pizza = await _pizzaRepository.GetByIdInt(pizzaId);
                if (pizza == null)
                    throw new PizzaDataException("Pizza not found");
                if (pizza.UserId != userId)
                    return new CustomResponse<PizzaDto>("You don't have authorization to update the pizza");
                _mapper.Map(updatePizzaDto, pizza);
                await _pizzaRepository.Update(pizza);
                var pizzaDtoResult = _mapper.Map<PizzaDto>(pizza);
                return new CustomResponse<PizzaDto>(pizzaDtoResult);
            }
            catch (PizzaDataException ex)
            {

                throw new PizzaDataException($"Unexpected error while deleting pizza. {ex.Message}");
            }
        }
    }
}
