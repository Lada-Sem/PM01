using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MorePage : ContentPage
    {
        private readonly DatabaseHelper _databaseHelper;

        public MorePage()
        {
            InitializeComponent();
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "testresults.db");
            _databaseHelper = new DatabaseHelper(dbPath);
            LoadResults();
        }   

        private async void LoadResults()
        {
            var results = await _databaseHelper.GetResultsAsync();

            // Создаем список строк для отображения в ListView
            var resultStrings = results.Select(result =>
                new
                {
                    ResultText = $"Тест {result.TestIndex}: {result.CorrectAnswersCount} правильных ответов, дата: {result.DateTaken}"
                }).ToList();

            // Устанавливаем ItemsSource для ListView
            ResultsListView.ItemsSource = resultStrings;
        }
    }
}