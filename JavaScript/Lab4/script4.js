"use strict"
// Task1

function Gruppa(n, spec, kol){
  this.n = n;
  this.spec = spec;
  this.kol = kol;
  this.sub_stud = function sub_stud(k){
    this.kol -= k;
    document.write('В группе ' + this.n + ' осталось ' + this.kol + ' студентов' + '<br>');
  }
  this.add_stud = function add_stud(k){
    this.kol += k;
  }
}
let gr1 = new Gruppa(1, "ИСИТ", 32);
let gr2 = new Gruppa(2, "ИСИТ", 30);
let gr3 = new Gruppa(3, "ИСИТ", 35);
let gr4 = new Gruppa(4, "ПОИТ", 50);

gr1.add_stud(2);
gr2.add_stud(5);
gr3.add_stud(1);
gr4.add_stud(4);

gr1.sub_stud(0);
gr2.sub_stud(2);
gr3.sub_stud(4);
gr4.sub_stud(19);

// Task2

function Student(name, surname, phy, math, inf, place){
  this.name = name;
  this.surname = surname;
  this.phy = phy;
  this.math = math;
  this.inf = inf;
  this.place = "BSTU";
  this.me = function me(nam, fam){
    this.name = nam;
    this.surname = fam;
    alert(this.name + this.surname);
  }
  this.hwo = function hwo(){
    document.write(this.place + ' ' + this.name + ' ' + this.surname)
  }
  this.srb = function srb(){
    document.write(' - ' + Math.round((this.phy + this.math + this.inf)/3*100)/100 + '<br>');
  }
}
let gstud = new Student("Сергей", "Воробьёв", 8, 9, 10);
let nstud = new Student("Андрей", "Симонов", 8, 7, 8);
let bstud = new Student("Олег", "Булыжный", 5, 6, 6);

gstud.me('Фёдор ', 'Кудрицкий');
gstud.hwo();
gstud.srb();
nstud.hwo();
nstud.srb();
bstud.hwo();
bstud.srb();

// Task 3

let array = [
"pow",
"pop",
"push",
"shift",
"round",
"floor",
"slice",
"sort"
]
document.write(array + '<br>');
delete array[2];
document.write(array + '<br>');
document.write(2 in array);
document.write('<br>');
document.write('kurs' in Gruppa);
document.write('<br>');
if('name' in Student == 1){
  document.write('Это свойство присутствует' + '<br>');
}
else{
  document.write('Это свойство отсутствует' + '<br>');
}
document.write(gr1 instanceof Array);
document.write('<br>');
document.write(gr1 instanceof String);
document.write('<br>');
document.write(gr1 instanceof Object);
document.write('<br>');
function five(num){
  return num = 5;
}
document.write(typeof array);
document.write('<br>');
document.write(typeof 'sdghfd');
document.write('<br>');
document.write(typeof five());
document.write('<br>');
document.write(typeof gstud);
document.write('<br>');
document.write(typeof gr1.add_stud);
document.write('<br>');
document.write(typeof gr1.n);
document.write('<br>');
document.write(typeof gr1.spec);
document.write('<br>');
