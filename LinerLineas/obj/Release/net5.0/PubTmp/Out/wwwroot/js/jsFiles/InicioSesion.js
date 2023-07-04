$(document).ready(function () {
    //sessionStorage.clear();
    //$('#txtUsuario').focus();
});

/********** Funciones **********/

function MuestraTexto(id) {
    var txt = document.getElementById(id);
    var i = document.getElementById(id.replace("txt", "i"));

    if (txt.type === "password") {
        txt.type = "text";
    } else {
        txt.type = "password";
    }

    if (i.classList.contains("bi-eye-slash")) {
        i.classList.remove("bi-eye-slash");
        i.classList.add("bi-eye");
    } else {
        i.classList.add("bi-eye-slash");
        i.classList.remove("bi-eye");
    }
}

function ValidarUsuario(objeto)
{
    $.ajax({
        url: "/InicioSesion/ValidarUsuario",
        type: 'POST',
        data: objeto,
        success: function (result) {
            if (result) {
                window.location.href = '/Principal';
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Inicio de sesión',
                    text: 'Error al cargar la sesión',
                    footer: '<p>Inicio de sesión fallido</p>'
                });
            }
        },
        error: function (xhr, textStatus, errorThrow) {
            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
        }
    });
}

/********** Funciones **********/

/********** Eventos **********/

document.getElementById('btnAceptar').addEventListener('click', function (e) {
    e.preventDefault();

    let usuario = document.getElementById('txtUsuario').value;
    let contrasenia = document.getElementById('txtContrasena').value;

    if (usuario == '') {
        Swal.fire({
            icon: 'error',
            title: 'Usuario',
            text: 'Por favor proporcione el nombre de usuario o su correo',
            footer: '<p>Campos no proporcionados</p>'
        });

        bValida = false;
    } else if (contrasenia == '') {
        Swal.fire({
            icon: 'error',
            title: 'Contraseña',
            text: 'Por favor proporcione su contraseña',
            footer: '<p>Campos no proporcionados</p>'
        });

        bValida = false;
    } else if (contrasenia == '' && usuario == '') {
        Swal.fire({
            icon: 'error',
            title: 'Usuario y Contraseña',
            text: 'Por favor proporcione un usuario y contraseña',
            footer: '<p>Campos no proporcionados</p>'
        });

        bValida = false;
    } else {
        var oUsuarios = new Object();
        oUsuarios.sFSUSUARIO = usuario;
        oUsuarios.sFSCONTRASENA = contrasenia;

        $.ajax({
            url: "/InicioSesion/IniciarSesion",
            type: 'POST',
            data: oUsuarios,
            beforeSend: function () {
                $("#btnAceptar").html('<i class="fa fa-spinner fa-spin"></i> Validando...');
                //$("#btnAceptar").html('<i class="fa fa-spinner"></i> Validando...');
            },
            success: function (result) {
                    if (result.correct) {
                        sessionStorage.setItem("idUsuario", result.object.nFIIDUSUARIO)
                        sessionStorage.setItem("usuario", result.object.sFSUSUARIO)
                        sessionStorage.setItem("correo", result.object.sFSCORREO)
                        sessionStorage.setItem("nombre", result.object.sFSNOMBRE)
                        sessionStorage.setItem("apellidos", result.object.sFSAPELLIDOS)
                        sessionStorage.setItem("idPerfil", result.object.rUSUARIOS_REL_PERFILES.rPERFILES.nFIIDPERFIL)
                        sessionStorage.setItem("perfil", result.object.rUSUARIOS_REL_PERFILES.rPERFILES.sFSPERFIL)
                        sessionStorage.setItem("idRol", result.object.rUSUARIOS_REL_ROLES.rROLES.nFIIDROL)
                        sessionStorage.setItem("rol", result.object.rUSUARIOS_REL_ROLES.rROLES.sFSROL)

                        //sessionStorage.setItem("idCliente", data.oCliente.fiidcliente)
                        if (result.object.rUSUARIOS_REL_ROLES.rROLES.sFSROL == "CONTROL DE EQUIPO") {
                            sessionStorage.setItem("linea", result.object.rUSUARIOS_REL_LINEAS.rLINEAS.nFIIDLINEA)
                        }
                        else {
                            sessionStorage.setItem("idCliente", result.object.rUSUARIOS_REL_CLIENTES.rCLIENTES.nFIIDCLIETE)
                            sessionStorage.setItem("recIdAX", result.object.rUSUARIOS_REL_CLIENTES.rCLIENTES.nlFIRECIDCLIENTE_AX)
                        }
                        ValidarUsuario(result.object);
                        //window.location.href = '/Principal';
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Inicio de sesión',
                            text: 'El usuario y/o contraseña fueron incorrectos',
                            footer: '<p>Inicio de sesión fallido</p>'
                        });
                    }                    
                
            },
            error: function (xhr, textStatus, errorThrow) {
                console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
            },
            complete: function () {
                $("#btnAceptar").html('Iniciar');
            }
        });
    }
});

/********** Finaliza Eventos **********/