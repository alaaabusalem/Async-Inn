using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllers().AddNewtonsoftJson(options =>
	   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
	 );

			string connString = builder.Configuration.GetConnectionString("DefaultConnection");

			builder.Services
				.AddDbContext<AsyncInnDbContext>
				(opions => opions.UseSqlServer(connString));
			builder.Services.AddIdentity<User, IdentityRole>(options => {
			options.User.RequireUniqueEmail = true;	
			}).AddEntityFrameworkStores<AsyncInnDbContext>();

			builder.Services.AddTransient<IUser, UserService>();
			builder.Services.AddTransient<IHotel, HotelService>();
			builder.Services.AddTransient<IRoom, RoomService>();
			builder.Services.AddTransient<IAmenity, AmenityService>();
			builder.Services.AddTransient<IHotelRoom, HotelRoomService>();
			builder.Services.AddScoped<JWTService>();

			builder.Services.AddAuthentication(options =>
			{
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				// Tell the authenticaion scheme "how/where" to validate the token + secret
				options.TokenValidationParameters = JWTService.GetValidationPerameters(builder.Configuration);
			});

			builder.Services.AddAuthorization(options =>
			{
				options.AddPolicy("create", policy => policy.RequireClaim("permissions", "create"));
				options.AddPolicy("update", policy => policy.RequireClaim("permissions", "update"));
				options.AddPolicy("delete", policy => policy.RequireClaim("permissions", "delete"));
				options.AddPolicy("read", policy => policy.RequireClaim("permissions", "read"));

			});

			builder.Services.AddAuthorization();


			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
				{
					Title = "Async Inn API",
					Version = "v1",
				});
			});

			var app = builder.Build();
			app.UseAuthentication();
			app.UseAuthorization();			

			app.UseSwagger(options =>
			{
				options.RouteTemplate = "/api/{documentName}/swagger.json";
			});

			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/api/v1/swagger.json", "Async Inn API");
				options.RoutePrefix = "docs";
			});
			app.MapControllers();

			app.MapGet("/", () => "Hello World!");

			app.Run();
		}
	}
}