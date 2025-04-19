using Grapher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher.Utils
{
    public class SLAUSqulptor
    {
        public double Epsilon = 1e-10;
        public OperationModel Operation { get; private set; }
        public double[]? SolveGauss(double[,] augmentedMatrix)
        {
            Operation = new();
            if (augmentedMatrix == null)
            {
                throw new ArgumentNullException(nameof(augmentedMatrix));
            }

            int rows = augmentedMatrix.GetLength(0); // Количество уравнений (строк)
            int cols = augmentedMatrix.GetLength(1); // Количество столбцов (n + 1)
            int n = cols - 1; // Количество неизвестных

            // Создаем копию матрицы, чтобы не изменять исходную
            double[,] matrix = (double[,])augmentedMatrix.Clone();

            Operation.Description += "1. Реализуем прямой ход";
            // --- Прямой ход (Forward Elimination) ---
            for (int k = 0; k < n; k++)
            {
                // 1. Выбор главного элемента (Partial Pivoting)
                int maxRowIndex = k;
                for (int i = k + 1; i < rows; i++)
                {
                    if (Math.Abs(matrix[i, k]) > Math.Abs(matrix[maxRowIndex, k]))
                    {
                        maxRowIndex = i;
                    }
                }
                Operation.Description += $"\n1.{k+1}: Главный элемент на позиции: {k + 1}:{maxRowIndex + 1} равный {matrix[k, maxRowIndex]}";
                // 2. Перестановка строк k и maxRowIndex
                if (maxRowIndex != k)
                {
                    for (int j = k; j < cols; j++)
                    {
                        double temp = matrix[k, j];
                        matrix[k, j] = matrix[maxRowIndex, j];
                        matrix[maxRowIndex, j] = temp;
                    }
                }

                // 3. Проверка на сингулярность (нулевой пивот)
                if (Math.Abs(matrix[k, k]) < Epsilon)
                {
                    // Если пивот близок к нулю, система может быть вырожденной
                    // (нет решений или бесконечно много).
                    // В данном простом варианте просто возвращаем null.
                    // Более сложная реализация могла бы проверять оставшуюся часть строки
                    // для определения: нет решений ([0...0 | c!=0]) или бесконечно много ([0...0 | 0]).
                    Operation.Resolve = $"1.{k + 1}: Пивот на шаге {k + 1} близок к нулю. Система может не иметь единственного решения.";
                    return null;
                }

                // 4. Нормализация k-й строки (не обязательно, но иногда делают)
                // double pivot = matrix[k, k];
                // for (int j = k; j < cols; j++) {
                //     matrix[k, j] /= pivot;
                // }

                // 5. Обнуление элементов под пивотом в k-м столбце
                for (int i = k + 1; i < rows; i++)
                {
                    double factor = matrix[i, k] / matrix[k, k];
                    matrix[i, k] = 0; // Явно обнуляем для точности
                    for (int j = k + 1; j < cols; j++)
                    {
                        matrix[i, j] -= factor * matrix[k, j];
                    }
                    // Проверка на несовместность на лету (опционально)
                    // bool allZero = true;
                    // for(int l=0; l < n; ++l) { if (Math.Abs(matrix[i,l]) > Epsilon) allZero = false; }
                    // if (allZero && Math.Abs(matrix[i,n]) > Epsilon) return null; // Строка [0...0 | c!=0] -> нет решений
                }
            }

            // --- Обратный ход (Back Substitution) ---
            double[] solution = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < n; j++)
                {
                    sum += matrix[i, j] * solution[j];
                }

                // Проверка на случай, если диагональный элемент все же оказался нулевым
                // (хотя пивотинг должен был это предотвратить для невырожденных систем)
                if (Math.Abs(matrix[i, i]) < Epsilon)
                {
                    Operation.Resolve = $"Нулевой диагональный элемент на шаге {i + 1} обратного хода. Система может не иметь единственного решения.";
                    Operation.Resolve += "Вывод: Бесконечно много решений в том случае, если система совместима";
                    return null;
                }

                solution[i] = (matrix[i, n] - sum) / matrix[i, i];
            }

            // Проверка на несовместность (после приведения к треугольному виду)
            // Это более надежная проверка, чем на лету
            for (int i = n; i < rows; ++i) // Проверяем "лишние" строки, если они были
            {
                bool allZeroCoeffs = true;
                for (int j = 0; j < n; ++j)
                {
                    if (Math.Abs(matrix[i, j]) > Epsilon)
                    {
                        allZeroCoeffs = false;
                        break;
                    }
                }
                if (allZeroCoeffs && Math.Abs(matrix[i, n]) > Epsilon)
                {
                    Operation.Resolve = "Обнаружена несовместная строка {i + 1}: [0 ... 0 | {matrix[i, n]}]. Решений нет.";
                    return null;
                }
            }

            Operation.Resolve = $"Решение найдено: {string.Join(", ", solution.Select((s, i) => $"x{i}={s}"))}";
            return solution;
        }

    

        public double[]? SolveJordanGauss(double[,] augmentedMatrix)
        {
            Operation = new();
            if (augmentedMatrix == null)
            {
                throw new ArgumentNullException(nameof(augmentedMatrix));
            }

            int rows = augmentedMatrix.GetLength(0); // Количество уравнений (строк)
            int cols = augmentedMatrix.GetLength(1); // Количество столбцов (n + 1)
            int n = cols - 1; // Количество неизвестных

            // Создаем копию, чтобы не изменять оригинал
            double[,] matrix = (double[,])augmentedMatrix.Clone();

            int pivotRow = 0; // Текущая строка для поиска пивота
            for (int k = 0; k < n && pivotRow < rows; k++) // Идем по столбцам (переменным)
            {
                // 1. Найти строку с максимальным элементом в текущем столбце k (начиная с pivotRow)
                int maxRowIndex = pivotRow;
                for (int i = pivotRow + 1; i < rows; i++)
                {
                    if (Math.Abs(matrix[i, k]) > Math.Abs(matrix[maxRowIndex, k]))
                    {
                        maxRowIndex = i;
                    }
                }

                // Если максимальный элемент близок к нулю, в этом столбце нет пивота, переходим к следующему
                if (Math.Abs(matrix[maxRowIndex, k]) < Epsilon)
                {
                    Operation.Description += $"\n{k + 1} В столбце не найдено пивота";
                    continue; // Пропустить этот столбец
                }

                // 2. Поменять местами pivotRow и maxRowIndex
                if (maxRowIndex != pivotRow)
                {
                    for (int j = k; j < cols; j++)
                    {
                        double temp = matrix[pivotRow, j];
                        matrix[pivotRow, j] = matrix[maxRowIndex, j];
                        matrix[maxRowIndex, j] = temp;
                    }
                }

                // 3. Нормализовать pivotRow (сделать пивот = 1)
                double pivotValue = matrix[pivotRow, k];
                for (int j = k; j < cols; j++)
                {
                    matrix[pivotRow, j] /= pivotValue;
                }

                // 4. Обнулить все остальные элементы в столбце k (включая те, что выше пивота)
                for (int i = 0; i < rows; i++)
                {
                    if (i != pivotRow)
                    {
                        double factor = matrix[i, k];
                        // matrix[i, k] = 0; // Не обязательно, т.к. вычитание сделает это, но можно для явности
                        for (int j = k; j < cols; j++) // Вычитаем нормализованную pivotRow * factor
                        {
                            matrix[i, j] -= factor * matrix[pivotRow, j];
                        }
                    }
                }

                pivotRow++; // Переходим к следующей строке для поиска пивота
            }

            // --- Анализ результата и извлечение решения ---

            // 5. Проверка на несовместность
            for (int i = pivotRow; i < rows; i++) // Проверяем строки, где могли остаться только нули в части A
            {
                if (Math.Abs(matrix[i, n]) > Epsilon) // Если в части A нули, а в части b не ноль
                {
                    Operation.Resolve = $"Система несовместна (обнаружена строка [{i + 1}] вида [0...0 | c!=0]).";
                    return null;
                }
            }

            // 6. Проверка на бесконечное число решений
            if (pivotRow < n) // Если количество найденных пивотов меньше числа переменных
            {
                Operation.Resolve = $"Система имеет бесконечно много решений (ранг={pivotRow} < числа переменных={n}).";
                return null;
            }

            // 7. Извлечение единственного решения
            double[] solution = new double[n];
            for (int i = 0; i < n; i++)
            {
                // В идеальной ситуации matrix[i, i] == 1, и решение находится в последнем столбце
                // Но из-за пропущенных столбцов (шаг 1) пивот может быть не на диагонали.
                // Правильнее искать строку, где matrix[row, i] == 1 (пивот для i-й переменной)
                // Но в нашей реализации, если pivotRow == n, то пивоты должны быть на диагонали [0..n-1]
                if (Math.Abs(matrix[i, i] - 1.0) < Epsilon) // Убедимся что на диагонали 1
                {
                    solution[i] = matrix[i, n];
                }
                else
                {
                    // Эта ситуация не должна возникать если pivotRow == n и нет ошибок
                    Operation.Description += $"\nОшибка: Ожидался пивот = 1 в позиции [{i + 1},{i + 1}], но его там нет.";
                    // Можно добавить поиск строки с пивотом для столбца i
                    bool foundPivot = false;
                    for (int r = 0; r < n; ++r)
                    {
                        if (Math.Abs(matrix[r, i] - 1.0) < Epsilon)
                        {
                            // Проверить, что остальные элементы столбца i нулевые
                            bool colIsClean = true;
                            for (int check_r = 0; check_r < n; ++check_r) { if (check_r != r && Math.Abs(matrix[check_r, i]) > Epsilon) colIsClean = false; }

                            if (colIsClean)
                            {
                                solution[i] = matrix[r, n];
                                foundPivot = true;
                                break;
                            }
                        }
                    }
                    if (!foundPivot)
                    {
                        Operation.Resolve = $"\nНе удалось найти пивот для переменной x{i + 1}.";
                        return null; // Ошибка или бесконечное число решений не обработано правильно
                    }
                }
            }
            Operation.Resolve = $"Решение найдено: {string.Join(", ", solution.Select((s, i) => $"x{i + 1}={s}"))}";
            return solution;
        }

        public double[]? SolveIteration(
                    double[,] aMatrix,
                    double[] bVector,
                    double tolerance = 1e-6,
                    int maxIterations = 1000,
                    double[]? initialGuess = null)
        {
            Operation = new();

            int n = bVector.Length;
            if (aMatrix.GetLength(0) != n || aMatrix.GetLength(1) != n)
            {
             //   Operation.Description = "Размеры матрицы A и вектора b несовместимы.";
                return null;
            }

            // Проверка на нулевые диагональные элементы
            for (int i = 0; i < n; i++)
            {
                if (Math.Abs(aMatrix[i, i]) < double.Epsilon) // Используем Epsilon для сравнения с нулем
                {
                //    Operation.Resolve = $"Нулевой диагональный элемент a[{i},{i}]. Метод простой итерации неприменим без преобразований.";
                    return null;
                }
            }

            // Опциональная проверка диагонального преобладания (для предупреждения)
            if (!HasDiagonalDominance(aMatrix))
            {
              //  Operation.Description += "Предупреждение: Матрица не обладает строгим диагональным преобладанием. Сходимость метода не гарантирована.";
            }

            double[] x = new double[n]; // Текущее приближение x(k)
            double[] x_new = new double[n]; // Следующее приближение x(k+1)

            // Инициализация начального приближения
            if (initialGuess != null)
            {
                if (initialGuess.Length != n)
                {
                  //  Operation.Resolve = "Размер начального приближения не совпадает с размером системы.";
                    return null;
                }
                Array.Copy(initialGuess, x, n);
            }
            else
            {
                // По умолчанию используем нулевой вектор
                for (int i = 0; i < n; i++) x[i] = 0;
            }

            // Итерационный процесс
            for (int k = 0; k < maxIterations; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (i != j)
                        {
                            sum += aMatrix[i, j] * x[j]; // Используем значения из x(k)
                        }
                    }
                    x_new[i] = (bVector[i] - sum) / aMatrix[i, i];
                }

                // Проверка критерия остановки (по максимальной разности компонент)
                double maxDiff = 0;
                for (int i = 0; i < n; i++)
                {
                    maxDiff = Math.Max(maxDiff, Math.Abs(x_new[i] - x[i]));
                }

                // Обновление приближения
                Array.Copy(x_new, x, n);

                if (maxDiff < tolerance)
                {
                 //   Operation.Resolve = ($"Метод сошелся за {k + 1} итераций. Макс. разница: {maxDiff}");
                  //  Operation.Resolve += $"\nРешение найдено: {string.Join(", ", x.Select((s, i) => $"x{i + 1}={s}"))}";
                }
            }

            // Если цикл завершился без сходимости
            try
            {
              //  Operation.Resolve = ($"Метод не сошелся за {maxIterations} итераций. Последняя макс. разница: {CalculateMaxDifference(x, x_new)}"); // Нужна функция для расчета разницы последнего шага
            }
            catch(Exception ex)
            {
                Operation.Resolve = ex.Message;
            }
            return null;
        }

        public double[]? SolveZaidel(
        double[,] aMatrix,
        double[] bVector,
        double tolerance = 1e-6,
        int maxIterations = 1000,
        double[]? initialGuess = null)
        {
            Operation = new();
            int n = bVector.Length;
            // Проверка на нулевые диагональные элементы
            for (int i = 0; i < n; i++)
            {
                if (Math.Abs(aMatrix[i, i]) < Epsilon)
                {
                   // Operation.Resolve = $"Нулевой диагональный элемент a[{i},{i}]. Метод Зейделя неприменим без преобразований.";
                    return null;
                }
            }

            // Опциональная проверка диагонального преобладания (как в методе Якоби)
            if (!HasDiagonalDominance(aMatrix)) // Используем проверку из предыдущего примера
            {
               // Operation.Description += "\nПредупреждение: Матрица не обладает строгим диагональным преобладанием. Сходимость метода Зейделя не гарантирована.";
            }

            double[] x = new double[n]; // Текущий/обновляемый вектор приближения x(k) -> x(k+1)
            double[] x_old = new double[n]; // Вектор приближения на начало итерации x(k) для проверки сходимости

            // Инициализация начального приближения
            if (initialGuess != null)
            {
                if (initialGuess.Length != n)
                {
                 //   Operation.Resolve = "Размер начального приближения не совпадает с размером системы.";
                    return null;
                }
                Array.Copy(initialGuess, x, n);
            }
            else
            {
                // По умолчанию используем нулевой вектор
                for (int i = 0; i < n; i++) x[i] = 0;
            }

            // Итерационный процесс
            for (int k = 0; k < maxIterations; k++)
            {
                // Сохраняем текущее состояние x в x_old для последующей проверки сходимости
                Array.Copy(x, x_old, n);

                // Обновляем компоненты вектора x "на месте"
                for (int i = 0; i < n; i++)
                {
                    double sum = 0;
                    // Используем текущие значения x, которые включают уже обновленные x[0]..x[i-1]
                    for (int j = 0; j < n; j++)
                    {
                        if (i != j)
                        {
                            sum += aMatrix[i, j] * x[j];
                        }
                    }
                    // Обновляем x[i] немедленно
                    x[i] = (bVector[i] - sum) / aMatrix[i, i];
                }

                // Проверка критерия остановки (сравниваем новый x с x_old)
                double maxDiff = 0;
                for (int i = 0; i < n; i++)
                {
                    maxDiff = Math.Max(maxDiff, Math.Abs(x[i] - x_old[i]));
                }

                if (maxDiff < tolerance)
                {
                  //  Operation.Resolve = $"Метод Зейделя сошелся за {k + 1} итераций. Макс. разница: {maxDiff}";
                    return x; // Возвращаем найденное приближение
                }
            }

            // Если цикл завершился без сходимости
            // Можно вычислить и вывести последнюю разницу при желании
            try
            {
                double finalDiff = CalculateMaxDifference(x, x_old);
               // Operation.Resolve = $"Метод Зейделя не сошелся за {maxIterations} итераций.";
               // Operation.Resolve += $"\nПоследняя найденная максимальная разница составляет:\n{finalDiff}";
            }
            catch(Exception ex)
            {
                Operation.Resolve = ex.Message;
            }
            return null;
        }

        /// <summary>
        /// Вспомогательный метод для проверки строгого диагонального преобладания.
        /// </summary>
        private bool HasDiagonalDominance(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (matrix.GetLength(1) != n) return false; // Не квадратная

            for (int i = 0; i < n; i++)
            {
                double diag = Math.Abs(matrix[i, i]);
                double sum_off_diag = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        sum_off_diag += Math.Abs(matrix[i, j]);
                    }
                }
                if (diag <= sum_off_diag)
                {
                    return false; // Условие не выполнено для строки i
                }
            }
            return true; // Условие выполнено для всех строк
        }

        /// <summary>
        /// Вспомогательный метод для расчета максимальной разницы между двумя векторами.
        /// </summary>
        private double CalculateMaxDifference(double[] v1, double[] v2)
        {
            if (v1.Length != v2.Length) throw new ArgumentException("Векторы должны иметь одинаковую длину.");
            double maxDiff = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                maxDiff = Math.Max(maxDiff, Math.Abs(v1[i] - v2[i]));
            }
            return maxDiff;
        }


    }
}

