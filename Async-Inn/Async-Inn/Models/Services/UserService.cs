using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Async_Inn.Models.Services
{
	public class UserService : IUser
	{

		private readonly UserManager<User> _userManager;
        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<UserDTO> Login(string password, string email)
		{
			var user= await _userManager.FindByEmailAsync(email);
			bool checkPassword= await _userManager.CheckPasswordAsync(user, password);
			if (checkPassword) {
				return new UserDTO() { 
				Id= user.Id,
				Username= user.UserName
				};
			}
			return null;
		}

		public async Task<UserDTO> Registration(RegisterUserDTO registrationDTO, ModelStateDictionary modelState)
		{
			var user = new User()
			{
				UserName = registrationDTO.Username,
				Email = registrationDTO.Email,	
				PhoneNumber=registrationDTO.PhoneNumber,
			   
			};

			var result = await _userManager.CreateAsync(user, registrationDTO.Password);

			if (result.Succeeded) {
				return new UserDTO()
				{
					Id= user.Id,
					Username=user.UserName
				};
			}
			foreach (var error in result.Errors)
			{
				var errorKey = error.Code.Contains("Password") ? nameof(registrationDTO.Password) :
						 error.Code.Contains("Email") ? nameof(registrationDTO.Email) :
						 error.Code.Contains("Username") ? nameof(registrationDTO.Username) :
						 "";

				modelState.AddModelError(errorKey, error.Description);
			}
			return null;
		}
	}
}
