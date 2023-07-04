var botonEstatus = true;

//Variables para la paginación
var nIdFuncionPaginacion = 3;
var apiTotalRegistros = '/Referencias/ObtenerNumeroTotalReferencias';

$(document).ready(function () {
    document.getElementById('txt_Bandera_Menu').value = '1';
    recargarTablaPorTiempo();
    obtenerTotalRegistros();
    datosBancariosGuardados();
});



function recargarTablaPorTiempo() {
    setInterval(ObtenerReferencias, 300000);
}

//----------------------------------------------- Funciones para obtener los datos de las referencias que se mostraran en las tablas, así como tambien los contenedores de cada referencia ----------------------
function ObtenerReferencias() {
    var idCliente = sessionStorage.getItem('idCliente') == null ? null : sessionStorage.getItem('idCliente');

    var oReferencia = {
        sReferencia: "",
        rASPNET_USERS: {
            sId: sessionStorage.getItem('idUsuario')
        },
        rASPNET_ROLES: {
            sName: sessionStorage.getItem("rol")
        },
        rDATOS_BUSQUEDA: {
            sTexto: "",
            sFechaInicial_Desde: null,
            sFechaFinal_Hasta: null
        },
        rCLIENTE: {
            nFIIDCLIETE: idCliente
        },
        rPAGINACION: {
            nPaginaActual: paginaActualVGlobal,
            nRegistrosPorPagina: registrosPorPagina
        }
    };

    var sBusqueda = document.getElementById('txtBusqueda').value;
    sBusqueda = sBusqueda.replace(/\s/g, '');
    if (sBusqueda.length >= 3) {
        oReferencia.rDATOS_BUSQUEDA.sTexto = sBusqueda;

        bFiltros = true;
    }

    if (document.getElementById('txtDesde').value !== '') {
        oReferencia.rDATOS_BUSQUEDA.sFechaInicial_Desde = document.getElementById('txtDesde').value;
        bFiltros = true;
    }

    if (document.getElementById('txtHasta').value !== '') {
        oReferencia.rDATOS_BUSQUEDA.sFechaFinal_Hasta = document.getElementById('txtHasta').value;
    }

    $.ajax({
        url: '/Referencias/ObtenerReferencias',
        type: 'POST',
        data: oReferencia,
        beforeSend: function () {
            document.getElementById('txtBLGeneraReferencia').disabled = true;
            /*document.getElementById('btnGenerarReferencia').disabled = true;*/
            document.getElementById('txtBusqueda').disabled = true;
            document.getElementById('txtDesde').disabled = true;
            document.getElementById('txtHasta').disabled = true;
            document.getElementById('tReferencias').disabled = true;
            document.getElementById('dvSpinner').classList.remove('invisibilidad');
        },
        success: function (result) {
            if (result != null) {

                $('#tbReferencias').empty();
                $.each(result.objects, function (i, obj) {
                    tr = $('<tr/>');
                    tr.append("<td class='centrarColumna tdMostrarContenedores' onclick =\"mostrarContenedores('tr" + obj.sNUM_BL + "','" + obj.sNUM_BL + "'," + obj.nNUM_ITINE + ",'" + obj.sReferencia + "')\"><i class='fa fa-caret-down' aria-hidden='true'></i></td>")
                    tr.append("<td class='centrarColumna' hidden>" + obj.nIdReferencia + "</td>")
                    tr.append("<td class='centrarColumna' id='tdReferencia" + obj.nIdReferencia + "'>" + obj.sReferencia + "</td>")
                    tr.append("<td class='centrarColumna'>" + obj.sNUM_BL + "</td>")
                    tr.append("<td class='centrarColumna' hidden>" + obj.nNUM_ITINE + "</td>")
                    tr.append("<td class='centrarColumna'>$" + convertirFormatoMoneda(obj.dMontoMXN) + "</td>")
                    tr.append("<td class='centrarColumna'>$" + convertirFormatoMoneda(obj.dMontoUSD) + "</td>")
                    tr.append("<td class='centrarColumna'>" + formatoFecha(obj.daFechaRegistro) + "</td>")
                    tr.append("<td class='centrarColumna'>" + obj.rCAMPOS_EXTRA.sESTATUS_DEPOSITO + "</td>")

                    if (obj.rREMESAS_AX.daFDFECHA_DEPOSITO == null)
                        tr.append("<td class='centrarColumna'>SIN PAGO</td>");
                    else
                        tr.append("<td class='centrarColumna'>" + formatoFecha(obj.rREMESAS_AX.daFDFECHA_DEPOSITO) + "</td>");

                    tr.append("<td class='centrarColumna'>$" + convertirFormatoMoneda(obj.rCAMPOS_EXTRA.dMONTO_FALTANTE) + "</td>");

                    tr.append("<td class='centrarColumna'>" + obj.rLINEAS.sNOM_LINEA + "</td>")
                    tr.append("<td class='centrarColumna'>" + obj.rBUQUES.sNOM_BUQUE + "</td>")
                    tr.append("<td class='centrarColumna'>" + obj.rPUERTOS.sNOM_PTO + "</td>")

                    //Fecha y estatus de devolucion (Se ocultaron los campos)
                    if (obj.rCONTROL_DEVOLUCIONES.daFDDEVOLUCION == null)
                        tr.append("<td class='centrarColumna' hidden>SIN DEVOLUCION</td>")
                    else
                        tr.append("<td class='centrarColumna' hidden>" + formatoFecha(obj.rCONTROL_DEVOLUCIONES.daFDDEVOLUCION) + "</td>")

                    if (obj.rCAMPOS_EXTRA.nESTATUS_DEVOLUCION_DG == 0) {
                        tr.append("<td class='centrarColumna' hidden>SIN ESTATUS</td>")
                    } else if (obj.rCAMPOS_EXTRA.nESTATUS_DEVOLUCION_DG == 1) {
                        tr.append("<td class='centrarColumna' hidden>EN PROCESO</td>")
                    }
                    else {
                        tr.append("<td class='centrarColumna' hidden>DEVUELTO</td>")
                    }



                    //Se oculto el campo
                    if (obj.rITINERARIO.daF_ARRIVO_P == null)
                        tr.append("<td class='centrarColumna' hidden></td>");
                    else
                        tr.append("<td class='centrarColumna' hidden>" + formatoFecha(obj.rITINERARIO.daF_ARRIVO_P) + "</td>");

                    tr.append("<td class='centrarColumna'><div type='button' id='btnDescargarReferencia' class='btnCrearPdf' onclick =\"crearPDF(" + obj.nIdReferencia + ")\"><i class='fa fa-download' aria-hidden='true'></i></div></td>");

                    //Aparecera el boton para solicitar devolución
                    //Si los contenedores del BL no se ractifican como sin daño no se habilitará el boton
                    //Si uno de los contenedores del BL es marcado como con daño se habilita pero al presionarlo se 
                    //Si ya se marcaron todos los contenedores como sin daño validar si existe un adeudo en flete o demora


                    //El parametro obj.rCAMPOS_EXTRA.nESTATUS_DEVOLUCION_DG  permite saber si se realizó la devolución del DG si esta en proceso o pendiente
                    if (obj.rCONTROL_DEVOLUCIONES.daFDDEVOLUCION == null) {
                        //Si la solicitud de devolucion de DG con los datos Bancarios no fue rechazada por tesorería
                        if (obj.sEstatusDatosBancariosTesoreria == null) {
                            if (obj.nEstatusContenedoresControlEquipo != 0) {
                                tr.append("<td class='centrarColumna'><button type='button' id='btnSolicitarDevolucion' class='btnSolicitarDevolucion' onclick =\"validarAdeudosDelLaReferencia(" + obj.nEstatusContenedoresControlEquipo + "," + obj.nNUM_ITINE + ", '" + obj.sNUM_BL + "'," + obj.nIdReferencia + ",'" + obj.sReferencia + "'," + obj.nIdDatosBancarios + ")\">DEVOLUCIÓN</button></td>");
                            }
                            else {
                                tr.append("<td class='centrarColumna'>-</td>");
                               // tr.append("<td class='centrarColumna'><button type='button' id='btnSolicitarDevolucion' class='btnSolicitarDevolucion_Restringido' onclick =\"validarAdeudosDelLaReferencia(" + obj.nEstatusContenedoresControlEquipo + "," + obj.nNUM_ITINE + ", '" + obj.sNUM_BL + "'," + obj.nidReferencia + ",'" + obj.sReferencia + "'," + obj.nIdDatosBancarios + ")\"disabled>Devolución</button></td>");
                            }
                        }
                        else if (obj.sEstatusDatosBancariosTesoreria == "Rechazado") {
                            tr.append("<td class='centrarColumna'><button type='button' id='btnSolicitarDevolucion' class='btnSolicitarDevolucionActualizacion' title = '" + obj.rDatos_Bancarios_Referencia.sFSOBSERVACIONES+"' onclick =\"validarAdeudosDelLaReferencia(" + obj.nEstatusContenedoresControlEquipo + "," + obj.nNUM_ITINE + ", '" + obj.sNUM_BL + "'," + obj.nIdReferencia + ",'" + obj.sReferencia + "'," + obj.nIdDatosBancarios + ")\">DEVOLUCIÓN</button></td>");
                        }
                        else if (obj.sEstatusDatosBancariosTesoreria == "Aceptado") {
                            tr.append("<td class='centrarColumna'>ACEPTADO</td>");
                        }
                        else {
                            tr.append("<td class='centrarColumna'>PENDIENTE</td>");
                        }
                    }
                    else if (obj.rCAMPOS_EXTRA.nESTATUS_DEVOLUCION_DG == 2) {
                        tr.append("<td class='centrarColumna'>DEVUELTO <br> " + formatoFecha(obj.rCONTROL_DEVOLUCIONES.daFDDEVOLUCION) + "</td>");
                    }
                    else if (obj.rCAMPOS_EXTRA.nESTATUS_DEVOLUCION_DG == 1) {
                        tr.append("<td class='centrarColumna'>PENDIENTE</td>");
                    }

                    $('#tbReferencias').append(tr.addClass("punteroTabla"));

                    //Fila agregada para desplegar los contenedores
                    var filaPrueba = "<tr id='tr" + obj.sNUM_BL + "' hidden class='trContenedoresReferencias'><td colspan='12' id='td" + obj.sNUM_BL + "'></td></tr>"
                    $('#tbReferencias').append(filaPrueba);

                });
            }
        },
        error: function (xhr, textStatus, errorThrow) {
            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
        },
        complete: function () {
            document.getElementById('txtBLGeneraReferencia').disabled = false;
            document.getElementById('txtBusqueda').disabled = false;
            document.getElementById('txtDesde').disabled = false;
            document.getElementById('txtHasta').disabled = false;
            document.getElementById('tReferencias').disabled = false;
            document.querySelector('#lblAlerta').innerText = "Ingresa el nombre del consignatario";
            document.getElementById('dvSpinner').classList.add('invisibilidad');
        }
    });
}

function mostrarContenedores(idfila, numeroBL, numeroItinerario, numeroReferencia) {
    mostrarFila(idfila);
    var oReferencia = {
        sNUM_BL: numeroBL,
        nNUM_ITINE: numeroItinerario,
        sReferencia: numeroReferencia
    };

    $.ajax({
        url: '/Referencias/ObtenerContenedoresPorReferencia',
        type: 'POST',
        data: oReferencia,
        beforeSend: function () {
            document.getElementById('dvSpinner').classList.remove('invisibilidad');
        },
        success: function (result) {
            if (result != null) {

                //Obtener el monto por contenedor
                var montoMXN = result.object.dMontoMXN;
                var montoUSD = result.object.dMontoUSD;
                var cantidadContenedores = result.object.nCANTIDAD_CONTENEDORES;

                var montoPorContenedorMXN = montoMXN / cantidadContenedores;
                var montoPorContenedorUSD = montoUSD / cantidadContenedores;

                $('#td' + numeroBL).empty();
                //$('#td' + numeroBL).append("<table class='tableContenedoresRefencias'><thead><tr><td>Contenedores</td> <td>Depósito en Garantía Pagado</td> <td>Monto Daño (USD)</td> <td>Monto Daño (MXN)</td></tr></thead><tbody id='tb" + numeroBL + "'></tbody></table>");
                $('#td' + numeroBL).append("<table class='tableContenedoresRefencias'><thead><tr><td>CONTENEDORES</td><td>MONTO MXN</td> <td>MONTO USD</td> <td class='centrarColumna'>PAGADO</td></tr></thead><tbody id='tb" + numeroBL+ "'></tbody></table>");

                $.each(result.objects, function (i, obj) {
                    tr = $('<tr/>');
                    tr.append("<td>" + obj.sNOMBRE_CONTENEDOR + "</td>");
                    tr.append("<td>$" + convertirFormatoMoneda(montoPorContenedorMXN) + "</td>");
                    tr.append("<td>$" + convertirFormatoMoneda(montoPorContenedorUSD) + "</td>");
                    if (obj.bCONTENEDOR_PAGADO == 0) {
                        tr.append("<td  class='centrarColumna'>NO</td>");
                    }
                    else {
                        tr.append("<td class='centrarColumna'>SI</td>");
                    } 
                    //tr.append("<td>" + obj.rDESPERFECTO_CONTENEDOR.dCOSTO_REPARACION_USD + "</td>");
                    //tr.append("<td class='centrarColumna'>" + obj.rDESPERFECTO_CONTENEDOR.dCOSTO_REPARACION_MXN + "</td>");
                    $('#tb' + numeroBL).append(tr)
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


//-------------------------------------Funciones para los controles que permiten generar una referenia nueva, validando la referencia, el consignatario y generarndola--------------------------

function GenerarReferencia() {

    var valor = document.getElementById("txtDatosBL").value;
    var valores = valor.split('|');

    var oReferencia = {
        sReferencia: "",
        sNUM_BL: valores[0],
        //claveCliente: "XD,
        nNUM_ITINE: valores[1],
        dMontoMXN: valores[2],
        //sIdUsuario: sessionStorage.getItem("idUsuario"), //sessionStorage.setItem("rol", result.object.rASPNET_USERS_ROLES.rASPNER_ROLES.sName)
        rDATOS_BUSQUEDA: {
            sTexto: "",
            sFechaInicial_Desde: null,
            sFechaFinal_Hasta: null
        },
        rASPNET_USERS: {
            sId: sessionStorage.getItem('idUsuario')
        }
    };

    $.ajax({
        url: '/Referencias/InsertarReferenciaPago',
        type: 'POST',
        data: oReferencia,
        beforeSend: function () {
            $("#btnGenerarReferencia").html('<i class="fa fa-spinner fa-spin"></i> Procesando...');
            document.getElementById('btnGenerarReferencia').disabled = true;
            document.getElementById('txtBLGeneraReferencia').disabled = true;
            document.getElementById('txtBusqueda').disabled = true;
            document.getElementById('txtDesde').disabled = true;
            document.getElementById('txtHasta').disabled = true;
            document.getElementById('tReferencias').disabled = true;
            $('#btnGenerarReferencia').css('cursor','not-allowed');
        },
        success: function (result) {
            if (result != null) {
                if (result.nIdReferencia > 0) {
                    Swal.fire({
                        icon: 'success',
                        html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Generación de referencia de pago </h1>' +
                            '<br>' +
                            '<p style=\'font-size: 16px;\'>La referencia de pago <strong style=\'font-size: 16px;\'>' + result.sReferencia + '</strong> para el BL: <strong style=\'font-size: 16px;\'>' + oReferencia.sNUM_BL + '</strong> ha sido generada y enviada a su correo electrónico.</p>',
                        confirmButtonText: 'Aceptar'
                        
                    });
                    ObtenerReferencias();
                    document.getElementById('txtBLGeneraReferencia').value = '';
                    document.getElementById('txtBLGeneraReferencia').value = '';
                } else {
                    Swal.fire({
                        icon: 'error',
                        html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Generación de referencia de pago </h1>' +
                            '<br>' +
                            '<p style=\'font-size: 16px;\'>Ocurrió un error al generar la referencia, por favor inténtelo nuevamente. Si el problema persiste, favor de notificar al administrador.</p>',
                        confirmButtonText: 'Aceptar'
                    });
                }

            }
        },
        error: function (xhr, textStatus, errorThrow) {
            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
        },
        complete: function () {
            $("#btnGenerarReferencia").html('VALIDAR');
            $('#btnGenerarReferencia').css('cursor', 'pointer');
            document.getElementById('btnGenerarReferencia').disabled = false;
            document.getElementById('txtBusqueda').disabled = false;
            document.getElementById('txtBusqueda').value = "";
            document.getElementById('txtDesde').disabled = false;
            document.getElementById('txtHasta').disabled = false;
            document.getElementById('tReferencias').disabled = false;
            document.getElementById('txtConsignatarioGeneraReferencia').value = "";
            document.getElementById('txtConsignatarioGeneraReferencia').disabled = false;
            document.getElementById('colConsignatario').hidden = true;
            document.getElementById('divTitulosGenerarReferenciaConsignatario').hidden = true;
            document.getElementById('txtBLGeneraReferencia').disabled = false;
        }
    });
}

function ObtenerDatosBL() {
    let sURL = '/Referencias/ObtenerDatosBL';

    var oObjeto = {
        sNUM_BL: document.getElementById("txtBLGeneraReferencia").value
    }

    $.ajax({
        url: sURL,
        type: 'POST',
        data: oObjeto,
        beforeSend: function () {
            document.getElementById('txtBLGeneraReferencia').disabled = true;
            document.getElementById('btnGenerarReferencia').disabled = true;
            document.getElementById('txtBusqueda').disabled = true;
            document.getElementById('txtDesde').disabled = true;
            document.getElementById('txtHasta').disabled = true;
            document.getElementById('tReferencias').disabled = true;
        },
        success: function (result) {
            if (result.correct == true) {
                document.getElementById('txtDatosBL').value = "";
                document.getElementById('txtDatosBL').value = result.object.sNUM_BL + "|" + result.object.nNUM_ITINE + "|" + result.object.dMontoUSD;
                document.getElementById('colConsignatario').hidden = false;
                $("#btnGenerarReferencia").html('GENERAR REFERENCIA');
                document.getElementById('divTitulosGenerarReferenciaConsignatario').hidden = false;
                $("#txtConsignatarioGeneraReferencia").focus();
            }
            else {
                document.getElementById('txtDatosBL').value = "";
            }
        },
        error: function (xhr, textStatus, errorThrow) {
            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
        },
        complete: function () {
           
            document.getElementById('txtBusqueda').disabled = false;
            document.getElementById('txtDesde').disabled = false;
            document.getElementById('txtHasta').disabled = false;
            document.getElementById('tReferencias').disabled = false;
        }
    });
}


function ComprobarBLValido() {
    let sURL = '/Referencias/ComprobarBLValido';
    var bl = document.getElementById("txtBLGeneraReferencia").value;
    bl = bl.replace(/\s/g, '');

    const oObjeto = {
        sNUM_BL: bl,
        sUSER_ID: sessionStorage.getItem("idUsuario")
    }

    var bandera = true;

    $.ajax({
        url: sURL,
        type: 'POST',
        data: oObjeto,
        beforeSend: function () {
            $("#btnGenerarReferencia").html('<i class="fa fa-spinner"></i> Validando...');
            document.getElementById('txtBLGeneraReferencia').disabled = true;
            document.getElementById('btnGenerarReferencia').disabled = true;
            document.getElementById('txtBusqueda').disabled = true;
            document.getElementById('txtDesde').disabled = true;
            document.getElementById('txtHasta').disabled = true;
            document.getElementById('tReferencias').disabled = true;
            $('#txtBLGeneraReferencia').removeClass('border-danger')
            $('#lblError').addClass('text-hide')
            document.getElementById('colConsignatario').hidden = true;
            document.getElementById('txtConsignatarioGeneraReferencia').disabled = false;
            document.getElementById('txtConsignatarioGeneraReferencia').value = "";
        },
        success: function (result) {

            if (oObjeto.sNUM_BL == "") {
                result = 7;
            }

            switch (result) {
                case 0:
                    MensajeErrorBL("El BL: " + bl + " ya cuenta con una referencia generada");
                    break;
                case 1:
                    MensajeErrorBL("El BL: " + bl + " no existe");
                    break;
                case 2:
                    MensajeErrorBL("El BL: " + bl + " fue cancelado");
                    break;
                case 3:
                    MensajeErrorBL("El BL: " + bl + " no cumple con el tipo de revalidación para DG");
                    break;
                case 4:
                    MensajeErrorBL("El BL: " + bl + " no tiene aviso de arribo");
                    break;
                case 5:
                    $('#txtBLGeneraReferencia').removeClass('border-danger')
                    $('#lblError').addClass('text-hide')
                    bandera = false;
                    ObtenerDatosBL();
                    break;
                case 6: //Condicion Agregada
                    MensajeErrorBL("Es necesario agregar el cliente a la lista de clientes para poder generar referencia a este BL");
                    break;
                default:
                    $('#txtBLGeneraReferencia').removeClass('border-danger')
                    $('#lblError').addClass('text-hide')
                    break;
            }

        },
        error: function (xhr, textStatus, errorThrow) {
            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
        },
        complete: function () {
            if (bandera) {
                $("#btnGenerarReferencia").html('VALIDAR');
                document.getElementById('txtBLGeneraReferencia').disabled = false;
            }
            else {
                document.getElementById('txtBLGeneraReferencia').disabled = true;
            }
            document.getElementById('btnGenerarReferencia').disabled = false;
            document.getElementById('txtBusqueda').disabled = false;
            document.getElementById('txtDesde').disabled = false;
            document.getElementById('txtHasta').disabled = false;
            document.getElementById('tReferencias').disabled = false;
        }
    });
}

function ObtenerConsignatario(tipoEvento) {
    let sURL = '/Referencias/ObtenerConsignatario';

    var oObjeto = {
        sTexto: document.getElementById("txtConsignatarioGeneraReferencia").value,
        sNumBL: document.getElementById("txtBLGeneraReferencia").value
    }

    var retorno = true;

    $.ajax({
        url: sURL,
        type: 'POST',
        data: oObjeto,
        beforeSend: function () {
            document.getElementById('txtBLGeneraReferencia').disabled = true;
            document.getElementById('txtConsignatarioGeneraReferencia').disabled = true;
            document.getElementById('btnGenerarReferencia').disabled = true;
            document.getElementById('txtBusqueda').disabled = true;
            document.getElementById('txtDesde').disabled = true;
            document.getElementById('txtHasta').disabled = true;
            document.getElementById('tReferencias').disabled = true;
        },
        success: function (result) {
            if (result.correct == true) {
                document.getElementById('txtConsignatarioGeneraReferencia').value = result.object;
               
                if (tipoEvento == 1) {
                    document.querySelector('#lblAlerta').innerText = "Puede generar la referencia";
                    document.getElementById('btnGenerarReferencia').disabled = false;
                    document.getElementById('txtConsignatarioGeneraReferencia').disabled = true;
                }
                else {
                    GenerarReferencia();
                }

            }
            else {
                document.getElementById('txtConsignatarioGeneraReferencia').value = "";
                document.querySelector('#lblAlerta').innerText = "No se encontro el consignatario";
                document.getElementById('txtConsignatarioGeneraReferencia').disabled = false;
            }
        },
        error: function (xhr, textStatus, errorThrow) {
            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
        },
        complete: function () {
            //document.getElementById('txtBusqueda').disabled = false;
            //document.getElementById('txtDesde').disabled = false;
            //document.getElementById('txtHasta').disabled = false;
            //document.getElementById('tReferencias').disabled = false;
        }
    });

}

//-----------------------------------Funciones para genrar PDF al presionar el boton de PDF y mostrar la fila de los contendores de una referencia------------------

function crearPDF(idReferencia) {
    oDatos = {
        nIdReferencia: idReferencia
    }
    window.open(URLDescargaPDF + "?idReferencia=" + idReferencia);
}

function mostrarFila(idfila) {
    if (botonEstatus) {
        document.getElementById(idfila).hidden = false;
        botonEstatus = false;
    }
    else {
        document.getElementById(idfila).hidden = true;
        botonEstatus = true;
    }
    
}


function MensajeErrorBL(mensaje) {
    $('#txtBLGeneraReferencia').addClass('border-danger')
    $('#lblError').removeClass('text-hide')
    document.querySelector('#lblError').innerText = mensaje;
    document.getElementById("txtBLGeneraReferencia").value = "";
}


//----------------------------------------------Eventos para la creación de referencias--------------------------------------------

//Eventos Ajax para cuando se preciona la tecla enter
$("#txtBLGeneraReferencia").keyup(function (event) {
    if (event.keyCode === 13) {
        ComprobarBLValido();
    }
});

$("#txtConsignatarioGeneraReferencia").keyup(function (event) {
    if (event.keyCode === 13) {
        var sBusqueda = document.getElementById('txtConsignatarioGeneraReferencia').value;

        if (sBusqueda.length >= 6) {
            ObtenerConsignatario(1);
        }
        else {
            document.querySelector('#lblAlerta').innerText = "Ingrese minimo 6 caracteres para buscar el consignatario";
        }
    }
});

//Eventos de tipo blur y click
document.querySelector('#txtConsignatarioGeneraReferencia').addEventListener('blur', (e) => {
    e.preventDefault();
    var sBusqueda = document.getElementById('txtConsignatarioGeneraReferencia').value;

    if (sBusqueda.length >= 6) {
        ObtenerConsignatario(1);
    }
    else {
        document.querySelector('#lblAlerta').innerText = "Ingrese minimo 6 caracteres para buscar el consignatario";
    }
    
});

document.querySelector('#btnGenerarReferencia').addEventListener('click', (e) => {
    e.preventDefault();
    var sBusqueda = document.getElementById('txtConsignatarioGeneraReferencia').value;

    var txtBtnGenerarReferencia = $('#btnGenerarReferencia').text();
    if (txtBtnGenerarReferencia == "VALIDAR") {
        ComprobarBLValido();
    }
    else {
        if (sBusqueda.length >= 6) {
            ObtenerConsignatario(2);
        }
        else {
            document.querySelector('#lblAlerta').innerText = "Ingrese minimo 6 caracteres para buscar el consignatario";
        }
    }
});



//-----------------------------Eventos para busqueda de información filtrada--------------------------------------------
document.querySelector('#txtBusqueda').addEventListener('keyup', function () {
    var sBusqueda = document.getElementById('txtBusqueda').value;
    sBusqueda = sBusqueda.replace(/\s/g, '');
    if (sBusqueda.length >= 3 || sBusqueda.length == 0) {
        paginaActualVGlobal = 1;
        ObtenerReferencias();
    }
});

document.querySelector('#txtDesde').addEventListener('change', () => {
    paginaActualVGlobal = 1;
    ObtenerReferencias();
});

document.querySelector('#txtHasta').addEventListener('change', () => {
    paginaActualVGlobal = 1;
    ObtenerReferencias();
});

/* Finaliza Eventos */

////var URLActual = "http://localhost:8153/Referencias";
////var URLDescargaPDF = "http://localhost:8153/Referencias/DescargarPDF?idReferencia=";


//-------Variables para paginacion-----//
//var totalRegistros = 0;
//var paginaActualVGlobal = 0;
//var registrosPorPagina = 10;
//----------------------------------------//

//function validarAdeudosDelLaReferencia(estatusContenedoresControlEquipo, itinerario, bl) {

//    //Primero validar si existen adeudos si no existen  mandar los siguientes mensajes


//    if (estatusContenedoresControlEquipo == 1) {
//        validarEstatusBL(numeroBL, itinerario);
//    }
//    else if (estatusContenedoresControlEquipo == 2) {
//        //Si no tiene adeudos mostrar el mensaje. 
//        if (!validarEstatusBL(bl, itinerario)) {
//            Swal.fire({
//                icon: 'warning',
//                html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Operación no disponible</h1>' +
//                    '<br>' +
//                    '<p style=\'font-size: 16px;\'>Existen contenedores marcados con daño por parte de control de equipo para la referencia del BL: <strong style=\'font-size: 16px;\'>' + bl + '</strong> no se puede solicitar la devolución </p>',
//                button: 'Aceptar'
//            });
//        }
//    }
//    else {
//        Swal.fire({
//            icon: 'warning',
//            html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Operación no disponible</h1>' +
//                '<br>' +
//                '<p style=\'font-size: 16px;\'>La referencia de pago <strong style=\'font-size: 16px;\'> Aun no se validan todos los contenedores del BL:  <strong style=\'font-size: 16px;\'>' + bl + '</strong> por parte de Control de Equipo.</p>',
//            button: 'Aceptar'
//        });
//    }
//}

/* Funciones */

//Funcion para la paginacion
//function agregarPaginacion(paginaActual) {
//    paginaActualVGlobal = paginaActual;
//    ObtenerReferencias();

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
//            $("#listaPaginacion").append('<li class="paginaActual" onclick =\"agregarPaginacion(' + i + ')\"><span>'+i+'</span></li>');
//        }
//        else {
//            $("#listaPaginacion").append('<li class = "opcionesPaginacion" onclick =\"agregarPaginacion(' + i + ')\"><span>' + i + '</span></li>');
//        }
//    }
//    $("#listaPaginacion").append('<li class="btnDerecho" onclick =\"agregarPaginacion(' + cantidadPaginas + ')\"><i class="fa fa-angle-double-right" aria-hidden="true"></i></li>');
//}

//function obtenerTotalRegistros() {
//    var idCliente = sessionStorage.getItem('idCliente') == null ? null : sessionStorage.getItem('idCliente');

//    var oReferencia = {
//        rASPNET_USERS: {
//            sId: sessionStorage.getItem('idUsuario')
//        },
//        rCLIENTE: {
//            nFIIDCLIETE: idCliente
//        }
//    };

//    $.ajax({
//        url: '/Referencias/ObtenerNumeroTotalReferencias',
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
//        }
//    });
//}