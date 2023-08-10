import {Grid,html} from "https://unpkg.com/gridjs@6.0.6/dist/gridjs.module.js?module";

export function listaSurtimientoAjax (folio){

$('#cuerpo_opcion').empty();

$('#cuerpo_opcion').append(
    '<div id="chart"></div>'+
    '<div id="wrapper"></div>'
)

var datatable=[];

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
     
          for (var i = 0; i < resultado.length; i++) {
              var almacenista = (resultado[i].almacenista_);
              var id_almacenista = (resultado[i].id_almacenista_);
              var cajas = (resultado[i].cajas_);

              datatable.push({id: id_almacenista, nombre: almacenista , cajas2: cajas});

          }

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
      }
  },
  error: function () { }
});


var options = {
    series: [70],
    chart: {
        height: 350,
        type: 'radialBar',
    },
    plotOptions: {
        radialBar: {
            hollow: {
                size: '70%',
            }
        },
    },
    fill:
        {
            colors:'#52794d',
        },
    colors: ['#52794d'],
    labels: ['Avance'],
};

var chart = new ApexCharts(document.querySelector("#chart"), options);
chart.render();

}