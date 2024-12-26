using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzaApp.DomainModels.Entites;
using PizzaApp.DTOs.UserDtos;
using PizzaApp.Services.UserServices.Interfaces;
using PizzaApp.Shared.CustomExceptions.UserExceptions;
using PizzaApp.Shared.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace PizzaApp.Services.UserServices.Implementations
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public UserService(IMapper mapper, UserManager<User> userManager, ITokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<CustomResponse> DeleteUserAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    return new CustomResponse("User not found!");
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                    return new CustomResponse(result.Errors.Select(x => x.Description));
                return new CustomResponse(true);
            }
            catch (UserDataException ex)
            {

                throw new UserDataException(ex.Message);
            }
        }

        public async Task<CustomResponse<List<UserDto>>> GetAllUsersAsync()
        {
            try
            {
                var response = new CustomResponse<List<UserDto>>();
                var users = await _userManager.Users.ToListAsync();
                var userDtos = users.Select(User => _mapper.Map<UserDto>(User)).ToList();
                response.Result = userDtos;
                response.IsSuccessfull = true;
                return response;

            }
            catch (UserDataException ex)
            {

                throw new UserDataException(ex.Message);
            }
            
        }

        public async Task<CustomResponse<UserDto>> GetUsersByIdAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if(user == null) 
                    return new CustomResponse<UserDto>("User not found");
                var userDto = _mapper.Map<UserDto>(user);
                return new CustomResponse<UserDto>(userDto);
            }
            catch (UserDataException ex)
            {

                throw new UserDataException(ex.Message);
            }
        }

        public async Task<CustomResponse<LoginUserResponseDto>> LoginUserAsync(LoginUserRequestDto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                    throw new UserDataException("Username and password are required!");

                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null)
                    return new("User does NOT exist");
                bool isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!isPasswordValid)
                    return new("Password does not match!");
                var token = await _tokenService.GenerateTokenAsync(user);

                return new CustomResponse<LoginUserResponseDto>(
                    new LoginUserResponseDto
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        ValidTo = token.ValidTo
                    }
                );
            }
            catch (UserDataException ex)
            {
                throw new UserDataException(ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                throw new UserNotFoundException(ex.Message);
            }
        }

        public async Task<CustomResponse<RegisterUserResponseDto>> RegisterUserAsync(RegisterUserRequestDto request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Username))
                    throw new UserDataException("Username must not be empty!");

                if (string.IsNullOrEmpty(request.Password))
                    throw new UserDataException("Password must not be empty!");
                if (string.IsNullOrEmpty(request.Email))
                    throw new UserDataException("Email must not be empty!");

                var user = new UserDto { Email = request.Email, UserName = request.Username };
                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                    return new(result.Errors.Select(x => x.Description));

                return new(new RegisterUserResponseDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                });
            }
            catch (UserDataException ex)
            {
                throw new UserDataException(ex.Message);
            }
        }

        public async Task<CustomResponse<UpdateUserDto>> UpdateUserAsync(string id, UpdateUserDto updatedUser)
        {
            try
            {
                var user =await _userManager.FindByIdAsync(id);
                if (user == null)
                    return new CustomResponse<UpdateUserDto>("User not found!");
                var result = await _userManager.UpdateAsync(user);
                if(!result.Succeeded)
                    return new CustomResponse<UpdateUserDto>(result.Errors.Select(x => x.Description));
                _mapper.Map(updatedUser, user);
                return new CustomResponse<UpdateUserDto>(updatedUser);

            }
            catch (UserDataException ex)
            {
                throw new UserDataException(ex.Message);
            }
        }
    }
}
