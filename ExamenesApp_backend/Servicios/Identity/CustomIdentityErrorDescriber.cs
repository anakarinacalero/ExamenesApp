using Microsoft.AspNetCore.Identity;

namespace ExamenesApp_backend.Servicios.NewFolder
{
    public class CustomIdentityErrorDescriber:IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = $"La contraseña debe tener al menos {length} caracteres."
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "La contraseña debe contener al menos un carácter no alfanumérico (ej: @, #, $)."
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = "La contraseña debe contener al menos un número."
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description = "La contraseña debe contener al menos una letra minúscula."
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "La contraseña debe contener al menos una letra mayúscula."
            };
        }

        // Usuario
        

        // Email
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = $"El correo electrónico '{email}' ya está registrado."
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(InvalidEmail),
                Description = $"El correo electrónico '{email}' no es válido."
            };
        }

        // Otros errores comunes
        public override IdentityError UserAlreadyHasPassword()
        {
            return new IdentityError
            {
                Code = nameof(UserAlreadyHasPassword),
                Description = "El usuario ya tiene una contraseña asignada."
            };
        }

        public override IdentityError ConcurrencyFailure()
        {
            return new IdentityError
            {
                Code = nameof(ConcurrencyFailure),
                Description = "Falla de concurrencia, intenta nuevamente."
            };
        }

        public override IdentityError DefaultError()
        {
            return new IdentityError
            {
                Code = nameof(DefaultError),
                Description = "Ocurrió un error desconocido."
            };
        }
    }
}
