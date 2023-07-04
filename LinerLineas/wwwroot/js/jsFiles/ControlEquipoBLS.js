//Variables para la paginación
var nIdFuncionPaginacion = 2;
var apiTotalRegistros = '/ControlEquipo/ObtenerNumeroTotalBLS';


$(document).ready(function () {
    reloadPageCache()
    document.getElementById('txt_Bandera_Menu').value = '1';
    recargarTablaPorTiempo();
    obtenerTotalRegistros();
});

function recargarTablaPorTiempo() {
    setInterval(cargarBLSDesperfecto, 60000);
}


function cargarBLSDesperfecto() {
    let sURL = '/ControlEquipo/GetBLS';
    var linea = sessionStorage.getItem('linea') == null ? null : sessionStorage.getItem('linea');
    var oDatos = {
            sTexto: "",
            sFechaInicial_Desde: "",
            sFechaFinal_Hasta: "",
            nLinea: linea,
            rPAGINACION: {
                nPaginaActual: paginaActualVGlobal,
                nRegistrosPorPagina: registrosPorPagina
            }
    }
    var sBusqueda = document.getElementById('txtBusqueda').value;
    sBusqueda = sBusqueda.replace(/\s/g, '');

    if (sBusqueda.length >= 3) {
        oDatos.sTexto = sBusqueda;
        bFiltros = true;
    }
    else {
        oDatos.sTexto = null;
    }

    $.ajax({
        url: sURL,
        type: 'POST',
        data: oDatos,
        beforeSend: function () {
            document.getElementById('dvSpinner').classList.remove('invisibilidad');
        },
        success: function (result) {
            if (result.objects != null) {
                $('#tbContenedores').empty();
                $.each(result.objects, function (i, obj) {
                    tr = $('<tr/>');

                    tr.append("<td class='centrarColumna'>" + obj.rBL_I.sNUM_BL + "</td>")
                    tr.append("<td class='centrarColumna' hidden>" + obj.rREFERENCIAS.nIdReferencia + "</td>")
                    tr.append("<td class='centrarColumna'>" + obj.rREFERENCIAS.sReferencia + "</td>")
                    tr.append("<td class='centrarColumna' hidden>" + obj.rCLIENTES.nFIIDCLIETE + "</td>")
                    tr.append("<td class='centrarColumna'>" + obj.rCLIENTES.sFSCLIENTE + "</td>")
                    tr.append("<td class='centrarColumna' hidden>" + obj.rLINEAS.dNUM_LINEA + "</td>")
                    tr.append("<td class='centrarColumna'>" + obj.rLINEAS.sNOM_LINEA + "</td>")
                    tr.append("<td class='centrarColumna'>" + obj.nNUM_ITINEI + "</td>")
                    tr.append("<td class='centrarColumna'>" + obj.rCAMPOS_EXTRA.sESTATUS + "</td>")

                    tr.append("<td class='centrarColumna'>$" + convertirFormatoMoneda(obj.rREFERENCIAS.dTipoCambio) + "</td>")
                    tr.append("<td class='centrarColumna'>$" + convertirFormatoMoneda(obj.rREFERENCIAS.dMontoMXN) + "</td>")
                    tr.append("<td class='centrarColumna'>$" + convertirFormatoMoneda(obj.rREFERENCIAS.dMontoUSD) + "</td>")
                    if (obj.rREMESAS_AX.daFDFECHA_DEPOSITO == null)
                        tr.append("<td class='centrarColumna' hidden>SIN DEPOSITO</td>")
                    else
                        tr.append("<td class='centrarColumna' hidden>" + formatoFecha(obj.rREMESAS_AX.daFDFECHA_DEPOSITO) + "</td>")

                    tr.append("<td class='centrarColumna'><div type='submit' id='btnMostrarContenedores'  class='btnMostrarContenedores' onclick =\"AbrirVistaContenedoresByBL('" + obj.rBL_I.sNUM_BL + "'," + obj.rREFERENCIAS.nIdReferencia + ",'" + obj.rREFERENCIAS.sReferencia + "'," + obj.nNUM_ITINEI + ")\" style='cursor: pointer'><i class='fa fa-share' aria-hidden='true'></i></div></td>");
                    $('#tbContenedores').append(tr.addClass("punteroTabla"));
                });
            }
        },
        error: function (xhr, textStatus, errorThrow) {
            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
        },
        complete: function () {
            document.getElementById('dvSpinner').classList.add('invisibilidad');
        }
    });
}

function AbrirVistaContenedoresByBL(sBL, nIdReferencia, sReferencia, nItinerario) {
    location.href = URLGetContenedores + "?sBL=" + sBL + "&nIdReferencia=" + nIdReferencia + "&sReferencia=" + sReferencia + "&nItinerario=" + nItinerario;
}


function error(labelName, txtName) {
    $('#' + labelName).text("");
    $('#' + txtName).removeClass('border-danger');
}

/*Terminan las funciones*/


/*Eventos*/
document.querySelector('#txtBusqueda').addEventListener('keyup', function () {
    var sBusqueda = document.getElementById('txtBusqueda').value;
    sBusqueda = sBusqueda.replace(/\s/g, '');
    if (sBusqueda.length >= 3 || sBusqueda.length == 0) {
        paginaActualVGlobal = 1;
        cargarBLSDesperfecto()
    }
});

///---------Funciones para recargar el captcha de una pagina automaticamente----------------
function reloadPageCache() {
    //Declarar un variable en cache para comprobar si se ha recargado anteriormente
    var reloading = sessionStorage.getItem("reloading");

    //Si reloading no tiene valor, recargamos la página
    if (!reloading) {
        realoadPage();
    }
}

function realoadPage() {
    //Recargamos la pagina actual y borramos el cache
    document.location.reload();

    //Guardamos una variable para indicar que ya se recargo la página y no volver a recargar
    sessionStorage.setItem("reloading", "true");
}



/*var URLGetContenedores = "http://localhost:8153/ControlEquipo/GetContenedores";*/

//-------Variables para paginacion-----//
//var totalRegistros = 0;
//var paginaActualVGlobal = 0;
//var registrosPorPagina = 10;
//----------------------------------------//


//Funcion para la paginacion
//function agregarPaginacion(paginaActual) {
//    paginaActualVGlobal = paginaActual;
//    cargarBLSDesperfecto();

//    var integral = Math.ceil(parseFloat(totalRegistros / registrosPorPagina));
//    var cantidadPaginas = parseInt(integral, 10);

//    //Parametros para definir el inicio, final y numero de paginas a mostrar
//    var inicial = 1;
//    var radio = 3;
//    var cantidadMaximaDePaginas = radio * 2 + 1;
//    var final = cantidadPaginas > cantidadMaximaDePaginas ? cantidadMaximaDePaginas : cantidadPaginas;
//    if (paginaActual > radio + 1) {
//        inicial = paginaActual - radio;
//        if (cantidadPaginas > paginaActual + radio) {
//            final = paginaActual + radio;
//        }
//        else {
//            final = cantidadPaginas;
//        }
//    }

//    //Insersion de las listas para agregar los botones de direccionamiento de paginas
//    $('#listaPaginacion').empty();
//    $("#listaPaginacion").append('<li class="btnIzquierdo" onclick =\"agregarPaginacion(' + 1 + ')\"><i class="fa fa-angle-double-left" aria-hidden="true"></i></li>');
//    for (var i = inicial; i <= final; i++) {
//        if (i == paginaActual) {
//            $("#listaPaginacion").append('<li class="paginaActual" onclick =\"agregarPaginacion(' + i + ')\"><span>' + i + '</span></li>');
//        }
//        else {
//            $("#listaPaginacion").append('<li class = "opcionesPaginacion" onclick =\"agregarPaginacion(' + i + ')\"><span>' + i + '</span></li>');
//        }
//    }
//    $("#listaPaginacion").append('<li class="btnDerecho" onclick =\"agregarPaginacion(' + cantidadPaginas + ')\"><i class="fa fa-angle-double-right" aria-hidden="true"></i></li>');
//}

//function obtenerTotalRegistros() {
//    var linea = sessionStorage.getItem('linea') == null ? null : sessionStorage.getItem('linea');
//    var oReferencia = {
//        nLinea: linea
//    };

//    $.ajax({
//        url: '/ControlEquipo/ObtenerNumeroTotalBLS',
//        type: 'POST',
//        data: oReferencia,
//        beforeSend: function () {
//            document.getElementById('dvSpinner').classList.remove('invisibilidad');
//        },
//        success: function (result) {
//            totalRegistros = result;
//            agregarPaginacion(1);
//        },
//        error: function (xhr, textStatus, errorThrow) {
//            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
//        },
//        complete: function () {
//            //document.getElementById('dvSpinner').classList.add('invisibilidad');
//        }
//    });
//}

//document.querySelector('#txtDesde').addEventListener('change', () => {
//    cargarBLSDesperfecto()
//});

//document.querySelector('#txtHasta').addEventListener('change', () => {
//    cargarBLSDesperfecto()
//});

/*Terminan Eventos*/