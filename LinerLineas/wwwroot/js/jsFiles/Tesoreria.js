//Vatiable menu desplegable
var botonEstatus = true;

//Variables para la paginación
var nIdFuncionPaginacion = 1;
var apiTotalRegistros = '/Tesoreria/GetTotalReferenciasDatosBancarios';

$(document).ready(function () {
    document.getElementById('txt_Bandera_Menu').value = '1';
    recargarTablaPorTiempo();
    obtenerTotalRegistros();
});

function recargarTablaPorTiempo() {
    setInterval(ObtenerReferencias, 300000);
}



//Funciones y eventos para mostrar datos 
function ObtenerReferencias() {
    var oDatosBusqueda = {
        sRol: sessionStorage.getItem("rol"),
        sTexto: "",
        sFechaInicial_Desde: null,
        sFechaFinal_Hasta: null,
        nIdTipoConsultaTesoseria: 1, //Siempre sera 1 ya que en la consulta este estatus es utilizado para obtener todas las referencias.
        rPAGINACION: {
            nPaginaActual: paginaActualVGlobal,
            nRegistrosPorPagina: registrosPorPagina
        }
    };

    var sBusqueda = document.getElementById('txtBusqueda').value;
    sBusqueda = sBusqueda.replace(/\s/g, '');
    if (sBusqueda.length >= 3) {
        oDatosBusqueda.sTexto = sBusqueda;
    }

    if (document.getElementById('txtDesde').value !== '') {
        oDatosBusqueda.sFechaInicial_Desde = document.getElementById('txtDesde').value;
    }

    if (document.getElementById('txtHasta').value !== '') {
        oDatosBusqueda.sFechaFinal_Hasta = document.getElementById('txtHasta').value;
    }

    $.ajax({
        url: '/Tesoreria/GetReferenciasDatosBancarios',
        type: 'POST',
        data: oDatosBusqueda,
        beforeSend: function () {
            document.getElementById('btnDescargarReferencias').disabled = true;
            document.getElementById('txtBusqueda').disabled = true;
            document.getElementById('txtDesde').disabled = true;
            document.getElementById('txtHasta').disabled = true;
            document.getElementById('dvSpinner').classList.remove('invisibilidad');
        },
        success: function (result) {
            if (result != null) {

                $('#tbReferencias').empty();
                $.each(result.objects, function (i, obj) {
                    tr = $('<tr/>');

                    tr.append("<td class='centrarColumna tdMostrarContenedores' onclick =\"mostrarBL('tr" + obj.rREFERENCIAS.sNUM_BL + "','" + obj.rREFERENCIAS.sNUM_BL + "')\"><i class='fa fa-caret-down' aria-hidden='true'></i></td>")
                    tr.append("<td class='centrarColumna' hidden>" + obj.nFIIDDATOS_BANCARIOS + "</td>")
                    tr.append("<td class='centrarColumna'>" + obj.rREFERENCIAS.sReferencia + "</td>")
                    tr.append("<td class='centrarColumna'>" + obj.sFSRAZONSOCIAL + "</td>")
                    tr.append("<td class='centrarColumna'>" + obj.rBancos_Datos_Bancarios.sFSDESCRIPCION  + "</td>")

                    tr.append("<td class='centrarColumna'>" + obj.sFSNUMERO_CUENTA + "</td>")
                    tr.append("<td class='centrarColumna'>" + obj.sFSNUMERO_CLAVE_CUENTA + "</td>")
                    tr.append("<td class='centrarColumna'>" + obj.rREFERENCIAS_TESORERIA.sNombreConsignatario + "</td>")
                    tr.append("<td class='centrarColumna'>" + obj.sFSEMAIL_CONTACTO + "</td>")

                    tr.append("<td class='centrarColumna'>" + formatoFecha(obj.daFDAFECHA_SOLICITUD)+ "</td>")

                    tr.append("<td class='centrarColumna'>$" + convertirFormatoMoneda(obj.rREFERENCIAS.dMontoUSD) + "</td>")

                    tr.append("<td class='centrarColumna'>$" + convertirFormatoMoneda(obj.rREFERENCIAS.dMontoMXN) + "</td>")


                    if (obj.rREFERENCIAS_TESORERIA.sEstatusDevolucionSaldo == 0) {
                        tr.append("<td class='centrarColumna'>SIN ESTATUS</td>")
                    } else if (obj.rREFERENCIAS_TESORERIA.sEstatusDevolucionSaldo == 1) {
                        tr.append("<td class='centrarColumna'>EN PROCESO</td>")
                    }
                    else {
                        tr.append("<td class='centrarColumna'>DEVUELTO</td>")
                    }

                    //Boton para descarggar la caratula 
                    tr.append("<td class='centrarColumna'><div type='button' id='btnMostrarCaratula' class='btnMostrarCaratula' onclick =\"MostrarCaratulaBancaria('" + obj.rARCHIVO_RECIBO.sFSNOMBRE + "')\"><i class='fa fa-download' aria-hidden='true'></i></div></td>");
                    //tr.append("<td class='centrarColumna'><a id='btnMostrarCaratula' class='btnMostrarCaratula' href='~/CaratulasBancarias/SolitituddemejoraDepositos.docx' target='_blank' rel='noopener'>ccxzxvxzcvxz</a></td>");

                    //Validar solicitud
                    if (obj.rREFERENCIAS_TESORERIA.sEstatusSolicitudDatosBancarios == "Pendiente" ){
                        tr.append("<div class='divBtnSolicitudDG'><td class='centrarColumna'><button type='button' id='btnAceptarDatosBancarios' class='btnAceptarDatosBancarios' onclick =\"SolicitarDevolucionAX(" + obj.rREFERENCIAS.nIdReferencia + ",'" + obj.rREFERENCIAS.sNUM_BL + "', " + obj.rREFERENCIAS.nNUM_ITINE + "," + obj.nFIIDDATOS_BANCARIOS + ",'" + obj.rREFERENCIAS.sReferencia + "')\">Aceptar</button></td>"
                            + " <td class='centrarColumna'><button type='button' id='btnRechazarDatosBancarios' class='btnRechazarDatosBancarios' onclick =\"MostrarModalObservaciones(" + obj.nFIIDDATOS_BANCARIOS + "," + 2 + ",'" + obj.rREFERENCIAS.sReferencia + "','" + obj.rREFERENCIAS.sNUM_BL + "', '" + obj.rBancos_Datos_Bancarios.sFSDESCRIPCION  + "', '" + obj.sFSNUMERO_CUENTA + "', '" + obj.sFSNUMERO_CLAVE_CUENTA + "', '" + obj.sFSRAZONSOCIAL + "','" + obj.rREFERENCIAS_TESORERIA.sCorreoUsuario + "')\">Rechazar</button></td></div>"); //El estatus 2 simboliza rechazado en la base de datos
                    }
                    else if (obj.rREFERENCIAS_TESORERIA.sEstatusSolicitudDatosBancarios == "Rechazado") {
                        tr.append("<td class='centrarColumna'>DATOS RECHAZADOS</td>");
                    }
                    else {
                        tr.append("<td class='centrarColumna'>DATOS ACEPTADOS</td>");
                    }

                    tr.append("<td class='centrarColumna' hidden>" + obj.rREFERENCIAS.nIdReferencia + "</td>")
                    tr.append("<td class='centrarColumna' hidden>" + obj.rREFERENCIAS.sNUM_BL + "</td>")
                    tr.append("<td class='centrarColumna' hidden>" + obj.rREFERENCIAS.nNUM_ITINE + "</td>")
                    tr.append("<td class='centrarColumna' hidden>" + obj.rREFERENCIAS_TESORERIA.sCorreoUsuario + "</td>")

                    $('#tbReferencias').append(tr.addClass("punteroTabla"));

                    //Fila agregada para desplegar los contenedores
                    var filaPrueba = "<tr id='tr" + obj.rREFERENCIAS.sNUM_BL + "' hidden class='trContenedoresReferencias'><td colspan='12' id='td" + obj.rREFERENCIAS.sNUM_BL + "'></td></tr>"
                    $('#tbReferencias').append(filaPrueba);

                });
            }
        },
        error: function (xhr, textStatus, errorThrow) {
            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
        },
        complete: function () {
            document.getElementById('btnDescargarReferencias').disabled = false;
            document.getElementById('txtBusqueda').disabled = false;
            document.getElementById('txtDesde').disabled = false;
            document.getElementById('txtHasta').disabled = false;
            document.getElementById('dvSpinner').classList.add('invisibilidad');
        }
    });
}

function mostrarBL(idfila, numeroBL) {
    mostrarFila(idfila);

    $('#td' + numeroBL).empty();
    $('#td' + numeroBL).append("<table class='tableContenedoresRefencias'><thead><tr><td>BL</td></tr></thead><tbody id='tb" + numeroBL + "'></tbody></table>");
    tr = $('<tr/>');
    tr.append("<td>" + numeroBL + "</td>");
    $('#tb' + numeroBL).append(tr)
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


function MostrarModalObservaciones(idDatosBancarios, idEstatus, numeroReferencia, numeroBL, banco, numeroCuenta, claveCuenta, razonSocial, correo) {
    document.getElementById('txt_idDatosBancarios').value = idDatosBancarios;
    document.getElementById('txt_idEstatus').value = idEstatus;
    document.getElementById('txt_numeroReferencia').value = numeroReferencia;
    document.getElementById('txt_numeroBL').value = numeroBL;
    document.getElementById('txt_banco').value = banco;
    document.getElementById('txt_numeroCuenta').value = numeroCuenta;
    document.getElementById('txt_claveCuenta').value = claveCuenta;
    document.getElementById('txt_razonSocial').value = razonSocial;
    document.getElementById('txt_correo').value = correo;

    var modal = document.getElementById('modalObservacionesDatosBancarios');
    modal.classList.add("modal--show");
}

function ActualizarEstatusDatosBancarios() {
    let sURL = '/Tesoreria/UpdateEstatusDatosBancarios';

    var oDatos = {
        nFIIDDATOS_BANCARIOS: document.getElementById('txt_idDatosBancarios').value,
        nIdEstatusReferencia: document.getElementById('txt_idEstatus').value,
        sFSOBSERVACIONES: document.getElementById('txtArea_Observaciones').value,
        rREFERENCIAS_TESORERIA: {
            sIdUsuario: sessionStorage.getItem('idUsuario')
        }
    }
    var numeroReferencia = document.getElementById('txt_numeroReferencia').value;

    $.ajax({
        url: sURL,
        type: 'POST',
        data: oDatos,
        beforeSend: function () {
            document.getElementById('dvSpinner').classList.remove('invisibilidad');
        },
        success: function (result) {
            if (result.correct) {
                //Objeto con los datos para enviar el correo
                //Referencia, BL, Banco, NumeroCuenta, Clave, razon socila y correo

                oDatosCorreo = {
                    sFSRAZONSOCIAL: document.getElementById('txt_razonSocial').value,
                    sFSBANCO: document.getElementById('txt_banco').value ,
                    sFSNUMERO_CUENTA: document.getElementById('txt_numeroCuenta').value,
                    sFSNUMERO_CLAVE_CUENTA: document.getElementById('txt_claveCuenta').value,
                    rREFERENCIAS: {
                        sReferencia: numeroReferencia,
                        sNUM_BL: document.getElementById('txt_numeroBL').value
                    },
                    rREFERENCIAS_TESORERIA: {
                        sCorreoUsuario: document.getElementById('txt_correo').value
                    },
                    sFSOBSERVACIONES: document.getElementById('txtArea_Observaciones').value
                }

                EnviarCorreo(oDatosCorreo);
                CerrarModalObservaciones();
                Swal.fire({
                    icon: 'success',
                    html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Datos bancarios rechazados</h1>' +
                        '<p style=\'font-size: 16px;\'> Se notificó al usuario de la referencia: <strong style=\'font-size: 16px;\'>' + numeroReferencia + ' </strong> que debe actualizar sus datos bancarios. </p>',
                    confirmButtonText: 'Aceptar'
                }).then(resp => {
                    if (resp.isConfirmed) {
                        location.reload();
                    } else {
                        location.reload();
                    }
                });
            }
            else {
                Swal.fire({
                    icon: 'error',
                    html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Datos bancarios erroneos</h1>' +
                        '<p style=\'font-size: 16px;\'> No se pudo notificar al usuario que los datos bancarios de la referencia <strong style=\'font-size: 16px;\'> ' + numeroReferencia + '</strong> son incorrectos </p>',
                    confirmButtonText: 'Aceptar'
                }).then(resp => {
                    if (resp.isConfirmed) {
                        location.reload();
                    } else {
                        location.reload();
                    }
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

function SolicitarDevolucionAX(idReferencia, numeroBL, numeroItinerario, idDatosBancarios, numeroReferencia) {
 
    let sURL = '/DevolucionesAutomaticas/SolicitarDevolucionSaldo';

    
    var oDatos = {
        nIdReferencia: idReferencia,
        sBL: numeroBL,
        nNumeroItinerario: numeroItinerario,
        rDATOS_BANCARIOS_REFERENCIAS: {
            nFIIDDATOS_BANCARIOS: idDatosBancarios
        },
        sID_USUARIO: sessionStorage.getItem('idUsuario')
    }

    $.ajax({
        url: sURL,
        type: 'POST',
        data: oDatos,
        beforeSend: function () {
            document.getElementById('dvSpinner').classList.remove('invisibilidad');
            $(".btnAceptarDatosBancarios").prop('disabled', true);
            $(".btnRechazarDatosBancarios").prop('disabled', true);
            $(".btnAceptarDatosBancarios").css('cursor', 'not-allowed');
            $(".btnRechazarDatosBancarios").css('cursor', 'not-allowed');
        },
        success: function (result) {
            if (result) {

                Swal.fire({
                    icon: 'success',
                    html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Solicitud devolución DG</h1>' +
                        '<p style=\'font-size: 16px;\'> Se envió la solicitud de devolución para la referencia: <strong style=\'font-size: 16px;\'> ' + numeroReferencia + ' </strong> </p>',
                    confirmButtonText: 'Aceptar'
                }).then(resp => {
                    if (resp.isConfirmed) {
                        location.reload();
                    } else {
                        location.reload();
                    }
                });
            }
            else {
                Swal.fire({
                    icon: 'error',
                    html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Solicitud devolucion DG</h1>' +
                        '<p style=\'font-size: 16px;\'> No se pudo solicitar la devolución para la referencia: <strong style=\'font-size: 16px;\'>' + numeroReferencia  + ' </strong> </p>',
                    confirmButtonText: 'Aceptar'
                }).then(resp => {
                            if (resp.isConfirmed) {
                                location.reload();
                            } else {
                                location.reload();
                            }
                });
            }

        },
        error: function (xhr, textStatus, errorThrow) {
            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
        },
        complete: function () {
            document.getElementById('dvSpinner').classList.add('invisibilidad');
            $(".btnAceptarDatosBancarios").prop('disabled', false);
            $(".btnRechazarDatosBancarios").prop('disabled', false);
            $(".btnAceptarDatosBancarios").css('cursor', 'pointer');
            $(".btnRechazarDatosBancarios").css('cursor', 'pointer');
        }
    });
}

function EnviarCorreo( oDatosCorreo) {
    let sURL = '/Tesoreria/SentEmail';


    $.ajax({
        url: sURL,
        type: 'POST',
        data: oDatosCorreo,
        beforeSend: function () {
        },
        success: function (result) {
            if (result) {

                console.log("Correo enviado");
            }
            else {
                console.log("Fallo al enviar el correo");
            }

        },
        error: function (xhr, textStatus, errorThrow) {
            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
        },
        complete: function () {
        }
    });
}

function MostrarCaratulaBancaria(nombreArchivo) {
    var p = pathCaratulaBancaria;
    var ruta = pathCaratulaBancaria + nombreArchivo;
    var win = window.open(ruta, '_blank');
    // cambiar el foco al nuevo tab (punto opcional)
    win.focus();
}

//Funciones y eventos para el modal
function CerrarModal() {
    var modal = document.getElementById('modalReferenciasDatosBancarios');
    modal.classList.remove("modal--show");
}

function MostrarModal() {
    var modal = document.getElementById('modalReferenciasDatosBancarios');
    modal.classList.add("modal--show");
}

$(".rdReferencias").change(function () {
    if (this.checked) {
        document.getElementById('modal_btnAceptar').disabled = false;
        $('#modal_btnAceptar').css('cursor', 'pointer');
        $('#modal_btnAceptar').css('opacity', '1');
    }
    else {
        document.getElementById('modal_btnAceptar').disabled = true;
        $('#modal_btnAceptar').css('cursor', 'not-allowed');
        $('#modal_btnAceptar').css('opacity', '0.3');
    }
});

//Funciones y eventos para el modal de observaciones 
document.querySelector('#modal_Boton_Salir').addEventListener('click', (e) => {
    CerrarModal();
});

document.querySelector('#modal_Boton_Salir_Observaciones').addEventListener('click', (e) => {
    CerrarModalObservaciones();
});

function CerrarModalObservaciones() {
    var modal = document.getElementById('modalObservacionesDatosBancarios');
    modal.classList.remove("modal--show");
    LimpiarModal();
}

function LimpiarModal() {
    document.getElementById('txtArea_Observaciones').value = '';
    document.getElementById('txt_idDatosBancarios').value = '';
    document.getElementById('txt_idEstatus').value = '';
    document.getElementById('txt_numeroReferencia').value = '';
    document.getElementById('txt_numeroBL').value = '';
    document.getElementById('txt_banco').value = '';
    document.getElementById('txt_numeroCuenta').value = '';
    document.getElementById('txt_claveCuenta').value = '';
    document.getElementById('txt_razonSocial').value = '';
    document.getElementById('txt_correo').value = '';

}


//Eventos para la busqueda filtrada
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


document.querySelector('#txtArea_Observaciones').addEventListener('input', () => {

    var txtObservacion = document.getElementById('txtArea_Observaciones').value;
    if (txtObservacion != '') {
        document.getElementById('modal_btnAceptar_Observaciones').disabled = false;
        $('#modal_btnAceptar_Observaciones').css('cursor', 'pointer');
        $('#modal_btnAceptar_Observaciones').css('opacity', '1');
    }
    else {
        document.getElementById('modal_btnAceptar_Observaciones').disabled = true;
        $('#modal_btnAceptar_Observaciones').css('cursor', 'not-allowed');
        $('#modal_btnAceptar_Observaciones').css('opacity', '0.3');
    }
});

document.querySelector('#modal_btnAceptar_Observaciones').addEventListener('click', () => {
    ActualizarEstatusDatosBancarios();
});
