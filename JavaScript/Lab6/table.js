"use strict"
document.querySelectorAll('td')[1].innerHTML = navigator.userAgent;
document.querySelectorAll('td')[3].innerHTML = navigator.appVersion;
document.querySelectorAll('td')[5].innerHTML = navigator.appName;
document.querySelectorAll('td')[7].innerHTML = navigator.appCodeName;
document.querySelectorAll('td')[9].innerHTML = navigator.platform;
document.querySelectorAll('td')[11].innerHTML = navigator.javaEnabled();
document.querySelectorAll('td')[13].innerHTML = screen.width + ' ' + screen.height;
document.querySelectorAll('td')[15].innerHTML = screen.colorDepth;
document.querySelectorAll('td')[17].innerHTML = history.length;
document.querySelectorAll('td')[19].innerHTML = location.href;
document.querySelectorAll('td')[21].innerHTML = location.pathname;
document.querySelectorAll('td')[23].innerHTML = location.host;
