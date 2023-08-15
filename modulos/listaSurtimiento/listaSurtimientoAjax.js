import {Grid,html} from "https://unpkg.com/gridjs@6.0.6/dist/gridjs.module.js?module";

export function listaSurtimientoAjax (folio){

    //? Limpiamos el contenido de los contenedores "cuerpo_opcion" y "titulo"
    $('#cuerpo_opcion').empty();
    $('#titulo').empty();

    //? Agregamos cajas a la opcion "cuerpo_opcion"
    $('#cuerpo_opcion').append(
      '<h5 id="titulo"></h5>'+
      '<div id="chart"></div>'+
      '<div id="wrapper"></div>'
     )

    //? Limpiamos el input "folio" y colocamos el puntero sobre el
    $('#folio').val('');
    $('#folio').focus();

    //? Creamos un array limpio que contendra la información para una tabla
    //? al igual que una variable cajas3 con el porcentaje de avance
    var datatable=[];
    var cajas3;

    //? Creamos una conexion con ajax y C# para la consulta de datos con el dato folio
$.ajax({
  type: "POST",
  url: 'modulos/listaSurtimiento/listasurtimiento.aspx/lista_surtimiento',
  data: '{folio:"'+folio+'"}',
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

          //? - Si existe información la tomaremos con un bucle "for" y  la añaderimos al array que contiene 
          //? - la información de la tabla
          for (var i = 0; i < resultado.length; i++) {

              //TODO: Valores de la consulta
              var almacenista = (resultado[i].almacenista_);
              var id_almacenista = (resultado[i].id_almacenista_);
              var cajas = (resultado[i].cajas_);
              var factura = (resultado[i].factura_);
              var cajas_totales = (resultado[i].cajas2_);

              //? - Esta variable suma el dato de cajas para conseguir el total de ellas que han sido escaneadas
              cajas_escaneadas = cajas_escaneadas + parseInt(cajas);

              //? - Este calculo realiza una regla de 3 para poder saber el porcentaje de avance 
              var num1 = cajas_escaneadas * 100;
              cajas3 = parseInt(num1 / cajas_totales );

              //? - Agregamo las información del folio a la caja "titulo" [Factura, folio, cajas]
              $('#titulo').text("Factura: "+ factura + " -- " + folio + " | Cajas:" +cajas_totales);

              //? - Agregamos los datos [Id almacenista, nombre almacenista, cajas escaneadas] al array datatable 
              datatable.push({id: id_almacenista, nombre: almacenista , cajas2: cajas});

          }

          //TODO: - Esta constante es la configuracion de la tabla que mostrara los datos del array datatable
          const grid = new Grid({
              columns: [{id:'id', name:'#ID'},{id:'nombre', name:'Nombre'}, {id:'cajas2', name:'Cajas'}],
              pagination:{ limit: 5},
              search: true,
              sort: true,
              resizable:true,
              data: datatable,
              style:{
                  table:{width:'50px', height:'50px'},
                  th:{'background-color':'#52794d', color:'white'}
              }
          }).render(document.getElementById("wrapper"));

          //TODO: - Esta variable es la configuracion de la grafica que mostrara los datos del porcentaje de avance
          var options = {
              series: [cajas3],
              chart: {
                  height: 350,
                  type: 'radialBar',
              },
              plotOptions: {
                  radialBar: {
                      hollow: {
                          size: '50%',
                      }
                  },
              },
              fill:
                  {
                      colors:'#52794d',
                  },
              colors: ['#52794d'],
              labels: ['Avance']
          };

          //? - Esta variable renderiza la grafica
          var chart = new ApexCharts(document.querySelector("#chart"), options);
          chart.render();

      }
  },
  error: function () { }
});


}