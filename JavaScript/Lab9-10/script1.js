let duck = document.getElementById("duck");

function mouseDown() {
  let shiftX = event.clientX - duck.getBoundingClientRect().left;
  let shiftY = event.clientY - duck.getBoundingClientRect().top;

  duck.style.position = "absolute";

  function moveAt(pageX, pageY) {
    duck.style.left = pageX - shiftX + "px";
    duck.style.top = pageY - shiftY + "px";
  }

  function onMouseMove(event) {
    moveAt(event.pageX, event.pageY);
  }

  document.addEventListener("mousemove", onMouseMove);

  duck.onmouseup = function () {
    document.removeEventListener("mousemove", onMouseMove);
    duck.onmouseup = null;
  };
}

duck.ondragstart = function () {
  return false;
};

let text = document.getElementById("text");

text.onmousedown = function (event) {
  let shiftX = event.clientX - text.getBoundingClientRect().left;
  let shiftY = event.clientY - text.getBoundingClientRect().top;

  text.style.position = "absolute";

  moveAt(event.pageX, event.pageY);

  function moveAt(pageX, pageY) {
    text.style.left = pageX - shiftX + "px";
    text.style.top = pageY - shiftY + "px";
  }

  function onMouseMove(event) {
    moveAt(event.pageX, event.pageY);
  }

  document.addEventListener("mousemove", onMouseMove);

  text.onmouseup = function () {
    document.removeEventListener("mousemove", onMouseMove);
    text.onmouseup = null;
  };
};

text.ondragstart = function () {
  return false;
};
