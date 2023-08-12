using System.Security.Claims;
using Async_Inn.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Async_Inn.Models.Interfaces
{
	public interface IUser
	{

		Task<UserDTO> Registration(RegisterUserDTO registrationDTO, ModelStateDictionary modelState);
		Task<UserDTO> Login(string password, string email);
		 Task<UserDTO> GetUser(ClaimsPrincipal principal);
	}
}
		

