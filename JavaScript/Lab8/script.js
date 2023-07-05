"use strict";
let losechange = function () {
  document.getElementById("los").src = "drugoilos.jpg";
};
let kaban = function () {
  document.getElementById("kaban").innerHTML = '<img id="kab" src="kaban.jpg">';
};
document.querySelector("img").onmouseover = function () {
  // setTimeout(function () {
  document.querySelector("img").style.width = "700px";
  // }, 2000);
};
document.querySelector("img").onmouseout = function () {
  document.querySelector("img").style.width = "500px";
};
document.ondblclick = function (event) {
  if (event.target.tagName == "P") event.target.style.border = "3px solid red";
};
