using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using ExamenesApp;
using ExamenesApp_Modelos.DTOs;
using ExamenesApp_Modelos.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

public class AutenticacionService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _js;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AutenticacionService(HttpClient http, IJSRuntime js, AuthenticationStateProvider authenticationStateProvider)
    {
        _http = http;
        _js = js;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<LoginResponseDTO> Login(LoginUsuarioViewModel modelo)
    {
        var response = await _http.PostAsJsonAsync("Autenticacion/login", modelo);
        var loginResult = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

        if (loginResult != null && loginResult.Token != null)
        {
            // Guardar el token en localStorage para mantener sesión
            await _js.InvokeVoidAsync("localStorage.setItem", "authToken", loginResult.Token);

           
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", loginResult.Token);
        }
         ((CustomAuthenticationStateProvider)_authenticationStateProvider).NotifyUserAuthentication(loginResult.Token);
        return loginResult;
    }

    public async Task<RegistroResponse> Registro(RegistroUsuarioViewModel modelo)
    {
        var response = await _http.PostAsJsonAsync("Autenticacion/registro", modelo);
        var resultado = await response.Content.ReadFromJsonAsync<RegistroResponse>();
        await _js.InvokeVoidAsync("localStorage.setItem", "authToken", resultado.Token);
        _http.DefaultRequestHeaders.Authorization =
               new AuthenticationHeaderValue("Bearer", resultado.Token);
        ((CustomAuthenticationStateProvider)_authenticationStateProvider).NotifyUserAuthentication(resultado.Token);
        return resultado ?? new RegistroResponse { Exitoso = false, Mensaje = "Error desconocido" };
    }

}
