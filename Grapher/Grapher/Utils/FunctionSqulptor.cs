using MathNet.Symbolics;
using MathNet.Numerics;
using Grapher.Models;
using System.Xml.Linq;
using System.Windows.Markup;

namespace Grapher.Utils
{
    public class FunctionSqulptor
    {
        private static readonly Dictionary<int, (double[] Nodes, double[] Weights)> GaussLegendrePoints =
        new()
        {
            { 1, (new[] { 0.0 },
                   new[] { 2.0 }) },
            { 2, (new[] { -0.5773502691896257645091488, 0.5773502691896257645091488 },
                   new[] { 1.0, 1.0 }) },
            { 3, (new[] { -0.7745966692414833770358531, 0.0, 0.7745966692414833770358531 },
                   new[] { 0.5555555555555555555555556, 0.8888888888888888888888889, 0.5555555555555555555555556 }) },
            { 4, (new[] { -0.8611363115940525752239465, -0.3399810435848562648026658, 0.3399810435848562648026658, 0.8611363115940525752239465 },
                   new[] { 0.3478548451374538573730639, 0.6521451548625461426269361, 0.6521451548625461426269361, 0.3478548451374538573730639 }) },
            { 5, (new[] { -0.9061798459386639927976269, -0.5384693101056830910363144, 0.0, 0.5384693101056830910363144, 0.9061798459386639927976269 },
                   new[] { 0.2369268850561890875142640, 0.4786286704993664680412915, 0.5688888888888888888888889, 0.4786286704993664680412915, 0.2369268850561890875142640 }) },
        };
        public FunctionSqulptor() { }

        public OperationModel Operation { get; set; }
        public SymbolicExpression Function { get; private set; }
        public Func<double, double> Expression { get; private set; }
        public string ExpressionString { get; private set; }
        public void CreateFunction(string function)
        {
            ExpressionString = function;
            Function = SymbolicExpression.Parse(function);
            Expression = x => Function.Evaluate(new Dictionary<string, FloatingPoint> { { "x", x } }).RealValue;
        }

        public void Bisection(double a, double b, double tolerance, int maxIterations)
        {
            Operation = new OperationModel();
            if (string.IsNullOrWhiteSpace(ExpressionString))
                Operation.Description = "Зайдите в раздел для построения графиков и постройте график, относительно которого будет реализовываться функционал анализа";
            Operation.Description = "Идея: Самый простой и надежный метод. Требует начальный интервал [a, b], на концах которого функция f(x) имеет разные знаки (f(a) * f(b) < 0). Метод гарантирует, что корень находится внутри этого интервала.\r\n";
            Operation.Description += "\nХарактеристики:\r\n - Гарантированно сходится, если начальный интервал выбран верно.\r\n - Сходимость линейная (довольно медленная).\r\n - Не требует производной функции.\r\n";
            if (Expression(a) * Expression(b) >= 0)
            {
                Operation.Description += "Вывод: Функция должна иметь разные знаки на концах интервала a и b.";
                return;
            }

            double c = a; // Инициализация
            for (int i = 0; i < maxIterations; i++)
            {
                c = (a + b) / 2.0;

                if (Math.Abs(Expression(c)) < tolerance || (b - a) / 2.0 < tolerance)
                {
                   // Operation.Resolve = $"Корень найден с нужной точностью: {c}";
                    return;
                }

                if (Expression(a) * Expression(c) < 0)
                {
                    b = c; // Корень в левой половине
                }
                else
                {
                    a = c; // Корень в правой половине
                }
            }
           // Operation.Resolve = $"Превышено количество итераций {maxIterations}\nПоследний найденный корень: {c}";
        }

        public void Newton( double initialGuess, double tolerance, int maxIterations)
        {
            Operation = new OperationModel();
            Operation.Description = "Идея: Использует касательную к графику функции для нахождения следующего приближения к корню. Требует начальное приближение x₀ и знание производной функции f'(x).";
            Operation.Description += "\nХарактеристики:\r\n - Очень быстрая сходимость (квадратичная), если начальное приближение достаточно близко к корню.\r\n - Требует вычисления производной f'(x).\r\n - Может расходиться, если начальное приближение выбрано неудачно, или если производная близка к нулю (f'(x) ≈ 0) вблизи корня.";
            // Вычисляем производную функции
            var f_prime = Function.Differentiate(SymbolicExpression.Variable("x"));

            // Устанавливаем текущее значение x равным начальному приближению
            double x_current = initialGuess;

            // Начинаем итерации
            for (int i = 0; i < maxIterations; i++)
            {
                // Вычисляем значение функции в текущей точке
                double fx = Expression(x_current);

                // Вычисляем значение производной в текущей точке
                double fpx = f_prime.Evaluate(new Dictionary<string, FloatingPoint> { { "x", x_current } }).RealValue;

                // Проверяем, не близка ли производная к нулю, чтобы избежать деления на ноль
                if (Math.Abs(fpx) < 1e-12)
                {
                    Operation.Resolve = "Производная близка к нулю.";
                    return; // Завершаем выполнение, так как дальнейшие вычисления невозможны
                }

                // Вычисляем следующее приближение к корню
                double x_next = x_current - fx / fpx;

                // Проверяем, достигли ли мы желаемой точности
                if (Math.Abs(x_next - x_current) < tolerance || Math.Abs(Expression(x_next)) < tolerance)
                {
                    Operation.Resolve = $"Корень найден: {x_next}";
                    return; // Завершаем выполнение, так как корень найден
                }

                // Обновляем текущее значение для следующей итерации
                x_current = x_next;
            }

            // Если максимальное количество итераций превышено, сообщаем об этом
            Operation.Resolve = $"Превышено количество итераций {maxIterations}\nПоследний найденный корень: {x_current}";
        }

        public void Secant(double A, double B, double tolerance, int maxIterations)
        {
            Operation = new OperationModel();
            Operation.Description = "Идея: Похож на метод Ньютона, но вместо явной производной f'(x) используется её аппроксимация через разностное отношение (секущую), проведенную через две последние точки (xᵢ, f(xᵢ)) и (xᵢ₋₁, f(xᵢ₋₁)). Требует два начальных приближения x₀ и x₁.\n";
            Operation.Description += "Характеристики:\r\n - Сходимость быстрее линейной (суперлинейная, порядок ~1.618), обычно быстрее метода половинного деления, но медленнее метода Ньютона.\r\n - Не требует вычисления производной.\r\n - Требует два начальных приближения.\r\n - Как и метод Ньютона, может расходиться. Есть риск деления на ноль, если f(xᵢ) ≈ f(xᵢ₋₁).\n";
            double fx0 = Expression(A);
            double fx1 = Expression(B);

            for (int i = 0; i < maxIterations; i++)
            {
                double denominator = fx1 - fx0;
                if (Math.Abs(denominator) < 1e-12)
                {
                    Operation.Resolve = "Знаменатель близок к нулю (f(x1) ≈ f(x0)).";
                    return;
                }

                double x_next = B - fx1 * (A - A) / denominator;

                if (Math.Abs(x_next - B) < tolerance || Math.Abs(Expression(x_next)) < tolerance)
                {
                    Operation.Resolve = $"Корень найден: {x_next}";
                    return;
                }

                // Обновляем точки для следующей итерации
                A = B;
                fx0 = fx1;
                B = x_next;
                fx1 = Expression(B);
            }
            // Можно вернуть последнее значение или выбросить исключение
            Operation.Resolve = $"Превышено количество итераций {maxIterations}\nПоследний найденный корень: {B}";
        }

        public void FixedPointIteration(double initialGuess, double tolerance, int maxIterations)
        {
            Operation = new OperationModel();
            Operation.Description = "Идея: Преобразовать исходное уравнение f(x) = 0 к эквивалентному виду x = g(x). Затем, начиная с начального приближения x₀, строить последовательность xᵢ₊₁ = g(xᵢ). Если последовательность сходится к некоторому значению α, то это значение будет корнем исходного уравнения, так как α = g(α).\r\n";
            Operation.Description += "Характеристики:\r\n - Сходимость зависит от выбора функции g(x). Условие сходимости: |g'(x)| < 1 в окрестности корня.\r\n - Сходимость обычно линейная (если сходится).\r\n - Может расходиться или сходиться очень медленно, если g(x) выбрана неудачно.\r\n - Простота реализации, если подходящая g(x) найдена.\n";

            double x_current = initialGuess;
            for (int i = 0; i < maxIterations; i++)
            {
                double x_next = Expression(x_current);

                if (double.IsNaN(x_next) || double.IsInfinity(x_next))
                {
                 //   Operation.Resolve = "Итерация расходится (NaN или Infinity).";
                    return;
                }

                if (Math.Abs(x_next - x_current) < tolerance)
                {
                 //   Operation.Resolve = $"Найден корень: {x_next}";
                    return;
                }
                x_current = x_next;
            }
          //  Operation.Resolve = $"Превышено количество итераций {maxIterations}\nПоследний найденный корень: {x_current}";
        }

        public void Lagger()
        {
            Operation = new OperationModel();
            Operation.Description = "Метод Лагерра (Laguerre's method): Итерационный метод, обычно быстро сходящийся (кубически) для полиномов с вещественными корнями.\n";

            // Точки, через которые/к которым строим полином
            double[] xData = {-2, -1, 0, 1, 2};
            double[] yData = xData.Select(s => Expression(s)).ToArray();
        //    Operation.Description += $"Входные данные:\n x [{string.Join(", ", xData)}]";
        //    Operation.Description += $"\n y [{string.Join(", ", yData)}]";

            // Аппроксимация (наименьшие квадраты) полиномом 2-й степени
            int degree = 2;
            double[] coeffsApprox = Fit.Polynomial(xData, yData, degree);

         //   Operation.Description += $"\nКоэффициенты аппроксимирующего полинома {degree}-й степени (a0, a1, ...):";
         //   Operation.Description += string.Join(", ", coeffsApprox.Select(c => c.ToString("F4")));
            // Вывод значения аппроксимирующего полинома в точке x=1.5
            double approxValue = MathNet.Numerics.Polynomial.Evaluate(1.5, coeffsApprox);
          //  Operation.Description += $"\nApprox P(1.5) = {approxValue:F4}";

            // Интерполяция (только если число точек = степень + 1)
            // Найдем полином 3-й степени, точно проходящий через 4 точки
            try
            {
                // Для интерполяции MathNet предоставляет другие методы,
                // например, Interpolate.Polynomial или через решение СЛАУ.
                // Fit.Polynomial здесь тоже можно использовать, если degree = xData.Length - 1
                int interpolationDegree = xData.Length - 1;
                double[] coeffsInterpolate = Fit.Polynomial(xData, yData, interpolationDegree);
         //       Operation.Resolve = $"\nКоэффициенты интерполяционного полинома {interpolationDegree}-й степени:";
         //       Operation.Resolve += string.Join(", ", coeffsInterpolate.Select(c => c.ToString("F4")));
                // Проверим, что он проходит через точки
         //       Operation.Resolve += $"\nInterpol P(2) = {MathNet.Numerics.Polynomial.Evaluate(2.0, coeffsInterpolate):F10} (ожидаем 4)";
            }
            catch (Exception ex)
            {
         //       Operation.Resolve = "Ошибка интерполяции";
            }
        }
        /// <summary>
        /// Вычисляет определенный интеграл функции методом левых прямоугольников.
        /// </summary>

        public void RectangleLeft(double a, double b, int n)
        {
            try
            {
                if (Expression == null) throw new ArgumentException("Опишите функцию");
                if (n <= 0) throw new ArgumentException("Количество шагов n должно быть положительным.", nameof(n));
                if (b <= a) throw new ArgumentException("Верхний предел b должен быть больше нижнего предела a.", nameof(b));
            }
            catch (Exception ex)
            {
                Operation.Description = ex.Message;
                return;
            }

            Operation.Description += "Вычисляем интеграл функции методом левых прямоугольников";
            Operation.Description += $"\nКоординаты: x0 = {a:F2}, x1 = {b:F2}";

            double h = (b - a) / n; // Шаг интегрирования
            double sum = 0;

            for (int i = 0; i < n; i++) // Суммируем n прямоугольников, используя левые точки
            {
                double xi = a + i * h;
                sum += Expression(xi);
            }
            Operation.Resolve += $"\nПо методу левых прямоугольников, приближенное значение интеграла равно: {h * sum:F10}";
        }

        /// <summary>
        /// Вычисляет определенный интеграл функции методом правых прямоугольников.
        /// </summary>
        // Параметры и исключения аналогичны RectangleLeft
        public void RectangleRight(double a, double b, int n)
        {
            try
            {
                if (Expression == null) throw new ArgumentException("Опишите функцию");
                if (n <= 0) throw new ArgumentException("Количество шагов n должно быть положительным.", nameof(n));
                if (b <= a) throw new ArgumentException("Верхний предел b должен быть больше нижнего предела a.", nameof(b));
            }
            catch (Exception ex)
            {
                Operation.Description = ex.Message;
                return;
            }
            Operation.Description += "\nВычисляем интеграл функции методом правых прямоугольников";
            Operation.Description += $"\nКоординаты: x0 = {a:F2}, x1 = {b:F2}";

            double h = (b - a) / n;
            double sum = 0;

            for (int i = 1; i <= n; i++) // Суммируем n прямоугольников, используя правые точки (индекс от 1 до n)
            {
                double xi = a + i * h;
                sum += Expression(xi);
            }
            Operation.Resolve += $"\nПо методу правых прямоугольников, приближенное значение интеграла равно: {h * sum}";
        }

        /// <summary>
        /// Вычисляет определенный интеграл функции методом средних (центральных) прямоугольников.
        /// </summary>
        // Параметры и исключения аналогичны RectangleLeft
        public void RectangleMidpoint(double a, double b, int n)
        {
            try
            {
                if (Expression == null) throw new ArgumentException("Опишите функцию");
                if (n <= 0) throw new ArgumentException("Количество шагов n должно быть положительным.", nameof(n));
                if (b <= a) throw new ArgumentException("Верхний предел b должен быть больше нижнего предела a.", nameof(b));
            }
            catch (Exception ex)
            {
                Operation.Description = ex.Message;
                return;
            }
            Operation.Description += "\nВычисляем интеграл функции методом средних (центральных) прямоугольников";
            Operation.Description += $"\nКоординаты: x0 = {a:F2}, x1 = {b:F2}";
            double h = (b - a) / n;
            double sum = 0;

            for (int i = 0; i < n; i++) // Суммируем n прямоугольников, используя средние точки
            {
                double midpoint = a + (i + 0.5) * h; // Находим середину i-го интервала
                sum += Expression(midpoint);
            }

            Operation.Resolve += $"\nПо методу средних (центральных) прямоугольников, приближенное значение интеграла равно: {(h * sum):F10}";
        }

        public void TrapezoidalRule(double a, double b, int n)
        {
            Operation = new();
            try
            {
                if (Expression == null) throw new ArgumentException("Опишите функцию");
                if (n <= 0) throw new ArgumentException("Количество шагов n должно быть положительным.", nameof(n));
                if (b <= a) throw new ArgumentException("Верхний предел b должен быть больше нижнего предела a.", nameof(b));
            }
            catch (Exception ex)
            {
                Operation.Description = ex.Message;
                return;
            }
            Operation.Description += "\nВычисляем интеграл функции методом трапеций";
            Operation.Description += $"\nКоординаты: x0 = {a:F2}, x1 = {b:F2}";

            double h = (b - a) / n; // Шаг интегрирования
            Operation.Description += $"\nШаг интегрирования равен: {h:F2}";
                                    // Начинаем сумму с полусуммы значений на концах отрезка
            double sum = (Expression(a) + Expression(b)) / 2.0;

            // Добавляем значения во внутренних точках (умноженные на 1)
            for (int i = 1; i < n; i++) // Цикл от 1 до n-1
            {
                double xi = a + i * h;
                sum += Expression(xi);
            }
            Operation.Resolve = $"\nПо методу трапеций, приближенное значение интеграла равно: {(h * sum):F10}";
        }

        public void SimpsonsRuleSingleLoop(double a, double b, int n)
        {
            Operation = new();
            try
            {
                if (Expression == null) throw new ArgumentException("Опишите функцию");
                if (n <= 0) throw new ArgumentException("Количество шагов n должно быть положительным.", nameof(n));
                if (b <= a) throw new ArgumentException("Верхний предел b должен быть больше нижнего предела a.", nameof(b));
                if (n % 2 != 0) throw new ArgumentException("Количество шагов n для формулы Симпсона должно быть четным.", nameof(n));
            }
            catch (Exception ex)
            {
                Operation.Description = ex.Message;
                return;
            }
            Operation.Description += "\nВычисляем интеграл функции методом Симпсона";
            Operation.Description += $"\nКоординаты: x0 = {a:F2}, x1 = {b:F2}";
            double h = (b - a) / n;
            Operation.Description += $"\nШаг интегрирования равен: {h:F2}";
            double sum = Expression(a) + Expression(b); // Коэффициенты 1 для концов
            Operation.Description += $"\nКоэффициенты 1 для концов: {sum:F2}";
            for (int i = 1; i < n; i++) // Цикл по всем внутренним точкам
            {
                double xi = a + i * h;
                if (i % 2 != 0) // Нечетный индекс
                {
                    sum += 4 * Expression(xi); // Коэффициент 4
                }
                else // Четный индекс
                {
                    sum += 2 * Expression(xi); // Коэффициент 2
                }
            }
            Operation.Resolve = $"\nПо методу Симпсона, приближенное значение интеграла равно: {((h / 3.0) * sum):F10}";
        }

        public void Gauss(double a, double b, int n)
        {
            Operation = new();
            var hasPoints = !GaussLegendrePoints.TryGetValue(n, out var points);
            try
            {
                if (Expression == null) throw new ArgumentException("Опишите функцию");
                if (b <= a) throw new ArgumentException("Верхний предел b должен быть больше нижнего предела a.", nameof(b));
                if (hasPoints) throw new ArgumentException($"Узлы и веса для квадратуры Гаусса-Лежандра с n={n} не предвычислены в данной реализации.", nameof(n));
            }
            catch (Exception ex)
            {
                Operation.Description = ex.Message;
                return;
            }
            Operation.Description += "\nВычисляем интеграл функции методом Гаусса";
            Operation.Description += $"\nКоординаты: x0 = {a:F2}, x1 = {b:F2}";

            double sum = 0;
            double c1 = (b - a) / 2.0; // Коэффициент масштабирования и для dx
            double c2 = (a + b) / 2.0; // Сдвиг центра интервала
            double[] nodes = points.Nodes;   // Узлы T_i на [-1, 1]
            double[] weights = points.Weights; // Веса W_i на [-1, 1]

            Operation.Description += $"\nКоэффициент масштабирования и для dx: {c1:F2}";
            Operation.Description += $"\nСдвиг центра интервала: {c2:F2}";

            for (int i = 0; i < n; i++)
            {
                double t_i = nodes[i];     // Узел на [-1, 1]
                double w_i = weights[i];   // Вес на [-1, 1]

                // Преобразуем узел t_i из [-1, 1] в узел x_i на [a, b]
                double x_i = c1 * t_i + c2;

                // Вычисляем f(x_i)
                double f_xi = Expression(x_i);

                // Добавляем в сумму согласно формуле: W_i * f(x_i)
                sum += w_i * f_xi;
            }
            Operation.Resolve = $"\nПо методу Гаусса, приближенное значение интеграла равно: {(c1 * sum):F10}";
        }
        public void LagrangeInterpolate(double[]? xNodes, double[]? yNodes, double xTarget)
        {
            Operation = new();
            Operation.Description = "Идея:\r\n\r\nДля заданного набора из n+1 точки (x₀, y₀), (x₁, y₁), ..., (xi, yi), где все xᵢ различны, существует единственный полином P(x) степени не выше n, такой что P(xᵢ) = yᵢ для всех i = 0, ..., n. Формула Лагранжа предоставляет прямой способ построения этого ";
            Operation.Description += "\n\nЭта конструкция гарантирует, что при подстановке любого xᵢ из исходного набора, все слагаемые в сумме, кроме одного (при j=i), обратятся в ноль из-за свойства базисных полиномов, а i-е слагаемое будет равно yᵢ * L,ᵢ(xᵢ) = yᵢ * 1 = yᵢ. Таким образом, P(xᵢ) = yᵢ.";
            Operation.Description += "\n\nПреимущества:\r\nПростая и понятная формула для построения интерполяционного полинома.\r\nГарантированно дает единственный полином нужной степени, проходящий через точки.";
            try
            {
                if (xNodes == null) throw new ArgumentException("Опишите функцию");
                if (yNodes == null) throw new ArgumentException("Опишите функцию");
                if (xNodes.Length != yNodes.Length) throw new ArgumentException("Массивы x и y должны иметь одинаковую длину.");
                if (xNodes.Length == 0) throw new ArgumentException("Массивы узлов не могут быть пустыми.");
            }
            catch(Exception ex)
            {
                Operation.Description = ex.Message;
                return;
            }

            // Проверка на уникальность узлов x (важно для знаменателя)
            if (xNodes.Length != xNodes.Distinct().Count())
            {
                Operation.Resolve = "Узлы интерполяции x должны быть уникальными.";
                return;
            }

            int n = xNodes.Length; // Количество точек (n+1 в формуле, здесь n - это размер массива)
            double lagrangePolynomialValue = 0;

            // Внешний цикл по j (согласно формуле P(x) = Сумма y_j * L_j(x))
            for (int j = 0; j < n; j++)
            {
                double basisPolynomial_j = 1.0; // Значение L_j(xTarget)

                // Внутренний цикл по k для вычисления L_j(xTarget) = Произведение (xTarget - x_k) / (x_j - x_k) для k != j
                for (int k = 0; k < n; k++)
                {
                    if (j != k)
                    {
                        // Проверка знаменателя на ноль (хотя проверка на уникальность x уже была)
                        double denominator = xNodes[j] - xNodes[k];
                        if (Math.Abs(denominator) < 1e-15) // Малое число для сравнения с нулем
                        {
                            // Эта ситуация не должна возникать после проверки на Distinct,
                            // но оставим как дополнительную защиту.
                            Operation.Resolve = $"Обнаружены очень близкие или одинаковые узлы x: x[{j}]={xNodes[j]}, x[{k}]={xNodes[k]}";
                            return;
                        }
                        basisPolynomial_j *= (xTarget - xNodes[k]) / denominator;
                    }
                }

                // Добавляем слагаемое y_j * L_j(xTarget) к общей сумме
                lagrangePolynomialValue += yNodes[j] * basisPolynomial_j;
            }
            Operation.Resolve = $"Интерполированное значение в точке x = {xTarget:F4}: y = {lagrangePolynomialValue:F8}";
        }

        public void CubicSplineInterpolate(double[]? xNodes, double[]? yNodes, double xTarget)
        {
            Operation = new();
            Operation.Description = "Идея:\r\n\r\nМетод сплайнов использует кусочно-полиномиальную функцию\nНа каждом отрезке [xᵢ, xᵢ₊₁] строится свой кубический полином Sᵢ(x):\r\nSᵢ(x) = aᵢ + bᵢ(x - xᵢ) + cᵢ(x - xᵢ)² + dᵢ(x - xᵢ)³";
            Operation.Description += "\n\nЭти кубические \"кусочки\" соединяются вместе так, чтобы итоговая функция S(x) (которая равна Sᵢ(x) на отрезке [xᵢ, xᵢ₊₁]) удовлетворяла следующим условиям:\r\n\r\n - Интерполяция: Функция должна проходить через все заданные точки: S(xᵢ) = yᵢ для всех i = 0, ..., n.\r\n - Гладкость (Непрерывность производных): В точках соединения (\"узлах\" x₁, ..., x<0xE2><0x82><0x99>₋₁) первая и вторая производные соседних кубических полиномов должны совпадать:\r\n - S'ᵢ₋₁(xᵢ) = S'ᵢ(xᵢ) (Непрерывность первой производной - нет \"изломов\")\r\n - S''ᵢ₋₁(xᵢ) = S''ᵢ(xᵢ) (Непрерывность второй производной - нет резких изменений кривизны)";
            Operation.Description += "\n\nОпределение Коэффициентов и Граничные Условия:\r\n\r\nЭти условия порождают систему линейных уравнений для нахождения коэффициентов aᵢ, bᵢ, cᵢ, dᵢ всех полиномов. Однако количество уравнений оказывается на два меньше, чем количество неизвестных коэффициентов. Чтобы сделать решение единственным, необходимо добавить два граничных условия. Наиболее распространены:\r\n\r\n - Естественный сплайн (Natural Spline): Вторые производные на концах интервала равны нулю: S''(x₀) = 0 и S'' = 0. Это наиболее часто используемый тип сплайна по умолчанию, он соответствует \"гибкой линейке\", проходящей через точки без приложения дополнительных изгибающих моментов на концах.\r\n - Закрепленный сплайн (Clamped Spline): Задаются значения первой производной на концах интервала: S'(x₀) = y'₀ и S' = y'. Требует знания производных в конечных точках.";
            try
            {
                if (xNodes == null) throw new ArgumentException("Опишите функцию");
                if (yNodes == null) throw new ArgumentException("Опишите функцию");
                if (xNodes.Length != yNodes.Length) throw new ArgumentException("Массивы x и y должны иметь одинаковую длину.");
                if (xNodes.Length == 0) throw new ArgumentException("Массивы узлов не могут быть пустыми.");
            }
            catch (Exception ex)
            {
                Operation.Description = ex.Message;
                return;
            }

            // Проверка на уникальность узлов x (важно для знаменателя)
            if (xNodes.Length != xNodes.Distinct().Count())
            {
                Operation.Resolve = "Узлы интерполяции x должны быть уникальными.";
                return;
            }

            var new_nodes = xNodes.OrderBy(x => x).ToArray();
            var spline = new CubicSpline(new_nodes, yNodes);
            var interpolateValue = spline.Interpolate(xTarget);

          //  Operation.Resolve = $"Интерполированное значение в точке x = {xTarget:F4}: y = {interpolateValue:F8}";
        }

        public void MinimalRectangle(double[] xNodes, double[] yNodes, double xTarget, int degree = 2)
        {
            Operation = new();
            Operation.Description += "Идея:\r\n\r\nВ отличие от интерполяции, где мы строим функцию, точно проходящую через все заданные точки, МНК используется, когда:\r\n\r\n - Данных много, и они могут содержать погрешности (шум).\r\n - Мы хотим найти простую модель (например, прямую, параболу), которая наилучшим образом описывает общую тенденцию в данных, не обязательно проходя через каждую точку.\r\n - МНК ищет параметры такой модели f(x, β), которая минимизирует сумму квадратов отклонений (остатков) между реальными значениями yᵢ и значениями f(xᵢ, β), предсказанными моделью:";
            Operation.Description += "\nМНК ищет параметры такой модели f(x, β), которая минимизирует сумму квадратов отклонений (остатков) между реальными значениями yᵢ и значениями f(xᵢ, β), предсказанными моделью:";
            Operation.Description += "\nРешение:\r\n\r\nЗадача сводится к решению системы линейных алгебраических уравнений (СЛАУ) Aβ = b, где A = XᵀX и b = Xᵀy. Матрица A квадратная (n x n), симметричная и часто положительно определенная, что позволяет использовать эффективные методы решения СЛАУ (например, метод Гаусса, разложение Холецкого).";
            try
            {
                if (xNodes == null) throw new ArgumentException("Опишите функцию");
                if (yNodes == null) throw new ArgumentException("Опишите функцию");
                if (xNodes.Length != yNodes.Length) throw new ArgumentException("Массивы x и y должны иметь одинаковую длину.");
                if (degree < 0) throw new ArgumentException("Степень полинома должна быть неотрицательной.", nameof(degree));
            }
            catch (Exception ex)
            {
                Operation.Description = ex.Message;
                return;
            }
            int numPoints = xNodes.Length;
            int numCoefficients = degree + 1; // β0, β1, ..., β_degree

            if (numPoints < numCoefficients)
            {
                Operation.Resolve = $"Для полинома степени {degree} требуется как минимум {numCoefficients} точек данных.";
                return;
            }

            // 1. Создание матрицы плана X (размер numPoints x numCoefficients)
            double[,] designMatrixX = new double[numPoints, numCoefficients];
            for (int i = 0; i < numPoints; i++)
            {
                for (int j = 0; j < numCoefficients; j++)
                {
                    designMatrixX[i, j] = Math.Pow(xNodes[i], j); // X_ij = x_i ^ j
                }
            }

            // 2. Создание вектора y
            double[] yVector = yNodes; // Используем yData напрямую как вектор-столбец (логически)

            // 3. Вычисление X^T (транспонирование)
            double[,] designMatrixXT = Transpose(designMatrixX);

            // 4. Вычисление A = X^T * X (размер numCoefficients x numCoefficients)
            double[,] matrixA = Multiply(designMatrixXT, designMatrixX);

            // 5. Вычисление b = X^T * y (размер numCoefficients x 1)
            double[] vectorB = Multiply(designMatrixXT, yVector);

            // 6. Решение системы нормальных уравнений A * beta = b
            // Используем метод Гаусса (предполагается, что он реализован где-то)
            double[] coefficients = SolveLinearSystem(matrixA, vectorB);

            var polynomial = EvaluatePolynomial(coefficients, xTarget);
            Operation.Resolve = $"Полином в точке x = {xTarget}, найден: {polynomial}";
        }

        private double EvaluatePolynomial(double[] coefficients, double x)
        {
            double result = 0;
            double xPower = 1; // x^0
            for (int i = 0; i < coefficients.Length; i++)
            {
                result += coefficients[i] * xPower;
                xPower *= x; // x^1, x^2, ...
            }
            return result;
        }

        private static double[,] Transpose(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            double[,] result = new double[cols, rows];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }
            return result;
        }

        private static double[,] Multiply(double[,] matrixA, double[,] matrixB)
        {
            int aRows = matrixA.GetLength(0); int aCols = matrixA.GetLength(1);
            int bRows = matrixB.GetLength(0); int bCols = matrixB.GetLength(1);
            if (aCols != bRows) throw new ArgumentException("Несовместимые размеры матриц для умножения.");

            double[,] result = new double[aRows, bCols];
            for (int i = 0; i < aRows; i++)
            {
                for (int j = 0; j < bCols; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < aCols; k++)
                    {
                        sum += matrixA[i, k] * matrixB[k, j];
                    }
                    result[i, j] = sum;
                }
            }
            return result;
        }

        private static double[] Multiply(double[,] matrixA, double[] vectorB)
        {
            int aRows = matrixA.GetLength(0); int aCols = matrixA.GetLength(1);
            int bRows = vectorB.Length;
            if (aCols != bRows) throw new ArgumentException("Несовместимые размеры матрицы и вектора для умножения.");

            double[] result = new double[aRows];
            for (int i = 0; i < aRows; i++)
            {
                double sum = 0;
                for (int j = 0; j < aCols; j++)
                {
                    sum += matrixA[i, j] * vectorB[j];
                }
                result[i] = sum;
            }
            return result;
        }
        private static double[] SolveLinearSystem(double[,] matrixA, double[] vectorB)
        {
            int n = vectorB.Length;
            if (matrixA.GetLength(0) != n || matrixA.GetLength(1) != n)
                throw new ArgumentException("Несовместимые размеры матрицы и вектора для решения СЛАУ.");

            // Создаем расширенную матрицу
            double[,] augmentedMatrix = new double[n, n + 1];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    augmentedMatrix[i, j] = matrixA[i, j];
                augmentedMatrix[i, n] = vectorB[i];
            }

            // Прямой ход Гаусса с частичным выбором главного элемента
            for (int k = 0; k < n; k++)
            {
                int maxRowIndex = k;
                for (int i = k + 1; i < n; i++)
                    if (Math.Abs(augmentedMatrix[i, k]) > Math.Abs(augmentedMatrix[maxRowIndex, k]))
                        maxRowIndex = i;

                if (maxRowIndex != k) // Перестановка строк
                    for (int j = k; j <= n; j++)
                    {
                        double temp = augmentedMatrix[k, j];
                        augmentedMatrix[k, j] = augmentedMatrix[maxRowIndex, j];
                        augmentedMatrix[maxRowIndex, j] = temp;
                    }

                if (Math.Abs(augmentedMatrix[k, k]) < 1e-12) // Проверка на сингулярность
                    throw new InvalidOperationException("Матрица системы вырождена или близка к вырожденной.");

                for (int i = k + 1; i < n; i++) // Обнуление под диагональю
                {
                    double factor = augmentedMatrix[i, k] / augmentedMatrix[k, k];
                    augmentedMatrix[i, k] = 0;
                    for (int j = k + 1; j <= n; j++)
                        augmentedMatrix[i, j] -= factor * augmentedMatrix[k, j];
                }
            }

            // Обратный ход
            double[] solution = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < n; j++)
                    sum += augmentedMatrix[i, j] * solution[j];
                solution[i] = (augmentedMatrix[i, n] - sum) / augmentedMatrix[i, i];
            }

            return solution;
        }
    }
}
