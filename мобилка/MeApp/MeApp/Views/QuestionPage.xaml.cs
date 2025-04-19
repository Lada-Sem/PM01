using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionPage : ContentPage
    {
        private List<Question> questions;
        private int currentQuestionIndex = 0;
        private int selectedAnswerIndex = -1;
        private int correctAnswersCount = 0; // Количество правильных ответов

        public QuestionPage(int testIndex)
        {
            InitializeComponent();
            LoadQuestions(testIndex);
            DisplayQuestion();
        }

        private void LoadQuestions(int testIndex)
        {
            questions = new List<Question>();

            switch (testIndex)
            {
                case 0: // Тест 1: Решение уравнений
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для нахождения корней уравнений?",
                        Options = new List<string>
        {
            "Метод Гаусса",
            "Метод Ньютона",
            "Метод Рунге-Кутты",
            "Метод Эйлера"
        },
                        CorrectAnswerIndex = 1
                    });
                    questions.Add(new Question
                    {
                        Text = "Какое из следующих определений соответствует методу Ньютона?",
                        Options = new List<string>
        {
            "Метод нахождения корней с использованием производной",
            "Метод нахождения корней с использованием интервалов",
            "Метод нахождения корней с использованием последовательных приближений",
            "Метод нахождения корней с использованием графиков"
        },
                        CorrectAnswerIndex = 0
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов является итерационным?",
                        Options = new List<string>
        {
            "Метод Гаусса",
            "Метод простой итерации",
            "Метод Рунге-Кутты",
            "Метод трапеций"
        },
                        CorrectAnswerIndex = 1
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов используется для нахождения корней уравнений, если функция не является строго монотонной?",
                        Options = new List<string>
        {
            "Метод половинного деления",
            "Метод Ньютона",
            "Метод секущих",
            "Метод простой итерации"
        },
                        CorrectAnswerIndex = 2
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов требует, чтобы функция была непрерывной на интервале?",
                        Options = new List<string>
        {
            "Метод Гаусса",
            "Метод половинного деления",
            "Метод Рунге-Кутты",
            "Метод Эйлера"
        },
                        CorrectAnswerIndex = 1
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих вариантов является формулой метода Ньютона?",
                        Options = new List<string>
        {
            "x_{n+1} = x_n - f(x_n) / f'(x_n)",
            "x_{n+1} = (a + b) / 2",
            "x_{n+1} = x_n - (f(x_n) * (x_n - x_{n-1})) / (f(x_n) - f(x_{n-1}))",
            "x_{n+1} = x_n + h * f(x_n)"
        },
                        CorrectAnswerIndex = 0
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов является методом численного интегрирования?",
                        Options = new List<string>
        {
            "Метод Ньютона",
            "Метод Эйлера",
            "Метод трапеций",
            "Метод простой итерации"
        },
                        CorrectAnswerIndex = 2
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для нахождения корней уравнений с помощью последовательных приближений?",
                        Options = new List<string>
        {
            "Метод Гаусса",
            "Метод простой итерации",
            "Метод Ньютона",
            "Метод секущих"
        },
                        CorrectAnswerIndex = 1
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод может быть использован для нахождения корней уравнений, если известны два начальных приближения?",
                        Options = new List<string>
        {
            "Метод половинного деления",
            "Метод Ньютона",
            "Метод Рунге-Кутты",
            "Метод простой итерации"
        },
                        CorrectAnswerIndex = 0
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов является формулой для нахождения корней уравнений методом секущих?",
                        Options = new List<string>
        {
            "x_{n+1} = x_n - f(x_n) / f'(x_n)",
            "x_{n+1} = x_n - (f(x_n) * (x_n - x_{n-1})) / (f(x_n) - f(x_{n-1}))",
            "x_{n+1} = (a + b) / 2",
            "x_{n+1} = x_n + h * f(x_n)"
        },
                        CorrectAnswerIndex = 1
                    });
                    break;


                case 1: // Тест 2: Решение систем линейных алгебраических уравнений (СЛАУ)
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для решения СЛАУ с помощью прямого и обратного хода?",
                        Options = new List<string>
        {
            "Метод Гаусса",
            "Метод Жордана-Гаусса",
            "Метод простой итерации",
            "Метод Зейделя"
        },
                        CorrectAnswerIndex = 0
                    });
                    questions.Add(new Question
                    {
                        Text = "Какое из следующих определений соответствует методу Жордана-Гаусса?",
                        Options = new List<string>
        {
            "Метод, который использует прямой и обратный ход для получения диагональной матрицы",
            "Метод, который использует только прямой ход",
            "Метод, который использует итерации для нахождения решения",
            "Метод, который основан на разложении матрицы"
        },
                        CorrectAnswerIndex = 0
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для решения СЛАУ с помощью итераций?",
                        Options = new List<string>
        {
            "Метод Гаусса",
            "Метод Жордана-Гаусса",
            "Метод простой итерации",
            "Метод Зейделя"
        },
                        CorrectAnswerIndex = 2
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов является улучшенной версией метода простой итерации?",
                        Options = new List<string>
        {
            "Метод Гаусса",
            "Метод Жордана-Гаусса",
            "Метод Зейделя",
            "Метод релаксации"
        },
                        CorrectAnswerIndex = 3
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для решения СЛАУ с помощью итераций, основанных на предыдущих значениях?",
                        Options = new List<string>
        {
            "Метод Гаусса",
            "Метод Жордана-Гаусса",
            "Метод простой итерации",
            "Метод Зейделя"
        },
                        CorrectAnswerIndex = 3
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов является формулой для решения СЛАУ методом Гаусса?",
                        Options = new List<string>
        {
            "Ax = b",
            "x = A^{-1}b",
            "x_{n+1} = x_n - (f(x_n) * (x_n - x_{n-1})) / (f(x_n) - f(x_{n-1}))",
            "x_{n+1} = x_n + h * f(x_n)"
        },
                        CorrectAnswerIndex = 0
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для нахождения решения СЛАУ, если матрица является вырожденной?",
                        Options = new List<string>
        {
            "Метод Гаусса",
            "Метод Жордана-Гаусса",
            "Метод Зейделя",
            "Метод обратной матрицы"
        },
                        CorrectAnswerIndex = 2
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов является итерационным методом для решения СЛАУ?",
                        Options = new List<string>
        {
            "Метод Гаусса",
            "Метод Жордана-Гаусса",
            "Метод Зейделя",
            "Метод Крамера"
        },
                        CorrectAnswerIndex = 2
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для улучшения сходимости итерационного процесса?",
                        Options = new List<string>
        {
            "Метод Гаусса",
            "Метод Жордана-Гаусса",
            "Метод релаксации",
            "Метод простой итерации"
        },
                        CorrectAnswerIndex = 2
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов является методом для решения СЛАУ с использованием матричных разложений?",
                        Options = new List<string>
        {
            "Метод Гаусса",
            "Метод LU-разложения",
            "Метод Жордана-Гаусса",
            "Метод простой итерации"
        },
                        CorrectAnswerIndex = 1
                    });
                    break;


                case 2: // Тест 3: Интерполяция и аппроксимация
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для интерполяции с помощью полинома Лагранжа?",
                        Options = new List<string>
        {
            "Интерполяционный полином Лагранжа",
            "Кубические сплайны",
            "Метод наименьших квадратов",
            "Метод простой итерации"
        },
                        CorrectAnswerIndex = 0
                    });
                    questions.Add(new Question
                    {
                        Text = "Какое из следующих определений соответствует кубическим сплайнам?",
                        Options = new List<string>
        {
            "Метод, который использует полиномы для интерполяции между точками",
            "Метод, который использует линейные функции для интерполяции",
            "Метод, который использует только один полином для всех точек",
            "Метод, который использует только кусочные линейные функции"
        },
                        CorrectAnswerIndex = 0
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для нахождения наилучшей аппроксимации данных?",
                        Options = new List<string>
        {
            "Интерполяционный полином Лагранжа",
            "Кубические сплайны",
            "Метод наименьших квадратов",
            "Метод простой итерации"
        },
                        CorrectAnswerIndex = 2
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих вариантов является формулой для полинома наименьших квадратов?",
                        Options = new List<string>
        {
            "y = a + bx",
            "y = a + b(x - x_0)^2",
            "y = a + b(x - x_0) + c(x - x_0)^2",
            "y = a + b(x - x_0) + c(x - x_0)^2 + d(x - x_0)^3"
        },
                        CorrectAnswerIndex = 2
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для интерполяции с помощью кусочных полиномов?",
                        Options = new List<string>
        {
            "Интерполяционный полином Лагранжа",
            "Кубические сплайны",
            "Метод наименьших квадратов",
            "Метод простой итерации"
        },
                        CorrectAnswerIndex = 1
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов используется для оценки ошибки аппроксимации?",
                        Options = new List<string>
        {
            "Метод наименьших квадратов",
            "Метод Лагранжа",
            "Метод сплайнов",
            "Метод простой итерации"
        },
                        CorrectAnswerIndex = 0
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для нахождения производной интерполяционного полинома?",
                        Options = new List<string>
        {
            "Метод Лагранжа",
            "Метод Ньютона",
            "Метод сплайнов",
            "Метод простой итерации"
        },
                        CorrectAnswerIndex = 1
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов используется для интерполяции на основе заданных узлов?",
                        Options = new List<string>
        {
            "Метод Лагранжа",
            "Метод Ньютона",
            "Кубические сплайны",
            "Все вышеперечисленные"
        },
                        CorrectAnswerIndex = 3
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для нахождения коэффициентов полинома наименьших квадратов?",
                        Options = new List<string>
        {
            "Интерполяционный полином Лагранжа",
            "Кубические сплайны",
            "Метод наименьших квадратов",
            "Метод простой итерации"
        },
                        CorrectAnswerIndex = 2
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов является методом для нахождения интерполяционного полинома?",
                        Options = new List<string>
        {
            "Метод Лагранжа",
            "Метод Ньютона",
            "Метод сплайнов",
            "Все вышеперечисленные"
        },
                        CorrectAnswerIndex = 3
                    });
                    break;


                    case 3: // Тест 4: Численное интегрирование
    questions.Add(new Question
    {
        Text = "Какой метод используется для численного интегрирования с помощью формул прямоугольников?",
        Options = new List<string>
        {
            "Формулы прямоугольников",
            "Формула трапеций",
            "Формула Симпсона",
            "Формулы Гаусса"
        },
        CorrectAnswerIndex = 0
    });
                    questions.Add(new Question
                    {
                        Text = "Какое из следующих утверждений верно для формулы трапеций?",
                        Options = new List<string>
        {
            "Использует значения функции в концах интервала",
            "Использует значения функции в середине интервала",
            "Использует только одно значение функции",
            "Не требует вычисления производной"
        },
                        CorrectAnswerIndex = 0
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для численного интегрирования с помощью формулы Симпсона?",
                        Options = new List<string>
        {
            "Формулы прямоугольников",
            "Формула трапеций",
            "Формула Симпсона",
            "Формулы Гаусса"
        },
                        CorrectAnswerIndex = 2
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для численного интегрирования с помощью формул Гаусса?",
                        Options = new List<string>
        {
            "Формулы прямоугольников",
            "Формула трапеций",
            "Формула Симпсона",
            "Формулы Гаусса"
        },
                        CorrectAnswerIndex = 3
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов используется для численного интегрирования с помощью средних значений?",
                        Options = new List<string>
        {
            "Формулы прямоугольников",
            "Формула трапеций",
            "Формула Симпсона",
            "Формулы Гаусса"
        },
                        CorrectAnswerIndex = 0
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод численного интегрирования использует параболы для аппроксимации функции?",
                        Options = new List<string>
        {
            "Формулы прямоугольников",
            "Формула трапеций",
            "Формула Симпсона",
            "Формулы Гаусса"
        },
                        CorrectAnswerIndex = 2
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов является более точным для интегрирования, чем формула трапеций?",
                        Options = new List<string>
        {
            "Формулы прямоугольников",
            "Формула Симпсона",
            "Формулы Гаусса",
            "Все вышеперечисленные"
        },
                        CorrectAnswerIndex = 1
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод численного интегрирования может использоваться для функций с высокой степенью гладкости?",
                        Options = new List<string>
        {
            "Формулы прямоугольников",
            "Формула трапеций",
            "Формула Симпсона",
            "Формулы Гаусса"
        },
                        CorrectAnswerIndex = 3
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов является методом для нахождения интеграла функции с использованием весов?",
                        Options = new List<string>
        {
            "Формулы прямоугольников",
            "Формула трапеций",
            "Формулы Гаусса",
            "Формула Симпсона"
        },
                        CorrectAnswerIndex = 2
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод численного интегрирования требует, чтобы функция была непрерывной на интервале интегрирования?",
                        Options = new List<string>
        {
            "Формулы прямоугольников",
            "Формула трапеций",
            "Формула Симпсона",
            "Все вышеперечисленные"
        },
                        CorrectAnswerIndex = 3
                    });
                    break;


                case 4: // Тест 5: Численное дифференцирование и решение ОДУ
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для решения ОДУ с помощью метода Эйлера?",
                        Options = new List<string>
        {
            "Метод Эйлера (явный)",
            "Метод Рунге-Кутты",
            "Метод Адамса",
            "Метод простой итерации"
        },
                        CorrectAnswerIndex = 0
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов является более точным, чем метод Эйлера?",
                        Options = new List<string>
        {
            "Метод Эйлера (явный)",
            "Метод Рунге-Кутты",
            "Метод Адамса",
            "Метод простой итерации"
        },
                        CorrectAnswerIndex = 1
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для решения ОДУ с помощью метода Адамса?",
                        Options = new List<string>
        {
            "Метод Эйлера (явный)",
            "Метод Рунге-Кутты",
            "Метод Адамса",
            "Метод простой итерации"
        },
                        CorrectAnswerIndex = 2
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для решения ОДУ с помощью неявного метода Эйлера?",
                        Options = new List<string>
        {
            "Метод Эйлера (неявный)",
            "Метод Рунге-Кутты",
            "Метод Адамса",
            "Метод простой итерации"
        },
                        CorrectAnswerIndex = 0
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов является модификацией метода Эйлера?",
                        Options = new List<string>
        {
            "Метод Эйлера (модифицированный)",
            "Метод Рунге-Кутты",
            "Метод Адамса",
            "Метод простой итерации"
        },
                        CorrectAnswerIndex = 0
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для численного дифференцирования функции?",
                        Options = new List<string>
        {
            "Метод Эйлера",
            "Метод конечных разностей",
            "Метод Рунге-Кутты",
            "Метод Адамса"
        },
                        CorrectAnswerIndex = 1
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов является методом для решения жестких ОДУ?",
                        Options = new List<string>
        {
            "Метод Эйлера",
            "Метод Рунге-Кутты",
            "Метод Адамса",
            "Метод Рунге-Кутты для жестких уравнений"
        },
                        CorrectAnswerIndex = 3
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для повышения точности решения ОДУ?",
                        Options = new List<string>
        {
            "Метод Эйлера",
            "Метод Рунге-Кутты",
            "Метод Адамса",
            "Все вышеперечисленные"
        },
                        CorrectAnswerIndex = 1
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой метод используется для решения ОДУ с переменными коэффициентами?",
                        Options = new List<string>
        {
            "Метод Эйлера",
            "Метод Рунге-Кутты",
            "Метод Адамса",
            "Метод вариации постоянных"
        },
                        CorrectAnswerIndex = 3
                    });
                    questions.Add(new Question
                    {
                        Text = "Какой из следующих методов является адаптивным методом для решения ОДУ?",
                        Options = new List<string>
        {
            "Метод Эйлера",
            "Метод Рунге-Кутты с адаптивным шагом",
            "Метод Адамса",
            "Метод простой итерации"
        },
                        CorrectAnswerIndex = 1
                    });
                    break;

            }
        }

        private async void DisplayQuestion()
        {
            if (currentQuestionIndex < questions.Count)
            {
                var question = questions[currentQuestionIndex];
                QuestionLabel.Text = question.Text;
                LoadOptions(question.Options);
            }
            else
            {
                // Сохранение результатов
                try
                {
                    // Инициализация DatabaseHelper
                    var dbPath = Path.Combine(FileSystem.AppDataDirectory, "testresults.db");
                    var dbHelper = new DatabaseHelper(dbPath); // Используем ваш класс Database

                    var result = new TestResult
                    {
                        TestIndex = 1, // Укажите правильный индекс теста
                        CorrectAnswersCount = correctAnswersCount,
                        DateTaken = DateTime.Now
                    };

                    // Сохранение результата в базе данных
                    await dbHelper.SaveResultAsync(result);

                    // Показать результаты на ResultPage
                    await Navigation.PushAsync(new ResultPage(correctAnswersCount, result.DateTaken));
                }
                catch (Exception ex)
                {
                    // Обработка ошибок
                    await Application.Current.MainPage.DisplayAlert("Ошибка", $"Не удалось сохранить результаты: {ex.Message}", "OK");
                }
            }
        }



        private void LoadOptions(List<string> options)
        {
            OptionsLayout.Children.Clear();
            for (int i = 0; i < options.Count; i++)
            {
                var button = new Button
                {
                    Text = options[i],
                    CommandParameter = i
                };
                button.Clicked += OnOptionSelected;
                OptionsLayout.Children.Add(button);
            }
        }

        private void OnOptionSelected(object sender, EventArgs e)
        {
            var button = sender as Button;
            selectedAnswerIndex = (int)button.CommandParameter;

            // Проверка правильности ответа
            if (selectedAnswerIndex == questions[currentQuestionIndex].CorrectAnswerIndex)
            {
                correctAnswersCount++; // Увеличиваем счетчик правильных ответов
            }

            DisplayAlert("Выбранный ответ", questions[currentQuestionIndex].Options[selectedAnswerIndex], "OK");
        }

        private async void OnNextQuestionClicked(object sender, EventArgs e)
        {
            if (selectedAnswerIndex == -1)
            {
                await DisplayAlert("Ошибка", "Пожалуйста, выберите ответ.", "OK");
                return;
            }

            currentQuestionIndex++;
            selectedAnswerIndex = -1; // Сбросить выбранный ответ
            DisplayQuestion();
        }
    }

public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }
    }
}