// * Este es el javascript principal de la aplicación//

// ! Importamos Grid.js
import { Grid, html } from "https://unpkg.com/gridjs?module";

// ! Importamos las funciones
import { listaSurtimiento } from "../modulos/listaSurtimiento/listaSurtimiento.js";
import { embarque } from "../modulos/embarque/embarque.js";
import { asignartarima } from "../modulos/asignartarima/asignartarima.js";
import { guiasasignadas } from "../modulos/guiasasignadas/guiasasignadas.js";
import { actanacimiento } from "../modulos/actanacimiento/actanacimineto.js";
import { tarimaembarque } from "../modulos/tarimaembarque/tarimaembarque.js";
import { tarimalistasurtimiento } from "../modulos/tarimalistasurtimiento/tarimalistasurtimiento.js";
import { entregalistasurtimiento } from "../modulos/entregalistasurtimiento/entregalistasurtimiento.js";

//TODO: Este escript esta generado para escuchar el click de los botones

$("#opcion1").click(function () {
  listaSurtimiento();
});

$("#opcion2").click(function () {
  embarque();
});

$("#opcion3").click(function () {
  asignartarima();
});

$("#opcion4").click(function () {
  guiasasignadas();
});

$("#opcion5").click(function () {
  actanacimiento();
});

$("#opcion6").click(function () {
  tarimaembarque();
});

$("#opcion7").click(function () {
  tarimalistasurtimiento();
});

$("#opcion8").click(function () {
  entregalistasurtimiento();
});
