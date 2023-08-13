export function asignartarima() {
  //? Llamamos la función cambiarTema para editar la paleta de colores
  cambiarTema();

  //? Borramos la opcion anterior que se estaba ejecutando y nombramos el titulo
  $("#opcion").empty();
  $(".navbar").text("Asignar tarima");
}

function cambiarTema() {
  $(".menu").css({
    "box-shadow": "0 0 20vw 1vw #79764d",
    transition: "0.5s",
  });

  $(".navbar").css({
    "background-color": "#79764d",
    transition: "0.5s",
  });

  $(".cuerpo_menu").css({
    "background-color": "rgb(249, 255, 212)",
    transition: "0.5s",
  });

  $(".opcion").css({
    "border-bottom": "solid 1px #79764d",
    color: "#79764d",
    transition: "0.5s",
  });
}
