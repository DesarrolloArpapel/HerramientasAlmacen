import {Grid,html} from "https://unpkg.com/gridjs@6.0.6/dist/gridjs.module.js?module";

export function buscador (){

  

    $('#opcion').append(
        '<input id="form" type="text" class="form" placeholder="Acta de nacimiento" maxlength="9">'+
        '<input id="btnClick" type="button" class="form-btn" value="Buscar">'
    )



    $('#form').focus();

    $('#form').change(function(){
        $('#btnClick').click();
    });
    
    $('#btnClick').click(function(){
        $('#wrapper').remove(); /*LIMPIA EL DIV*/
        $('#opcion').append(
        '<div id="wrapper"></div>'
        )
        var text= $('#form').val();
       
        var tabla;
        $.ajax({
            type: "POST",
            url: 'modulos/actanacimiento/componentes/actanacimiento.aspx/actanacimiento',
            data: '{acta:"'+text+'"}',
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
                        title: "No se encontro la acta de nacimiento " + text,
                        showConfirmButton: false,
                        timer: 1500
                    });
                }
                else {
                    var resultado = (typeof response.d) == 'string' ?
                        eval('(' + response.d + ')') :
                        response.d;

                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: "Acta de nacimiento " + text,
                        showConfirmButton: false,
                        timer: 1500
                    });
                    //? - Si existe información la tomaremos con un bucle "for" y  la añaderimos al array que contiene 
                    //? - la información de la tabla
                    for (var i = 0; i < resultado.length; i++) {

                        //TODO: Valores de la consulta
                        var clave = (resultado[i].clave_);
                        var descripcion = (resultado[i].descripcion_);
                        var tarima = (resultado[i].tarima_);
                        var codigo = (resultado[i].codigo_);
                        var ayudante = (resultado[i].ayudante_);
                        var operador = (resultado[i].operador_);
                        var maquina = (resultado[i].maquina_);
                        var turno = (resultado[i].turno_);
                     
                        tabla={

                        config :[{
                            clave: clave, 
                            descripcion: descripcion ,
                            tarima: tarima,
                            codigo: codigo,
                            ayudante: ayudante,
                            operador: operador,
                            maquina:maquina,
                            turno: turno
                        }]
                        
                        };

                    }
            
                    $('#wrapper').append(
                        '<table>'+
                         '<tr>'+
                        '<th>ACTA NACIMIENTO</th>'+
                        '<td>'+text+'</td>'+
                        '</tr>'+
                        '<tr>'+
                        '<th>CLAVE</th>'+
                        '<td>'+JSON.stringify(tabla.config[0].clave).replaceAll('"', "")+'</td>'+
                        '</tr>'+
                        '<tr>'+
                        '<th>DESCRIPCION</th>'+
                        '<td>'+JSON.stringify(tabla.config[0].descripcion).replaceAll('"', "")+'</td>'+
                        '</tr>'+    
                        '<tr>'+
                        '<th>TARIMA</th>'+
                        '<td>'+JSON.stringify(tabla.config[0].tarima).replaceAll('"', "")+'</td>'+
                        '</tr>'+    
                        '<tr>'+
                        '<th>CODIGO</th>'+
                        '<td>'+JSON.stringify(tabla.config[0].codigo).replaceAll('"', "")+'</td>'+
                        '</tr>'+    
                        '<tr>'+
                        '<th>AYUDANTE</th>'+
                        '<td>'+JSON.stringify(tabla.config[0].ayudante).replaceAll('"', "")+'</td>'+
                        '</tr>'+     
                        '<tr>'+
                        '<th>OPERADOR</th>'+
                        '<td>'+JSON.stringify(tabla.config[0].operador).replaceAll('"', "")+'</td>'+
                        '</tr>'+    
                        '<tr>'+
                        '<th>MAQUINA</th>'+
                        '<td>'+JSON.stringify(tabla.config[0].maquina).replaceAll('"', "")+'</td>'+
                        '</tr>'+     
                        '<tr>'+
                        '<th>TURNO</th>'+
                        '<td>'+JSON.stringify(tabla.config[0].turno).replaceAll('"', "")+'</td>'+
                        '</tr>'+
                        '</table>'
                    )
         
                    designColor();
                }
            },
            error: function () { }
        });

        $('#form').val("");
        $('#form').focus();
    });

}


function designColor (){

    $('#wrapper').css({
        'margin-top': '4%',
        width:'100%',
        border:'solid 1px #f1f2f3',
        'border-radius': '1vw'
    });

    $('th').css({
        'background-color': '#6a4d79',
        padding : '0.5vw',
        color: 'white',
        'font-size': '1.2vw',
        'border-bottom-left-radius': '5px',
        'border-top-left-radius': '5px',
    })

    $('td').css({
        'text-align':'left',
        'font-size': '1.4vw',
        padding : '0.5vw',
        width : '100%',
        border:'solid 1px #000',
        'border-top-right-radius': '1vw',
        'border-bottom-right-radius': '1vw'
    })

    $('tr').css({
         border:'solid 1px #000',
         'border-radius': '1vw',
    })
}