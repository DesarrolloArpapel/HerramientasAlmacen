import { cambiarTema } from "../compartidos/cambiartema.js";

export function asignartarima() {
  //? Llamamos la función cambiarTema para editar la paleta de colores
  cambiarTema("rgb(249, 255, 212)", "#79764d");

  //? Borramos la opcion anterior que se estaba ejecutando y nombramos el titulo
  $("#opcion").empty();
  $(".navbar").text("Asignar tarima");
}
