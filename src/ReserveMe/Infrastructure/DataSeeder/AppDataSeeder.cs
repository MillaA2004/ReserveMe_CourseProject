namespace Infrastructure.DataSeeder
{
	using Application.Helpers;
	using Common;
	using Domain.Entities;
	using Microsoft.AspNetCore.Identity;

	public class AppDataSeeder
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		string _defaultPassword = "ReserveMe2@25";

		string _envPath = string.Empty;
		string _storagePath = "Storage";

		public AppDataSeeder(
			ApplicationDbContext context,
			UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager,
			string envPath)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
			_envPath = envPath;
		}

		public async Task SeedAllAsync(CancellationToken cancellationToken)
		{
			// 1. Seed App User Roles
			var roles = await UsersRolesDataSeeder.SeedData(_context, _roleManager, cancellationToken);
			await SeedAdministratorsAsync(cancellationToken);
			await SeedAppUsersAsync(cancellationToken);
		}


		private async Task SeedAdministratorsAsync(CancellationToken cancellationToken)
		{
			try
			{
				var profileStoragePath = Path.Combine(_envPath, _storagePath);

				// Default Admin
				var defaultAdmin = new ApplicationUser()
				{
					FirstName = "Admin",
					LastName = $"User",
					UserName = $"superadmin@reserveme.com",
					Email = $"superadmin@reserveme.com",
					EmailConfirmed = true
				};
				defaultAdmin.ProfilePicture = Task.Run(() => RandomFaceGenerator.GetRandomFaceAsync(profileStoragePath)).Result;
				var defaultResult = await _userManager.CreateAsync(defaultAdmin, _defaultPassword);
				if (defaultResult.Succeeded)
				{
					await _userManager.AddToRoleAsync(defaultAdmin, UserRoles.ADMINISTRATOR_ROLE);
				}
			}
			catch (Exception ex)
			{ }
		}

		private async Task SeedAppUsersAsync(CancellationToken cancellationToken)
		{
			try
			{
				var profileStoragePath = Path.Combine(_envPath, _storagePath);

				// Add 3 App Users
				for (int y = 0; y < 3; y++)
				{
					var waiter = new ApplicationUser()
					{
						FirstName = "Ivan",
						LastName = $"Ivanov {y.ToString("D2")}",
						UserName = $"user{y.ToString("D2")}@local.com",
						Email = $"user{y.ToString("D2")}@local.com",
						EmailConfirmed = true
					};
					waiter.ProfilePicture = Task.Run(() => RandomFaceGenerator.GetRandomFaceAsync(profileStoragePath)).Result;
					var waiterResult = await _userManager.CreateAsync(waiter, _defaultPassword);
					if (waiterResult.Succeeded)
					{
						await _userManager.AddToRoleAsync(waiter, UserRoles.CLIENT_ROLE);
					}
				}
			}
			catch (Exception ex)
			{ }
		}
	}
}
