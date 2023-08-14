import { cambiarTema } from "../compartidos/cambiartema.js";

export function guiasasignadas() {
  //? Llamamos la función cambiarTema para editar la paleta de colores
  cambiarTema("rgb(255, 238, 212)", "#79694d");

  //? Borramos la opcion anterior que se estaba ejecutando y nombramos el titulo
  $("#opcion").empty();
  $(".navbar").text("Guias Asignadas");
}
