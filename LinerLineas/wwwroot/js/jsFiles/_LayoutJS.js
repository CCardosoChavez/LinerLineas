$(document).ready(function () {
    //validaMenu();
    reloadPageCache()
    reducirPleca();
    ObtenerIdUsuario();
});

//var timer = 1000;
//window.onload = function () {
//    timer = 5000;
//    setInterval(() => {
//        validaSesion();
//    }, timer);    
//}


function ObtenerIdUsuario() {
    if ($('#txtDatos')[0]) {
        const valores = window.location.search;
        const urlParameters = new URLSearchParams(valores);
        document.getElementById('txtDatos').value = urlParameters.get('nIdUsuario');
        var idUsuario = urlParameters.get('nIdUsuario');
        ObtenerDatosUsuario(idUsuario);
    }
    else {
        var idUsuario = sessionStorage.getItem("idUsuario");
        ObtenerDatosUsuario(idUsuario);
    }
}

function ObtenerDatosUsuario(idUsuario) {
    var oUsuario = new Object();
    oUsuario.sId = idUsuario;
    $.ajax({
        url: "/InicioSessionPagoReferenciado/GetDatosUsuario",
        type: 'POST',
        data: oUsuario,
    beforeSend: function () {
     },
     success: function (result) {
         if (result.correct) {
             sessionStorage.setItem("idUsuario", result.object.sId)
             sessionStorage.setItem("usuario", result.object.sUserName)
             sessionStorage.setItem("correo", result.object.sEmail)
             sessionStorage.setItem("nombre", result.object.sNombre)
             sessionStorage.setItem("apellidos", result.object.sApellidoP)
             sessionStorage.setItem("rol", result.object.rASPNET_USERS_ROLES.rASPNER_ROLES.sName)

              //sessionStorage.setItem("idCliente", data.oCliente.fiidcliente)
             if (result.object.rASPNET_USERS_ROLES.rASPNER_ROLES.sName == "Operativo_PIL" || result.object.rASPNET_USERS_ROLES.rASPNER_ROLES.sName == "Operativo_YangMing" || result.object.rASPNET_USERS_ROLES.rASPNER_ROLES.sName == "Operativo_Oceanus") {
                 sessionStorage.setItem("linea", result.object.rLINEAS_USUARIO.rLINEAS.nNumLinea)
             }
             else if (result.object.rASPNET_USERS_ROLES.rASPNER_ROLES.sName == "Clientes") {
                 sessionStorage.setItem("idCliente", result.object.rCLIENTES_USUARIOS.rCLIENTE.nIdCliente)
             }
               
              validaMenu();
         }
         else {
               Swal.fire({
               icon: 'error',
               title: 'Inicio de sesión',
               text: 'El usuario  no encontrado',
               footer: '<p>Inicio de sesión fallido</p>'
               });
               document.getElementById('liReferencias').classList.add("invisibilidad");
               document.getElementById('liControlEquipoDevAuto').classList.add("invisibilidad");
         }
     },
      error: function (xhr, textStatus, errorThrow) {
      console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
      },
      });
}

function validaMenu() {
    if (sessionStorage.getItem('rol') == "Operativo_PIL" || sessionStorage.getItem('rol') == "Operativo_YangMing" || sessionStorage.getItem('rol') == "Operativo_Oceanus") {
        document.getElementById('pleca_Menu_ControlEquipo_Enlace').hidden = false;
        document.getElementById('pleca_Menu_Referencias_Enlace').hidden = true;
        if (document.getElementById('txt_Bandera_Menu').value == '') {
            location.href = pathControlEquipo
        }     
    }
    //Solo podran ver estas opciones los roles de cliente y agente aduanal
    else if (sessionStorage.getItem('rol') == "Clientes") {

        if (sessionStorage.getItem("idCliente") != 0) {
            /*document.getElementById('liReferencias').hidden = false;*/
            document.getElementById('pleca_Menu_Referencias_Enlace').hidden = false;
            document.getElementById('pleca_Menu_ControlEquipo_Enlace').hidden = true;
            if (document.getElementById('txt_Bandera_Menu').value == '') {
                location.href = pathReferencias
            }        
        }

        else {
            Swal.fire({
                icon: 'error',
                title: 'Sin cliente',
                text: 'Agrega un cliente para poder generar referencias de Depósitos en Garantía',
                footer: '<p>Sin cliente asignado</p>'
            });
        }
    }
    else if (sessionStorage.getItem('rol') == "Admin") {
        sessionStorage.setItem("linea", 0)
    //    document.getElementById('liEnlaceControlEquipo').hidden = false;
    //    document.getElementById('liEnlaceReferencias').hidden = false;
        document.getElementById('pleca_Menu_ControlEquipo_Enlace').hidden = false;
        document.getElementById('pleca_Menu_Referencias_Enlace').hidden = false;
        document.getElementById('pleca_Menu_Tesoreria_Enlace').hidden = false;
        if (document.getElementById('txt_Bandera_Menu').value == '') {
            location.href = pathReferencias
        }
        
    }
    else if (sessionStorage.getItem('rol') == "Tesoreria") {
        document.getElementById('pleca_Menu_ControlEquipo_Enlace').hidden = true;
        document.getElementById('pleca_Menu_Referencias_Enlace').hidden = true;
        document.getElementById('pleca_Menu_Tesoreria_Enlace').hidden = false;
        if (document.getElementById('txt_Bandera_Menu').value == '') {
            location.href = pathTesoreria
        }
    }

    else {
        Swal.fire({
            icon: 'error',
            title: 'Inhabilitado',
            text: 'No existen opciones disponibles para tu usuario',
            footer: '<p>Sin opciones</p>'
        });
    }
}


function validaSesion() {
    //debugger;
    console.log('Valida sesión');
    var s = sessionStorage.getItem('idUsuario');
    if (sessionStorage.getItem('idUsuario') === null || sessionStorage.getItem('idUsuario') === 0 || sessionStorage.getItem('idUsuario') === undefined) {
        window.location.href = '/';
    }
}

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

function cerrarSesion() {
    $.ajax({
        url: "/InicioSesion/CerrarSesion",
        type: 'GET',
        success: function (result) {
            if (result) {
                sessionStorage.clear();
                timer = 1000;
                window.location.href = '/InicioSesion';
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Inicio de sesión',
                    text: 'Error al cerrar sesión',
                    footer: '<p>Inicio de sesión fallido</p>'
                });
            }
        },
        error: function (xhr, textStatus, errorThrow) {
            console.log('Error: ' + xhr.status + ' || ' + xhr.readyState + ' || ' + xhr.responseText + ' || ' + xhr.statusText + ' || ' + textStatus + ' || ' + errorThrow);
        },
    });
}

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
