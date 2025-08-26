namespace ExamenesApp_backend.Servicios
{
    public class ExcepcionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExcepcionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Ocurrió un error inesperado.");
            }
        }
    }
}
