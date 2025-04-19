using Grapher.Models;
using LiveChartsCore.Kernel;
using MathNet.Symbolics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher.Utils
{
    public class ODUSqulptor
    {
        public OperationModel Operation { get; set; }
        public SymbolicExpression Function { get; private set; }
        public Func<double, double, double> Expression { get; private set; }

        public string ExpressionString { get; private set; }

        public void CreateFunction(string function)
        {
            ExpressionString = function;
            Function = SymbolicExpression.Parse(function);
            Expression = (x, y) => Function.Evaluate(new Dictionary<string, FloatingPoint> { { "x", x }, { "y", y } }).RealValue;
        }


        public List<(double x, double y)> ExplicitEuler(double x0, double y0, double xEnd, int steps)
        {
            try
            {
                if (Expression == null) throw new ArgumentException("Опишите функцию");
                if (steps <= 0) throw new ArgumentException("Количество шагов должно быть положительным.", nameof(steps));
            }
            catch (Exception ex)
            {
                Operation.Description = ex.Message;
                return null;
            }
            var results = new List<(double x, double y)>
            {
                (x0, y0)
            };

            double h = (xEnd - x0) / steps;
            double x = x0;
            double y = y0;

            for (int i = 0; i < steps; i++)
            {
                y = y + h * Expression(x, y); // Формула явного метода
                x = x + h; // Или x = x0 + (i + 1) * h;
                results.Add((x, y));
            }
            return results;
        }

        public List<(double x, double y)> ImplicitEuler(double x0, double y0, double xEnd, int steps, int maxIter = 100, double tolerance = 1e-7)
        {
            try
            {
                if (Expression == null) throw new ArgumentException("Опишите функцию");
                if (steps <= 0) throw new ArgumentException("Количество шагов должно быть положительным.", nameof(steps));
            }
            catch (Exception ex)
            {
                Operation.Description = ex.Message;
                return null;
            }
            var results = new List<(double x, double y)>
            {
                (x0, y0)
            };

            double h = (xEnd - x0) / steps;
            double x = x0;
            double y = y0;

            for (int i = 0; i < steps; i++)
            {
                double x_next = x + h;
                double y_next_guess = y; // Начальное приближение для y_next (можно улучшить явным методом Эйлера)
                                         // y_next_guess = y + h * f(x, y); // Улучшенное начальное приближение

                bool converged = false;
                for (int j = 0; j < maxIter; j++)
                {
                    double y_next_new = y + h * Expression(x_next, y_next_guess); // Итерационная формула: y_k+1 = y_i + h*f(x_i+1, y_k)
                    if (Math.Abs(y_next_new - y_next_guess) < tolerance)
                    {
                        y_next_guess = y_next_new;
                        converged = true;
                        break;
                    }
                    y_next_guess = y_next_new;
                }

                if (!converged)
                {
                    Operation.Resolve = $"Неявный метод Эйлера: внутренний итерационный процесс не сошелся на шаге {i + 1} для x={x_next}.";
                    return null;
                }

                y = y_next_guess; // Обновляем y
                x = x_next;       // Обновляем x
                results.Add((x, y));
            }
            return results;
        }

        public List<(double x, double y)> ModifiedEuler(double x0, double y0, double xEnd, int steps)
        {
            try
            {
                if (Expression == null) throw new ArgumentException("Опишите функцию");
                if (steps <= 0) throw new ArgumentException("Количество шагов должно быть положительным.", nameof(steps));
            }
            catch (Exception ex)
            {
                Operation.Description = ex.Message;
                return null;
            }
            var results = new List<(double x, double y)>();
            results.Add((x0, y0));

            double h = (xEnd - x0) / steps;
            double x = x0;
            double y = y0;

            for (int i = 0; i < steps; i++)
            {
                double k1 = h * Expression(x, y);              // Наклон в начале (как в явном методе)
                double y_predictor = y + k1;         // Предиктор (явный шаг Эйлера)
                double x_next = x + h;
                double k2 = h * Expression(x_next, y_predictor); // Наклон в предсказанной точке

                y = y + (k1 + k2) / 2.0;          // Корректор (используем средний наклон)
                x = x_next;
                results.Add((x, y));
            }
            return results;
        }

        public List<(double x, double y)> RungeKutta4(double x0, double y0, double xEnd, int steps)
        {
            Operation = new();
            try
            {
                if (Expression == null) throw new ArgumentException("Опишите функцию");
                if (steps <= 0) throw new ArgumentException("Количество шагов должно быть положительным.", nameof(steps));
            }
            catch (Exception ex)
            {
                Operation.Description = ex.Message;
                return null;
            }
            var results = new List<(double x, double y)>
            {
                (x0, y0)
            };

            double h = (xEnd - x0) / steps;
            double x = x0;
            double y = y0;

            for (int i = 0; i < steps; i++)
            {
                // Вычисляем четыре промежуточных коэффициента
                double k1 = h * Expression(x, y);
                double k2 = h * Expression(x + h / 2.0, y + k1 / 2.0);
                double k3 = h * Expression(x + h / 2.0, y + k2 / 2.0);
                double k4 = h * Expression(x + h, y + k3);

                // Вычисляем следующее значение y
                y = y + (k1 + 2.0 * k2 + 2.0 * k3 + k4) / 6.0;
                // Обновляем x
                x = x + h; // Или x = x0 + (i + 1) * h;

                results.Add((x, y));
            }
            return results;
        }

        public List<(double x, double y)> AdamsBashforthMoulton4(double x0, double y0, double xEnd, int steps)
        {
            Operation = new();
            try
            {
                if (Expression == null) throw new ArgumentException("Опишите функцию");
                if (steps < 3) throw new ArgumentException("Количество шагов должно быть не меньше 4-х.", nameof(steps));
            }
            catch (Exception ex)
            {
                Operation.Description = ex.Message;
                return null;
            }

            Operation.Description += "Для метода Адамса (Предиктор-Корректор) 4-го порядка нам нужно 4 начальные точки (y0, y1, y2, y3) и соответственно 3 шага для их вычисления";
            var results = new List<(double x, double y)>();
            var fValues = new List<double>(); // Хранилище для значений f(x_i, y_i)

            double h = (xEnd - x0) / steps;
            var startupPoints = new List<(double x, double y)>
            {
                (x0, y0) // Добавляем начальную точку
            };

            double currentX = x0;
            double currentY = y0;
        //    Operation.Description += "\n--- Фаза \"старта\" (Используем RK4 для получения первых 4 точек) ---\nНам нужно вычислить y1, y2, y3. RK4 вычисляет точки ,включая, начальную:\n";
            // Выполняем 3 шага RK4
            Operation.Description += $"\nТочка шага 1:[{x0},{y0}]";
            for (int i = 0; i < 3; i++)
            {
                // Вычисляем k1, k2, k3, k4 для текущего шага
                double k1 = h * Expression(currentX, currentY);
                double k2 = h * Expression(currentX + h / 2.0, currentY + k1 / 2.0);
                double k3 = h * Expression(currentX + h / 2.0, currentY + k2 / 2.0);
                double k4 = h * Expression(currentX + h, currentY + k3);

                // Вычисляем следующее значение y и x
                currentY = currentY + (k1 + 2.0 * k2 + 2.0 * k3 + k4) / 6.0;
                currentX = currentX + h; // Или x0 + (i + 1) * h;
                Operation.Description += $"\nТочка шага {i + 2}:[{currentX},{currentY}]";
                startupPoints.Add((currentX, currentY));
            }

            // Добавляем стартовые точки в итоговый результат и вычисляем f для них
            foreach (var point in startupPoints)
            {
                results.Add(point);
                fValues.Add(Expression(point.x, point.y)); // f0, f1, f2, f3
            }

            // --- Основной цикл Адамса (начиная с 4-й точки до последней) ---
            // i соответствует индексу текущей известной точки, вычисляем i+1
            for (int i = 3; i < steps; i++)
            {
                // Получаем предыдущие значения f
                double f_i = fValues[i];
                double f_im1 = fValues[i - 1];
                double f_im2 = fValues[i - 2];
                double f_im3 = fValues[i - 3];

                // Текущее значение y
                double y_i = results[i].y; // results[i] = (x_i, y_i)

                // Предиктор (AB4)
                double y_predictor = y_i + h / 24.0 * (55.0 * f_i - 59.0 * f_im1 + 37.0 * f_im2 - 9.0 * f_im3);

                // Вычисление f в предсказанной точке
                double x_ip1 = results[i].x + h; // x_{i+1}
                double f_predictor = Expression(x_ip1, y_predictor);

                // Корректор (AM4)
                double y_ip1 = y_i + h / 24.0 * (9.0 * f_predictor + 19.0 * f_i - 5.0 * f_im1 + 1.0 * f_im2);

                // Добавляем результат
                results.Add((x_ip1, y_ip1));

                // Вычисляем и сохраняем f для следующего шага (на основе скорректированного y)
                // Это PECE вариант - Predict, Evaluate, Correct, Evaluate
                fValues.Add(Expression(x_ip1, y_ip1));
            }

            return results;
        }
    }
}
