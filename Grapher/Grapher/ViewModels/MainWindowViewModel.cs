using Grapher.Common;
using Grapher.ViewModels.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Grapher.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private ViewModelBase _selectedViewModel;
        public ViewModelBase SelectedViewModel
        {
            get => _selectedViewModel;
            set => Set(value, ref _selectedViewModel);
        }

        public RelayCommand<Window> ExitCommand { get; set; }
        public RelayCommand<Window> MinimizeCommand { get; set; }
        public RelayCommand<string> SwitchMenuCommand { get; set; }

        public InfoViewModel InfoViewModel { get; private set; }
        public GraphViewModel GraphViewModel { get; private set; }
        public SLAUViewModel SLAUViewModel { get; private set; }
        public CalculateViewModel CalculateViewModel { get; private set; }
        public ODUViewModel ODUViewModel { get; private set; }
        public MainWindowViewModel()
        {
            ExitCommand = new RelayCommand<Window>(ExitHandler);
            MinimizeCommand = new RelayCommand<Window>(MinimizeHandler);
            SwitchMenuCommand = new RelayCommand<string>(SwitchMenuHandler);
            SLAUViewModel = new SLAUViewModel();
            CalculateViewModel = new CalculateViewModel();
            InfoViewModel = new InfoViewModel();
            GraphViewModel = new GraphViewModel(this);
            ODUViewModel = new ODUViewModel();
            CalculateViewModel.AddGraph(GraphViewModel);
        }

        private void SwitchMenuHandler(string tag)
        {
            switch (tag)
            {
                case "FAQ":
                    {
                        SelectedViewModel = InfoViewModel;
                        break;
                    }
                case "Graph":
                    {
                        SelectedViewModel = GraphViewModel;
                        break;
                    }
                case "Calculate":
                    {
                        SelectedViewModel = CalculateViewModel;
                        break;
                    }
                case "SLAU":
                    {
                        SelectedViewModel = SLAUViewModel;
                        break;
                    }
                case "ODU":
                    {
                        SelectedViewModel = ODUViewModel;
                        break;
                    }
            }
        }

        private void MinimizeHandler(Window window)
        {
            window.WindowState = WindowState.Minimized;
        }

        private void ExitHandler(Window window)
        {
            window.Close();
        }
    }
}
