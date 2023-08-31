import { cambiarTema } from "../compartidos/cambiartema.js";
import { buscador } from "./componentes/actanacimientoComponentes.js";

export function actanacimiento() {
  //? Llamamos la función cambiarTema para editar la paleta de colores
  cambiarTema("rgb(236, 212, 255)", "#6a4d79");

  //? Borramos la opcion anterior que se estaba ejecutando y nombramos el titulo
  $("#opcion").empty();
  $(".navbar").text("Acta de nacimiento");
  buscador();
}
