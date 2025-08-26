namespace ExamenesApp.Servicios
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using ExamenesApp_Modelos.DTOs;

    public class PerfilService
    {
        private readonly HttpClient _http;

        public PerfilService(HttpClient http)
        {
            _http = http;
        }

        public async Task<UsuarioDTO?> GuardarPerfilAsync(UsuarioDTO usuario)
        {
            var response = await _http.PostAsJsonAsync("PerfilUsuario/guardar", usuario);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UsuarioDTO>();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error {response.StatusCode}: {error}");
            }
        }

        public async Task<UsuarioDTO?> GetPerfilAsync()
        {
            try
            {
                var perfil = await _http.GetFromJsonAsync<UsuarioDTO>("PerfilUsuario/perfil");
                return perfil;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Perfil no existe
                return null;
            }
        }
    }

}
