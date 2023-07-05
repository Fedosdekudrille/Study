function moove(){
  let start = Date.now(); // запомнить время начала

  let timer = setInterval(function() {
    let timePassed = Date.now() - start;

    if (timePassed >= 4000) {
      clearInterval(timer);
      return;
    }

    draw(timePassed);

    function draw(timePassed) {
      if (timePassed <= 2000)
        document.getElementById('car').style.left = timePassed / 2 + 'px';
      else
        document.getElementById('car').style.left = (4000 - timePassed) / 2 + 'px';
  }
  }, 20);
}
