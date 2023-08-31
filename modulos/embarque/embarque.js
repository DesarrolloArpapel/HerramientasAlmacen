import { embarqueporsuper } from "./embarqueporsuper/embarqueporsuper.js";
import { cambiarTema } from "../compartidos/cambiartema.js";

export function embarque() {
  //? Llamamos la función cambiarTema para editar la paleta de colores
  cambiarTema("rgb(203, 244, 255)", "#4d6679");

  //? Borramos la opcion anterior que se estaba ejecutando y nombramos el titulo
  $("#opcion").empty();
  $(".navbar").text("Embarque por super");

  //? Agregamos 2 checbox y un boton buscar
  $("#opcion").append(
    '<input id="radio2" class="radio" type="radio" title="Click si es por jaula"> Embarque por super'  +
    '<input id="relacion" class="relacion" type="search" title="Anote su relación de embarque" placeholder="Relación de embarque">'  +
    '<input id="buscar" class="buscar" type="button" title="Click para buscar la relación" value="Buscar" disabled>'+
    '<h1 id="titulo"></h1>' +
    '<div id="cuerpo_opcion"></div>'
  );

  //? Llamamos las clases de CSS a la pagina
  classesCss();

  //TODO: Funciones de los eventos click en los checkbox, para evitar chequeo en los dos

  $("#radio2").click(function () {
      $("#relacion").focus();
  });

  //? Al cambiar el texto en el ID "relacion" hacemos click en el boton buscar
  $("#relacion").change(function(){
      $("#buscar").click();
  });

  //? Escuchamos el evento click del boton y ejecutamos la funcion deacuerdo a su elección
  $('#buscar').click(function(){

      //? Tomamos el valor del input relacion
      var relacion = $('#relacion').val();

      //* Si el checkbox 1 es elejido
      if( $("#radio1").prop("checked"))
      {

      }

      //* Si el checkbox 2 es elejido
      else if ( $("#radio2").prop("checked"))
      {
          embarqueporsuper(relacion);
      }

      //* Si ninguno es elegido
      else{
          alert("Elige una opción");
      }

      //? Limpiamos el valor del input relacion
      $('#relacion').val('');
      $('#relacion').focus();

  })

}

//TODO: Funcion que da los estilos de CSS a los items solo a esta pagina
function classesCss() {

  //? Estilo para los checkbox
  $(".radio").css({
    cursor: "pointer",
    "margin-left": "1vw",
  });

  //? Estilo para el boton buscar
  $("#buscar").css({
      cursor: "pointer",
      "margin-left": "1%",
      "background-color": "#4d6679",
      color: "white",
      border: "none",
      "padding-left": "5px",
      "padding-right": "5px",
      "font-size": "1.2vw",
      "border-radius": "1vw"
  })

  //? Estilo para el boton buscar
  $("#relacion").css({
      "margin-left": "25%",
      border: "none",
      "border-bottom": "solid 1px #4d6679",
      "padding-left": "5px",
      "padding-right": "5px",
      "font-size": "1.2vw",
      outline: "none"
  })

  $("#cuerpo_opcion").css({
      width: "100%",
      "flex-wrap": "wrap"
  })
  
}