export function actanacimiento() {
  //? Llamamos la función cambiarTema para editar la paleta de colores
  cambiarTema();

  //? Borramos la opcion anterior que se estaba ejecutando y nombramos el titulo
  $("#opcion").empty();
  $(".navbar").text("Acta de nacimiento");
}

function cambiarTema() {
  $(".menu").css({
    "box-shadow": "0 0 20vw 1vw #6a4d79",
    transition: "0.5s",
  });

  $(".navbar").css({
    "background-color": "#6a4d79",
    transition: "0.5s",
  });

  $(".cuerpo_menu").css({
    "background-color": "rgb(236, 212, 255)",
    transition: "0.5s",
  });

  $(".opcion").css({
    "border-bottom": "solid 1px #6a4d79",
    color: "#6a4d79",
    transition: "0.5s",
  });
}
