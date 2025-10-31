using System.Net.Http.Json;
using BlazorAppointmentSystem.Shared.Models;

namespace BlazorAppointmentSystem.Client.Services
{
    public class CompanyService
    {
        private readonly HttpClient _httpClient;

        public CompanyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TbCompany>> GetCompaniesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<TbCompany>>("api/TbCompany/GetAll");
            return response ?? new List<TbCompany>();
        }
    }
}
