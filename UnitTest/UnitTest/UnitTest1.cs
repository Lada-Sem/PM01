using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private double Expression(double x)
        {
            // Определяем функцию f(x) = x^3 - 2x - 5
            return x-1;
        }

        private double Derivative(double x)
        {
            // Определяем производную f'(x) = 3x^2 - 2
            return 1;
        }

        public void Newton(double initialGuess, double tolerance, int maxIterations, out string result)
        {
            double x_current = initialGuess;

            for (int i = 0; i < maxIterations; i++)
            {
                double fx = Expression(x_current);
                double fpx = Derivative(x_current);

                // Проверяем, не близка ли производная к нулю
                if (Math.Abs(fpx) < 1e-12)
                {
                    result = "Производная близка к нулю.";
                    return;
                }

                // Вычисляем следующее приближение к корню
                double x_next = x_current - fx / fpx;

                // Проверяем, достигли ли мы желаемой точности
                if (Math.Abs(x_next - x_current) < tolerance || Math.Abs(Expression(x_next)) < tolerance)
                {
                    result = $"Корень найден: {x_next}";
                    return;
                }

                // Обновляем текущее значение для следующей итерации
                x_current = x_next;
            }

            result = $"Превышено количество итераций {maxIterations}\nПоследний найденный корень: {x_current}";
        }

        [TestMethod]
        public void TestNewtonMethod()
        {
            // Arrange
            double initialGuess = 2.0; // Начальное приближение
            double tolerance = 1e-5; // Допустимая погрешность
            int maxIterations = 100; // Максимальное количество итераций
            string result;

            // Act
            Newton(initialGuess, tolerance, maxIterations, out result);

            // Assert
            Assert.IsTrue(result.Contains("Корень найден"), "Метод Ньютона не нашел корень.");
            Assert.IsTrue(result.Contains("1"), "Найденный корень не соответствует ожидаемому значению.");
        }
    }
}