using System.Net.Http.Headers;

namespace SalesService.Services.Implementations
{
    public class InventoryClient
    {
        private readonly HttpClient _http;

        public InventoryClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> DeductStock(Guid sparePartId, int qty, string token)
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.PostAsJsonAsync(
                "/revstock/api/inventory/stock-out",
                new { sparePartId, quantity = qty });

            return response.IsSuccessStatusCode;
        }
    }
}
