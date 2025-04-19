using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher.Utils
{
    internal class CubicSpline
    {
        // Массивы для хранения коэффициентов каждого кубического полинома S_i(x) = a_i + b_i*(x-x_i) + c_i*(x-x_i)^2 + d_i*(x-x_i)^3
        private readonly double[] a;
        private readonly double[] b;
        private readonly double[] c;
        private readonly double[] d;
        private readonly double[] xNodes; // Узлы x

        /// <summary>
        /// Типы граничных условий для сплайна.
        /// </summary>
        public enum BoundaryConditionType
        {
            Natural
        }

        /// <summary>
        /// Создает экземпляр кубического сплайна для заданного набора точек.
        /// </summary>
        /// <param name="x">Массив узлов x (должен быть отсортирован по возрастанию).</param>
        /// <param name="y">Массив значений y в узлах.</param>
        /// <param name="boundaryType">Тип граничных условий (по умолчанию Natural).</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public CubicSpline(double[] x, double[] y, BoundaryConditionType boundaryType = BoundaryConditionType.Natural)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));
            if (x.Length != y.Length) throw new ArgumentException("Массивы x и y должны иметь одинаковую длину.");
            if (x.Length < 2) throw new ArgumentException("Для построения сплайна нужно как минимум 2 точки.");

            int n = x.Length;
            this.xNodes = new double[n];
            Array.Copy(x, this.xNodes, n); // Сохраняем копию узлов

            // Проверяем сортировку x
            for (int i = 0; i < n - 1; i++)
            {
                if (x[i] >= x[i + 1])
                {
                    throw new ArgumentException("Узлы x должны быть отсортированы по строгому возрастанию.");
                }
            }

            // Инициализируем массивы коэффициентов для n-1 полиномов
            // S_i(x) определен на [x_i, x_{i+1}] для i = 0..n-2
            // Нам нужно n точек, n-1 интервал
            a = new double[n];
            b = new double[n - 1];
            c = new double[n]; // Будем хранить M_i / 2, включая M_0 и M_n
            d = new double[n - 1];

            double[] h = new double[n - 1];
            for (int i = 0; i < n - 1; i++)
            {
                h[i] = x[i + 1] - x[i];
            }

            // --- Решение системы для вторых производных M_i = S''(x_i) ---
            // Мы ищем M_0, M_1, ..., M_n (всего n+1 значение, если считать S'' на концах)
            // Но система уравнений составляется для M_1, ..., M_{n-1}
            double[] M = new double[n]; // M[i] = S''(x_i)

            if (n > 2) // Для n=2 сплайн - это просто прямая линия
            {
                // --- Настройка и решение трехдиагональной системы для M ---
                // Ax = B, где x - вектор неизвестных M_1...M_{n-1}
                int systemSize = n - 2;
                double[] subDiagonal = new double[systemSize];   // Диагональ под главной (a_i)
                double[] mainDiagonal = new double[systemSize];  // Главная диагональ (b_i)
                double[] superDiagonal = new double[systemSize]; // Диагональ над главной (c_i)
                double[] rhs = new double[systemSize];           // Правая часть (d_i)

                for (int i = 0; i < systemSize; i++) // Индекс системы j соответствует M_{j+1}
                {
                    int actual_i = i + 1; // Индекс M в полной системе (M_1 ... M_{n-1})

                    mainDiagonal[i] = 2.0 * (h[actual_i - 1] + h[actual_i]);

                    if (actual_i > 1)
                        subDiagonal[i] = h[actual_i - 1]; // Для первого уравнения subDiagonal[0] не используется в решателе
                    if (actual_i < n - 1)
                        superDiagonal[i] = h[actual_i]; // Для последнего уравнения superDiagonal[systemSize-1] не используется

                    rhs[i] = 6.0 * ((y[actual_i + 1] - y[actual_i]) / h[actual_i] - (y[actual_i] - y[actual_i - 1]) / h[actual_i - 1]);
                }

                // Применение граничных условий (Natural Spline: M[0]=0, M[n-1]=0)
                if (boundaryType == BoundaryConditionType.Natural)
                {
                    M[0] = 0;
                    M[n - 1] = 0;
                    // Корректируем правую часть для первого и последнего уравнений системы
                    if (systemSize > 0) // Если есть хотя бы M_1
                    {
                        rhs[0] -= subDiagonal[0] * M[0]; // Отнимаем h_0 * M_0 = 0
                        if (systemSize > 1) // Если есть M_{n-1} и уравнение для него
                            rhs[systemSize - 1] -= superDiagonal[systemSize - 1] * M[n - 1]; // Отнимаем h_{n-1}*M_n = 0
                    }
                }
                else
                {
                    throw new NotImplementedException("Поддерживаются только Natural граничные условия.");
                }


                // Решаем систему M_1...M_{n-1} методом прогонки
                if (systemSize > 0)
                {
                    double[] solvedM = SolveTridiagonalSystem(subDiagonal, mainDiagonal, superDiagonal, rhs);
                    // Копируем решение в M[1]...M[n-1]
                    for (int i = 0; i < systemSize; ++i)
                    {
                        M[i + 1] = solvedM[i];
                    }
                }
            }
            else // n = 2 (только две точки) - линейная интерполяция
            {
                M[0] = 0;
                M[1] = 0;
            }


            // --- Вычисляем коэффициенты a, b, c, d ---
            for (int i = 0; i < n - 1; i++) // Для каждого интервала [x_i, x_{i+1}]
            {
                this.a[i] = y[i];
                this.c[i] = M[i] / 2.0; // c_i = M_i / 2
                this.d[i] = (M[i + 1] - M[i]) / (6.0 * h[i]);
                this.b[i] = (y[i + 1] - y[i]) / h[i] - h[i] * (M[i + 1] + 2.0 * M[i]) / 6.0;
            }
            // Коэффициент 'a' для последней точки может быть полезен
            this.a[n - 1] = y[n - 1];
            this.c[n - 1] = M[n - 1] / 2.0; // c_n = M_n / 2

        }

        /// <summary>
        /// Вычисляет интерполированное значение y для заданной точки x.
        /// </summary>
        /// <param name="xTarget">Точка x, в которой вычисляется значение сплайна.</param>
        /// <returns>Интерполированное значение y.</returns>
        public double Interpolate(double xTarget)
        {
            int n = xNodes.Length;

            // Обработка краев интервала (можно добавить экстраполяцию или выброс ошибки)
            if (xTarget < xNodes[0] || xTarget > xNodes[n - 1])
            {
                // Простая линейная экстраполяция или выброс ошибки
                // В данном случае вернем значение на границе или выбросим ошибку
                // Console.WriteLine("Предупреждение: Точка находится вне интервала интерполяции. Возвращено значение на границе.");
                if (xTarget < xNodes[0]) return a[0] + b[0] * (xTarget - xNodes[0]); // Линейная экстраполяция по первому отрезку
                if (xTarget > xNodes[n - 1]) return a[n - 2] + b[n - 2] * (xTarget - xNodes[n - 2]) + c[n - 2] * Math.Pow(xTarget - xNodes[n - 2], 2) + d[n - 2] * Math.Pow(xTarget - xNodes[n - 2], 3); // Экстраполяция по последнему кубическому полиному

                // throw new ArgumentOutOfRangeException(nameof(xTarget), "Точка находится вне интервала интерполяции.");
            }

            // Находим нужный интервал [x_i, x_{i+1}] с помощью бинарного поиска (или линейного для малого n)
            int low = 0;
            int high = n - 1;
            int intervalIndex = -1;

            // Простой линейный поиск (для малого n)
            for (int i = 0; i < n - 1; i++)
            {
                if (xNodes[i] <= xTarget && xTarget <= xNodes[i + 1])
                {
                    intervalIndex = i;
                    break;
                }
            }
            // Особый случай: точка совпадает с последним узлом
            if (xTarget == xNodes[n - 1])
            {
                intervalIndex = n - 2; // Используем последний полином
            }


            if (intervalIndex < 0)
            {
                // Этого не должно произойти, если точка внутри интервала
                throw new InvalidOperationException("Не удалось найти подходящий интервал.");
            }

            // Вычисляем значение полинома S_i(xTarget)
            double dx = xTarget - xNodes[intervalIndex];
            double interpolatedValue = a[intervalIndex] +
                                       b[intervalIndex] * dx +
                                       c[intervalIndex] * dx * dx + // c_i = M_i / 2
                                       d[intervalIndex] * dx * dx * dx;

            return interpolatedValue;
        }

        /// <summary>
        /// Решает трехдиагональную систему Ax=d методом прогонки (алгоритм Томаса).
        /// A представлена тремя диагоналями: a (под), b (главная), c (над).
        /// Все массивы диагоналей и d должны иметь одинаковую длину m.
        /// a[0] и c[m-1] не используются.
        /// </summary>
        /// <param name="a">Поддиагональ (a[0] не используется).</param>
        /// <param name="b">Главная диагональ.</param>
        /// <param name="c">Наддиагональ (c[m-1] не используется).</param>
        /// <param name="d">Правая часть.</param>
        /// <returns>Вектор решения x.</returns>
        private static double[] SolveTridiagonalSystem(double[] a, double[] b, double[] c, double[] d)
        {
            int n = d.Length;
            if (n == 0) return new double[0];
            if (a.Length != n || b.Length != n || c.Length != n) throw new ArgumentException("Размеры диагоналей и правой части не совпадают.");

            // Модифицируем массивы c и d (прямой ход)
            double[] c_prime = new double[n];
            double[] d_prime = new double[n];

            c_prime[0] = c[0] / b[0];
            d_prime[0] = d[0] / b[0];

            for (int i = 1; i < n; i++)
            {
                double temp = b[i] - a[i] * c_prime[i - 1];
                if (Math.Abs(temp) < 1e-15) throw new InvalidOperationException("Нулевой элемент на диагонали в методе прогонки. Система вырождена?");

                if (i < n - 1) // c[n-1] не используется
                    c_prime[i] = c[i] / temp;

                d_prime[i] = (d[i] - a[i] * d_prime[i - 1]) / temp;
            }

            // Обратный ход
            double[] x = new double[n];
            x[n - 1] = d_prime[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                x[i] = d_prime[i] - c_prime[i] * x[i + 1];
            }

            return x;
        }
    }
}
