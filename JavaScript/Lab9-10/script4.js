function grafik() {
  let width = 1000;
  let height = 700;
  var color = colors.value;
  var str = "";
  x0 = width / 2;
  y0 = height / 2;

  kx = width / 20;
  ky = height / 10;

  str += "<font color=" + color + ">";

  for (i = 1; i < width; i = i + 20)
    str += " <div style='top: " + y0 + " ; left: " + i + " '>_</div> ";
  for (i = 1; i < height; i = i + 20)
    str += " <div style='left: " + x0 + " ; top: " + i + " '>|</div> ";
  for (x = -10; x < 10; x = x + 0.001) {
    switch (+document.forms[0].fun.value) {
      case 1:
        y = x * x;
        break;
      case 2:
        y = x * x * x;
        break;
      case 3:
        y = Math.cos(x);
        break;
      case 4:
        y = Math.sin(x);
        break;
      default:
        alert("Выберите функцию, которую хотите построить");
        return;
    }
    str +=
      "<div style='left:" +
      (x0 + kx * x) +
      ";top:" +
      (y0 - ky * y + 3) +
      " '>.</div>";
  }
  document.getElementsByTagName("p")[0].innerHTML = str;
}
