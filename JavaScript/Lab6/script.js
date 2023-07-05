"use strict";
let win1;
let fun1 = function () {
  win1 = open();
  win1.document.title = "Окно 1";
};
let win2;
let fun2 = function () {
  win2 = open();
  win2.document.title = "Окно 2";
};
let text1 = function () {
  win1.document.write("Это" + " " + win1.document.title);
  win1.document.title = "Окно 1";
};
let text2 = function () {
  win2.document.write("А это" + " " + win2.document.title);
  win2.document.title = "Окно 2";
};
