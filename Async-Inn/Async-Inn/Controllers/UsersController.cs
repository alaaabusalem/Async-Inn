using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Async_Inn.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{

		private IUser userService;
		public UsersController(IUser service)
		{
			userService = service;
		}

		[HttpPost("Register")]
		public async Task<ActionResult<UserDTO>> Registration(RegisterUserDTO registerUserDTO)
		{
           var user= await userService.Registration(registerUserDTO,this.ModelState);
			
			if (user != null) {
				  return user;
			}
			return BadRequest(new ValidationProblemDetails(ModelState));

		}

		[HttpPost("Login")]
		public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
		{
			var user = await userService.Login(loginDto.Password, loginDto.Email);

			if (user == null)
			{
				return Unauthorized();
			}
			return user;
		}
	}
}
