$(".langs .lang").mouseover(function () {
  $(this).css("box-shadow", "10px 10px 10px 10px white");
});
$(".langs .lang").mouseout(function () {
  $(this).css("box-shadow", "none");
});
vk.onclick = function () {
  window.open("https://vk.com/fedosdekudrille");
};
telegram.addEventListener("click", function () {
  window.open("https://t.me/Fedosdekudrille");
});
$("#snow").click(function (event) {
  let coords = event.screenX;
  if (getComputedStyle(snowman).bottom == "-250px") {
    $(".snowman")
      .animate({bottom: 0}, 1000)
      .animate({bottom: 0}, 1000)
      .animate({bottom: -250}, 1000)
      .css("display", "block")
      .css("left", coords - snowman.getBoundingClientRect().width / 2);
  }
});
$(".img").click(function () {
  $(".popar").css("display", "flex");
  $(".popar").html("<img class='img' src=" + $(this).attr("src") + ">");
  $("body").css("overflow", "hidden");
});
$(".popar").click(function () {
  $(".popar").css("display", "none");
  $("body").css("overflow", "auto");
});
