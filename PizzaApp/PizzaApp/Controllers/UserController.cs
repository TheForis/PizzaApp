﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.DTOs.UserDtos;
using PizzaApp.Services.UserServices.Interfaces;
using PizzaApp.Shared.CustomExceptions.UserExceptions;

namespace PizzaApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserRequestDto model)
        {
            try
            {
                var request = new RegisterUserRequestDto
                {
                    Email = model.Email,
                    Username = model.Username,
                    Password = model.Password,
                };
                var response = await _userService.RegisterUserAsync(request);
                return Ok(Response(response));
            }
            catch (UserDataException ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserRequestDto model)
        {
            try
            {
                var response = await _userService.LoginUserAsync(model);
                if (response.IsSuccessfull)
                    return Ok(response);
                return BadRequest(response.Errors.ToString());
            }
            catch (UserDataException ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var response = await _userService.GetAllUsersAsync();
                if (response.IsSuccessfull)
                    return Ok(response);
                return BadRequest(response.Errors.ToString());
            }
            catch (UserDataException ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var response = await _userService.GetUsersByIdAsync(id);
                if (response.IsSuccessfull)
                    return Ok(response);
                return NotFound(response.Errors.ToString());
            }
            catch (UserDataException ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto updatedUser)
        {
            try
            {
                var response = await _userService.UpdateUserAsync(id, updatedUser);
                if (response.IsSuccessfull)
                    return Ok(response);
                return BadRequest(response.Errors.ToString());
            }
            catch (UserDataException ex)
            {
                return BadRequest(ex.Message);

            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser (string id)
        {
            try
            {
                var response = await _userService.DeleteUserAsync(id);
                if (!response.IsSuccessfull)
                    return NotFound(response.Errors.ToString());
                return Ok(response);
            }
            catch (UserDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
