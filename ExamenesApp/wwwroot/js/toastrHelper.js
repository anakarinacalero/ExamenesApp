window.toastrHelper = {
    successAndRedirect: function (message, redirectUrl) {
        toastr.success(message);
        setTimeout(function () {
            window.location.href = redirectUrl;
        }, 2000); // espera 2 segundos antes de redirigir
    }
}
window.toastrNotificaciones = {
    success: function (mensaje, titulo) {
        toastr.success(mensaje, titulo, { preventDuplicates: true });
    },
    error: function (mensaje, titulo) {
        toastr.error(mensaje, titulo, { preventDuplicates: true });
    },
    warning: function (mensaje, titulo) {
        toastr.warning(mensaje, titulo, { preventDuplicates: true });
    },
    info: function (mensaje, titulo) {
        toastr.info(mensaje, titulo, { preventDuplicates: true });
    }
};


