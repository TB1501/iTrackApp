using Microsoft.EntityFrameworkCore;

using iTrack.Data;
using Microsoft.AspNetCore.Identity;
using iTrack.Services;

namespace iTrack
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorPages();
			builder.Services.AddServerSideBlazor();

			builder.Services.AddScoped<IClientServices, ClientServices>();
			
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContextFactory<DataContext>(options =>options.UseSqlServer(connectionString));

			builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequiredLength = 5;
				options.SignIn.RequireConfirmedEmail = false;
			}).AddEntityFrameworkStores<DataContext>();

			var app = builder.Build();

			using(var scope = app.Services.CreateScope())
			{
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
				dbContext.Database.EnsureCreated();
            }

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapBlazorHub();
			app.MapFallbackToPage("/_Host");

			app.Run();
		}
	}
}