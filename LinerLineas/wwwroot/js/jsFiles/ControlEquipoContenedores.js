
$(document).ready(function () {
    document.getElementById('txt_Bandera_Menu').value = '1';
    if (document.getElementById("tContenedores")) {
        cargarElementos();
    }    
});


//--------------------------------------------------Funciones para saber que información o alertas cargará la pagina------------------------------------------------------------------- 
function cargarElementos() {
    cargarTitulos();
    validarEstatusDeContenedoresConDesperfecto();
    cargarContenedoresByBL();
}

function validarEstatusDeContenedoresConDesperfecto() {
    var sURL = '/ControlEquipo/UpdateEstatusDesperfectoContenedores';
    var oDatosURL = cargarDatosUrl();

    var oDatos = {
        sNUM_BL: oDatosURL.sNUM_BL,
        nNUM_ITINE: oDatosURL.nItinerario,
        nIdReferencia: oDatosURL.rREFERENCIA.nIdReferencia
    }

    $.ajax({
        url: sURL,
        type: 'POST',
        data: oDatos,
        success: function (result) {
            if (result.correct) {
                console.log("Estatus del daño de los contenedores actualizado con exito.");
            }
            else {
                console.log("No se actualizó el estatus del daño de los contenedores.");
            }

        },
        error: function (xhr, textStatus, errorThrow) {
            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
        }
    });
}

function cargarTitulos() {
    var oDatosURL = cargarDatosUrl();
    if ($('#spanBL')[0] && $('#spanReferencia')[0]) {
        document.querySelector('#spanBL').innerText = "BL: " + oDatosURL.sNUM_BL;
        document.querySelector('#spanReferencia').innerText = "Referencia: " + oDatosURL.rREFERENCIA.sReferencia;
    }

    if ($('#h3Cont')[0]) {
        const valores = window.location.search;
        const urlParameters = new URLSearchParams(valores);
        sNUM_CONT = urlParameters.get('NumeroContenedor')
        document.querySelector('#h3Cont').innerText = "Contenedor: " + sNUM_CONT;
    }
}


function validarEstatusContenedor() {
    var estatus = 0;
    var validacion = leerCeldas(10, "SIN ESTATUS", "CON DAÑO");
    if (validacion) {
        validacion = leerCeldas(13, "DEVUELTO", "DEVUELTO");
        if (validacion) {
            //Se devolvieron y mostrará otro mensaje
            estatus = 2;
        }
        else {
            //No se han devuelto y mostrará otro mensaje
            estatus = 1;
        }
    }
    return estatus;
}


function leerCeldas(cell, primerValidacion, segundaValidacion) {
    let tabla = document.getElementById("tContenedores");
    var filas = tabla.rows.length;
    for (let i = 1, celda; i < filas; i++) {
        const celda = tabla.rows[i].cells[cell];
        if (celda.innerText == primerValidacion || celda.innerText == segundaValidacion) {
            return false;
        }
    }
    return true;
}

function cargarDatosUrl() {
    const valores = window.location.search;
    const urlParameters = new URLSearchParams(valores);
    var datosURL = {
        sNUM_BL: urlParameters.get('sBL'),
        rREFERENCIA: {
            nIdReferencia: urlParameters.get('nIdReferencia'),
            sReferencia: urlParameters.get('sReferencia')
        },
        dCOSTO_REPARACION_USD: 0,
        nItinerario: urlParameters.get('nItinerario')
    }
    return datosURL;
}





//----------------------------------------------------Función para obtener los datos que se mostrarán en pantalla----------------------------------------------------------------
function cargarContenedoresByBL() {
    let sURL = '/ControlEquipo/GetContenedores';
    var oDatosURL = cargarDatosUrl();
    var oDatos = {
        sTexto: "",
        sNumBL: oDatosURL.sNUM_BL,
        nItinerario: oDatosURL.nItinerario
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
                    //if (obj.rDESPERFECTO_CONTENEDOR.dCOSTO_REPARACION_MXN == 0)
                    if (obj.rDESPERFECTO_CONTENEDOR.rESTATUS_DESPERFECTO_CONTENEDOR.nFIIDESTATUS_DESPERFECTO_CONTENEDOR == 1)
                        tr.append("<td class='centrarColumna' id='trDelete'><div type='submit' id='btnDeleteDesperfectoContenedor' class='btnDeleteDesperfectoContenedor'  style='cursor: pointer' onclick =\"eliminarDanoContenedor('" + obj.sNUM_CONT + "')\"><span><i class='fa fa-trash' aria-hidden='true'></i></span></div></td>");  
                    else
                        tr.append("<td class='centrarColumna' id='trDelete'><p id='pEstatus'>-</p></td>")

                    tr.append("<td class='centrarColumna'>" + obj.sNUM_CONT + "</td>")
                    tr.append("<td class='centrarColumna' hidden>" + obj.rLINEAS.dNUM_LINEA + "</td>")
                    tr.append("<td class='centrarColumna' hidden>" + obj.rCLIENTES.nFIIDCLIETE + "</td>")
                    tr.append("<td class='centrarColumna' hidden>" + obj.rCLIENTES.sFSCLIENTE + "</td>")
                    tr.append("<td class='centrarColumna' hidden>" + obj.rCAMPOS_EXTRA.sESTATUS + "</td>");
                    tr.append("<td class='centrarColumna'>$" + convertirFormatoMoneda(obj.rDESPERFECTO_CONTENEDOR.dCOSTO_REPARACION_MXN) + "</td>");
                    tr.append("<td class='centrarColumna'>$" + convertirFormatoMoneda(obj.rDESPERFECTO_CONTENEDOR.dCOSTO_REPARACION_USD) + "</td>");
                    
                    if (obj.rDESPERFECTO_CONTENEDOR.daFECHA_ALTA == null)
                        tr.append("<td class='centrarColumna'>-</td>")
                    else
                        tr.append("<td class='centrarColumna'>" + formatoFecha(obj.rDESPERFECTO_CONTENEDOR.daFECHA_ALTA) + "</td>")

                    tr.append("<td class='centrarColumna'>" + obj.rCAMPOS_EXTRA.sCONTENEDOR_PERTENECE_CLIENTE + "</td>")

                    if (obj.rCAMPOS_EXTRA.nEstatusDanioContenedor == 1)
                        tr.append("<td class='centrarColumna'>SIN DAÑO</td>")
                    else if (obj.rCAMPOS_EXTRA.nEstatusDanioContenedor == 2)
                        tr.append("<td class='centrarColumna'>CON DAÑO</td>")
                    else
                        tr.append("<td class='centrarColumna'>SIN ESTATUS</td>")

                    //Estatus del contenedor por fecha de devolucion
                    //if (obj.daF_DEV_CTE == null)
                    //    tr.append("<td class='centrarColumna'><p id='pEstatus'>SIN DAÑO</p></td>")
                    //else {
                    //    if (obj.rCAMPOS_EXTRA.nEstatusDanioContenedor == 2) {
                    //        tr.append("<td class='centrarColumna'><p id='pEstatus'>CON DAÑO</p></td>")
                    //    }
                    //    else {
                    //        tr.append("<td class='centrarColumna'><p id='pEstatus'>SIN ESTATUS</p></td>")
                    //    }
                    //}

                    if (obj.daF_DEV_CTE != null) {
                        tr.append("<td class='centrarColumna' hidden>" + formatoFecha(obj.daF_DEV_CTE) + "</td>")
                    }
                    else {
                        tr.append("<td class='centrarColumna' hidden>NO DEVUELTO</td>")
                    }


                    if (obj.rCONTROL_DEVOLUCIONES.daFDDEVOLUCION == null)
                        tr.append("<td class='centrarColumna' hidden>SIN DEVOLUCION</td>")
                    else
                        tr.append("<td class='centrarColumna' hidden>" + formatoFecha(obj.rCONTROL_DEVOLUCIONES.daFDDEVOLUCION) + "</td>")

                    if (obj.rCAMPOS_EXTRA.nESTATUS_DEVOLUCION_DG  == 0) {
                        tr.append("<td class='centrarColumna' hidden>SIN ESTATUS</td>")
                    } else if (obj.rCAMPOS_EXTRA.nESTATUS_DEVOLUCION_DG  == 1) {
                        tr.append("<td class='centrarColumna' hidden>EN PROCESO</td>")
                    }
                    else {
                        tr.append("<td class='centrarColumna' hidden>DEVUELTO</td>")
                    }



                    if (obj.rCAMPOS_EXTRA.bCONTENEDOR_PAGADO) {
                        tr.append("<td class='centrarColumna'>SI</td>")
                    }
                    else {
                        tr.append("<td class='centrarColumna'>NO</td>")
                    }

                    ////rDESPERFECTO_CONTENEDOR.rESTATUS_DESPERFECTO_CONTENEDOR.nFIIDESTATUS_DESPERFECTO_CONTENEDOR
                    //if (obj.rDESPERFECTO_CONTENEDOR.dCOSTO_REPARACION_MXN == 0 && obj.rCAMPOS_EXTRA.nESTATUS_DEVOLUCION_DG  == 0 && obj.daF_DEV_CTE != null) {
                    //Si el estatus aparece como 2 = Pagado Poner la leyenda Pagado 
                    if (obj.rDESPERFECTO_CONTENEDOR.rESTATUS_DESPERFECTO_CONTENEDOR.nFIIDESTATUS_DESPERFECTO_CONTENEDOR != 2) {

                        //Si no ha sido solicitado y tampoco ha sido devuelto el Deposito en Garantía
                        if (obj.rCAMPOS_EXTRA.nESTATUS_DEVOLUCION_DG  == 0 && obj.daF_DEV_CTE != null) {
                            if (obj.rCAMPOS_EXTRA.nEstatusDanioContenedor == 1 || obj.rCAMPOS_EXTRA.nEstatusDanioContenedor == 2) {
                                tr.append("<td class='centrarColumna'>-</td>")
                            }
                            else {
                                if (obj.rCAMPOS_EXTRA.bCONTENEDOR_PAGADO) {
                                    tr.append("<td class='centrarColumna'><div class='row justify-content-center'><button type='submit' id='btnContenedorSinDesperfectoContenedor' class='btn  btnControlEquipo btnContenedorSinDesperfectoContenedor' onclick =\"contenedorSinDano('" + obj.sNUM_CONT + "')\"><span> SIN DAÑO  </span></button> </div>" +
                                        "<div class='row justify-content-center' style='padding-top : 10px'><button type='submit' id='btnAgregarDesperfectoContenedor' class='btn btnControlEquipo btnAgregarDesperfectoContenedor' onclick =\"abrirVistaAsignarMonto('" + oDatosURL.sNUM_BL + "'," + oDatosURL.rREFERENCIA.nIdReferencia + ",'" + oDatosURL.rREFERENCIA.sReferencia + "','" + obj.sNUM_CONT + "'," + oDatos.nItinerario + "," + obj.rMONTOS_APLICABLES.doFNMONTO_APLICABLE + ",0)\" ><span>AGREGAR DAÑO</span></button></div></td> ")
                                }
                                else {
                                    tr.append("<td class='centrarColumna'>-</td>")
                                }

                            }

                        }
                        else {
                            tr.append("<td class='centrarColumna'><p>-</p></td>")
                        }

                    }
                    else{
                        tr.append("<td class='centrarColumna'><p>Daño pagado</p></td>")
                    }

                    //if (obj.rDESPERFECTO_CONTENEDOR.dCOSTO_REPARACION_MXN == 0)
                    if (obj.rDESPERFECTO_CONTENEDOR.rESTATUS_DESPERFECTO_CONTENEDOR.nFIIDESTATUS_DESPERFECTO_CONTENEDOR == 1)
                        tr.append("<td class='centrarColumna' id='trUpdate'><div type='submit' id='btnUpdateDesperfectoContenedor' class='btnUpdateDesperfectoContenedor'  style='cursor: pointer'  onclick =\"abrirVistaAsignarMonto('" + oDatosURL.sNUM_BL + "'," + oDatosURL.rREFERENCIA.nIdReferencia + ",'" + oDatosURL.rREFERENCIA.sReferencia + "','" + obj.sNUM_CONT + "'," + oDatos.nItinerario + "," + obj.rMONTOS_APLICABLES.doFNMONTO_APLICABLE + ",1)\"><i class='fa fa-pencil' aria-hidden='true'></i></span></div></td>")
                    else
                        tr.append("<td class='centrarColumna' id='trUpdate'><p id='pEstatus'>-</p></td>")
                         
                    $('#tbContenedores').append(tr);
                    $('#tbContenedores').append(tr.addClass("punteroTabla"));
                });
            }
        },
        error: function (xhr, textStatus, errorThrow) {
            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
        },
        complete: function () {
            document.getElementById('dvSpinner').classList.add('invisibilidad');
            if (validarEstatusContenedor() == 2) {

                //Ya no se validará el estatus actual del Bl para  saber si tiener algun tipo de adeudo, esto se realizará ahora desde la ventana del cliente 
                //validarEstatusBL();
                Swal.fire({
                    icon: 'info',
                    html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Depósito en garantía</h1>' +
                        '<p style=\'font-size: 16px;\'> Todos los contenedores fueron marcados como sin daño.</p>',
                    confirmButtonText: 'Aceptar',
                }).then((result) => {
                    if (result.isConfirmed) {
                        window.location = URLGetBLS;
                    }
                });
            }
            else if (validarEstatusContenedor() == 1 ) {
                Swal.fire({
                    icon: 'info',
                    html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Depósito en garantía</h1>' +
                        '<p style=\'font-size: 16px;\'> El depósito en garantía ha sido devuelto al cliente.</p>',
                    button: 'Aceptar'
                });
            }
                
        }
    });
}


//------------------------------------------Funciones para agregar, eliminar o actualizar daño a contenedor-------------------------------------------

function eliminarDanoContenedor(contenedor) {
    var DesperfectoContenedor = cargarDatosUrl();
    var oDatos = {
        nFIIDREFERENCIA: DesperfectoContenedor.rREFERENCIA.nIdReferencia,
        sNUM_CONT: contenedor
    };

    var URL = "/ControlEquipo/DeleteDesperfectoContenedor"

    $.ajax({
        url: URL,
        type: 'POST',
        data: oDatos,
        beforeSend: function () {
            document.getElementById('dvSpinner').classList.remove('invisibilidad');
            document.getElementById('txtBusqueda').disabled = true;
            $('.btnDeleteDesperfectoContenedor').attr('disabled', true)
            $('.btnUpdateDesperfectoContenedor').attr('disabled', true)

            $('.btnDeleteDesperfectoContenedor').css('cursor', 'not-allowed')
            $('.btnUpdateDesperfectoContenedor').css('cursor', 'not-allowed')

            if ($(".btnContenedorSinDesperfectoContenedor")[0] && $(".btnAgregarDesperfectoContenedor")[0]) {
                $('.btnContenedorSinDesperfectoContenedor').attr('disabled', true);
                $('.btnAgregarDesperfectoContenedor').attr('disabled', true);
                $('.btnContenedorSinDesperfectoContenedor').css('cursor', 'not-allowed')
                $('.btnAgregarDesperfectoContenedor').css('cursor', 'not-allowed')
            }
        },
        success: function (result) {
            if (result.correct) {
                cargarElementos();
            }
            else {
                Swal.fire({
                    icon: 'error',
                    html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Cargo por daño a contenedor</h1>' +
                        '<p style=\'font-size: 16px;\'> No se puedo eliminar el cargo por daño al contenedor: <br> <strong style=\'font-size: 16px;\'>' + oDatos.sNUM_CONT + '</strong></p>',
                    button: 'Aceptar',
                    footer: '<p>Error al eliminar cargo</p>'
                });
            }
        },
        error: function (xhr, textStatus, errorThrow) {
            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
        }
    });
}

function actualizarDanoContenedor() {
    const valores = window.location.search;
    const urlParameters = new URLSearchParams(valores);

    var oDatos = {
        dMONTO_USD: document.getElementById('txtCostoDesperfecto').value,
        nFIIDREFERENCIA: urlParameters.get('nIdReferencia'),
        sNUM_CONT: urlParameters.get('NumeroContenedor')
    }
    var sBL = urlParameters.get('sBL');
    var sReferencia = urlParameters.get('sReferencia');
    var nItinerario = urlParameters.get('nItinerario');

    var URL = "/ControlEquipo/UpdateDesperfectoContenedor"

    $.ajax({
        url: URL,
        type: 'POST',
        data: oDatos,
        beforeSend: function () {
            $("#btnAgregarDesperfecto").html('<i class="fa fa-spinner fa-spin"></i> Validando...');
            document.getElementById('btnAgregarDesperfecto').disabled = true;
            document.getElementById('txtCostoDesperfecto').disabled = true;
        },
        success: function (result) {
            if (result.correct) {
                Swal.fire({
                    icon: 'success',
                    html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Actualización de daño al conetenedor</h1>' +
                        '<p style=\'font-size: 16px;\'> Se actualizo el daño del contenedor: <br> <strong style=\'font-size: 16px;\'>' + oDatos.sNUM_CONT + '</strong></p>',
                    confirmButtonText: 'Aceptar'
                }).then(resp => {
                    if (resp.isConfirmed) {
                        location.href = URLGetContenedores + "?sBL=" + sBL + "&nIdReferencia=" + oDatos.nFIIDREFERENCIA + "&sReferencia=" + sReferencia + "&nItinerario=" + nItinerario;
                    } else {
                        location.href = URLGetContenedores + "?sBL=" + sBL + "&nIdReferencia=" + oDatos.nFIIDREFERENCIA + "&sReferencia=" + sReferencia + "&nItinerario=" + nItinerario;
                    }
                });;
            }
            else {
                Swal.fire({
                    icon: 'error',
                    html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Actualización de daño al conetenedor</h1>' +
                        '<p style=\'font-size: 16px;\'> No se puedo actualizar el daño del contenedor: <br> <strong style=\'font-size: 16px;\'>' + oDatos.sNUM_CONT + '</strong></p>',
                    button: 'Aceptar'
                });
            }
        },
        error: function (xhr, textStatus, errorThrow) {
            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
        },
        complete: function () {
            $("#btnAgregarDesperfecto").html('Agregar monto')
            document.getElementById('btnAgregarDesperfecto').disabled = false;
            document.getElementById('txtCostoDesperfecto').disabled = false;
            $("#txtCostoDesperfecto").text("")
        }
    });
}

function agregarDesperfecto() {
    const valores = window.location.search;
    const urlParameters = new URLSearchParams(valores);

    var oDatos = {
        rBL_I: {
            sNUM_BL: urlParameters.get('sBL'),
            nNUM_ITINE: urlParameters.get('nItinerario')
        },
        sFSNUMERO_CONTENEDOR: urlParameters.get('NumeroContenedor'),
        rREFERENCIA: {
            nIdReferencia: urlParameters.get('nIdReferencia'),
            sReferencia: urlParameters.get('sReferencia')
        },
        rUSUARIOS: {
            sFSUSUARIO: sessionStorage.getItem('idUsuario')
        },
        dCOSTO_REPARACION_USD: 0
    }
    oDatos.dCOSTO_REPARACION_USD = document.getElementById('txtCostoDesperfecto').value;


    var URL = "/ControlEquipo/AgregarDesperfectoContenedor"

    $.ajax({
        url: URL,
        type: 'POST',
        data: oDatos,
        beforeSend: function () {
            $("#btnAgregarDesperfecto").html('<i class="fa fa-spinner fa-spin"></i> Validando...');
            document.getElementById('btnAgregarDesperfecto').disabled = true;
            document.getElementById('txtCostoDesperfecto').disabled = true;
        },
        success: function (result) {
            if (result.correct) {
                Swal.fire({
                    icon: 'success',
                    html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Daño del contenedor</h1>' +
                        '<p style=\'font-size: 16px;\'> Se agregó el cargo por daño al contenedor: <br> <strong style=\'font-size: 16px;\'>' + oDatos.sFSNUMERO_CONTENEDOR + '</strong></p >',
                    confirmButtonText: 'Aceptar'
                }).then(resp => {
                    if (resp.isConfirmed) {
                        location.href = URLGetContenedores + "?sBL=" + oDatos.rBL_I.sNUM_BL + "&nIdReferencia=" + oDatos.rREFERENCIA.nIdReferencia + "&sReferencia=" + oDatos.rREFERENCIA.sReferencia + "&nItinerario=" + oDatos.rBL_I.nNUM_ITINE;
                    } else {
                        location.href = URLGetContenedores + "?sBL=" + oDatos.rBL_I.sNUM_BL + "&nIdReferencia=" + oDatos.rREFERENCIA.nIdReferencia + "&sReferencia=" + oDatos.rREFERENCIA.sReferencia + "&nItinerario=" + oDatos.rBL_I.nNUM_ITINE;
                    }
                });;
            }
            else {
                Swal.fire({
                    icon: 'error',
                    html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Daño del contenedor</h1>' +
                        '<p style=\'font-size: 16px;\'>No se puedo agregar el cargo por daño al contenedor: <br> <strong style=\'font-size: 16px;\'>' + oDatos.sFSNUMERO_CONTENEDOR + '</strong></p >',
                    confirmButtonText: 'Aceptar'
                });
            }
        },
        error: function (xhr, textStatus, errorThrow) {
            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
        },
        complete: function () {
            $("#btnAgregarDesperfecto").html('Agregar monto')
            document.getElementById('btnAgregarDesperfecto').disabled = false;
            document.getElementById('txtCostoDesperfecto').disabled = false;
            $("#txtCostoDesperfecto").text("")
        }
    });
}

//----------------------------------------------------------Función  para asignar un contenedor el estatus como "SIN DAÑO"-------------------------------------------

function contenedorSinDano(sNumeroConenedor) {
    var oDatosURL = cargarDatosUrl();
    var oDatos = {
        sFSCONTENEDOR: sNumeroConenedor,
        sFSBL: oDatosURL.sNUM_BL,
        rCAT_ESTATUS_DANO_CONTENEDORES: {
            nFIIDESTATUS_DANO_CONTENEDORES : 1
        }
    }

    var URL = "/ControlEquipo/AddEstatusContenedorSinDesperfecto"

    $.ajax({
        url: URL,
        type: 'POST',
        data: oDatos,
        beforeSend: function () {
            document.getElementById('txtBusqueda').disabled = true;
            document.getElementById('dvSpinner').classList.remove('invisibilidad');
            document.getElementById('txtBusqueda').disabled = true;
            if ($(".btnDeleteDesperfectoContenedor")[0] && $(".btnUpdateDesperfectoContenedor")[0]) {
                $('.btnDeleteDesperfectoContenedor').attr('disabled', true)
                $('.btnUpdateDesperfectoContenedor').attr('disabled', true)
                $('.btnDeleteDesperfectoContenedor').css('cursor', 'not-allowed')
                $('.btnUpdateDesperfectoContenedor').css('cursor', 'not-allowed')
            }

            if ($(".btnContenedorSinDesperfectoContenedor")[0] && $(".btnAgregarDesperfectoContenedor")[0]) {
                $('.btnContenedorSinDesperfectoContenedor').attr('disabled', true);
                $('.btnAgregarDesperfectoContenedor').attr('disabled', true);
                $('.btnContenedorSinDesperfectoContenedor').css('cursor', 'not-allowed')
                $('.btnAgregarDesperfectoContenedor').css('cursor', 'not-allowed')
            }
        },
        success: function (result) {
            if (result.correct) {
                cargarElementos();
            }
            else {
                Swal.fire({
                    icon: 'error',
                    html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Estatus del contenedor</h1>' +
                        '<p style=\'font-size: 16px;\'> No se pudo cambiar el estatus del contenedor : <br> <strong style=\'font-size: 16px;\'>' + oDatos.sFSCONTENEDOR + '</strong></p>',
                    button: 'Aceptar',
                    footer: '<p>Error al cambiar el estatus</p>'
                });
            }
        },
        error: function (xhr, textStatus, errorThrow) {
            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
        }
    });
}




//---------------------------Funciones para validar el monto que se agrega como daño, que sea mayor a 0 que sean números y que no exeda el deposito en garantía----------------------------------------
//---------------------------Esto se muestra en la vista de ControEquipo  del controlador ControlEquipo

function validacionTipoDano() {
    const valores = window.location.search;
    const urlParameters = new URLSearchParams(valores);
    var idTipoOperacion = urlParameters.get('nTipoOperacion')
    var costoReparacion = document.getElementById('txtCostoDesperfecto').value;
    costoReparacion = costoReparacion != "" ? parseFloat(costoReparacion, 10) : 0;
    var costoDG = parseInt(urlParameters.get('dMontoDG'));
    if (costoReparacion == 0 ) {
        Swal.fire({
            icon: 'error',
            html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Monto del daño</h1>' +
                '<p style=\'font-size: 16px;\'>No pudes asignar un daño de $0 USD</p >',
            button: 'Aceptar'
        });
    }
    else if (costoReparacion > costoDG) {
        Swal.fire({
            icon: 'warning',
            html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Monto del daño</h1>' +
                '<p style=\'font-size: 16px;\'> El costo del daño es mayor al valor del depósito en garantía(' + costoDG + ' USD), no se podra solicitar la devolúción. ¿Desea continuar?</p >' ,
            showCancelButton: true,
            cancelButtonColor: '#d33',
            confirmButtonColor: '#008CBA',
            confirmButtonText: 'Continuar'
        }).then((result) => {
            if (result.isConfirmed) {
                if (idTipoOperacion == 0)
                    agregarDesperfecto();
                else 
                    actualizarDanoContenedor();
            }
        });
    }
    else {
        if (idTipoOperacion == 0) {
            agregarDesperfecto();
        }
        else {
            actualizarDanoContenedor();
        }
    }
}

function abrirVistaAsignarMonto(sBL, nIdReferencia, sReferencia, sContenedor, nItinerario, dMontoDG, nTipoOperacion) {
    location.href = URLControlEquipo + "?sBL=" + sBL + "&nIdReferencia=" + nIdReferencia + "&sReferencia=" + sReferencia + "&NumeroContenedor=" + sContenedor + "&nItinerario=" + nItinerario + "&nTipoOperacion=" + nTipoOperacion + "&dMontoDG=" + dMontoDG;
}



function validarSoloNumeros(e, labelName, txtName) {
    var regex = /^[0-9]{1,10}$/;
    var letra = e.key;
    if (regex.test(letra) || letra == 'Enter') {
        $('#' + labelName).text("");
        $('#' + txtName).removeClass('border-danger');
        return true;
    }
    else {
        $('#' + labelName).text("Solo se permiten numeros");
        $('#' + labelName).css({ "color": "red" })
        $('#' + txtName).addClass('border-danger');
        return false;
    }
}

function error(labelName, txtName) {
    $('#' + labelName).text("");
    $('#' + txtName).removeClass('border-danger');
}

$("#txtCostoDesperfecto").keyup(function (event) {
    if (event.keyCode === 13) {
        validacionTipoDano();
    }
});




//Codigo comentado


////var URLGetContenedores = "http://localhost:8153/ControlEquipo/GetContenedores";
////var URLGetBLS = "http://localhost:8153/ControlEquipo/GetBLS";
////var URLControlEquipo = "http://localhost:8153/ControlEquipo/ControlEquipo";

//function solicitarDevolucionDG() {
//    let sURL = '/DevolucionesAutomaticas/SolicitarDevolucionSaldo';

//    var oDatosURL = cargarDatosUrl();
//    var oDatos = {
//        nIdReferencia: oDatosURL.rREFERENCIA.nIdReferencia,
//        sBL: oDatosURL.sNUM_BL,
//        nNumeroItinerario: oDatosURL.nItinerario
//    }

//    $.ajax({
//        url: sURL,
//        type: 'POST',
//        data: oDatos,
//        beforeSend: function () {
//            document.getElementById('dvSpinner').classList.remove('invisibilidad');
//        },
//        success: function (result) {
//            if (result) {
//                //Se leen las celdas que contienen el numero de los contenedores para adjuntarlos a la alerta
//                var cadenaContenedores = "";
//                let tabla = document.getElementById("tContenedores");
//                var filas = tabla.rows.length;
//                for (let i = 1; i < filas; i++) {
//                    cadenaContenedores += '<br>  <strong style=\'font-size: 16px;\'> ' + tabla.rows[i].cells[1].innerText + ' </strong> ';
//                }

//                Swal.fire({
//                    icon: 'success',
//                    html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Solicitud Devolucion DG</h1>' +
//                        '<p style=\'font-size: 16px;\'> Se envio la solicitud de devolucion para los contenedores: ' + cadenaContenedores + '</p>',
//                    confirmButtonText: 'Aceptar',
//                    footer: '<p>Solicitud exitosa</p>'
//                }).then(resp => {
//                    if (resp.isConfirmed) {
//                        location.href = URLGetBLS;
//                    } else {
//                        location.href = URLGetBLS;
//                    }
//                });;
//            }
//            else {
//                Swal.fire({
//                    icon: 'error',
//                    html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Solicitud Devolucion DG</h1>' +
//                        '<p style=\'font-size: 16px;\'> No se pudo solicitar la devolución para los contenedores: ' + cadenaContenedores + '</p>',
//                    button: 'Aceptar',
//                    footer: '<p>Error al solicitar devolución</p>'
//                });
//            }

//        },
//        error: function (xhr, textStatus, errorThrow) {
//            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
//        },
//        complete: function () {
//            document.getElementById('dvSpinner').classList.add('invisibilidad');
//        }
//    });
//}


//function validarEstatusContenedor() {
//    var validacion = leerCeldas(10, "SIN ESTATUS", "CON DAÑO");
//    if (validacion == false) {
//        //Se deshabilita el boton
//        return false;
//    } else {
//        validacion = leerCeldas(13, "EN PROCESO", "DEVUELTO");
//        if (validacion) {
//            //Se habilita el boton
//            return true;
//        }
//        else {
//            //Se deshabilita el boton
//            return false;
//        }
//    }
//    return true;
//}


//document.querySelector('#btnAgregarDesperfecto').addEventListener('click', () => {
//    validacionTipoDano();
//});

//document.querySelector('#txtBusqueda').addEventListener('keyup', function () {
//    var sBusqueda = document.getElementById('txtBusqueda').value;

//    if (sBusqueda.length >= 3 || sBusqueda.length == 0) {
//        cargarContenedoresByBL();
//    }
//});

//document.querySelector('#txtBusqueda').addEventListener('blur', function () {
//    var sBusqueda = document.getElementById('txtBusqueda').value;

//    if (sBusqueda.length >= 3 || sBusqueda.length == 0) {
//        cargarContenedoresByBL();
//    }
//});


//----------------------Se comentó el evento que ejecutaba la función para solicitar la devolución de DG ya que esto lo hara el usuario de tesoreria----------
//document.querySelector('#btn_SolicitarDevolucionDeposito').addEventListener('click', () => {
//    solicitarDevolucionDG();
//});

//function validarEstatusBL() {
//    let sURL = '/ControlEquipo/ValidarDevolucionRemesa';
//    var oDatosURL = cargarDatosUrl();
//    var oDatos = {
//        sNumBL: oDatosURL.sNUM_BL,
//        nItinerario: oDatosURL.nItinerario
//    }

//    $.ajax({
//        url: sURL,
//        type: 'POST',
//        data: oDatos,
//        beforeSend: function () {
//            document.getElementById('dvSpinner').classList.remove('invisibilidad');
//        },
//        success: function (result) {
//            if (result.correct) {
//                document.getElementById('btn_SolicitarDevolucionDeposito').disabled = false;
//                Swal.fire({
//                    icon: 'info',
//                    html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>Depósito en Garantía</h1>' +
//                        '<p style=\'font-size: 16px;\'> Todos los contenedores fueron marcados como sin daño y no se tienen adeudos pendientes. Puede solicitar la Devolución </p>',
//                    button: 'Aceptar'
//                });
//                //solicitarDevolucionDG();
//            }
//            else {
//                var flag = 0;
//                var cadena = "";
//                if (result.object.dADEUDO_FLETES != 0) {
//                    cadena += " <br> Fletes : <strong style=\'font-size: 16px;\'> $" + convertirFormatoMoneda(result.object.dADEUDO_FLETES) + " USD </strong> ";
//                    flag = 1;
//                }
//                if (result.object.dADEUDO_DEMORAS != 0) {
//                    cadena += " <br> Demoras : <strong style=\'font-size: 16px;\'> $" + convertirFormatoMoneda(result.object.dADEUDO_DEMORAS) + " USD </strong> ";
//                    flag = 1;
//                }
//                if (result.object.dADEUDO_DANIO_CONTENEDOR != 0) {
//                    cadena += " <br> Daño contenedores : <strong style=\'font-size: 16px;\'> $" + convertirFormatoMoneda(result.object.dADEUDO_DANIO_CONTENEDOR) + " USD </strong> ";
//                    flag = 1;
//                }
//                if (flag == 0) {
//                    cadena = " <br>  <strong style=\'font-size: 16px;\'> Se produjo un error </strong>";
//                }

//                Swal.fire({
//                    icon: 'error',
//                    html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>No se puede solicitar la dovolución de DG</h1>' +
//                        '<p style=\'font-size: 16px;\'> Se tienen los siguientes adeudos: '+ cadena +'</p>',
//                    button: 'Aceptar'
//                });
//            }

//        },
//        error: function (xhr, textStatus, errorThrow) {
//            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
//        },
//        complete: function () {
//            document.getElementById('dvSpinner').classList.add('invisibilidad');         
//        }
//    });
//}

/*Terminan Eventos*/