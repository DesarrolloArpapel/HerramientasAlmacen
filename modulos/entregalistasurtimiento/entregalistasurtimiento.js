import { cambiarTema } from "../compartidos/cambiartema.js";
import {buscar} from "./componentes/entregalistasurtimientocom.js";

export function entregalistasurtimiento() {
  //? Llamamos la función cambiarTema para editar la paleta de colores
  cambiarTema("rgb(232, 225, 214)", "#9a8851");

  //? Borramos la opcion anterior que se estaba ejecutando y nombramos el titulo
  $("#opcion").empty();
  $(".navbar").text("Entrega de lista de surtimiento");
  buscar();
}
