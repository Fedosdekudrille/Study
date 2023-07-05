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
alert(array + "\n" +"length: " +array.length)
let arr = array.slice(1,4)
arr.push(array[6])
arr.push(array[7])
arr.unshift("concat")
alert(arr + "\n" + "length: " + arr.length)
let math = array.slice(4,6)
math.unshift(array[0])
math.push("sqrt")
alert(math + "\n" + "length: " + math.length)

//Task 3
let str = "Кудрицкий Фёдор Александрович"
document.write(str + str.length + '</br>')
let b = str.toUpperCase() + ', ';
let m = str.toLowerCase()
document.write(b.concat(m) + '</br>')
let s = str.replace('Кудрицкий Фёдор Александрович', 'КФА')
document.write(s + '</br>')

//Task 4
let x = new Date()
let y = x.getMonth() + 1;
document.write("<table border='1'>")
document.write("<tr>" + "<td>" + "Год" + "</td>" + "<td>" + x.getFullYear() + "</td>" + "</tr>")
document.write("<tr>" + "<td>" + "Месяц" + "</td>" + "<td>" + y + "</td>" + "</tr>")
document.write("<tr>" + "<td>" + "День" + "</td>" + "<td>" + x.getDate() + "</td>" + "</tr>")
document.write("<tr>" + "<td>" + "Час" + "</td>" + "<td>" + x.getHours() + "</td>" + "</tr>")
document.write("<tr>" + "<td>" + "Минута" + "</td>" + "<td>" + x.getMinutes() + "</td>" + "</tr>")
document.write("<tr>" + "<td>" + "Секунда" + "</td>" + "<td>" + x.getSeconds() + "</td>" + "</tr>")
document.write("</table>")
