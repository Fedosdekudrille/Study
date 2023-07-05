$("span").mouseover(function () {
  $("span").css("color", "red");
});
$("span").click(function () {
  $("span").css("font-size", "100px");
});
$("#los").click(function () {
  $("#los").attr("src", "drugoilos.jpg");
});
$("#button").click(function () {
  $("#text").html("<img src='kaban.jpg' alt='Кобан' id='kaban' />");
});
$("img").mouseover(function () {
  $(this).css("width", "700px");
});
$("img").mouseout(function () {
  $(this).css("width", "500px");
});
$("p").dblclick(function () {
  $(this).css("border", "5px red solid");
});
