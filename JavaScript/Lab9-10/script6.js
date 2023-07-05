div.onmouseover = function (event) {
  let target = event.target;
  if (target.className == "menu") {
    var submenu = target.getElementsByClassName("submenu");
    closeMenu();
    submenu[0].style.display = "block";
  }
};
document.onmouseover = function (event) {
  let target = event.target;
  console.log(event.target);
  if (target.className != "menu" && target.className != "submenu") {
    closeMenu();
  }
};
function closeMenu() {
  var submenu = document.getElementsByClassName("submenu");
  for (var i = 0; i < submenu.length; i++) submenu[i].style.display = "none";
}
