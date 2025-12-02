using Domain.Entities;
using Infrastructure;
using Infrastructure.DataSeeder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
	.AddIdentity<ApplicationUser, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	try
	{
		var appDbContext = services.GetRequiredService<ApplicationDbContext>();
		appDbContext.Database.Migrate();

		var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
		if (roleManager is null) throw new NullReferenceException(nameof(RoleManager<IdentityRole>));
		var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
		if (userManager is null) throw new NullReferenceException(nameof(UserManager<ApplicationUser>));

		var env = app.Services.GetRequiredService<IWebHostEnvironment>();

		var seedAppData = new AppDataSeeder(appDbContext, userManager, roleManager, env.ContentRootPath);
		await seedAppData.SeedAllAsync(CancellationToken.None);
	}
	catch (Exception ex)
	{
		var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
		logger.LogError(ex, "An error occurred during database initialisation.");

		throw;
	}
}

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();