namespace ReserveMe.Pages.Account
{
	using Common;
	using Microsoft.AspNetCore.Components;
	using Shared.Dtos;
	using Shared.Helpers;

	public partial class Login
	{
		[Inject]
		private IAuthenticationHelper? _authHelper { get; set; }

		[Inject]
		private NavigationManager? navManager { get; set; }

		private LoginUserDto loginUser = new LoginUserDto();

		public async Task LoginUser()
		{
			var result = await _authHelper?.LoginAsync(loginUser)!;

			var isAdmin = await _authHelper.IsUserInRole(UserRoles.ADMINISTRATOR_ROLE);
			var isOwnerWaiter = await _authHelper.IsUserInRole(UserRoles.OWNER_ROLE) || await _authHelper.IsUserInRole(UserRoles.WAITER_ROLE);

			if (!string.IsNullOrEmpty(result))
			{
				if (isAdmin)
				{
					navManager?.NavigateTo("/admin/venues", forceLoad: true);
				}
				else if (isOwnerWaiter)
				{
					navManager?.NavigateTo("/live", forceLoad: true);
				}
				else
				{
					navManager?.NavigateTo("/", forceLoad: true);
				}
			}
			else
			{
				navManager?.NavigateTo("/401", forceLoad: true);
			}
		}
	}
}
