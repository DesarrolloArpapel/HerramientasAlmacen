import { cambiarTema } from "../compartidos/cambiartema.js";

export function tarimalistasurtimiento() {
  //? Llamamos la función cambiarTema para editar la paleta de colores
  cambiarTema("rgb(214, 218, 232)", "#51679a");

  //? Borramos la opcion anterior que se estaba ejecutando y nombramos el titulo
  $("#opcion").empty();
  $(".navbar").text("Tarima por lista de surtimiento");
}
