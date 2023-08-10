import { listaSurtimientoAjax } from "./listaSurtimientoAjax.js";

export function listaSurtimiento (){
    
    //? Borramos la opcion anterior que se estaba ejecutando
    $('#opcion').empty();                                                 
    
    //? Agregaoms los botones input:folio y input:buscar
    $('#opcion').append(  
        '<input id="folio" class="form" type="text" placeholder="Ingresa el Folio">'+  
        '<input id="buscar" class="form-btn" type="button" value="Buscar" >'+
        '<div id="cuerpo_opcion"></div>'

    );
    
    //? Evento al realizar click en el input:buscar
    $("#buscar").click(function(){ 

        //? Tomamos los valores de los input:folio y input:buscar
        var folio = $('#folio').val()
        listaSurtimientoAjax(folio);
    })


}