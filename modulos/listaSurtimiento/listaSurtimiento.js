import { listaSurtimientoAjax } from "./listaSurtimientoAjax.js";

export function listaSurtimiento() {
  //? Llamamos la función cambiarTema para editar la paleta de colores
  cambiarTema();

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

function cambiarTema() {
  $(".menu").css({
    "box-shadow": "0 0 20vw 1vw #52794d",
    transition: "0.5s",
  });

  $(".navbar").css({
    "background-color": "#52794d",
    transition: "0.5s",
  });

  $(".cuerpo_menu").css({
    "background-color": "rgb(220, 255, 203)",
    transition: "0.5s",
  });

  $(".opcion").css({
    "border-bottom": "solid 1px #52794d",
    color: "#52794d",
    transition: "0.5s",
  });
}
