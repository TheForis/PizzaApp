using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.DomainModels.Entites;
using PizzaApp.DTOs.PizzaDtos;
using PizzaApp.Services.PizzaServices.Interfaces;
using PizzaApp.Shared.CustomExceptions.PizzaExceptions;
using PizzaApp.Shared.CustomExceptions.ServerExceptions;
using System.Security.Claims;

namespace PizzaApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController(IPizzaService pizzaService, UserManager<User> userManager) : BaseController
    {
        private readonly IPizzaService _pizzaService = pizzaService;
        private readonly UserManager<User> _userMenager = userManager;


        [HttpGet]
        public async Task<IActionResult> GetAllPizzas()
        {
            try
            {
                var result = await _pizzaService.GetAllPizzas();
                return Response(result);
            }
            catch (PizzaDataException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            { return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _pizzaService.GetPizzaById(id);
                return Response(response);
            }
            catch (PizzaDataException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            { return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); }
        }
        [HttpPost]
        public async Task<IActionResult> CreatePizza([FromBody] AddPizzaDto addPizzaDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                    return BadRequest("You don't have authoriation to do this action");
                var response = await _pizzaService.CreatePizza(userId, addPizzaDto);
                return Response(response);
            }
            catch (PizzaDataException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); 
            }
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePizza(int id, [FromBody] UpdatePizzaDto updatepizzaDto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                    return BadRequest("You don't have authoriation to do this action");
                var response = await _pizzaService.UpdatePizza(userId,id, updatepizzaDto);
                return Response(response);
            }
            catch (PizzaDataException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                    return BadRequest("You don't have authoriation to do this action");
                var response = await _pizzaService.DeletePizza(userId, id);
                return Response(response);
            }
            catch (PizzaDataException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
