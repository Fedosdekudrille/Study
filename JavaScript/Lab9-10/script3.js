    let f = 0;
    setInterval(function () {
        f += 2 * Math.PI / 180;
        document.getElementById('car').style.left = (235 + 250 * Math.sin(f)) + 'px';
        document.getElementById('car').style.top = (235 + 250 * Math.cos(f)) + 'px';
    }, 20);