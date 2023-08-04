using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.Services;
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

			builder.Services.AddTransient<IHotel, HotelService>();
			builder.Services.AddTransient<IRoom, RoomService>();
			builder.Services.AddTransient<IAmenity, AmenityService>();
			builder.Services.AddTransient<IHotelRoom, HotelRoomService>();

			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
				{
					Title = "Async Inn API",
					Version = "v1",
				});
			});

			var app = builder.Build();

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