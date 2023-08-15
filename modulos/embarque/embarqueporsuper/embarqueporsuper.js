import {Grid,html} from "https://unpkg.com/gridjs@6.0.6/dist/gridjs.module.js?module";

export function embarqueporsuper (relacion){

    $('#cuerpo_opcion').empty();
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
            }
            else {
                var resultado = (typeof response.d) == 'string' ?
                    eval('(' + response.d + ')') :
                    response.d;
                var cajas_escaneadas=0;
                $('#cuerpo_opcion').append(
                    '<div class="form2">'+
                    '<input type="search" id="factura" class="factura" placeholder="Factura">'+
                    '<input type="button" id="ingresar" class="ingresar" value="Ingresar">'+
                    '</div>'+
                    '<div id="wrapper"></div>'
                    )
                //? - Si existe información la tomaremos con un bucle "for" 
                for (var i = 0; i < resultado.length; i++) {

                    //TODO: Valores de la consulta
                    var factura = (resultado[i].factura_);
                    var validar = (resultado[i].validar_);
                    var fecha_inicio = (resultado[i].fecha_inicio_);
                    var fecha_fin = (resultado[i].fecha_fin_);

                    datatable.push({ factura: factura, validar: validar , fecha_inicio: fecha_inicio , fecha_fin: fecha_fin});

                }

                //TODO: - Esta constante es la configuracion de la tabla que mostrara los datos del array datatable
                const grid = new Grid({
                    columns: [{id:'factura', name:'Factura'},{id:'validar', name:'Validado'}, {id:'fecha_inicio', name:'Inicio'}, {id:'fecha_fin', name:'Fin'}],
                    pagination:{ limit: 6},
                    sort: true,
                    resizable:true,
                    data: datatable,
                    style:{
                        table:{width:'50%', height:'50%'},
                        th:{'background-color':'#4d6679', color:'white'}
                    }
                }).render(document.getElementById("wrapper"));
                estilosCss ();
              
            }
        },
        error: function () { }
    });

}

function estilosCss (){
    $('.form2').css({
        width: "100%"
    });

    $('.ingresar').css({
        cursor: "pointer",
        "margin-left": "1%",
        "background-color": "#4d6679",
        color: "white",
        border: "none",
        "padding-left": "5px",
        "padding-right": "5px",
        "font-size": "1.2vw",
        "border-radius": "1vw"
    });

    //? Estilo para el boton buscar
    $("#factura").css({
        border: "none",
        "border-bottom": "solid 1px #4d6679",
        "padding-left": "5px",
        "padding-right": "5px",
        "font-size": "1.2vw",
        outline: "none"
    })
}