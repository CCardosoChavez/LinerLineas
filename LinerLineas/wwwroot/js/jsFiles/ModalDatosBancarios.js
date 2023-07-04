
function validarArchivo() {
    var fileInput = document.getElementById('filesB');
    var filePath = fileInput.value;
    var allowedExtensions = /(.pdf)$/i;
    if (!allowedExtensions.exec(filePath)) {
        alert('Solo se pueden subri archivos con extensión .pdf');
        fileInput.value = '';
        document.getElementById("txtFile").value = "";
        document.getElementById('modal_btnAceptar').disabled = true;
        $('#modal_btnAceptar').css('cursor', 'not-allowed');
        return false;
    }
    else {
        document.getElementById('modal_btnAceptar').disabled = false;
        $('#modal_btnAceptar').css('cursor', 'pointer');
        $('#modal_btnAceptar').css('opacity', '1');

        return true;
    }
}

document.querySelector('#filesB').addEventListener('change', () => {

    var dato = document.getElementById("txtFile");

    var inputValue = document.querySelector('#filesB').value;

    var filePath = inputValue.toString().split('\\');

    dato.value = filePath[filePath.length - 1];

    validarArchivo();
});

function validacionesTextbox(e, labelName, txtName, validacion) {
    const regexLetras = /^[A-Za-zÑñÁáÉéÍíÓóÚúÜü\s]{1,50}$/;
    const regexNumeros = /^[0-9]{1,10}$/;
    const regexLetrasNumeros = /^[A-Za-z0-9\s]{1,10}$/;
    var mensaje = "";
    switch (validacion) {
        case "letras":
            mensaje = "Solo se permiten letras"
            return onKeyPress(e, labelName, txtName, regexLetras, mensaje);
            break;
        case "numeros":
            mensaje = "Solo se permiten numeros"
            return onKeyPress(e, labelName, txtName, regexNumeros, mensaje);
            break;
        case "letrasNumeros":
            mensaje = "Solo se pueden usar letras o numeros"
            return onKeyPress(e, labelName, txtName, regexLetrasNumeros, mensaje);
            break;
        default:
            return false;
            break;
    }
}

function onKeyPress(e, labelName, txtName, regex, mensaje) {
    var letra = e.key;
    if (e.keyCode !== 8 && e.keyCode !== 9) {
        if (regex.test(letra)) {
            $('#' + labelName).text("");
            $('#' + txtName).removeClass('border-danger');
            return true;
        }
        else {
            $('#' + labelName).text(mensaje);
            $('#' + labelName).css({ "color": "red" })
            $('#' + txtName).addClass('border-danger');
            return false;
        }
    }    
}


function datosBancariosGuardados() {
    switch (ResultDatosBancarios) {
        case "0":
            Swal.fire({
                icon: 'error',
                html: '<h1 style=\'font-size: 20px; font-weight: bold;\'>Devolución depósito en garantía</h1>' +
                    '<p style=\'font-size: 16px;\'>Error al guardar la información. </p>',
                button: 'Aceptar'
            });
            break;
        case "1":
            Swal.fire({
                icon: 'success',
                html: '<h1 style=\'font-size: 20px; font-weight: bold;\'>Devolución depósito en garantía</h1>' +
                    '<p style=\'font-size: 16px;\'>Se envíó la solictud con exito.</p>',
                button: 'Aceptar'
            });
            break;
        case "2":
            Swal.fire({
                icon: 'error',
                html: '<h1 style=\'font-size: 20px; font-weight: bold;\'>Devolución depósito en garantía</h1>' +
                    '<p style=\'font-size: 16px;\'> No se pudo solicitar la devolución.</p>',
                button: 'Aceptar'
            });
            break;
        case "3":
            Swal.fire({
                icon: 'succes',
                html: '<h1 style=\'font-size: 20px; font-weight: bold;\'>Devolución depósito en garantía</h1>' +
                    '<p style=\'font-size: 16px;\'> No se pudo guardar el archivo.</p>',
                button: 'Aceptar'
            });
            break;
        case "4":
            Swal.fire({
                icon: 'error',
                html: '<h1 style=\'font-size: 20px; font-weight: bold;\'>Devolución depósito en garantía</h1>' +
                    '<p style=\'font-size: 16px;\'>No se pudo leer el archivo. </p>',
                button: 'Aceptar'
            });
            break;
        case "5":
            Swal.fire({
                icon: 'error',
                html: '<h1 style=\'font-size: 20px; font-weight: bold;\'>Devolución depósito en garantía</h1>' +
                    '<p style=\'font-size: 16px;\'>No puede haber campos vacíos, intente de nuevo.</p>',
                button: 'Aceptar'
            });
            break;
        case "6":
            Swal.fire({
                icon: 'error',
                html: '<h1 style=\'font-size: 20px; font-weight: bold;\'>Devolución depósito en garantía</h1>' +
                    '<p style=\'font-size: 16px;\'>La clabe de la cuenta debe de contar con 18 dígitos.</p>',
                button: 'Aceptar'
            });
            break;
        case "7":
            Swal.fire({
                icon: 'error',
                html: '<h1 style=\'font-size: 20px; font-weight: bold;\'>Devolución depósito en garantía</h1>' +
                    '<p style=\'font-size: 16px;\'>La cuenta debe de contar con 10 dígitos como minimo.</p>',
                button: 'Aceptar'
            });
            break;
        default:
            break;
    }
}

function validarAdeudosDelLaReferencia(estatusContenedoresControlEquipo, itinerario, numeroBL, idReferencia, referencia, idDatosBancarios) {
    let sURL = '/Referencias/ValidarAdeudosBL';
    var oDatos = {
        sNumBL: numeroBL,
        nItinerario: itinerario
    }

    $.ajax({
        url: sURL,
        type: 'POST',
        data: oDatos,
        beforeSend: function () {
            document.getElementById('dvSpinner').classList.remove('invisibilidad');
        },
        success: function (result) {
            if (result.correct) {
                //Abre el formulario
                    document.getElementById("Formulario_IdReferencia").value = idReferencia;
                    document.getElementById("Formulario_sNumeroReferencia").value = referencia;
                    document.getElementById("Formulario_nIdDatosBancarios").value = idDatosBancarios;
                    document.getElementById("Formulario_sIdUsuario").value = sessionStorage.getItem('idUsuario');

                    var modal = document.getElementById('modalDatosBancarios');
                    modal.classList.add("modal--show");
                
            }
            else {
                var flag = 0;
                var cadena = "";
                if (result.object.dADEUDO_FLETES != 0) {
                    cadena += " <br> Fletes : <strong style=\'font-size: 16px;\'> $" + convertirFormatoMoneda(result.object.dADEUDO_FLETES) + " USD </strong> ";
                    flag = 1;
                }
                if (result.object.dADEUDO_DEMORAS != 0) {
                    cadena += " <br> Demoras : <strong style=\'font-size: 16px;\'> $" + convertirFormatoMoneda(result.object.dADEUDO_DEMORAS) + " USD </strong> ";
                    flag = 1;
                }
                if (result.object.dADEUDO_DANIO_CONTENEDOR != 0) {
                    cadena += " <br> Daño a contenedores : <strong style=\'font-size: 16px;\'> $" + convertirFormatoMoneda(result.object.dADEUDO_DANIO_CONTENEDOR) + " USD </strong> ";
                    flag = 1;
                }
                if (flag == 0) {
                    cadena = " <br>  <strong style=\'font-size: 16px;\'> Se produjo un error </strong>";
                }

                Swal.fire({
                    icon: 'error',
                    html: '<h1 style=\'font-size: 23px; font-weight: bold;\'>No se puede solicitar la devolución de DG</h1>' +
                        '<p style=\'font-size: 16px;\'> Se tienen los siguientes adeudos: ' + cadena + '</p>',
                    button: 'Aceptar'
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

function CerrarModal() {
    //Restaurar todos los elementos del modal al cerrarlo
    LimpiarModal();
    var modal = document.getElementById('modalDatosBancarios');
    modal.classList.remove("modal--show");
    //location.reload();
}

function LimpiarModal() {
    var fileInput = document.getElementById('filesB');
    fileInput.value = '';
    document.getElementById("txtFile").value = "";

    document.getElementById("Formulario_IdReferencia").value = "";
    document.getElementById("Formulario_sNumeroReferencia").value = "";
    document.getElementById("Formulario_txtRazonSocial").value = "";
    document.getElementById("Formulario_ddlBanco").value = "";
    //var banco = document.getElementById("Formulario_ddlBanco");
    //banco.options[0].value;
    document.getElementById("Formulario_txtNumeroCuenta").value = "";
    document.getElementById("Formulario_txtNumeroCuentaClave").value = "";
    document.getElementById("Formulario_txtCorreoContacto").value = "";

    //Cambiar el color de los bordes
    $("#Formulario_txtCorreoContacto").removeClass('border-danger');
    $('#Formulario_txtNumeroCuentaClave').removeClass('border-danger');
    $('#Formulario_txtNumeroCuenta').removeClass('border-danger');

    $("#Formulario_lblError").text("");

    document.getElementById("modal_btnAceptar").disabled = true;
    $('modal_btnAceptar').css("cursor", "not-allowed");
}

document.querySelector('#modal_Boton_Salir').addEventListener('click', (e) => {
    CerrarModal();
});


document.querySelector('#Formulario_txtNumeroCuenta').addEventListener('blur', function () {
    let txtNumCuenta = document.getElementById('Formulario_txtNumeroCuenta').value;
    if (txtNumCuenta.length < 10) {
        $("#Formulario_lblError").text("El número de cuenta bancaria debe de contener 10 dígitos minimo");
        $('#Formulario_lblError').css({ "color": "red" })
        $('#Formulario_txtNumeroCuenta').addClass('border-danger');
    }
});

document.querySelector('#Formulario_txtNumeroCuentaClave').addEventListener('blur', function () {
    var txtClbCuenta = document.getElementById('Formulario_txtNumeroCuentaClave').value;
    if (txtClbCuenta.length <18) {
        $("#Formulario_lblError").text("El número de la clabe bancaria debe de contener 18 dígitos");
        $('#Formulario_lblError').css({ "color": "red" })
        $('#Formulario_txtNumeroCuentaClave').addClass('border-danger');
    }
});


document.querySelector('#Formulario_txtCorreoContacto').addEventListener('blur', function () {
    let funcionRegexEmail = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
    var txtCorreoContacto = document.getElementById('Formulario_txtCorreoContacto').value;

    let result = funcionRegexEmail.test(txtCorreoContacto);
    if (!result) {
        $("#Formulario_lblError").text("El formato de correo es incorrecto.");
        $('#Formulario_lblError').css({ "color": "red" })
        $('#Formulario_txtCorreoContacto').addClass('border-danger');
    }
    else{
        $("#Formulario_lblError").text("");
        $("#Formulario_txtCorreoContacto").removeClass('border-danger');
        return true;
    }
});


