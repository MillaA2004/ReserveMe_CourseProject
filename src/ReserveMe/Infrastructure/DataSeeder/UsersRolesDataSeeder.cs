namespace Infrastructure.DataSeeder
{
	using Common;
	using Microsoft.AspNetCore.Identity;

	public class UsersRolesDataSeeder
	{
		public static async Task<List<IdentityRole>> SeedData(
		  ApplicationDbContext context,
		  RoleManager<IdentityRole> roleManager,
		  CancellationToken cancellationToken)
		{
			var roleNames = new[]
			{
				UserRoles.ADMINISTRATOR_ROLE,
				UserRoles.OWNER_ROLE,
				UserRoles.WAITER_ROLE,
				UserRoles.CLIENT_ROLE
			};

			foreach (var role in roleNames)
			{
				if (!await roleManager.RoleExistsAsync(role))
				{
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}

			var roles = roleManager.Roles.ToList();

			return roles;
		}
	}
}
