namespace ReserveMe.Pages.Venues
{
	using Microsoft.AspNetCore.Components;
	using Microsoft.AspNetCore.Components.Authorization;
	using Microsoft.AspNetCore.Components.Forms;
	using Microsoft.JSInterop;
	using Shared.Dtos.Venues;
	using Shared.Dtos.VenueTypes;
	using Shared.Services.Media;
	using Shared.Services.Venues;
	using Shared.Services.VenueTypes;

	public partial class VenuesPage
	{
		[Inject] private IVenuesService _venuesService { get; set; } = null!;
		[Inject] private IVenueTypesService _venueTypesService { get; set; } = null!;
		[Inject] private IMediaService _mediaService { get; set; } = null!;
		[Inject] private IJSRuntime jsRuntime { get; set; }
		[Inject] private AuthenticationStateProvider authStateProvider { get; set; } = null!;

		//string SelectedImage { get; set; } = "../assets/images/emptyImg.jpg";
		string SelectedLogo { get; set; } = "../assets/images/emptyImg.jpg";
		string SelectedVenueImage { get; set; } = "../assets/images/emptyImg.jpg";

		private string env = "https://localhost:7000";

		private List<VenueAdminDto> venues = new List<VenueAdminDto>();

		private List<VenueTypeDto> venueTypes = new List<VenueTypeDto>();

		private IBrowserFile? selectedFile;
		const long MaxAllowedSize = 10 * 1024 * 1024;

		private VenueCreateDto venueDto = new();
		private EditContext venueEditContext = default!;
		private bool isSubmitting;

		protected override async Task OnInitializedAsync()
		{
			venueEditContext = new EditContext(venueDto);
			var authState = await authStateProvider.GetAuthenticationStateAsync();

			this.venues = await this._venuesService.GetVenues();
			this.venueTypes = await this._venueTypesService.GetAllAsync();
		}

		private string? GetVenueOwner(int menuId)
		{
			//TODO: Get the real owner
			return "Ivan Ivanov";
		}

		private async Task CreateVenue()
		{
			if (isSubmitting) return;
			isSubmitting = true;

			try
			{
				await _venuesService.CreateVenueAsync(venueDto);

				venues = await _venuesService.GetVenues();

				await jsRuntime.InvokeVoidAsync("clickModalClose", "edit-item");

				ResetVenueForm();
			}
			finally
			{
				isSubmitting = false;
			}
		}

		private async Task OnLogoSelected(InputFileChangeEventArgs e)
		{
			var file = e.File;
			if (file != null)
			{
				try
				{
					var result = await _mediaService.UploadImage(file);

					if (result != null && !string.IsNullOrEmpty(result.SavePath))
					{
						venueDto.LogoUrl = result.SavePath;
					}
				}
				catch (Exception ex)
				{ }
			}
		}

		private async Task OnImageSelected(InputFileChangeEventArgs e)
		{
			var file = e.File;
			if (file != null)
			{
				try
				{
					var result = await _mediaService.UploadImage(file);

					if (result != null && !string.IsNullOrEmpty(result.SavePath))
					{
						venueDto.ImageUrl = result.SavePath;
					}
				}
				catch (Exception ex)
				{ }
			}
		}

		//async Task OnFileSelected(InputFileChangeEventArgs e)
		//{
		//	selectedFile = e.File;
		//	using var ms = new MemoryStream();
		//	await selectedFile.OpenReadStream(MaxAllowedSize).CopyToAsync(ms);
		//	var originalDataUrl =
		//	  $"data:{selectedFile.ContentType};base64,{Convert.ToBase64String(ms.ToArray())}";

		//	SelectedImage = originalDataUrl;
		//}

		private async Task OnLogoFileSelected(InputFileChangeEventArgs e)
		{
			var file = e.File;
			if (file is null) return;

			SelectedLogo = await ToDataUrl(file);

			var result = await _mediaService.UploadImage(file);
			if (result != null && !string.IsNullOrEmpty(result.SavePath))
				venueDto.LogoUrl = result.SavePath;
		}

		private async Task OnVenueImageFileSelected(InputFileChangeEventArgs e)
		{
			var file = e.File;
			if (file is null) return;

			SelectedVenueImage = await ToDataUrl(file);

			var result = await _mediaService.UploadImage(file);
			if (result != null && !string.IsNullOrEmpty(result.SavePath))
				venueDto.ImageUrl = result.SavePath;
		}

		private async Task<string> ToDataUrl(IBrowserFile file)
		{
			using var ms = new MemoryStream();
			await file.OpenReadStream(MaxAllowedSize).CopyToAsync(ms);
			return $"data:{file.ContentType};base64,{Convert.ToBase64String(ms.ToArray())}";
		}

		private void OnCancelVenue()
		{
			ResetVenueForm();
			//SelectedImage = "../assets/images/emptyImg.jpg";
			SelectedLogo = "../assets/images/emptyImg.jpg";
			SelectedVenueImage = "../assets/images/emptyImg.jpg";
		}

		private void ResetVenueForm()
		{
			venueDto = new VenueCreateDto();
			venueEditContext = new EditContext(venueDto);
			StateHasChanged();
		}
	}
}
