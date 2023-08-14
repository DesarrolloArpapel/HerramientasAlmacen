export function cambiarTema(color1, color2) {
  $(".menu").css({
    "box-shadow": "0 0 0vw 0px " + color1,
    transition: "0.2s",
  });

  $(".navbar").css({
    "background-color": color2,
    transition: "0.2s",
  });

  $(".cuerpo_menu").css({
    background: "linear-gradient(" + color1 + ", white )",
    transition: "0.2s",
  });

  $(".opcion").css({
    "border-bottom": "solid 1px " + color2,
    color: color2,
    transition: "0.2s",
  });
}
