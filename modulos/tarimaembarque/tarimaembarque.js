export function tarimaembarque() {
  //? Llamamos la función cambiarTema para editar la paleta de colores
  cambiarTema();

  //? Borramos la opcion anterior que se estaba ejecutando y nombramos el titulo
  $("#opcion").empty();
  $(".navbar").text("Tarima de embarque");
}

function cambiarTema() {
  $(".menu").css({
    "box-shadow": "0 0 20vw 1vw #4d796f",
    transition: "0.5s",
  });

  $(".navbar").css({
    "background-color": "#4d796f",
    transition: "0.5s",
  });

  $(".cuerpo_menu").css({
    "background-color": "rgb(212, 255, 226)",
    transition: "0.5s",
  });

  $(".opcion").css({
    "border-bottom": "solid 1px #4d796f",
    color: "#4d796f",
    transition: "0.5s",
  });
}
