choise.onchange = function () {
  answer.innerHTML = "";
  answer.className = "";
  let val = document.forms[0].choise.value;
  if (val == "in" || val == "out") $("#secondtext").css("display", "none");
  else $("#secondtext").css("display", "inline-block");
  if (val == "in")
    alarm.innerHTML =
      'Для перевода в двоичный код введите число в десятичной системе в строку для ввода и нажмите кнопку "вычислить". Мы сможем рассчитать перевод как положительного, так и отрицательного числа, но мы не работаем с дробными числами. Максимальное число для вычислений - "4294967295", минимальное - "-4294967295"';
  if (val == "out")
    alarm.innerHTML =
      'Для перевода из двоичного кода введите число в двоичной системе в строку для ввода и нажмите кнопку "вычислить". Мы сможем рассчитать только положительное целое число. Максимальное число для вычислений - "1111111111111111"';
  if (val == "plus")
    alarm.innerHTML =
      'Для сложения в двоичой системе введите в обе строки слагаемые в той же системе и нажмите кнопку "вычислить". Мы сможем рассчитать только положительные целые числа. Максимальные числа для вычислений - "1111111111111111"';
  if (val == "minus")
    alarm.innerHTML =
      'Для вычитания в двоичой системе введите в первую строку уменьшаемое число, а во вторую вычитаемое в той же системе и нажмите кнопку "вычислить". Мы сможем рассчитать только положительные целые числа. Максимальные числа для вычислений - "1111111111111111"';
  if (val == "multiply")
    alarm.innerHTML =
      'Для вычитания в двоичой системе введите в обе строки множители в той же системе и нажмите кнопку "вычислить". Мы сможем рассчитать только положительные целые числа. Максимальные числа для вычислений - "1111111111111111"';
  if (val == "division")
    alarm.innerHTML =
      'Для вычитания в двоичой системе введите в первую строку делимое число, а во вторую делитель в той же системе и нажмите кнопку "вычислить". Мы сможем рассчитать только положительные целые числа. Максимальные числа для вычислений - "1111111111111111"';
};
but.onclick = function () {
  answer.innerHTML = "";
  let val = document.forms[0].choise.value;
  answer.className = "answer";
  if (val === "in") inbi();
  else if (val === "out") outbi();
  else mathacts();
};
function inbi() {
  let num = +document.forms[0].firsttext.value;
  if (!num) {
    answer.innerHTML =
      "<img src='src/error.png' style='vertical-align: middle;'>Введите десятичное число";
    return;
  }
  if (num > 4294967295) {
    answer.innerHTML =
      "<img src='src/error.png' style='vertical-align: middle;'>Извините, но мы не работаем с настолько большими числами";
    return;
  }
  if (num != Math.round(num)) {
    answer.innerHTML =
      "<img src='src/error.png' style='vertical-align: middle;'>Извините, но мы не работаем с дробными числами";
    return;
  }
  if (num < 0) {
    num = num * -1;
    num = "-" + (num >>> 0).toString(2);
  } else num = (num >>> 0).toString(2);
  answer.innerHTML = num;
}
function outbi() {
  let num = +document.forms[0].firsttext.value;
  if (!num) {
    answer.innerHTML =
      "<img src='src/error.png' style='vertical-align: middle;'>Введите число в двоичной системе";
    return;
  }
  if (document.forms[0].firsttext.value[0] == "-") {
    answer.innerHTML =
      "<img src='src/error.png' style='vertical-align: middle;'>Извините, мы не работаем с отрицательными числами";
    return;
  }
  for (let i = 0; i < document.forms[0].firsttext.value.length; i++)
    if (document.forms[0].firsttext.value[i] == ".") {
      answer.innerHTML =
        "<img src='src/error.png' style='vertical-align: middle;'>Извините, мы не работаем с дробными числлами";
      return;
    }
  for (let i = 0; i < num.toString().length; i++)
    if (
      Math.round(num / Math.pow(10, i)) % 10 != 0 &&
      Math.round(num / Math.pow(10, i)) % 10 != 1
    ) {
      answer.innerHTML =
        "<img src='src/error.png' style='vertical-align: middle;'>Введите двоичное, а не десятичное число";
      return;
    }
  num = parseInt(num, 2).toString(10);
  answer.innerHTML = num;
}
function mathacts() {
  let num1 = +document.forms[0].firsttext.value;
  let num2 = +document.forms[0].secondtext.value;
  if (!num1) {
    answer.innerHTML =
      "<img src='src/error.png' style='vertical-align: middle;'>Введите число в двоичной системе в первую строку";
    return;
  }
  if (!num2) {
    answer.innerHTML =
      "<img src='src/error.png' style='vertical-align: middle;'>Введите число в двоичной системе во вторую строку";
    return;
  }
  if (
    document.forms[0].firsttext.value[0] == "-" ||
    document.forms[0].secondtext.value[0] == "-"
  ) {
    answer.innerHTML =
      "<img src='src/error.png' style='vertical-align: middle;'>Извините, мы не работаем с отрицательными числами";
    return;
  }
  let max = 0;
  if (num1.toString().length > num2.toString().length)
    max = num1.toString().length;
  else max = num2.toString().length;
  for (let i = 0; i < max; i++) {
    if (document.forms[0].firsttext.value[i] == ".") {
      answer.innerHTML =
        "<img src='error.png' style='vertical-align: middle;'>Извините, мы не работаем с дробными числлами";
      return;
    }
    if (document.forms[0].secondtext.value[i] == ".") {
      answer.innerHTML =
        "<img src='error.png' style='vertical-align: middle;'>Извините, мы не работаем с дробными числлами";
      return;
    }
  }
  for (let i = 0; i < max; i++)
    if (
      (Math.round(num1 / Math.pow(10, i)) % 10 != 0 &&
        Math.round(num1 / Math.pow(10, i)) % 10 != 1) ||
      (Math.round(num2 / Math.pow(10, i)) % 10 != 0 &&
        Math.round(num2 / Math.pow(10, i)) % 10 != 1)
    ) {
      answer.innerHTML =
        "<img src='src/error.png' style='vertical-align: middle;'>Введите двоичные, а не десятичные числа";
      return;
    }
  let val = document.forms[0].choise.value;
  let num = 0;
  switch (val) {
    case "plus":
      num = (
        (+parseInt(num1, 2).toString(10) + +parseInt(num2, 2).toString(10)) >>>
        0
      ).toString(2);
      break;
    case "minus":
      if (
        +parseInt(num1, 2).toString(10) - +parseInt(num2, 2).toString(10) <
        0
      ) {
        num =
          "-" +
          (
            (+parseInt(num2, 2).toString(10) -
              +parseInt(num1, 2).toString(10)) >>>
            0
          ).toString(2);
      } else {
        num = (
          (+parseInt(num1, 2).toString(10) -
            +parseInt(num2, 2).toString(10)) >>>
          0
        ).toString(2);
      }
      break;
    case "multiply":
      num = (
        (+parseInt(num1, 2).toString(10) * +parseInt(num2, 2).toString(10)) >>>
        0
      ).toString(2);
      break;
    case "division":
      num =
        "0." +
        (
          (+parseInt(num1, 2).toString(10) /
            +parseInt(num2, 2).toString(10)) >>>
          0
        ).toString(2);
      break;
  }
  answer.innerHTML = num;
}
