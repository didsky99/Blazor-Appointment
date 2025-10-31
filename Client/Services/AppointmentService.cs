using System.Net.Http.Json;
using BlazorAppointmentSystem.Shared.Models;

namespace BlazorAppointmentSystem.Client.Services
{
    public class AppointmentService
    {
        private readonly HttpClient _httpClient;

        public AppointmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AppointmentViewModel>> GetAppointmentsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<AppointmentViewModel>>("api/TxAppointment/GetAll");
            return response ?? new List<AppointmentViewModel>();
        }

        public async Task<List<AppointmentViewModel>> GetAppointmentListAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<AppointmentViewModel>>("api/TxAppointment/GetAppointmentList");
            return response ?? new List<AppointmentViewModel>();
        }

        public async Task<TxAppointment> CreateAppointmentAsync(TxAppointment model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/TxAppointment/Create", model);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TxAppointment>() ?? new TxAppointment();
        }

        public async Task<TxAppointment> UpdateAppointmentAsync(AppointmentViewModel model)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/TxAppointment/Update/{model.Id}", model);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TxAppointment>() ?? new TxAppointment();
        }


        public async Task<TxAppointment> CancelAppointmentAsync(int appointmentId)
        {
            var response = await _httpClient.PostAsJsonAsync(
                $"api/TxAppointment/ChangeStatus/{appointmentId}",
                "CAN"); // just the status string
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TxAppointment>() ?? new TxAppointment();
        }


    }
}
