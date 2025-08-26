using System.Net.Http.Json;
using System.Reflection;
using ExamenesApp_Modelos.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace ExamenesApp.Servicios
{
    public class InscripcionExamenService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public InscripcionExamenService(HttpClient http, IJSRuntime js, AuthenticationStateProvider authenticationStateProvider)
        {
            _http = http;
            _js = js;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<InscripcionExamenDTO?> RegistrarInscripcion(int id)
        {
            var response = await _http.PostAsJsonAsync("InscripcionExamenes/Inscripcion", id);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<InscripcionExamenDTO>();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return null;
            }
            {

                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error {response.StatusCode}: {error}");
            }
        }
        public async Task<List<InscripcionExamenDTO>> ObtenerInscripcionesAsync()
        {
            try
            {
                var inscripciones = await _http.GetFromJsonAsync<List<InscripcionExamenDTO>>("InscripcionExamenes/inscripciones");

                if (inscripciones == null)
                {
                    return new List<InscripcionExamenDTO>();
                }

                return inscripciones;
            }
            catch (HttpRequestException ex)
            {
                
                Console.WriteLine($"Error al obtener inscripciones: {ex.Message}");
                return new List<InscripcionExamenDTO>();
            }
        }
        public async Task<bool> EliminarInscripcion(int id)
        {
            var response = await _http.DeleteAsync($"InscripcionExamenes/eliminar/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false; // No se encontró la inscripción
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error {response.StatusCode}: {error}");
            }
        }
    }
}
