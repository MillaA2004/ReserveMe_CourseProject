namespace ReserveMe.Pages.Venues
{
	using Microsoft.AspNetCore.Components;
	using Microsoft.JSInterop;
	using Microsoft.Extensions.Configuration;
	using Shared.Dtos.Reviews;
	using Shared.Dtos.Users;
	using Shared.Dtos.Venues;
	using Shared.Requests.Venues;
	using Shared.Helpers;
	using Shared.Services.Users;
	using Shared.Services.Venues;

	public partial class MyVenue
	{
		[Parameter] public int AdminVenueId { get; set; } = -1;
		[Inject] private IVenuesService _venuesService { get; set; } = null!;
		[Inject] private IAuthenticationHelper _authHelper { get; set; } = null!;
		[Inject] private IUserService _userService { get; set; } = null!;
		[Inject] private NavigationManager? navManager { get; set; } = null!;
		[Inject] private IJSRuntime JS { get; set; } = null!;
		[Inject] private IConfiguration Configuration { get; set; } = null!;

		public int VenueId { get; set; }

		private VenueDetailsDto? venue { get; set; }
		private List<ReviewDto> recentReviews { get; set; } = new();

		private List<UserDto> owners { get; set; } = new();
		private List<UserDto> waiters { get; set; } = new();

		// State for the confirm modal
		private bool isDeleteModalVisible = false;
		private string confirmTitle = string.Empty;
		private string confirmMessage = string.Empty;
		private string deleteWaiterId = string.Empty;

		private double? selectedLat;
		private double? selectedLng;

		protected override async Task OnInitializedAsync()
		{
			try
			{
				VenueId = await _authHelper.GetUserMenuId();

				if (AdminVenueId != -1 && AdminVenueId != 0)
					VenueId = AdminVenueId;

				if (VenueId == 0)
				{
					navManager.NavigateTo("/404", forceLoad: true);
					return;
				}

				this.venue = await this._venuesService.GetMyVenue(VenueId);
				this.owners = venue.Owners;
				this.waiters = venue.Waiters;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private async Task OpenEditLocationModal()
		{
			if (venue == null) return;

			selectedLat = venue.Latitude;
			selectedLng = venue.Longitude;

			var apiKey = Configuration["GoogleMaps:ApiKey"];

			await Task.Delay(500);

			await JS.InvokeVoidAsync("mapPicker.init", "map-picker-container", apiKey, selectedLat ?? 0, selectedLng ?? 0, DotNetObjectReference.Create(this));
		}

		[JSInvokable]
		public void OnMapClick(double lat, double lng)
		{
			selectedLat = lat;
			selectedLng = lng;
			StateHasChanged();
		}

		private async Task SaveLocation()
		{
			if (venue == null || !selectedLat.HasValue || !selectedLng.HasValue) return;

			var request = new SaveVenueRequest
			{
				Name = venue.Name,
				Description = venue.Description,
				VenueTypeId = venue.VenueTypeId,
				Latitude = selectedLat.Value,
				Longitude = selectedLng.Value,
				LogoUrl = venue.LogoUrl,
				ImageUrl = venue.ImageUrl
			};

			await _venuesService.UpdateVenueAsync(VenueId, request);

			venue.Latitude = selectedLat.Value;
			venue.Longitude = selectedLng.Value;

			StateHasChanged();
		}

		private void ShowRemoveWaiterModal(string email, string id)
		{
			confirmTitle = "Remove waiter";
			confirmMessage = $"Are you sure you want to remove waiter '{email}'?";
			deleteWaiterId = id;
			isDeleteModalVisible = true;
		}

		private void ConfirmRemoveOwner(string ownerId, string email)
		{
			throw new NotImplementedException();
			//this.title = "Remove Owner";
			//this.deleteMessage = $"Are you sure you want to remove owner '{email}'?";
			//this.deleteWaiterId = ownerId;
			//this.isDeleteModalVisible = true;
		}

		private async Task ConfirmDelete()
		{
			if (deleteWaiterId != string.Empty)
			{
				await RemoveWaiter(deleteWaiterId);
			}

			ResetDeleteData();
			isDeleteModalVisible = false;
		}

		private void CloseModal()
		{
			isDeleteModalVisible = false;
			ResetDeleteData();
		}

		private void ResetDeleteData()
		{
			deleteWaiterId = string.Empty;
			confirmTitle = string.Empty;
			confirmMessage = string.Empty;
		}

		private async Task RemoveWaiter(string waiterId)
		{
			var selected = waiters.FirstOrDefault(w => w.Id == waiterId);
			if (selected is null) return;

			waiters.Remove(selected);

			await _userService.ChangeWaiterMenu(waiterId, null);

			StateHasChanged();
		}

		private async Task ShowWeiterModal(string isCreateModal, string? waiterId = null)
		{
			throw new NotImplementedException();
		}

		private async Task ShowOwnerModal(string isUpdate, string? ownerId = null)
		{
			throw new NotImplementedException();
		}
		private async Task ShowAddOwnerModal()
		{
			throw new NotImplementedException();
			//await JS.InvokeVoidAsync("openAddOwnerOffcanvas");
		}
	}
}
