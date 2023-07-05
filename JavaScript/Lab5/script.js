"use strict";

// Task1

document.write('<div id="first">');

for (let n = 0; n < document.all.length; n++) {
  document.write(
    n + 1 + "-" + document.all[n].tagName + document.all[n].nodeType
  );
  document.write(" ");
}

document.write("</div>");
document.write("<br>");
document.write("<br>");

// Task2

document.write('<div id="second">');

for (let i = 0; i < document.body.childNodes.length; i++) {
  document.write(document.body.childNodes[i]);
  document.write(" ");
}
document.write("</div>");

// Task3

alert(document.querySelectorAll("span")[0].textContent);
alert(document.getElementById("span").textContent);
alert(document.all[14].textContent);

// Task4

let x =
  +document.querySelectorAll("th")[1].textContent +
  +document.querySelectorAll("th")[2].textContent;
let z =
  +document.querySelectorAll("td")[1].textContent +
  +document.querySelectorAll("td")[2].textContent +
  +document.querySelectorAll("td")[4].textContent +
  +document.querySelectorAll("td")[5].textContent;
document.querySelectorAll("span")[1].innerHTML =
  "Военной революцией" + Math.round(((x + z) * 100) / 6) / 100;
