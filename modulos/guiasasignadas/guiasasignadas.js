export function guiasasignadas() {
  //? Llamamos la función cambiarTema para editar la paleta de colores
  cambiarTema();

  //? Borramos la opcion anterior que se estaba ejecutando y nombramos el titulo
  $("#opcion").empty();
  $(".navbar").text("Guias Asignadas");
}

function cambiarTema() {
  $(".menu").css({
    "box-shadow": "0 0 20vw 1vw #79694d",
    transition: "0.5s",
  });

  $(".navbar").css({
    "background-color": "#79694d",
    transition: "0.5s",
  });

  $(".cuerpo_menu").css({
    "background-color": "rgb(255, 238, 212)",
    transition: "0.5s",
  });

  $(".opcion").css({
    "border-bottom": "solid 1px #79694d",
    color: "#79694d",
    transition: "0.5s",
  });
}
