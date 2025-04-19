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
    public partial class DashboardPage : ContentPage
    {
        private string _dashboardContent;
        public string DashboardContent
        {
            get => _dashboardContent;
            set
            {
                if (_dashboardContent != value)
                {
                    _dashboardContent = value;
                    OnPropertyChanged();
                }
            }
        }
        public DashboardPage()
        {
            DashboardContent = "Выберите раздел.";
            InitializeComponent();
            BindingContext = Application.Current.MainPage.BindingContext;
        }
    }

    public class DashboardData
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}