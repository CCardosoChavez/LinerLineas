function formatoFecha(fecha) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(fecha);

    if (results !== null) {
        var dt = new Date(parseFloat(results[1]));
        return ("0" + dt.getDate()).slice(-2) + "/" + ("0" + (dt.getMonth() + 1)).slice(-2) + "/" + dt.getFullYear();
    } else {
        return fecha.substring(8, 10) + "/" + fecha.substring(5, 7) + "/" + fecha.substring(0, 4);
    }
}

function convertirFormatoMoneda(cantidad) {
    let cantidadFormateada = Number(cantidad).toLocaleString('en');
    let existe = cantidadFormateada.includes(".");
    if (!existe) {
        cantidadFormateada = cantidadFormateada + ".00"
    }
    return cantidadFormateada;
}