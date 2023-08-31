import {Grid,html} from "https://unpkg.com/gridjs@6.0.6/dist/gridjs.module.js?module";

export function buscadorTarima (){

    $('#opcion').append(
        '<input id="form" type="text" class="form" placeholder="Tarima de embarque" maxlength="9">'+
        '<input id="btnClick" type="button" class="form-btn" value="Buscar" disabled></br>'
    )

    $('#form').focus();

    $('#form').change(function(){
        $('#btnClick').click();
    });
    
    $('#btnClick').click(function(){

        $('#tarimaembarque').remove();
        $('#title').remove();

        $('#opcion').append(
        '<input id="title" type="text" class="title_tarima" disabled>'+
        '<div id="tarimaembarque">'+
        '<div id="chart"></div>'+
        '<div id="wrapper"></div>'+
        '</div>'
        )
        var text= $('#form').val();
        designColor();
        var tabla=[];
        var cajas_series=[];
        var clave_series=[];
        $.ajax({
            type: "POST",
            url: 'modulos/tarimaembarque/componentes/tarimaembarque.aspx/tarimaembarque',
            data: '{tarima:"'+text+'"}',
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
                        title: "No se encontro la tarima de embarque " + text,
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
                        title: "Tarima de embarque " + text,
                        showConfirmButton: false,
                        timer: 1500
                    });

                    //? - Si existe información la tomaremos con un bucle "for" y  la añaderimos al array que contiene 
                    //? - la información de la tabla
                    for (var i = 0; i < resultado.length; i++) {
                        //TODO: Valores de la consulta
                        var clave = (resultado[i].clave_);
                        var id_operador = (resultado[i].id_operador_);
                        var cajas = (resultado[i].cajas_);
                        var cajas_totales = (resultado[i].cajas_totales_);
                        cajas_series.push(parseInt(cajas));
                        clave_series.push(clave);

                        $('#title').val("Tarima " +text + " con " + cajas_totales +" cajas.");

                        tabla.push({clave: clave, id_operador: id_operador , cajas: cajas});
                    }
            
                    const grid = new Grid({
                        columns: [{id:'clave', name:'CLAVE'},{id:'id_operador', name:'OPERADOR'}, {id:'cajas', name:'Cajas'}],
                        pagination:{ limit: 5},
                        search: true,
                        sort: true,
                        resizable:true,
                        data: tabla,
                        style:{
                            table:{width:'50px', height:'50px'},
                            th:{'background-color':'#4d796f', color:'white'}
                        }
                    }).render(document.getElementById("wrapper"));

                    //TODO: - Esta variable es la configuracion de la grafica que mostrara los datos del porcentaje de avance
                    var options = {
                        series: cajas_series,
                        chart: {
                            width: 420,
                            type: 'pie',
                        },
                        labels: clave_series,
                        responsive: [{
                            breakpoint: 420,
                            options: {
                                chart: {
                                    width: 400
                                },
                                legend: {
                                    position: 'bottom'
                                }
                            }
                        }]
                    };
                    var chart = new ApexCharts(document.getElementById("chart"), options);
                    chart.render();
                }
            },
            error: function () { }
        });

        $('#form').val("");
        $('#form').focus();
    });

}

function designColor (){

    $('#tarimaembarque').css({
        display:'flex',
        'flex-wrap':'nowrap',
        'justify-content':'center',
        'align-items':'center'
    })

    $('#chart').css({
        'text-align':'center',
        width:'50%'
    })

    $('#wrapper').css({
        'margin-top': '4%',
        width:'50%',
        border:'solid 1px #f1f2f3',
        'border-radius': '1vw'
    });

    $('.title_tarima').css({
        'text-align': 'center',
        width: '100%',
        'font-size': '1.5vw',
        'background-color': '#4d796f',
        color: 'white',
        'margin-top': '1vw',
        'border-radius':'1VW'
    })

}