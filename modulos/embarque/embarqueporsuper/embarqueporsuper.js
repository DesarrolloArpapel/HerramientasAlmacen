import {Grid,html} from "https://unpkg.com/gridjs@6.0.6/dist/gridjs.module.js?module";

export function embarqueporsuper (relacion){

    //? Limpiamos la caja/div "cuerpo_opcion" y removemos "form2" para poder ingresar nuevos items
    $('#cuerpo_opcion').empty();
    $('#form2').remove();

    //TODO: Ajax que nos dara la informacion de la relacion de embarque
    var datatable = [];
    $.ajax({
        type: "POST",
        url: 'modulos/embarque/embarqueporsuper/embarqueporsuper.aspx/relacionEmbarque',
        data: '{relacion:"'+relacion+'"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            console.log(response);
            console.log(Object.keys(response.d).length);
            if (Object.keys(response.d).length === 2) {
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: "No se encontro el embarque",
                    showConfirmButton: false,
                    timer: 1500
                });
            }
            else {
                var resultado = (typeof response.d) == 'string' ?
                    eval('(' + response.d + ')') :
                    response.d;
                
                //? Agregamos los items a la caja/div "cuerpo_opcion" 
                //* [--div #form]
                //* [--button #ingresar]
                //* [--text #factura]
                //* [--text relacion]
                //* [--tabla/HTML #wrapper]
                //* [--button #cerrar]
                $('#cuerpo_opcion').append(
                    '<div class="form2">'+
                    '<input type="text" id="factura" class="factura" placeholder="Factura">'+
                    '<input type="button" id="ingresar" class="ingresar" value="Ingresar">'+
                    '<input type="text" disabled class="ingresar" value="'+relacion+'">'+
                    '<input type="button" id="cerrar" class="cerrar" value="Cerrar">'+
                    '</div>'+
                    '<div id="wrapper"></div>'
                    )

                //? Se apunta al texbox de factura para inicio de puntero automatico
               // $('#factura').focus();

                //? - Si existe información la tomaremos con un bucle "for" 
                for (var i = 0; i < resultado.length; i++) {

                    //TODO: Valores de la consulta
                    var factura = (resultado[i].factura_);
                    var validar = (resultado[i].validar_);
                    var fecha_inicio = (resultado[i].fecha_inicio_);
                    var fecha_fin = (resultado[i].fecha_fin_);
                    var lista = (resultado[i].lista_);
                    
                    datatable.push({ factura: factura, lista: lista, validar: validar});

                }
                var embarque = relacion;
                //TODO: - Esta constante es la configuracion de la tabla que mostrara los datos del array datatable
                const grid = new Grid({
                    columns: [
                {id:'factura', name:'Factura'},
                {id:'lista', name:'Folio'},
                {id:'validar', name:'Validado'}
                /*{id:'button', name:'button',
                    formatter: (cell, row) => {
                        return h('button', {
                            className: 'botonClick',
                            onClick: () => pruebas(`${row.cells[1].data}`,`${embarque}`)
                    }, 'Validar');
                }}*/
                ],
                    pagination:{ limit: 6},
                    sort: true,
                    resizable:true,
                    data: datatable,
                    style:{
                        table:{width:'50%', height:'50%'},
                        th:{'background-color':'#4d6679', color:'white'}
                    }
                }).render(document.getElementById("wrapper"));

                //? Llamamos la funcion estilosCss que da el estilo [colores y formas] a la pagina
                estilosCss();

                //? Limpiamos el input factura y colocamos el puntero 
                $('#factura').empty();
                $('#factura').focus();

                //TODO: Evento que se ejecuta al hacer un cambio en input "factura" 
                $('#factura').change(function(){$('#ingresar').click()});

                //TODO: Evento que se ejecuta al hacer click en input "ingresar" 
                $('#ingresar').click(function(){

                    //? Tomamos el valor del input "Factura"
                    var factura = $('#factura').val();

                    //? Enviamos el valor a la funcion "facturas" con la variable factura
                    facturas(factura, relacion);

                    //? Limpiamos el input factura y colocamos el puntero 
                    $('#factura').val('');
                    $('#factura').focus();
                })

                $('#cerrar').click(function(){
                    cerrar(relacion);
                });
            }
        },
        error: function () { }
    });
}

function facturas(factura, relacion){

    //? Limpiamos la caja/div "cuerpo_opcion" y removemos "form2" para poder ingresar nuevos items
    $('#cuerpo_opcion').empty();
    $('#form2').remove();

    //? Ejecutamos el ajax para la validacion de facturas 
    $.ajax({
        type: "POST",
        url: 'modulos/embarque/embarqueporsuper/embarqueporsuper.aspx/facturas',
        data: '{folio:"'+factura+'", embarque:"'+relacion+'"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            console.log(response);
            console.log(Object.keys(response.d).length);
            if (Object.keys(response.d).length === 2) {
            }
            else {
                var resultado = (typeof response.d) == 'string' ?
                    eval('(' + response.d + ')') :
                    response.d;
                //? - Si existe información la tomaremos con un bucle "for" 
                for (var i = 0; i < resultado.length; i++) {
                    //TODO: Valores de la consulta
                    var caso = (resultado[i].caso_);
                    if(caso == "1")
                    {
                        /*Se valido correctamente*/
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: factura,
                            showConfirmButton: false,
                            timer: 1500
                        });
                        embarqueporsuper(relacion);
                    }
                    else
                    {
                        /*No existe en el embarque o no se encuentran datos*/
                        Swal.fire({
                            position: 'center',
                            icon: 'error',
                            title: "Factura no corresponde al embarque",
                            showConfirmButton: false,
                            timer: 1500
                        });
                        embarqueporsuper(relacion);
                    }
               }
            }
        },
        error: function () { }
    });
}


function cerrar(relacion){
    //? Ejecutamos el ajax para la validacion de facturas 
    $.ajax({
        type: "POST",
        url: 'modulos/embarque/embarqueporsuper/embarqueporsuper.aspx/cerrar',
        data: '{embarque:"'+relacion+'"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            console.log(response);
            console.log(Object.keys(response.d).length);
            if (Object.keys(response.d).length === 2) {
            }
            else {
                var resultado = (typeof response.d) == 'string' ?
                    eval('(' + response.d + ')') :
                    response.d;
                //? - Si existe información la tomaremos con un bucle "for" 
                for (var i = 0; i < resultado.length; i++) {
                    //TODO: Valores de la consulta
                    var caso = (resultado[i].cerrado_);
                    if(caso == "0")
                    {
                        /*Todas las listas estan escaneadas*/
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'Se cerro el embarque',
                            showConfirmButton: false,
                            timer: 1500
                        });

                        //? Limpiamos la caja/div "cuerpo_opcion" y removemos "form2"
                        $('#cuerpo_opcion').empty();
                        $('#form2').remove();
                    }
                    else
                    {
                        /*Faltan listas por ingresar*/
                        Swal.fire({
                            position: 'center',
                            icon: 'warning',
                            title: 'Faltan listas por ingresar',
                            showConfirmButton: false,
                            timer: 1500
                        });
                        embarqueporsuper(relacion);
                    }
                }
            }
        },
        error: function () { }
    });
}

function estilosCss (){

    //? Estilo para el div contenedor form2
    $('.form2').css({
        width: "100%"
    });

    //? Estilo para el boton buscar
    $('.ingresar').css({
        cursor: "pointer",
        "margin-left": "1%",
        "background-color": "#4d6679",
        color: "white",
        border: "none",
        "padding-left": "5px",
        "padding-right": "5px",
        "font-size": "1.2vw",
        "border-radius": "1vw",
        "text-align": "center"
    });

    //? Estilo para el boton cerrar
    $('.cerrar').css({
        cursor: "pointer",
        "margin-left": "4%",
        "background-color": "#598f3d",
        color: "white",
        border: "none",
        "padding-left": "5px",
        "padding-right": "5px",
        "font-size": "1.2vw",
        "border-radius": "1vw",
        "text-align": "center"
    });

    //alargar el mensage

    //? Estilo para el formulario factura
    $("#factura").css({
        border: "none",
        "border-bottom": "solid 1px #4d6679",
        "padding-left": "5px",
        "padding-right": "5px",
        "font-size": "1.2vw",
        outline: "none",
        "margin-bottom": "1vw"
    })

}