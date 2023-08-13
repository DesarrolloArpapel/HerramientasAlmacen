export function tarimalistasurtimiento() {
  //? Llamamos la función cambiarTema para editar la paleta de colores
  cambiarTema();

  //? Borramos la opcion anterior que se estaba ejecutando y nombramos el titulo
  $("#opcion").empty();
  $(".navbar").text("Tarima por lista de surtimiento");
}

function cambiarTema() {
  $(".menu").css({
    "box-shadow": "0 0 20vw 1vw #51679a",
    transition: "0.5s",
  });

  $(".navbar").css({
    "background-color": "#51679a",
    transition: "0.5s",
  });

  $(".cuerpo_menu").css({
    "background-color": "rgb(214, 218, 232)",
    transition: "0.5s",
  });

  $(".opcion").css({
    "border-bottom": "solid 1px #51679a",
    color: "#51679a",
    transition: "0.5s",
  });
}
