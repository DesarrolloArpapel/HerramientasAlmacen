import {Grid,html} from "https://unpkg.com/gridjs@6.0.6/dist/gridjs.module.js?module";

export function buscador (){

    $('#opcion').append(
    '<input id="form" type="text" class="form" placeholder="Ingrese su Folio">'+
    '<input id="click" type="button" class="form-btn" value="Buscar">'
    );


    $('#form').focus();

    $('#form').change(function(){ $('#click').click(); })

    $('#click').click(function(){

        $('#tabla').remove();
        $('#title').remove();
        $('#opcion').append(
        '<input type="text" id="title" class="title">'+
        '<div id="tabla"></div>'
       );
        designColor();

        var text = $('#form').val();
        $('#form').val('');
        var tabla=[];

        $.ajax({
            type: "POST",
            url: 'modulos/tarimalistasurtimiento/componentes/tarimalistasuertimientoCom.aspx/tarima',
            data: '{folio:"'+text+'"}',
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
                        title: "No se encontro la lista de surtimiento  " + text,
                        showConfirmButton: false,
                        timer: 1500
                    });
                    $('#tabla').remove();
                    $('#title').remove();
                }
                else {
                    var resultado = (typeof response.d) == 'string' ?
                        eval('(' + response.d + ')') :
                        response.d;

                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: "Lista de surtimiento " + text,
                        showConfirmButton: false,
                        timer: 1500
                    });

                    //? - Si existe información la tomaremos con un bucle "for" y  la añaderimos al array que contiene 
                    //? - la información de la tabla
                    var c = 0;
                    for (var i = 0; i < resultado.length; i++) {
                        //TODO: Valores de la consulta
                        c++;
                        var tarima = (resultado[i].tarima_);
                        var cajas = (resultado[i].cajas_);
                        var cajas_qad = (resultado[i].cajas_qad_);
                        $('#title').val("Lista de surtimiento " + text + " #Tarimas " + c);
                        tabla.push({tarima: tarima, cajas: cajas, cajas_qad: cajas_qad});
                    }

                    const grid = new Grid({
                        columns: [{id:'tarima', name:'Tarima'},{id:'cajas', name:'Cajas SQL'},{id:'cajas_qad', name:'Cajas QAD'}],
                        pagination:{ limit: 5},
                        search: true,
                        sort: true,
                        resizable:true,
                        data: tabla,
                        style:{
                            table:{width:'50px', height:'50px'},
                            th:{'background-color':'#51679a', color:'white'}
                        }
                    }).render(document.getElementById("tabla"));
                    $('#form').focus();

                }
            },
            error: function () { }
        });

    });
}

function designColor(){
    $('.title').css({
        width: '100%',
        'font-size':'1.5vw',
        'color':'white',
        'margin-top': '1vw',
        'border-radius':'1VW',
        'text-align':'center',
        'background-color':'#51679a',
        border: 'none'
    })

};