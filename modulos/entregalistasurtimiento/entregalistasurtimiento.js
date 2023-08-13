export function entregalistasurtimiento() {
  //? Llamamos la función cambiarTema para editar la paleta de colores
  cambiarTema();

  //? Borramos la opcion anterior que se estaba ejecutando y nombramos el titulo
  $("#opcion").empty();
  $(".navbar").text("Entrega de lista de surtimiento");
}

function cambiarTema() {
  $(".menu").css({
    "box-shadow": "0 0 20vw 1vw #9a8851",
    transition: "0.5s",
  });

  $(".navbar").css({
    "background-color": "#9a8851",
    transition: "0.5s",
  });

  $(".cuerpo_menu").css({
    "background-color": "rgb(232, 225, 214)",
    transition: "0.5s",
  });

  $(".opcion").css({
    "border-bottom": "solid 1px #9a8851",
    color: "#9a8851",
    transition: "0.5s",
  });
}
