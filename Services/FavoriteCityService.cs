using Blazored.LocalStorage;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace weather.Services
{
    public class FavoriteCityService
    {
        private const string FavoriteCitiesKey = "favoriteCities";
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public FavoriteCityService(ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        private async Task<string> GetUserKeyAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier); // Use unique user identifier (e.g., User ID or Email)
            return $"{FavoriteCitiesKey}_{userId}";
        }

        public async Task<List<string>> GetFavoriteCitiesAsync()
        {
            try
            {
                var userKey = await GetUserKeyAsync();
                var favorites = await _localStorage.GetItemAsync<List<string>>(userKey);
                return favorites ?? new List<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting favorite cities: {ex.Message}");
                return new List<string>();
            }
        }

        public async Task AddFavoriteCityAsync(string cityName)
        {
            var favorites = await GetFavoriteCitiesAsync();
            if (!favorites.Contains(cityName))
            {
                favorites.Add(cityName);
                var userKey = await GetUserKeyAsync();
                await _localStorage.SetItemAsync(userKey, favorites);
            }
        }

        public async Task RemoveFavoriteCityAsync(string cityName)
        {
            var favorites = await GetFavoriteCitiesAsync();
            if (favorites.Remove(cityName))
            {
                var userKey = await GetUserKeyAsync();
                await _localStorage.SetItemAsync(userKey, favorites);
            }
        }

        public async Task<bool> IsFavoriteCityAsync(string cityName)
        {
            var favorites = await GetFavoriteCitiesAsync();
            return favorites.Contains(cityName);
        }
    }
}