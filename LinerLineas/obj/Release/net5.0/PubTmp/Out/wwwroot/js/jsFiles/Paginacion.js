//-------Variables para paginacion-----//
var totalRegistros = 0;
var paginaActualVGlobal = 0;
var registrosPorPagina = 50;
//----------------------------------------//



//Funcion para la paginacion
function agregarPaginacion(paginaActual) {
    paginaActualVGlobal = paginaActual;

    if (nIdFuncionPaginacion == 1) {
        ObtenerReferencias();
    }
    else if (nIdFuncionPaginacion == 2) {
        cargarBLSDesperfecto();
    }
    else if (nIdFuncionPaginacion == 3) {
        ObtenerReferencias();
    }
    //ObtenerReferencias();

    var integral = Math.ceil(parseFloat(totalRegistros / registrosPorPagina));
    var cantidadPaginas = parseInt(integral, 10);

    //Parametros para definir el inicio, final y numero de paginas a mostrar
    var inicial = 1;
    var radio = 3;
    var cantidadMaximaDePaginas = radio * 2 + 1;
    var final = cantidadPaginas > cantidadMaximaDePaginas ? cantidadMaximaDePaginas : cantidadPaginas;
    if (paginaActual > radio + 1) {
        inicial = paginaActual - radio;
        if (cantidadPaginas > paginaActual + radio) {
            final = paginaActual + radio;
        }
        else {
            final = cantidadPaginas;
        }
    }

    //Insersion de las listas para agregar los botones de direccionamiento de paginas
    $('#listaPaginacion').empty();
    $("#listaPaginacion").append('<li class="btnIzquierdo" onclick =\"agregarPaginacion(' + 1 + ')\"><i class="fa fa-angle-double-left" aria-hidden="true"></i></li>');
    for (var i = inicial; i <= final; i++) {
        if (i == paginaActual) {
            $("#listaPaginacion").append('<li class="paginaActual" onclick =\"agregarPaginacion(' + i + ')\"><span>' + i + '</span></li>');
        }
        else {
            $("#listaPaginacion").append('<li class = "opcionesPaginacion" onclick =\"agregarPaginacion(' + i + ')\"><span>' + i + '</span></li>');
        }
    }
    $("#listaPaginacion").append('<li class="btnDerecho" onclick =\"agregarPaginacion(' + cantidadPaginas + ')\"><i class="fa fa-angle-double-right" aria-hidden="true"></i></li>');
}


function obtenerTotalRegistros() {
    var idCliente = sessionStorage.getItem('idCliente') == null ? null : sessionStorage.getItem('idCliente');

    var oReferencia = {
        rASPNET_USERS: {
            sId: sessionStorage.getItem('idUsuario')
        },
        rCLIENTE: {
            nFIIDCLIETE: idCliente
        }
    };

    if (nIdFuncionPaginacion == 2) {
        oReferencia = {
             nLinea: sessionStorage.getItem('linea')
        };
    }

    $.ajax({
        url: apiTotalRegistros,
        type: 'POST',
        data: oReferencia,
        beforeSend: function () {
            document.getElementById('dvSpinner').classList.remove('invisibilidad');
        },
        success: function (result) {
            totalRegistros = result;
            agregarPaginacion(1);
            
        },
        error: function (xhr, textStatus, errorThrow) {
            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
        }
    });
}