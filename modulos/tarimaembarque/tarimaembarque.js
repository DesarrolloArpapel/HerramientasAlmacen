import { cambiarTema } from "../compartidos/cambiartema.js";
import {buscadorTarima} from "./componentes/tarimaembarquecomponentes.js";

export function tarimaembarque() {
  //? Llamamos la función cambiarTema para editar la paleta de colores
  cambiarTema("rgb(212, 255, 226)", "#4d796f");

  //? Borramos la opcion anterior que se estaba ejecutando y nombramos el titulo
  $("#opcion").empty();
  $(".navbar").text("Tarima de embarque");
  buscadorTarima();
}
