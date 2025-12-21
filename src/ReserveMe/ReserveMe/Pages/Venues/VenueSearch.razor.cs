using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Shared.Dtos.Venues;
using Shared.Dtos.VenueTypes;
using Shared.Services.Venues;
using Shared.Services.VenueTypes;

namespace ReserveMe.Pages.Venues;

public partial class VenueSearch : ComponentBase
{
	[Inject] private IVenueTypesService _venueTypesService { get; set; } = null!;
	[Inject] private IVenuesService _venuesService { get; set; } = null!;

	// Location
	private string _currentLocation = "Sofia, Bulgaria";
	private double _userLatitude = 42.6977;
	private double _userLongitude = 23.3219;
	private int _selectedRadius = 5;

	// Search and filters
	private string _searchTerm = string.Empty;
	private SortOption _sortBy = SortOption.Rating;
	private HashSet<int> _selectedTypeIds = new();
	private bool _filterModalOpen = false;

	// Data
	private List<VenueSearchDto> _venues = new();
	private List<VenueTypeDto> _venueTypes = new();
	private int _pageSize = 6;
	private int _currentPage = 1;
	private int _totalCount = 0;
	private bool _hasMore = true;

	// UI State
	private bool _isLoading = false;
	private bool _isLoadingMore = false;
	private string? _errorMessage;

	// Filtered venues
	private IEnumerable<VenueSearchDto> _filteredVenues => _venues;

	protected override async Task OnInitializedAsync()
	{
		this._venues = await this._venuesService.GetVenuesForClient();
		this._venueTypes = await this._venueTypesService.GetAllAsync();
	}

	private async Task OnSearchChanged()
	{
		//TODO: Filter there
	}

	private async Task OnFilterChanged()
	{
		//TODO: Filter there
	}
	private void LoadMore()
	{
		//TODO: Filter there
	
		if (_isLoadingMore || !_hasMore) return;

		_isLoadingMore = true;
		StateHasChanged();

		try
		{
			throw new NotImplementedException();
			//_currentPage++;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
		finally
		{
			_isLoadingMore = false;
			StateHasChanged();
		}
	}

	private async Task ClearSearch()
	{
		_searchTerm = string.Empty;
		this._venues = await this._venuesService.GetVenuesForClient();
	}

	private void ToggleFilterModal()
	{
		_filterModalOpen = !_filterModalOpen;
	}

	private void ToggleTypeFilter(int typeId)
	{
		if (_selectedTypeIds.Contains(typeId))
			_selectedTypeIds.Remove(typeId);
		else
			_selectedTypeIds.Add(typeId);
		StateHasChanged();
	}

	private async void RemoveTypeFilter(int typeId)
	{
		_selectedTypeIds.Remove(typeId);
		this._venues = await this._venuesService.GetVenuesForClient();
	}

	private async Task ClearAllFilters()
	{
		_selectedTypeIds.Clear();
		_filterModalOpen = false;
		this._venues = await this._venuesService.GetVenuesForClient();
	}

	private async Task ApplyFilters()
	{
		_filterModalOpen = false;

		//TODO: Filter there
		this._venues = await this._venuesService.GetVenuesForClient();
	}

	private void ToggleFavorite(VenueSearchDto venue)
	{
		try
		{
			throw new NotImplementedException();
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	private string GetVenueTypeName(int? typeId)
	{
		if (!typeId.HasValue) return "Other";
		var type = _venueTypes.FirstOrDefault(t => t.Id == typeId);
		return type?.Name ?? "Other";
	}

	private static string GetVenueTypeIcon(int? typeId) => typeId switch
	{
		1 => "🍽️", // Restaurant
		2 => "🍸", // Bar
		3 => "☕", // Cafe
		4 => "🔥", // Grill
		5 => "🍕", // Pizzeria
		6 => "🍔", // Fast Food
		7 => "🍺", // Pub
		8 => "🥗", // Bistro
		_ => "📍"
	};
}

public enum SortOption
{
	Rating,
	Reservations
}