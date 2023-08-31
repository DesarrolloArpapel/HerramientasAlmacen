import {Grid,html} from "https://unpkg.com/gridjs@6.0.6/dist/gridjs.module.js?module";

export function buscar(){
    $('#opcion').append(
      '<div>'+
      '<select id="almacenista">'+
      '<option>Seleccione un almacenista</option>'+
      '</select>'+
      '<input id="form" type="text" class="form" placeholder="Lista de surtimiento" maxlength="9">'+
      '<input id="btnClick" type="button" class="form-btn" value="Asignar" disabled></div>');
    $('#form').focus();

    $('#form').change(function(){$('#btnClick').click()});
    $('#almacenista').change(function(){
        $('#form').focus();
    });

    $('#btnClick').click(function (){
        asignarlista();
    });

    $.ajax({
        type: "POST",
        url: 'modulos/entregalistasurtimiento/componentes/entregalistasurtimientocom.aspx/almacenistas',
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            console.log(response);
            console.log(Object.keys(response.d).length);
            if (Object.keys(response.d).length === 2) {

                Swal.fire({
                    position: 'center',
                    icon: 'warning',
                    title: "Tenemos inconvenientes con Meta 4 ",
                    showConfirmButton: false,
                    timer: 1500
                });
            }
            else {
                var resultado = (typeof response.d) == 'string' ?
                    eval('(' + response.d + ')') :
                    response.d;

                //? - Si existe información la tomaremos con un bucle "for" y  la añaderimos al array que contiene 
                //? - la información de la tabla
                for (var i = 0; i < resultado.length; i++) {
                    var id_almacenista = (resultado[i].id_almacenista_);
                    var almacenista = (resultado[i].almacenista_);
                    $('#almacenista').append(
                        '<option value="'+id_almacenista+'">'+almacenista+'</option>')
                }
            }
        },
        error: function () { }
    });
}


function asignarlista(){


    var almacenista = $('#almacenista').val();
    var lista = $('#form').val();
    $('#form').val('');
    $('#form').focus();
    $('#wrapper').empty();
    $('#opcion').append(
    '<div id="wrapper"></div>'+
    '<center><div id="loader" class="lds-ellipsis"><div></div><div></div><div></div><div></div></div></center>');
    var datatable=[];

    $.ajax({
        type: "POST",
        url: 'modulos/entregalistasurtimiento/componentes/entregalistasurtimientocom.aspx/entregalista',
        data: '{lista:"'+lista+'", almacenista:"'+almacenista+'"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {
            console.log(response);
            console.log(Object.keys(response.d).length);
            if (Object.keys(response.d).length === 2) {   
                Swal.fire({
                    position: 'center',
                    icon: 'warning',
                    title: "Tenemos inconvenientes con Meta 4 ",
                    showConfirmButton: false,
                    timer: 1500
                });
            }
            else {
                var resultado = (typeof response.d) == 'string' ?
                    eval('(' + response.d + ')') :
                    response.d;
                //? - Si existe información la tomaremos con un bucle "for" y  la añaderimos al array que contiene 
                //? - la información de la tabla
                for (var i = 0; i < resultado.length; i++) {
                    var almacenista = (resultado[i].almacenista_);
                    var id_almacenista = (resultado[i].id_almacenista_);
                    var existencia = (resultado[i].existencia_);
                    var fecha = (resultado[i].fecha_);
                    var factura = (resultado[i].factura_);

                    datatable.push({almacenista:almacenista, factura:factura, fecha:fecha, folio:lista})

                    if(existencia == 0){
                        Swal.fire({
                            position: 'center',
                            icon: 'error',
                            title: "No se encontraron datos de la lista", //? No existe en QAD
                            showConfirmButton: false,
                            timer: 1500
                        });

                    }
                    else
                    {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: "Se asigno la lista correctamente",
                            showConfirmButton: false,
                            timer: 1500
                        });
                    }
                  /*  var almacenista = (resultado[i].almacenista_);
                    $('#almacenista').append(
                        '<option value="'+id_almacenista+'">'+almacenista+'</option>')*/
                }
                $('#loader').remove();
                const grid = new Grid({
                    columns: [
                        {id:'folio', name:'Folio'}
                       ,{id:'factura', name:'Factura'}
                       ,{id:'almacenista', name:'Almacenista'}
                       ,{id:'fecha', name:'Fecha'}],
                    pagination:{ limit: 5},
                    search: true,
                    sort: true,
                    resizable:true,
                    data: datatable,
                    style:{
                        table:{width:'50px', height:'50px'},
                        th:{'background-color':'#9a8851', color:'white'}
                    }
                }).render(document.getElementById("wrapper"));
            }
        },
        error: function () { }
    });

}