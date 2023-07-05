"use strict";
let pech = function () {
  document.querySelector("h1").innerHTML = document.form.FIT.value;
  document.getElementById("p").innerHTML =
    "Студент " +
    document.form.name.value +
    " " +
    document.form.surname.value +
    ", учащийся на специальности " +
    document.form.spec.value +
    " на " +
    document.form.kurs.value +
    " курсе, должен сдавать следующие предметы:";
  for (let n = 0; n < document.form.pred.length; n++) {
    if (document.form.pred[n].checked) {
      let x = document.createElement("p");
      x.innerHTML = "<li>" + document.form.pred[n].value + "</li>";
      document.getElementById("one").append(x);
    }
  }
};
but.addEventListener("click", () => {
  subm.style.display = "block";
  subm.innerHTML = "";
  document.querySelectorAll("input:checked").forEach(function (el_checked) {
    subm.innerHTML += "<option>" + el_checked.value + "</option>";
  });
});
