$(document).ready(function () {
    sessionStorage.clear();
    DefinirValoresSession();
});


function DefinirValoresSession() {
    const valores = window.location.search;
    const urlParameters = new URLSearchParams(valores);
    var datosURL = {
        nIdUsuario = urlParameters.get('nIdUsuario')
    }
    
}