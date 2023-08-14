import { embarqueportarimaAjax } from "./embarqueportarimaAjax.js";
import { cambiarTema } from "../compartidos/cambiartema.js";

export function embarqueportarima() {
  //? Llamamos la función cambiarTema para editar la paleta de colores
  cambiarTema("rgb(203, 244, 255)", "#4d6679");

  //? Borramos la opcion anterior que se estaba ejecutando y nombramos el titulo
  $("#opcion").empty();
  $(".navbar").text("Embarque por tarima");

  //? Agregamos un checbox
  $("#opcion").append(
    '<input id="radio1" class="radio" type="checkbox" title="Click si es por tarima">Embarque por tarima' +
      '<input id="radio2" class="radio" type="checkbox" title="Click si es por jaula"> Embarque por super'
  );

  //? Llamamos las classes de CSS a la pagina
  classesCss();

  //TODO: Funciones de los eventos click en los checkbox, para evitar chequeo en los dos
  $("#radio1").click(function () {
    $("#radio2").prop("checked", false);
  });

  $("#radio2").click(function () {
    $("#radio1").prop("checked", false);
  });
}

//TODO: Funcion que da los estilos de CSS a los items solo a esta pagina
function classesCss() {
  $(".radio").css({
    cursor: "pointer",
    "background-color": "red",
    "margin-left": "1vw",
  });
}
