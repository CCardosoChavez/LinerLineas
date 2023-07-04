document.querySelector("#pleca_MenuDesplegable").addEventListener('click', function () {
    reducirPleca();
});

function reducirPleca() {
    document.getElementById('divSecundario').classList.remove('divSecundario');
    document.getElementById('divSecundario').classList.add('divSecundario_AnimacionPleca');
    //document.getElementById('pleca_MenuDesplegable_Icon').classList.add('pleca_MenuDesplegable_Icon_AnimacionPleca');
    document.getElementById('pleca_MenuDesplegable_Icon').hidden= true;
    document.getElementById('plecaDePosicionamiento').classList.add('plecaDePosicionamiento_AnimacionPleca');
    document.getElementById('divIconoMenu').classList.add('divIconoMenu_AnimacionPleca');
    document.getElementById('pleca').classList.add('pleca_AnimacionPleca');
    document.getElementById('pleca_Logo').classList.add('pleca_Logo_AnimacionPleca');
    document.getElementById('pleca_Logo_Imagen').classList.add('pleca_Logo_Imagen_AnimacionPleca');
    document.getElementById('pleca_Menu').classList.add('pleca_Menu_AnimacionPleca');

    //Clases para animar el menú de la pleca
    $('.pleca_Menu_Enlace').addClass('pleca_Menu_Enlace_AnimacionPleca');
    $('.pleca_Menu_Icon').addClass('pleca_Menu_Icon_AnimacionPleca');
    $('.pleca_Menu_Title').hide();
}

function agrandarPleca() {
    document.getElementById('divSecundario').classList.add('divSecundario');
    document.getElementById('divSecundario').classList.remove('divSecundario_AnimacionPleca');
    document.getElementById('plecaDePosicionamiento').classList.remove('plecaDePosicionamiento_AnimacionPleca');
    document.getElementById('divIconoMenu').classList.remove('divIconoMenu_AnimacionPleca');
    document.getElementById('pleca').classList.remove('pleca_AnimacionPleca');
    document.getElementById('pleca_Logo').classList.remove('pleca_Logo_AnimacionPleca');
    document.getElementById('pleca_Logo_Imagen').classList.remove('pleca_Logo_Imagen_AnimacionPleca');
    document.getElementById('pleca_Menu').classList.remove('pleca_Menu_AnimacionPleca');

    document.getElementById('pleca_MenuDesplegable_Icon').hidden = false;

    //Clases para animar el menú de la pleca
    $('.pleca_Menu_Enlace').removeClass('pleca_Menu_Enlace_AnimacionPleca');
    $('.pleca_Menu_Icon').removeClass('pleca_Menu_Icon_AnimacionPleca');
    $('.pleca_Menu_Title').show(250);

   /* mostrarOpciones();*/
}





//Codigo Comentado

//document.querySelector("#pleca_Menu_Referencias_Enlace").addEventListener('mouseover', function () {
//    agrandarPleca();
//});

//document.querySelector("#pleca_Menu_ControlEquipo_Enlace").addEventListener('mouseover', function () {
//    agrandarPleca();
//});

//function mostrarOpciones() {
//    setTimeout(function () {
//        $('.pleca_Menu_Title').show();
//    }, 120);
    
//}