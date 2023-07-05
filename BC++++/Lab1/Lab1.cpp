#include <iostream>

using namespace std;

double minimizator(double a, double b, ...) {
	double* ptr = &b;
	double min = *ptr;
	while (a--) {

		if (*ptr < min) {
			min = *ptr;
		}
		ptr++;

	}
	return min;
}

double maximizator(double a, double b, ...) {
	double* ptr = &b;
	double max = *ptr;
	while (a--) {

		if (*ptr > max) {
			max = *ptr;
		}
		ptr++;

	}
	return max;
}

int sum(int n, ...) {
	int* p = &n;
	int sum = 0;
	for (int i = 0; i < n; i++) {
		int b = *p;
		int a = *(++p);
		sum += b * a;
	}
	return sum;
}
float f1(float x)
{
	float fun = pow(3, x) + 2 * x - 1;
	return fun;
}

float f2(float x)
{
	float fun = exp(x) - 2;
	return fun;
}

float f3(float x)
{
	float fun = 2 * x + pow(x, 3) - 7;
	return fun;
}

float f4(float x)
{
	float fun = exp(x) + 2 * x;
	return fun;
}

float fun(float e, float a, float b, float (*f)(float))
{
	float c;
	while (abs(a - b) > e)
	{
		c = (a + b) / 2;
		if (f(a) * f(c) < 0)
		{
			b = c;
		}
		else {
			a = c;
		}
	}
	return (a + b) / 2;
}

void main()
{
	setlocale(0, "RU");
	float e = 0.001, a = 3.4 * pow(10, 38), b = 3.4 * pow(10, -38);
	int number = 1;
	float (*f)(float);
	while (number != 0)
	{
		cout << "Введите номер функции, корни которой желаете увидеть, введите ноль для прекращения цикла" << endl;
		cout << "1 - pow(3, x) + 2 * x - 1" << endl << "2 - exp(x) - 2" << endl << "2 * x + pow(x, 3) - 7" << endl << "exp(x) + 2 * x" << endl;
		cin >> number;
		switch (number)
		{
		case 1:
			cout << fun(e, a, b, f1) << endl;
			break;
		case 2:
			cout << fun(e, a, b, f2) << endl;
			break;
		case 3:
			cout << fun(e, a, b, f3) << endl;
			break;
		case 4:
			cout << fun(e, a, b, f4) << endl;
		case 0:
			cout << "Выход из программы" << endl;
			break;
		default:
			cout << "Неправильно введено число" << endl;
			break;
		}
	}
	cout << "Сумма: " << sum(6, 4, 5, 1, 2, 3, 0) << endl;
	cout << "Сумма: " << sum(2, 34, 4445) << endl;
	cout << "Минимум: " << minimizator(3, 0.3, 5.1, 9.4) << endl;
	cout << "Минимум: " << minimizator(5, -19.0, -29.0, 0.0, 3.0, -8.0) << endl;
	cout << "Максимум: " << maximizator(5, -20, 34, 34.2, 0) << endl;
	cout << "Максимум: " << maximizator(5, -20, -200, 34.2, 120) << endl;
}