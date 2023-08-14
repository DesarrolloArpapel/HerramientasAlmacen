import { listaSurtimientoAjax } from "./listaSurtimientoAjax.js";
import { cambiarTema } from "../compartidos/cambiartema.js";

export function listaSurtimiento() {
  //? Llamamos la función cambiarTema para editar la paleta de colores
  cambiarTema("rgb(220, 255, 203)", "#52794d");

  //? Borramos la opcion anterior que se estaba ejecutando y nombramos el titulo
  $("#opcion").empty();
  $(".navbar").text("Lista de Surtimiento");

  //? Agregaoms los botones input:folio y input:buscar
  $("#opcion").append(
    '<input id="folio" class="form" type="text" placeholder="Ingresa el Folio">' +
      '<input id="buscar" class="form-btn" type="button" value="Buscar" >' +
      '<h1 id="titulo"></h1>' +
      '<div id="cuerpo_opcion"></div>'
  );

  //? Evento al realizar click en el input:buscar
  $("#buscar").click(function () {
    //? Tomamos los valores de los input:folio y input:buscar
    var folio = $("#folio").val();
    listaSurtimientoAjax(folio);
  });
}
