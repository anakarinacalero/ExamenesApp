namespace ExamenesApp.Modelos
{
    public class ViewModels
    {
        //public class RegistroUsuarioViewModel
        //{
           
        //    public string Email { get; set; } = string.Empty;
        //    public string Password { get; set; } = string.Empty;
        //    public string ConfirmPassword { get; set; } = string.Empty;

        //}

        public class LoginUsuarioViewModel
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class LoginResponseDTO
        {
            public string Mensaje { get; set; }
            public string Token { get; set; }
        }
        public class ErrorDTO
        {
            public string Mensaje { get; set; }
        }
        

    }
}
