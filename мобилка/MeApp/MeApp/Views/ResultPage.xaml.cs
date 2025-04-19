using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        public ResultPage(int correctAnswersCount, DateTime dateTaken)
        {
            InitializeComponent();
            DisplayResults(correctAnswersCount, dateTaken);
            
        }

        private void DisplayResults(int correctAnswersCount, DateTime dateTaken)
        {
            ResultsLabel.Text = $"Вы ответили правильно на {correctAnswersCount} из 10 вопросов.";
            DateTakenLabel.Text = $"Дата прохождения: {dateTaken}";
        }

        private async void OnBackToTestSelectionClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync(); // Возвращаемся на страницу выбора тестов
        }
    }

    
}
