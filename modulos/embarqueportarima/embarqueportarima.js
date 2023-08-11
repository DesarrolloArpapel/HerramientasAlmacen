import { embarqueportarimaAjax } from "./embarqueportarimaAjax.js";

export function embarqueportarima (){
     
    //? Llamamos la función cambiarTema para editar la paleta de colores
     cambiarTema();

     //? Borramos la opcion anterior que se estaba ejecutando y nombramos el titulo
     $('#opcion').empty();                                                 
     $('.navbar').text("Embarque por tarima");     

     //? Agregaoms los botones input:folio y input:buscar
     $('#opcion').append(  
        '<input id="folio" class="form" type="text" placeholder="Ingresa el Folio">'+  
        '<input id="buscar" class="form-btn" type="button" value="Buscar" >'+
        '<h1 id="titulo"></h1>'+
        '<div id="cuerpo_opcion"></div>'
     );
    
     //? Evento al realizar click en el input:buscar
     $("#buscar").click(function(){ 

        //? Tomamos los valores de los input:folio y input:buscar
        var folio = $('#folio').val()
        embarqueportarimaAjax(folio);
     })
}

function cambiarTema(){
    $('body').css({
        'background-color': "rgb(212, 232, 255)",
        'transition': '0.5s'
    });

    $('.menu').css({
        'box-shadow':'0 0 20vw 1vw #4d6679',
        'transition': '0.5s'
    });

    $('.navbar').css({
        'background-color':'#4d6679',
        'transition': '0.5s'
    });

    $('.cuerpo_menu').css({
        'background-color': 'rgb(203, 244, 255)',
        'transition': '0.5s'
    });

    $('.opcion').css({
        'border-bottom': 'solid 1px #4d6679',
        'color':'#4d6679',
        'transition': '0.5s'
    });
}