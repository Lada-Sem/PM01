using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();           
            CreateTestButtons();
        }

            private void CreateTestButtons()
            {
                for (int i = 1; i <= 5; i++)
                {
                    var button = new Button
                    {
                        Text = $"Тест {i}"
                    };
                    button.Clicked += OnTestSelected;
                    TestButtonsLayout.Children.Add(button);
                }
            }

            private async void OnTestSelected(object sender, EventArgs e)
            {
                var button = sender as Button;
                int testIndex = int.Parse(button.Text.Split(' ')[1]) - 1; // Получаем индекс теста
                await Navigation.PushAsync(new QuestionPage(testIndex));
            }
    }
}